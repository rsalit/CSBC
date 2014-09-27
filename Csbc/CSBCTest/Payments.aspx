<%@ Page Language="VB" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="false" CodeFile="Payments.aspx.vb" Inherits="Payments" title="Registration/Payment" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 430px; height: 164px">
        <tr>
            <td align="left" colspan="4" valign="top" style="height: 26px" bgcolor="#e0e0e0">
                &nbsp;<asp:LinkButton ID="lnkName" runat="server" TabIndex="20" Width="269px"></asp:LinkButton>
                <asp:ImageButton ID="imgLast" runat="server" Height="20px" ImageUrl="~/Images/SEARCH.JPG"
                    TabIndex="18" Width="20px" ToolTip="Search By People" /></td>
            <td align="left" colspan="1" style="height: 26px" valign="bottom">
                <asp:Label ID="lblDraftID" runat="server" Height="20px" Text="Draft ID:" Width="80px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 26px" valign="bottom">
                <asp:TextBox ID="txtDraftID" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="15px"
                    TabIndex="1" Width="30px" MaxLength="3"></asp:TextBox></td>
            <td align="left" colspan="3" style="height: 26px">
                    <asp:LinkButton ID="btnTeam" runat="server" TabIndex="20" Width="300px"></asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" bgcolor="#e0e0e0" colspan="4" style="height: 20px" valign="bottom">
                <asp:Label ID="lblAddress" runat="server" Height="20px" Width="280px">Address</asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <asp:Label ID="lblDraftNote" runat="server" Height="20px" Text="Draft Note:" Width="80px"></asp:Label></td>
            <td align="left" colspan="4" style="height: 20px" valign="bottom">
                <asp:TextBox ID="txtDraftNotes" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="15px"
                    TabIndex="2" Width="354px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="left" bgcolor="#e0e0e0" colspan="4" style="height: 20px" valign="bottom">
                <asp:Label ID="lblCSZ" runat="server" Height="20px" Width="280px">City, State Zip</asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <asp:Label ID="lblRating" runat="server" Height="20px" Text="Rating:" Width="80px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom"><asp:DropDownList ID="cboRating" runat="server" Height="20px" Width="54px" TabIndex="3">
                <asp:ListItem Value="0">N/A</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>1/2</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>2/3</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>3/4</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>4/5</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>5/6</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>6/7</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>7/8</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem Value="9">C/R</asp:ListItem>
            </asp:DropDownList></td>
            <td align="right" style="height: 20px" colspan="2">
                Change Division:</td>
            <td align="center" style="width: 120px; height: 20px"><asp:DropDownList ID="PlaysDownUp" runat="server" Height="20px" Width="96px" TabIndex="3">
                <asp:ListItem Value="0">N/A</asp:ListItem>
                <asp:ListItem Value="1">Plays Up</asp:ListItem>
                <asp:ListItem Value="2">Plays Down</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" bgcolor="#e0e0e0" colspan="4" style="height: 20px" valign="middle">
                <asp:Label ID="lblPhone" runat="server" Height="20px" Width="280px">Phone</asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="middle">
                <asp:Label ID="lblFee" runat="server" Height="20px" Text="Fee Waived:" Width="80px"></asp:Label></td>
            <td align="left" colspan="2" rowspan="4" valign="top">
                <asp:CheckBoxList ID="chkWaived" runat="server" BackColor="#E0E0E0" BorderColor="Navy"
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Height="16px" RepeatColumns="1"
                    Width="122px" TabIndex="4">
                    <asp:ListItem>Scholarship</asp:ListItem>
                    <asp:ListItem>Rollover</asp:ListItem>
                    <asp:ListItem>Family Discount</asp:ListItem>
                    <asp:ListItem>Athletic Director</asp:ListItem>
                    <asp:ListItem>Partial Refund</asp:ListItem>
                </asp:CheckBoxList></td>
            <td align="center" style="width: 127px; height: 20px">
                <asp:TextBox ID="txtPlaysUp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Height="15px" MaxLength="3" TabIndex="1" Visible="False" Width="30px"></asp:TextBox></td>
            <td align="center" style="width: 120px; height: 20px">
                <asp:Button ID="btnSave" runat="server" TabIndex="11" Text="Save" Width="90px" /></td>
        </tr>
        <tr>
            <td align="left" colspan="4" rowspan="2" valign="bottom">
                <asp:RadioButtonList ID="radPayment" runat="server" BorderColor="Blue" BorderStyle="Solid"
                    BorderWidth="2px" Height="20px" RepeatColumns="4" RepeatDirection="Horizontal"
                    TabIndex="8" TextAlign="Left" Width="80px">
                    <asp:ListItem>Check</asp:ListItem>
                    <asp:ListItem>C.Card</asp:ListItem>
                    <asp:ListItem>Online</asp:ListItem>
                    <asp:ListItem>Cash</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
            </td>
            <td align="right" style="width: 127px; height: 20px">
            </td>
            <td align="center" style="width: 120px; height: 20px">
                <asp:Button ID="btnDelete" runat="server" TabIndex="12" Text="Delete" Width="90px" /></td>
        </tr>
        <tr>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
            </td>
            <td align="right" rowspan="2" style="width: 127px">
            </td>
            <td align="center" rowspan="2" style="width: 120px" valign="top">
                <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                    Visible="False" Width="87px"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="height: 20px" valign="bottom">
                <asp:Label ID="lblAmount" runat="server" Height="20px" Text="Amount:" Width="60px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <igtxt:WebCurrencyEdit ID="mskAmount" runat="server" BorderColor="Black" Height="15px"
                    TabIndex="5" Width="77px">
                </igtxt:WebCurrencyEdit>
            </td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <asp:Label ID="lblBalance" runat="server" Height="20px" Text="Balance:" Width="60px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <igtxt:WebCurrencyEdit ID="mskBalance" runat="server" BorderColor="Black" Height="15px"
                    TabIndex="6" Width="77px">
                </igtxt:WebCurrencyEdit>
            </td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 20px" valign="bottom">
                <asp:Label ID="lblCheck" runat="server" Height="20px" Text="Chk#:" Width="60px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <asp:TextBox ID="txtCheck" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Height="15px" TabIndex="7" Width="77px"></asp:TextBox></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <asp:Label ID="lblDate" runat="server" Height="20px" Text="Date:" Width="60px"></asp:Label></td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
                <igtxt:WebDateTimeEdit ID="mskPayDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Height="15px" TabIndex="8" Width="77px">
                </igtxt:WebDateTimeEdit>
            </td>
            <td align="left" colspan="1" style="height: 20px" valign="bottom">
            </td>
            <td align="left" style="height: 20px" colspan="2">
                <asp:Label ID="lblBM" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                    Visible="False" Width="123px">*Board Member*</asp:Label></td>
            <td align="right" style="width: 127px; height: 20px" valign="middle">
                &nbsp;</td>
            <td align="left" style="width: 120px; height: 20px" valign="middle">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="height: 20px" valign="bottom">
                <asp:Label ID="lblMemo" runat="server" Height="20px" Text="Memo:" Width="60px"></asp:Label></td>
            <td align="left" colspan="4" style="height: 20px">
                <asp:TextBox ID="txtMemo" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="15px"
                    TabIndex="9" Width="312px"></asp:TextBox></td>
            <td align="left" style="width: 150px; height: 20px">
            </td>
            <td align="left" style="width: 150px; height: 20px">
            </td>
            <td align="left" style="width: 127px; height: 20px">
            </td>
            <td align="left" style="width: 150px; height: 20px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="height: 28px" valign="bottom">
            </td>
            <td align="left" colspan="1" style="height: 28px" valign="bottom">
            </td>
            <td align="left" colspan="1" style="height: 28px" valign="bottom">
            </td>
            <td align="left" colspan="1" style="height: 28px" valign="bottom">
            </td>
            <td align="left" colspan="1" style="height: 28px" valign="bottom">
            </td>
            <td align="left" style="width: 150px; height: 28px">
            </td>
            <td align="left" style="width: 127px; height: 28px">
            </td>
            <td align="left" style="width: 120px; height: 28px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="5" valign="top" rowspan="5">
                &nbsp;<igtbl:UltraWebGrid ID="grdPlayers" runat="server" Height="137px" Width="376px">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                            </FilterOptions>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout AllowColSizingDefault="Free" AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                        CellClickActionDefault="RowSelect" ColWidthDefault="33%" HeaderClickActionDefault="SortMulti"
                        Name="grdPlayers" RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Single"
                        TableLayout="Fixed" UseFixedHeaders="True" Version="4.00" StationaryMargins="Header">
                        <GroupByBox>
                            <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                        </GroupByBox>
                        <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                        </GroupByRowStyleDefault>
                        <ActivationObject BorderColor="" BorderWidth="">
                        </ActivationObject>
                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                        </FooterStyleDefault>
                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderWidth="1px">
                            <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            <Padding Left="3px" />
                        </RowStyleDefault>
                        <FilterOptionsDefault>
                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                            </FilterHighlightRowStyle>
                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                Font-Size="11px" Width="200px">
                                <Padding Left="2px" />
                            </FilterDropDownStyle>
                        </FilterOptionsDefault>
                        <SelectedRowStyleDefault BackColor="Gainsboro" ForeColor="Black">
                        </SelectedRowStyleDefault>
                        <HeaderStyleDefault BackColor="#002D62" BorderStyle="Solid" Font-Bold="True" Font-Names="Arial"
                            Font-Size="9pt" ForeColor="White" HorizontalAlign="Center">
                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                        </HeaderStyleDefault>
                        <RowAlternateStyleDefault BackColor="WhiteSmoke">
                        </RowAlternateStyleDefault>
                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                        </EditCellStyleDefault>
                        <FrameStyle BackColor="Window" Font-Names="Arial" Font-Size="9pt" Height="137px"
                            Width="376px">
                        </FrameStyle>
                        <Pager>
                            <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                        </Pager>
                        <AddNewBox>
                            <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
