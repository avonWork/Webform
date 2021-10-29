<%@ Page Title="" Language="C#" MasterPageFile="~/font.Master" AutoEventWireup="true" CodeBehind="fontContact.aspx.cs" Inherits="yacht.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--引用SweetAlert2.js-->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/7.0.0/sweetalert2.all.js"></script>
    <script type="text/javascript">
        // js的值抓(國家下拉選單)
        function getcountry() {
            var country = document.getElementById("<%=fontcountry.ClientID %>").value; //下拉選單
            var countryid = document.getElementById("<%=txt_Test.ClientID %>");//textbox
            countryid.value = country;
        }
        // js的值抓(遊艇下拉選單)
        function getyacht() {
            var yacht = document.getElementById("<%=fontyacht.ClientID %>").value; //下拉選單
            var yachtid = document.getElementById("<%=txt2_Test.ClientID %>");//textbox
            yachtid.value = yacht;
        }
        //彈跳視窗
        function myConfirm() {
            let btnName = $("input#<%= ImageButton1.ClientID%>").attr("name");
            //confirm dialog範例
            swal().then(
                function (result) {
                    if (result.value) {
                        //使用者按下「確定」要做的事
                        //呼叫ASP.net自動產生的JS函數，第二個參數不給值避免網頁出錯
                        __doPostBack(btnName, "");
                    }
                }//end function
            );//end then
        }//end function myConfirm()
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="left">
        <div class="left1">
            <p><span>CONTACT</span></p>
            <ul>
                <li><a href="#">contacts</a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#"><span class="on1">Contact</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>Contact</span></div>
            <!--------------------------------內容開始---------------------------------------------------->
            <!--表單-->
            <asp:TextBox ID="txt_Test" Style="display: none;" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="txt2_Test" Style="display: none;" runat="server" Text=""></asp:TextBox>
            <div class="from01">
                <p>
                    Please Enter your contact information<span class="span01">*Required</span>
                </p>
                <br />
                <table>
                    <tr>
                        <td class="from01td01">Name :</td>
                        <td><span>*</span><asp:TextBox ID="name" name="name" type="text" Style="width: 250px;" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Email :</td>
                        <td><span>*</span><asp:TextBox ID="email" name="email" type="email" Style="width: 250px;" runat="server"></asp:TextBox><asp:Label ID="Label2" runat="server" Text="*Email格式不正確!" Visible="false" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Phone :</td>
                        <td><span>*</span>
                            <asp:TextBox ID="phone" name="phone" type="tel" Style="width: 250px;" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Country :</td>
                        <td><span>*</span>
                            <asp:DropDownList ID="fontcountry" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                    </tr>
                    <tr>
                        <td class="from01td01">&nbsp;</td>
                        <td><span>*</span>
                            <asp:DropDownList ID="fontyacht" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Comments:</td>
                        <td>
                            <asp:TextBox ID="comments" Rows="2" cols="20" Style="height: 150px; width: 330px;" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">&nbsp;</td>
                        <td class="f_right">
                            <asp:Image ID="image1" runat="server" Height="30px" ImageUrl="~/CreateImage.aspx" Style="padding-right: 8px" align="middle" alt="Please enter the verification code" border="0" title="Please enter the verification code" />
                            <asp:TextBox ID="TxtVCode" type="text" placeholder="請輸入驗證碼" runat="server"></asp:TextBox><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">&nbsp;</td>
                        <td class="f_right">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="font/images/buttom03.gif" Style="border-width: 0px;" OnClick="ImageButton1_Click" OnClientClick="getcountry();getyacht();" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--表單-->
            <div class="box1">
                <span class="span02">Contact with us</span><br />
                Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
                As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers.
                If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
            </div>
            <div class="list03">
                <p>
                    <span>TAYANA HEAD OFFICE</span><br />
                    NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                    tel. +886(7)641 2422<br />
                    fax. +886(7)642 3193<br />
                </p>
            </div>
            <div class="box4">
                <h4>Location</h4>
                <p>
                    <iframe width="695" height="518" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d58974.911650660564!2d120.3588577652943!3d22.506735165310847!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3471e28b31b583b9%3A0x5a4c7301708ccba5!2z5p6X5ZyS5rW35rSL5rq85Zyw5YWs5ZyS!5e0!3m2!1szh-TW!2stw!4v1611585857298!5m2!1szh-TW!2stw"></iframe>
                </p>
            </div>
            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>
    <!--------------------------------右邊選單結束---------------------------------------------------->
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