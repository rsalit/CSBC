<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Accounting1.aspx.cs" Inherits="CSBC.Admin.Web.Accounting1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sponsor Payment info
                    </div>
                </div>
                <div class="panel-body">
                        <div class="form-group col-md-6">
                            <label for="cmbSponsorName" class="control-label">Sponsor Name</label>
                            <asp:DropDownList runat="server" ID="cmbSponsorNames" 
                                AutoPostBack="true"
                                OnSelectedIndexChanged="cmbSponsorNames_OnSelectedIndexChanged"
                                CssClass="form-control dropdown" />
                        </div>
                   
                   
                        <div class="form-group col-md-4">
                            <label for="txtPaymentDate" class="control-label">Payment date</label>
                            <asp:TextBox runat="server" ID="txtPaymentDate" TextMode="Date" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label for="txtPaymentAmount" class="control-label">Amount</label>
                            <asp:TextBox runat="server" ID="txtPaymentAmount" TextMode="Number" CssClass="form-control "></asp:TextBox>
                        </div>
                        <div class="col-md-4 radio">

                            <div class="form-group border">
                                <label for="radPayment" class="control-label">Payment Type</label>
                                <asp:RadioButtonList ID="radPayment" runat="server"
                                    RepeatColumns="1"
                                    TabIndex="8" TextAlign="Left" CssClass="radio" RepeatLayout="Flow">
                                    <asp:ListItem>Check</asp:ListItem>
                                    <asp:ListItem>Credit Card</asp:ListItem>
                                    <asp:ListItem>Online</asp:ListItem>
                                    <asp:ListItem>Cash</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <label for="txtCheckNo" class="control-label">Check No</label>
                            <asp:TextBox runat="server" ID="txtCheckNo" TextMode="Number" CssClass="form-control "></asp:TextBox>
                        </div>
                
                </div>
                <div class="panel-footer">
                    <div class="btn-group">
                        <asp:Button ID="btnAddPayment" runat="server" 
                            Text="Add Payment" 
                            CssClass="btn btn-primary" 
                            OnClick="btnAddPayment_Click" />
                        <asp:Button ID="btnSave" runat="server" 
                            Text="Save" 
                            CssClass="btn btn-primary" 
                            OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Sponsor Payment info
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 240px">
                    <asp:GridView ID="grd" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        DataKeyNames="SponsorProfileID"
                        ItemType="CSBC.Core.Models.SponsorPayment"
                        OnRowCommand="grd_OnRowCommand"
                        ShowHeader="true">
                        <EmptyDataTemplate>
                            No Data Found.  

                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sponsor's Name" ShowHeader="true">
                                <HeaderTemplate>Payment Date</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSponsorNo" runat="server"
                                        Text='<%# Item.TransactionDate.Value.ToShortDateString() %>'
                                        CommandName="SelectTransaction"
                                        CommandArgument='<%# Item.PaymentID %>'></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="10px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" ShowHeader="true">
                                <HeaderTemplate>Amount</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSponsorAmount" runat="server"
                                        Text='<%# Item.Amount %>'
                                        CommandName="SelectTransaction"
                                        CommandArgument='<%# Item.PaymentID %>'></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="10px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
</asp:Content>
