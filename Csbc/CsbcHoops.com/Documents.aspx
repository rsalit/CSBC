<%@ Page Language="VB" AutoEventWireup="false" Inherits="CSBCHOOPS_COM.Documents" Codebehind="Documents.aspx.vb" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Coral Springs Basketball Club</title>
<link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
			<a href="default.aspx">Home</a>   |   
			<a href="Games.aspx">Games</a>   |   
			<a href="http://secure.csbchoops.com/RegLogin.aspx">Registration</a>   |   
			<a href="Documents.aspx">Documents</a>   |   
			<a href="http://admin.csbchoops.com">admin</a>   |   
			<a href="Sponsors.aspx">Our Sponsors</a>   |   
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a> </div>--%>
            </td>
        </tr>
      </table>
    </div></td>
  </tr>
    <tr><td class="box">
      <h1 class="boxHeadingcenter">Club's Documents and Rules</h1>
  </td>
  </tr>
  <tr>
    <td valign="top"><div id="mainCol">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="460" valign="top"><table width="450" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="top"><div class="box">
                <div class="boxContent">
                  <table width="100%" border="0" cellspacing="5" cellpadding="0" class="Chart">
                  <tr>
                    <td align="left">
                    <h1 class="boxHeading">Documents and Links</h1>
                    </td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk1" runat="server" Target="_blank" Width="100%">[Lnk1]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk2" runat="server" Target="_blank" Width="100%">[Lnk2]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk3" runat="server" Target="_blank" Width="100%">[Lnk3]</asp:HyperLink></td>
                  </tr> 
                  <tr>
                    <td><asp:HyperLink ID="Lnk4" runat="server" Target="_blank" Width="100%">[Lnk4]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk5" runat="server" Target="_blank" Width="100%">[Lnk5]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk6" runat="server" Target="_blank" Width="100%">[Lnk6]</asp:HyperLink></td>
                  </tr>    
                  <tr>
                    <td><asp:HyperLink ID="Lnk7" runat="server" Target="_blank" Width="100%">[Lnk7]</asp:HyperLink></td>
                  </tr>                                                      
                  <tr>
                    <td><asp:HyperLink ID="Lnk26" runat="server" Target="_blank" Width="100%">[Lnk26]</asp:HyperLink></td>
                  </tr>                                                      
                  <tr>
                    <td align="left">
                    <h1 class="boxHeading">Game Rules</h1>
                    </td>
                  </tr>                                                      
                  <tr>
                    <td><asp:HyperLink ID="Lnk8" runat="server" Target="_blank" Width="100%">[Lnk8]</asp:HyperLink></td>
                  </tr>                                                      
                  <tr>
                    <td><asp:HyperLink ID="Lnk9" runat="server" Target="_blank" Width="100%">[Lnk9]</asp:HyperLink></td>
                  </tr>                                                      
                  <tr>
                    <td><asp:HyperLink ID="Lnk10" runat="server" Target="_blank" Width="100%">[Lnk10]</asp:HyperLink></td>
                  </tr>                                                      
                  </table>
                </div>
              </div>

            </td>
            </tr>
            
          </table></td>
          <td valign="top">
                 <div class="box">
                 <div class="boxContent">
                  <table width="100%" border="0" cellspacing="5" cellpadding="5" class="Chart"> 
                  <tr>
                    <td>
                    <h1 class="boxHeading">Girl's Divisions Rules</h1>
                      </td>
                    <td>
                    <h1 class="boxHeading">Boy's Divisions Rules</h1>
                      </td>
                  </tr>
                  <tr>
  
                    <td align="center">
                        <asp:HyperLink ID="Lnk11" runat="server" Target="_blank" Width="100%">[Lnk11]</asp:HyperLink>
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="Lnk17" runat="server" Target="_blank" Width="100%">[Lnk17]</asp:HyperLink>
                    </td>
                  </tr>  
                  <tr>
                    <td><asp:HyperLink ID="Lnk12" runat="server" Target="_blank" Width="100%">[Lnk12]</asp:HyperLink></td>
                    <td><asp:HyperLink ID="Lnk18" runat="server" Target="_blank" Width="100%">[Lnk18]</asp:HyperLink></td>
                  </tr>  
                  <tr>
                    <td><asp:HyperLink ID="Lnk13" runat="server" Target="_blank" Width="100%">[Lnk13]</asp:HyperLink></td>
                    <td><asp:HyperLink ID="Lnk19" runat="server" Target="_blank" Width="100%">[Lnk19]</asp:HyperLink></td>
                  </tr> 
                   <tr>
                    <td><asp:HyperLink ID="Lnk14" runat="server" Target="_blank" Width="100%">[Lnk14]</asp:HyperLink></td>
                    <td><asp:HyperLink ID="Lnk20" runat="server" Target="_blank" Width="100%">[Lnk20]</asp:HyperLink></td>
                  </tr>  
                  <tr>
                    <td><asp:HyperLink ID="Lnk15" runat="server" Target="_blank" Width="100%">[Lnk15]</asp:HyperLink></td>
                    <td><asp:HyperLink ID="Lnk21" runat="server" Target="_blank" Width="100%">[Lnk21]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td><asp:HyperLink ID="Lnk16" runat="server" Target="_blank" Width="100%">[Lnk16]</asp:HyperLink></td>
                    <td><asp:HyperLink ID="Lnk22" runat="server" Target="_blank" Width="100%">[Lnk22]</asp:HyperLink></td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                    <td><asp:HyperLink ID="Lnk23" runat="server" Target="_blank" Width="100%">[Lnk23]</asp:HyperLink></td>
                  </tr>    
                  <tr>
                    <td>&nbsp;</td>
                    <td><asp:HyperLink ID="Lnk24" runat="server" Target="_blank" Width="100%">[Lnk24]</asp:HyperLink></td>
                  </tr>                                                                               
                  <tr>
                    <td>&nbsp;</td>
                    <td><asp:HyperLink ID="Lnk25" runat="server" Target="_blank" Width="100%">[Lnk25]</asp:HyperLink></td>
                  </tr>                                                                               
                  </table>
                </div>
                </div>
         </td>
             
      </table>
      <table>
      <tr>
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                    Width="725px"></asp:Label></tr>
      </table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td align="center" valign="top"><div class="box">
            <a href="Sponsors.aspx" target="_blank" class="boxHeading">SPONSORS &amp; PARTNERS</a>
            <div class="boxContent">
			<div class="boxPadding">
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
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
		<embed src="/Movies/Sponsors.swf" quality="high" width="920" height="80" name="Sponsors" align="left"
			type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
		</embed>
    </object>
              </table>
			</div>
            </div>
          </div></td>
        </tr>
      </table>
    </div></td>
  </tr>
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
    </form>
</body>
</html>

