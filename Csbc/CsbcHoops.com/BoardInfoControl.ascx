<%@ Control Language="VB" AutoEventWireup="false" Inherits="CSBCHOOPS_COM.BoardInfoControl" Codebehind="BoardInfoControl.ascx.vb" %>
    <div>
    <asp:Panel ID="pnlMain" runat="server" Font-Names="Courier New" Font-Size="XX-Small"
        Height="60px" Width="760px" Wrap="False">
        <table id="Table1" border="0" bordercolor="lightgrey" cellpadding="1" cellspacing="0"
            style="font-size: xx-small; width: 760px; font-family: tahoma; height: 18px">
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Width="220px"></asp:Label></td>
                <td style="width: 180px">
                    <asp:Label ID="lblName" runat="server" Width="180px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 220px; height: 18px">
                    <asp:Label ID="lblPhone" runat="server" Width="220px"></asp:Label></td>
                <td style="width: 143px; height: 18px">
                    <asp:LinkButton ID="lnkEmail" runat="server" Height="20px" Width="232px"></asp:LinkButton></td>
            </tr>
        </table>
        <table>
        <tr>
        <td>====================================================================================================================================
        </td>
        </tr>
        </table>
    </asp:Panel>
</div>