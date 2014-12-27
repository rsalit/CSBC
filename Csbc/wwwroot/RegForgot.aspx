<%@ Page Language="vb" AutoEventWireup="false" Codefile="RegForgot.aspx.vb" Inherits="RegForgot" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Coral Springs Basketball Club</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="default.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" type="text/JavaScript">
<!--
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="MM_preloadImages('images/b3r.jpg','images/b5ra.jpg','images/b5rh.jpg','images/b5rd.jpg','images/b5rg.jpg','images/b5rr.jpg','images/b7r.jpg','images/1r.jpg','images/2r.jpg','images/3r.jpg','images/f2.jpg','images/f3.jpg','images/f4.jpg','images/f5.jpg','images/f6.jpg')"
		MARGINWIDTH="0" MARGINHEIGHT="0">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="50%" background="images/bg_lft.gif"></td>
					<td style="PADDING-LEFT: 28px" background="images\bg_lft.jpg"></td>
					<td vAlign="top" bgColor="#ffffff" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="770" border="0">
							<tr>
								<td colSpan="2"><IMG style="HEIGHT: 109px" height="109" alt="" src="images\topCSBC.jpg" width="770" border="0"></td>
							</tr>
							<tr>
								<td vAlign="top" width="504" bgColor="#ffffff" height="100%">
									<DIV style="WIDTH: 768px; POSITION: relative; HEIGHT: 330px" ms_positioning="GridLayout">&nbsp;&nbsp;
										<asp:label id="lblTitle" style="Z-INDEX: 109; LEFT: 248px; POSITION: absolute; TOP: 16px" runat="server"
											Width="288px" Height="24px" Font-Bold="True" ForeColor="RoyalBlue" Font-Size="Large" Font-Underline="True">UserName Request</asp:label>
										<asp:LinkButton id="lnkClose" style="Z-INDEX: 110; LEFT: 696px; POSITION: absolute; TOP: 16px" runat="server"
											Width="48px" Height="24px" Font-Size="Small">Close</asp:LinkButton>
										<asp:label id="Label1" style="Z-INDEX: 111; LEFT: 200px; POSITION: absolute; TOP: 72px" runat="server"
											Width="130px" Height="20px" BorderStyle="None" BorderWidth="1px" Font-Bold="True" ForeColor="RoyalBlue"
											Font-Size="X-Small">Household Name:</asp:label>
										<asp:label id="Label2" style="Z-INDEX: 112; LEFT: 200px; POSITION: absolute; TOP: 104px" runat="server"
											Width="130px" Height="20px" BorderStyle="None" BorderWidth="1px" Font-Bold="True" ForeColor="RoyalBlue"
											Font-Size="X-Small">Home Phone:</asp:label>
										<asp:textbox id="txtName" style="Z-INDEX: 113; LEFT: 336px; POSITION: absolute; TOP: 72px" tabIndex="1"
											runat="server" Width="168px" Height="26px" Font-Size="Small"></asp:textbox>
										<asp:textbox id="txtPhone1" style="Z-INDEX: 114; LEFT: 336px; POSITION: absolute; TOP: 104px"
											tabIndex="2" runat="server" Width="33" Height="22px" BorderStyle="Inset" Font-Size="X-Small"
											MaxLength="3">954</asp:textbox>
										<asp:textbox id="txtPhone2" style="Z-INDEX: 115; LEFT: 368px; POSITION: absolute; TOP: 104px"
											tabIndex="3" runat="server" Width="33px" Height="22px" BorderStyle="Inset" Font-Size="X-Small"
											MaxLength="3"></asp:textbox>
										<asp:textbox id="txtPhone3" style="Z-INDEX: 116; LEFT: 400px; POSITION: absolute; TOP: 104px"
											tabIndex="4" runat="server" Width="48px" Height="22px" BorderStyle="Inset" Font-Size="X-Small"
											MaxLength="4"></asp:textbox>
										<asp:label id="lblMSG" style="Z-INDEX: 117; LEFT: 168px; POSITION: absolute; TOP: 304px" runat="server"
											Width="576px" Height="16px" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:label>
										<asp:button id="comUpdate" style="Z-INDEX: 118; LEFT: 304px; POSITION: absolute; TOP: 144px"
											tabIndex="5" runat="server" Width="96px" Height="24px" Font-Size="Small" Text="Submit"></asp:button>
									</DIV>
								</td>
							</tr>
							<tr>
								<td colSpan="2"><IMG height="50" src="images/footer_left.jpg" width="266"><IMG height="50" src="images/footer_right.jpg" width="504"></td>
							</tr>
						</table>
					</td>
					<td width="50%" background="images/bg_lft.gif"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
