<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" ValidateRequest="False" AutoEventWireup="true" CodeBehind="bank_new.aspx.cs" Inherits="yacht.bank.WebForm1" %>

<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ckeditor/ckeditor.js"></script>
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
        //服器端控件透過jq加class(請先加入jquery版本) 
        $(document).ready(function () {
            $("#ContentPlaceHolder1_CheckBox1").attr("class", "custom-control-input");
        });
    </script>
    <%-- 預覽fileupload照片 end --%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">新增新聞</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_newsList.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">新增新聞</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <%-- 標題s--%>
        <div class="form-group row mt-4">
            <label for="date" style="text-align: center;" class="col-sm-2 col-form-label col-form-label-lg">新聞日期：</label>
            <div class="col-sm-4 ">
                <asp:TextBox ID="date" type="date" runat="server" CssClass="form-control" placeholder="請輸入標題" required="required">
                </asp:TextBox>
            </div>
            <%-- bootstrap checkbox效果搭配jq --%>
            <div class="custom-control custom-switch col-sm-5 text-right pt-2" >
                <asp:CheckBox ID="CheckBox1" runat="server" />
               <label class="custom-control-label" for="ContentPlaceHolder1_CheckBox1">新聞置頂</label>
            </div>
        </div>

        <div class="form-group row text-right">
            <label for="title" style="text-align: center;" class="col-sm-2 col-form-label col-form-label-lg">新聞標題：</label>
            <div class="col-sm-9 ">
                <asp:TextBox ID="title" runat="server" CssClass="form-control" placeholder="請輸入標題" required="required">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <label for="detail" style="text-align: center;" class="col-sm-2 col-form-label col-form-label-lg">新聞介紹：
            </label>
            &nbsp;<div class="col-sm-9 ">
                <asp:TextBox ID="detail" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" placeholder="請輸入介紹" required="required">
                </asp:TextBox>
            </div>
        </div>

        <%-- 標題e--%>
        <%-- 上傳s --%>
        <div class="form-group row">
            <label for="FileUpload" style="text-align: center;" class="col-sm-2 col-form-label col-form-label-lg">圖片上傳：<br />
                <br />
                <br />
                新聞內容：</label>
            <div class="col-sm-4">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="pt-3" required=" required" onchange="picview(this)" />
            </div>
            <div class="col-sm-2 ">
                <%-- 預覽fileupload照片  --%>
                <div class="mt-2 " id="picview"></div>
                <%--預覽fileupload照片 end  --%>
            </div>
            <%--   </div>--%>
            <%-- 上傳e --%>

            <%-- ckedit s --%>
            <%--  <div class="form-group row">--%>
            <div class="container m-2">
                <asp:TextBox ID="content" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" CssClass="btn  btn-warning  mt-4 mr-5" Text="送出" OnClick="Button1_Click" />
                <a href="bank_newsList.aspx" class="btn  btn-info  mt-4">回首頁</a>
            </div>
        </div>
        <script>
            CKEDITOR.replace('ContentPlaceHolder1_content',
                {
                    filebrowserBrowseUrl: '../Scripts/ckfinder/ckfinder.html',
                    filebrowserImageBrowseUrl: '../Scripts/ckfinder/ckfinder.html?type=Images',
                    filebrowserFlashBrowseUrl: '../Scripts/ckfinder/ckfinder.html?type=Flash',
                    filebrowserUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
                    filebrowserImageUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
                    filebrowserFlashUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
                });
        </script>
        <%-- ckedit e --%>
    </div>
</asp:Content>