using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure;
using CSBC.Components;
using CSBC.Admin.Web.ViewModels;
using CSBC.Core.Models;
using CSBC.Core.Repositories;
using CSBC.Core.Data;
using System.Diagnostics.Contracts;

namespace CSBC.Admin.Web
{
    public partial class Colors1 : BaseForm
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            Session["Title"] = "Colors";
            base.Page_Load(sender, e);
            if (Page.IsPostBack == false)
            {
                if (Session["AccessType"].ToString() == "R")
                {
                    btnSave.Enabled = false;
                }
                this.txtName.Focus();

                //LoadList();
            }

        }

        protected override void SetUser()
        {
            base.SetUser();
            if (Session["AccessType"] == "R")
            {
                btnSave.Enabled = false;
                btnNew.Enabled = false;
                //btnSend.Enabled = false;
            }
        }

        private void LoadRow(long RowID)
        {
            //Website.ClsColors oColor = new Website.ClsColors();
            //DataTable rsData = default(DataTable);
            ColorVM oColors = new ColorVM();
            List<ColorVM> rsData = default(List<ColorVM>);
            try
            {
                //rsData = oColor.LoadColors(RowID, Session["CompanyID"])
                if ((rsData != null))
                {
                    if (rsData.Count > 0)
                    {
                        lblID.Text = rsData[0].ID.ToString();
                        txtName.Text = rsData[0].ColorName;
                        chkDiscontinue.Checked = (bool)rsData[0].Discontinued;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "LoadRow::" + ex.Message;
            }
        }

        private void LoadList()
        {
            ColorVM oColors = new ColorVM();
            List<ColorVM> rsData = default(List<ColorVM>);

            try
            {
                /*
                rsData = oColors.GetRecords((int)Session["CompanyID"]);
                grdColors.Columns.Clear();
                if (rsData.Count > 0)
                {
                   
                    grdColors.DataSource = rsData;
                    grdColors.DataBind();
                    grdColors.Columns[0].Visible = false;
                */
                //    (0).].Visible = false;
                //.FromKey("ColorName").Header.Caption = "Color Name"
                //.FromKey("bDiscontinued").Hidden = True
                //.FromKey("sDiscontinued").Header.Caption = "Discontinued"
                //.FromKey("sDiscontinued").CellStyle.HorizontalAlign = HorizontalAlign.Center

            }
            catch (Exception ex)
            {
                lblError.Text = "LoadList::" + ex.Message;
            }
            finally
            {
                oColors = null;
            }
        }
        public IQueryable<Color> GetAllRecords()
        {
            Contract.Ensures(Contract.Result<IQueryable<Color>>() != null, "result is null.");
            var rep = new ColorRepository(new CSBCDbContext());
            var colors = rep.GetAll(1);

            return colors;
        }

        private void UpdRow(long RowID)
        {
            /* Website.ClsColors oColors = new Website.ClsColors();
             try
             {
                 var _with2 = oColors;
                 _with2.Descr = txtName.Text;
                 _with2.bDiscontinue = chkDiscontinue.Checked;
                 oColors.UpdRow(RowID, Session["CompanyID"], Session["TimeZone"]);
             }
             catch (Exception ex)
             {
                 Session["ErrorMSG"] = "UpdRow::" + ex.Message;
             }
             finally
             {
                 oColors = null;
             }
             * */
        }

        private void ClearFields()
        {
            grdColors.Controls.Clear();
            txtName.Text = "";
            txtName.Focus();
            chkDiscontinue.Checked = false;
            Session["ErrorMsg"] = "";
            Session["ColorID"] = 0;
        }

        private void ADDRow()
        {
            /*
            if (Session["AccessType"] == "R")
                return;
            Website.ClsColors oColors = new Website.ClsColors();
            try
            {
                var _with3 = oColors;
                _with3.Descr = txtName.Text;
                _with3.CreatedUser = Session["UserName"];
                _with3.bDiscontinue = chkDiscontinue.Checked;
                _with3.UpdRow(0, Session["CompanyID"], Session["TimeZone"]);
                Session["ColorID"] = _with3.ColorID;
            }
            catch (Exception ex)
            {
                Session["ErrorMSG"] = ex.Message;
            }
            finally
            {
                oColors = null;
            }
            */
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //if ((int)Session["ColorID"] > 0)
            //{
            //    UpdateColor();
            //}
            //else

            AddColor();
            //GetAllRecords();
            ClearFields();
        }

        private void UpdateColor()
        {

        }

        private void AddColor()
        {
            var colorName = txtName.Text;
            if (!String.IsNullOrEmpty(colorName))
            {
                try
                {
                    var color = new Color
                    {
                        ColorName = colorName,
                        Discontinued = chkDiscontinue.Checked,
                        CompanyID = Master.CompanyId,
                        CreatedDate = DateTime.Today,
                        CreatedUser = Master.UserName
                    };

                    ColorVM.Insert(color);
                }
                catch (Exception e)
                {
                    MasterVM.MsgBox(this, "Error " + e.InnerException.ToString());

                }

            }

        }

        private bool errorRTN()
        {
            bool functionReturnValue = false;
            functionReturnValue = false;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                if (Session["USERACCESS"].ToString() == "R")
                {
                    lblError.Text = "Update Not allowed";
                }
                else
                {
                    lblError.Text = "Name missing ";
                }
                functionReturnValue = true;
            }
            return functionReturnValue;
        }

        protected void grdColors_SelectedRowsChange(object sender, EventArgs e)
        {
            /*
            Session["ColorID"] = grdColors.DisplayLayout.ActiveRow.Cells.FromKey("ID").Value;
            if (Session["ColorID"] > 0)
            {
                LoadRow(Session["ColorID"]);
                btnSave.Enabled = true;
            }
            if (Session["AccessType"] == "R")
            {
                btnSave.Enabled = false;
            }
             * */
        }

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            ClearFields();
            Session["ColorID"] = 0;
            txtName.Focus();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void grdColors_UpdateItem(int id)
        {
            using (var context = new CSBC.Core.Data.CSBCDbContext())
            {
                var rep = new ColorRepository(context);
                Color item = null;
                item = rep.GetById(id);
                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                }
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void grdColors_DeleteItem(int id)
        {
            using (var context = new CSBC.Core.Data.CSBCDbContext())
            {
                var rep = new ColorRepository(context);

                var item = rep.GetById(id);
                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                    return;
                }
                if (HasColorBeenUsed(id))
                {
                    MasterVM.MsgBox(this, "Can't delete color that has  been used!");
                }
                else
                {
                    rep.Delete(item);
                }
            }
        }

        private bool HasColorBeenUsed(int id)
        {
            return ColorVM.ColorUsed(id); ;
        }

        
    }
}