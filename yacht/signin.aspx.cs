using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace yacht
{
    public partial class bank_signin : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //UserName Password
            string loginID = UserName.Text.Trim();
            string loginpwd = Password.Text.Trim();
            DataRow dataRow = dBhelper.Login(loginID, loginpwd);
            if (dataRow != null)
            {
                #region 保留舊版cookie

                //if (CheckBox1.Checked)
                //{
                //    Response.Cookies["userid"].Value = loginID;
                //    Response.Cookies["password"].Value = loginpwd;
                //    Response.Cookies["userid"].Expires = DateTime.Now.AddDays(15);
                //    Response.Cookies["password"].Expires = DateTime.Now.AddDays(15);
                //}
                //else
                //{
                //    Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
                //}

                #endregion 保留舊版cookie

                //創類別物件
                userinformation Userinformation = new userinformation();
                //替類別存入內容(下面是用DataTable從資料庫取得資料)
                Userinformation.id = Convert.ToInt16(dataRow["id"].ToString());
                Userinformation.userid = dataRow["loginId"].ToString();
                Userinformation.password = dataRow["loginPwd"].ToString();
                Userinformation.name = dataRow["adminName"].ToString();
                Userinformation.permission = dataRow["authority"].ToString();
                Userinformation.photo = "~/ckfinder/userfiles/images/" + dataRow["imgName"].ToString();
                //將物件序列化成字串
                string userData = JsonConvert.SerializeObject(Userinformation);
                //副程式SetAuthenTicket 創立一張驗證票跟存入Cookie
                SetAuthenTicket(userData, Userinformation.permission);//使用者資訊和使用者權限
                //跳轉至登入後的頁面
                Response.Redirect("bank/bank_home.aspx");
            }
            //登入失敗
            else
            {
                lblError.Text = "帳號或密碼輸入失敗!請重新輸入~";
            }
        }

        private void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, //版本
                userId, //使用者名稱
                DateTime.Now, //發行時間
                DateTime.Now.AddHours(3), //有效時間
                false, //是否將 Cookie 設定成 Session Cookie，如果是則會在瀏覽器關閉後移除。
                userData); //使用者資訊(可以想成備註欄);

            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authenticationcookie.Expires = DateTime.Now.AddHours(3);

            //將Cookie寫入回應
            Response.Cookies.Add(authenticationcookie);
        }
    }
}