<%@ Page Language="vb" AutoEventWireup="false" Codefile="RegUserName.aspx.vb" Inherits="RegUserName"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML xmlns:o>
	<HEAD>
		<title>Coral Springs Basketball Club</title>
		<script language="JavaScript" type="text/JavaScript">

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" 
		MARGINHEIGHT="0" MARGINWIDTH="0">
		<form id="Form1" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="50%" background="images/bg_lft.gif"></td>
					<td style="PADDING-LEFT: 28px" background="images\bg_lft.jpg"><SPAN style="FONT-SIZE: 9pt"><SPAN style="FONT-SIZE: 9pt"><SPAN style="FONT-SIZE: 9pt"></SPAN></SPAN></SPAN></td>
					<td vAlign="top" bgColor="#ffffff" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="770" border="0">
							<tr>
								<td colSpan="2"><IMG style="HEIGHT: 109px" height="109" alt="" src="images\topCSBC.jpg" width="770" border="0"></td>
							</tr>
							<tr>
								<td vAlign="top" width="504" bgColor="#ffffff" height="100%">
									<DIV style="WIDTH: 777px; POSITION: relative; HEIGHT: 335px" ms_positioning="GridLayout">&nbsp;&nbsp;
										<asp:Label id="Label6" style="Z-INDEX: 100; LEFT: 232px; POSITION: absolute; TOP: 8px" runat="server"
											Width="408px" Height="36px" Font-Underline="True" ForeColor="RoyalBlue" Font-Bold="True"
											Font-Size="Large">Sign Up (Create UserName)</asp:Label>
										<asp:label id="Label11" style="Z-INDEX: 102; LEFT: 336px; POSITION: absolute; TOP: 72px" runat="server"
											Width="104px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="XX-Small"
											BorderStyle="None" BorderWidth="1px">Your email account:</asp:label>
										<asp:label id="Label5" style="Z-INDEX: 103; LEFT: 336px; POSITION: absolute; TOP: 96px" runat="server"
											Width="104px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="XX-Small"
											BorderStyle="None" BorderWidth="1px">Full Name:</asp:label>
										<asp:label id="Label3" style="Z-INDEX: 104; LEFT: 336px; POSITION: absolute; TOP: 120px" runat="server"
											Width="104px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="XX-Small"
											BorderStyle="None" BorderWidth="1px"> UserName:</asp:label>
										<asp:label id="Label2" style="Z-INDEX: 105; LEFT: 336px; POSITION: absolute; TOP: 144px" runat="server"
											Width="104px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="XX-Small"
											BorderStyle="None" BorderWidth="1px"> Password:</asp:label>
										<asp:label id="Label4" style="Z-INDEX: 106; LEFT: 336px; POSITION: absolute; TOP: 168px" runat="server"
											Width="104px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="XX-Small"
											BorderStyle="None" BorderWidth="1px">Verify Password:</asp:label>
										<asp:textbox id="txtEmail" style="Z-INDEX: 107; LEFT: 448px; POSITION: absolute; TOP: 64px" tabIndex="1"
											runat="server" Width="304px" Height="22px" Font-Size="XX-Small" MaxLength="40"></asp:textbox>
										<asp:textbox id="txtName" style="Z-INDEX: 108; LEFT: 448px; POSITION: absolute; TOP: 88px" tabIndex="2"
											runat="server" Width="304px" Height="22px" Font-Size="XX-Small" MaxLength="40"></asp:textbox>
										<asp:textbox id="txtUserName" style="Z-INDEX: 109; LEFT: 448px; POSITION: absolute; TOP: 112px"
											tabIndex="3" runat="server" Width="193" Height="22px" Font-Size="XX-Small" MaxLength="12"></asp:textbox><INPUT id="txtPassword" style="Z-INDEX: 110; LEFT: 448px; WIDTH: 192px; POSITION: absolute; TOP: 136px; HEIGHT: 22px"
											tabIndex="4" type="password" maxLength="12" size="26" name="txtPassword" runat="server" autocomplete="off"><INPUT id="txtPassword2" style="Z-INDEX: 111; LEFT: 448px; WIDTH: 193px; POSITION: absolute; TOP: 160px; HEIGHT: 22px"
											tabIndex="5" type="password" maxLength="12" size="26" name="txtPassword" runat="server" autocomplete="off">
										<asp:button id="comUpdate" style="Z-INDEX: 112; LEFT: 656px; POSITION: absolute; TOP: 120px"
											tabIndex="6" runat="server" Text="Sign Up" Width="96px" Height="24px" Font-Size="Small"></asp:button>
										<asp:label id="lblMSG" style="Z-INDEX: 114; LEFT: 336px; POSITION: absolute; TOP: 184px" runat="server"
											Width="411px" Height="22px" ForeColor="Red" Font-Bold="True" Font-Size="XX-Small"></asp:label>
										<asp:Panel id="Panel1" style="Z-INDEX: 115; LEFT: 328px; POSITION: absolute; TOP: 208px" runat="server"
											Width="432px" Height="121px" BorderStyle="Ridge" BorderWidth="1px">
											<span style="FONT-SIZE: 12pt">
												<P class="MsoNormal">You need a <B style="mso-bidi-font-weight: normal">username</B>
													to use our <I style="mso-bidi-font-style: normal">Online Registration</I>. 
													Online Registration will allow you to sign up your children to our Basketball 
													seasons. <U>Never share your password with anyone!</U> Your email account must 
                                                    match your email in our Database, if you don&#39;t have a valid email account in our 
                                                    database call Hotline (954) 360-1200.</P>
										</asp:Panel>
										<asp:CheckBox id="chkAgree" style="Z-INDEX: 116; LEFT: 8px; POSITION: absolute; TOP: 304px" runat="server"
											Font-Size="X-Small" Font-Bold="True" ForeColor="RoyalBlue" Height="12px" Width="301px" BorderWidth="1px"
											BorderStyle="None" Text="AGREE" BorderColor="DarkGray" Font-Names="Arial Narrow"></asp:CheckBox><SPAN style="FONT-SIZE: 9pt"></SPAN>
										<asp:Panel id="Panel2" style="Z-INDEX: 117; LEFT: 8px; POSITION: absolute; TOP: 64px" runat="server"
											Font-Size="XX-Small" Height="240px" Width="301px" BorderWidth="1px" BorderStyle="None"><SPAN style="FONT-SIZE: 9pt">
												<P><B style="mso-bidi-font-weight: normal"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial"></SPAN></B></P>
												<SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">
													<P style="MARGIN: 0in 0in 0pt; TEXT-ALIGN: center" align="center"><B style="mso-bidi-font-weight: normal"><U><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">Rules 
																	of Parents Conduct</SPAN></U></B><B style="mso-bidi-font-weight: normal"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">
																<o:p></o:p></SPAN></B></P>
													<P style="MARGIN: 0in 0in 0pt"><B style="mso-bidi-font-weight: normal"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">
																<o:p>&nbsp;</o:p></SPAN></B></P>
													<P style="MARGIN: 0in 0in 0pt"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">1. I 
															understand that youth sports are intended for children, for their growth 
															</SPAN></P>
													<P style="MARGIN: 0in 0in 0pt"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">2. I 
															will encourage my child to show good sportsmanship, cooperation and respect for 
															coaches, officials, teammates and opponents.&nbsp;
															<o:p></o:p></SPAN></P>
													<P style="MARGIN: 0in 0in 0pt"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">3. I 
															will provide my child with proper example by showing the same sportsmanship, 
															cooperation and respect.
															<o:p></o:p></SPAN></P>
													<P style="MARGIN: 0in 0in 0pt"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">4. I 
															understand that winning is more fun than losing, but we must learn how to do 
															both.&nbsp;
															<o:p></o:p></SPAN></P>
													<P style="MARGIN: 0in 0in 0pt"><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">5. I 
															will treat others the way I would like myself and my child to be treated.<B style="mso-bidi-font-weight: normal">
																<o:p></o:p></B></SPAN></P>
													<P><SPAN style="FONT-SIZE: 7pt; FONT-FAMILY: Arial">Failure to follow these codes will 
															result in disciplinary action. Each league will provide their own rules of 
															discipline should these codes&nbsp;not be followed. I, the undersigned, agree 
															to follow these codes and understand that disciplinary action will take place 
															should I fail to follow these codes. I also agree that I will be responsible 
															for actions of any other family members or friends that attend.</SPAN>
												</SPAN></asp:Panel></DIV>
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
