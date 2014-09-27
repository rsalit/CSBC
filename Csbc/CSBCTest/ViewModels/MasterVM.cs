using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CSBC.Components;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSBC.Admin.Web.ViewModels
{
    public class MasterVM
    {

        public MasterVM()
        {

        }
        public static string AccessType(int userId, string screenName, int companyId, int seasonId)
        {
            string functionReturnValue = null;
            var oSecurity = new CSBC.Components.Security.ClsUsers();
            try
            {
                oSecurity.GetAccess(userId, screenName, companyId, seasonId);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
            }
            finally
            {
                functionReturnValue = oSecurity.AccessType;
                oSecurity = null;
            }
            return functionReturnValue;
        }
        public static void MsgBox(Page page, string Message)
        {
            Label strScript = new Label();
            //strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "window.alert('" + Message + "')</script>";
            strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "toastr.success('" + Message + "', Error);</script>";
            page.Controls.Add(strScript);
        }

        public enum MessageTypes { Success, Error, Info };
        //introduce messageType - could be enumerated type in the future.
        public static void MsgBox(Page page, string Message, MessageTypes messageType)
        {
            Label strScript = new Label();

            switch (messageType)
            {
                case MessageTypes.Success:

                    strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "toastr.success('" + Message + "');</script>";
                    break;
                case MessageTypes.Error:
                    strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "toastr.error('" + Message + "');</script>";
                    break;
                default:
                    strScript.Text = "<SCRIPT LANGUAGE='JavaScript'>" + Environment.NewLine + "toastr.info('" + Message + "');</script>";
                    break;
            }
            page.Controls.Add(strScript);
        }
    }
}