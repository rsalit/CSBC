<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailForm.ascx.cs" Inherits="CSBC.Admin.Web.MailForm" %>

<div class="panel panel-primary">
    <div class="panel-header">
        <div class="panel-title">Announcements</div>
    </div>
    <div class="panel-body">
        <div class="form-group">
            <label for="lblFrom" class="control-label">From</label>
            <asp:Label ID="lblFrom" runat="server" CssClass="control-form"></asp:Label>
        </div>
        <div class="form-group">
            <label for="cboTo" class="control-label">From</label>
            <asp:DropDownList ID="cboTo" runat="server" TabIndex="1" CssClass="form-control dropdown"
                AutoPostBack="True">
                <asp:ListItem Value="0">Board Members</asp:ListItem>
                <asp:ListItem Value="1">Season Candidates Only</asp:ListItem>
                <asp:ListItem Value="2">Season Players</asp:ListItem>
                <asp:ListItem Value="3">Season Coaches/Sponsors</asp:ListItem>
                <asp:ListItem Value="4">Members(All seasons)</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtSubject" class="control-label">From</label>
            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Label ID="lblUsernameCaption" runat="server" Text="Content:" Width="86px"></asp:Label>
        <div class="form-group">
            <label for="htmlMail" class="control-label">From</label>
            <asp:TextBox ID="htmlMail" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200px"></asp:TextBox>
        </div>
        <asp:ListBox ID="lstEmails" runat="server" Height="280px" SelectionMode="Multiple"
					Width="330px" Font-Size="Small" Font-Names="Courier New"></asp:ListBox>
				
    </div>
    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="600px"></asp:Label>
</div>
