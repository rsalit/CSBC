<%@ Page Title="" Language="C#" MasterPageFile="~/CSBCAdminMasterPage.master" AutoEventWireup="true" CodeBehind="EmailBlast.aspx.cs" Inherits="CSBC.Admin.Web.EmailBlast" %>

<%@ MasterType VirtualPath="~/CSBCAdminMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <section class="col-sm-8">
            <div class="form-group">
                <label for="lblFrom" class="control-label">From</label>

                <asp:Label ID="lblFrom" runat="server" CssClass="form-control"></asp:Label>
            </div>
            <div class="dropdown">
                <label for="cboTo" class="control-label">To</label>
                <asp:DropDownList ID="cboTo" runat="server" CssClass="dropdown" TabIndex="1" AutoPostBack="True">
                    <asp:ListItem Value="0">Board Members</asp:ListItem>
                    <asp:ListItem Value="1">Season Candidates Only</asp:ListItem>
                    <asp:ListItem Value="2">Season Players</asp:ListItem>
                    <asp:ListItem Value="3">Season Coaches/Sponsors</asp:ListItem>
                    <asp:ListItem Value="4">Members(All seasons)</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtSubject" class="control-label">Subject</label>

                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
            </div>


            <label id="lblUsernameCaption" runat="server" text="Content:" width="86px"></label>
            <div class="form-group">
                <label for="htmlMail" class="form-label">Message</label>
                <asp:TextBox ID="htmlMail" runat="server" TextMode="multiline" type="text" placeholder="type text here"></asp:TextBox>
            </div>
            <div class="btn-group">
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" TabIndex="4" />
            </div>

        </section>
        <div class="col-sm-4">
            <div class="form-group">
            <label for="lstEmails" class="label">Recipients</label>
            <asp:ListBox ID="lstEmails" runat="server" class="" SelectionMode="Multiple"></asp:ListBox>
        </div>
            </div>
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Width="600px"></asp:Label>

    </div>
</asp:Content>
