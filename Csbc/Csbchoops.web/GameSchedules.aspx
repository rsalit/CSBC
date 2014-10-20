<%@ Page Language="C#" AutoEventWireup="true" Inherits="Csbchoops.Web.GameSchedules" Title="Schedule / Standings" CodeBehind="GameSchedules.aspx.cs"
    MaintainScrollPositionOnPostback ="true" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>

<!DOCTYPE HTML >

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Coral Springs Basketball Club</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="content/style.css" rel="stylesheet" type="text/css" />
    <link href="//netdna.bootstrapcdn.com/twitter-bootstrap/3.0.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="//netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet">
    <style type="text/css">
        .style1 {
            width: 201px;
            height: 25px;
        }

        .style2 {
            width: 231px;
            height: 25px;
        }

        .style3 {
            width: 422px;
        }

        table {
            border: 0;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function OnItemSelected(grid) {
            var item = grid.getSelectedItem();
            var info = "Selected Item Index: " + item.getIndex();
            var div = document.getElementById("divInfo");
            div.innerHTML = info;
        }
        function OnItemSelected(grid) {
            //Get the selected item
            var item = grid.getSelectedItem();

            //This value is not used by the Grid
            grid.raiseItemCommandEvent(item.getIndex(), "select");
        }

    </script>
    <script src="../js/jquery-1.10.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="Scripts/toastr.min.js"></script>

</head>
<body>
    <table width="950" align="center">
        <tr>
            <td valign="top">
                <div id="header">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="headerTop">
                                    <tr>
                                        <td valign="top" align="left">
                                            <div id="logo">
                                                <a href="index.html">
                                                    <img src="images/spacer.gif" alt="" width="404" height="84" border="0" /></a>
                                            </div>
                                        </td>
                                        <td align="right" valign="middle"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <uc1:MainMenu runat="server" ID="MainMenu" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="firebox" class="container" width="950" align="center">
        <form id="form1" runat="server">
            <div>
                <asp:Label ID="lblTitle" runat="server" class="boxHeadingcenter">Game Schedules</asp:Label>
            </div>

            <%--  <div class="col-xs-12 col-md-6 center-block">
                <div class="modal fade"
                    id="loginDialog"
                    role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button"
                                    class="close"
                                    data-dismiss="modal">
                                    &times;
                                </button>
                                <h4 class="modal-title"
                                    id="loginLabel">Login</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="txtHomeTeam">Home Team</label>
                                    <asp:TextBox ID="txtHomeTeam" runat="server"
                                        CssClass="form-control"
                                        autofocus="autofocus"
                                        required="required"
                                        title="HomeTeam"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtVisitorTeam">
                                        Visitor Team</label>
                                    <asp:TextBox ID="txtLoginPassword" runat="server"
                                        TextMode="Password"
                                        CssClass="form-control"
                                        required="required"
                                        placeholder="Password"
                                        title="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancel" runat="server"
                                    Text="Cancel"
                                    CssClass="btn btn-default"
                                    title="Cancel"
                                    data-dismiss="modal" />
                                <asp:Button ID="btnSignIn" runat="server"
                                    Text="Enter Scores"
                                    CssClass="btn btn-primary"
                                    title="Enter Scores"
                                    OnClick="btnSignIn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>

            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:Panel ID="panelSelections" runat="server" CssClass="panel">
                        <div class="row left-content">
                            <div class=" form-group-sm  col-sm-3">
                                <label id="lblDiv" for="ddlDivisions" class="control-label text-left">Divisions:</label>
                                <asp:DropDownList ID="ddlDivisions" runat="server"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged"
                                    CssClass="form-control input-sm   dropdown ">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group-sm   col-sm-3">
                                <label id="lblTeams" for="cobTeams" class="control-label text-left">Team:</label>
                                <asp:DropDownList ID="cobTeams" runat="server"
                                    CssClass="form-control dropdown input-sm "
                                    OnSelectedIndexChanged="cobTeams_SelectedIndexChanged1"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group-sm checkbox-inline  col-sm-2">
                                <asp:CheckBox ID="cbAllTeams"
                                    AutoPostBack="true" runat="server"
                                    OnCheckedChanged="cbAllTeams_CheckedChanged"
                                    CssClass="checkbox-inline"
                                    Checked="true"
                                    Text="All Teams" />
                            </div>
                            <div class="btn-group  col-sm-2">

                                <asp:LinkButton ID="linkPanelSelection"
                                    AutoPostBack="true"
                                    runat="server"
                                    Text="Show Standings"
                                    OnClick="linkPanelSelection_Click"
                                    CssClass="alert-link">
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-2 pull-right">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-sm" Text="Login" OnClick="btnLogin_Click1"></asp:Button>
                                <asp:Label runat="server" ID="lblName" Visible="false" CssClass="text-primary"></asp:Label>
                                <asp:Button runat="server" ID="btnLogout" Visible="false" CssClass="badge" Text="Logout" OnClick="btnLogout_Click" />
                                <%--data-toggle="modal"
                                data-target="#loginDialog">--%>
                            </div>
                        </div>

                        <asp:Panel runat="server" ID="loginForm" Visible="false" class="row">
                            <div class="col-sm-8 col-xs-12 panel panel-default form-horizontal pull-right">

                                <div class="panel-body">
                                    <div class="text-left">Please sign in</div>
                                    <div class="form-group">
                                        <div>
                                            <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" CssClass="form-control col-sm-4"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control col-sm-4">
                                        </asp:TextBox>
                                    </div>

                                    <asp:Button class="btn btn-primary btn-sm " type="button" ID="btnSubmit" runat="server"
                                        Text="Submit" OnClick="btnSubmit_Click1" />
                                    <asp:Button class="btn btn-primary btn-sm " type="button" ID="btnCancel1" runat="server"
                                        Text="Cancel" OnClick="btnCancel1_Click" />
                                </div>
                                <asp:Label ID="labelLoginError" runat="server" CssClass="text-warning" Visible="false" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Panel ID="panelGames" runat="server" CssClass="row">
                        <div class="col-sm-12 col-xs-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="panel-title">
                                        <h3>Games</h3>
                                    </div>

                                </div>
                                <div >

                                    <asp:GridView runat="server" ID="grdSchedule"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-bordered  table-responsive table-condensed"
                                        RowStyle-CssClass="td"
                                        HeaderStyle-CssClass="th"
                                        OnRowEditing="grdSchedule_RowEditing"
                                        OnRowUpdating="grdSchedule_RowUpdating"
                                        OnRowCancelingEdit="grdSchedule_RowCancelingEdit"
                                        ItemType="Csbchoops.Web.ViewModels.GameSchedulesViewModel">
                                        <EmptyDataTemplate>No results found</EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" Visible="false" ShowHeader="true">
                                                <HeaderTemplate>No.</HeaderTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGameNumber" runat="server" Text='<%# Item.GameNumber %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20px" Visible="false" ShowHeader="true">
                                                <HeaderTemplate>Last Name</HeaderTemplate>
                                                <HeaderStyle Width="40px" CssClass="th" />
                                                <ItemTemplate>
                                                    Schedule Number
                                                </ItemTemplate>
                                                <ItemStyle Width="20px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="18px" HeaderStyle-CssClass="text-center" ShowHeader="true">
                                                <HeaderTemplate>Date</HeaderTemplate>
                                                <HeaderStyle Width="18px" CssClass="th" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGameDate" runat="server" Text='<%# Item.GameDate.ToShortDateString() %>' ToolTip='<%#Item.GameDate%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="18px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="18px" HeaderStyle-CssClass="th" ShowHeader="true">
                                                <HeaderTemplate>Time</HeaderTemplate>
                                                <HeaderStyle Width="18px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGameTime" runat="server"
                                                        Text='<%# Item.GameTime.ToShortTimeString() %>' ToolTip='<%# Item.GameTime %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="18px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                                <HeaderTemplate>Location</HeaderTemplate>
                                                <HeaderStyle Width="34px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGameLocation" runat="server"
                                                        Text='<%# Item.LocationName %>' ToolTip='<%# Item.LocationName %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="34px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="60px" ShowHeader="true">
                                                <HeaderTemplate>Home</HeaderTemplate>
                                                <HeaderStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHomeTeam" runat="server"
                                                        Text='<%# Item.HomeTeamName %>' ToolTip='<%# Item.HomeTeamName %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="60px" ShowHeader="true">
                                                <HeaderTemplate>Visitor</HeaderTemplate>
                                                <HeaderStyle Width="60px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitingTeam" runat="server"
                                                        Text='<%# Item.VisitingTeamName %>' ToolTip='<%# Item.VisitingTeamName %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="24px" ShowHeader="true">
                                                <HeaderTemplate>Home Score</HeaderTemplate>
                                                <HeaderStyle Width="24px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHomeScoreTeam" runat="server"
                                                        Text='<%# Item.HomeTeamScore == -1 ? 0 : Item.HomeTeamScore %>' ToolTip='<%# Item.VisitingTeamName %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="24px"></ItemStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHomeScore" runat="server" Text='<%# Item.HomeTeamScore == -1 ? 0 : Item.HomeTeamScore %>' ToolTip='<%# Item.HomeTeamName %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="24px" ShowHeader="true">
                                                <HeaderTemplate>Visitor Score</HeaderTemplate>
                                                <HeaderStyle Width="24px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitingScore" runat="server"
                                                        Text='<%# Item.VisitingTeamScore == -1 ? 0 : Item.VisitingTeamScore %>' ToolTip='<%# Item.VisitingTeamName %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="24px"></ItemStyle>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtVisitingScore" runat="server" Text='<%# Item.VisitingTeamScore == -1 ? 0 : Item.VisitingTeamScore %>' ToolTip='<%# Item.VisitingTeamName %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="10px" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit1" runat="server" CssClass="btn btn-sm icon-edit" CommandName="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnupdate" runat="server"
                                                        CommandName="Update" CssClass="btn icon-save"></asp:LinkButton>
                                                    <asp:LinkButton ID="btncancel" runat="server"
                                                        CommandName="Cancel" CssClass="btn icon-close"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:Panel ID="panelStandings" runat="server" CssClass="row" Visible="false">
                        <div class="col-sm-12">
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <div class="panel-title">
                                        <h3>Standings</h3>
                                    </div>
                                </div>
                                <asp:GridView ID="grdStandings" runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered  table-responsive table-condensed"
                                    RowStyle-CssClass="td"
                                    HeaderStyle-CssClass="th"
                                    OnRowEditing="grdSchedule_RowEditing"
                                    OnRowUpdating="grdSchedule_RowUpdating"
                                    ItemType="Csbchoops.Web.ViewModels.ScheduleStandingsViewModel">
                                    <EmptyDataTemplate>No results found</EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>No.</HeaderTemplate>
                                            <HeaderStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeamNo" runat="server"
                                                    Text='<%# Item.TeamNo %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>Team </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeamName" runat="server"
                                                    Text='<%# Item.TeamName%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px" CssClass="text-left text-primary"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>Won </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server"
                                                    Text='<%# Item.Won %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>Lost </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server"
                                                    Text='<%# Item.Lost %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>Pct </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server"
                                                    Text='<%# Item.Pct.ToString("#0.##") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>PF </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server"
                                                    Text='<%# Item.PF %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px" Visible="true" ShowHeader="true">
                                            <HeaderTemplate>PA </HeaderTemplate>
                                            <HeaderStyle Width="18px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server"
                                                    Text='<%# Item.PA %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="18px"></ItemStyle>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </form>
    </div>


    <script>
        function ShowLogin() {
            $("#loginForm").show();
        }
    </script>
</body>
</html>

