<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Board.aspx.cs" Inherits="CSBC.Admin.Web.Board" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <section class="col-sm-6 col-xs-12">
            <div class="panel panel-primary">
                <header class="panel-heading">
                    <div class="panel-title">
                        Board Member Detail
                    </div>
                </header>

                <div class="panel-body row">
                    <div id="dropDownGroupVolunteers" class="col-md-10">
                        <label for="txtlblBoard" class="control-label">Board Members (Volunteers)</label>
                        <asp:DropDownList ID="cboBM" runat="server"
                            CssClass="form-control dropdown"
                            OnSelectedIndexChanged="cboBM_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-10">
                        <label for="txtTitle" class="control-label">Title</label>
                        <asp:TextBox ID="txtTitle" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-10">
                        <label for="cobPhones" class="control-label">Preferred Phones</label>
                        <asp:DropDownList ID="cobPhones" runat="server" class="form-control dropdown">
                        </asp:DropDownList>

                    </div>
                    <div id="emailHide" class="col-md-4 col-sm-offset-1 checkbox-inline">
                        <asp:CheckBox ID="chkEmail" runat="server" Text="Hide Email"  CssClass="checkbox" />
                       <%-- --%>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtSequence" class="control-label">Sequence No</label>
                        <asp:TextBox ID="txtSequence" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                    </div>
                    <div id="Addressinfo" class="well col-sm-offset-1 col-sm-8 center-block">

                        <asp:Label ID="lblName" runat="server" ></asp:Label>
                        <br />
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblCSZ" runat="server" ></asp:Label>
                        <br />
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblEmail" runat="server"  ></asp:Label>
                    </div>
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#C00000" Width="735px"></asp:Label>
                    <asp:Label ID="lblDeleteBM" runat="server" Font-Size="Small" ForeColor="Red" Width="100px" Font-Bold="True"></asp:Label>
                </div>
                <div class="panel-footer btn-group">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click" />


                </div>
            </div>
        </section>
        <section class="col-sm-6 col-xs-12">
            <div class="panel panel-primary">
                <header class="panel-heading">
                    <div class="panel-title">
                        Board Member List
                    </div>
                </header>
                <div style="overflow-y: scroll; height: 420px">
                    <asp:GridView ID="grdBM" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table  table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        DataKeyNames="ID"
                        ItemType="CSBC.Core.Models.vw_Directors"
                        OnRowCommand="grdBM_RowCommand">
                        <Columns>
                            <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:LinkButton ID="linkID" runat="server"
                                        Text='<%# Bind("Seq") %>'
                                        CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle CssClass="th"></HeaderStyle>

                        <RowStyle CssClass="td"></RowStyle>

                    </asp:GridView>
                </div>
            </div>
        </section>
    </div>
    </asp:Content>
