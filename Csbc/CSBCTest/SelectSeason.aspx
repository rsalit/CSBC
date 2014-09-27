<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="SelectSeason.aspx.cs" Inherits="CSBC.Admin.Web.SelectSeason" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-10">
            <div style="overflow-y: scroll; height: 600px">
                <asp:GridView ID="grdSeasons" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered table-condensed"
                    RowStyle-CssClass="td"
                    HeaderStyle-CssClass="th"
                    ItemType="CSBC.Admin.Web.ViewModels.SelectSeasonVM"    
                    OnRowCommand="grdSeasons_RowCommand"
                    DataKeyNames="SeasonID">
                    <Columns>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Activate</HeaderTemplate>
                            <HeaderStyle Wrap="true" Width="60px" />

                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSeasonActivate" runat="server" Text="Activate"
                                    CommandName="SelectSeason"
                                    CommandArgument='<%# Item.SeasonID %>'></asp:LinkButton>
                            </ItemTemplate>

                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Season" ShowHeader="true" >
                            <HeaderStyle Wrap="true" Width="180px" />

                            <HeaderTemplate>Season</HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSeasonDescription" runat="server" 
                                    Text='<%# Item.Description %>'
                                    CssClass="text-left"
                                    CommandName="ViewSeason"
                                    CommandArgument='<%# Item.SeasonID %>' ></asp:LinkButton>
                            </ItemTemplate>

                            <ItemStyle Width="180px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromDate"  DataFormatString="{0:d}" HeaderText="From"  />
                        <asp:BoundField DataField="ToDate" DataFormatString="{0:d}" HeaderText="To" />
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Current</HeaderTemplate>
                            <HeaderStyle Wrap="true" Width="40px" />

                            <ItemTemplate>
                                <asp:CheckBox runat="server" DataField="CurrentSeason" HeaderText="Current"
                                    Checked='<%# Convert.ToBoolean(Item.CurrentSeason) %>'
                                    CssClass="checkbox text-center"/>
                            </ItemTemplate>
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Schedules</HeaderTemplate>
                            <HeaderStyle Wrap="true" Width="40px" />

                            <ItemTemplate>
                                <asp:CheckBox runat="server" 
                                    DataField="CurrentSchedule" 
                                    Checked='<%# Convert.ToBoolean(Item.CurrentSeason) %>' />
                            </ItemTemplate>
                            <ItemStyle Width="40px"></ItemStyle>

                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="true">
                            <HeaderTemplate>Online</HeaderTemplate>
                            <HeaderStyle Wrap="true" Width="40px" />

                            <ItemTemplate>
                                <asp:CheckBox runat="server"
                                    DataField="CurrentSignups"
                                    Checked='<%# Convert.ToBoolean(Item.CurrentSignUps) %>' />
                            </ItemTemplate>


                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
            <div>
            </div>

            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="727px"></asp:Label>
        </div>
    </div>

</asp:Content>
