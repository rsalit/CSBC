<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Csbchoops.Web.ContactUs" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>

<!DOCTYPE html>

<html>

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Contact Us</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="content/style.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            background-color:black;
        }
        .boardContainer {
            width: 100%;
            border-bottom:1px solid #746e85;
            padding-left: 5%;
            background-color: #f7e4ca;
            align-content:center;
            padding-top: 10px;
            padding-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
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
                                    <td valign="top">
                                        <uc1:MainMenu runat="server" ID="MainMenu" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="box">
                        <h1 class="boxHeadingcenter">Board Members</h1>
                    </td>
                </tr>
                <tr id="graybox">
                    <td align="left" colspan="2" style="width: 743px; height: 71px">
                        <div>
                            <asp:Repeater ID="repBoard" runat="server">
                                <HeaderTemplate>
                                 
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <div class="boardContainer">

                                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Text='<%# Eval("Title") %>' Width="220px">

                                        </asp:Label>
                                        <asp:Label ID="lblName" runat="server" Width="180px" Text='<%# Eval("People.FirstName") + " " + Eval("People.LastName") %>'></asp:Label>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("People.CellPhone") %>' Width="220px"></asp:Label>
                    <asp:LinkButton ID="lnkEmail" runat="server" Height="20px" Text='<%# Eval("People.Email") %>'></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>


                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                        Width="749px"></asp:Label>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="footer">
                            <div class="footerLink">
                            </div>


                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
