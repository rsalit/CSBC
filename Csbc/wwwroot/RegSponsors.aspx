<%@ Page Language="vb"  MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" Codefile="RegSponsors.aspx.vb" Inherits="RegSponsors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td vAlign="top" style="width: 752px" >
						<table height="100%" cellSpacing="0" cellPadding="0">
							<tr>
								<td vAlign="top" bgColor="#ffffff" height="100%" style="width: 515px">
									<div style="WIDTH: 768px; POSITION: relative; HEIGHT: 334px">&nbsp;&nbsp;
										<asp:button id="comPrev" 
                                            style="Z-INDEX: 101; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="18"
											runat="server" ToolTip="Refresh" Text="<< Return <<" BorderColor="Silver" BackColor="LightGray"
											BorderStyle="Outset" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="X-Small"></asp:button>
                                        <asp:button id="comNext" 
                                            style="Z-INDEX: 103; LEFT: 648px; POSITION: absolute; TOP: 56px" tabIndex="17"
											runat="server" ToolTip="Save and Continue" Text=">> Continue >>" BorderColor="Silver" BackColor="LightGray" 
                                            BorderStyle="Outset" Font-Bold="True" ForeColor="RoyalBlue" Height="32" 
                                            Width="97" Font-Size="X-Small"></asp:button><asp:label id="lblSize" style="Z-INDEX: 105; LEFT: 200px; POSITION: absolute; TOP: 216px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="116px" Font-Size="XX-Small">Sponsor Shirt:</asp:label>
                                        <asp:dropdownlist id="cmbShirts" style="Z-INDEX: 106; LEFT: 328px; POSITION: absolute; TOP: 216px"
											tabIndex="11" runat="server" Height="20px" Width="88px" Font-Size="XX-Small">
											<asp:ListItem Value="N/A" Selected="True">Select Size</asp:ListItem>
											<asp:ListItem Value="SMALL">Small</asp:ListItem>
											<asp:ListItem Value="MEDIUM">Medium</asp:ListItem>
											<asp:ListItem Value="LARGE">Large</asp:ListItem>
											<asp:ListItem Value="X-LARGE">x-Large</asp:ListItem>
											<asp:ListItem Value="XX-LARGE">xx-Large</asp:ListItem>
											<asp:ListItem Value="3X-LARGE">3x-Large</asp:ListItem>
										</asp:dropdownlist>
                                        <asp:label id="lblPhone" style="Z-INDEX: 107; LEFT: 440px; POSITION: absolute; TOP: 216px; width: 31px;"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Font-Size="XX-Small"> Phone:</asp:label>
                                        <asp:textbox id="txtPhone1" style="Z-INDEX: 108; LEFT: 480px; POSITION: absolute; TOP: 216px"
											tabIndex="12" runat="server" BorderStyle="Inset" Height="20px" Width="33" Font-Size="X-Small" 
                                            MaxLength="3"></asp:textbox><asp:textbox id="txtPhone2" style="Z-INDEX: 109; LEFT: 512px; POSITION: absolute; TOP: 216px"
											tabIndex="13" runat="server" BorderStyle="Inset" Height="20px" Width="33px" Font-Size="X-Small" 
                                            MaxLength="3"></asp:textbox><asp:textbox id="txtPhone3" style="Z-INDEX: 110; LEFT: 544px; POSITION: absolute; TOP: 216px"
											tabIndex="14" runat="server" BorderStyle="Inset" Height="20px" Width="48px" Font-Size="X-Small" 
                                            MaxLength="4"></asp:textbox><asp:label id="Label2" style="Z-INDEX: 102; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="160px" Font-Size="Large" Font-Underline="True"> Registration</asp:label><asp:label id="Label3" style="Z-INDEX: 111; LEFT: 320px; POSITION: absolute; TOP: 40px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="208px" Font-Size="Small" Font-Underline="True">(Sponsors Sign Up)</asp:label><asp:label id="lblMSG" style="Z-INDEX: 112; LEFT: 200px; POSITION: absolute; TOP: 288px" runat="server"
											Font-Bold="True" ForeColor="Red" Height="16px" Width="481px" Font-Size="XX-Small"></asp:label>
                                        <asp:button id="comCancel" style="Z-INDEX: 140; LEFT: 648px; POSITION: absolute; TOP: 173px"
											tabIndex="21" runat="server" Text="Delete Sponsor" BorderColor="Silver" BackColor="LightGray" 
                                            BorderStyle="Outset" Font-Bold="True" ForeColor="RoyalBlue" Height="32" 
                                            Width="97" Font-Size="X-Small"
											Enabled="False"></asp:button><asp:textbox id="txtSponsor" style="Z-INDEX: 113; LEFT: 328px; POSITION: absolute; TOP: 72px"
											tabIndex="3" runat="server" Height="21" Width="264px" Font-Size="XX-Small"></asp:textbox><asp:textbox id="txtContact" style="Z-INDEX: 114; LEFT: 328px; POSITION: absolute; TOP: 96px"
											tabIndex="4" runat="server" Height="21" Width="264px" Font-Size="XX-Small"></asp:textbox><asp:textbox id="txtAddress" style="Z-INDEX: 115; LEFT: 328px; POSITION: absolute; TOP: 120px"
											tabIndex="5" runat="server" BorderStyle="Inset" Height="21px" Width="264px" Font-Size="XX-Small"></asp:textbox><asp:label id="Label4" style="Z-INDEX: 116; LEFT: 200px; POSITION: absolute; TOP: 71px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="114px" Font-Size="XX-Small">Company Name:</asp:label><asp:label id="Label5" style="Z-INDEX: 117; LEFT: 200px; POSITION: absolute; TOP: 97px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="114px" Font-Size="XX-Small">Contact Name:</asp:label><asp:label id="Label6" style="Z-INDEX: 118; LEFT: 200px; POSITION: absolute; TOP: 119px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="114px" Font-Size="XX-Small">Company Address:</asp:label><asp:label id="Label7" style="Z-INDEX: 119; LEFT: 200px; POSITION: absolute; TOP: 144px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="114px" Font-Size="XX-Small">City, State, Zip:</asp:label><asp:textbox id="txtCity" style="Z-INDEX: 121; LEFT: 328px; POSITION: absolute; TOP: 144px" tabIndex="6"
											runat="server" BorderStyle="Inset" Height="21px" Width="176px" Font-Size="XX-Small">Coral Springs</asp:textbox><asp:textbox id="txtZip" style="Z-INDEX: 122; LEFT: 528px; POSITION: absolute; TOP: 144px" tabIndex="8"
											runat="server" BorderStyle="Inset" Height="21px" Width="64px" Font-Size="XX-Small"></asp:textbox><asp:textbox id="txtWebsite" style="Z-INDEX: 123; LEFT: 328px; POSITION: absolute; TOP: 168px"
											tabIndex="9" runat="server" BorderStyle="Inset" Height="21px" Width="264px" Font-Size="XX-Small"></asp:textbox><asp:label id="Label8" style="Z-INDEX: 124; LEFT: 200px; POSITION: absolute; TOP: 168px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="115px" Font-Size="XX-Small">WebSite:</asp:label><asp:label id="Label11" style="Z-INDEX: 125; LEFT: 200px; POSITION: absolute; TOP: 192px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="115px" Font-Size="XX-Small">Email:</asp:label><asp:textbox id="txtEmail" style="Z-INDEX: 126; LEFT: 328px; POSITION: absolute; TOP: 192px"
											tabIndex="10" runat="server" BorderStyle="Inset" Height="21px" Width="264px" Font-Size="XX-Small"></asp:textbox>
                                        <asp:textbox id="txtState" style="Z-INDEX: 127; LEFT: 504px; POSITION: absolute; TOP: 144px"
											tabIndex="7" runat="server" BorderStyle="Inset" Height="21px" Width="21px" Font-Size="XX-Small">FL</asp:textbox><asp:label id="Label17" style="Z-INDEX: 130; LEFT: 200px; POSITION: absolute; TOP: 243px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="116px" Font-Size="XX-Small">1st Color choice:</asp:label><asp:label id="Label18" style="Z-INDEX: 131; LEFT: 448px; POSITION: absolute; TOP: 243px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="25px" Font-Size="XX-Small">2nd:</asp:label>
										<table id="Table1" style="Z-INDEX: 120; LEFT: 8px; WIDTH: 152px; POSITION: absolute; TOP: 192px; HEIGHT: 134px"
											cellspacing="1" cellpadding="1" width="152" border="0">
											<tr>
												<td><asp:label id="Label16" runat="server" Font-Bold="True" Height="16px" Width="144px" Font-Size="X-Small">Registration Steps:</asp:label></td>
											</tr>
											<tr>
												<td style="HEIGHT: 20px"><asp:label id="Label15" runat="server" BorderColor="Silver" BorderStyle="None" Font-Bold="True"
														Height="16px" Width="144px" Font-Size="XX-Small" BorderWidth="2px">1) Household Information</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label14" runat="server" Height="16px" Width="144px" Font-Size="XX-Small">2) Parents Information</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label13" runat="server" Height="16px" Width="144px" Font-Size="XX-Small">3) Players Registration</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label12" runat="server" Height="16px" Width="144px" Font-Size="XX-Small">4) Coaches Sign Up</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label10" runat="server" ForeColor="RoyalBlue" Height="16px" Width="144px" Font-Size="XX-Small">5) Sponsors Sign Up</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label9" runat="server" Height="16px" Width="144px" Font-Size="XX-Small">6) Payment Completion</asp:label></td>
											</tr>
										</table>
										<asp:dropdownlist id="cmbShirtsOLD" style="Z-INDEX: 100; LEFT: 476px; POSITION: absolute; TOP: 266px"
											tabindex="11" runat="server" Height="26px" Width="88px" Font-Size="XX-Small" Visible="False">
											<asp:ListItem Value="N/A" Selected="True">Select Size</asp:ListItem>
											<asp:ListItem Value="Small">Small</asp:ListItem>
											<asp:ListItem Value="Medium">Medium</asp:ListItem>
											<asp:ListItem Value="Large">Large</asp:ListItem>
											<asp:ListItem Value="X-Large">x-Large</asp:ListItem>
											<asp:ListItem Value="XX-Large">xx-Large</asp:ListItem>
											<asp:ListItem Value="3X-Large">3x-Large</asp:ListItem>
										</asp:dropdownlist><asp:checkboxlist id="chkPlayers" style="Z-INDEX: 133; LEFT: 16px; POSITION: absolute; TOP: 88px"
											runat="server" ForeColor="RoyalBlue" Height="88px" Width="176px" Font-Size="XX-Small" RepeatLayout="Flow"></asp:checkboxlist><asp:label id="Label1" style="Z-INDEX: 135; LEFT: 16px; POSITION: absolute; TOP: 72px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="176px" Font-Size="XX-Small" Font-Underline="True">Sponsored Player(s)</asp:label><asp:checkbox id="chkCheck" style="Z-INDEX: 136; LEFT: 202px; POSITION: absolute; TOP: 264px"
											runat="server" Text="Mailing Company Check?" ForeColor="RoyalBlue" Height="8px" Width="146px" Font-Size="XX-Small" TextAlign="Left"></asp:checkbox><asp:checkbox id="chkCheckOLD" style="Z-INDEX: 137; LEFT: 24px; POSITION: absolute; TOP: 16px"
											runat="server" Text="Mailing Company Check?" ForeColor="RoyalBlue" Height="8px" Width="146px" Font-Size="XX-Small" Visible="False" TextAlign="Left"></asp:checkbox>
                                        <asp:label id="lblPlayers" style="Z-INDEX: 138; LEFT: 24px; POSITION: absolute; TOP: 48px"
											runat="server" Height="16px" Width="232px">Label</asp:label><asp:panel id="Panel2" style="Z-INDEX: 139; LEFT: 168px; POSITION: absolute; TOP: 304px" runat="server"
											Font-Bold="True" Height="24px" Width="584px"><span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Script MT Bold'"><span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'"><span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">&nbsp;You 
														can add Sponsorship fee to this transaction&nbsp;or mail&nbsp;your company 
														check. We will contact you.</span></span></span></asp:panel>
										<asp:DropDownList id="cmbColors1" style="Z-INDEX: 141; LEFT: 328px; POSITION: absolute; TOP: 240px"
											runat="server" Font-Size="XX-Small" Width="112px" Height="20px" TabIndex="15"></asp:DropDownList>
										<asp:DropDownList id="cmbColors2" style="Z-INDEX: 142; LEFT: 472px; POSITION: absolute; TOP: 240px"
											runat="server" Font-Size="XX-Small" Width="123px" Height="20px" TabIndex="16"></asp:DropDownList>
									    <asp:label id="lblDeleteSponsor" style="Z-INDEX: 134; LEFT: 648px; POSITION: absolute; TOP: 209px"
											runat="server" BorderColor="Black" BackColor="White" BorderStyle="None" ForeColor="Red" Height="64px" 
                                            Width="97px" Font-Size="X-Small" Visible="False"></asp:label>
                                        <asp:button id="comUpdate" style="Z-INDEX: 104; LEFT: 648px; POSITION: absolute; TOP: 135px"
											tabIndex="19" runat="server" Text="Save Sponsor" BorderColor="Silver" BackColor="LightGray" 
                                            BorderStyle="Outset" Font-Bold="True" ForeColor="RoyalBlue" Height="32" 
                                            Width="97" Font-Size="X-Small"></asp:button>
									</div>
	                            </td>
							</tr>
							<tr>
								<td></td>
							</tr>
						</table>
					</td>
					<td></td>
				</tr>
			</table>
</asp:Content>