</Style>
                        </AddNewBox>
                    </DisplayLayout>
                </igtbl:UltraWebGrid></td>
            <td align="left" colspan="2" style="height: 20px" valign="bottom">
                <asp:Label ID="LblDivisions" runat="server" Height="20px" Text="Divisions:" Width="80px"></asp:Label></td>
            <td align="left" style="width: 127px; height: 20px">
            </td>
            <td align="left" style="width: 120px; height: 20px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" bordercolor="mediumblue" colspan="3" style="height: 20px">
                <asp:DropDownList ID="cboDivisions" runat="server" Height="20px" Width="216px" TabIndex="13" AutoPostBack="True">
                </asp:DropDownList></td>
            <td align="left" colspan="1" style="height: 20px">
                &nbsp;<asp:Button ID="btnOR" runat="server" TabIndex="14" Text="Add DraftID" Width="90px" /></td>
        </tr>
        <tr>
            <td align="left" bordercolor="mediumblue" colspan="1" style="height: 20px">
            </td>
            <td align="left" style="width: 150px; height: 20px;">
                </td>
            <td align="left" colspan="1" style="height: 20px; width: 127px;">
            </td>
            <td align="left" colspan="1" style="height: 20px">
                </td>
        </tr>
        <tr>
            <td align="left" bordercolor="mediumblue" colspan="1" style="height: 20px">
            </td>
            <td align="left" style="width: 150px; height: 20px;">
                </td>
            <td align="left" colspan="1" style="width: 127px; height: 20px;">
            </td>
            <td align="left" colspan="1" style="height: 20px">
                </td>
        </tr>
        <tr>
            <td align="left" bordercolor="mediumblue" colspan="1" height="20">
            </td>
            <td align="left" height="20" style="width: 150px">
                </td>
            <td align="left" colspan="1" height="20" style="width: 127px">
            </td>
            <td align="left" colspan="1" height="20">
            </td>
        </tr>
        <tr>
            <td align="left" bordercolor="mediumblue" colspan="5" rowspan="1">
                </td>
            <td align="left" colspan="4" rowspan="1" style="height: 7px" valign="top">
                </td>
        </tr>
        <tr>
            <td align="left" colspan="9" style="height: 28px">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="755px"></asp:Label></td>
        </tr>
    </table>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>



