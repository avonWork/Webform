<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_email.aspx.cs" Inherits="yacht.bank.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //var exampleModal = document.getElementById('exampleModal')
        //exampleModal.addEventListener('show.bs.modal', function (event) {
        //    var button = event.relatedTarget
        //})
        function openModal() { $('#exampleModal').modal('show'); }
        // 編輯的 Modal 事件
        //$('#exampleModal').on('show', function (event) {
        //    var button = $(event.relatedTarget) // 選則當初觸發事件的按鈕
        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  --%>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">New message</h5>
                    <button type="button" class="btn-close" data-dismiss="modal">X</button>
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="modelid" Visible="False" />
                    <%-- 表格s --%>
                    <div class="form-group h5 mt-2 text-info">
                        contact：<asp:Literal ID="username" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group h5">
                        email：<asp:Literal ID="email" runat="server"></asp:Literal>
                    </div>
                    <%-- 表格e--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">取消</button>
                </div>
            </div>
        </div>
    </div>
    <%--  --%>
    <div class="container my-4">
        <div class="d-flex justify-content-center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand1" OnRowDataBound="GridView1_RowDataBound1">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ename" HeaderText="姓名" SortExpression="ename" />
                    <asp:BoundField DataField="eemail" HeaderText="Email" SortExpression="eemail" />
                    <asp:BoundField DataField="country" HeaderText="國家" SortExpression="country" />
                    <asp:BoundField DataField="yachtid" HeaderText="遊艇id" SortExpression="yachtid" />
                    <asp:TemplateField HeaderText="查看" ShowHeader="False">
                        <ItemTemplate>
                            <%-- data-toggle="modal" data-target="#exampleModal" --%>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Look" CommandArgument='<%#Eval("id") %>' CssClass="text-warning fa-2x"><i class="fas fa-address-card"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Delete" CommandArgument='<%#Eval("id") %>' CssClass="text-danger fa-2x" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br />
        </div>
    </div>
</asp:Content>