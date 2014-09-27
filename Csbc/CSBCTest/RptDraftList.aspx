<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="RptDraftList.aspx.cs" Inherits="CSBC.Admin.Web.RptDraftList" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <%-- <div id="btnPrint">
        <asp:ImageButton runat="server"  CssClass="btn btn-primary"/>--%>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Style="margin-right: 63px" Width="677px">
        <LocalReport ReportEmbeddedResource="CSBC.Admin.Web.DraftListReport.rdlc" ReportPath="reports/DraftListReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="DraftListDataSource" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="DraftListDataSource" runat="server" SelectMethod="GetSeasonPlayers" TypeName="CSBC.Admin.Web.ViewModels.PlayerVM">
        <SelectParameters>
            <asp:Parameter DefaultValue="70" Name="seasonId" Type="Int32"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
