<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="True" CodeBehind="fontNews.aspx.cs" Inherits="yacht.WebForm2" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/page.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p>
                <span>NEWS</span>
            </p>
            <ul>
                <li><a href="#">News & Events</a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="crumb">
        <a href="index.aspx">Home</a> >> <a href="#">News </a>>> <a href="#"><span class="on1">News &
                    Events</span></a>
    </div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <span>News & Events</span>
            </div>
            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box2_list">
                <ul>
                    <%-- 綁新聞數據s --%>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="list01">
                                    <ul>
                                        <li>
                                            <div>
                                                <p>
                                                    <img id="imgs" alt="新聞圖片" src="ckfinder/userfiles/images/<%# Eval("img") %>" style="border-width: 0px;" />
                                                </p>
                                            </div>
                                        </li>
                                        <li><span><%# Eval("date") %></span><br />
                                            <a href="fontView.aspx?id=<%#Eval("id") %>"><%# Eval("title") %></a></li>
                                        <br />
                                        <li><%# Eval("detail") %> </li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- 綁新聞數據e --%>
                </ul>
                <div class="pagenumber">
                    <%--<div class="pagination">共<span style="color: red">63</span>筆資料<span class="disabled">上一頁</span><span class="current">1</span><a href="new_list.aspx?page=2">2</a><a href="new_list.aspx?page=3">3</a><a href="new_list.aspx?page=4">4</a><a href="new_list.aspx?page=5">5</a><a href="new_list.aspx?page=6">6</a><a href="new_list.aspx?page=7">7</a><a href="new_list.aspx?page=2">下一頁</a></div>--%>
                    <uc1:WebUserControl1 runat="server" ID="WebUserControl1" />
                </div>
            </div>

            <!--------------------------------內容結束------------------------------------------------------>
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