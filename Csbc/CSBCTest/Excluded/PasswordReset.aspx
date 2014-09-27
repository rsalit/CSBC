<%@ Page Language="VB" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="false" CodeFile="PasswordReset.aspx.vb" Inherits="PasswordReset" title="Reset Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" style="width: 402px; height: 162px">
        <tr>
            <td align="center" colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 295px">
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 467px">
                <asp:Label ID="Label2" runat="server" Text="User Name:"></asp:Label></td>
            <td align="left" style="width: 234px">
                <asp:Label ID="lblUserName" runat="server" Width="192px"></asp:Label></td>
        </tr>
        <tr>
            <td align="right" style="width: 467px; height: 28px">
                <asp:Label ID="lblCurrent" runat="server" Text="Current Password:"></asp:Label></td>
            <td align="left" style="width: 234px; height: 28px">
                <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="15" TabIndex="1" TextMode="Password"
                    Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 467px; height: 28px">
                <asp:Label ID="lblNewPassword" runat="server" Text="New Password:"></asp:Label></td>
            <td align="left" style="width: 234px; height: 28px">
                <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="15" TabIndex="2" TextMode="Password"
                    Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 467px; height: 28px">
                <asp:Label ID="lblConfirm" runat="server" Text="Confirm New Password:"></asp:Label></td>
            <td align="left" style="width: 234px; height: 28px">
                <asp:TextBox ID="txtConfirm" runat="server" MaxLength="15" TabIndex="3" TextMode="Password"
                    Width="190px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="center">
                <asp:Button ID="btnSubmit" runat="server" TabIndex="4" Text="Update" Width="104px" /></td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

