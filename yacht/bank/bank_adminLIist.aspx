<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_adminLIist.aspx.cs" Inherits="yacht.bank.WebForm26" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/page.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">管理者列表</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_adminLIist.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">管理者列表</a></li>
                    </ul>
                </div>
                `
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                <table class="table table-responsive table-hover ml-5">
                    <thead>
                        <tr>
                            <th>編號</th>
                            <th>頭像圖片</th>
                            <th>管理者帳號</th>
                            <th>管理者名稱</th>
                            <th>管理者權限</th>
                            <th class="text-center">操作</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <th scope="row"><%# Container.ItemIndex+1 %></th>
                    <td class="text-primary">
                        <img style="width: 50px; height: 50px;" src="../ckfinder/userfiles/images/<%# Eval("imgName") %>" />
                    </td>
                    <td><%# Eval("loginId") %></td>
                    <td><%# Eval("adminName") %></td>
                    <td>
                        <%-- 第一種解法 --%>
                        <%--                        <span id="span1" class="badge badge-danger" runat="server" visible='<%#(Convert.ToInt16(Eval("authority"))&1)>0%>'>代理商列表</span>
                        <span id="span2" class="badge badge-warning" runat="server" visible='<%#(Convert.ToInt16(Eval("authority"))&2)>0%>'>遊艇列表</span>
                        <span id="span3" class="badge badge-success" runat="server" visible='<%#(Convert.ToInt16(Eval("authority"))&4)>0%>'>新聞列表</span>
                        <span id="span4" class="badge badge-secondary" runat="server" visible='<%#(Convert.ToInt16(Eval("authority"))&8)>0%>'>管理者列表</span>--%>
                        <%--  第二種解法 --%>
                        <%--                        <%# (Convert.ToInt16(Eval("authority"))&1)>0?"<span class='badge badge-danger'>代理商列表</span>":""%>
                        <%# (Convert.ToInt16(Eval("authority"))&2)>0?"<span class='badge badge-warning'>遊艇列表</span>":""%>
                        <%# (Convert.ToInt16(Eval("authority"))&4)>0?"<span class='badge badge-success'>新聞列表</span>":""%>
                        <%# (Convert.ToInt16(Eval("authority"))&8)>0?"<span class='badge badge-secondary'>管理者列表</span>":""%>--%>
                        <asp:Literal ID="Literal1" Text='<%# Eval("authority")%>' runat="server"></asp:Literal>
                    </td>
                    <td class="text-center">
                        <div class="btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                            <asp:Button ID="Button1" CssClass="btn btn-outline-secondary" CommandName="comButton1" CommandArgument='<%#Eval("id") %>' runat="server" Text="編輯" />
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-secondary" CommandName="Delete" CommandArgument='<%#Eval("id") %>' OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;" Text="刪除" />
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
    </table>
            </FooterTemplate>
        </asp:Repeater>
        <uc1:WebUserControl1 runat="server" ID="WebUserControl1" />
    </div>
</asp:Content>