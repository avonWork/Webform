using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;

namespace yacht.bank
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 4) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                //宣告id變數(抓數據庫id)
                string id = Request.QueryString["id"];
                SqlDataReader reader = dBhelper.SelectNewbyid(id);
                if (reader.Read())
                {
                    CheckBox2.Checked = (bool)reader["sticky"];
                    editDate.Text = DateTime.Parse(reader["date"].ToString()).ToString("yyyy-MM-dd");
                    editTitle.Text = reader["title"].ToString();
                    editDetail.Text = reader["detail"].ToString();
                    editContent.Text = reader["new_content"].ToString();
                    HiddenField1.Value = reader["img"].ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //宣告id變數(抓數據庫id)
            string id = Request.QueryString["id"];
            //宣告輸入框變數
            string editdate = editDate.Text;
            string edittitle = editTitle.Text;
            string editdetail = editDetail.Text;
            string editcontent = editContent.Text;
            string pic = "";
            if (FileUpload1.HasFile)
            {
                //IMG上傳
                string path = Server.MapPath("~/ckfinder/userfiles/images/");   //-- 網站的URL路徑。
                string filetime = DateTime.Now.ToString("yyyyMMddhhmmss");
                string filename = filetime + FileUpload1.FileName;
                string imgPath = path + filename;
                FileUpload1.PostedFile.SaveAs(imgPath);
                pic = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
            }
            else
            {
                pic = HiddenField1.Value;
            }
            int checkvalue = 0;

            //如果
            if (CheckBox2.Checked == true)
            {
                checkvalue = 1;
                dBhelper.UpdateNewTop();
            }
            dBhelper.UpdateNew(id, editdate, edittitle, editdetail, pic, editcontent, checkvalue);
            Response.Redirect("bank_newsList.aspx");
        }
    }
}