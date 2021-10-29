using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm27 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //宣告輸入框變數
            string s = "";
            string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            string loginld = Myperson.userid; //取得UserData內的使用者資訊
            string newpwd = NewPwd.Text; //新密碼
            string oldpwd = OldPwd.Text;
            SqlDataReader reader = dBhelper.SelectAdminGetpwd(loginld, oldpwd);
            if (reader.Read())
            {
                s = reader["loginPwd"].ToString();
            }
            if (s != MD5password(oldpwd))
            {
                ltaMsg.Text = "<style='color:red;'>**原密碼有錯!請重新輸入</style>";
                return;
            }
            dBhelper.UpdateAdminPwd(loginld, newpwd);
            Response.Write("<script type='text/javascript'> alert('密碼修改成功!請重新登入');location.href = '../signIn.aspx';</script>");
        }

        #region MD5--學長提供

        protected static string MD5password(string str)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        #endregion MD5--學長提供
    }
}