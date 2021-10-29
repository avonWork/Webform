<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" ValidateRequest="False" AutoEventWireup="true" CodeBehind="yachts_reserve.aspx.cs" Inherits="yacht.bank.WebForm10" %>

<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ckeditor/ckeditor.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            (function () {
                $('.btn-tab-prev').on('click', function (e) {
                    e.preventDefault()
                    $('#' + $('.nav-item > .active').parent().prev().find('a').attr('id')).tab('show')
                })
                $('.btn-tab-next').on('click', function (e) {
                    e.preventDefault()
                    $('#' + $('.nav-item > .active').parent().next().find('a').attr('id')).tab('show')
                })
            })();
            //bootstrap 檔案上傳jq
            $(".custom-file-input").on("change", function () {
                var fileName = $(this).val().split("\\").pop();
                $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
            });

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
        <%-- 標題s--%>
        <ul class="nav nav-pills nav-fill mb-2" id="pills-tab" role="tablist">
            <li class="nav-item h5">
                <a class="nav-link active" id="tab1-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="true">Overview&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">1</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab2-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false">Layout&deck plan&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">2</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab3-tab" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false">Specification&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">3</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab4-tab" data-toggle="tab" href="#tab4" role="tab" aria-controls="tab4" aria-selected="false">Fileupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">4</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab5-tab" data-toggle="tab" href="#tab5" role="tab" aria-controls="tab5" aria-selected="false">Imageupload&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">5</span></a>
            </li>
            <li class="nav-item h5">
                <a class="nav-link" id="tab6-tab" data-toggle="tab" href="#tab6" role="tab" aria-controls="tab6" aria-selected="false">YachtName&nbsp&nbsp&nbsp<span class="badge badge-pill badge-warning">6</span></a>
            </li>
        </ul>
        <%-- 頁面 --%>
        <div class="tab-content" id="tab-next-prev-content">
            <%-- Overview --%>
            <div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="tab1-tab">
                <asp:TextBox ID="content" runat="server" TextMode="MultiLine"></asp:TextBox>
                <a href="#" role="button" class="btn  btn-warning mt-4 float-right btn-tab-next">Next</a>
            </div>
            <%-- Layout --%>
            <div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="tab2-tab">
                <asp:TextBox ID="content1" runat="server" TextMode="MultiLine"></asp:TextBox>
                <a href="#" role="button" class="btn  btn-info  mt-4 btn-tab-prev">Previous</a>
                <a href="#" role="button" class="btn  btn-warning mt-4 float-right btn-tab-next">Next</a>
            </div>
            <%-- Specification --%>
            <div class="tab-pane fade" id="tab3" role="tabpanel" aria-labelledby="tab3-tab">
                <asp:TextBox ID="content2" runat="server" TextMode="MultiLine"></asp:TextBox>
                <a href="#" role="button" class="btn  btn-info  mt-4 btn-tab-prev">Previous</a>
                <a href="#" role="button" class="btn  btn-warning mt-4 float-right btn-tab-next">Next</a>
            </div>
            <%-- Fileupload --%>
            <div class="tab-pane fade" id="tab4" role="tabpanel" aria-labelledby="tab4-tab">
                <%-- bootstrap 檔案上傳樣式s(留著) --%>
  <%--                    <div class="form-inline pt-2">
                    <label for="FileUpload1" class="ml-5 col-sm-2 col-form-label col-form-label-lg mb-2">檔案上傳：</label>--%>
                    <div class="custom-file  col-sm-8">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="custom-file-input text-left bg-primary" />
                        <label class="custom-file-label" for="customFile">Choose file</label>
                    </div>
      <%--          </div>--%>
                <%-- bootstrap 檔案上傳樣式e --%>
                <input onclick="addFile()" type="button" value="增加檔案(Add)">
                <input onclick="deleteFile()" type="button" value="刪除檔案(Delete)">
                <table class="table table-info">
                    <thead>
                        <tr>
                            <th>檔案上傳(最多5個檔案)</th>
<%--                            <th style="width: 32%;">檔案檔名(未填依檔案檔名為主)</th>--%>
                            <th style="width: 60%;">檔案文字(檔案未顯示會顯示的文字)</th>
                            <th style="width: 6%;">順序</th>
                        </tr>
                        <tr>
                            <td>
                                <input name="fileFile" type="file" runat="server" /></td>
