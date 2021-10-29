<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_editAdmin.aspx.cs" Inherits="yacht.bank.WebForm23" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- 放header 預覽fileupload照片 start --%>
    <style type="text/css">
        #picview, .img1 {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            position: relative;
            left: -1px;
            top: -1px;
            border: 1px solid orange;
        }

        #picview {
            border: 1px solid orange;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        //img 網頁初始加載
        window.onload = function () {
            var imgvalue = document.all('ContentPlaceHolder1_HiddenField1').value;
                //var imgvalue = document.getElementById("<%=HiddenField1.ClientID %>").value;
            document.getElementById('picview').innerHTML = '<img class="img1" src="../ckfinder/userfiles/images/' + imgvalue + '" />';
        }
        //服器端控件透過jq加class(請先加入jquery版本)
        $(document).ready(function () {
            $("#ContentPlaceHolder1_CheckBox2").attr("class", "custom-control-input");
        });
        //img 檔案加載
        function picview(file) {
            var picviewDiv = document.getElementById('picview');
            if (file.files && file.files[0]) {
                var reader = new FileReader();
                reader.onload = function (evt) {
                    picviewDiv.innerHTML = '<img class="img1 border border-warning rounded-circle" src="' + evt.target.result + '" />';
                }
                reader.readAsDataURL(file.files[0]);
            }
            else {
                picviewDiv.innerHTML = '<div class="img1" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>';
                var imgvalue = document.all('ContentPlaceHolder1_HiddenField1').value;
                document.getElementById('picview').innerHTML = '<img class="img1 border border-warning rounded-circle" src="../ckfinder/userfiles/images/' + imgvalue + '" />';
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
                        <h5 class="m-b-10">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal></h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_adminLIist.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </a></li>
                    </ul>
                </div>
                `
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="media">
            <div class="media-body col-md-6">
                <div class="row mr-2">
                    <div class="form-group  col-md-11">
                        <label for="exampleInputEmail1" class="text-success">管理者帳號: </label>
                        <asp:TextBox ID="UserId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group  col-md-11">
                        <label for="exampleInputEmail1">修改管理者姓名:</label>
                        <asp:TextBox ID="Name" runat="server" CssClass="form-control" placeholder="請輸入姓名" required="required"></asp:TextBox>
                    </div>
                    <div id="adpwd" class="form-group  col-md-11" style="display: none;" runat="server">
                        <label for="exampleInputEmail1">修改管理者密碼: </label>
                        <%--  required --%>
                        <asp:TextBox ID="Pwd" runat="server" CssClass="form-control" placeholder="請輸入密碼" TextMode="Password">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <%-- 預覽fileupload照片  --%>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <div class="mt-2 " id="picview">
            </div>
            <%--預覽fileupload照片 end  --%>
            <%--            <asp:Image ID="Image1" class="border border-warning rounded-circle mt-3" Style="width: 150px; height: 150px;" runat="server" />--%>
        </div>
        <%-- 上傳圖片開始 --%>
        <div class="form-group  col-md-8">
            <label for="exampleInputEmail1">個人圖片上傳: </label>
            <asp:FileUpload ID="FileUpload1" runat="server" onchange="picview(this)" />
            <%--預覽fileupload照片 end  --%>
        </div>

        <div id="cbauthority" class="form-group  col-md-8" style="display: none;" runat="server">
            <label for="exampleInputEmail1">使用權限:</label>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                <%--                <asp:ListItem>管理者權限</asp:ListItem>
                <asp:ListItem>新聞權限</asp:ListItem>
                <asp:ListItem>遊艇權限</asp:ListItem>
                <asp:ListItem>代理商權限</asp:ListItem>--%>
            </asp:CheckBoxList>
        </div>
        <div id="progressNumber"></div>
        <%-- 上傳圖片結束 --%>

        <div class="form-group  col-md-8 mt-5">
            <asp:Button ID="btSignup" runat="server" CssClass="btn btn-success" Text="修改" OnClick="btSignup_Click" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>