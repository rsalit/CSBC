using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
    public partial class Accounting1 : BaseForm
    {
        public int PaymentId {
            get
            {
                return Convert.ToInt32(Session["PaymentId"]);
            }
            set
            {
                Session["PaymentId"] = value;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            Session["Title"] = "Sponsors";
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                LoadSponsors();
                if (Master.SponsorProfileId > 0)
                {
                    cmbSponsorNames.SelectedValue = Master.SponsorProfileId.ToString();
                    LoadPayments(Master.SponsorProfileId);
                }
            }

        }

        protected void cmbSponsorNames_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var sponsorProfileId = Convert.ToInt32(cmbSponsorNames.SelectedValue);
            LoadPayments(sponsorProfileId);
            ClearFields();
        }

        private void ClearFields()
        {
            txtPaymentDate.Text = String.Empty;
            txtPaymentAmount.Text = String.Empty;
            txtCheckNo.Text = String.Empty;
        }

        protected void grd_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var paymentId = Convert.ToInt32(e.CommandArgument);
            PopulateRecord(paymentId);
        }

        private void PopulateRecord(int paymentId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SponsorPaymentRepository(db);
                var payment = rep.GetById(paymentId);
                txtPaymentDate.Text = payment.TransactionDate.Value.ToShortDateString();
                txtPaymentAmount.Text = payment.Amount.ToString();
                txtCheckNo.Text = payment.TransactionNumber;
                radPayment.SelectedIndex = SetPaymentType(payment.PaymentType);
                PaymentId = paymentId;
            }
        }

        private int SetPaymentType(string paymentType)
        {
            switch (paymentType)
            {
                case "CHECK":
                    return 0;
                case "CC":
                    return 1;
                case "ONLINE":
                    return 2;
                case "CASH":
                    return 3;
                default:
                    return 0;
            }
        }

        protected void LoadSponsors()
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SponsorProfileRepository(db);
                var sponsors = rep.GetAll(Master.CompanyId).ToList<SponsorProfile>();
                cmbSponsorNames.DataSource = sponsors;
                cmbSponsorNames.DataValueField = "SponsorProfileId";
                cmbSponsorNames.DataTextField = "SpoName";
                cmbSponsorNames.DataBind();
            }
        }
        protected void LoadPayments(int sponsorProfileId)
        {
            using (var context = new CSBCDbContext())
            {
                var rep = new SponsorPaymentRepository(context);
                var payments = rep.GetSponsorPayments(sponsorProfileId);
                grd.DataSource = payments;
                grd.DataBind();

            }
        }

        protected void btnAddPayment_Click(object sender, EventArgs e)
        {
            ClearFields();
            PaymentId = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new SponsorPaymentRepository(db);
                rep.Insert(
                    new SponsorPayment
                    {
                        Amount = Convert.ToDecimal(txtPaymentAmount.Text),
                        TransactionDate = Convert.ToDateTime(txtPaymentDate.Text),
                        CompanyID = Master.CompanyId,
                        TransactionNumber = txtCheckNo.Text,
                       PaymentType = GetPaymentType(radPayment.SelectedValue),
                       SponsorProfileID = Convert.ToInt32(cmbSponsorNames.SelectedValue),
                       CreatedDate = DateTime.Today,
                       CreatedUser = Master.UserName,
                       PaymentID = PaymentId
                    });
                LoadPayments(Master.SponsorProfileId);
            }
        }

        private string GetPaymentType(string paymentType)
        {
           switch  (paymentType.ToUpper())
           {
                case "0" :
                    return "CHECK";
                case "1":
                    return "CC";
                case "2":
                    return "ONLINE";
                case "3":
                    return "CASH";
                default:
                    return "CC";
            }
        }


    }
}