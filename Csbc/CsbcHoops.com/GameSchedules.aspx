<%@ Page Language="C#" AutoEventWireup="true" Inherits="Csbchoops.Web.GameSchedules" Title="Schedule / Standing" CodeBehind="GameSchedules.aspx.cs" %>

<%--<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>--%>
<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" 
"http://www.w3.org/TR/html4/loose.dtd">


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

        //Raises server side ItemCommand event.
        //The first parameter is the item index.
        //The second parameter is an additional
        //value that you pass to the server side.
        //This value is not used by the Grid
        grid.raiseItemCommandEvent(item.getIndex(), "select");
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Coral Springs Basketball Club</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
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
    </style>
</head>
<body>
    <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <div id="header">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                <%--<div id="topMenu">
			<a href="default.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="http://secure.csbchoops.com">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a> </div>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="firebox">
                    <form id="form1" runat="server">
                        <table style="width: 950px; height: 155px">
                            <tr>
                                <td class="box" colspan="3">
                                    <asp:Label ID="lblTitle" runat="server" class="boxHeadingcenter"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="style1">
                                    <asp:LinkButton ID="lnkSwitch" runat="server" Height="24px" TabIndex="35"
                                        Width="100px" Font-Bold="True" Font-Size="Large" Font-Underline="True">Standings</asp:LinkButton></td>
                                <td class="style3">&nbsp;</td>
                                <td align="right" valign="top" class="style2">
                                    <asp:LinkButton ID="lnkStats" runat="server" Height="16px" TabIndex="35"
                                        Width="88px" Visible="False">Players Stats</asp:LinkButton>
                                    <asp:LinkButton ID="lnkPrint" runat="server" Height="16px" TabIndex="35"
                                        Width="87px" Font-Bold="True" Font-Size="Large" Font-Underline="True">Print</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 201px" valign="top">&nbsp;<table style="height: 77px">
                                    <tr>
                                        <td style="width: 3px">
                                            <asp:Label ID="lblDiv" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Width="143px">Divisions:</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3px">
                                            <asp:DropDownList ID="cobDivisions" runat="server" Font-Size="XX-Small"
                                                Width="144px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3px">
                                            <asp:Label ID="lblTeams" runat="server" Font-Bold="True" ForeColor="RoyalBlue"
                                                Width="143px">Teams:</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 3px; height: 20px;">
                                            <asp:DropDownList ID="cobTeams" runat="server" Font-Size="XX-Small"
                                                Width="144px" AutoPostBack="True">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                    <table style="width: 149px">
                                        <tr>
                                            <td style="width: 125px" align="left">
                                                <asp:Label ID="lblLocation" runat="server" Font-Bold="True" ForeColor="Blue" Height="16px"
                                                    Width="155px" Visible="False" Font-Size="X-Small">Location:</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 125px" align="left">
                                                <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Blue"
                                                    Height="16px" Visible="False" Width="155px">Date / Time:</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 125px" align="right">
                                                <asp:Label ID="lblHome" runat="server" Font-Bold="True" ForeColor="Blue" Height="16px"
                                                    Width="70px" Visible="False" Font-Size="X-Small">Home:</asp:Label>&nbsp;
							<asp:TextBox ID="txtHScores" runat="server" Visible="False" Width="34px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 125px" align="right">
                                                <asp:Label ID="lblVisitor" runat="server" Font-Bold="True" ForeColor="Blue" Height="16px"
                                                    Width="70px" Visible="False" Font-Size="X-Small">Visitor:</asp:Label>&nbsp;
							<asp:TextBox ID="txtVScores" runat="server" Visible="False" Width="34px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 125px" align="center">
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="86px" Visible="False" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 125px">
                                                <asp:Label ID="lblUsername" runat="server" Font-Bold="True" ForeColor="Blue" Height="16px"
                                                    Width="150px" Font-Size="Small" Visible="False">UserName:</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 125px">
                                                <asp:TextBox ID="txtUser" runat="server" Font-Size="Small" MaxLength="12" Visible="False"
                                                    Width="155px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 125px">
                                                <asp:Label ID="lblPassword" runat="server" Font-Bold="True" ForeColor="Blue" Height="16px"
                                                    Width="150px" Font-Size="Small" Visible="False">Password:</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 125px">
                                                <asp:TextBox ID="txtPwd" runat="server" Font-Size="Small" MaxLength="12" Visible="False"
                                                    Width="155px" TextMode="Password"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 125px">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="86px" ToolTip="Login" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkForgot" runat="server" Height="16px" Visible="False" Width="110px">Forgot Password</asp:LinkButton></td>
                                <td align="left" valign="top" colspan="2">
                                    <asp:GridView runat="server" ID="grdSchedule"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-bordered"
                                        RowStyle-CssClass="td"
                                        HeaderStyle-CssClass="th"
                                        ClientSideOnItemSelected="OnItemSelected"
                                        OnItemCommand="grdGames_ItemCommand">
                                        <EmptyDataTemplate>No results found</EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                                                <HeaderTemplate>Last Name</HeaderTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkLastName" runat="server" Text='<%# Item.LastName%>'
                                                        CommandName="Select"
                                                        CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <%-- <eo:Grid ID="grdSchedule" 
                                        ColumnHeaderHeight="24"
                                        ItemHeight="19">
                                        <ItemStyles>

                                        <Columns>
                                            <eo:StaticColumn DataField="GameNumber" Name="GameNumber" HeaderText=""
                                                Visible="False">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="RecType" Name="RecType" Visible="False">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="ScheduleNumber" Name="ScheduleNumber"
                                                Visible="False">
                                            </eo:StaticColumn>
                                            <eo:DateTimeColumn DataField="Date" DataFormat="{0:ddd MMM/dd/yyyy}"
                                                DataType="DateTime" HeaderText="Date" ReadOnly="True" Width="120">
                                                <DatePicker TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" TitleRightArrowImageUrl="DefaultSubMenuIcon" DayHeaderFormat="FirstLetter" DayCellHeight="16" DayCellWidth="19" SelectedDates="" DisabledDates="" OtherMonthDayVisible="True" ControlSkinID="None">
                                                    <PickerStyle CssText="font-family:Courier New; padding-left:5px; padding-right: 5px;"></PickerStyle>

                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma"></CalendarStyle>

                                                    <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;"></TitleStyle>

                                                    <TitleArrowStyle CssText="cursor:hand"></TitleArrowStyle>

                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px"></MonthStyle>

                                                    <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid"></DayHeaderStyle>

                                                    <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></DayStyle>

                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid"></DayHoverStyle>

                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid"></TodayStyle>

                                                    <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></SelectedDayStyle>

                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></DisabledDayStyle>

                                                    <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></OtherMonthDayStyle>
                                                </DatePicker>
                                            </eo:DateTimeColumn>
                                            <eo:StaticColumn DataField="Time"
                                                HeaderText="Time" Width="80">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Location" HeaderText="Location">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Home" HeaderText="Home" Width="60">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Visitor" HeaderText="Visitor" Width="60" Text="">
                                                <CellStyle CssText="" />
                                                <CellStyle CssText=""></CellStyle>
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="H-Score" HeaderText="H-Score" Width="80"
                                                Name="H-Score">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="V-Score" HeaderText="V-Score" Width="80">
                                            </eo:StaticColumn>
                                        </Columns>
                                        <ColumnTemplates>
                                            <eo:TextBoxColumn>
                                                <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                            </eo:TextBoxColumn>
                                            <eo:DateTimeColumn>
                                                <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19"
                                                    DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True"
                                                    SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL"
                                                    TitleRightArrowImageUrl="DefaultSubMenuIcon">
                                                    <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                                                    <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;"></PickerStyle>

                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma"></CalendarStyle>

                                                    <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                                                    <TitleArrowStyle CssText="cursor:hand" />
                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />

                                                    <TitleArrowStyle CssText="cursor:hand"></TitleArrowStyle>

                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px"></MonthStyle>

                                                    <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                    <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />

                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid"></DayHoverStyle>

                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid"></TodayStyle>

                                                    <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />

                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></DisabledDayStyle>

                                                    <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                </DatePicker>
                                            </eo:DateTimeColumn>
                                            <eo:MaskedEditColumn>
                                                <MaskedEdit ControlSkinID="None"
                                                    TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                                </MaskedEdit>
                                            </eo:MaskedEditColumn>
                                        </ColumnTemplates>
                                        <ContentPaneStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;" />
                                        <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                        <GoToBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; BORDER-TOP: #7f9db9 1px solid; BORDER-LEFT: #7f9db9 1px solid; WIDTH: 40px; BORDER-BOTTOM: #7f9db9 1px solid" />
                                    </eo:Grid>--%>
                                    <eo:grid id="grdStanding" runat="server" bordercolor="#828790" borderwidth="1px"
                                        columnheaderascimage="00050204" columnheaderdescimage="00050205"
                                        columnheaderdividerimage="00050203" fixedcolumncount="1" font-bold="False"
                                        font-italic="False" font-names="Tahoma" font-overline="False" font-size="8.75pt"
                                        font-strikeout="False" font-underline="False" gridlinecolor="240, 240, 240"
                                        gridlines="Both" height="250px" itemheight="19" width="600px"
                                        visible="False" columnheaderheight="24" borderstyle="None">
                                        <ColumnHeaderStyle CssText="background-image:url('00050301');padding-left:8px;padding-top:2px;font-weight: bold;color:white;" />

                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: white" />
                                                <ItemHoverStyle CssText="background-image: url(00050206); background-repeat: repeat-x" />
                                                <SelectedStyle CssText="background-image: url(00050207); background-repeat: repeat-x" />
                                                <CellStyle CssText="padding-left:8px;padding-top:2px; color:#336699;white-space:nowrap;" />

                                                <ItemHoverStyle CssText="background-image: url(00050206); background-repeat: repeat-x"></ItemHoverStyle>

                                                <SelectedStyle CssText="background-image: url(00050207); background-repeat: repeat-x"></SelectedStyle>

                                                <CellStyle CssText="padding-left:8px;padding-top:2px;white-space:nowrap;"></CellStyle>
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>

                                        <ColumnHeaderStyle CssText="background-image:url('00050201');padding-left:8px;padding-top:4px;"></ColumnHeaderStyle>
                                        <ColumnHeaderHoverStyle CssText="background-image:url('00050202');padding-left:8px;padding-top:4px;" />
                                        <Columns>
                                            <eo:StaticColumn DataField="Team"
                                                HeaderText="Team" Width="150">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Won" Name="Won" HeaderText="Won" Width="40">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Lost" HeaderText="Lost" Name="Lost" Width="40">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="PCT" DataFormat="{0:N3.3}" DataType="Float"
                                                HeaderText="PCT" Name="PCT" Width="60">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="Streak" HeaderText="Streak" Name="Streak" Text=""
                                                Width="60">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="PF" HeaderText="PF" Name="PF" Text="" Width="60"
                                                DataFormat="{0:N1.2}">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="PA" HeaderText="PA" Name="PA" Width="60"
                                                DataFormat="{0:N1.2}">
                                            </eo:StaticColumn>
                                            <eo:StaticColumn DataField="GB" HeaderText="GB" Name="GB" Text=""
                                                DataFormat="{0:N1.2}" Width="80">
                                            </eo:StaticColumn>
                                        </Columns>
                                        <ColumnTemplates>
                                            <eo:TextBoxColumn>
                                                <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                            </eo:TextBoxColumn>
                                            <eo:DateTimeColumn>
                                                <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19"
                                                    DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True"
                                                    SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL"
                                                    TitleRightArrowImageUrl="DefaultSubMenuIcon">
                                                    <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                                                    <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;"></PickerStyle>

                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma"></CalendarStyle>

                                                    <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                                                    <TitleArrowStyle CssText="cursor:hand" />
                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />

                                                    <TitleArrowStyle CssText="cursor:hand"></TitleArrowStyle>

                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px"></MonthStyle>

                                                    <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                    <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />

                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid"></DayHoverStyle>

                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid"></TodayStyle>

                                                    <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />

                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid"></DisabledDayStyle>

                                                    <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                </DatePicker>
                                            </eo:DateTimeColumn>
                                            <eo:MaskedEditColumn>
                                                <MaskedEdit ControlSkinID="None"
                                                    TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                                </MaskedEdit>
                                            </eo:MaskedEditColumn>
                                        </ColumnTemplates>
                                        <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                    </eo:grid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 201px" valign="top"></td>
                                <td align="left" colspan="2" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 201px" valign="top" rowspan="1">
                                    <asp:HyperLink ID="lnkWebmaster" runat="server" Font-Size="Small" Font-Underline="True"
                                        ForeColor="RoyalBlue" Height="16px" NavigateUrl="mailto:webmaster@csbchoops.net"
                                        Target="_blank" Visible="False" Width="174px">Email scores to Club AD</asp:HyperLink></td>
                                <td align="left" rowspan="1" valign="top" class="style3">&nbsp;
				<asp:LinkButton ID="lnkTie" runat="server" Height="16px" Width="110px">(*) Indicates tie</asp:LinkButton></td>
                                <td align="left" rowspan="1" style="width: 231px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3" rowspan="1" valign="top">
                                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                                        Width="725px"></asp:Label></td>
                            </tr>
                        </table>

                    </form>


                </div>
            </td>
            <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" valign="top">
                        <div class="box">
                            <a href="Sponsors.aspx" target="_blank" class="boxHeading">SPONSORS &amp; PARTNERS</a>
                            <div class="boxContent">
                                <div class="boxPadding">



                                    <object id="Sponsors" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0"
                                        height="80" style="z-index: 104; left: 0px; position: static; top: 396px" viewastext=""
                                        width="910">
                                        <param name="_cx" value="12383" />
                                        <param name="_cy" value="1588" />
                                        <param name="FlashVars" value="" />
                                        <param name="Movie" value="Movies/Sponsors.swf" />
                                        <param name="Src" value="Movies/Sponsors.swf" />
                                        <param name="WMode" value="Window" />
                                        <param name="Play" value="-1" />
                                        <param name="Loop" value="-1" />
                                        <param name="Quality" value="High" />
                                        <param name="SAlign" value="" />
                                        <param name="Menu" value="-1" />
                                        <param name="Base" value="" />
                                        <param name="AllowScriptAccess" value="" />
                                        <param name="Scale" value="ShowAll" />
                                        <param name="DeviceFont" value="0" />
                                        <param name="EmbedMovie" value="0" />
                                        <param name="BGColor" value="" />
                                        <param name="SWRemote" value="" />
                                        <param name="MovieData" value="" />
                                        <param name="SeamlessTabbing" value="1" />
                                        <param name="Profile" value="0" />
                                        <param name="ProfileAddress" value="" />
                                        <param name="ProfilePort" value="0" />
                                        <param name="AllowNetworking" value="all" />
                                        <param name="AllowFullScreen" value="false" />
                                        <embed src="/Movies/Sponsors.swf" quality="high" width="950" height="80" name="Sponsors" align=""
                                            type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
		</embed>
                                    </object>


                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

        </tr>
        <tr>
            <td valign="top">
                <div id="footer">
                    <div class="footerLink">
                        <a href="default.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="http://secure.csbchoops.com/Default.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx" class="active">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a>
                    </div>

                    <!-- Begin http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->
                    <div>Template by <a href="http://www.sportstemplates.org" target="_blank"><font color="#cccc00">Sports Website Templates</font></a></div>
                    <!-- End http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->

                </div>
            </td>
        </tr>
    </table>
</body>
</html>

