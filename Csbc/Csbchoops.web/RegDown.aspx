<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegDown.aspx.cs" Inherits="Csbchoops.Web.RegDown" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Page</title>
    <link href="content/bootstrap.css" rel="stylesheet" />
    <link href="content/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div id="header">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td valign="top" align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="headerTop">
                                <tr>
                                    <td valign="top" align="left">
                                        <div id="logo">
                                            <a href="index.html">
                                                <img src="images/spacer.gif" alt="" width="404" height="84" border="0" /></a>
                                        </div>
                                    </td>
                                    <td align="right" valign="middle"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:MainMenu runat="server" ID="MainMenu" />

                            </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-6 col-md-offset-3">
                    <h1 class="whiteText center-block">On line Registration</h1>
                    <p class="lead whiteText">On line registration is not currently available. We are working on it and hope to have it back soon!</p>

                </div>

            </div>
        </div>
    </form>
</body>
</html>