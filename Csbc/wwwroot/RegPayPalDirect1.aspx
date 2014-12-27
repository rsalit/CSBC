<%@ Register TagPrefix="cc1" Namespace="PayPal.Web.Controls" Assembly="PayPal.Web.Controls, Version=1.0.22.18680, Culture=neutral, PublicKeyToken=97b06e1531ed482a" %>
<%@ Page Language="vb" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" Codefile="RegPayPalDirect.aspx.vb" Inherits="RegPayPalDirect" Title="Payment"%>
<%@ OutputCache Duration="1" VaryByParam="*" %>
<%--<%@ Register TagPrefix="cc2" Namespace="nsoftware.IBizPayPal" Assembly="nsoftware.IBizPayPalWeb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="50%" background="images/bg_lft.gif"></td>
					<td vAlign="top" bgColor="#ffffff" height="100%">
						<table height="100%" cellSpacing="0" cellPadding="0" width="770" border="0">
							<tr>
								<td></td>
							</tr>
							<tr>
								<td vAlign="top" width="504" bgColor="#ffffff" height="100%">
									<DIV style="WIDTH: 768px; POSITION: relative; HEIGHT: 364px" ms_positioning="GridLayout">&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="lblTitle" style="Z-INDEX: 102; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Underline="True" Font-Size="Large" ForeColor="RoyalBlue" Font-Bold="True" Width="232px"
											Height="24px"> Registration</asp:label><asp:label id="lblMSG" style="Z-INDEX: 104; LEFT: 480px; POSITION: absolute; TOP: 308px" runat="server"
											Font-Size="Small" ForeColor="Red" Font-Bold="True" Width="272px" Height="40px"></asp:label>&nbsp;&nbsp;
										<asp:button id="comPrev" style="Z-INDEX: 105; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="16"
											runat="server" Font-Size="X-Small" ForeColor="RoyalBlue" Font-Bold="True" Width="97" Height="32"
											BorderColor="Silver" BorderStyle="Outset" Text="<< Return <<" BackColor="LightGray" ToolTip="Refresh"></asp:button><asp:button id="btnPayNow" style="Z-INDEX: 101; LEFT: 648px; POSITION: absolute; TOP: 56px"
											tabIndex="15" runat="server" Font-Size="X-Small" ForeColor="RoyalBlue" Font-Bold="True" Width="98" Height="32" BorderColor="Silver" BorderStyle="Outset" Text="Pay Now" BackColor="LightGray" Enabled="False"></asp:button>&nbsp;
										<TABLE id="Table2" style="Z-INDEX: 106; LEFT: 16px; WIDTH: 400px; POSITION: absolute; TOP: 64px; HEIGHT: 262px"
											cellSpacing="1" cellPadding="1" width="400" border="1">
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 17px"><asp:label id="lblName" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px">FirstName:</asp:label></TD>
												<TD style="HEIGHT: 17px">&nbsp;<INPUT id="txtName" style="BORDER-RIGHT: gray thin solid; BORDER-TOP: gray thin solid; FONT-SIZE: 10pt; BORDER-LEFT: gray thin solid; WIDTH: 296px; COLOR: royalblue; BORDER-BOTTOM: gray thin solid; HEIGHT: 16px"
														tabIndex="1" type="text" size="44" name="Name" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 17px"><asp:label id="lblLast" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px">Last Name:</asp:label></TD>
												<TD style="HEIGHT: 17px">&nbsp;<INPUT id="txtLast" style="BORDER-RIGHT: gray thin solid; BORDER-TOP: gray thin solid; FONT-SIZE: 10pt; BORDER-LEFT: gray thin solid; WIDTH: 296px; COLOR: royalblue; BORDER-BOTTOM: gray thin solid; HEIGHT: 16px"
														tabIndex="2" type="text" size="36" name="Name" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 17px"><asp:label id="lblAddress" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px">Address:</asp:label></TD>
												<TD style="HEIGHT: 17px">&nbsp;<INPUT id="txtAddress" style="BORDER-RIGHT: gray thin solid; BORDER-TOP: gray thin solid; FONT-SIZE: 10pt; BORDER-LEFT: gray thin solid; WIDTH: 296px; COLOR: royalblue; BORDER-BOTTOM: gray thin solid; HEIGHT: 16px"
														tabIndex="3" type="text" size="36" name="address1" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 17px"><asp:label id="lblCityState" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px">City:</asp:label></TD>
												<TD style="HEIGHT: 17px">&nbsp;<INPUT id="txtCity" style="BORDER-RIGHT: gray thin solid; BORDER-TOP: gray thin solid; FONT-SIZE: 10pt; BORDER-LEFT: gray thin solid; WIDTH: 296px; COLOR: royalblue; BORDER-BOTTOM: gray thin solid; HEIGHT: 16px"
														tabIndex="4" type="text" size="36" value="CORAL SPRINGS" name="city" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 17px"><asp:label id="Label1" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="80px" Height="16px">State, Zip:</asp:label></TD>
												<TD style="HEIGHT: 17px">&nbsp;<INPUT id="txtState" style="FONT-SIZE: 10pt; WIDTH: 16px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														tabIndex="5" type="text" size="1" value="FL" name="state" runat="server">,&nbsp;&nbsp;&nbsp;
													<INPUT id="txtZip" style="FONT-SIZE: 10pt; WIDTH: 112px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														tabIndex="6" type="text" size="4" name="zip" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 15px"><asp:label id="lblPhone" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="32px" Height="16px">Phone:</asp:label></TD>
												<TD style="HEIGHT: 15px">&nbsp;<INPUT id="txtPhone1" style="FONT-SIZE: 9pt; WIDTH: 32px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														tabIndex="7" type="text" maxLength="3" name="PHONE1" runat="server">&nbsp;-
													<INPUT id="txtPhone2" style="FONT-SIZE: 9pt; WIDTH: 32px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														tabIndex="8" type="text" maxLength="3" name="PHONE2" runat="server">&nbsp;-
													<INPUT id="txtPhone3" style="FONT-SIZE: 9pt; WIDTH: 32px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														tabIndex="9" type="text" maxLength="4" size="1" name="PHONE3" runat="server"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px; HEIGHT: 25px"><asp:label id="Label4" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="112px" Height="8px">Card Type:</asp:label></TD>
												<TD style="HEIGHT: 25px">
													<P>&nbsp;<IMG style="WIDTH: 40px; HEIGHT: 24px" height="24" alt="" src="images\logo_ccVisa.gif"
															width="40"><IMG style="WIDTH: 40px; HEIGHT: 24px" height="24" alt="" src="images\logo_ccMC.gif"
															width="40"><IMG style="WIDTH: 40px; HEIGHT: 24px" height="24" alt="" src="images\logo_ccDiscover.gif"
															width="40"><IMG style="WIDTH: 40px; HEIGHT: 24px" height="24" alt="" src="images\logo_ccAmEx.gif"
															width="40">&nbsp;
														<asp:dropdownlist id="cmbCC" tabIndex="10" runat="server" Font-Size="XX-Small" ForeColor="RoyalBlue"
															Width="128px" Height="23px">
															<asp:ListItem Value="Select Card Type" Selected="True">Select Card Type</asp:ListItem>
															<asp:ListItem Value="ccVisa">Visa</asp:ListItem>
															<asp:ListItem Value="ccMasterCard">Master Card</asp:ListItem>
															<asp:ListItem Value="ccDiscover">Discover</asp:ListItem>
															<asp:ListItem Value="ccAmex">American Express</asp:ListItem>
														</asp:dropdownlist></P>
												</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px"><asp:label id="Label5" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="136px" Height="16px">Card Number: (No Spaces)</asp:label></TD>
												<TD>&nbsp;<INPUT id="txtCard" style="FONT-SIZE: 10pt; WIDTH: 296px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-left-color: gray; border-bottom-width: thin; border-bottom-color: gray; border-top-color: gray; border-right-width: thin; border-right-color: gray;"
														tabIndex="11" type="text" maxLength="16" size="13" name="CCard" runat="server" visible="true"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px"><asp:label id="Label6" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="128px" Height="16px">Expiration Date: (mm/yyyy)</asp:label></TD>
												<TD>&nbsp;<asp:dropdownlist id="cmbMonth" tabIndex="12" runat="server" Font-Size="Small" ForeColor="RoyalBlue"
														Width="48px" Height="26px">
														<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
														<asp:ListItem Value="2">2</asp:ListItem>
														<asp:ListItem Value="3">3</asp:ListItem>
														<asp:ListItem Value="4">4</asp:ListItem>
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Value="6">6</asp:ListItem>
														<asp:ListItem Value="7">7</asp:ListItem>
														<asp:ListItem Value="8">8</asp:ListItem>
														<asp:ListItem Value="9">9</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="11">11</asp:ListItem>
														<asp:ListItem Value="12">12</asp:ListItem>
													</asp:dropdownlist>&nbsp;
													<asp:dropdownlist id="cmbYear" tabIndex="13" runat="server" Font-Size="Small" ForeColor="RoyalBlue"
														Width="72px" Height="26px"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px"><asp:label id="lblcvv2" runat="server" ForeColor="RoyalBlue" Width="136px" Height="14px" ToolTip="This is found on the back of the card, enter the last 3 digits." Font-Bold="True" Font-Size="Small">Verification Number:</asp:label></TD>
												<TD>&nbsp;<INPUT id="txtCVV2" style="BORDER-RIGHT: red thin solid; BORDER-TOP: red thin solid; BORDER-LEFT: red thin solid; WIDTH: 40px; COLOR: royalblue; BORDER-BOTTOM: red thin solid; HEIGHT: 30px"
														tabIndex="14" type="text" maxLength="4" size="1" name="cvv2" runat="server">
													&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG style="WIDTH: 48px; HEIGHT: 32px" height="32" alt="" src="images\mini_cvv2.gif"
														width="48">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:label id="Label8" runat="server" Font-Underline="True" ForeColor="RoyalBlue" Width="88px"
														Height="22px" ToolTip="The verification number is a 3-digit number printed on the back of your card. It appears after and to the right of your card number.">What's this?</asp:label>&nbsp;
													<asp:label id="Label10" runat="server" Font-Underline="True" ForeColor="RoyalBlue" Width="112px"
														Height="22px" ToolTip="The American Express verification number is a 4-digit number printed on the front of your card. It appears after and to the right of your card number.">Using AmEx?</asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 155px"><asp:label id="Label7" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px">Amount:</asp:label></TD>
												<TD>&nbsp;<asp:label id="lblAmount2" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Width="224px"
														Height="14px"></asp:label></TD>
											</TR>
										</TABLE>
										<TABLE id="Table1" style="Z-INDEX: 107; LEFT: 480px; WIDTH: 264px; POSITION: absolute; TOP: 144px; HEIGHT: 64px"
											cellSpacing="1" cellPadding="1" width="264" border="1">
											<TR>
												<TD colSpan="2">
													<P class="MsoNormal"><B style="mso-bidi-font-weight: normal"><SPAN style="COLOR: blue">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TRANSACTION DETAIL</SPAN></B></P>
												</TD>
											</TR>
											<TR>
												<TD><asp:label id="Label9" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="89px" Height="16px">Season</asp:label></TD>
												<TD><INPUT id="txtSeason" style="FONT-SIZE: 10pt; WIDTH: 162px; COLOR: royalblue; HEIGHT: 16px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin; border-right-width: thin;"
														readOnly type="text" size="21" name="item_name" runat="server"></TD>
											</TR>
											<TR>
												<TD><asp:label id="Label12" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="89px" Height="16px">Player(s)</asp:label></TD>
												<TD><asp:label id="lblPlayers" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="57px" Height="16px"></asp:label></TD>
											</TR>
											<TR>
												<TD><asp:label id="lblSponsors" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="89px" Height="16px">Sponsor</asp:label></TD>
												<TD><asp:label id="lblSponsor" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="65px" Height="16px"></asp:label></TD>
											</TR>
											<TR>
												<TD><asp:label id="Label14" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="89px" Height="16px">Convenience Fee</asp:label></TD>
												<TD><asp:label id="lblFee" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="96px" Height="16px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="height: 23px"><asp:label id="Label2" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True"
														Width="89px" Height="16px">TOTAL</asp:label></TD>
												<TD style="height: 23px"><asp:label id="lblAmount" runat="server" ForeColor="RoyalBlue" Font-Bold="True" Width="40px"
														Height="16"></asp:label></TD>
											</TR>
										</TABLE>
										<asp:textbox id="txtUsername" style="Z-INDEX: 108; LEFT: 32px; POSITION: absolute; TOP: 40px"
											runat="server" Font-Size="8pt" Width="314px" Visible="False" Font-Names="Verdana">treasurer_api3.csbchoops.net</asp:textbox><asp:textbox id="txtPassword" style="Z-INDEX: 109; LEFT: 32px; POSITION: absolute; TOP: 56px"
											runat="server" Font-Size="8pt" Width="316px" Visible="False" Font-Names="Verdana">rosa7057</asp:textbox>
                                      <%--  <cc2:Directpayment ID="Directpayment1" runat="server">
                                        </cc2:Directpayment>--%>
                                        &nbsp; &nbsp;&nbsp;
                                    </DIV>
								</td>
							</tr>
							<TR>
								<TD style="height: 19px"></TD>
							</TR>
						</table>
					<TD></TD>
			</table>
</asp:Content>
