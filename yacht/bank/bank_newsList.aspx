<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_newsList.aspx.cs" Inherits="yacht.bank.WebForm2" %>
<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/page.css" rel="stylesheet" />
    <style>
        .bg-cover {
            background-size: cover;
            background-position: center center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <div class="card">
            <div class="card-header h4">
                 <div class="d-flex justify-content-between">
                        新聞列表
                        <a href="bank_new.aspx" class="btn btn-primary btn-sm">新增新聞</a>
                 </div>
            </div>
            <div class="card-body">
                <div class="d-flex justify-content-between">
                    <div class="form-inline">
                        <%-- search日期 s--%>
                        <div class="form-group mr-2">
                            <label for="statrdate"  class="text-info">新聞日期：</label>
                                 <asp:TextBox ID="statrdate" type="date" runat="server" CssClass="form-control form-control-sm">
                                 </asp:TextBox>
                        </div>
                        <div class="form-group mr-2">
                            <label for="enddate" class="mr-2 text-info">到</label>
                                 <asp:TextBox ID="enddate" type="date" runat="server" CssClass="form-control form-control-sm">
                                 </asp:TextBox>
                        </div>
                        <div class="form-group mr-5">
                            <label for="newtitle" class="text-warning">新聞標題：</label>
                            <asp:TextBox ID="newtitle" type="text" runat="server" CssClass="form-control form-control-sm pr-5 mr-2" placeholder="請輸入新聞標題">
                            </asp:TextBox>
                        </div>
                        <button  id="findate" type="button" class="btn  btn-icon btn-outline-info ml-2" style="width:35px;height:35px;" runat="server" onserverclick="Button1_Click"><i class="fas fa-search"></i></button>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="text-danger ml-3" OnClick="LinkButton1_Click"><i class="fas fa-calendar-times fa-2x" ></i></asp:LinkButton>
                    </div>
                </div>
                <!-- 表格s -->
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-bordered mt-4">
                                <thead class="thead-dark text-center"">
                                    <tr>
                                        <th style="width:5%;">編號</th>
                                        <th>新聞圖片</th>
                                        <th style="width:15%;">新聞日期</th>
                                        <th>新聞標題</th>
                                        <th class="text-center" style="width:15%;">操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row" class="text-center"><%# Container.ItemIndex+1 %></th>
                                <td  class="p-0 text-center" style=" width: 141px;">
                                    <img class="" style=" width: 141px;height: 81px;" src="../ckfinder/userfiles/images/<%# Eval("img")%>" />
                                </td>
                                <td class="text-center"><%#Eval("date","{0:yyyy/MM/dd}")%>
                                </td>
                                  <td><span runat=server class="text-danger mr-2" visible=<%#Eval("sticky")%>><i class="fas fa-thumbtack"></i></span><%# Eval("title") %>
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
                <!-- 表格e -->
                <%-- 分頁控件s --%>
               <div class="container">
                <div class="d-flex justify-content-center">
                 <uc1:WebUserControl1 runat="server" ID="WebUserControl1" />
                    </div>
                   </div>
                <%--分頁控件e --%>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
                    <div class="page-block">
                        <div class="row align-items-center">
                            <div class="col-md-12">
                                <div class="page-header-title">
                                    <h5 class="m-b-10">新聞列表</h5>
                                </div>
                                <ul class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="bank_newsList.aspx"><i class="feather icon-home"></i></a></li>
                                    <li class="breadcrumb-item"><a href="#!">新聞列表</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>