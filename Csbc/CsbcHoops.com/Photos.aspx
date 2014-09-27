<%@ Page Language="VB" AutoEventWireup="false" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Coral Springs Basketball Club</title>
<link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
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
              <uc1:MainMenu runat="server" ID="MainMenu" />
             <%-- <div id="topMenu">
			<a href="index.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="https://secure.csbchoops.com/Default.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a> </div>--%>
            </td>
        </tr>
      </table>
    </div></td>
  </tr>
  <tr>
    <td valign="top"><div id="blackbox">
    <iframe align="top" src="http://www.flickr.com/slideShow/index.gne?user_id=52334882@N07&tags=fall2011" width="650" height="500" frameborder="0" scrolling="no"></iframe><br /><center><small>Created with <a href="http://www.flickrslideshow.com">flickr slideshow</a>.</small></center>
    </div></td>
  </tr>
  <tr>
    <td valign="top"><div id="footer">
      <div class="footerLink">
			<a href="index.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="https://secure.csbchoops.com/Default.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">Admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx" class="active">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a>
	  </div>
	  
	  <!-- Begin http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove --> 
      <div>Template by <a href="http://www.sportstemplates.org" target="_blank"><font color="#cccc00">Sports Website Templates</font></a></div> 
<!-- End http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->

    </div></td>
  </tr>
</table>
</body>
</html>

