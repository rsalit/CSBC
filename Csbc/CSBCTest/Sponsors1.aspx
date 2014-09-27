<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Sponsors1.aspx.cs" Inherits="CSBC.Admin.Web.Sponsors1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Sponsor Info</div>
                </div>
                <div class="panel-body">

                    <div class="form-group col-md-5">
                        <label for="txtSponsorName" class="control-label">Sponsor Name</label>

                        <asp:TextBox ID="txtSponsorName" runat="server" MaxLength="50" TabIndex="1" CssClass="form-control"></asp:TextBox>
                    </div>
                   
                    <div class="form-group col-md-5">
                        <label for="txtContact" class="control-label">Contact Name</label>
                        <asp:TextBox ID="txtContact" runat="server" MaxLength="50" TabIndex="3" CssClass="form-control"></asp:TextBox>
                    </div> 
                    <div class="col-md-2">
                        <a href='<%= Master.SearchSponsors %>'<span class="btn btn-primary">Search</span></a>
                        
                    </div>
                    <div class="form-group col-md-10">
                        <label for="txtAddress" class="control-label">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" AutoCompleteType="Disabled" MaxLength="50" TabIndex="4" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtCity" class="control-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" TabIndex="5" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2">
                        <label for="txtState" class="control-label">State</label>
                        <asp:TextBox ID="txtState" runat="server" MaxLength="2" TabIndex="6" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtZip" class="control-label">Zip</label>
                        <asp:TextBox ID="txtZip" runat="server" MaxLength="5" TabIndex="7" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtPhone" class="control-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" TabIndex="8" CssClass="form-control glyphicon-phone input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-5">
                        <label for="txtUniformName" class="control-label">Name on Shirt</label>
                        <asp:TextBox ID="txtUniformName" runat="server" TabIndex="9" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="cmbSizes" class="control-label">Size</label>
                        <asp:DropDownList ID="cmbSizes" runat="server" TabIndex="12" CssClass="form-control dropdown input-sm">
                            <asp:ListItem Selected="True" Value="N/A">N/A</asp:ListItem>
                            <asp:ListItem Value="SMALL">SMALL</asp:ListItem>
                            <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                            <asp:ListItem Value="LARGE">LARGE</asp:ListItem>
                            <asp:ListItem Value="X-LARGE">X-LARGE</asp:ListItem>
                            <asp:ListItem Value="XX-LARGE">XX-LARGE</asp:ListItem>
                            <asp:ListItem Value="3X-LARGE">3X-LARGE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="cmbColors" class="control-label">Color 1</label>
                        <asp:DropDownList ID="cmbColors" runat="server" TabIndex="10" CssClass="form-control dropdown input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="cmbColors2" class="control-label">Color 2</label>
                        <asp:DropDownList ID="cmbColors2" runat="server" TabIndex="11" CssClass="form-control dropdown input-sm">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group col-md-6">
                        <label for="txtWebsite" class="control-label">Website</label>
                        <asp:TextBox ID="txtWebsite" runat="server" Width="250px" MaxLength="50" TabIndex="13" CssClass="form-control input-sm" EnableTheming="True"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-5">
                       <label for="txtEmail" class="control-label">Color 2</label>
                        <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="50" TabIndex="14" CssClass="form-control input-sm"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-5">
                        <label for="cmbFees" class="control-label">Season Fee</label>
                        <asp:DropDownList ID="cmbFees" runat="server" TabIndex="15" CssClass="form-control dropdown">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="lblBalance" class="control-label">Balance</label>
                        <asp:Label ID="lblBalance" runat="server" CssClass="text-info"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton ID="btnPayments" runat="server" TabIndex="16" 
                            PostBackUrl= "~/Accounting1.aspx">Payments</asp:LinkButton>
                    </div>
                    <div class="form-group col-md-10">
                        <asp:TextBox ID="txtComments" runat="server" Height="52px" TextMode="MultiLine" Width="353px" CssClass="form-control "></asp:TextBox>
                        <asp:LinkButton ID="lnkComments" runat="server" Height="20px" TabIndex="16" Width="183px">Comments:</asp:LinkButton>
                    </div>
                </div>
                <div class="panel-footer">
                    
                    <asp:Button ID="btnAddNew" runat="server" TabIndex="22" Text="New Sponsor" CssClass="btn btn-primary" OnClick="btnAddNew_Click" />
                    
                    <asp:Button ID="btnSave" runat="server" TabIndex="22" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    
                    <asp:Button ID="btnAddToSeason" runat="server" TabIndex="22" Text="Add To Season" CssClass="btn btn-primary" OnClick="btnAddToSeason_Click" />
                    
                    <asp:Button ID="btnDelete" runat="server" TabIndex="23" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_OnClick" />
                </div>

            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Current Season Sponsors  
                    </div>
                </div>
                <div class="panel-body">
                    <asp:LinkButton ID="lnkPrint" runat="server" Height="20px" TabIndex="20" Width="95px" Font-Size="Small">Print Sponsors</asp:LinkButton>

                    <div style="overflow-y: scroll; height: 240px">
                        <asp:GridView ID="grdSponsors" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-condensed"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            DataKeyNames="SponsorID"
                            ItemType="CSBC.Core.Models.Sponsor"
                            OnRowCommand="grd_OnRowCommand"
                            ShowHeader="true">
                            <EmptyDataTemplate>
                                No Data Found.  

                            </EmptyDataTemplate> 
                            <Columns>
                                <asp:TemplateField HeaderText="Sponsor's Name" ShowHeader="true">
                                    <HeaderTemplate>Sponsor Name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSponsorNo" runat="server" Text='<%# Item.SponsorProfile.SpoName %>'
                                            CommandName="SelectSponsor"
                                            CommandArgument='<%# Item.SponsorID %>'></asp:LinkButton>
                                    </ItemTemplate>

                                    <ItemStyle Width="10px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!--<input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                            type="button" value="OK" />&nbsp;
                                    <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                        type="button" value="Cancel" />  -->

                </div>


            </div>     
            <!-- Season Players -->
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Current Season Players     
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 200px">
                    <asp:GridView ID="grdPlayers" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        OnRowCommand="grd_OnRowCommand"
                        DataKeyNames="PlayerID"
                        ItemType="CSBC.Core.Models.SeasonPlayer">
                        <EmptyDataTemplate>
                            No Data Found.  

                        </EmptyDataTemplate> 
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Player Name" ShowHeader="True" />
                            <asp:TemplateField ShowHeader="True">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button2" runat="server"
                                        CausesValidation="false"
                                        CommandName="Add"
                                        Text="Add"
                                        CommandArgument='<%# Bind("PlayerID")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>       
            <!-- Sponsor Kids -->
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sponsors' Kids             
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 120px">
                    <asp:GridView ID="grdKids" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        OnRowCommand="grd_OnRowCommand"
                        DataKeyNames="PlayerID"
                        ItemType="CSBC.Core.Models.SeasonPlayer">
                        <EmptyDataTemplate>
                            No Data Found.  

                        </EmptyDataTemplate>

                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Player Name" ShowHeader="True" />
                            <asp:TemplateField ShowHeader="True">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Button2" runat="server"
                                        CausesValidation="false"
                                        CommandName="Remove"
                                        Text="Remove"
                                        CommandArgument='<%# Bind("PlayerID")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
            Height="51px" Visible="False" Width="88px"></asp:Label>



        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
            Width="748px"></asp:Label>
</asp:Content>


