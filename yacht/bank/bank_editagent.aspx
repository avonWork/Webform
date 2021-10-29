<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_editagent.aspx.cs" Inherits="yacht.bank.WebForm8" %>

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

    <script type="text/javascript">
        //img 網頁初始加載
        window.onload = function () {
            var imgvalue = document.all('ContentPlaceHolder1_HiddenField1').value;
            document.getElementById('picview').innerHTML = '<img class="img1" src="../ckfinder/userfiles/images/' + imgvalue + '" />';
        }

        //img 檔案加載
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
                var imgvalue = document.all('ContentPlaceHolder1_HiddenField1').value;
                document.getElementById('picview').innerHTML = '<img class="img1" src="../ckfinder/userfiles/images/' + imgvalue + '" />';
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
                        <h5 class="m-b-10">編輯代理商</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#!"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">編輯代理商</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <%-- 下拉選單s--%>
        <div class="form-group row mt-3">
            <label for="ddlarea" style="text-align: end;" class="col-sm-2 col-form-label col-form-label-lg">國家地區：</label>
            <div class="d-flex col-sm-9 ">
                <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control  col-sm-6" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList ID="ddlarea" runat="server" CssClass="form-control  col-sm-6"></asp:DropDownList>
            </div>
        </div>
        <%-- 下拉選單e --%>
        <div class="form-group row">
            <label for="editAgent" style="text-align: end;" class="col-sm-2 col-form-label col-form-label-lg text-primary">代理商：</label>
            <div class="col-sm-9 ">
                <asp:TextBox ID="editAgent" runat="server" CssClass="form-control" placeholder="請輸入代理商" required="required">
                </asp:TextBox>
            </div>
        </div>
        <%--  --%>
        <div class="row">
            <div class="col-sm-2 text-center mb-3 mt-3 d-flex flex-column">
                <label for="editContact" class="col-form-label col-form-label-lg">聯繫人：</label>
                <label for="editTel" class="col-form-label col-form-label-lg">電話：</label>
                <label for="editFax" class="col-form-label col-form-label-lg">傳真：</label>
                <label for="FileUpload1" class="col-form-label col-form-label-lg ">圖片上傳：</label>
            </div>
            <div class="col-sm-5 mb-3 pt-0 d-flex flex-column">
                <asp:TextBox ID="editContact" runat="server" CssClass="form-control mt-3" placeholder="請輸入聯繫人姓名" required="required">
                </asp:TextBox>
                <asp:TextBox ID="editTel" type="tel" runat="server" CssClass="form-control mt-3" placeholder="請輸入電話" required="required">
                </asp:TextBox>
                <asp:TextBox ID="editFax" type="tel" runat="server" CssClass="form-control mt-3 mb-3" placeholder="請輸入傳真">
                </asp:TextBox>
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("img") %>' />
                <asp:FileUpload ID="FileUpload1" runat="server" onchange="picview(this)" />
            </div>
            <div class="col-sm-5 mt-2 pl-5 d-flex flex-column">
                <div class="mt-2 " id="picview"></div>
            </div>
        </div>

        <div class="form-group row">
            <label for="editAddress" style="text-align: end;" class="col-sm-2 col-form-label col-form-label-lg">地址：</label>
            <div class="col-sm-9 ">
                <asp:TextBox ID="editAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" placeholder="請輸入地址" required="required">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <label for="editEmail" style="text-align: end;" class="col-sm-2 col-form-label col-form-label-lg">E-mail：</label>
            <div class="col-sm-9 ">
                <asp:TextBox ID="editEmail" type="email" runat="server" CssClass="form-control" placeholder="請輸入E-mail" required="required">
                </asp:TextBox><asp:Label ID="Label2" runat="server" Text="*Email格式不正確!" Visible="false" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="container" style="text-align: center;">
            <asp:Button ID="Button1" runat="server" CssClass="btn  btn-danger  mt-4 mr-5" Text="更新" OnClick="Button1_Click" />
            <%--            <input type="reset" class="btn  btn-warning  mt-4 mr-5 " value="清除">--%>
            <asp:Button ID="Button2" runat="server" CssClass="btn  btn-info  mt-4" Text="取消" OnClick="Button2_Click" />
        </div>
    </div>
</asp:Content>