<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_agentList.aspx.cs" Inherits="yacht.bank.WebForm7" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/page.css" rel="stylesheet" />
    <script type="text/javascript">
        function openModal() { $('#model_del_gov_lic').modal('show'); }
    </script>
    <style>
        .modal {
            text-align: center;
        }

        @media screen and (min-width: 768px) {
            .modal:before {
                display: inline-block;
                vertical-align: middle;
                content: " ";
                height: 100%;
            }
        }

        .modal-dialog {
            display: inline-block;
            text-align: left;
            vertical-align: middle;
        }

        .img-cover {
            background-position: center center;
            background-repeat: no-repeat;
            background-size: cover;
            overflow: hidden;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%-- Modal content --%>
    <div class="container">
        <div id="model_del_gov_lic" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 400px; height: 350px;">
                    <div class="modal-header table-warning">
                        <h5 class="modal-title">代理商資訊</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="modelid" Visible="False" />
                        <%-- 表格s --%>
                        <div class="form-group h5 mt-2 text-info">
                            contact：<asp:Literal ID="username" runat="server"></asp:Literal>
                        </div>
                        <div class="form-group h5">
                            tel：<asp:Literal ID="tel" runat="server"></asp:Literal>
                        </div>
                        <div class="form-group h5">
                            email：<asp:Literal ID="email" runat="server"></asp:Literal>
                        </div>
                        <div class="form-group ">
                            address：<asp:Literal ID="address" runat="server"></asp:Literal>
                        </div>
                        <%-- 表格e--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">取消</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Modal content --%>

    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">代理商列表</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#!"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="bank_agentList.aspx">代理商列表</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <%-- 下拉選單s--%>
        <div class="form-inline mt-1">
            <label for="ddlcountry" class="col-sm-1 col-form-label">國家：</label>
            <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control form-control-sm col-sm-2" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <%--        </div>
        <div class="form-inline">--%>
            <label for="ddlcountry" class="col-sm-1 col-form-label">地區：</label>
            <asp:DropDownList ID="ddlarea" runat="server" CssClass="form-control  form-control-sm col-sm-3"></asp:DropDownList>
            <%-- search新聞s --%>
            <label for="newtitle" class="text-primary h5 ml-3">代理商：</label>
            <asp:TextBox ID="searchAgent" type="text" runat="server" CssClass="form-control form-control-sm col-sm-3 mr-3" placeholder="請輸入搜尋代理商的名稱">
            </asp:TextBox>
            <button id="findagent" type="button" class="btn  btn-icon btn-outline-primary" style="width: 35px; height: 35px;" runat="server" onserverclick="Button2_Click"><i class="fas fa-search"></i></button>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="text-danger ml-3" OnClick="LinkButton1_Click"><i class="fas fa-calendar-times fa-2x" ></i></asp:LinkButton>
            <%-- search新聞 e --%>
        </div>
        <%-- 下拉選單e--%>
        <%-- 代理商表格單s --%>
        <div class="card mt-3">
            <div class="card-header h4">
                <div class="d-flex justify-content-between">
                    代理商列表
                        <a href="bank_agent.aspx" class="btn btn-primary btn-sm">新增代理商</a>
                </div>
            </div>
            <div class="card-body">
                <div class="col-md-10 pl-0">
                    <div class="form-inline">
                        <%-- search新聞s --%>
                        <%--    <label for="newtitle" class="text-primary h5">代理商：</label>
                        <asp:TextBox ID="searchAgent" type="text" runat="server" CssClass="col-md-7 form-control form-control-sm pr-5 mr-3" placeholder="請輸入搜尋代理商的名稱">
                        </asp:TextBox>
                        <button id="findagent" type="button" class="btn  btn-icon btn-outline-primary" style="width: 35px; height: 35px;" runat="server" onserverclick="Button2_Click"><i class="fas fa-search"></i></button>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="text-danger ml-3" OnClick="LinkButton1_Click"><i class="fas fa-calendar-times fa-2x" ></i></asp:LinkButton>--%>
                        <%-- search新聞 e --%>
                    </div>
                </div>
                <!-- 表格s -->
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead class="text-center">
                                <tr>
                                    <th style="width: 5%;">編號</th>
                                    <th style="width: 5%;">圖片</th>
                                    <th style="width: 12%;">國家</th>
                                    <th style="width: 20%;">地區</th>
                                    <th style="width: 20%;">代理商</th>
                                    <th style="width: 15%;">聯絡人</th>
                                    <th class="text-center">編輯</th>
                                    <th class="text-center">查看</th>
                                    <th class="text-center">刪除</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row" class="text-center pt-4"><%# Container.ItemIndex+1 %></th>
                            <td class="p-0 text-center">
                                <img class="img-cover" style="width: 121px; height: 101px;" src="../ckfinder/userfiles/images/<%# Eval("img")%>" />
                            </td>
                            <td class="pt-4 text-center"><%# Eval("country") %></td>
                            <td class="pt-4"><%# Eval("area") %></td>
                            <td class="pt-4"><%# Eval("agent") %></td>
                            <td class="pt-4"><%# Eval("contact") %></td>
                            <td class="text-center pt-4">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("id") %>' CssClass="text-info fa-2x"><i class="fas fa-pen-square"></i></asp:LinkButton>
                            </td>
                            <td class="text-center pt-4">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Look" CommandArgument='<%#Eval("id") %>' CssClass="text-warning fa-2x"><i class="fas fa-address-card"></i></asp:LinkButton>
                            </td>
                            <td class="text-center pt-4">
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Delete" CommandArgument='<%#Eval("id") %>' CssClass="text-danger fa-2x" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;"><i class="fas fa-trash-alt"></i></asp:LinkButton>
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
        <%--  --%>
    </div>
</asp:Content>