<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_country.aspx.cs" Inherits="yacht.bank.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">新增國家</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#!"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">新增國家</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <%-- Add s--%>
        <div class="form-inline mt-2">
            <label for="addCountry" class="col-sm-2 col-form-label col-form-label-lg mb-2">新增國家：</label>
            <div class="input-group  col-sm-6 px-0">
                <asp:TextBox ID="addCountry" class="form-control  col-form-control-lg" placeholder="請輸入要增加的國家" runat="server"></asp:TextBox>
                <div class="input-group-append">
                    <button id="add" type="button" class="btn  btn-danger py-0" runat="server" onserverclick="Button1_Click"><i class="fas fa-plus fa-2x"></i></button>
                </div>
            </div>
        </div>
        <%-- 表格 --%>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
                <div class="row ml-3 ">
                    <div class="col-md-8">
                        <div class="card mt-3">
                            <div class="card-header">
                                <div class="h4">國家表單</div>
                            </div>
                            <div class="card-body table-border-style">
                                <div class="table-responsive">
                                    <table class="table table-inverse">
                                        <thead>
                                            <tr>
                                                <th>編號</th>
                                                <th>國家</th>
                                                <th class="text-center">操作</th>
                                            </tr>
                                        </thead>
                                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Panel ID="plItem" runat="server">
                    <tr>
                        <th scope="row"><%# Container.ItemIndex+1 %></th>
                        <td><%# Eval("country") %></td>
                        <td class="text-center">
                            <div class="btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                                <asp:Button ID="lbtEdit" CssClass="btn btn-outline-secondary" CommandName="Edit" CommandArgument='<%#Eval("id") %>' runat="server" Text="編輯" />
                                <asp:Button ID="lbtDelete" runat="server" CssClass="btn btn-outline-secondary" CommandName="Delete" CommandArgument='<%#Eval("id") %>' OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;" Text="刪除" />
                            </div>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="plEdit" runat="server">
                    <tr>
                        <th scope="row"><%# Container.ItemIndex+1 %></th>
                        <td>
                            <asp:TextBox ID="updatecountry" Text='<%# Eval("country") %>' runat="server" CssClass="form-control form-control-sm"></asp:TextBox></td>
                        <td class="text-center">
                            <div class="btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                                <asp:Button ID="lbtUpdate" CssClass="btn btn-outline-secondary" CommandName="Update" CommandArgument='<%#Eval("id") %>' runat="server" Text="更新" />
                                <asp:Button ID="lbtCancel" runat="server" CssClass="btn btn-outline-secondary" CommandName="Cancel" CommandArgument='<%#Eval("id") %>' Text="取消" />
                            </div>
                        </td>
                    </tr>
                </asp:Panel>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
         </table>
                    </div>
                 </div>
                 </div>
                 </div>
                 </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>