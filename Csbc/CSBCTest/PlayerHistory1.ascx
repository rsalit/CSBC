<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlayerHistory1.ascx.cs" Inherits="CSBC.Admin.Web.PlayerHistory1" %>

<div style="overflow-y: scroll; max-height: 200px">
    <%--<table class="table table-bordered table-condensed table-striped">
        <tr>
            <th>Season</th>
            <th>Coach</th>
            <th>Rating</th>
            <th>Balance</th>
        </tr>
        <% foreach (var row in PlayerHistoryList)
           { %>
        <tr>
            <td><%= row.Season %> </td>
            <td><%= row.Coach %> </td>
            <td><%= row.Rating %> </td>
            <td><%= row.BalanceOwed %> </td>

            

        </tr>
         <% } %>
    </table>--%>
    <asp:GridView ID="gridPlayerHistory" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped table-condensed"
        RowStyle-CssClass="td"
        HeaderStyle-CssClass="th"
        ItemType="CSBC.Core.Models.PlayerHistory">
        <EmptyDataTemplate>No results found</EmptyDataTemplate>
        <Columns>
                <asp:BoundField DataField="Season" HeaderText="Season" />
                <asp:BoundField DataField="Coach" HeaderText="Coach" />
                <%--<asp:BoundField DataField="Rating" HeaderText="Rating" />--%>
                <asp:BoundField DataField="BalanceOwed" DataFormatString="{0:C}" HeaderText="Balance" />            
        </Columns>

        <HeaderStyle CssClass="th"></HeaderStyle>

        <RowStyle CssClass="td"></RowStyle>
    </asp:GridView>
</div>
