<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Welcome1.aspx.cs" Inherits="CSBC.Admin.Web.Welcome1" %>
<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-9 col-md-offset-1">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Summary Info</div>
                </div>

                <asp:GridView ID="grdTotals"
                    RowStyle-CssClass="tbl tbl-striped"
                    CssClass="table table-bordered"
                    HeaderStyle-CssClass="th"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="DivisionID"
                    ShowHeaders="true">
                    <Columns>
                        <asp:BoundField DataField="DivisionID" HeaderText="ID" ShowHeader="true">
                            <ItemStyle Width="20px" CssClass="text-center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Div_Desc" HeaderText="Division" ShowHeader="true">
                            <ItemStyle Width="150px" CssClass="text-center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Totals">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Totals
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="text-center" HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Coaches">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Coaches") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Coaches
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" CssClass="text-center"  Text='<%# Bind("Coaches") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sponsors">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Sponsors") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Sponsors
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server"  CssClass="text-center" Text='<%# Bind("Sponsors") %>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Online Total">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="text-center"  Text='<%# Bind("TotalOR") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Online Totals
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server"  CssClass="text-center"  Text='<%# Bind("TotalOR") %>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Online Coaches" ShowHeader="true">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Coaches") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Online Coaches
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server"  CssClass="text-center"  Text='<%# Bind("Coaches") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Online Sponsors">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("SponsorsOR") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Online Sponsors
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server"  CssClass="text-center" Text='<%# Bind("SponsorsOR") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle CssClass="th"></HeaderStyle>

                    <RowStyle CssClass="tbl tbl-striped"></RowStyle>
                </asp:GridView>

            </div>
            <asp:Label ID="lblError" runat="server" class="has-error"></asp:Label>&nbsp;&nbsp;
        </div>
    </div>
</asp:Content>
