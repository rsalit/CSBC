<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="RegPayments.aspx.cs" Inherits="CSBC.Admin.Web.RegPayments" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="Scripts/CSBCUI.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Player Info</div>
                </div>
                <div class="panel-body">
                    <div class="col-md-8">
                        <asp:LinkButton ID="lnkName" runat="server" TabIndex="20" CssClass="btn-link" OnClick="lnkName_OnClick"></asp:LinkButton>

                        <asp:Label ID="lblAddress" runat="server" CssClass="text-info" Width="280px">Address</asp:Label>
                        <asp:Label ID="lblCSZ" runat="server" CssClass="text-info" Width="280px">City, State Zip</asp:Label>
                        <asp:Label ID="lblPhone" runat="server" CssClass="text-info" Width="280px">Phone</asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblBM" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                            Visible="False" Width="123px">*Board Member*</asp:Label>
                    </div>
                </div>
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="panel-title">Payment Info</div>
                </div>
                <div class="panel-body">
                    <div class="form-group border col-md-4">
                        <label for="radPayment">Payment Type</label>
                        <asp:RadioButtonList ID="radPayment" runat="server"
                            RepeatColumns="1"
                            TabIndex="8" TextAlign="Right" CssClass="radio radio-inline" 
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="true">Check</asp:ListItem>
                            <asp:ListItem>C.Card</asp:ListItem>
                            <asp:ListItem>Online</asp:ListItem>
                            <asp:ListItem>Cash</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="mskAmount">Amount:</label>
                        <asp:TextBox ID="mskAmount" runat="server" TextMode="Number" CssClass="form-control col-md-4"
                            TabIndex="5">   </asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label for="mskBalance">Balance</label>
                        <asp:TextBox ID="mskBalance" runat="server" TextMode="Number" CssClass="form-control col-md-3"
                            TabIndex="6">   </asp:TextBox>
                    </div>

                    <div class="form-group col-md-3">
                        <label for="txtCheck">Check#</label>
                        <asp:TextBox ID="txtCheck" runat="server" TabIndex="7" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-5">
                        <label for="mskPayDate">Date</label>
                        <asp:TextBox ID="mskPayDate" runat="server" TextMode="Date" CssClass="form-control"
                            TabIndex="8">
                        </asp:TextBox>
                       
                    </div>
                    <div class="form-group col-md-10">
                        <label for="txtMemo">Memo</label>
                        <asp:TextBox ID="txtMemo" runat="server"
                            TabIndex="9"
                            CssClass="form-control"></asp:TextBox>
                    </div>


                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Draft Info </div>
                </div>
                
                <div class="panel-body">
                    <div class="form-group col-md-4">
                        <label for="txtDraftID">Draft ID</label>
                        <asp:TextBox ID="txtDraftID" runat="server" TabIndex="1" CssClass="form-control" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <asp:LinkButton ID="btnTeam" runat="server" OnClick="btnTeam_Click" TabIndex="20" Width="300px"></asp:LinkButton>
                    </div>
                    <div class="form-group col-md-10">
                        <label for="lblDraftNote">Draft Note</label>
                        <asp:TextBox ID="txtDraftNotes" runat="server" CssClass="form-control"
                            TabIndex="2"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="cboRating">Rating</label>
                        <asp:DropDownList ID="cboRating" runat="server" CssClass="form-control dropdown" TabIndex="3">
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
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="PlaysDownUp">ChangeDivision </label>
                        <asp:DropDownList ID="PlaysDownUp" runat="server" CssClass="form-control dropdown" TabIndex="3">
                            <asp:ListItem Value="0">N/A</asp:ListItem>
                            <asp:ListItem Value="1">Plays Up</asp:ListItem>
                            <asp:ListItem Value="2">Plays Down</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group checkbox border col-md-10">
                        <label for="chkWaived">Fee Waived:</label>
                        <asp:CheckBoxList ID="chkWaived" runat="server"
                            CssClass="checkbox"
                            RepeatColumns="2"
                            TabIndex="4">
                            <asp:ListItem>Scholarship</asp:ListItem>
                            <asp:ListItem>Rollover</asp:ListItem>
                            <asp:ListItem>Family Discount</asp:ListItem>
                            <asp:ListItem>Athletic Director</asp:ListItem>
                            <asp:ListItem>Partial Refund</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <asp:TextBox ID="txtPlaysUp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Height="15px" MaxLength="3" TabIndex="1" Visible="False" Width="30px"></asp:TextBox>
                </div>
            </div>
            <asp:Button ID="btnSave" runat="server" TabIndex="11" Text="Save" 
                CssClass="btn btn-primary"  
                OnClientClick="return validate();" 
                OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" TabIndex="12" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_OnClick" />
            <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                Visible="False" Width="87px"></asp:Label>
        </div>

        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Drafted Players
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="cboDivisions">Divisions</label>
                        <asp:DropDownList ID="cboDivisions" runat="server"
                            CssClass="form-control dropdown"
                            TabIndex="13"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="cboDivisions_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <%--<div class="form-group">
                        <asp:Button ID="btnOR" runat="server" TabIndex="14" Text="Add DraftID" CssClass="btn btn-primary" />
                    </div>--%>
                    <div style="overflow-y: scroll; max-height: 600px">
                        <asp:GridView runat="server"
                            ID="grdPlayers"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-condensed"
                            RowStyle-CssClass="td"
                            HeaderStyle-CssClass="th"
                            ItemType="CSBC.Core.Models.SeasonPlayer"
                            OnRowCommand="grdPlayers_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="DraftID" runat="server" HeaderText="Draft ID" />
                                <asp:TemplateField HeaderText="Name">
                                    <HeaderTemplate>Name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPlayerName" runat="server"
                                            Text='<%# Item.Name %>'
                                            CommandName="Select"
                                            CommandArgument='<%# Item.PlayerID  %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BirthDate" runat="server" DataFormatString="{0:d}" HeaderText="DOB" />
                            </Columns>

                            <HeaderStyle CssClass="th"></HeaderStyle>

                            <RowStyle CssClass="td"></RowStyle>

                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="755px"></asp:Label>

    </div>
    <script type="text/javascript">
        $(document).ready(function () {
         

        });

    </script>
       <script type="text/javascript">
        function validate() {

            var isValid = true;

            var txt = document.getElementById('mskPayDate');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Payment date is required", "Error");
            }
            else {
                var date = Date(txt.value)
                if (date == null) {
                    isValid = false;
                    toastr.error("Date not in date format", "Error");
                }
            }
            txt = document.getElementById('mskAmount');
            if (txt.value == null) {
                isValid = false;
                toastr.error("Amount must be entered", "Error");
            }

            txt = document.getElementById('mskBalance');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Balance required", "Error");
            }

            txt = document.getElementById('txtDraftID');
            if (txt.value == "") {
                isValid = false;
                toastr.error("Draft ID is required", "Error");
            }

            return isValid;
        }

    </script>

</asp:Content>



