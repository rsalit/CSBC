<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Seasons1.aspx.cs" Inherits="CSBC.Admin.Web.Seasons1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <section class="col-md-6">
            <div class="panel panel-primary" id="SeasonSummary">
                <header class="panel-heading">
                    <div class="panel-title">
                        Season Summary
                    </div>
                </header>
                <div style="overflow-y: scroll; height: 360px">
                    <asp:GridView ID="grdSeasons" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-striped table-condensed table-bordered"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        OnRowCommand="grdSeasons_RowCommand"
                        ItemType="CSBC.Admin.Web.ViewModels.SelectSeasonVM"
                        DataKeyNames="SeasonID">
                        <Columns>
                            <%--<asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>Activate</HeaderTemplate>
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSeasonActivate" runat="server" Text="Activate"
                                        CommandName="SelectSeason"
                                        CommandArgument='<%# Item.SeasonID %>'></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Season" ShowHeader="true">
                                <HeaderTemplate>Season</HeaderTemplate>
                                <HeaderStyle Width="160px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSeasonDescription" runat="server"
                                        Text='<%# Item.Description %>'
                                        CssClass="text-left"
                                        CommandName="ViewSeason"
                                        CommandArgument='<%# Item.SeasonID %>'></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="160px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FromDate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%# Bind("FromDate", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="130px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ToDate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ToDate", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="130px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>Current</HeaderTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server"
                                        Checked='<%# Convert.ToBoolean(Item.CurrentSeason) %>'
                                        CssClass="checkbox text-center" />
                                </ItemTemplate>
                                <ItemStyle Width="40px"></ItemStyle>

                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>Schedules</HeaderTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server"
                                        Checked='<%# Convert.ToBoolean(Item.CurrentSchedule) %>'
                                        CssClass="checkbox text-center" />
                                </ItemTemplate>
                                <ItemStyle Width="40px"></ItemStyle>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="true">
                                <HeaderTemplate>Online</HeaderTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server"
                                        Checked='<%# Convert.ToBoolean(Item.CurrentSignUps) %>'
                                        CssClass="checkbox text-center" />
                                </ItemTemplate>
                                <ItemStyle Width="40px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle CssClass="th"></HeaderStyle>

                        <RowStyle CssClass="tr"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
        </section>
        <%--</div>
    <div class="row">--%>
        <section class="col-md-6">
            <div class="panel panel-primary" id="SeasonDetail">
                <header class="panel-heading">
                    <div class="panel-title">Season Detail</div>
                </header>
                <div class="panel-body">
                    <div class="form-group col-md-10">
                        <label for="txtName" class="control-label">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="mskStartDate" class="control-label">Start Date</label>
                        <asp:TextBox ID="mskStartDate" runat="server" CssClass="form-control" TextMode="Date" TabIndex="2"> </asp:TextBox>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="mskEndDate" class="control-label">End Date</label>
                        <asp:TextBox ID="mskEndDate" runat="server" TextMode="Date" CssClass="form-control" TabIndex="3"> </asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="txtPlayersFee" class="control-label">Player Fee</label>
                        <asp:TextBox ID="txtPlayersFee" runat="server" CssClass="form-control" TabIndex="4" TextMode="Number"> </asp:TextBox>


                    </div>
                    <div class="form-group col-md-4">
                        <label for="mskSponsorFee" class="control-label">Sponsor Fee</label>
                        <asp:TextBox ID="mskSponsorFee" runat="server" CssClass="form-control" TabIndex="5"> </asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="mskSponsorFeeDiscounted" class="control-label">Sponsor Disc.</label>
                        <asp:TextBox ID="mskSponsorFeeDiscounted" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="mskORStart" class="control-label">Online Starts</label>
                        <asp:TextBox ID="mskORStart" runat="server" CssClass="form-control" TextMode="Date" TabIndex="11"> </asp:TextBox>

                    </div>
                    <div class="form-group col-md-6">

                        <label for="mskOREnds" class="control-label">Online Stops</label>
                        <asp:TextBox ID="mskOREnd" runat="server" CssClass="form-control" TextMode="Date"  TabIndex="12"> </asp:TextBox>

                    </div>

                    <div class="well-sm border col-md-8">
                        <div class="checkbox checkbox-inline">
                            <asp:CheckBox ID="chkSchedules" runat="server" Text="Games Schedules" CssClass="checkbox" TabIndex="8" />
                            <asp:CheckBox ID="chkCurrentSeason" runat="server" Text="Current Season" CssClass="checkbox" TabIndex="9" />
                            <asp:CheckBox ID="chkRegistration" runat="server" Text="Online Registration" CssClass="checkbox" AutoPostBack="True" TabIndex="10" />
                        </div>
                    </div>


                    <div class="form-group col-md-4">
                        <asp:Label ID="lblNewYear" runat="server" Text="New School Year?:" CssClass="control-label" Visible="False"></asp:Label>
                        <asp:CheckBox ID="chkNewSchool" runat="server" CssClass="form-control" Text="Yes" Visible="False" TabIndex="7" />
                    </div>
                    <asp:HiddenField runat="server" ID="txtSeasonID" />
                </div>

                <div class="panel-footer btn-group">
                    <asp:Button ID="btnNew" runat="server" Text="New Season" CssClass="btn btn-primary" OnClick="btnNew_OnClick" TabIndex="13" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" TabIndex="14" Visible="False" OnClick="btnSave_OnClick" />
                </div>
            </div>
        </section>
    </div>
    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="727px"></asp:Label>

    <script type="text/javascript">
        function GetUserAccess() {
            var access = '<%= Session["AccessType"] %>';
                   if (access == "R") {
                       var nameField = getElementById("txtName");
                       nameField.disabled = "disabled";
                   }
               }


    </script>
</asp:Content>
