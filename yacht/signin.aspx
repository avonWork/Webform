<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signin.aspx.cs" Inherits="yacht.bank_signin" %>

<html lang="en">

<head>
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="">
    <meta name="author" content="Phoenixcoded" />
    <!-- Favicon icon -->
    <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon">

    <!-- vendor css -->
    <link href="assets/css/style.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            //服器端控件透過jq加class(請先加入jquery版本)
            $(document).ready(function () {
                $("#CheckBox1").attr("class", "custom-control-input");
            });
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="auth-wrapper1">
            <div class="auth-content text-center">
                <img src="assets/images/logo.png" alt="" class="img-fluid mb-4">
                <div class="card borderless">
                    <div class="row align-items-center ">
                        <div class="col-md-12">
                            <div class="card-body">
                                <h4 class="mb-3 f-w-400">Signin</h4>
                                <hr>
                                <div class="form-group mb-3">
                                    <asp:TextBox ID="UserName" CssClass="form-control" runat="server" placeholder="請輸入帳號"></asp:TextBox>
                                </div>
                                <div class="form-group mb-4">
                                    <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password" placeholder="請輸入密碼"></asp:TextBox>
                                </div>
                                <div class="custom-control custom-checkbox text-left mb-4 mt-2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <label class="custom-control-label" for="CheckBox1">Save credentials.</label>
                                </div>
                                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                                <hr>
                                <asp:Button ID="Button1" runat="server" Text="登入" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <hr>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- [ auth-signin ] end -->

        <!-- Required Js -->
        <script src="font/assets/js/vendor-all.min.js"></script>
        <script src="font/assets/js/plugins/bootstrap.min.js"></script>

        <script src="font/assets/js/pcoded.min.js"></script>
    </form>
</body>
</html>