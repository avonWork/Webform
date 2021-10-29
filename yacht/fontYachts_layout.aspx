<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontYachts_layout.aspx.cs" Inherits="yacht.WebForm10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $('.topbuttom').click(function () {
                    $('html, body').scrollTop(0);
                });
            });
        });
    </script>

    <link rel="stylesheet" type="text/css" href="font/css/jquery.ad-gallery.css">
    <style type="text/css">
        img,
        div,
        input {
            behavior: url("");
        }
    </style>
    <script type="text/javascript" src="font/Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                var galleries = $('.ad-gallery').adGallery();
                galleries[0].settings.effect = 'fade';
                if ($('.banner input[type=hidden]').val() == "0") {
                    $(".bannermasks").hide();
                    $(".banner").hide();
                    $("#crumb").css("top", "125px");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>YACHTS</span></p>
            <%--左選單  --%>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a href='fontYachts_layout.aspx?id=<%# Eval("id") %>'><%# Eval("title") %></a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- 麵包屑s --%>
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#">Dealers </a>>><asp:HyperLink ID="HyperLink1" runat="server" CssClass="on1" NavigateUrl="#"></asp:HyperLink></div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
            <%-- 麵包屑e --%>
            <!--次選單-->
            <div class="menu_y">
                <ul>
                    <li class="menu_y00">YACHTS</li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" CssClass="menu_yli01" runat="server">Interior</asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="HyperLink4" CssClass="menu_yli02" runat="server">Layout & deck plan</asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" CssClass="menu_yli03" runat="server">Specification</asp:HyperLink></li>
                    <li></li>
                </ul>
            </div>
            <!--次選單-->
            <%-- layout s --%>
            <div class="box6">
                <p>Layout & deck plan</p>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
            <%-- layout e --%>
            <p class="topbuttom">
                <img src="font/images/top.gif" alt="top" />
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="footer">
        <div class="footerp00">
            <a href="http://www.tognews.com/" target="_blank">
                <img src="font/images/tog.jpg" alt="TOG" />
            </a>

            <p class="footerp001">
                © 1973-2012 Tayana Yachts, Inc. All Rights Reserved
            </p>
        </div>
        <div class="footer01">
            <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan,
                    R.O.C.</span><br />
            <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <!--------------------------------選單開始結束---------------------------------------------------->
    <!--遮罩-->
    <div class="bannermasks">
        <img src="font/images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <div class="banner1">
        <input type="hidden" name="ctl00$ContentPlaceHolder1$Gallery1$HiddenField1"
            id="ctl00_ContentPlaceHolder1_Gallery1_HiddenField1" value="1" />
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>