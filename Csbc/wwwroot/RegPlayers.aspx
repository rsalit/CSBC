<%@ Page Language="vb" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" Codefile="RegPlayers.aspx.vb" Inherits="RegPlayers" Title="Players"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<table height="100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td vAlign="top" style="width: 752px" >
						<table height="100%" cellSpacing="0" cellPadding="0">
							<tr>
								<td vAlign="top" bgColor="#ffffff" height="100%" style="width: 515px">
									<DIV style="WIDTH: 736px; POSITION: relative; HEIGHT: 342px; left: 2px; top: 0px;" ms_positioning="GridLayout">&nbsp;&nbsp;
										<asp:label id="lblTitle" style="Z-INDEX: 100; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Underline="True" Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="160px"
											Font-Size="Large"> Registration</asp:label><asp:label id="lblParents" 
                                            style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 32px; right: 496px;" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="232px" Font-Size="Medium">Select a Player:</asp:label><asp:datagrid id="grdMembers" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 48px" runat="server"
											Height="96px" Width="232px" Font-Size="Small" BorderColor="RoyalBlue" BackColor="Transparent" GridLines="None" CellPadding="3" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
											<ItemStyle ForeColor="RoyalBlue"></ItemStyle>
											<HeaderStyle Font-Size="XX-Small" Font-Bold="True" ForeColor="RoyalBlue" BorderStyle="Solid"></HeaderStyle>
											<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="PeopleID" HeaderText="ID"></asp:BoundColumn>
												<asp:ButtonColumn Text="Select" CommandName="Select">
													<HeaderStyle Width="10px"></HeaderStyle>
													<ItemStyle Font-Size="XX-Small"></ItemStyle>
												</asp:ButtonColumn>
												<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Name">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" Width="60px"></HeaderStyle>
													<ItemStyle Font-Size="X-Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="Div_Desc" HeaderText="Division">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" Width="30px"></HeaderStyle>
													<ItemStyle Font-Size="X-Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Status" HeaderText="Status">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" Width="30px"></HeaderStyle>
													<ItemStyle Font-Size="X-Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid><asp:label id="lblDiv" style="Z-INDEX: 103; LEFT: 328px; POSITION: absolute; TOP: 64px" runat="server"
											ForeColor="Black" Height="24px" Width="304px" Font-Size="Small" Font-Italic="True"></asp:label><asp:label id="lblPlayerName" style="Z-INDEX: 104; LEFT: 328px; POSITION: absolute; TOP: 88px"
											runat="server" Height="16px" Width="304px" Font-Size="Small" BorderWidth="1px" BorderStyle="Solid" Font-Italic="True"></asp:label><asp:label id="lblName" style="Z-INDEX: 105; LEFT: 248px; POSITION: absolute; TOP: 88px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="72px" Font-Size="Small">Name:</asp:label>
                                        <asp:label id="lblBirthDate" style="Z-INDEX: 106; LEFT: 248px; POSITION: absolute; TOP: 112px; right: 416px;"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="72px" Font-Size="Small">BirthDate:</asp:label><asp:label id="lblDate" style="Z-INDEX: 107; LEFT: 328px; POSITION: absolute; TOP: 112px" runat="server"
											Height="16px" Width="120px" Font-Size="Small" BorderWidth="1px" BorderStyle="Solid" Font-Italic="True"></asp:label>
                                      
                                        <asp:RadioButtonList ID="radPlayerGender" style="Z-INDEX: 108; LEFT: 496px; POSITION: absolute; TOP: 112px; width: 127px; height: 24px;"
											tabIndex="7" runat="server" Enabled="False" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:RadioButtonList><asp:label id="lblSchool" style="Z-INDEX: 109; LEFT: 248px; POSITION: absolute; TOP: 136px"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="24px" Width="72px" Font-Size="Small">School:</asp:label><asp:textbox id="txtSchool" style="Z-INDEX: 110; LEFT: 328px; POSITION: absolute; TOP: 136px"
											tabIndex="11" runat="server" Height="16px" Width="305px" Font-Size="X-Small" BackColor="White" Enabled="False" MaxLength="50"></asp:textbox><asp:label id="lblGrade" style="Z-INDEX: 111; LEFT: 248px; POSITION: absolute; TOP: 160px"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="16px" Width="72px" Font-Size="Small">Grade:</asp:label>
                                        <asp:dropdownlist id="cmbGrade" style="Z-INDEX: 113; LEFT: 328px; POSITION: absolute; TOP: 160px; height: 14px; width: 84px;"
											tabIndex="10" runat="server" Font-Size="X-Small" Enabled="False">
											<asp:ListItem Value="0">K</asp:ListItem>
											<asp:ListItem Value="1">1st</asp:ListItem>
											<asp:ListItem Value="2">2nd</asp:ListItem>
											<asp:ListItem Value="3">3rd</asp:ListItem>
											<asp:ListItem Value="4">4th</asp:ListItem>
											<asp:ListItem Value="5">5th</asp:ListItem>
											<asp:ListItem Value="6">6th</asp:ListItem>
											<asp:ListItem Value="7">7th</asp:ListItem>
											<asp:ListItem Value="8">8th</asp:ListItem>
											<asp:ListItem Value="9">9th</asp:ListItem>
											<asp:ListItem Value="10">10th</asp:ListItem>
											<asp:ListItem Value="11">11th</asp:ListItem>
											<asp:ListItem Value="12">12th</asp:ListItem>
											<asp:ListItem Value="13" Selected="True">N/A</asp:ListItem>
										</asp:dropdownlist><asp:label id="Label1" style="Z-INDEX: 114; LEFT: 248px; POSITION: absolute; TOP: 192px" runat="server"
											Font-Bold="True" ForeColor="RoyalBlue" Height="48px" Width="72px" Font-Size="Small">Tryout message to CSBC:</asp:label><asp:textbox id="txtDraftNotes" style="Z-INDEX: 115; LEFT: 328px; POSITION: absolute; TOP: 184px"
											tabIndex="7" runat="server" Height="40px" Width="304px" Font-Size="X-Small" Enabled="False" MaxLength="100" TextMode="MultiLine"></asp:textbox><asp:button id="comPrev" style="Z-INDEX: 116; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="12"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="X-Small" BorderColor="Silver" BackColor="LightGray" BorderStyle="Outset" Text="<< Return <<" ToolTip="Refresh"></asp:button><asp:button id="comNext" style="Z-INDEX: 117; LEFT: 648px; POSITION: absolute; TOP: 56px" tabIndex="11"
											runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="X-Small" BorderColor="Silver" BackColor="LightGray" BorderStyle="Outset" Text=">> Continue >>" ToolTip="Save and Continue"></asp:button><asp:button id="comUpdate" style="Z-INDEX: 118; LEFT: 648px; POSITION: absolute; TOP: 136px"
											tabIndex="13" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="X-Small" BorderColor="Silver" BackColor="LightGray" BorderStyle="Outset" Enabled="False" Text="Register"></asp:button><asp:button id="comCancel" style="Z-INDEX: 119; LEFT: 648px; POSITION: absolute; TOP: 176px"
											tabIndex="14" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Height="32" Width="97" Font-Size="X-Small" BorderColor="Silver" BackColor="LightGray" BorderStyle="Outset" Enabled="False" Text="UNRegister"></asp:button><asp:label id="lblMSG" style="Z-INDEX: 120; LEFT: 249px; POSITION: absolute; TOP: 248px" runat="server"
											Font-Bold="True" ForeColor="Red" Height="16px" Width="496px" Font-Size="Medium"></asp:label>
                                        &nbsp;
										<TABLE id="Table1" style="Z-INDEX: 121; LEFT: 8px; WIDTH: 152px; POSITION: absolute; TOP: 192px; HEIGHT: 134px"
											cellSpacing="1" cellPadding="1" width="152" border="0">
											<TR>
												<TD>
													<asp:Label id="Label16" runat="server" Font-Size="Medium" Width="144px" Height="16px" Font-Bold="True">Registration Steps:</asp:Label></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 20px">
													<asp:Label id="Label15" runat="server" Font-Size="Small" Width="144px" Height="16px" Font-Bold="False"
														BorderStyle="None" BorderWidth="2px" BorderColor="Silver">1) Household Information</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label14" runat="server" Font-Size="Small" Width="144px" Height="16px">2) Parents Information</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label13" runat="server" Font-Size="Small" Width="144px" Height="16px" ForeColor="RoyalBlue">3) Players Registration</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label12" runat="server" Font-Size="Small" Width="144px" Height="16px">4) Coaches Sign Up</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label10" runat="server" Font-Size="Small" Width="144px" Height="16px">5) Sponsors Sign Up</asp:Label></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="Label9" runat="server" Font-Size="Small" Width="144px" Height="16px">6) Payment Completion</asp:Label></TD>
											</TR>
										</TABLE>
										<asp:dropdownlist id="cmbGradeOLD" style="Z-INDEX: 126; LEFT: 429px; POSITION: absolute; TOP: 160px"
											tabIndex="10" runat="server" Font-Size="X-Small" Width="56px" Height="32px" Enabled="False" Visible="False">
											<asp:ListItem Value="0">K</asp:ListItem>
											<asp:ListItem Value="1">1st</asp:ListItem>
											<asp:ListItem Value="2">2nd</asp:ListItem>
											<asp:ListItem Value="3">3rd</asp:ListItem>
											<asp:ListItem Value="4">4th</asp:ListItem>
											<asp:ListItem Value="5">5th</asp:ListItem>
											<asp:ListItem Value="6">6th</asp:ListItem>
											<asp:ListItem Value="7">7th</asp:ListItem>
											<asp:ListItem Value="8">8th</asp:ListItem>
											<asp:ListItem Value="9">9th</asp:ListItem>
											<asp:ListItem Value="10">10th</asp:ListItem>
											<asp:ListItem Value="11">11th</asp:ListItem>
											<asp:ListItem Value="12">12th</asp:ListItem>
											<asp:ListItem Value="13" Selected="True">N/A</asp:ListItem>
										</asp:dropdownlist>
										<asp:panel id="Panel2" style="Z-INDEX: 127; LEFT: 200px; POSITION: absolute; TOP: 280px" runat="server"
											Width="544px" Height="49px" Font-Bold="True">
											<SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Script MT Bold'"><SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">
													<SPAN style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'">&nbsp;**Select the Player 
														and register him(her) by clicking on Register before continuing (unless you are 
														registering only for coaching).</SPAN></SPAN></SPAN></asp:panel>
									    
									</DIV>
								</td>
							</table>
					</td>
					<td></td>
				</tr>
			</table>
</asp:Content>