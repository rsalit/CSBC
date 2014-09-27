<%@ Page Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Teams.aspx.cs" Inherits="CSBC.Admin.Web.Teams" Title="Team Builder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading clearfix">
                    <div class="panel-title col-md-4 pull-left">Teams</div>
                    <div class="form-group col-md-6 pull-right">
                        <!--<label for="cmbDivisions" class="label label-primary">Division:</label>-->
                        <asp:DropDownList ID="cmbDivisions"
                            runat="server"
                            CssClass="form-control dropdown small"
                            TabIndex="1"
                            OnSelectedIndexChanged="DivisionChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel
                        ID="UpdatePanelTeamGrid"
                        UpdateMode="Conditional"
                        runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                             <asp:AsyncPostBackTrigger ControlID="cmbDivisions" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="overflow-y: scroll; max-height: 240px">

                                <asp:GridView ID="grdTeams" runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-condensed"
                                    RowStyle-CssClass="td"
                                    HeaderStyle-CssClass="th"
                                    DataKeyNames="TeamID"
                                    ItemType="CSBC.Admin.Web.ViewModels.vw_Team"
                                    OnRowCommand="grdTeams_RowCommand"
                                    ShowHeader="true">
                                    <EmptyDataTemplate>No Teams Found!</EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="TeamID" Visible="false" />
                                        <asp:TemplateField HeaderText="Team #" ItemStyle-Width="50px" ShowHeader="true">
                                            <HeaderTemplate>Team #</HeaderTemplate>
                                            <HeaderStyle Width="30"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTeamNo" runat="server" Text='<%# Item.TeamNumber %>'
                                                    CommandName="Select"
                                                    CommandArgument='<%# Item.TeamID  %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>Team Name</HeaderTemplate>
                                            <HeaderStyle Width="80"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeamName" runat="server" Text='<%# Item.TeamName %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color">
                                            <HeaderTemplate>Color</HeaderTemplate>
                                            <HeaderStyle Width="90"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeamColor" runat="server" Text='<%# Item.TeamColor %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Coach">
                                            <HeaderTemplate>Coach</HeaderTemplate>
                                            <HeaderStyle Width="110"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCoach" runat="server" Text='<%# Item.CoachName %>' ToolTip='<%# Item.CoachPhone %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle CssClass="th"></HeaderStyle>

                                    <RowStyle CssClass="td"></RowStyle>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Team Information</div>
                </div>
                <div class="panel-body">

                    <div class="form-group col-md-4">
                        <label for="txtName" class="control-label">Team Name</label>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="25" CssClass="form-control"
                            TabIndex="2"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <label for="txtTeamNumber" class="control-label">Team No</label>
                        <asp:TextBox ID="txtTeamNumber" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-5">
                        <label for="cmbColors" class="control-label">Color</label>
                        <asp:DropDownList ID="cmbColors" runat="server" TabIndex="5" CssClass="form-control dropdown">
                        </asp:DropDownList>

                    </div>
                    <!--<div class="col-md-2">
                            <!--<button type="submit" class="btn col-md-2 btn"></button>
                            <asp:Button ID="imgTeam" runat="server" CssClass="btn"
                                TabIndex="3" ToolTip="Search By Team" Text="Search"></asp:Button>
                            <span class="glyphicon glyphicon-search"></span>

                        </div>              -->


                    <div class="form-group col-md-6">
                        <label for="cmbCoach" class="control-label">Coach Name:</label>
                        <asp:DropDownList ID="cmbCoach" runat="server"
                            OnSelectedIndexChanged="cmbCoach_SelectedIndexChanged"
                            AutoPostBack="true"
                            TabIndex="6"
                            CssClass="form-control dropdown">
                        </asp:DropDownList>

                        <asp:UpdatePanel
                            ID="CoachInfoUpdate"
                            UpdateMode="Conditional"
                            runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmbCoach" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblCoachPhone" runat="server" CssClass="small" />
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label ID="lblCCPhone" runat="server" CssClass="small"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label ID="lblCHPhone" runat="server" CssClass="small"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="form-group col-md-6 ">
                        <label for="cmbAsstCoach" class="control-label">Asst Coach:</label>
                        <asp:DropDownList ID="cmbAsstCoach" runat="server"
                            CssClass="form-control dropdown"
                            OnSelectedIndexChanged="cmbAsstCoach_SelectedIndexChanged"
                            AutoPostBack="true"
                            TabIndex="8">
                        </asp:DropDownList>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblCAsstPhone" runat="server"></asp:Label>
                            </div>

                            <div class="col-md-12">
                                <asp:Label ID="lblHAsstPhone" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group col-md-6">

                        <label for="cmbSponsors" class="control-label">Sponsor:</label>
                        <asp:DropDownList ID="cmbSponsors" runat="server" TabIndex="9"
                            CssClass="form-control dropdown"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="cmbSponsors_SelectedIndexChanged1">
                        </asp:DropDownList>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="lblColors" class="control-label">Preferred Color:</label>
                        <asp:Label ID="lblColors" runat="server" CssClass="form-control-static"></asp:Label>
                    </div>
                </div>
                <asp:HiddenField ID="lblTeamId" runat="server" />
                <div class="panel-footer">
                    <div class="btn-group">
                        <div class="btn-group">
                            <asp:Button ID="btnNew" runat="server" TabIndex="12" Text="New Team" OnClick="btnNew_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="btnSave" runat="server" TabIndex="13" Text="Save" CssClass="btn  btn-primary" OnClick="btnSave_Click1" />
                            <asp:Button ID="btnDelete" runat="server" TabIndex="14" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblDeleteTeam" runat="server" CssClass="text-danger"></asp:Label>
        </div>

    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <div class="panel-title">Team Players</div>
                        </div>
                        <div style="overflow-y: scroll; max-height: 300px">

                            <asp:GridView ID="grdPlayers" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered table-condensed"
                                RowStyle-CssClass="td"
                                HeaderStyle-CssClass="th"
                                OnRowCommand="grdUndraftedPlayers_RowCommand"
                                DataKeyNames="PlayerID"
                                ItemType="CSBC.Core.Models.SeasonPlayer">
                                <Columns>
                                    <asp:BoundField DataField="DraftID" HeaderText="Draft #" />
                                    <asp:BoundField runat="server" DataField="Name" HeaderText="Player Name" ShowHeader="True" />
                                    <asp:BoundField DataField="Rating" HeaderText="Rating" />
                                    <asp:TemplateField ShowHeader="True">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Button2" runat="server"
                                                HeaderText="Player Name"
                                                CausesValidation="false"
                                                CommandName="RemoveFromTeam"
                                                Text="Remove from Team"
                                                CommandArgument='<%# Item.PlayerID  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="panel-footer">
                            <asp:Label ID="lblRecordCount" runat="server" CssClass="text-info" />
                        </div>
                    </div>
                </div>

                <div class="col-md-6 ">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <div class="panel-title">Undrafted Players</div>
                        </div>
                        <div style="overflow-y: scroll; max-height: 300px">

                            <asp:GridView ID="grdUndraftedPlayers" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-condensed"
                                RowStyle-CssClass="td"
                                HeaderStyle-CssClass="th"
                                OnRowCommand="grdUndraftedPlayers_RowCommand"
                                DataKeyNames="PlayerID"
                                ItemType="CSBC.Core.Models.UndraftedPlayer">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Button1" runat="server"
                                                CausesValidation="false"
                                                CommandName="AddToTeam"
                                                Text="Add to Team"
                                                CommandArgument='<%# Bind("PlayerID")  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DraftID" HeaderText="Draft #" />
                                    <asp:BoundField DataField="Name" HeaderText="Player Name" />
                                    <asp:BoundField DataField="Rating" HeaderText="Rating" />
                                    <asp:BoundField DataField="Sponsor" HeaderText="Player Sponsor" />

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="panel-footer">
                            <asp:Label ID="lblUndraftedRecordCount" runat="server" CssClass="text-info" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
        Width="718px"></asp:Label>

</asp:Content>

