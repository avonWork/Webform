<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_addAdmin.aspx.cs" Inherits="yacht.bank.WebForm22" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- 放header 預覽fileupload照片 start --%>
    <style type="text/css">
        #picview, .img1 {
            width: 161px;
            height: 121px;
            position: relative;
            left: -1px;
            top: -1px;
            border: 1px solid #000;
        }

        #picview {
            border: 1px solid #000;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        function picview(file) {
            var picviewDiv = document.getElementById('picview');
            if (file.files && file.files[0]) {
                var reader = new FileReader();
                reader.onload = function (evt) {
                    picviewDiv.innerHTML = '<img class="img1" src="' + evt.target.result + '" />';
                }
                reader.readAsDataURL(file.files[0]);
            }
            else {
                picviewDiv.innerHTML = '<div class="img1" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>';
            }
        }
    </script>
    <%-- 預覽fileupload照片 end --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">新增管理者</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_adminLIist.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">新增管理者</a></li>
                    </ul>
                </div>
                `
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group  col-md-8">
                <label for="exampleInputEmail1">管理者帳號: </label>
                <asp:TextBox ID="UserId" runat="server" CssClass="form-control" placeholder="請輸入帳號" required="required"></asp:TextBox>
            </div>

            <div class="form-group  col-md-8">
                <label for="exampleInputEmail1">管理者姓名:</label>
                <asp:TextBox ID="Name" runat="server" CssClass="form-control" placeholder="請輸入姓名" required="required"></asp:TextBox>
            </div>

            <div class="form-group  col-md-8">
                <label for="exampleInputEmail1">管理者密碼: </label>
                <%--  required --%>
                <asp:TextBox ID="Pwd" runat="server" CssClass="form-control" placeholder="請輸入密碼" TextMode="Password" required="required">
                </asp:TextBox>
            </div>
            <%-- 上傳圖片開始 --%>
            <div class="form-group  col-md-8">
                <label for="exampleInputEmail1">個人圖片上傳: </label>
                <%-- 預覽fileupload照片  --%>
                <div id="picview"></div>
                <asp:FileUpload ID="FileUpload" runat="server" required="" onchange="picview(this)" />
                <%--預覽fileupload照片 end  --%>
            </div>

            <div class="form-group  col-md-8">
                <label for="exampleInputEmail1">使用權限:</label>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>管理者權限</asp:ListItem>
                    <asp:ListItem>新聞權限</asp:ListItem>
                    <asp:ListItem>遊艇權限</asp:ListItem>
                    <asp:ListItem>代理商權限</asp:ListItem>
                </asp:CheckBoxList>
            </div>
            <div id="progressNumber"></div>
            <%-- 上傳圖片結束 --%>

            <div class="form-group  col-md-8 mt-3">
                <asp:Button ID="btSignup" runat="server" CssClass="btn btn-success" Text="註冊" OnClick="btSignup_Click" />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>