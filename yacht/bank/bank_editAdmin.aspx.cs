using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class WebForm23 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            int Authoritynum = 0;
            if (!IsPostBack)
            {
                //宣告id變數(抓數據庫id)
                string id = Request.QueryString["id"];
                SqlDataReader reader = dBhelper.SelectAdminbyid(id);
                if (reader.Read())
                {
                    UserId.Text = reader["loginId"].ToString();
                    Name.Text = reader["adminName"].ToString();
                    //Image1.ImageUrl = "~/ckfinder/userfiles/images/" + reader["imgName"].ToString();
                    Authoritynum = int.Parse(reader["authority"].ToString()); //權限
                    HiddenField1.Value = reader["imgName"].ToString();
                }
                Literal1.Text = "個人資料設定";
                Literal2.Text = "個人資料設定";
                //驗證票主人權限
                string authority = HttpContext.Current.User.Identity.Name;
                //ReadOnly=True就不会回传
                //UserId.Attributes["readonly"] = "true";
                //如果有管理者權限
                if (Request.QueryString["admin"] == "1" && int.Parse(authority) == 15)
                {
                    Literal1.Text = "編輯管理者";
                    Literal2.Text = "編輯管理者";
                    adpwd.Style["Display"] = "Block"; //密碼顯示
                    cbauthority.Style["Display"] = "Block"; //權限顯示
                }
                DataTable table = dBhelper.Getauthority();
                int j = 0;
                //動態增加checkbox 文字
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    CheckBoxList1.Items.Add(new ListItem(table.Rows[i]["heading"].ToString()));
                }
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    for (int i = j; i < table.Rows.Count; i++)
                    {
                        if ((Authoritynum & Convert.ToInt16(table.Rows[i]["score"])) > 0)
                        {
                            item.Selected = true;
                            item.Text = table.Rows[i]["heading"].ToString();
                            j++;
                            break;
                        }
                        else
                        {
                            item.Selected = false;
                            j++;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 修改管理者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSignup_Click(object sender, EventArgs e)
        {
            //宣告id變數(抓數據庫id)
            string id = Request.QueryString["id"];
            //宣告輸入框變數
            string name = Name.Text;
            string pic = "";
            if (FileUpload1.HasFile)
            {
                //IMG上傳
                string path = Server.MapPath("~/ckfinder/userfiles/images/");   //-- 網站的URL路徑。
                string filetime = DateTime.Now.ToString("yyyyMMdd");
                string filename = filetime + FileUpload1.FileName;
                string imgPath = path + filename;
                FileUpload1.PostedFile.SaveAs(imgPath);
                pic = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
            }
            else
            {
                pic = HiddenField1.Value;
            }
            string Authority = ""; //權限
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
            DateTime editAdtime = DateTime.Now;
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "編輯管理者-編輯者: " + Myperson.id + " 姓名: " + Myperson.name;
            //如果有管理者權限
            if (Request.QueryString["admin"] == "1" && int.Parse(Myperson.permission) == 15)
            {
                //編輯管理者_密碼不為空
                if (!string.IsNullOrEmpty(Pwd.Text))
                {
                    string pwd = Pwd.Text;
                    dBhelper.UpdateAdminDataPwd(id, name, pic, Authoritynum, pwd, editAdtime, adMypersonId, dotStirng);
                    Response.Write("<script type='text/javascript'> alert('更新成功!回到管理者列表');location.href = 'bank_adminLIist.aspx';</script>");
                }
                //編輯管理者_密碼為空
                dBhelper.UpdateAdminData(id, name, pic, Authoritynum, editAdtime, adMypersonId, dotStirng);
                Response.Write("<script type='text/javascript'> alert('更新成功!回到管理者列表');location.href = 'bank_adminLIist.aspx';</script>");
            }
            else
            {
                //個人資料設定
                dBhelper.UpdateAdminData(id, name, pic, Authoritynum, editAdtime, adMypersonId, dotStirng);
                Response.Write("<script type='text/javascript'> alert('更新成功!請重新登入');location.href = '../signin.aspx';</script>");
            }
        }
    }
}