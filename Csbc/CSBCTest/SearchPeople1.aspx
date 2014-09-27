<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="SearchPeople1.aspx.cs" Inherits="CSBC.Admin.Web.SearchPeople1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="panel-title">Search People</div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="txtLastName" class="control-label">Last Name</label>
                            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtFirstName" class="control-label">First Name</label>
                            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                         <div class="form-group checkbox checkbox-inline ">
                            <%--<label for="checkPlayersOnly" class="control-label">Players Only</label>--%>
                            <asp:CheckBox ID="checkPlayersOnly" CssClass="checkbox checkbox-inline form-control" runat="server" Text="Players Only"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="panel-footer btn-group">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                       <%-- <asp:Button ID="btnAdd" runat="server" Text="New Person" CssClass="btn btn-primary" OnClick="btnAdd_Click" />--%>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="panel-title">Search Results</div>
                    </div>
                    <div style="overflow-y: scroll; height: 500px">
                        <asp:GridView ID="grdPeople" runat="server"
                            AutoGenerateColumns="false"
                            CssClass="table table-bordered"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            ItemType="CSBC.Core.Models.Person"
                            OnRowCommand="grdPeople_RowCommand">
                            <EmptyDataTemplate>No results found</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                    <HeaderTemplate>Last Name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkLastName" runat="server" Text='<%# Item.LastName%>'
                                            CommandName="Select"
                                            CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                    <HeaderTemplate>First Name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFirstName" runat="server" Text='<%# Item.FirstName%>'
                                            CommandName="Select"
                                            CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                    <HeaderTemplate>Cell Phone</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lnkPhone" runat="server" Text='<%# Item.Cellphone%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                    <HeaderTemplate>DOB</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Item.BirthDate==null? "": Item.BirthDate.Value.ToShortDateString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRegister" runat="server" Text="Register"
                                            CssClass="btn btn-xs btn-danger"
                                            CommandName="Register"
                                            CommandArgument='<%# Item.PeopleID%>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
