<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ck.aspx.cs" ValidateRequest="False" Inherits="yacht.WebForm1" %>
<%@ Register Assembly="CKFinder" Namespace="CKFinder" TagPrefix="CKFinder" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="200px" Width="600px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <script>
            CKEDITOR.replace('TextBox1',
                {
                    filebrowserBrowseUrl: '/Scripts/ckfinder/ckfinder.html',
                    filebrowserImageBrowseUrl: '/Scripts/ckfinder/ckfinder.html?type=Images',
                    filebrowserFlashBrowseUrl: '/Scripts/ckfinder/ckfinder.html?type=Flash',
                    filebrowserUploadUrl: '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
                    filebrowserImageUploadUrl: '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
                    filebrowserFlashUploadUrl: '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
                });
        </script>
    </form>
</body>
</html>