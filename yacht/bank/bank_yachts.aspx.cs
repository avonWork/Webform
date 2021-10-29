using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm14 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 2) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }

                yachttile.Text = Request.QueryString["title"];
                if (yachttile.Text == "")
                {
                    File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/overview.txt", "");
                    File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/layout.txt", "");
                    File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/specification.txt", "");
                    //sql清掉中途檔案圖片-初始化

                    string sql = "SELECT DISTINCT yachtid FROM fileyacht WHERE NOT EXISTS(SELECT id FROM yachts WHERE yachts.id = fileyacht.yachtid)";
                    SqlDataReader reader = DBhelper.ExecuteRreader(sql);
                    if (reader.Read())
                    {
                        string sql2 = "DELETE fileyacht WHERE yachtid =(SELECT DISTINCT yachtid FROM fileyacht WHERE NOT EXISTS(SELECT id FROM yachts WHERE yachts.id = fileyacht.yachtid))";
                        DBhelper.ExecuteNonQuery(sql2);
                    }
                    //sql語法
                    string sql3 = "SELECT DISTINCT yachtid FROM imgyacht WHERE NOT EXISTS(SELECT id FROM yachts WHERE yachts.id = imgyacht.yachtid)";
                    SqlDataReader reader2 = DBhelper.ExecuteRreader(sql3);
                    if (reader2.Read())
                    {
                        string sql4 = "DELETE imgyacht WHERE yachtid =(SELECT DISTINCT yachtid FROM imgyacht WHERE NOT EXISTS(SELECT id FROM yachts WHERE yachts.id = imgyacht.yachtid))";
                        DBhelper.ExecuteNonQuery(sql4);
                    }
                }
                else
                {
                    if (Request.QueryString["check"].ToString() == "1")
                    {
                        CheckBox1.Checked = true;
                    }
                    content.Text = File.ReadAllText("C:/Users/user/source/repos/yacht/yacht/txt/overview.txt");
                    content1.Text = File.ReadAllText("C:/Users/user/source/repos/yacht/yacht/txt/layout.txt");
                    content2.Text = File.ReadAllText("C:/Users/user/source/repos/yacht/yacht/txt/specification.txt");
                }
                //寫好的文字檔
                //yachtcontent();
            }
        }

        /// <summary>
        /// 測試用(正式不用)
        /// </summary>
        private void yachtcontent()
        {
            //Tayana 37-1
            //string filegoblank1 = @"C:\Users\user\Desktop\Tayana\Tayana 54_2.txt";
            string filegoblank1 = @"C:\Users\user\source\repos\yacht\yacht\txt\overview.txt";
            content.Text = Goblank(filegoblank1);
            //Tayana 37-2
            //string filegoblank2 = @"C:\Users\user\Desktop\Tayana\Tayana 58_2.txt";
            string filegoblank2 = @"C:\Users\user\source\repos\yacht\yacht\txt\layout.txt";
            content1.Text = Goblank(filegoblank2);
            //Tayana 37-3
            //string filegoblank3 = @"C:\Users\user\Desktop\Tayana\Tayana 58_3.txt";
            string filegoblank3 = @"C:\Users\user\source\repos\yacht\yacht\txt\specification.txt";
            content2.Text = Goblank(filegoblank3);
        }

        /// <summary>
        /// Html編碼解碼(ckeditor)測試用(正式不用)
        /// </summary>
        /// <param name="filegoblank"></param>
        /// <returns></returns>
        private string Goblank(string filegoblank)
        {
            //.Replace("\"","\'")
            string filename = File.ReadAllText(filegoblank);
            string file = HttpUtility.HtmlEncode(filename).Replace(" \r\n\t", "");
            string result = HttpUtility.HtmlDecode(file).Replace("\t", "").Replace("\r\n\r\n", "");
            return result;
        }

        protected void yachttile_TextChanged(object sender, EventArgs e)
        {
            //標題後面必須多一個空格避免連結第二次會失效
            Session["yachtTitle"] = yachttile.Text + " ";
            //標題重複
            string sql8 = "select * from yachts";
            DataTable table = DBhelper.GetDataTable(sql8);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["title"].ToString().Trim() == Session["yachtTitle"].ToString().Trim())
                {
                    Response.Write("<script type='text/javascript'> alert('遊艇標題重複!請更改標題~');</script>");
                }
            }
            string sql5 =
                "SELECT CASE WHEN(SELECT COUNT(1) FROM yachts) = 0 THEN 0 ELSE IDENT_CURRENT('yachts')END AS NextId; ";
            SqlDataReader reader6 = DBhelper.ExecuteRreader(sql5);
            if (reader6.Read())
            {
                Session["yachtID"] = Convert.ToInt16(reader6[0].ToString()) + 1;
            }
        }

        protected void tab6a_OnClick(object sender, EventArgs e)
        {
            Context.Items["content"] = content.Text;
            Context.Items["content1"] = content1.Text;
            Context.Items["content2"] = content2.Text;
            int checkvalue = 0;
            if (CheckBox1.Checked == true)
            {
                checkvalue = 1;
            }

            Context.Items["checkvalue"] = checkvalue;
            //儲存ckedit內容
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/overview.txt", content.Text);
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/layout.txt", content1.Text);
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/specification.txt", content2.Text);
            Server.Transfer("bank_imgpage.aspx", true);
        }

        protected void tab5a_OnClick(object sender, EventArgs e)
        {
            Context.Items["content"] = content.Text;
            Context.Items["content1"] = content1.Text;
            Context.Items["content2"] = content2.Text;
            int checkvalue = 0;
            if (CheckBox1.Checked == true)
            {
                checkvalue = 1;
            }
            Context.Items["checkvalue"] = checkvalue;
            //儲存ckedit內容
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/overview.txt", content.Text);
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/layout.txt", content1.Text);
            File.WriteAllText("C:/Users/user/source/repos/yacht/yacht/txt/specification.txt", content2.Text);
            Server.Transfer("bank_filepage.aspx", true);
        }
    }
}