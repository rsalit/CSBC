<%@ Page Language="C#" AutoEventWireup="true" Inherits="CSBC.Admin.Web.Login" CodeBehind="Login.aspx.cs" Title="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coral Springs Basketball Club - Login</title>
    <link href="~/Content/bootstrap.css" rel="stylesheet" media="screen" />
    <style type="text/css">
        body {
            padding-top: 30px;
        }
    </style>
    <link href="~/Content/bootstrap-responsive.css" rel="stylesheet" />
    <link href="~/Content/body.css" rel="stylesheet" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <meta name="description" content="Login Page for CSBC Hoops" />
</head>
<body>
    <div class="container center-block">
        <img style="height: 109px; text-align:center;display:block;margin:auto;" alt="" src="Images/topCSBC.jpg" />

        <br />
        <br />
        <form class="form-signin center-block" runat="server" role="form">
            <h3 class="text-center">Please sign in</h3>
            <div class="form-group">
                
                <div>
                    <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" CssClass="form-control"></asp:TextBox>
                </div>

            </div>
            <div class="form-group center-block">
                
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control">
                </asp:TextBox>
            </div>

            <asp:Button class="btn btn-primary btn-lg btn-block" type="button" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CommandName="CommandBtn_Click"
                Text="Log In" UseSubmitBehavior="True" CommandArgument="Submit" />
            <br />
            <div class="row">
                <asp:LinkButton ID="lnkPassword" runat="server" Font-Size="Small">Forgot Password?</asp:LinkButton>
            </div>
           <!-- <div class="row border">
                <div class="col-md-4"><asp:CheckBox ID="checkTestMode" runat="server" CssClass="checkbox" Text="Test Mode"></asp:CheckBox></div>
                <div class="col-md-8"><p class="bg-info">Check this box to indicate you wish to operate in test mode. This will allow you to edit data 
                    without affecting the live database</p></div>
            </div>-->
            <div class="container">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
            </div>
        </form>
    </div>

</body>
</html>
