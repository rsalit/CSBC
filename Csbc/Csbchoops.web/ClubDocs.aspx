<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClubDocs.aspx.cs" Inherits="Csbchoops.Web.ClubDocs" %>

<%@ Register Src="~/MainMenu.ascx" TagPrefix="uc1" TagName="MainMenu" %>

<!DOCTYPE HTML>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Coral Springs Basketball Club</title>

    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
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
                                                    <a href="index.aspx">
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
                <td class="box">
                    <h1 class="boxHeadingcenter">Club's Documents and Rules</h1>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">

                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="row">
                                <div class="list-group col-sm-6 col-xs-12">
                                    <h1 class="boxHeading">Documents and Links</h1>
                                    <a href="../docs/RegistrationForm.pdf" id="lnkReg" target="_blank" class="col-sm-12 list-group-item">Registration Form</a>
                                    <a href="docs/SponsorForm.pdf" id="lnkSponsor" target="_blank" class="col-sm-12 list-group-item">Sponsor Form</a>
                                    <a href="docs/TieBreaker.pdf" id="Lnk2" target="_blank" class="col-sm-12 list-group-item">Tie Break Instructions</a>
                                    <a href="docs/ADScores.pdf" id="Lnk3" target="_blank" class="col-sm-12 list-group-item">AD Scores / Website Instructions</a>
                                    <a href="docs/Scorebook.pdf" id="Lnk4" target="_blank" class="col-sm-12 list-group-item">Keeping the Scorebook</a>
                                    <a href="docs/CourtsLocations.pdf" id="Lnk5" target="_blank" class="col-sm-12 list-group-item">Directions to the Courts</a>
                                    <a href="docs/Scholarship.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Jon Miller, Scholarship</a>
                                    <a href="docs/Maxey.pdf" id="Lnk76" target="_blank" class="col-sm-12 list-group-item">Sean Maxey, Scholarship</a>
                                </div>

                                <div class="list-group col-sm-3  col-xs-12">
                                    <h1 class="boxHeading">Girl's Divisions Rules</h1>

                                    <a href="docs/tg.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Trainee Girls</a>
                                    <a href="docs/ig.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Int Girls</a>
                                    <a href="docs/jvg.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">JV Girls</a>
                                    <a href="docs/vg.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Varsity Girls</a>
                                    <a href="docs/hsg.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">HS Girls</a>
                                    <a href="docs/women.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Women</a>

                                </div>

                                <div class="list-group  col-sm-3  col-xs-12">
                                    <h1 class="boxHeading">Boy's Divisions Rules</h1>

                                    <a href="docs/t1.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Traineee 1</a>
                                    <a href="docs/t2.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Traineee 2</a>
                                    <a href="docs/t3.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Traineee 3</a>
                                    <a href="docs/t4.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Traineee 4</a>
                                    <a href="docs/ib.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Int Boys</a>
                                    <a href="docs/jvb.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">JV Boys</a>
                                    <a href="docs/vb.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Varsity Boys</a>
                                    <a href="docs/hsb.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">HS Boys</a>
                                    <a href="docs/men18.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Men 18+</a>

                                </div>
                            </div>
                            <div class="row">
                                <div class="list-group  col-sm-6  col-xs-12">
                                    <h1 class="boxHeading">Game Rules</h1>
                                    <a href="docs/GeneralRules.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">General Rules</a>
                                    <a href="docs/ParkRules.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Park Rules</a>
                                    <a href="docs/Clockrules.pdf" id="Lnk6" target="_blank" class="col-sm-12 list-group-item">Clock Rules</a>


                                </div>
                            </div>
                        </div>
                    </div>

                </td>
                </tr>
                                    </table>
                               
                            
                                                            <tr>
                                                                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                                                                    Width="725px"></asp:Label>
                                                            </tr>
               
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="top">
                                    <div class="box">
                                        <a href="Sponsors.aspx" target="_blank" class="boxHeading">SPONSORS &amp; PARTNERS</a>
                                        <div class="boxContent">
                                            <div class="boxPadding">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                
            <tr>
                <td valign="top">
                    <div id="footer">
                    </div>
                </td>
            </tr>
      
    </form>
</body>
</html>



