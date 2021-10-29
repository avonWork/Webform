<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontView.aspx.cs" Inherits="yacht.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        cite img {
            width: 700px;
            height: 360px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p>
                <span>News & Events</span>
            </p>
            <ul>
                <li><a href="#">News & Events</a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="fontNews.aspx">News</a>>> <a href="#"><span class="on1">News & Events</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>News & Events</span></div>
            <div class="box3">
                <h4>
                    <asp:Label ID="Label2" runat="server" Text='<%Eval("title")%>'></asp:Label></h4>
                <!--------------------------------內容開始---------------------------------------------------->
                <%--<asp:Literal ID="Literal1" runat="server" Text='<%Eval("new_content")%>'></asp:Literal>--%>
                <asp:Label ID="Label1" runat="server" Text='<%Eval("new_content")%>'></asp:Label>
                <!--------------------------------內容結束------------------------------------------------------>
                <div class="buttom001">
                    <a href="javascript:window.history.back();">
                        <img src="font/images/back.gif" alt="&quot;&quot;" width="55" height="28"></a>
                </div>
            </div>
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
    <!--遮罩-->
    <div class="bannermasks">
        <img src="font/images/company.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <div class="banner">
        <ul>
            <li>
                <img src="font/images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
</asp:Content>