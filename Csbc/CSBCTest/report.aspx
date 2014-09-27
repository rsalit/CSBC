<%@ page language="C#" autoeventwireup="true" inherits="CSBC.Admin.Web.Report" CodeBehind="Report.aspx.cs" %>
<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" Runat="server" AutoDataBind="True"
            Height="947px" ReportSourceID="CrystalReportSource1" Width="845px" />
     </div>
    </form>
</body>
</html>
