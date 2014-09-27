<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="Coaches1.aspx.cs" Inherits="CSBC.Admin.Web.Coaches1" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">Coach Detail Info</div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="cmbCoaches">Coach</label>
                        <asp:DropDownList ID="cmbCoaches" runat="server"
                            AutoPostBack="True"
                            CssClass="form-control dropdown"
                            TabIndex="1">
                        </asp:DropDownList>
                    </div>
                    <asp:Panel ID="pnlCoach" runat="server" BorderStyle="None" Visible="False"
                        BorderWidth="1px">
                        <address>
                            <asp:LinkButton ID="lnkName" runat="server" CssClass="strong" TabIndex="20"
                                Width="317px" ValidateRequestMode="Inherit" OnClientClick="lnkName_Click">Name</asp:LinkButton>
                            <br />
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            <br />
                            <asp:Label ID="lblCSZ" runat="server" Text="City, State, Zip"></asp:Label>
                            <br />
                            <asp:Label ID="lblPhone" runat="server" CssClass="text-info" Text="Phone"></asp:Label>
                        </address>
                    </asp:Panel>
                    <div class="form-group col-md-6">
                        <label for="cmbSizes">Shirt:</label>
                        <asp:DropDownList ID="cmbSizes" runat="server" CssClass="form-control dropdown"
                            TabIndex="1">
                            <asp:ListItem Selected="True">N/A</asp:ListItem>
                            <asp:ListItem>SMALL</asp:ListItem>
                            <asp:ListItem>MEDIUM</asp:ListItem>
                            <asp:ListItem>LARGE</asp:ListItem>
                            <asp:ListItem>X-LARGE</asp:ListItem>
                            <asp:ListItem>XX-LARGE</asp:ListItem>
                            <asp:ListItem>3X-LARGE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtCoachPhone">Coach Phone:</label>

                        <asp:TextBox runat="server" ID="txtCoachPhone"
                            TextMode="Phone"
                            TabIndex="2"
                            Placeholder="954-###-####"
                            CssClass="form-control text-primary">
                        </asp:TextBox>
  
                    </div>
                    <asp:HiddenField ID="lblCoachId" runat="server" />
                </div>
                <div class="panel-footer">
                    <div class="btn-group">
                        <asp:Button ID="btnNew" runat="server" TabIndex="3" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" />
                        <asp:Button ID="btnSave" runat="server" TabIndex="4" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" TabIndex="5" Text="Delete" CssClass="btn btn-primary" OnClick="btnDelete_Click1" />
                    </div>
                    <asp:Label ID="lblDelete" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
                        Height="51px" Visible="False" Width="88px"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Current Season Coaches
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 300px">
                    <asp:GridView ID="grd" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        DataKeyNames="CoachID"
                        ItemType="CSBC.Core.Models.vw_Coaches"
                        OnRowCommand="grdCoaches_RowCommand"
                        ShowHeader="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Coach Name">
                                <HeaderTemplate>
                                    Coach Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCoach"
                                        runat="server"
                                        Text='<%# Item.Name %>'
                                        CommandName="Select"
                                        CommandArgument='<%# Item.CoachID  %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CoachPhone" HeaderText="Phone" ShowHeader="true"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                    </rsweb:ReportViewer>
                </div>

                <div class="panel-footer">
                    <asp:LinkButton ID="lnkPrint" runat="server" TabIndex="20" CssClass="visible-print">Print Coaches</asp:LinkButton>
                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">Coaches' Kids</div>
                </div>
                <div style="overflow-y: scroll; height: 240px">
                    <asp:GridView ID="grdKids" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-condensed"
                        RowStyle-CssClass="td"             
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.SeasonPlayer">
                        <Columns>
                            <asp:BoundField DataField="Name" ShowHeader="true" HeaderText="Player Name"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="panel-title">
                        Current Season Players
                    </div>
                </div>
                <div style="overflow-y: scroll; height: 240px">
                    <asp:GridView ID="grdPlayers" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped table-bordered table-condensed"
                        RowStyle-CssClass="td"
                        HeaderStyle-CssClass="th"
                        ItemType="CSBC.Core.Models.SeasonPlayer">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Player Name" ShowHeader="true"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:LinkButton ID="lnkComments" runat="server" Height="20px" TabIndex="6" Width="183px">Comments:</asp:LinkButton>
            <asp:TextBox ID="txtComments" runat="server" Height="67px" TextMode="MultiLine" Width="522px"></asp:TextBox>
        </div>

    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"
        Width="746px"></asp:Label>
</asp:Content>
