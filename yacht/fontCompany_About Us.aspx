<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontCompany_About Us.aspx.cs" Inherits="yacht.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>COMPANY </span></p>
            <ul>
                <li><a href='fontCompany_About Us.aspx' target='_self'>About Us</a></li>
                <li><a href='fontCompany_Certificat.aspx' target='_self'>Certificat</a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#">Company  </a>>> <a href="#"><span class="on1">About Us</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>About Us</span></div>

            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box3">
                <p class="box3pright">
                    <img src="font/images/pit010.jpg" alt="&quot;&quot;" width="274" height="192" />
                </p>
                “Our aim is to create outstanding styling, live aboard comfort, and safety at sea for every proud Tayana owner.”<br />
                <br />
                Founded in 1973, Ta Yang Building Co., Ltd. Has built over 1400 blue water cruising yachts to date. This world renowned custom yacht builder offers a large compliment of sailboats ranging from 37’ to 72’, many offer aft or center cockpit design, deck saloon or pilothouse options.<br />
                <br />
                In 2003, Tayana introduced the new Tayana 64 Deck Saloon, designed by Robb Ladd, which offers the latest in building techniques, large sail area and a beam of 18 feet.
                <br />
                <br />
                Tayana Yachts have been considered the leader in building custom interiors for the last two decades, offering it`s clients the luxury of a living arrangement they prefer rather than have to settle for the compromise of a production boat. Using the finest in exotic woods, the best equipment such as Lewmar, Whitlock, Yanmar engines, Selden spars to name a few, Ta yang has achieved the reputation for building one of the finest semi custom blue water cruising yachts in the world, at an affordable price.
 <br />
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