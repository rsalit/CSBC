<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CSBC.Admin.Web.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-3">
            <section id="Users" class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">User List</div>
                </div>
                <div class="panel-body">
                    
                        <asp:RadioButtonList runat="server" ID="radioFilterUserType"  
                            AutoPostBack="true" 
                            CssClass="radio radio-inline"
                            OnSelectedIndexChanged="radioFilterUserTypes_SelectedIndexChanged">
                            <asp:ListItem Value="5" >All</asp:ListItem>
                            <asp:ListItem Value="0">Public Website</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">Board Member</asp:ListItem>
                            <asp:ListItem Value="3">Administrator</asp:ListItem>
                        </asp:RadioButtonList>
                  
                    <div style="overflow-y: scroll; height: 400px">
                        <asp:GridView ID="gridUsers" runat="server"
                            AutoGenerateColumns="false"
                            CssClass="table table-bordered table-condensed"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            DataKeyNames="UserID"
                            ItemType="CSBC.Admin.Web.ViewModels.UserVM"
                            OnRowCommand="gridUsers_RowCommand"
                            ShowHeader="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="10px" ShowHeader="true">
                                    <HeaderTemplate>Name</HeaderTemplate>
                                    <HeaderStyle Width="40"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkName" runat="server" Text='<%# Item.Name %>'
                                            CommandName="Select"
                                            CommandArgument='<%# Item.UserId  %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle CssClass="th"></HeaderStyle>

                            <RowStyle CssClass="td"></RowStyle>
                        </asp:GridView>
                    </div>
                </div>
            </section>
        </div>
        <div class="col-md-8">
            <section id="userdetails" class="panel panel-primary ">
                <div class="panel-heading">
                    <div class="panel-title">User Detail</div>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="lblId" runat="server" />
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group col-md-8">
                                <label id="Household" class="control-label">Household</label>
                                <asp:Label ID="lblHouseHold" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <%--<div class="form-group col-md-3">
                                <asp:ImageButton ID="imgName" runat="server" ImageUrl="~/Images/SEARCH.JPG"
                                    TabIndex="1" ToolTip="Search By Name" Width="20px" />
                            </div>--%>
                            <div class="form-group col-md-8">
                                <label id="lblEmailCaption" for="lblEmail" class="control-label">Email</label>
                                <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-md-8">
                                <label id="lblName" for="txtName" class="control-label">Name</label>
                                <asp:TextBox ID="txtName" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-8">
                                <label id="lblUsernameCaption" for="txtUserName" class="control-label">User Name </label>
                                <asp:TextBox ID="txtUserName" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-8">
                                <label id="lblPasswordCaption" for="txtPWord" class="control-label">Password</label>
                                <asp:TextBox ID="txtPWord" runat="server" Font-Bold="True" TabIndex="6" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6">
                                <label id="lblUserType" class="control-label" for="radioUserType">User Type</label>
                                <asp:RadioButtonList ID="radioUserType" runat="server" CssClass="radio-inline">
                                    <asp:ListItem Value="0">Public Website</asp:ListItem>
                                    <asp:ListItem Value="2">Board Member</asp:ListItem>
                                    <asp:ListItem Value="3">Administrator</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <div class="col-md-3 well">
                            <label id="lblRoles" class="control-label" for="listRoles">User Type</label>
                            <asp:CheckBoxList ID="listRoles"
                                CssClass="checkbox-inline"
                                runat="server"
                                OnSelectedIndexChanged="listRoles_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:CheckBoxList>
                        </div>

                        <asp:Label ID="lblDeleteUser" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                            Height="61px" Width="89px"></asp:Label>
                    </div>
                    <asp:Label ID="lblHouseID" runat="server" Font-Bold="True" Width="10px" Visible="False"></asp:Label>

                </div>
                <div class="panel-footer">
                    <div class="btn-group">
                       <%-- <asp:Button ID="btnNew" runat="server" Text="New" TabIndex="7" CssClass="btn btn-primary" OnClick="btnNew_Click" />--%>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="8" OnClick="btnSave_Click" />
                       <%-- <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Delete"  OnClick="btnDelete_Click"/>--%>
                    </div>
                </div>
            </section>
        </div>

    </div>

    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="600px"></asp:Label>
</asp:Content>
