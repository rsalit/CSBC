<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Division1.aspx.cs" Inherits="CSBC.Admin.Web.Division1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Division Info</div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label for="txtName" class="control-label">
                                    Name</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control "
                                    TabIndex="1"></asp:TextBox>
                                <asp:HiddenField ID="lblDivisionID" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group ">
                                <label for="txtMinDate" class="control-label">Min Date:</label>
                                <asp:TextBox ID="txtMinDate" runat="server" TabIndex="2" TextMode="Date"  CssClass="form-control ">
                                </asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group ">
                                <label for="txtMaxDate" class="control-label">Max Date:</label>
                                <asp:TextBox ID="txtMaxDate" TabIndex="3" runat="server" TextMode="Date" CssClass="form-control date">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4 ">
                            <div class="radio">
                                <asp:RadioButtonList ID="radGender" runat="server"
                                    RepeatColumns="1" RepeatLayout="Flow" TabIndex="4">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group ">
                                <label for="txtMinDate" class="control-label">Min Date 2:</label>
                                <asp:TextBox ID="txtMinDate2" runat="server" TabIndex="5" TextMode="Date" CssClass="form-control">
                                </asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-4" >
                            <div class="form-group ">
                                <label for="txtMaxDate" class="control-label">Max Date 2:</label>
                                <asp:TextBox ID="txtMaxDate2" runat="server" TextMode="Date" CssClass="form-control date">
                                </asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-4 ">
                            <div class="radio">
                                <asp:RadioButtonList ID="radGender2" runat="server" CssClass="radio-inline"
                                    RepeatColumns="1" RepeatLayout="Flow" TabIndex="4" TextAlign="Left">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label for="cboAD" class="control-label">Director:</label>

                                <asp:DropDownList ID="cboAD" runat="server" TabIndex="8" CssClass="form-control dropdown">
                                </asp:DropDownList>

                                <label for="lblHPhon" class="control-label">AD Phone</label>
                                <asp:Label ID="lblHPhon" runat="server" CssClass="form-control label"></asp:Label>
                                <asp:Label ID="lblCPhon" runat="server" CssClass="form-control label"></asp:Label>
                            </div>
                        </div>
                    </div>


                    <div class="well well-sm">

                        <h4>Tryouts</h4>
                        <div class="row">
                            <div class="col-md-4">

                                <div class="form-group">
                                    <label for="txtVenue" class="control-label">Venue:</label>

                                    <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" TabIndex="15"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtDate" class="control-label">Date:</label>
                                    <asp:TextBox ID="txtDate" runat="server" TabIndex="15" TextMode="Date"
                                        CssClass="form-control">
                                    </asp:TextBox>


                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtTime" class="control-label">Time:</label>

                                    <asp:TextBox ID="txtTime" runat="server" TextMode="Time" CssClass="form-control" TabIndex="16"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="btn-group">
                        <asp:Button ID="btnNew" runat="server" TabIndex="12" Text="New Division" Width="90px" CssClass="btn btn-primary" OnCommand="btnNew_Command" />
                        <asp:Button ID="btnSave" runat="server" TabIndex="13" Text="Save" Width="90px" CssClass="btn  btn-primary" OnCommand="btnSave_Command" />
                        <asp:Button ID="btnDelete" runat="server" TabIndex="14" Text="Delete" Width="90px" CssClass="btn btn-primary" OnCommand="btnDelete_Command" />
                    </div>
                    <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                        Visible="False" Width="87px"></asp:Label>
                </div>
            </div>

        </div>
        <div class="col-md-5">
            <section class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Divisions
                    </div>
                </div>

                <div style="overflow-y: scroll; height: 240px">
                    <asp:GridView runat="server" ID="grdDivisions"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.Division"
                        OnRowCommand="grdDivisions_RowCommand">
                        <Columns>
                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>
                                    Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDivision" runat="server"
                                        Text='<%# Item.Div_Desc %>'
                                        CommandName="Select"
                                        CommandArgument='<%# Item.DivisionID  %>' />

                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="Gender" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="MinDate" HeaderText="Min Date" ShowHeader="true" DataFormatString="{0:d}"></asp:BoundField>
                            <asp:BoundField DataField="MaxDate" HeaderText="Max Date" DataFormatString="{0:d}"></asp:BoundField>

                        </Columns>
                    </asp:GridView>

                </div>

            </section>

            <section class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Teams</div>
                </div>
                <div style="overflow-y: scroll; height: 240px">
                    <asp:GridView runat="server" ID="grdTeams"
                        AutoGenerateColumns="false"
                        CssClass="table  table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Admin.Web.ViewModels.vw_Team"
                        DataKeyNames="TeamID"
                        OnRowCommand="grdTeams_RowCommand">
                        <EmptyDataTemplate>No Data found</EmptyDataTemplate>
                        <Columns>
                                                        <asp:TemplateField HeaderText="Name">
                                <HeaderTemplate>
                                    Team
                                </HeaderTemplate>
                                <HeaderStyle Width="100"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkTeamName"
                                        runat="server"
                                        Text='<%# Item.TeamName %>'
                                        CommandArgument='<%# Item.TeamID %>'
                                        CommandName="SelectTeam">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Color
                                </HeaderTemplate>
                                <HeaderStyle Width="60"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblTeamColor" runat="server"
                                        Text='<%# Item.TeamColor %>'
                                        CommandArgument='<%# Item.TeamColorID %>'
                                        CommandName="SelectColor"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="60"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Coach</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCoachName"
                                        runat="server"
                                        Text='<%# Item.CoachName %>'
                                        CommandName="SelectCoach"
                                        CommandArgument='<%# Item.CoachID %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Phone
                                </HeaderTemplate>
                                <HeaderStyle Width="60"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCoachPhone" runat="server" Text='<%# Item.CoachPhone %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </section>
        </div>




        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        <asp:HiddenField ID="hdnMinDateOld" runat="server" />
        <asp:HiddenField ID="hdnMaxDateOld" runat="server" />
        <asp:HiddenField ID="hdnGenderOld" runat="server" />
        <asp:HiddenField ID="hdnGender2Old" runat="server" />
        <asp:HiddenField ID="hdnMinDate2Old" runat="server" />
        <asp:HiddenField ID="hdnMaxDate2Old" runat="server" />
    </div>
</asp:Content>

