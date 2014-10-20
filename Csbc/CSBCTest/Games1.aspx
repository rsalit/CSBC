<%@ Page Title="Scheduled Games" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Games1.aspx.cs" Inherits="CSBC.Admin.Web.Games1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <juice:Datepicker runat="server" ID="dpGameDate" TargetControlID="mskDate" />

    <section>
        <div class="row-fluid ">
            <div class="col-sm-12 panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Filters</div>

                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class=" col-sm-3">
                             <div class="control-label">Division</div>
                            <asp:DropDownList ID="cmbDivisions" runat="server" TabIndex="1"
                                CssClass="dropdown form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="cmbDivisions_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <%--<div class="checkbox checkbox-inline  col-sm-2">
                            <asp:CheckBox ID="checkAllDivisions"
                                runat="server"
                                AutoPostBack="true"
                                TextAlign="right"
                                CssClass="checkbox "
                                Text="All"
                                OnCheckedChanged="checkAllDivisions_CheckedChanged" />
                        </div>--%>
                        <%--<label for="cmbDivisions" class="control-label">Divisions</label>--%>


                        <div class="col-sm-3 radio-inline">
                            <div class="control-label">Schedule Type</div>
                            <asp:RadioButtonList ID="radioRegularorPlayoff" runat="server" AutoPostBack="true"
                                CssClass="radio-inline" RepeatColumns="1" OnSelectedIndexChanged="radioRegularorPlayoff_SelectedIndexChanged">
                                <asp:ListItem Text="Regular Season" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Playoff" Value="P"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class=" ">
                            <div class="control-label">Schedule Dates</div>
                           
                            <div class="form-group  col-sm-3">
                                <div class='input-group date ' id='datetimepicker1'>
                                    <asp:TextBox ID="txtScheduleDate" type='text' 
                                        CssClass="form-control date-picker" 
                                        AutoPostBack="true" 
                                        OnTextChanged="txtScheduleDate_TextChanged" 
                                        runat="server" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                             <asp:CheckBox ID="checkAllDates" runat="server" AutoPostBack="true" CssClass="Checkbox col-sm-2" Text="All Dates" OnCheckedChanged="checkAllDates_CheckedChanged" Visible="true" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-8">

                <asp:Panel ID="panelRegularGamesGrid" runat="server">
                    <header>
                        <h3>Regular Season Games</h3>
                    </header>
                    <div style="overflow-y: auto; max-height: 300px">
                        <asp:GridView runat="server" ID="grdGames"
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-condensed table-bordered"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            ItemType="CSBC.Admin.Web.ViewModels.ScheduleGamesVM"
                            AutoPostBack="True"
                            OnRowCommand="grdGames_RowCommand">
                            <EmptyDataTemplate>No Games Found!</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Date</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGameDate"
                                            runat="server"
                                            Text='<%# Item.GameDate.ToShortDateString() %>'
                                            CommandName="SelectGame"
                                            CommandArgument='<%# Item.ScheduleNumber.ToString() + ":" + Item.GameNumber.ToString()   %>'>
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Game</HeaderTemplate>
                                    <HeaderStyle Width="120px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGameTime" runat="server" Text='<%# Item.GameTime %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="120px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Location</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Item.LocationName %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Home</HeaderTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGameType" runat="server" Text='<%# Item.HomeTeam %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Visitor</HeaderTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVisitingTeam" runat="server" Text='<%# Item.VisitorTeam %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Division</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDivision" runat="server" Text='<%# Item.Division %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panelPlayoffGrid" runat="server">
                    <header>
                            <h3>Playoff Games</h3>
                        </header>
                    <div style="overflow-y: auto; max-height: 300px">
                        
                        <asp:GridView runat="server" ID="grdPlayoffGames"
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-condensed table-bordered"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            ItemType="CSBC.Admin.Web.ViewModels.ScheduleGamesVM"
                            AutoPostBack="True"
                            OnRowCommand="grdGames_RowCommand">
                            <EmptyDataTemplate>No Games Found!</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Date</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGamePlayoffDate"
                                            runat="server"
                                            Text='<%# Item.GameDate.ToShortDateString() %>'
                                            CommandName="SelectPlayoff"
                                            CommandArgument='<%# Item.ScheduleNumber.ToString() + ":" + Item.GameNumber.ToString()  %>'>
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Time</HeaderTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGamePlayoffTime" runat="server" Text='<%# Item.GameTime %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Location</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlayoffLocation" runat="server" Text='<%# Item.LocationName %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Home</HeaderTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlayoffTeam1" runat="server" Text='<%# Item.HomeTeam %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Visitor</HeaderTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlayoffTeam2" runat="server" Text='<%# Item.VisitorTeam %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>Division</HeaderTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDivision1" runat="server" Text='<%# Item.Division %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                </asp:Panel>

            </div>
            <div class="col-sm-4">
                <section class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="panel-title">Game details</div>
                    </div>
                    <div class="panel-body">
                        <asp:DropDownList ID="ddlDivisions" runat="server" TabIndex="1"
                            CssClass="form-control dropdown">
                        </asp:DropDownList>
                        <div class="col-sm-6">

                            <label for="mskDate" class="control-label">Date</label>
                            <asp:TextBox ID="mskDate" runat="server"
                                TabIndex="2" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <label for="txtTime" class="control-label">Time</label>
                            <asp:TextBox ID="txtTime" runat="server"
                                Placeholder="NN:NN PM"
                                TabIndex="3"
                                CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Label ID="lblPlayoff" runat="server" Text="* Playoff *" Visible="False"></asp:Label>
                        <div class="col-sm-10">
                            <label id="lblLocation" for="cmbVenues">Location</label>
                            <asp:DropDownList ID="cmbVenues" runat="server" TabIndex="4" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-5">
                            <label id="lblHome" for="txtHome" class="control-label">Home Team</label>
                            <asp:TextBox ID="txtHome" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-5">
                            <label id="lblVisitor" for="txtVisitor" class="control-label">Visiting Team</label>
                            <asp:TextBox ID="txtVisitor" runat="server" TabIndex="6"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-5">
                            <asp:Label ID="lblDescription" for="txtDescr" runat="server" Visible="False" CssClass="control-label">Description</asp:Label>
                            <asp:TextBox ID="txtDescr" runat="server" TabIndex="7" Visible="False" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 panel-footer btn-group">
                        <asp:Button ID="btnNew" runat="server" TabIndex="8" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" />
                        <asp:Button ID="btnSave" runat="server" TabIndex="9" Text="Save" CssClass="btn btn-primary"
                            OnClientClick="javascript:return validate();"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" TabIndex="10" Text="Delete" Enabled="False" CssClass="btn btn-primary" />
                        <%--<asp:Button ID="btnSend" runat="server" TabIndex="11" Text="Email" CssClass="btn btn-primary" />--%>
                    </div>
                </section>

                <asp:Label ID="lblDeleteDate" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                    Height="61px" Width="89px"></asp:Label>
            </div>
        </div>
    </section>
    <%--<asp:ListBox ID="lstEmails" runat="server" Font-Names="Courier New" Font-Size="Small"
        Height="88px" SelectionMode="Multiple" Width="408px"></asp:ListBox>--%>

    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
        Width="600px"></asp:Label>
    <script type="text/javascript">
        $(document).ready(function () {
            var seasonType = ('#radioRegularorPlayoff').valueOf();
            $("button").click(function () {
                $('div').addClass("important");

            });
        });

        $(function () {

            $("#mskDate").datepicker();

        });

    </script>
    <script type="text/javascript">
        function validate() {

            var isValid = true;
            var txt = document.getElementById('mskDate');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Date required", "Error");
            }
            else {
                var date = Date(txt.value)
                if (date == null) {
                    isValid = false;
                    toastr.error("Date not in date format", "Error");
                }
            }
            txt = document.getElementById('txtTime');
            if (txt.value == null) {
                isValid = false;
                toastr.error("Time must be entered", "Error");
            }

            txt = document.getElementById('txtHome');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Home Team required", "Error");
            }

            txt = document.getElementById('txtVisitor');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Visiting Team required", "Error");
            }

            return isValid;
        }
        $(".date-picker").datepicker();

        $(".date-picker").on("change", function () {
            var id = $(this).attr("id");
            var val = $("label[for='" + id + "']").text();
            $("#msg").text(val + " changed");
        });
    </script>


</asp:Content>
