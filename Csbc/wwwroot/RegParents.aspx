<%@ Page Language="vb" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="RegParents.aspx.vb" Inherits="RegParents" Title="Parents"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td></td>
					<td valign="top">
						<table cellspacing="0" cellpadding="0" width="770" border="0">
							<tr>
								<td valign="top">
									<div style="WIDTH: 768px; POSITION: relative; HEIGHT: 338px">&nbsp;&nbsp;
										<asp:label id="lblMSG" style="Z-INDEX: 100; LEFT: 200px; POSITION: absolute; TOP: 264px" runat="server"
											Font-Size="Medium" Width="544px" Height="16px" ForeColor="Red" Font-Bold="True"></asp:label>
										<asp:DataGrid id="grdMembers" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 64px" runat="server"
											Font-Size="Small" Width="232px" Height="64px" BorderStyle="Solid" BorderWidth="1px" ShowHeader="False"
											GridLines="None" CellPadding="3" AllowSorting="True" AutoGenerateColumns="False" BorderColor="RoyalBlue"
											BackColor="Transparent">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
											<ItemStyle ForeColor="RoyalBlue"></ItemStyle>
											<HeaderStyle Font-Size="Large" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BackColor="RoyalBlue"></HeaderStyle>
											<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="PeopleID" HeaderText="ID"></asp:BoundColumn>
												<asp:ButtonColumn Text="Select" CommandName="Select">
													<HeaderStyle Width="10px"></HeaderStyle>
												</asp:ButtonColumn>
												<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Name">
													<HeaderStyle Width="60px"></HeaderStyle>
													<ItemStyle Font-Size="XX-Small"></ItemStyle>
												</asp:BoundColumn>
												<asp:ButtonColumn Visible="False" Text="Remove" CommandName="Delete"></asp:ButtonColumn>
												<asp:EditCommandColumn Visible="False" ButtonType="PushButton" EditText="^"></asp:EditCommandColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
										<asp:label id="lblTitle" style="Z-INDEX: 101; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Size="Large" Width="160px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True" Font-Underline="True"> Registration</asp:label>
										<asp:label id="lblParents" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 40px" runat="server"
											Font-Size="Medium" Width="232px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">Select a Parent:</asp:label>
										<asp:label id="lblLastName" style="Z-INDEX: 104; LEFT: 256px; POSITION: absolute; TOP: 64px"
											runat="server" Font-Size="Small" Width="91px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">Last Name:</asp:label>
										<asp:label id="lblFirstName" style="Z-INDEX: 105; LEFT: 256px; POSITION: absolute; TOP: 88px"
											runat="server" Font-Size="Small" Width="91px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">First Name:</asp:label>
										<asp:label id="lblWork" style="Z-INDEX: 106; LEFT: 256px; POSITION: absolute; TOP: 112px" runat="server"
											Font-Size="Small" Width="91px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">Work Phone:</asp:label>
										<asp:label id="lblCel" style="Z-INDEX: 107; LEFT: 256px; POSITION: absolute; TOP: 136px" runat="server"
											Font-Size="Small" Width="91px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">Cell Phone:</asp:label>
										<asp:textbox id="txtLastName" style="Z-INDEX: 109; LEFT: 360px; POSITION: absolute; TOP: 64px"
											tabIndex="1" runat="server" Font-Size="Small" Width="273" Height="16px" BackColor="White"
											MaxLength="50" Enabled="False"></asp:textbox>
										<asp:textbox id="txtFirstName" style="Z-INDEX: 110; LEFT: 360px; POSITION: absolute; TOP: 88px"
											tabIndex="2" runat="server" Font-Size="Small" Width="273px" Height="16px" BackColor="White"
											MaxLength="50" Enabled="False"></asp:textbox>
										<asp:textbox id="txtWork1" style="Z-INDEX: 111; LEFT: 360px; POSITION: absolute; TOP: 112px"
											tabIndex="3" runat="server" Font-Size="Small" Width="33" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtWork2" style="Z-INDEX: 112; LEFT: 392px; POSITION: absolute; TOP: 112px"
											tabIndex="4" runat="server" Font-Size="Small" Width="33px" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtWork3" style="Z-INDEX: 113; LEFT: 424px; POSITION: absolute; TOP: 112px"
											tabIndex="5" runat="server" Font-Size="Small" Width="48px" Height="16px" BorderStyle="Inset"
											MaxLength="4" Enabled="False"></asp:textbox>
										<asp:textbox id="txtCell1" style="Z-INDEX: 114; LEFT: 360px; POSITION: absolute; TOP: 136px"
											tabIndex="6" runat="server" Font-Size="Small" Width="33" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtCell2" style="Z-INDEX: 115; LEFT: 392px; POSITION: absolute; TOP: 136px"
											tabIndex="7" runat="server" Font-Size="Small" Width="33px" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtCell3" style="Z-INDEX: 116; LEFT: 424px; POSITION: absolute; TOP: 136px"
											tabIndex="8" runat="server" Font-Size="Small" Width="48px" Height="16px" BorderStyle="Inset"
											MaxLength="4" Enabled="False"></asp:textbox>
										<asp:radiobuttonlist id="radGender" style="Z-INDEX: 117; LEFT: 536px; POSITION: absolute; TOP: 112px"
											tabIndex="9" runat="server" Font-Size="Small" Width="96px" Height="24px" BorderStyle="Solid" BorderWidth="0px"
											ForeColor="RoyalBlue" Font-Bold="True" BorderColor="RoyalBlue" Enabled="False">
											<asp:ListItem Value="Male">Male</asp:ListItem>
											<asp:ListItem Value="Female">Female</asp:ListItem>
										</asp:radiobuttonlist>
										<asp:label id="lblVolunteer" style="Z-INDEX: 132; LEFT: 416px; POSITION: absolute; TOP: 168px"
											runat="server" Font-Size="Medium" Width="152px" Height="16px" BorderStyle="None" ForeColor="RoyalBlue"
											BorderWidth="1px" ToolTip="Your help is appreciated." Font-Bold="True">*********Volunteer**********</asp:label>
										<asp:checkboxlist id="chkVolunteer" style="Z-INDEX: 131; LEFT: 248px; POSITION: absolute; TOP: 184px"
											tabIndex="10" runat="server" Font-Size="Small" Width="496px" Height="64px" BorderStyle="Solid"
											BorderWidth="1px" ForeColor="RoyalBlue" BorderColor="RoyalBlue" Enabled="False" RepeatDirection="Horizontal"
											RepeatColumns="4">
											<asp:ListItem Value="Board Officer">Board Officer</asp:ListItem>
											<asp:ListItem Value="Board Member">Board Member</asp:ListItem>
											<asp:ListItem Value="Athletic Director">Athletic Director</asp:ListItem>
											<asp:ListItem Value="Sponsor">Sponsor</asp:ListItem>
											<asp:ListItem Value="Sign Ups">Sign Ups</asp:ListItem>
											<asp:ListItem Value="Try Outs">Try Outs</asp:ListItem>
											<asp:ListItem Value="Tee Shirts">Tee Shirts</asp:ListItem>
											<asp:ListItem Value="Printing">Printing Co.</asp:ListItem>
											<asp:ListItem Value="Equipment">Equipment</asp:ListItem>
											<asp:ListItem Value="Electrician">Electrician</asp:ListItem>
											<asp:ListItem Value="Coach">Coach</asp:ListItem>
										</asp:checkboxlist>
										<asp:button id="comPrev" style="Z-INDEX: 119; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="12"
											runat="server" Font-Size="X-Small" Width="97" Height="32" BorderStyle="Outset" ForeColor="RoyalBlue"
											Font-Bold="True" BorderColor="Silver" BackColor="LightGray" Text="<< Return <<" ToolTip="Refresh"></asp:button>
										<asp:button id="comNext" style="Z-INDEX: 120; LEFT: 648px; POSITION: absolute; TOP: 56px" tabIndex="11"
											runat="server" Font-Size="X-Small" Width="97" Height="32" BorderStyle="Outset" ForeColor="RoyalBlue"
											Font-Bold="True" BorderColor="Silver" BackColor="LightGray" Text=">> Continue >>" ToolTip="Save and Continue"></asp:button>
										<asp:button id="comUpdate" style="Z-INDEX: 121; LEFT: 624px; POSITION: absolute; TOP: 136px"
											tabIndex="13" runat="server" Font-Size="XX-Small" Width="97" Height="32" BorderStyle="Outset"
											ForeColor="RoyalBlue" Font-Bold="True" BorderColor="Silver" BackColor="LightGray" Text="Save"
											Enabled="False" Visible="False"></asp:button>
										<asp:button id="comCancel" style="Z-INDEX: 122; LEFT: 648px; POSITION: absolute; TOP: 136px"
											tabIndex="14" runat="server" Font-Size="XX-Small" Width="97" Height="32" BorderStyle="Outset"
											ForeColor="RoyalBlue" Font-Bold="True" BorderColor="Silver" BackColor="LightGray" Text="Refresh"
											Enabled="False" Visible="False"></asp:button>
										<table id="Table1" style="Z-INDEX: 123; LEFT: 8px; WIDTH: 152px; POSITION: absolute; TOP: 192px; HEIGHT: 134px"
											cellspacing="1" cellpadding="1" width="159" border="0">
											<tr>
												<td>
													<asp:Label id="Label9" runat="server" Font-Bold="True" Height="16px" Width="152px" Font-Size="Medium">Registration Steps:</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label10" runat="server" Height="16px" Width="153px" Font-Size="Small" BackColor="Transparent">1) Household Information</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label12" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="16px"
														Width="153px" Font-Size="Small" BorderStyle="None" BorderColor="Silver" BorderWidth="2px">2) Parents Information</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label13" runat="server" Height="16px" Width="153px" Font-Size="Small">3) Players Registration</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label14" runat="server" Height="16px" Width="153px" Font-Size="Small">4) Coaches Sign up</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label15" runat="server" Height="16px" Width="153px" Font-Size="Small">5) Sponsors Sign Up</asp:Label></td>
											</tr>
											<tr>
												<td>
													<asp:Label id="Label16" runat="server" Height="16px" Width="153px" Font-Size="Small">6) Payment Completion</asp:Label></td>
											</tr>
										</table>
										<asp:radiobuttonlist id="radGenderOLD" style="Z-INDEX: 127; LEFT: 656px; POSITION: absolute; TOP: 224px"
											tabIndex="9" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="96px" Font-Size="X-Small"
											BorderColor="RoyalBlue" BorderWidth="0px" BorderStyle="Solid" Enabled="False" Visible="False">
											<asp:ListItem Value="Male">Male</asp:ListItem>
											<asp:ListItem Value="Female">Female</asp:ListItem>
										</asp:radiobuttonlist>
										<asp:label id="lblVolPositions" style="Z-INDEX: 128; LEFT: 664px; POSITION: absolute; TOP: 280px"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="24" Width="91px" Font-Size="XX-Small"
											Visible="False"></asp:label>
										<asp:CheckBox id="chkGuardian" style="Z-INDEX: 129; LEFT: 533px; POSITION: absolute; TOP: 40px; width: 92px;"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="12px" Font-Size="Medium"
											BorderColor="RoyalBlue" BorderWidth="0px" BorderStyle="Solid" Font-Underline="True" Text="Guardian"
											ToolTip="THIS IS THE MAIN MAIL RECIPIENT INCLUDING REFUND CHECK."></asp:CheckBox>
										<asp:CheckBox id="chkGuardianOLD" style="Z-INDEX: 130; LEFT: 656px; POSITION: absolute; TOP: 24px"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="12px" Width="65px" Font-Size="XX-Small"
											BorderColor="RoyalBlue" BorderWidth="0px" BorderStyle="Solid" Font-Underline="True" Text="Guardian"
											Visible="False"></asp:CheckBox>
										<asp:panel id="Panel2" style="Z-INDEX: 133; LEFT: 200px; POSITION: absolute; TOP: 280px" runat="server"
											Font-Bold="True" Height="49px" Width="544px">
											<span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Script MT Bold'"><span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">
													<span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">&nbsp;**Update 
                                            parents’ information, it is very important to&nbsp;enter phones numbers where you can 
                                            be reached.
														</span></span></span></asp:panel>
									</div>
								</td>
							</tr>
						</table>
					</td>
					<td></td>
				</tr>
			</table>
</asp:Content>
