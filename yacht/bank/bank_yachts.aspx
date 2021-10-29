<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" ValidateRequest="False" AutoEventWireup="true" CodeBehind="bank_yachts.aspx.cs" Inherits="yacht.bank.WebForm14" %>

<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ckeditor/ckeditor.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var hash = location.hash;//獲取到跳轉頁面的引數
            var tab = $('.nav-pills a');
            var con = $('.tab-content .tab-pane');
            $(".nav-pills a").click(function (e) {
                window.location.hash = $(this).attr("href");
                if ($('input[name ="ctl00$ContentPlaceHolder1$yachttile"]').val() == "" && window.location.hash != "#tab1") {
                    alert("請輸入遊艇名稱");
                    return false;
                }
            });

            for (var i = 0; i < con.length; i++) {
                var mm = con[i];
                var selectCon = "#" + $(mm).attr('id');
                if (hash == selectCon) {
                    tab.siblings().removeClass('active');
                    con.siblings().removeClass('show active');
                    $(tab[i]).addClass('active');
                    $(con[i]).addClass('show active');
                } else if (hash == "") {
                    $(tab[0]).addClass('active');
                    $(con[0]).addClass('show active');
                }
            }
        });
    </script>
    <script>
        $(document).ready(function () {
            //服器端控件透過jq加class(請先加入jquery版本)
            $(document).ready(function () {
                $("#ContentPlaceHolder1_CheckBox1").attr("class", "custom-control-input");
            });
        });
    </script>
    <style>
        .custom-checkbox .custom-control-label:before {
            width: 1.5rem;
            height: 1.5rem;
        }

        .custom-checkbox .custom-control-label:after {
            left: -19px;
            top: 13px;
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
    <div class="container">
        <%-- 隱藏 TabName--%>
        <asp:HiddenField ID="TabName" runat="server" />
        <%-- 標題s--%>
        <ul class="nav nav-pills nav-fill mb-2" id="pills-tab" role="tablist">
            <li class="nav-item h5">
                <a class="nav-link" id="tab1-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="true">YachtName&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">1</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab2-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false">Overview&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">2</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab3-tab" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false">Layout&deck plan&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">3</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab4-tab" data-toggle="tab" href="#tab4" role="tab" aria-controls="tab4" aria-selected="false">Specification&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">4</span></a>
            </li>
            <li class="nav-item h5">
                <asp:LinkButton ID="tab5a" runat="server" class="nav-link" aria-controls="tab5" aria-selected="false" OnClick="tab5a_OnClick">Fileupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">5</span></asp:LinkButton>
                <%--<a class="nav-link" id="tab5-tab" aria-controls="tab5" aria-selected="false" href="bank_filepage.aspx?#tab5">Fileupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">5</span></a>--%>
            </li>
            <li class="nav-item h5">
                <%--<a class="nav-link" id="tab6-tab" aria-controls="tab6" aria-selected="false" href="bank_imgpage.aspx#tab6">Imageupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">6</span></a>--%>
                <asp:LinkButton ID="tab6a" runat="server" class="nav-link" aria-controls="tab6" aria-selected="false" OnClick="tab6a_OnClick">Imageupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">6</span></asp:LinkButton>
            </li>
        </ul>
        <%-- 頁面 --%>
        <div class="tab-content" id="tab-next-prev-content">

            <%-- YachtName --%>
            <div class="tab-pane  text-center table-warning mt-3" id="tab1" role="tabpanel" aria-labelledby="tab1-tab">
                <%-- 最新船型s --%>
                <div class="form-inline pt-2">
                    <label for="yachttile" class="ml-5 col-sm-2 col-form-label col-form-label-lg pb-2">最新船型：</label>
                    <div class="chk-option mb-3">
                        <label class="check-task custom-control custom-checkbox d-flex justify-content-center done-task">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <span class="custom-control-label"></span>
                        </label>
                    </div>
                </div>
                <%-- 遊艇型號s --%>
                <div class="form-inline py-3 ">
                    <label for="yachttile" class="ml-5 col-sm-2 col-form-label col-form-label-lg">遊艇型號：</label>
                    <asp:TextBox ID="yachttile" class="form-control  col-sm-8" placeholder="請輸入要增加的遊艇型號" runat="server" required="required" OnTextChanged="yachttile_TextChanged" AutoPostBack="True"></asp:TextBox>
                </div>
            </div>
            <%-- Overview --%>
            <div class="tab-pane" id="tab2" role="tabpanel" aria-labelledby="tab2-tab">
                <asp:TextBox ID="content" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <%-- Layout --%>
            <div class="tab-pane" id="tab3" role="tabpanel" aria-labelledby="tab3-tab">
                <asp:TextBox ID="content1" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <%-- Specification --%>
            <div class="tab-pane" id="tab4" role="tabpanel" aria-labelledby="tab4-tab">
                <asp:TextBox ID="content2" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <%-- Fileupload --%>
            <div class="tab-pane" id="tab5" role="tabpanel" aria-labelledby="tab5-tab">
            </div>
            <%--Imageupload  --%>
            <div class="tab-pane" id="tab6" role="tabpanel" aria-labelledby="tab6-tab">

                <asp:Button ID="Button3" runat="server" CssClass="btn  btn-dark  mt-4 float-lg-right" Text="Submit" />
            </div>
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
        CKEDITOR.replace('ContentPlaceHolder1_content1',
            {
                filebrowserBrowseUrl: '../Scripts/ckfinder/ckfinder.html',
                filebrowserImageBrowseUrl: '../Scripts/ckfinder/ckfinder.html?type=Images',
                filebrowserFlashBrowseUrl: '../Scripts/ckfinder/ckfinder.html?type=Flash',
                filebrowserUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
                filebrowserImageUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
                filebrowserFlashUploadUrl: '../Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
            });
        CKEDITOR.replace('ContentPlaceHolder1_content2',
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
</asp:Content>