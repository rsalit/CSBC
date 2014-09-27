<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="SearchRegPay.aspx.cs" Inherits="CSBC.Admin.Web.SearchRegPay" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary ">
                <div class="panel-heading clearfix">
                    <div class="panel-title col-md-8 pull-left">Season Registrations</div>
                    <div class="form-group col-md-4 pull-right">
                        <asp:DropDownList ID="dropDownDivisions" runat="server" CssClass="form-control dropdown small"
                            OnSelectedIndexChanged="dropDownDivisions_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 400px">
                    <asp:GridView ID="grdPlayers" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        OnRowCommand="grdPlayers_RowCommand"
                        DataKeyNames="PlayerID"
                        ItemType="CSBC.Core.Models.SeasonPlayer">
                        <EmptyDataTemplate></EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ShowHeader="True">
                                <HeaderTemplate>Last Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button2" runat="server"
                                        HeaderText="Player Name"
                                        CausesValidation="false"
                                        CommandName="Select"
                                        Text='<%# Item.Name  %>'
                                        CommandArgument='<%# Item.PeopleID  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True">
                                <HeaderTemplate>Draft ID</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDraftId" runat="server"
                                        Text='<%# Item.DraftID %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True">
                                <HeaderTemplate>Division</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDivision" runat="server"
                                        HeaderText="Division"
                                        CausesValidation="false"
                                        CommandName="Select"
                                        Text='<%# Item.Div_Desc %>'
                                        CommandArgument='<%# Item.DivisionID  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>


                </div>
                <div class="panel-footer btn-group ">

                    <asp:Button ID="btnRegister" runat="server"
                        Text="New Registration"
                        OnClick="btnRegister_Click"
                        CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
