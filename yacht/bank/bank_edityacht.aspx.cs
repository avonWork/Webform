using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 2) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                showedityacht();
            }
        }

        private void showedityacht()
        {
            //抓遊艇id(抓數據庫id)
            string id = Request.QueryString["id"];
            SqlDataReader reader = dBhelper.SelectYacht(id);
            if (reader.Read())
            {
                yachttile.Text = reader["title"].ToString();
                content.Text = reader["overview"].ToString();
                content1.Text = reader["layout"].ToString();
                content2.Text = reader["specification"].ToString();
                CheckBox1.Checked = (bool)reader["newcheck"];
            }
        }

        protected void yachttile_TextChanged(object sender, EventArgs e)
        {
            //標題後面必須多一個空格避免連結第二次會失效
            Session["yachtTitle"] = yachttile.Text + " ";
            //標題重複
            DataTable table = dBhelper.GetYacht();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["title"].ToString().Trim() == Session["yachtTitle"].ToString().Trim())
                {
                    Response.Write("<script type='text/javascript'> alert('遊艇標題重複!請更改標題~');</script>");
                }
            }
        }

        protected void tab5a_OnClick(object sender, EventArgs e)
        {
            UpdateYachtmethod();
        }

        private void UpdateYachtmethod()
        {
            //抓遊艇id(抓數據庫id)
            string id = Request.QueryString["id"];
            string YachtTitle = yachttile.Text;
            string Content = content.Text;
            string Content1 = content1.Text;
            string Content2 = content2.Text;
            int checkvalue = 0;
            //如果
            if (CheckBox1.Checked == true)
            {
                checkvalue = 1;
            }

            DateTime editAdtime = DateTime.Now;
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "編輯遊艇-編輯者: " + Myperson.id + " 姓名: " + Myperson.name;
            try
            {
                dBhelper.UpdateYacht(id, YachtTitle, Content, Content1, Content2, checkvalue, editAdtime, adMypersonId,
                    dotStirng);
                Response.Redirect("bank_editFilepage.aspx?id=" + id);
            }
            catch
            {
                Response.Write("<script type='text/javascript'> alert('遊艇標題重複!請更改標題~');</script>");
            }
        }

        protected void tab6a_OnClick(object sender, EventArgs e)
        {
            UpdateYachtmethod();
        }
    }
}