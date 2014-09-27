<%@ Page Language="VB" AutoEventWireup="true" Inherits="CSBCHOOPS_COM.GamesNotReady" Title="Schedule / Standing" CodeBehind="GamesNotReady.aspx.vb" %>

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
                                            <div id="logo"><a href="index.html">
                                                <img src="images/spacer.gif" alt="" width="404" height="84" border="0" /></a></div>
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
                                <h3 style="text-align:center">The Games / Schedule page is being updated for the new season</h3>    

<h3 style="text-align:center">Check back soon!</h3>                                    </td>
                            </tr>
                            </tr>
                        </table>

                    </form>


                </div>
            </td>
            <table width="950" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" valign="top">
                
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

