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
    public partial class WebForm22 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //驗證是否具有該權限 沒有回到首頁
            string authority = HttpContext.Current.User.Identity.Name;
            if ((int.Parse(authority) & 8) == 0)
            {
                Response.Redirect("bank_home.aspx");
            }
        }

        protected void btSignup_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/ckfinder/userfiles/images/");   //-- 網站的URL路徑。
            string filetime = DateTime.Now.ToString("yyyyMMdd");
            string filename = filetime + FileUpload.FileName;
            FileUpload.PostedFile.SaveAs(path + filename);

            //宣告輸入框變數
            string userid = UserId.Text;
            string name = Name.Text;
            string pwd = Pwd.Text;
            //string imgPath = this.Image1.ImageUrl;
            string imgPath = path + filename;

            string pic = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
            string Authority = ""; //權限
            //權限列表
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    Authority += 1;
                }
                else
                {
                    Authority += 0;
                }
            }
            int Authoritynum = Convert.ToInt16(Authority, 2);  //转化为二进制
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "新增管理者-新增者: " + Myperson.id + " 姓名: " + Myperson.name;

            #region 保留

            //參數化
            // SqlParameter[] paras2 = new SqlParameter[]
            //{
            // new SqlParameter("@userid", userid)
            //};
            // string sql2 = "select * from  addadmin where loginId=@userid";
            // SqlDataReader reader = DBhelper.ExecuteRreader(sql2, paras2);

            #endregion 保留

            SqlDataReader reader = dBhelper.GetselectAdminId(userid);
            if (reader.Read())
            {
                Label1.Text = "**帳號已有人註冊!!";
                Label1.Attributes.Add("style", "color: red; font-weight: bold");
            }
            else
            {
                #region 保留

                // SqlParameter[] paras1 = new SqlParameter[]
                //{
                //        new SqlParameter("@userid", userid),new SqlParameter("@name", name),new SqlParameter("@pwd", MD5password(pwd)),new SqlParameter("@pic", pic),new SqlParameter("@Authoritynum",  Authoritynum)
                //};
                // //sql語法 新增留言資料
                // string sql = "insert addadmin (loginId,adminName,loginPwd,imgName,authority) values(@userid,@name,@pwd,@pic,@Authoritynum) ";
                // DBhelper.ExecuteNonQuery(sql, paras1);

                #endregion 保留

                dBhelper.insertAdmin(userid, name, pwd, pic, Authoritynum, adMypersonId, dotStirng);
                //顯示在菜單上
                //Response.Write("<script type='text/javascript'> alert('新增管理者成功!請重新登入');location.href = '../signin.aspx';</script>");
                Response.Write("<script type='text/javascript'> alert('更新成功!回到管理者列表');location.href = 'bank_adminLIist.aspx';</script>");
            }
        }
    }
}