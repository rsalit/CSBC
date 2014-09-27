using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using CSBC.Components;
using System.Text.RegularExpressions;

namespace CSBC.Admin.Web
{
    public partial class MailForm : System.Web.UI.UserControl
    {

        private string ErrorMsg;

        private int EmailsSent;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblFrom.Text = Session["EmailSender"].ToString();
               
                this.txtSubject.Focus();
                LoadEmails(0);
            }
        }



        private void LoadEmails(Int32 iGroupType)
        {
            using (var db = new CSBC.Core.Data.CSBCDbContext())
            {

            }
            //Profile.ClsHouseholds oEmails = new Profile.ClsHouseholds();
            //DataTable rsData = default(DataTable);
            //try {
            //    rsData = oEmails.LoadEmails(iGroupType, Session["CompanyID"], Session["SeasonID"]);
            //    lstEmails.Items.Clear();
            //    if (rsData.Rows.Count > 0) {
            //        //   |||||   Set the DataValueField to the Primary Key
            //        lstEmails.DataValueField = "Email";
            //        //   |||||   Set the DataTextField to the text/data you want to display
            //        lstEmails.DataTextField = "Name";
            //        //   |||||   Set the DataSource the the OleDBDataReader's result
            //        lstEmails.DataSource = rsData;
            //        //   |||||   Bind the Source Data to the Web/Server Control
            //        lstEmails.DataBind();

            //    }
            //} catch (Exception ex) {
            //    lblError.Text = "LoadEmails::" + ex.Message;
            //} finally {
            //    oEmails = null;
            //}
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
                for (Int32 I = 0; I <= lstEmails.Items.Count - 1; I++)
                {
                    lstEmails.Items[I].Selected = false;
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

        public void MsgBox(string Message)
        {
            Label strScript = new Label();
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message + "')</script>";
            Page.Controls.Add(strScript);
        }

        private void SendEmails()
        {
            System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
            SmtpClient oSmtp = new SmtpClient("mail.csbchoops.net");
            oSmtp = new SmtpClient("mail.csbchoops.net");
            oSmtp.Host = "mail.csbchoops.net";
            oSmtp.Credentials = new System.Net.NetworkCredential("registrar@csbchoops.net", "csbc0317");
            oSmtp.Port = 25;

            for (Int32 I = 0; I <= lstEmails.Items.Count - 1; I++)
            {
                if (IsEmail(lstEmails.Items[I].Value) == true)
                {
                    oEmail = new System.Net.Mail.MailMessage();
                    oEmail.From = new System.Net.Mail.MailAddress("registration@csbchoops.net");
                    oEmail.To.Add(lstEmails.Items[I].Value);
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
            bool functionReturnValue = false;
            functionReturnValue = true;
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
        //Private Sub SendEmails()
        //    Dim emailCount As Integer = 0
        //    Dim BatchCount As Integer = 0

        //    For I As Int32 = 0 To lstEmails.Items.Count - 1
        //        emailCount += 1
        //        BatchCount += 1
        //        If (emailCount Mod 20 = 0) Then
        //            SendBatch(emailCount - 20, I)
        //            If lblError.Text > "" Then
        //                lblError.Text += " Count = " & emailCount & " Batch:" & BatchCount
        //                Exit Sub
        //            End If
        //            BatchCount = 0
        //        End If
        //    Next

        //    'This will send the last batch if it was less that the max per batch
        //    If emailCount Mod 20 = 0 = False Then
        //        SendBatch(lstEmails.Items.Count - BatchCount, lstEmails.Items.Count - 1)
        //    End If
        //    'If emailCount < lstEmails.Items.Count - 1 Or emailCount < 50 Then
        //    '    SendBatch(lstEmails.Items.Count - emailCount, lstEmails.Items.Count - 1)
        //    'End If

        //    If lblError.Text = "" Then lblError.Text = "(" & EmailsSent & ") Email(s) sent!"
        //    txtSubject.Text = ""
        //    htmlMail.Text = ""
        //End Sub

        //Private Sub SendBatch(ByVal iFrom As Int32, ByVal iTo As Int32)
        //    Dim oEmail As New System.Net.Mail.MailMessage()
        //    Dim oSmtp As New SmtpClient("mail.csbchoops.net")
        //    Try
        //        oSmtp.Host = "mail.csbchoops.net"
        //        oSmtp.Credentials = New System.Net.NetworkCredential("registrar@csbchoops.net", "0317")
        //        oSmtp.Port = 25

        //        For I As Int32 = iFrom To iTo
        //            If IsEmail(lstEmails.Items(I).Value) = True Then
        //                oEmail.Bcc.Add(lstEmails.Items(I).Value)
        //                EmailsSent += 1
        //            End If
        //        Next

        //        oEmail.From = New System.Net.Mail.MailAddress("registrar@csbchoops.net")
        //        oEmail.IsBodyHtml = True
        //        oEmail.Subject = txtSubject.Text
        //        oEmail.Body = htmlMail.Text
        //        oSmtp.Send(oEmail)
        //        oSmtp = Nothing
        //        oEmail.Dispose()
        //        oEmail = Nothing
        //    Catch ex As Exception
        //        lblError.Text = "Unable to send mail!  " & ex.Message
        //    End Try
        //End Sub

        //move this to 
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
            LoadEmails(Convert.ToInt32(cboTo.SelectedItem.Value));
        }

    }
}