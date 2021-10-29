using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace yacht.bank
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            string authority = HttpContext.Current.User.Identity.Name;
            if ((int.Parse(authority) & 4) == 0)
            {
                Response.Redirect("bank_home.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //IMG上傳
            string path = Server.MapPath("~/ckfinder/userfiles/images/");   //-- 網站的URL路徑。
            string filetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string filename = filetime + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(path + filename);

            //宣告輸入框變數
            string new_date = date.Text;
            string new_title = title.Text;
            string new_detail = detail.Text;
            string imgPath = path + filename;
            string pic = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
            string new_content = content.Text;
            int checkvalue = 0;
            //如果
            if (CheckBox1.Checked == true)
            {
                checkvalue = 1;
                dBhelper.UpdateNewTop();
            }
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "新增新聞-新增者: " + Myperson.id + " 姓名: " + Myperson.name;
            dBhelper.InsertNew(new_date, new_title, new_detail, pic, new_content, checkvalue, adMypersonId, dotStirng);
            //顯示彈跳訊息
            Response.Write("<script type='text/javascript'> alert('新增文章成功!回到新聞列表');</script>");
            Response.Redirect("bank_newsList.aspx");
        }
    }
}