<%--                            <td>
                                <input name="filename" type="text" class="form-control form-control-sm"></td>--%>
                            <td>
                                <input name="filetext" type="text" class="form-control form-control-sm"></td>
                            <td>
                                <input name="fileorder" type="text" class="form-control form-control-sm"></td>
                        </tr>
                    </thead>
                    <tbody id="MyFile">
                    </tbody>
                </table>
                <a href="#" role="button" class="btn  btn-info  mt-4 btn-tab-prev">Previous</a>
                <a href="#" role="button" class="btn  btn-warning mt-4 float-right btn-tab-next">Next</a>
            </div>
            <%--Imageupload  --%>
            <div class="tab-pane fade" id="tab5" role="tabpanel" aria-labelledby="tab5-tab">
                <input onclick="addImg()" type="button" value="增加圖片(Add)">
                <input onclick="deleteImg()" type="button" value="刪除圖片(Delete)">
                <table class="table table-danger">
                    <thead>
                        <tr>
                            <th>圖片上傳(最多5個圖片)</th>
                     <%--       <th style="width: 32%;">圖片檔名(未填依圖片檔名為主)</th>--%>
                            <th style="width: 60%;">圖片文字(圖片未顯示會顯示的文字)</th>
                            <th style="width: 6%;">順序</th>
                        </tr>
                        <tr>
                            <td>
                                <input name="imgfile" type="file" runat="server" /></td>
                          <%--  <td>
                                <input name="imgname" type="text" class="form-control form-control-sm"></td>--%>
                            <td>
                                <input name="imgtext" type="text" class="form-control form-control-sm"></td>
                            <td>
                                <input name="imgorder" type="text" class="form-control form-control-sm"></td>
                        </tr>
                    </thead>
                    <tbody id="MyImg">
                    </tbody>
                </table>

                <%--多選檔案上傳  AllowMultiple="True"  --%>
                <a href="#" role="button" class="btn  btn-info  mt-4 btn-tab-prev">Previous</a>
                <a href="#" role="button" class="btn  btn-warning mt-4 float-right btn-tab-next">Next</a>
            </div>
            <%-- YachtName --%>
            <div class="tab-pane fade text-center table-warning mt-3" id="tab6" role="tabpanel" aria-labelledby="tab6-tab">
                <%-- 最新船型s --%>
                <div class="form-inline pt-2">
                    <label for="yachttile" class="ml-5 col-sm-2 col-form-label col-form-label-lg pb-2">最新船型：</label>
                    <div class="chk-option mb-3">
                        <label class="check-task custom-control custom-checkbox d-flex justify-content-center done-task">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <%--<input type="checkbox" class="custom-control-input">--%>
                            <span class="custom-control-label"></span>
                        </label>
                    </div>
                </div>
                <%-- 最新船型e --%>
                <div class="form-inline py-3 ">
                    <label for="yachttile" class="ml-5 col-sm-2 col-form-label col-form-label-lg">遊艇型號：</label>
                    <asp:TextBox ID="yachttile" class="form-control  col-sm-8" placeholder="請輸入要增加的遊艇型號" runat="server" required="required"></asp:TextBox>
                </div>
                <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />--%>
                <asp:Button ID="Button3" runat="server" CssClass="btn  btn-dark  mt-4 float-lg-right" Text="Submit" OnClick="Button3_Click" />
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

        //增加檔案按鈕
        var j = 1
        function addFile() {
            if (j < 8) {
                var str = `<tr><td ><input type="file" name="fileFile" runat="server" /></td><td><input name="filetext" type="text" class="form-control form-control-sm"></td><td><input name="fileorder" type="text" class="form-control form-control-sm"></td></tr>`;
                document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", str);
                j++
            }
            else {
                alert("您一次最多能上傳8個檔案!")
            }
        }
        //減少檔案按鈕
        function deleteFile() {
            if (j > 1) {
                document.getElementById('MyFile').lastChild.remove();
                j--;
            }
        }
        //增加圖片按鈕

        var i = 1
        function addImg() {
            if (i < 8) {
                var str = `<tr><td ><input type="file" name="imgfile" runat="server" /></td><td><input name="imgtext" type="text" class="form-control form-control-sm"></td><td><input name="imgorder" type="text" class="form-control form-control-sm"></td></tr>`;
                document.getElementById('MyImg').insertAdjacentHTML("beforeEnd", str);
                i++
            }
            else {
                alert("您一次最多能上傳8張圖片!");
            }
        }
        //減少圖片按鈕
        function deleteImg() {
            if (i > 1) {
                document.getElementById('MyImg').lastChild.remove();
                i--;
            }
        }
    </script>
    <%-- ckedit e --%>
</asp:Content>