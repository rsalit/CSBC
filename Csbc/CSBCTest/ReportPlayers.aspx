<%@ Page Language="VB" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="false" CodeFile="ReportPlayers.aspx.vb" Inherits="ReportPlayers" title="Players Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 765px; height: 198px">
        <tr>
            <td align="center" rowspan="9" colspan="2" valign="top" style="width: 257px">
                &nbsp;</td>
            <td align="left" style="width: 85px; height: 26px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Navy"
                    Text="Season registered players that:" Width="241px"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="width: 85px; height: 26px">
                <asp:CheckBoxList ID="chkFilters" runat="server" Width="241px">
                    <asp:ListItem>Paid</asp:ListItem>
                    <asp:ListItem>Active</asp:ListItem>
                    <asp:ListItem>Insured</asp:ListItem>
                    <asp:ListItem>Residents</asp:ListItem>
                </asp:CheckBoxList></td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
                <asp:Button ID="btnReport" runat="server" Text="View Report" Width="104px" /></td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
                </td>
        </tr>
        <tr>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1" style="width: 257px">
            </td>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1" style="width: 257px">
            </td>
            <td style="width: 85px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" rowspan="1" valign="top">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#C00000" Width="735px"></asp:Label></td>
        </tr>
    </table>
</asp:Content>


