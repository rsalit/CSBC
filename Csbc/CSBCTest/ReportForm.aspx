<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="CSBC.Admin.Web.ReportForm" %>

<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="form-group col-md-4">
        <asp:DropDownList ID="ddlReports"  AutoPostBack="True" CssClass="dropdown form-control" OnSelectedIndexChanged="ddlReports_OnSelectedIndexChanged" runat="server" />
    </div>
    <div class="col-md-4">
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_OnClick" CssClass="btn btn-primary" />
    </div>
    <div class="col-md-12">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Style="margin-right: 63px" Width="100%">
            <LocalReport ReportEmbeddedResource="CSBC.Admin.Web.DraftListReport.rdlc" ReportPath="reports/DraftListReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="DataSourceId" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
