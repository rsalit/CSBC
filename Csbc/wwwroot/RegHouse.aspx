<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="RegHouse.aspx.vb" Inherits="RegHouse" Title="Household"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			&lt;header<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td width="50%"></td>
					<td valign="top" bgcolor="#ffffff" height="100%" style="width: 743px">
						<table cellspacing="0" cellpadding="0" border="0" style="width: 744px">
							<tr>
								<td valign="top" bgcolor="#ffffff" height="100%" style="width: 495px">
									<div style="WIDTH: 768px; POSITION: relative; HEIGHT: 330px; left: 0px; top: 0px;">&nbsp;&nbsp;
										<asp:label id="lblTitle" style="Z-INDEX: 101; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Underline="True" Height="24px" Width="232px" ForeColor="RoyalBlue" Font-Bold="True"
											Font-Size="Large"> Registration</asp:label><asp:textbox id="txtName" style="Z-INDEX: 102; LEFT: 288px; POSITION: absolute; TOP: 72px" tabIndex="1"
											runat="server" Height="16px" Width="249" Font-Size="Small" BorderStyle="Inset"></asp:textbox><asp:button id="comNext" style="Z-INDEX: 104; LEFT: 632px; POSITION: absolute; TOP: 56px" tabIndex="12"
											runat="server" Height="32" Width="97" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="X-Small" BorderStyle="Outset" Text=">> Continue >>" BackColor="LightGray" BorderColor="Silver" ToolTip="Save and Continue"></asp:button><asp:textbox id="txtAddress" style="Z-INDEX: 107; LEFT: 288px; POSITION: absolute; TOP: 96px"
											tabIndex="2" runat="server" Height="16px" Width="249px" Font-Size="Small" BorderStyle="Inset"></asp:textbox><asp:textbox id="txtAddress2" style="Z-INDEX: 108; LEFT: 288px; POSITION: absolute; TOP: 120px"
											tabIndex="3" runat="server" Height="16px" Width="249px" Font-Size="Small" BorderStyle="Inset"></asp:textbox><asp:textbox id="txtCity" style="Z-INDEX: 109; LEFT: 288px; POSITION: absolute; TOP: 144px" tabIndex="4"
											runat="server" Height="16px" Width="176px" Font-Size="Small" BorderStyle="Inset">Coral Springs</asp:textbox><asp:textbox id="txtZip" style="Z-INDEX: 110; LEFT: 472px; POSITION: absolute; TOP: 144px" tabIndex="5"
											runat="server" Height="16px" Width="64px" Font-Size="Small" BorderStyle="Inset"></asp:textbox><asp:textbox id="txtPhone1" style="Z-INDEX: 111; LEFT: 288px; POSITION: absolute; TOP: 168px"
											tabIndex="6" runat="server" Height="16px" Width="33" Font-Size="Small" BorderStyle="Inset" MaxLength="3">954</asp:textbox><asp:textbox id="txtPhone2" style="Z-INDEX: 112; LEFT: 320px; POSITION: absolute; TOP: 168px"
											tabIndex="7" runat="server" Height="16px" Width="33px" Font-Size="Small" BorderStyle="Inset" MaxLength="3"></asp:textbox><asp:textbox id="txtPhone3" style="Z-INDEX: 113; LEFT: 352px; POSITION: absolute; TOP: 168px"
											tabIndex="8" runat="server" Height="16px" Width="48px" Font-Size="Small" BorderStyle="Inset" MaxLength="4"></asp:textbox><asp:textbox id="txtEmailOLD" style="Z-INDEX: 114; LEFT: 616px; POSITION: absolute; TOP: 104px"
											tabIndex="8" runat="server" Height="16px" Width="41" Font-Size="Small" BackColor="White" MaxLength="50" Visible="False"></asp:textbox><asp:textbox id="txtEmail" style="Z-INDEX: 115; LEFT: 288px; POSITION: absolute; TOP: 192px"
											tabIndex="9" runat="server" Height="16px" Width="249px" Font-Size="Small" BorderStyle="Inset"></asp:textbox><asp:label id="lblHouseName" style="Z-INDEX: 116; LEFT: 200px; POSITION: absolute; TOP: 72px"
											runat="server" Height="24px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">Family:</asp:label><asp:label id="lblAddress1" style="Z-INDEX: 117; LEFT: 200px; POSITION: absolute; TOP: 96px"
											runat="server" Height="26px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">Address:</asp:label><asp:label id="Label1" style="Z-INDEX: 118; LEFT: 200px; POSITION: absolute; TOP: 120px" runat="server"
											Height="26px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">Address(cont.):</asp:label><asp:label id="lblCity" style="Z-INDEX: 119; LEFT: 200px; POSITION: absolute; TOP: 144px" runat="server"
											Height="26px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">City / Zip:</asp:label><asp:label id="lblPhone" style="Z-INDEX: 120; LEFT: 200px; POSITION: absolute; TOP: 168px"
											runat="server" Height="26px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">Home Phone:</asp:label><asp:label id="Label11" style="Z-INDEX: 121; LEFT: 200px; POSITION: absolute; TOP: 192px" runat="server"
											Height="26px" Width="88px" ForeColor="RoyalBlue" Font-Bold="True" Font-Size="Small">Email:</asp:label><asp:label id="lblMSG" style="Z-INDEX: 122; LEFT: 208px; POSITION: absolute; TOP: 248px" runat="server"
											Height="16px" Width="520px" ForeColor="Red" Font-Bold="True" Font-Size="Medium"></asp:label><asp:regularexpressionvalidator id="revEmail" style="Z-INDEX: 123; LEFT: 288px; POSITION: absolute; TOP: 224px"
											runat="server" Width="168px" Font-Bold="True" Font-Size="Medium" ErrorMessage="Invalid Email format" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:regularexpressionvalidator>
										<table id="Table1" style="Z-INDEX: 124; LEFT: 8px; WIDTH: 152px; POSITION: absolute; TOP: 192px; HEIGHT: 134px"
											cellspacing="1" cellpadding="1" width="152" border="0">
											<tr>
												<td><asp:label id="Label4" runat="server" Height="16px" Width="144px" Font-Bold="True" Font-Size="Medium">Registration Steps:</asp:label></td>
											</tr>
											<tr>
												<td style="HEIGHT: 20px"><asp:label id="Label5" runat="server" Height="16px" Width="144px" ForeColor="RoyalBlue" Font-Bold="True"
														Font-Size="Small" BorderStyle="None" BorderColor="Silver" BorderWidth="2px">1) Household Information</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label6" runat="server" Height="16px" Width="144px" Font-Size="Small">2) Parents Information</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label7" runat="server" Height="16px" Width="144px" Font-Size="Small">3) Players Registration</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label8" runat="server" Height="16px" Width="144px" Font-Size="Small">4) Coaches Sign Up</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label17" runat="server" Height="16px" Width="144px" Font-Size="Small">5) Sponsors Sign Up</asp:label></td>
											</tr>
											<tr>
												<td><asp:label id="Label18" runat="server" Height="16px" Width="144px" Font-Size="Small">6) Payment Completion</asp:label></td>
											</tr>
										</table>
										<asp:panel id="Panel2" style="Z-INDEX: 126; LEFT: 208px; POSITION: absolute; TOP: 280px" runat="server"
											Height="48px" Width="525px" Font-Bold="True"><span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Script MT Bold'"></span>
												<span style="FONT-SIZE: 14pt; FONT-FAMILY: 'Agency FB'"></span>
										</asp:panel>
										<asp:HyperLink id="hpEmail" style="Z-INDEX: 127; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
											Width="184px" Height="16px" Visible="False" NavigateUrl="RegFinish.aspx?Resend=YES" ToolTip="Registration Completed">Resend Confirmation Email</asp:HyperLink>
                                        <asp:TextBox ID="TxtEmailListing" runat="server" BackColor="White" Font-Size="Small" Height="16px"
                                            MaxLength="50" Style="z-index: 114; left: 616px; position: absolute; top: 128px"
                                            TabIndex="8" Visible="False" Width="41"></asp:TextBox>
                                        <asp:TextBox ID="txtState" runat="server" BackColor="White" Font-Size="Small" Height="16px"
                                            MaxLength="50" Style="z-index: 114; left: 616px; position: absolute; top: 152px"
                                            TabIndex="8" Visible="False" Width="41"></asp:TextBox>
                                    </div>
								</td>
							</tr>
						</table>
					</td>
					<td width="50%" background="images/bg_lft.gif"></td>
				</tr>
			</table>
</asp:Content>
