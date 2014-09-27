<%@ Import Namespace="CSBC_DLL.CSBC.Components" %>
<%@ Import Namespace="System.Data" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Calendar.aspx.vb" Inherits="Calendar" title="Events Calendar" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Calendar</title>
<link href="style.css" rel="stylesheet" type="text/css" />
<script language="VB" runat="server">
        Dim days(12, 31) As String  
        dim SQL as string
   
    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCalendar As New Website.clsCalendar
        Dim rsData As DataTable
        Dim I As Integer
        Try
            rsData = oCalendar.GetFullCalendar(Session("CompanyID"))

            For I = 0 To rsData.Rows.Count - 1
                days(rsData.Rows(I).Item("iMonth"), rsData.Rows(I).Item("iDay")) = rsData.Rows(I).Item("sTitle")
            Next
        Catch ex As Exception
            lblError.Text = "Page_Load:" & ex.Message
        Finally
            oCalendar = Nothing
            rsData = Nothing
        End Try
    End Sub
        
        Private Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs)

            Dim d as CalendarDay
            Dim c as TableCell

            d = e.Day
            c = e.Cell

            If d.IsOtherMonth Then
                c.Controls.Clear
            Else
                Try
                    Dim Hol As String = days(d.Date.Month,d.Date.Day)

                    If Hol <> "" Then
                        c.Controls.Add(new LiteralControl("<br>" + Hol))
                    End If
                Catch exc as Exception
                    Response.Write (exc.ToString())
                End Try
            End If
        End Sub
		
    Private Sub Date_Selected(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCalendar As New Website.clsCalendar
        lblTitle.Text = ""
        lblDetail.Text = ""
        Try
            oCalendar.GetDayEvent(Session("CompanyID"), Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Day, Calendar1.SelectedDate.Year)

            lblTitle.Text = oCalendar.sTitle
            If lblTitle.Text > "" Then lblTitle.Text = Calendar1.SelectedDate.ToShortDateString + "<br>" + lblTitle.Text
            lblDetail.Text = oCalendar.sDesc
        Catch ex As Exception
            lblError.Text = "Calendar1_DayRender:" & ex.Message
        End Try
        
    End Sub


		</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
    <td valign="top"><div id="header">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="top" align="left"><table width="100%" border="0" cellspacing="0" cellpadding="0" id="headerTop">
            <tr>
              <td valign="top" align="left"><div id="logo"><a href="index.html"><img src="images/spacer.gif" alt="" width="404" height="84" border="0" /></a></div></td>
              <td align="right" valign="middle">
		</td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td valign="top">
              <div id="topMenu">
			<a href="default.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="http://secure.csbchoops.com/Default.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a> </div>
            </td>
        </tr>
      </table>
    </div></td>
  </tr>

  <tr><td class="box">
      <h1 class="boxHeadingcenter">Events Calendar</h1>
  </td>
  </tr>
</table>


<table id="mainCol" width="950px" border="0" align="center" cellpadding="0" cellspacing="0">
  


  <tr>
      <td align="center">
           <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="True"></asp:Label>
           <br />
           <asp:Label ID="lblDetail" runat="server"  Font-Italic="True"></asp:Label>
           <br />
           <asp:Calendar ID="Calendar1" runat="server" BorderColor="Black" BorderWidth="1" DayStyle-Height="30px"
                    DayStyle-VerticalAlign="Top" DayStyle-Width="14%" Font-Name="Verdana" Font-Size="9px"
                    Height="310px" OnDayRender="Calendar1_DayRender" OnLoad="Page_Load" OnSelectionChanged="Date_Selected"
                    ShowGridLines="true"
                    TitleStyle-BackColor="White" TitleStyle-Font-Bold="true" TitleStyle-Font-Size="12px"
                    Width="504px"></asp:Calendar>

      </td>
  </tr>



  <tr><td><asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                    Width="725px"></asp:Label></td></tr>
</table>

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
        <param name="_cx" value="12383"/>
        <param name="_cy" value="1588"/>
        <param name="FlashVars" value=""/>
        <param name="Movie" value="Movies/Sponsors.swf"/>
        <param name="Src" value="Movies/Sponsors.swf"/>
        <param name="WMode" value="Window"/>
        <param name="Play" value="-1"/>
        <param name="Loop" value="-1"/>
        <param name="Quality" value="High"/>
        <param name="SAlign" value=""/>
        <param name="Menu" value="-1"/>
        <param name="Base" value=""/>
        <param name="AllowScriptAccess" value=""/>
        <param name="Scale" value="ShowAll"/>
        <param name="DeviceFont" value="0"/>
        <param name="EmbedMovie" value="0"/>
        <param name="BGColor" value=""/>
        <param name="SWRemote" value=""/>
        <param name="MovieData" value=""/>
        <param name="SeamlessTabbing" value="1"/>
        <param name="Profile" value="0"/>
        <param name="ProfileAddress" value=""/>
        <param name="ProfilePort" value="0"/>
        <param name="AllowNetworking" value="all"/>
        <param name="AllowFullScreen" value="false"/>
		<embed src="/Movies/Sponsors.swf" quality="high" width="950" height="80" name="Sponsors" align=""
			type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
		</embed>
    </object>
                
                
                </div></div></div></td>
  </tr>
  </table>
  
       <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top"><div id="footer">
      <div class="footerLink">
			<a href="default.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="http://secure.csbchoops.com/Default.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a>
	  </div>
	  
	  <!-- Begin http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove --> 
      <div>Template by <a href="http://www.sportstemplates.org" target="_blank"><font color="#cccc00">Sports Website Templates</font></a></div> 
<!-- End http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->

    </div></td>
  </tr>
</table>



    </div>
    </form>
</body>
</html>
