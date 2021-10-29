<%@ Page Title="" Language="C#" MasterPageFile="~/bank/Site1.Master" AutoEventWireup="true" CodeBehind="bank_changePwd.aspx.cs" Inherits="yacht.bank.WebForm27" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-12">
                    <div class="page-header-title">
                        <h5 class="m-b-10">修改密碼</h5>
                    </div>
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="bank_yachtsList.aspx"><i class="feather icon-home"></i></a></li>
                        <li class="breadcrumb-item"><a href="#!">修改密碼</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container ">
        <div class="form-group ml-5">
            <p class="h1 text-primary my-5">密碼修改</p>
        </div>
        <div class="form-group has-success row  ml-5">
            <div class="col-sm-2">
                <label class="h4 text-success">原密碼</label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="OldPwd" class="form-control form-control-warning" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reoldpwd" CssClass="text-danger" runat="server" ErrorMessage="*必填欄位" ControlToValidate="OldPwd"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group has-success row  ml-5">
            <div class="col-sm-2">
                <label class="h4 text-success">新密碼</label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="NewPwd" class="form-control form-control-danger" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="renewpwd" CssClass="text-danger" runat="server" ErrorMessage="*必填欄位" ControlToValidate="NewPwd"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group has-success row  ml-5 mb-0">
            <div class="col-sm-2">
                <label class="h4 text-success">確認密碼</label>
            </div>
            <div class="col-sm-4">
                <asp:TextBox ID="CheckPwd" class="form-control form-control-danger" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ErrorMessage="*必填欄位" ControlToValidate="CheckPwd"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ForeColor="Red" ControlToCompare="CheckPwd" ControlToValidate="NewPwd" ErrorMessage="*密碼不同"></asp:CompareValidator>
            </div>
        </div>
        <div class="form-group ml-5">
            <asp:Label ID="ltaMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group ml-5 mt-5">
            <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-out" Text="Button" OnClick="Button1_Click" />
        </div>
    </div>
</asp:Content>