<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontCompany_Certificat.aspx.cs" Inherits="yacht.WebForm5" %>

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
    <!--------------------------------右邊選單開始---------------------------------------------------->
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="fontCompany_About Us.aspx">Company  </a>>> <a href="fontCompany_Certificat.aspx"><span class="on1">Certificat</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>Certificat</span></div>

            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box3">
                Tayana Yacht has the approval of ISO9001: 2000 quality certification by Bureau Veritas Certification (Taiwan) Co., Ltd in 2002. In August, 2011, formally upgraded to ISO9001: 2008. We will continue to adhere to quality-oriented, transparent and committed to delivering improvement customer satisfaction and build even stronger trusting relationships with customers.
                <br />
                <br />

                <div class="pit">
                    <ul>
                        <li>
                            <p>
                                <img src="font/images/certificat001.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat002.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat003.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat004.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat005.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat006.jpg" alt="Tayana " /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat007.jpg" alt="Tayana " width="319" height="234" /></p>
                        </li>
                        <li>
                            <p>
                                <img src="font/images/certificat008.jpg" alt="Tayana " width="319" height="234" /></p>
                        </li>
                    </ul>
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