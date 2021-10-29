<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="google.aspx.cs" Inherits="yacht.WebForm11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="font/Scripts/jquery.min.js"></script>
    <script src="https://www.google.com/recaptcha/api.js?render=6LdqKVwaAAAAACV2w_KzR7WVyRWXv647Zcqb-eSF"></script>
    <script>
        $(function () {
            grecaptcha.ready(function () {
                grecaptcha.execute('6LdqKVwaAAAAACV2w_KzR7WVyRWXv647Zcqb-eSF', { action: 'login' }).then(function (token) {
                    $('#token').val(token);
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server" name="subscribeform" action="recaptcha3.aspx">
        <div class="card-body1">
            <h4 class="mb-3 f-w-400">管理者登入</h4>
            <hr />
            <div class="form-group mb-3">
                <input type="text" class="form-control" id="Email" name="email" placeholder="帳號" />
            </div>
            <%--  <div class="form-group mb-4">
                <input type="password" class="form-control" id="Password" placeholder="密碼">
            </div>
            <div class="custom-control custom-checkbox text-left mb-4 mt-2">
                <input type="checkbox" class="custom-control-input" id="customCheck1">
                <label class="custom-control-label" for="customCheck1">記住我的帳號</label>
            </div>--%>
            <input type="hidden" id="token" name="token" />
            <input type="submit" value="submit" name="submit" />
        </div>
    </form>
</body>
</html>