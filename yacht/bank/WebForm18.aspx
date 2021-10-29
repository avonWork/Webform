<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm18.aspx.cs" Inherits="yacht.bank.WebForm18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="repCustomers" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:CheckBox ID="Chkbulk" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCustomerId" Text='<%#Eval("id") %>' runat="server" />
                    &nbsp;&nbsp;
                </td>
                <td>
                    <asp:Label ID="lblCustomerName" Text='<%#Eval("fileorder") %>' runat="server" />
                    <asp:TextBox ID="no" runat="server" Width="120" Text='<%# Eval("fileorder") %>'
                        Visible="false" />
                    &nbsp;&nbsp; &nbsp;&nbsp;
                </td>
                <td>
                    <asp:Label ID="lblCountry" Text='<%#Eval("fileText") %>' runat="server" />
                    <asp:TextBox ID="name" runat="server" Width="120" Text='<%# Eval("fileText") %>'
                        Visible="false" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
<asp:LinkButton ID="btnEdit" Text="Edit" OnClick="EditMode" runat="server" />
<asp:LinkButton ID="btnUpdate" Text="Update" OnClick="OnUpdate" runat="server" />
</asp:Content>
