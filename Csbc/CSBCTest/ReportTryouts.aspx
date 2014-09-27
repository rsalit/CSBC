<%@ Page Language="VB" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="false" CodeFile="ReportTryouts.aspx.vb" Inherits="ReportTryouts" title="Tryouts Reports" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 765px; height: 198px">
        <tr>
            <td align="center" rowspan="9" colspan="2" valign="top">
                &nbsp;<igtbl:UltraWebGrid ID="grdDivisions" runat="server" Height="309px" Width="397px" style="left: -97px; top: 109px">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet">
                            </AddNewRow>
                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                            </FilterOptions>
                        </igtbl:UltraGridBand>
                    </Bands>
                    <DisplayLayout AllowColSizingDefault="Free" AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                        CellClickActionDefault="RowSelect" ColWidthDefault="33%" HeaderClickActionDefault="SortMulti"
                        Name="grdHouseholds" NoDataMessage="No Households found" RowHeightDefault="20px" RowSelectorsDefault="No"
                        ScrollBarView="Vertical" SelectTypeRowDefault="Single"
                        StationaryMarginsOutlookGroupBy="True"
                        Version="4.00" ScrollBar="Always" StationaryMargins="Header" TableLayout="Fixed">
                        <GroupByBox>
                            <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                        </GroupByBox>
                        <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                        </GroupByRowStyleDefault>
                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                        </FooterStyleDefault>
                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderWidth="1px">
                            <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            <Padding Left="3px" />
                        </RowStyleDefault>
                        <FilterOptionsDefault>
                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                Font-Size="11px" Width="200px">
                                <Padding Left="2px" />
                            </FilterDropDownStyle>
                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                            </FilterHighlightRowStyle>
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
                        <FrameStyle BackColor="Window" BorderColor="Navy" BorderStyle="None" BorderWidth="1px"
                            Font-Names="Arial" Font-Size="9pt" Height="309px" Width="397px">
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
                        <ActivationObject BorderColor="" BorderWidth="">
                        </ActivationObject>
                    </DisplayLayout>
                </igtbl:UltraWebGrid></td>
            <td align="center" style="width: 107px; height: 26px">
            </td>
            <td style="width: 107px; height: 26px" align="center">
                <asp:Button ID="btnByDivision" runat="server" Text="By Division" Width="104px" /></td>
        </tr>
        <tr>
            <td align="center" style="width: 107px; height: 26px">
            </td>
            <td style="width: 107px; height: 26px" align="center">
                <asp:Button ID="btnAllPlayers" runat="server" Text="All Players" Width="104px" /></td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="height: 10px; width: 107px;">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" rowspan="1">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
            <td style="width: 107px; height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" rowspan="1" valign="top">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#C00000" Width="735px"></asp:Label></td>
        </tr>
    </table>
</asp:Content>


