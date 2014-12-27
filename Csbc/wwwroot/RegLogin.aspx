<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="RegLogin.aspx.vb" Inherits="_RegLogin" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="imgStandings" runat="server" BorderStyle="Ridge" BorderWidth="1px"
                ImageUrl="images\BKShoot.bmp" />
        </div>
        <div class="col-md-8">
            <asp:Panel ID="pnlLogin" runat="server" CssClass="col-md-8">
                <div class="form-group ">
                    <asp:Label ID="lblUsername" runat="server" CssClass="control-label">UserName:</asp:Label>
                    <input id="txtUser" runat="server" autocomplete="on" maxlength="12" name="txtUser"
                        tabindex="1" class="form-control"
                        type="text" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server" Height="16px" Width="40px">Password:</asp:Label>
                    <input id="txtPassword" runat="server" autocomplete="off" maxlength="12" name="txtPassword"
                        class="form-control" tabindex="2" type="password" /><input
                            id="btnLogin" runat="server" name="Login In" class="btn btn-primary"
                            tabindex="3" type="submit" value="Login" onclick="return btnLogin_onclick()" />
                </div>
            </asp:Panel>
        
        <div class="row">
            <div class="well col-md-8">
                 <asp:LinkButton ID="lnkPassReset" runat="server" Height="24px" CssClass="btn-link">Forgot Password?</asp:LinkButton>
                <br/>
                <asp:LinkButton ID="lnkEmail" runat="server" CssClass="btn-link">Forgot UserName?</asp:LinkButton>
                <p class="text-info">
                    Online First Time users: 
                                            <asp:LinkButton ID="lnkNewUser" runat="server" CssClass="btn-link">Click Here</asp:LinkButton>
                </p>
            </div>
            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="Red"
                Height="64px" 
                Width="408px"></asp:Label>
            <div class="col-md-8">
                <p>
                    This is a secure site, you need to use Internet Explorer (IE).
                </p>
                <p>
                    Never share your password with anyone.
                </p>
            </div>
           
            
            </div>
            <asp:Label ID="label1" runat="server" BorderStyle="Solid" BorderWidth="0px" Font-Bold="True"
                Font-Names="Arial Narrow" Font-Size="Medium" ForeColor="Black" Height="24px"
                Width="355px">Registration is not available!</asp:Label>
            <asp:HyperLink ID="lnkWebmaster" runat="server" Font-Size="Small" ForeColor="RoyalBlue"
                Height="16px" NavigateUrl="mailto:webmaster@csbchoops.net" CssClass="btn-link"
                Target="_blank">Send Email to Webmaster</asp:HyperLink>
            <%--<asp:Label ID="label2" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="0px"
                    Font-Size="Medium" ForeColor="Black" Height="72px" Style="z-index: 110; left: 8px; position: absolute; top: 48px"
                    Width="354px">Come back later...</asp:Label>--%>
            <asp:Label ID="lblBrowser" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                Height="32px"
                Width="512px"></asp:Label>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        function btnLogin_onclick() {

        }

        // ]]>
    </script>
</asp:Content>
