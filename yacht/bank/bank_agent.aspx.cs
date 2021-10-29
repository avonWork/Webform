using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //驗證是否具有該權限 沒有回到首頁
            string authority = HttpContext.Current.User.Identity.Name;
            if ((int.Parse(authority) & 1) == 0)
            {
                Response.Redirect("bank_home.aspx");
            }
            //只有第一次進入 IsPostBack=false 回發後IsPostBack=true
            if (!IsPostBack)
            {
                LoadCountry();
            }
        }

        /// <summary>
        /// 國家下拉選單
        /// </summary>
        private void LoadCountry()
        {
            //sql語法
            ddlcountry.DataSource = dBhelper.Getcountry();
            ddlcountry.DataTextField = "country";
            ddlcountry.DataValueField = "id";
            ddlcountry.DataBind();
            //插入所有國家選單第一個位置
            ddlcountry.Items.Insert(0, new ListItem("請選擇代理商的國家", "-1"));
        }

        /// <summary>
        /// 依據選取國家秀出地區下拉選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadArea();
        }

        private void LoadArea()
        {
            //取得下拉選單選取值
            string countryid = ddlcountry.SelectedValue;
            ddlarea.DataSource = dBhelper.GetAreabyCountryid(countryid);
            ddlarea.DataTextField = "area";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();
            //插入所有國家選單第一個位置
            ddlarea.Items.Insert(0, new ListItem("請選擇要新增代理商的地區", "-1"));
        }

        /// <summary>
        /// 新增代理商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlarea.SelectedIndex > 0)
            {
                //IMG上傳
                string path = Server.MapPath("~/ckfinder/userfiles/images/");   //-- 網站的URL路徑。
                string filetime = DateTime.Now.ToString("yyyyMMddhhmmss");
                string filename = filetime + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(path + filename);

                //宣告輸入框變數
                string agent = Agent.Text;
                string contact = Contact.Text;
                string tel = Tel.Text;
                string fax = Fax.Text;
                string address = Address.Text;
                string email = Email.Text;
                string areaid = ddlarea.SelectedValue; //地區的id
                string imgPath = path + filename;
                string pic = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);

                //取得UserData
                string strUserData =
                    ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                //反序列化為物件，其名為Myperson
                userinformation Myperson =
                    JsonConvert.DeserializeObject<userinformation>(strUserData);
                //取得UserData內的使用者資訊
                int adMypersonId = Myperson.id;
                string dotStirng = "新增代理商-新增者: " + Myperson.id + " 姓名: " + Myperson.name;
                //email格式檢查
                if (getemail(email))
                {
                    Label2.Visible = false;
                    dBhelper.InsertAgent(agent, contact, tel, fax, address, email, pic, areaid, adMypersonId, dotStirng);
                    //顯示彈跳訊息
                    Response.Write("<script type='text/javascript'> alert('新增代理商成功!回到代理商列表');location.href = 'bank_agentList.aspx';</script>");
                }
                else
                {
                    Label2.Visible = true;
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('無法新增代理商~請先新增地區!');</script>");
            }
        }

        //email格式檢查
        private static bool getemail(string s)
        {
            //1@y.a
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool result = Regex.IsMatch(s, pattern);
            return result;
        }
    }
}