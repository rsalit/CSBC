<%@ Page Language="vb" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" Codefile="RegCoaches.aspx.vb" Inherits="RegCoaches" Title = "Coaches"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td vAlign="top" style="width: 752px" >
						<table height="100%" cellSpacing="0" cellPadding="0">
							<tr>
								<td vAlign="top" bgColor="#ffffff" height="100%" style="width: 515px">
									<DIV style="WIDTH: 768px; POSITION: relative; HEIGHT: 337px" ms_positioning="GridLayout">&nbsp;&nbsp;
										<asp:button id="comPrev" style="Z-INDEX: 100; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="8"
											runat="server" Font-Size="X-Small" Width="97" Height="32" ForeColor="RoyalBlue" Font-Bold="True"
											BorderStyle="Outset" BackColor="LightGray" BorderColor="Silver" Text="<< Return <<" ToolTip="Refresh"></asp:button><asp:button id="comNext" style="Z-INDEX: 102; LEFT: 648px; POSITION: absolute; TOP: 56px" tabIndex="7"
											runat="server" Font-Size="X-Small" Width="97" Height="32" ForeColor="RoyalBlue" Font-Bold="True" BorderStyle="Outset" BackColor="LightGray" BorderColor="Silver" Text=">> Continue >>" ToolTip="Save and Continue"></asp:button><asp:button id="comUpdate" style="Z-INDEX: 104; LEFT: 648px; POSITION: absolute; TOP: 136px"
											tabIndex="9" runat="server" Font-Size="X-Small" Width="97" Height="32" ForeColor="RoyalBlue" Font-Bold="True" BorderStyle="Outset" BackColor="LightGray" BorderColor="Silver" Text="Register Coach" Enabled="False"></asp:button>
                                        &nbsp;&nbsp;
										<asp:label id="lblSize" style="Z-INDEX: 109; LEFT: 216px; POSITION: absolute; TOP: 224px" runat="server"
											Font-Size="Small" Width="74px" Height="16px" ForeColor="RoyalBlue" Font-Bold="True">Coach Shirt:</asp:label>
										<asp:dropdownlist id="cmbShirts" style="Z-INDEX: 110; LEFT: 296px; POSITION: absolute; TOP: 224px"
											tabIndex="3" runat="server" Font-Size="X-Small" Width="160px" Height="16px" Enabled="False">
											<asp:ListItem Value="N/A" Selected="True">Select Size</asp:ListItem>
											<asp:ListItem Value="SMALL">Small</asp:ListItem>
											<asp:ListItem Value="MEDIUM">Medium</asp:ListItem>
											<asp:ListItem Value="LARGE">Large</asp:ListItem>
											<asp:ListItem Value="X-LARGE">x-Large</asp:ListItem>
											<asp:ListItem Value="XX-LARGE">xx-Large</asp:ListItem>
											<asp:ListItem Value="3X-LARGE">3x-Large</asp:ListItem>
										</asp:dropdownlist>
										<asp:label id="lblPhone" style="Z-INDEX: 126; LEFT: 216px; POSITION: absolute; TOP: 248px"
											runat="server" Font-Size="Small" Width="104px" Height="24px" ForeColor="RoyalBlue" Font-Bold="True">Prefered Phone:</asp:label>
										<asp:textbox id="txtPhone1" style="Z-INDEX: 112; LEFT: 320px; POSITION: absolute; TOP: 248px"
											tabIndex="4" runat="server" Font-Size="Small" Width="32px" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtPhone2" style="Z-INDEX: 113; LEFT: 360px; POSITION: absolute; TOP: 248px"
											tabIndex="5" runat="server" Font-Size="Small" Width="33px" Height="16px" BorderStyle="Inset"
											MaxLength="3" Enabled="False"></asp:textbox>
										<asp:textbox id="txtPhone3" style="Z-INDEX: 114; LEFT: 400px; POSITION: absolute; TOP: 248px"
											tabIndex="6" runat="server" Font-Size="Small" Width="48px" Height="16px" BorderStyle="Inset"
											MaxLength="4" Enabled="False"></asp:textbox>
                                        &nbsp;&nbsp;
										<asp:label id="lblMSG" style="Z-INDEX: 116; LEFT: 192px; POSITION: absolute; TOP: 280px" runat="server"
											Font-Bold="True" ForeColor="Red" Height="16px" Width="496px" Font-Size="Medium"></asp:label>
										<asp:button id="comCancel" style="Z-INDEX: 122; LEFT: 648px; POSITION: absolute; TOP: 176px"
											tabIndex="11" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97"
											Font-Size="X-Small" Text="Unregister Coach" BorderColor="Silver" BackColor="LightGray"
											BorderStyle="Outset" Enabled="False"></asp:button>
										<TABLE id="Table1" style="Z-INDEX: 117; LEFT: 8px; WIDTH: 152px; POSITION: absolute; TOP: 192px; HEIGHT: 134px"
											cellSpacing="1" cellPadding="1" width="152" border="0">
											<TR>
												<TD>
													<asp:Label id="Label16" runat="server" Font-Bold="True" Height="16px" Width="144px" Font-Size="Medium">Registration Steps:</asp:Label></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 20px">
													<asp:Label id="Label15" runat="server" BorderColor="Silver" BorderStyle="None" Font-Bold="False"
														Height="16px" Width="144px" Font-Size="Small" BorderWidth="2px">1) Household Information</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label14" runat="server" Height="16px" Width="144px" Font-Size="Small">2) Parents Information</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label13" runat="server" Height="16px" Width="144px" Font-Size="Small">3) Players Registration</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label12" runat="server" ForeColor="RoyalBlue" Height="16px" Width="144px" Font-Size="Small">4) Coaches Sign Up</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label10" runat="server" Height="16px" Width="144px" Font-Size="Small">5) Sponsors Sign Up</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label9" runat="server" Height="16px" Width="144px" Font-Size="Small">6) Payment Completion</asp:Label></TD>
											</TR>
										</TABLE>
										<asp:dropdownlist id="cmbParentsOLD" style="Z-INDEX: 118; LEFT: 488px; POSITION: absolute; TOP: 200px"
											tabIndex="2" runat="server" Height="22" Width="64px" Font-Size="XX-Small" Visible="False"></asp:dropdownlist>
										<asp:dropdownlist id="cmbShirtsOLD" style="Z-INDEX: 119; LEFT: 488px; POSITION: absolute; TOP: 224px"
											tabIndex="3" runat="server" Height="26px" Width="64px" Font-Size="XX-Small" Visible="False">
											<asp:ListItem Value="N/A" Selected="True">Select Size</asp:ListItem>
											<asp:ListItem Value="SMALL">Small</asp:ListItem>
											<asp:ListItem Value="MEDIUM">Medium</asp:ListItem>
											<asp:ListItem Value="LARGE">Large</asp:ListItem>
											<asp:ListItem Value="X-LARGE">x-Large</asp:ListItem>
											<asp:ListItem Value="XX-LARGE">xx-Large</asp:ListItem>
											<asp:ListItem Value="3X-LARGE">3x-Large</asp:ListItem>
										</asp:dropdownlist>
										<asp:button id="btnCancel" style="Z-INDEX: 120; LEFT: 643px; POSITION: absolute; TOP: 252px"
											tabIndex="10" runat="server" Text="Cancel" BorderColor="Silver" BackColor="LightGray" BorderStyle="Outset"
											Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="XX-Small" Enabled="False"
											Visible="False"></asp:button>
										<asp:Label id="lblPlayer" style="Z-INDEX: 121; LEFT: 608px; POSITION: absolute; TOP: 224px"
											runat="server" Height="16px" Width="96px" Visible="False"></asp:Label>
										<asp:panel id="Panel2" style="Z-INDEX: 123; LEFT: 200px; POSITION: absolute; TOP: 304px" runat="server"
											Font-Bold="True" Height="49px" Width="544px">
											<SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Script MT Bold'"><SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">
													<SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">
														<o:p>
															<P class="MsoNormal">&nbsp; **For each registered player you can choose a parent 
																(one that&nbsp;checked the coaches box in the Parents Information screen) to be 
																the coach. Thank you for volunteering to help!</P>
														</o:p></SPAN></SPAN></SPAN></asp:panel>
										<asp:datagrid id="grdPlayers" style="Z-INDEX: 124; LEFT: 167px; POSITION: absolute; TOP: 64px; width: 457px;"
											runat="server" BorderColor="RoyalBlue" BackColor="Transparent" BorderStyle="Solid" Height="96px" 
                                            Font-Size="Medium" BorderWidth="1px" GridLines="None" CellPadding="3" AllowSorting="True"
											AutoGenerateColumns="False">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
											<ItemStyle ForeColor="RoyalBlue"></ItemStyle>
											<HeaderStyle Font-Size="Medium" Font-Bold="True" ForeColor="RoyalBlue" BorderStyle="Solid"></HeaderStyle>
											<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="PeopleID" HeaderText="ID"></asp:BoundColumn>
												<asp:ButtonColumn Text="Select" CommandName="Select">
													<HeaderStyle Width="10px"></HeaderStyle>
													<ItemStyle Font-Size="Small"></ItemStyle>
												</asp:ButtonColumn>
												<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Player">
													<HeaderStyle Font-Size="Small" Font-Underline="True" Font-Bold="True" ForeColor="RoyalBlue"
														Width="100px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Div_Desc" HeaderText="Division">
													<HeaderStyle Font-Size="Small" Font-Underline="True" Font-Bold="True" ForeColor="RoyalBlue"
														Width="50px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Coach" HeaderText="Coach">
													<HeaderStyle Font-Size="Small" Font-Underline="True" Font-Bold="True" 
                                                        ForeColor="RoyalBlue" Width="100px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="cmbParents" runat="server" Enabled="False" Font-Size="X-Small"
                                            Height="16px" Style="z-index: 113; left: 216px; position: absolute; top: 200px"
                                            TabIndex="10" Width="240px">
                                        </asp:DropDownList>
									</DIV>
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