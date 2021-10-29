<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm17.aspx.cs" Inherits="yacht.bank.WebForm17" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" OnRowDataBound = "OnRowDataBound" DataKeyNames = "id">
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Name" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("fileorder") %>'></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("fileText") %>' Visible="false"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Country" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label ID = "lblCountry" runat="server" Text='<%# Eval("fileName") %>'></asp:Label>
                <asp:DropDownList ID="ddlCountries" runat="server" Visible = "false">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<%--<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick = "Update" Visible = "false"/>--%>
</asp:Content>
