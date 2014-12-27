<%@ Page Language="vb" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" Codefile="RegFinish.aspx.vb" Inherits="RegFinish" %>
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
									<DIV style="WIDTH: 768px; POSITION: relative; HEIGHT: 330px" ms_positioning="GridLayout">&nbsp;&nbsp;
										<asp:label id="lblTitle" style="Z-INDEX: 100; LEFT: 320px; POSITION: absolute; TOP: 8px" runat="server"
											Font-Underline="True" Font-Size="Large" ForeColor="RoyalBlue" Font-Bold="True" Height="24px"
											Width="152px"> Registration</asp:label><asp:label id="Label9" style="Z-INDEX: 102; LEFT: 49px; POSITION: absolute; TOP: 80px" runat="server"
											Font-Underline="True" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="16px" Width="106px">Registration Date:</asp:label><asp:label id="Label1" style="Z-INDEX: 103; LEFT: 49px; POSITION: absolute; TOP: 96px" runat="server"
											Font-Underline="True" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="16px" Width="106px">Amount Paid:</asp:label>
                                        <asp:label id="Label2" 
                                            style="Z-INDEX: 104; LEFT: 49px; POSITION: absolute; TOP: 112px" runat="server"
											Font-Underline="True" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="16px" Width="106px">Transaction ID:</asp:label>
                                        <INPUT id="txtRegistration" style="border-style: none; border-color: inherit; border-width: thin; FONT-SIZE: 7pt; Z-INDEX: 105; LEFT: 161px; WIDTH: 328px; COLOR: royalblue; POSITION: absolute; TOP: 80px; HEIGHT: 16px"
											readOnly type="text" size="49" name="item_name" runat="server"><INPUT id="txtAmount" style="BORDER-RIGHT: thin; BORDER-TOP: thin; FONT-SIZE: 7pt; Z-INDEX: 106; LEFT: 161px; BORDER-LEFT: thin; WIDTH: 328px; COLOR: royalblue; BORDER-BOTTOM: thin; POSITION: absolute; TOP: 96px; HEIGHT: 16px"
											readOnly type="text" size="49" name="item_name" runat="server"><INPUT id="txtTrxID" style="BORDER-RIGHT: thin; BORDER-TOP: thin; FONT-SIZE: 7pt; Z-INDEX: 107; LEFT: 161px; BORDER-LEFT: thin; WIDTH: 328px; COLOR: royalblue; BORDER-BOTTOM: thin; POSITION: absolute; TOP: 112px; HEIGHT: 16px"
											readOnly type="text" size="49" name="item_name" runat="server">
										<asp:datagrid id="grdPlayers" style="Z-INDEX: 108; LEFT: 46px; POSITION: absolute; TOP: 136px; width: 584px;"
											runat="server" Font-Size="Small" Height="87px" BackColor="Transparent" AutoGenerateColumns="False"
											AllowSorting="True" CellPadding="3" GridLines="None" BorderColor="RoyalBlue" BorderWidth="1px"
											BorderStyle="None">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
											<ItemStyle ForeColor="RoyalBlue"></ItemStyle>
											<HeaderStyle Font-Size="Small" Font-Bold="True" ForeColor="RoyalBlue" BorderStyle="Solid"></HeaderStyle>
											<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Name">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" 
                                                        Width="200px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Div_Desc" HeaderText="Division">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" 
                                                        Width="100px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DraftVenue" HeaderText="Venue" ReadOnly="True">
													<HeaderStyle Font-Size="Small" Font-Underline="True" ForeColor="RoyalBlue" 
                                                        Width="120px"></HeaderStyle>
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DraftDate" DataFormatString=" {0:MMM-dd-yyyy}" 
                                                    HeaderText="Date">
													<HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="True" Width="70px" />
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DraftTime" HeaderText="Time">
													<HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="True" />
													<ItemStyle Font-Size="Small" ForeColor="RoyalBlue"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid><asp:button id="btnDone" style="Z-INDEX: 109; LEFT: 648px; POSITION: absolute; TOP: 96px" tabIndex="1"
											runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="32" Width="97" BackColor="LightGray"
											BorderColor="Silver" BorderStyle="Outset" Text="DONE"></asp:button><asp:button id="btnPrint" style="Z-INDEX: 111; LEFT: 648px; POSITION: absolute; TOP: 136px"
											tabIndex="1" runat="server" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="32" Width="97" BackColor="LightGray" BorderColor="Silver" BorderStyle="Outset"
											Text="PRINT" Visible="False"></asp:button><asp:label id="lblMSG" style="Z-INDEX: 112; LEFT: 184px; POSITION: absolute; TOP: 304px" runat="server"
											Font-Size="Medium" ForeColor="Red" Font-Bold="True" Height="16px" Width="536px"></asp:label>
                                        <asp:label id="lblSponsor" style="Z-INDEX: 113; LEFT: 50px; POSITION: absolute; TOP: 264px"
											runat="server" Font-Underline="True" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="16px" 
                                            Width="106px" Visible="False">Sponsor:</asp:label>
                                        <asp:label id="lblSponsorAmount" style="Z-INDEX: 114; LEFT: 49px; POSITION: absolute; TOP: 285px"
											runat="server" Font-Underline="True" Font-Size="Small" ForeColor="RoyalBlue" Font-Bold="True" Height="16px" 
                                            Width="106px" Visible="False">Sponsor Amount:</asp:label>
                                        <INPUT id="txtSponsor" style="border-style: none; border-color: inherit; border-width: thin; FONT-SIZE: 7pt; Z-INDEX: 116; LEFT: 153px; WIDTH: 328px; COLOR: royalblue; POSITION: absolute; TOP: 264px; HEIGHT: 16px"
											readOnly type="text" size="49" name="item_name" runat="server" visible="false">
										<asp:label id="Label4" style="Z-INDEX: 117; LEFT: 464px; POSITION: absolute; TOP: 16px" runat="server"
											Font-Size="Small" ForeColor="Black" Height="56px" Width="289px" BorderColor="Gray" BorderWidth="0px"
											BorderStyle="Solid"></asp:label>
                                        <INPUT id="txtSponsorAmount" style="border-style: none; border-color: inherit; border-width: thin; FONT-SIZE: 7pt; Z-INDEX: 115; LEFT: 153px; WIDTH: 328px; COLOR: royalblue; POSITION: absolute; TOP: 283px; HEIGHT: 16px"
											readOnly type="text" size="49" name="item_name1" runat="server" visible="false"></DIV>
								</td>
							</tr>
							<tr>
								<td></td>
							</tr>
						</table>
				</tr>
			</table>
</asp:Content>
