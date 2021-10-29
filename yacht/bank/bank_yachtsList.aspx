<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="True" CodeBehind="bank_yachtsList.aspx.cs" Inherits="yacht.bank.WebForm11" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/page.css" rel="stylesheet" />
    <script type="text/javascript">function openModal() { $('#model_del_gov_lic').modal('show'); }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">遊艇列表</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#!"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">遊艇列表</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  test modal--%>
    <div id="model_del_gov_lic" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-white">
                    <h5 class="modal-title  text-danger">刪除單筆資料</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="modelid" Visible="False" />
                    <p>是否確認刪除這筆資料</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-dismiss="modal">否</button>
                    <asp:Button ID="Button1" CssClass="btn btn-outline-danger" runat="server" Text="是" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    </div>
    <%--test modal  --%>

    <%-- Modal content --%>

    <div class="container">
        <%-- 代理商表格單s --%>
        <div class="card mt-3">
            <div class="card-header h4">
                <div class="d-flex justify-content-between ml-2">
                    遊艇列表
                        <a href="bank_yachts.aspx" class="btn btn-primary mr-4">新增遊艇</a>
                </div>
            </div>
            <div class="card-body">
                <div class="d-flex justify-content-between mb-4">
                    <div class="form-inline">
                        <%-- search日期 s--%>
                        <div class="form-group mr-2">
                            <label for="statrdate" class="text-info">新增日期：</label>
                            <asp:TextBox ID="statrdate" type="date" runat="server" CssClass="form-control form-control-sm">
                            </asp:TextBox>
                        </div>
                        <div class="form-group mr-2">
                            <label for="enddate" class="mr-2 text-info">到</label>
                            <asp:TextBox ID="enddate" type="date" runat="server" CssClass="form-control form-control-sm">
                            </asp:TextBox>
                        </div>
                        <div class="form-group mr-5">
                            <label for="newtitle" class="text-warning">遊艇標題：</label>
                            <asp:TextBox ID="yachtitle" type="text" runat="server" CssClass="form-control form-control-sm pr-5 mr-2" placeholder="請輸入新聞標題">
                            </asp:TextBox>
                        </div>
                        <button id="findate" type="button" class="btn  btn-icon btn-outline-info ml-2" style="width: 35px; height: 35px;" runat="server" onserverclick="Button2_Click"><i class="fas fa-search"></i></button>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="text-danger ml-3" OnClick="LinkButton1_Click"><i class="fas fa-calendar-times fa-2x" ></i></asp:LinkButton>
                    </div>
                </div>
                <!-- 表格s -->
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <HeaderTemplate>
                        <table class="table">
                            <thead class="text-center">
                                <tr>
                                    <th>編號</th>
                                    <th>最新</th>
                                    <th class="text-left">新增日期</th>
                                    <th class="text-left">遊艇</th>
                                    <th>編輯</th>
                                    <th>刪除</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row" class="text-center pt-4" style="width: 6%;"><%# Container.ItemIndex+1 %></th>
                            <td class="text-center pt-4" style="width: 13%;"><span runat="server" class="badge badge-danger" visible='<%#Eval("newcheck")%>'>New</span></td>
                            <td class="pt-4" style="width: 18%;"><%#Eval("insertTime","{0:yyyy/MM/dd}")%></td>
                            <td class="pt-4"><%# Eval("title") %></td>
                            <td class="text-center pt-4" style="width: 15%;">
                                <asp:Button ID="lbtEdit" CssClass="btn bg-info btn-sm rounded-top text-white" CommandName="Edit" CommandArgument='<%#Eval("id") %>' runat="server" Text="編輯" />
                            </td>
                            <td class="text-center pt-4" style="width: 15%;">
                                <asp:Button ID="lbtDelete" runat="server" CssClass="btn btn-danger btn-sm rounded-top" CommandName="Delete" CommandArgument='<%#Eval("id") %>' Text="刪除" />
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