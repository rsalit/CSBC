<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Csbchoops.Web.Index" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Coral Springs Basketball Club</title>

    <link href="Content/style.css" rel="stylesheet" />
    <style type="text/css">
        .centerlow {
            background-color: #191818;
            color: #f6f2f2;
            text-align: center;
            font-size: 1.3em;
            font-weight: 500;
            padding-left: 12px;
            padding-right: 12px;
            padding-top: 10px;
            padding-bottom: 10px;
        }

            .centerlow a {
                font-size: 1em;
                font-weight: 400;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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

                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="mainCol">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="310" valign="top">
                                    <table width="300" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td valign="top">
                                                <div class="box">
                                                    <h1 class="boxHeading">Next Season Information</h1>
                                                    <div class="boxContent">
                                                        <br />
                                                        <h2 style="text-align: center; color: red">Final Winter Season 2014</h2>
                                                        <h2 style="text-align: center; color: red">Sign Ups!!
                   
                                                        </h2>
                                                        <br />
                                                        <h3>Monday October 20th</h3>
                                                        <h3>6PM - 7PM</h3>
                                                        <h3>at the Coral Springs Gymnasium</h3>

                                                        <h3>Girls and Boys Ages 6 and up</h3>
                                                        <h3>HS Girls and Boys</h3>
                                                        <h4>Season runs from approx. November - February</h4>

                                                        <hr />
                                                        <h3 style="text-align: center; color: red">Oct 20th Board Meeting</h3>
                                                        <h3>@7PM CS Gym</h3>
                                                        <%--<p style="text-align: center">On line registration is not currently available. </p>--%>
                                                        <%--<h2 style="text-align: center"><a href="pdf/CSTryoutsFall2014.pdf" target="_blank">Fall Tryout Info</a></h2>

                                                        <h4>NO NEW SIGN UPS FOR FALL SEASON WILL BE ACCEPTED AT TRYOUTS!!!!</h4>
                                                         <h4>Next sign ups for Winter Season 2014/2015 will be posted soon!!!</h4>--%>
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="Chart">
                                                            <tr>
                                                                <td>
                                                                    <asp:HyperLink ID="Link1" runat="server" Target="_blank" Visible="false" Width="100%">[Link1]</asp:HyperLink></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:HyperLink ID="Link2" runat="server" Target="_blank" Visible="false" Width="100%">[Link2]</asp:HyperLink></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:HyperLink ID="Link3" runat="server" Target="_blank" Visible="false" Width="100%">[Link3]</asp:HyperLink></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <%-- <div class="box">
                                                    <a href="calendar.aspx" target="_blank" class="boxHeading">Up coming events!
                                                    </a>
                                                    <div class="boxContent">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="Chart">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Bold="False" Font-Size="X-Small" ForeColor="Black" Height="100%" Width="100%"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>--%>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                                <td valign="top">
                                    <div>
                                        <div>
                                        </div>
                                    </div>
                                    <div class="box">
                                        <h1 class="boxHeading">Latest News</h1>

                                        <div class="boxContent">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="newsTitle">
                                                <tr>

                                                    <td>
                                                        <asp:Image ID="imgStandings" runat="server" ImageUrl="images/sky.jpg" Height="139px"
                                                            Width="625px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p class="centerlow">
                                                            To all members we are sad to 
inform everyone that Manny Rosa passed away on 8/27/14.
                                                            <br />
                                                            Our most deepest sympathy goes out to the family<br />
                                                        </p>

                                                        <%-- <span class="reportLink"><a href="Reports/t2.pdf" target="_blank">Trainee 2 Coed</a></span>
                                                        <span class="reportLink"><a href="Reports/t3.pdf" target="_blank">Trainee 3 Coed</a></span>
                                                        <span class="reportLink"><a href="Reports/t4.pdf" target="_blank">Trainee 4 Coed</a></span>
                                                        <span class="reportLink"><a href="Reports/si.pdf" target="_blank">SI Boys</a></span>
                                                        <span class="reportLink"><a href="Reports/fi.pdf" target="_blank">FI Boys</a></span>
                                                        <span class="reportLink"><a href="Reports/fjv.pdf" target="_blank">Freshman JV Boys</a></span>
                                                        <br />
                                                        <span class="reportLink"><a href="Reports/sjvg.pdf" target="_blank">SJV Girls</a></span>
                                                        <span class="reportLink"><a href="Reports/sjv.pdf" target="_blank">SJV Boys</a></span>
                                                        <span class="reportLink"><a href="Reports/HS.pdf" target="_blank">HS Boys</a></span>
                                                        <span class="reportLink"><a href="Reports/M18.pdf" target="_blank">Mens 18</a></span>
                                                        <span class="reportLink"><a href="Reports/wmn.pdf" target="_blank">Women</a></span>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink ID="Link4" runat="server" Target="_blank" Visible="false" Width="100%">[Link4]</asp:HyperLink>
                                                        <asp:HyperLink ID="Link5" runat="server" Target="_blank" Visible="false" Width="100%">[Link5]</asp:HyperLink><asp:HyperLink ID="Link6" runat="server" Target="_blank" Visible="false" Width="100%">[Link6]</asp:HyperLink><asp:HyperLink ID="Link7" runat="server" Target="_blank" Visible="false" Width="100%">[Link7]</asp:HyperLink><asp:HyperLink ID="Link8" runat="server" Target="_blank" Visible="false" Width="100%">[Link8]</asp:HyperLink><asp:HyperLink ID="Link9" runat="server" Target="_blank" Visible="false" Width="100%">[Link9]</asp:HyperLink>
                                                        <asp:HyperLink ID="Link10" runat="server" Target="_blank" Visible="false" Width="100%">[Link10]</asp:HyperLink></td>
                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                                    Width="725px"></asp:Label>
                            </tr>
                        </table>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="top">
                                    <div class="box">
                                        <a href="Sponsors.aspx" target="_blank" class="boxHeading">SPONSORS &amp; PARTNERS</a>
                                        <div class="boxContent">
                                            <div class="boxPadding">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
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
                                                        <embed src="/Movies/Sponsors.swf" quality="high" width="920" height="80" name="Sponsors" align="left"
                                                            type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
                                                        </embed>
                                                    </object>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
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
			<a href="Photos.aspx">Photo Gallery</a>   |   
			<a href="ContactUs.aspx">Contact Us</a>
                        </div>

                        <!-- Begin http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->
                        <div>Template by <a href="http://www.sportstemplates.org" target="_blank"><font color="#cccc00">Sports Website Templates</font></a></div>
                        <!-- End http://www.sportstemplates.org | http://www.sportstemplates.org Code | Do Not Remove -->

                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

