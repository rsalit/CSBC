<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="People1.aspx.cs" Inherits="CSBC.Admin.Web.People1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>
<%@ Register Src="~/PlayerHistory1.ascx" TagPrefix="uc1" TagName="PlayerHistory1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">

    <script src="Scripts/CSBCUI.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function() {
            //alert("Testing - People Page"); 
            $(initPage);
        });
      
        function initPage() {
            //alert("InitPage");
            SetElements();
        }

        function SetElements() {
            $('#<%=mskBirthDate.ClientID%>').change(HandleBirthDate());
        };
        function HandleBirthDate() {    
            var txtDate = $('#<%=mskBirthDate.ClientID%>').val();
            var dob = new Date(txtDate);
            var compareDate = new Date(today - (365 * 12));
            if (dob > compareDate) {
                alert("True");
            } else {
                alert("false");
            }
            alert(txtDate);
        };

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager runat="server" />
    <section class="col-md-5">
        <%--<div class="well">

            <asp:Label ID="lblLastSeason" runat="server" Height="20px" Width="280px">Last Played:</asp:Label><br />
            <asp:Label ID="lblLastRating" runat="server" Height="20px" Width="280px" Visible="False">Last Rating:</asp:Label><br />
            
        </div>--%>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-title">Household</div>
            </div>
            <div class="panel-body">
                <asp:LinkButton ID="lnkHouseName" runat="server" OnClick="lnkHouseName_Click" TabIndex="20"></asp:LinkButton>
                <br />
                <asp:Label ID="lblAddress" runat="server">Address</asp:Label><br />
                <asp:Label ID="lblCSZ" runat="server">City, State Zip</asp:Label><br />
                <asp:Label ID="lblPhone" runat="server">Phone</asp:Label><br />
                <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-body">
                <strong>
                    <asp:Label ID="lblBalance" runat="server" CssClass="col-md-4">Balance:</asp:Label></strong>
                <%--<label for="btnTeam" class="control-label"></label>--%>
                <asp:LinkButton ID="btnTeam" runat="server" TabIndex="20" CssClass="col-md-6 pull-right"></asp:LinkButton>

            </div>
        </div>

        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-title">Player History</div>
            </div>
            <uc1:PlayerHistory1 runat="server" id="PlayerHistory1" />
        </div>
        <%-- <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-title">Player History</div>
            </div>
            <div style="overflow-y: scroll; height: 240px">
                <asp:GridView runat="server" ID="gridPlayerHistory"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered"
                    RowStyle-CssClass="td"
                    HeaderStyle-CssClass="th"
                    ItemType="CSBC.Core.Models.Player"
                    >
                    <EmptyDataTemplate>No history found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Season</HeaderTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSeason" runat="server" Text='<%# Item.Season.Description%>'
                                    CommandName="Select"
                                    CommandArgument='<%# Item.SeasonID%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Rating</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lnkRating" runat="server" Text='<%# Item.Rating%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Team</HeaderTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lnkTeam" runat="server" Text='<%# Item.Team.TeamName%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Coach</HeaderTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lnkCoach" runat="server" Text='<%# Item.Team.Coach.Person.LastName%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="panel-footer">
                
            
            </div>
        </div>--%>

        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-title">Household Members</div>
            </div>
            <div style="overflow-y: scroll; max-height: 240px">
                <asp:GridView runat="server" ID="grdHouseholdMembers"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered"
                    RowStyle-CssClass="td"
                    HeaderStyle-CssClass="th"
                    ItemType="CSBC.Core.Models.Person"
                    OnRowCommand="grdHouseholdMembers_RowCommand">
                    <EmptyDataTemplate>No results found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Last Name</HeaderTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLastName" runat="server" Text='<%# Item.LastName%>'
                                    CommandName="Select"
                                    CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>First Name</HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFirstName" runat="server" Text='<%# Item.FirstName%>'
                                    CommandName="Select"
                                    CommandArgument='<%# Item.PeopleID%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>Gender</HeaderTemplate>
                            <HeaderStyle Width="14px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGender" runat="server" Text='<%# Item.Gender %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="20px" ShowHeader="true">
                            <HeaderTemplate>DOB</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDOB" runat="server" Text='<%# Item.BirthDate==null? "": Item.BirthDate.Value.ToShortDateString()%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>
    <section class="col-md-7">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="panel-title">Personal Info</div>
            </div>
            <div class="panel-body">
                <div class="form-group  col-md-5">
                    <label for="txtLastName" class="control-label">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                        TabIndex="1"></asp:TextBox>
                </div>

                <div class="form-group  col-md-4">
                    <label for="txtFirstName" class="control-label">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 pull-right">

                    <a href="searchPeople1.aspx"><span class="btn btn-default pull-right">Search</span></a>
                </div>
                <div class="form-group  col-md-4">
                    <label for="mskBirthDate" class="control-label">Birth Date</label>
                    <asp:TextBox ID="mskBirthDate" runat="server" TextMode="Date" placeholder="MM/DD/YYYY" CssClass="form-control" ClientIDMode="Static" TabIndex="3">    </asp:TextBox>
                </div>
                <div class="form-group  col-md-4">
                    <label for="chkBC" class="control-label">Birth Certificate?</label>
                    <asp:CheckBox ID="chkBC" runat="server" TabIndex="4" TextAlign="Left" />
                </div>
                <div class="form-group col-md-4">
                    <label for="raGender" class="control-label">Gender</label>
                    <asp:RadioButtonList ID="radGender" runat="server" CssClass="radio" RepeatColumns="1"
                        TextAlign="Left" TabIndex="5">
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group  col-md-4">
                    <label for="txtCellPhone" class="control-label">Cell Phone</label>
                    <asp:TextBox ID="txtCellPhone" runat="server" TextMode="Phone" Placeholder="954-999-9999" CssClass="form-control" TabIndex="6">
                    </asp:TextBox>
                    <asp:MaskedEditExtender ID="maskCellPhone" runat="server" TargetControlID="txtCellPhone" Mask="999-999-9999" />
                </div>
                <div class="form-group  col-md-4">
                    <label for="txtWorkPhone" class="control-label">Work Phone</label>
                    <asp:TextBox ID="txtWorkPhone" runat="server" TextMode="Phone" Placeholder="954-999-9999" CssClass="form-control" TabIndex="7">
                    </asp:TextBox>
                    <asp:MaskedEditExtender ID="maskWorkPhone" runat="server" TargetControlID="txtWorkPhone" Mask="999-999-9999" />
                </div>

                <div class="form-group col-md-3">
                    <label for="cmbGrade" class="control-label">Grade</label>
                    <asp:DropDownList ID="cmbGrade" runat="server" CssClass="form-control dropdown" TabIndex="8">
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
                        <asp:ListItem Value="99" Selected="True">Adult</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-5">
                    <label for="txtSChool" class="control-label">School</label>
                    <asp:TextBox ID="txtSchool" runat="server" TabIndex="9" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <div class="row">

                        <div class="form-group border col-md-5">
                            <asp:CheckBoxList ID="chkMoney" runat="server" RepeatColumns="1" class="checkbox" TabIndex="10">
                                <asp:ListItem>Current BM</asp:ListItem>
                                <asp:ListItem>Plays Up</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="form-group border col-md-5 pull-right">
                            <asp:CheckBoxList ID="chkParentPlayer" runat="server" CssClass="checkbox" ClientIDMode="Static" RepeatColumns="1"
                                TabIndex="11">
                                <asp:ListItem>Parent</asp:ListItem>
                                <asp:ListItem>Coach</asp:ListItem>
                                <asp:ListItem>Player</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="form-group  col-md-12 border">
                            <label for="chkVolunteer">VOLUNTEER</label>
                            <asp:CheckBoxList ID="chkVolunteer" runat="server" class="checkbox volunteer checkBoxList" TabIndex="12"
                                RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table">
                                <asp:ListItem>Board Officer</asp:ListItem>
                                <asp:ListItem>Board Member</asp:ListItem>
                                <asp:ListItem>Athletic Director</asp:ListItem>
                                <asp:ListItem>Sponsor</asp:ListItem>
                                <asp:ListItem>Sign Ups</asp:ListItem>
                                <asp:ListItem>Try Outs</asp:ListItem>
                                <asp:ListItem>Tee Shirts</asp:ListItem>
                                <asp:ListItem>Printing Co.</asp:ListItem>
                                <asp:ListItem>Equipment</asp:ListItem>
                                <asp:ListItem>Electrician</asp:ListItem>
                                <asp:ListItem>Asst Coach</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="form-group col-md-12">
                            <asp:LinkButton ID="btnComments" runat="server" TabIndex="13" CssClass="btn-link">Comments</asp:LinkButton>
                            <asp:TextBox ID="txtComments" runat="server" TabIndex="14"  TextMode="MultiLine" CssClass="text-left form-control"></asp:TextBox>
                        </div>
                    </div>



                </div>
            </div>
        </div>
        <div class="panel-footer btn-group ">
            <asp:Button ID="btnSave" runat="server"
                TabIndex="14"
                Text="Save"
                OnClick="btnSave_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnRegister" runat="server" TabIndex="15" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnAdd" runat="server"
                TabIndex="16"
                Text="Add Household Member"
                OnClick="btnAdd_Click"
                CssClass="btn btn-primary" />

            <asp:Button ID="btnDelete" runat="server" TabIndex="17" Text="Delete" OnClick="btnDelete_OnClick" CssClass="btn btn-primary" />
            <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                Visible="False" Width="87px"></asp:Label>
        </div>

        </div>
    </section>

    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="748px"></asp:Label>

    <script src="Scripts/CSBCUI.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function() {
            alert("Testing");
        });
        $(function() { $('#<%=mskBirthDate.ClientID%>').on("blur"), HandleBirthDate;
        });
        function initPage() {
            alert("InitPage");
            SetElements();
        }

        function SetElements() {
            $('#<%=mskBirthDate.ClientID%>').on("click"), HandleBirthDate);;
        }
        function HandleBirthDate() {    
            var txtDate = $(this).valueOf();
            alert(txtDate);
        };
        
    </script>
</asp:Content>

