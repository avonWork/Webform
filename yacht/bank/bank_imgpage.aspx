<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="bank_imgpage.aspx.cs" Inherits="yacht.bank.WebForm16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ckeditor/ckeditor.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            //bootstrap 檔案上傳jq
            $(".custom-file-input").on("change", function () {
                var fileName = $(this).val().split("\\").pop();
                $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
            });
        });
    </script>
    <style>
        .table_bottom {
            margin-bottom: 0rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">新增遊艇</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_yachtsList.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">新增遊艇</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- 頁面傳值內容 --%>
    <asp:HiddenField ID="Overview" runat="server" />
    <asp:HiddenField ID="Layout" runat="server" />
    <asp:HiddenField ID="Specification" runat="server" />
    <asp:HiddenField ID="checkvalue" runat="server" />
    <div class="container">
        <%-- 標題s--%>
        <ul class="nav nav-pills nav-fill mb-2" id="pills-tab" role="tablist">
            <li class="nav-item h5">
                <a class="nav-link" id="tab1-tab" href="bank_yachts.aspx?title=<%=yachttitle  %>&check=<%=Context.Items["checkvalue"]  %>#tab1" role="tab" aria-controls="tab1" aria-selected="false">YachtName&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">1</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab2-tab" href="bank_yachts.aspx?title=<%=yachttitle  %>&check=<%=Context.Items["checkvalue"]  %>#tab2" role="tab" aria-controls="tab2" aria-selected="false">Overview&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">2</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab3-tab" href="bank_yachts.aspx?title=<%=yachttitle  %>&check=<%=Context.Items["checkvalue"]  %>#tab3" role="tab" aria-controls="tab3" aria-selected="false">Layout&deck plan&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">3</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab4-tab" href="bank_yachts.aspx?title=<%=yachttitle  %>&check=<%=Context.Items["checkvalue"]  %>#tab4" role="tab" aria-controls="tab4" aria-selected="false">Specification&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">4</span></a>
            </li>
            <li class="nav-item h5">
                <%--<a class="nav-link" id="tab5-tab" href="bank_filepage.aspx#tab5" role="tab" aria-controls="tab5" aria-selected="false">Fileupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">5</span></a>--%>
                <asp:LinkButton ID="tab5a" runat="server" class="nav-link" aria-controls="tab5" aria-selected="false" OnClick="tab5a_OnClick">Fileupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">5</span></asp:LinkButton>
            </li>
            <li class="nav-item h5">
                <a class="nav-link active" id="tab6a" href="#tab6" aria-controls="tab6" aria-selected="true">Imageupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">6</span></a>
            </li>
        </ul>
        <%-- 頁面 --%>
        <div class="tab-content" id="tab-next-prev-content">

            <%-- imgupload --%>
            <div class="tab-pane show active" id="tab6" role="tabpanel" aria-labelledby="tab6-tab">
                <div class="container">
                    <div class="row mt-3">
                        <div class="custom-file col-sm-4">
                            <asp:FileUpload ID="FileUpload1" class="custom-file-input" runat="server" />
                            <%--<input type="file" class="custom-file-input" id="inputGroupFile02">--%>
                            <label class="custom-file-label" for="inputGroupFile02">請選擇圖片</label>
                        </div>
                        <div class=" col-sm-6 pr-0">
                            <asp:TextBox ID="ImgTXT" runat="server" class="form-control" placeholder="請輸入當圖片沒顯示的圖片文字"></asp:TextBox>
                        </div>
                        <div class=" col-sm-2 mb-4">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning ml-4" Text="Add" OnClick="Button1_Click" />
                        </div>
                    </div>
                </div>
                <%-- 表格 --%>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <div class="card">
                            <table class="table  table-danger table_bottom">
                                <thead>
                                    <tr>
                                        <th style="width: 5%">可選</th>
                                        <th style="width: 5%">編號</th>
                                        <th style="width: 10%" class="text-center">圖片</th>
                                        <th style="width: 9%">圖片名</th>
                                        <th style="width: 35%">圖片文字</th>
                                        <th class="text-center">操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Panel ID="plItem" runat="server">
                            <tr>
                                <td class="text-center">
                                    <asp:CheckBox ID="Chkbulk" runat="server" CssClass="" />
                                </td>
                                <th scope="row" class="text-center">
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("id") %>' />
                                    <asp:Label ID="imgTextNO" Text='<%#Eval("imgorder") %>' runat="server" />
                                    <asp:TextBox ID="imgNO" runat="server" Width="30" Text='<%# Eval("imgorder") %>'
                                        Visible="false" />
                                <td class="p-0 text-center">
                                    <img style="width: 80px; height: 65px;" src="../ckfinder/userfiles/moreimg/<%# Eval("imgName") %>" /></td>
                                <td><%# Eval("imgName") %></td>
                                <td><%# Eval("imgText") %></td>
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
                                <td></td>
                                <td class="text-center"><%# Eval("imgorder") %></td>
                                <td class="p-0 text-center">
                                    <img style="width: 80px; height: 65px;" src="../ckfinder/userfiles/moreimg/<%# Eval("imgName") %>" /></td>
                                <td>
                                    <%# Eval("imgName") %></td>
                                <td>
                                    <asp:TextBox ID="ImgText" Text='<%# Eval("imgText") %>' runat="server" CssClass="form-control form-control-sm"></asp:TextBox></td>
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
                    </FooterTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="btnEdit" Text="Edit" OnClick="EditMode" runat="server" />
                &nbsp;&nbsp;
                <asp:LinkButton ID="btncancel" Text="Cancel" OnClick="cancelMode" runat="server" />
                &nbsp;&nbsp;
                <asp:LinkButton ID="btnUpdate" Text="Update" OnClick="OnUpdate" runat="server" />
                <asp:Button ID="Button2" runat="server" CssClass="btn  btn-dark  mt-4 float-lg-right" Text="Submit" OnClick="Button2_Click" />
            </div>
        </div>
    </div>
</asp:Content>