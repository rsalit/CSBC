using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using CSBC.Components;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using System.Text.RegularExpressions;

namespace CSBC.Admin.Web
{
    public partial class EmailBlast : BaseForm
    {


        private string ErrorMsg;

        private int EmailsSent;

        protected override void Page_Load(object sender, System.EventArgs e)
        {
            Session["Title"] = "Announcements";
            base.Page_Load(sender, e);

            if (Page.IsPostBack == false)
            {

                //lblFrom.Text = Session["EmailSender"].ToString();
                if (Master.AccessType == "R")
                {
                    btnSend.Enabled = false;
                }

                this.txtSubject.Focus();
                LoadEmails(0);
            }
        }


        protected override void SetUser()
        {
            base.SetUser();
            if (Master.AccessType == "R")
            {
                //btnSave.Enabled = false;
                //btnNew.Enabled = false;
                btnSend.Enabled = false;
            }
        }

        private void LoadEmails(Int32 iGroupType)
        {
            var rep = new HouseholdRepository(new CSBCDbContext());
            
            try
            {
                var rsData = rep.LoadEmails(iGroupType, Master.CompanyId, Master.SeasonId);
                lstEmails.Items.Clear();
                if (rsData.Rows.Count > 0)
                {
                    lstEmails.DataValueField = "Email";
                    lstEmails.DataTextField = "Name";
                    lstEmails.DataSource = rsData;
                    lstEmails.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadEmails::" + ex.Message;
            }
            finally
            {
              
            }
        }

        private void ClearFields()
        {
            for (Int32 I = 0; I <= lstEmails.Items.Count - 1; I++)
            {
                lstEmails.Items[I].Selected = false;
            }
            txtSubject.Text = "";
            txtSubject.Focus();
            //txtBody.Text = ""
            cboTo.SelectedValue = "0";
            Session["ErrorMsg"] = "";
        }

        private void btnSend_Click(System.Object sender, System.EventArgs e)
        {
            if (errorRTN() == false)
                SendEmails();
            if (string.IsNullOrEmpty(lblError.Text))
            {
                for (Int32 i = 0; i <= lstEmails.Items.Count - 1; i++)
                {
                    lstEmails.Items[i].Selected = false;
                }
            }
        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = true;
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                lblError.Text = "Subject missing ";
                txtSubject.Focus();
                return functionReturnValue;
            }
            if (lstEmails.Items.Count == 0)
            {
                lblError.Text = "Recipients missing ";
                cboTo.Focus();
                return functionReturnValue;
            }
            if (string.IsNullOrEmpty(htmlMail.Text))
            {
                lblError.Text = "Email content missing ";
                return functionReturnValue;
            }
            functionReturnValue = false;
            return functionReturnValue;
        }

        private void SendEmails()
        {
            System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
            SmtpClient oSmtp = new SmtpClient("mail.csbchoops.com");
            oSmtp = new SmtpClient("mail.csbchoops.com");
            oSmtp.Host = "mail.csbchoops.com";
            oSmtp.Credentials = new System.Net.NetworkCredential("registration@csbchoops.com", "csbc0910");
            oSmtp.Port = 25;

            for (Int32 i = 0; i <= lstEmails.Items.Count - 1; i++)
            {
                if (IsEmail(lstEmails.Items[i].Value) == true)
                {
                    oEmail = new System.Net.Mail.MailMessage();
                    oEmail.From = new System.Net.Mail.MailAddress("registration@csbchoops.com");
                    oEmail.To.Add(lstEmails.Items[i].Value);
                    oEmail.IsBodyHtml = true;
                    oEmail.Body = htmlMail.Text;
                    oEmail.Subject = txtSubject.Text;
                    if (GoodEmail(oSmtp, oEmail) == true)
                        EmailsSent += 1;
                    oEmail.Dispose();
                    oEmail = null;
                }
            }

            if (EmailsSent == 0)
            {
                lblError.Text = ErrorMsg + " (0) Email(s) sent!";
            }
            else
            {
                lblError.Text = "(" + EmailsSent + ") Email(s) sent!";
            }
            txtSubject.Text = "";
            htmlMail.Text = "";
        }

        private bool GoodEmail(SmtpClient oSmtp, MailMessage oEmail)
        {
            bool functionReturnValue = true;
            try
            {
                oSmtp.Send(oEmail);
            }
            catch (Exception ex)
            {
                ErrorMsg = "Unable to send mail!  " + ex.Message;
                functionReturnValue = false;
            }
            return functionReturnValue;
        }


        private bool IsEmail(string Email)
        {
            bool functionReturnValue = false;
            string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            Match emailAddressMatch = Regex.Match(Email, pattern);
            if (emailAddressMatch.Success)
            {
                functionReturnValue = true;
            }
            else
            {
                functionReturnValue = false;
            }
            return functionReturnValue;
        }

        protected void cboTo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadEmails(cboTo.SelectedItem.Value);
        }

        private void LoadEmails(string p)
        {
            throw new NotImplementedException();
        }

    }
}

