<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="test_reserve.aspx.cs" Inherits="yacht.bank.WebForm13" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var i = 1
        function addFile() {
            if (i < 8) {
                var str = '<BR> <input type="file" name="File" runat="server" style="width: 300px" />描述:<input name="text" type="text" style="width: 150px" maxlength="20" />';
                document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", str);
            }
            else {
                alert("您一次最多隻能上傳8張圖片!")
            }
            i++
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="MyFile">
        <input onclick="addFile()" type="button" value="增加圖片(Add)"><br />
        <input type="file" name="File" runat="server" style="width: 300px" />
        描述:<input name="text" type="text" style="width: 150px" maxlength="20" />

        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <asp:Button ID="btnUpload" runat="server" Text="開始上傳" OnClick="btnUpload_Click" />
</asp:Content>