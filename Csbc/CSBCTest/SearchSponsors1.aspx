<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="SearchSponsors1.aspx.cs" Inherits="CSBC.Admin.Web.SearchSponsors1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sponsor Not in Season List
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 400px">
                    <asp:GridView ID="grdSearchSponsor" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.SponsorProfile"
                        OnRowCommand="grdSearchSponsor_RowCommand"
                        ShowHeader="true">
                        <EmptyDataTemplate>
                            No Data Found.  

                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sponsor's Name" ShowHeader="true">
                                <HeaderTemplate>Sponsor Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSponsorNo" runat="server" Text='<%# Item.SpoName %>'
                                        CommandName="SelectSponsor"
                                        CommandArgument='<%# Item.SponsorProfileID %>'></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAddToSeason" runat="server" 
                                        Text="Add To Season" 
                                        CssClass="btn btn-default btn-group-sm"
                                        CommandName="AddSponsor" 
                                        CommandArgument='<%# Item.SponsorProfileID %>'/>
                                </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
                <div class="panel-footer">

                    <label for="txtSponsorName">Sponsor Name</label>
                    <asp:TextBox ID="txtSponsorName" runat="server" CssClass="form-control"></asp:TextBox>
                    <div class="btn-group">

                        <asp:Button ID="btnAdd" Text="Add New Sponsor" runat="server" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Season Sponsor List
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 400px">
                    <asp:GridView ID="gridSponsorsInSeason" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.SponsorProfile"
                        OnRowCommand= "grdSearchSponsor_RowCommand"
                        ShowHeader="true">
                        <EmptyDataTemplate>
                            No Data Found.  

                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ShowHeader="false">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRemoveFromSeason" runat="server" 
                                        Text="Remove From Season" 
                                        CommandName="RemoveSponsor" 
                                        CommandArgument='<%# Item.SponsorProfileID %>'/>
                                </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sponsor's Name" ShowHeader="true">
                                <HeaderTemplate>Sponsor Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSponsorNo" runat="server" Text='<%# Item.SpoName %>'
                                        CommandName="SelectSponsor"
                                        CommandArgument='<%# Item.SponsorProfileID %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
</asp:Content>
