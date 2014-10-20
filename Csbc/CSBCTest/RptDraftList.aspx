<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="RptDraftList.aspx.cs" Inherits="CSBC.Admin.Web.RptDraftList" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <%-- <div id="btnPrint">
        <asp:ImageButton runat="server"  CssClass="btn btn-primary"/>--%>

    <div class="row">
        <div class="col-sm-8 form-group">
            <label for="ddlDivisions" class="control-label">Division</label>
            <asp:DropDownList ID="ddlDivisions" runat="server"
                AutoPostBack="true"
                CssClass="dropdown col-sm-4 col-sx-6"
                OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
        <div class="col-sm-2 pull-right">
            <asp:Button runat="server" ID="btnPrint" Text="Print" CssClass="btn glyphicon-print btn-primary" OnClick="btnPrint_Click" />
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="gridPlayers" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-condensed table-bordered"
            RowStyle-CssClass="td"
            HeaderStyle-CssClass="th"
            ItemType="CSBC.Core.Models.SeasonPlayer"
            AutoPostBack="True">
            <EmptyDataTemplate>No Games Found!</EmptyDataTemplate>
            <Columns>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>ID</HeaderTemplate>
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblId"
                            runat="server"
                            Text='<%# Item.DraftID %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Rating</HeaderTemplate>
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblRating"
                            runat="server"
                            Text='<%# Item.Rating %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Name</HeaderTemplate>
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblName"
                            runat="server"
                            Text='<%# Item.Name %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>BirthDate</HeaderTemplate>
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblBirthDate"
                            runat="server"
                            Text='<%# Item.BirthDate.ToShortDateString() %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Phone</HeaderTemplate>
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPhone"
                            runat="server"
                            Text='<%# Item.Phone %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Grade</HeaderTemplate>
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblGrade"
                            runat="server"
                            Text='<%# Item.Grade %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Draft Notes</HeaderTemplate>
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDraftNotes"
                            runat="server"
                            Text='<%# Item.DraftNotes %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="true">
                    <HeaderTemplate>Balance</HeaderTemplate>
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblBalance"
                            runat="server"
                            Text='<%# Item.Balance == 0 ? "" : Item.Balance.ToString("c") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
