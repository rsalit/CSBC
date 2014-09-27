<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Households1.aspx.cs" Inherits="CSBC.Admin.Web.Households1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Household Info
                    </div>
                </div>
                <div class="panel-body">

                    <div class="form-group col-md-6">
                        <label class="control-label control-label" for="txtName">
                            Household Name</label>
                        <asp:TextBox ID="txtName" runat="server" class="form-control" TabIndex="1"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <a href="searchHouse1.aspx"><span class="btn btn-default pull-right">Search</span></a>
                    </div>

                    <div class="form-group col-md-8">
                        <label class="control-label" for="txtAddress">
                            Address
                        </label>
                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" TabIndex="3">
                        </asp:TextBox>
                    </div>

                    <div class="form-group col-md-8">
                        <label class="control-label control-label" for="txtAddress2">
                            Address 2
                        </label>
                        <asp:TextBox ID="txtAddress2" runat="server" class="form-control " TabIndex="5"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-5">
                        <label class="control-label control-label" for="lblCity">
                            City
                        </label>
                        <asp:TextBox ID="txtCity" runat="server" class="form-control" TabIndex="6"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2">
                        <label for="txtState" class="control-label control-label">State</label>
                        <asp:TextBox ID="txtState" runat="server" class="form-control" TabIndex="7"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <label class="control-label control-label" for="txtZip">Zip</label>
                        <asp:TextBox ID="txtZip" runat="server" TextMode="Number" class="form-control" TabIndex="8"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-5">
                        <label class="control-label control-label" for="lblPhone">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" TextMode="Phone" placeholder="999-999-9999" TabIndex="9"></asp:TextBox>

                    </div>

                    <div class="form-group col-md-8">
                        <label class="control-label control-label" for="lblEmail">
                            <asp:Label ID="lblEmail" runat="server" placeholder = "abc@abcd.com" Text="Email:"></asp:Label>
                        </label>

                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email" TabIndex="10"></asp:TextBox>
                    </div>


                    <div class="col-md-3 checkbox">
                        <asp:CheckBox ID="chkEmail" runat="server" TabIndex="9" Text="Mailing List" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtCityCard" class="control-label control-label">Sports Card</label>
                        <asp:TextBox ID="txtCityCard" runat="server" class="form-control" TabIndex="14"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblUserNameCaption" runat="server" Text="UserName:" Width="180px"></asp:Label>
                        <asp:Label ID="lblUserName" runat="server" Width="254px"></asp:Label>
                    </div>
                </div>
                <div class="panel-footer btn-group">
                    
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_OnClick" TabIndex="15" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_OnClick" TabIndex="16" />
                    <asp:Label ID="lblDelete" runat="server" Visible="False" Width="88px" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
        <!-- <div class="col-md-4">
                              
                <asp:ListBox ID="listHouseHolds" runat="server" AutoPostBack="True" CssClass="" Height="100%" 
                    DataTextField="Name"
                    DataValueField="HouseID"
                    ItemType="CSBC.Core.Models.Household">
                   
                    
                </asp:ListBox>
                   </div>
            </div>-->


        <div class=" col-md-5">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Household Members
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 300px">
                    <asp:GridView ID="grdMembersNew" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-bordered"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.Person"
                        DataKeyNames="PeopleID"
                        OnRowCommand="grdMembers_OnRowCommand">
                        <Columns>
                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>Last Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkLastName" runat="server" Text='<%# Item.LastName%>'
                                        CommandName="Select"
                                        CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>First Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkFirstName" runat="server" Text='<%# Item.FirstName%>'
                                        CommandName="Select"
                                        CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="BirthDate" HeaderText="DOB" ControlStyle-Width="20px" DataFormatString="{0:d}" ShowHeader="True" />
                            <asp:BoundField DataField="Gender" HeaderText="Gender" ControlStyle-Width="12px" ShowHeader="True" />
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
                <div class="panel-footer">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Household Member" CssClass="btn btn-primary" OnClick="btnAdd_OnClick" TabIndex="17" />

                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <asp:LinkButton ID="btnComments" runat="server" TabIndex="20">Comments</asp:LinkButton>
        <asp:TextBox ID="txtComments" runat="server" Height="131px" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="600px"></asp:Label>
    </div>
    <asp:HiddenField ID="hidEMail" runat="server" />
</asp:Content>


