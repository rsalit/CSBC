<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptDivisionDraftList.aspx.cs" Inherits="CSBC.Admin.Web.Reports.rptDivisionDraftList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Draft List</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-sm-10 col-sm-offset-1 center-block">
                <h2 class="text-center">Coral Springs Basketball Club</h2>
                <h2 class="text-center">Draft List </h2>
                <h3 class="text-center"><%= Session["SeasonName"] + " - " + Session["DivisionName"] %></h3>

            </div>
            <div class="col-sm-10 col-sm-offset-1">
                <asp:GridView ID="gridPlayers" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-striped table-condensed table-bordered"
                    RowStyle-CssClass="td small"
                    HeaderStyle-CssClass="th"
                    ItemType="CSBC.Core.Models.SeasonPlayer"
                    AutoPostBack="True">
                    <EmptyDataTemplate>No Games Found!</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>ID</HeaderTemplate>
                            <HeaderStyle Width="20px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblId"
                                    runat="server"
                                    Text='<%# Item.DraftID %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Rating</HeaderTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblRating"
                                    runat="server"
                                    Text='<%# Item.Rating %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Name</HeaderTemplate>
                            <HeaderStyle Width="140px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblName"
                                    runat="server"
                                    Text='<%# Item.Name %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="140px"></ItemStyle>
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
                            <HeaderStyle Width="60px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPhone"
                                    runat="server"
                                    Text='<%# Item.Phone %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Grade</HeaderTemplate>
                            <HeaderStyle Width="30px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblGrade"
                                    runat="server"
                                    Text='<%# Item.Grade %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Draft Notes</HeaderTemplate>
                            <HeaderStyle Width="120px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDraftNotes"
                                    runat="server"
                                    Text='<%# Item.DraftNotes %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Balance</HeaderTemplate>
                            <HeaderStyle Width="40px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBalance"
                                    runat="server"
                                    Text='<%# Item.Balance == 0 ? "" : Item.Balance.ToString("c") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
