<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontDealers.aspx.cs" Inherits="yacht.WebForm6" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .small {
            width: 209px;
            height: 157px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>DEALERS</span></p>
            <%--左選單  --%>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a href='fontDealers.aspx?id=<%# Eval("id") %>'><%# Eval("country") %></a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- 右標題連結 --%>
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#">Dealers </a>>><asp:HyperLink ID="HyperLink1" runat="server" CssClass="on1" NavigateUrl="#"></asp:HyperLink></div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
            <div class="box2_list">
                <ul>

                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="list02">
                                    <ul>
                                        <li class="list02li">
                                            <div>
                                                <p>
                                                    <img class="small" id="<%# Eval("國家id") %>" alt="" src="ckfinder/userfiles/images/<%# Eval("img") %>" style="border-width: 0px;" />
                                                </p>
                                            </div>
                                        </li>
                                        <li class="list02li02"><span><%#Eval("地區") %></span><br />
                                            <%#Eval("agent") %><br />
                                            Contact：<%#Eval("contact") %><br />
                                            Address：<%#Eval("address") %><br />
                                            TEL：<%#Eval("tel") %><br />
                                            E-Mail: <%#Eval("email") %>
                                            <br />
                                            <a target='_blank'></a></li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="pagenumber">
                    <uc1:WebUserControl1 runat="server" ID="WebUserControl1" />
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