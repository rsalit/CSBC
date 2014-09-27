<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="SearchHouse1.aspx.cs" Inherits="CSBC.Admin.Web.SearchHouse1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-4">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Search Criteria
                    </div>
                </div>
                <div class="panel-body">
                    <div class="text-info">Enter as much as you know to identify household (partial information is OK)</div>
                    <div class="form-group col-md-10">
                        <label class="" for="txtLastName">
                            Family Name
                        </label>

                        <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                    </div>


                    <div class="form-group  col-md-10">
                        <label class="" for="txtAddress">Address</label>

                        <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group  col-md-10">
                        <label class="" for="txtPhone">Phone No.</label>

                        <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group  col-md-10">
                        <label class="" for="txtEmail">Email</label>

                        <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="btn-group">
                        <asp:Button ID="btnSearch2" runat="server" Text="Search" class="btn btn-primary btn-default" OnClick="btnSearch_Click" />

                        <asp:Button ID="btnNew" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnNew_Click" />
                    </div>
                </div>
            </div>


            <asp:Label ID="lblError" runat="server" class="label-danger"></asp:Label>
        </div>
  
        <div class=" col-md-8">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Search Results
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 400px">

                    <asp:GridView ID="grdHouseholds" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="HouseID"
                        OnRowCommand="grdHouseholds_OnRowCommand"
                        ItemType="CSBC.Core.Models.Household">
                        <RowStyle CssClass="table" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10px" ShowHeader="true" HeaderText="Name">
                                <HeaderTemplate>Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkName" runat="server" Text='<%# Item.Name%>'
                                        CommandName="Select"
                                        CommandArgument='<%# Item.HouseID%>'></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle Width="60px" />
                                <ItemStyle Width="60px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address1") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="60px" />
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>

    </div>
    
</asp:Content>
