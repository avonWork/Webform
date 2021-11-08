using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();
        //private string ddlcountrylist = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 1) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }

                showagent();
            }
        }

        /// <summary>
        /// 代理商資料
        /// </summary>
        private void showagent()
        {
            //宣告代理商id變數(抓數據庫id)
            string id = Request.QueryString["id"];
            SqlDataReader reader = dBhelper.ShowSelectAgent(id);
            if (reader.Read())
            {
                #region 保留

                //dropDownList
                //ddlcountry.Items.Add(new ListItem() { Text = reader["國家"].ToString(), Value = reader["國家id"].ToString()});
                //ddlarea.Items.Add(new ListItem() { Text = reader["地區"].ToString(), Value = reader["地區id"].ToString() });
                //ddlcountry.Items.Add(new ListItem(reader["國家"].ToString(), reader["國家id"].ToString()));
                //ddlarea.Items.Add(new ListItem(reader["地區"].ToString(), reader["地區id"].ToString()));

                #endregion 保留

                LoadCountry();
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(reader["國家id"].ToString()));

                LoadArea();
                ddlarea.SelectedIndex = ddlarea.Items.IndexOf(ddlarea.Items.FindByValue(reader["地區id"].ToString()));
                //TextBox
                editAgent.Text = reader["agent"].ToString();
                editContact.Text = reader["contact"].ToString();
                editTel.Text = reader["tel"].ToString();
                editFax.Text = reader["fax"].ToString();
                editAddress.Text = reader["address"].ToString();
                editEmail.Text = reader["email"].ToString();
                HiddenField1.Value = reader["img"].ToString();
            }
        }

        /// <summary>
        /// 取消回到上一頁(保留上一頁動作)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            string page = Request.QueryString["page"];

            if ((string)Session["ddlcountry"] == null)
            {
                Session["ddlcountry"] = null;
                Session["ddlarea"] = null;
                Response.Redirect("bank_agentList.aspx?page=" + page);
            }
            else
            {
                Response.Redirect("bank_agentList.aspx?page=" + page);
            }
        }

        /// <summary>
        /// 國家下拉選單
        /// </summary>
        private void LoadCountry()
        {
            ddlcountry.DataSource = dBhelper.Getcountry();
            ddlcountry.DataTextField = "country";
            ddlcountry.DataValueField = "id";
            ddlcountry.DataBind();
            //插入所有國家選單第一個位置
            ddlcountry.Items.Insert(0, new ListItem("請選擇代理商的國家", "-1"));
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //把國家 Session儲存起來
            Session["ddlcountry"] = ddlcountry.SelectedValue;
            LoadArea();
        }

        /// <summary>
        /// 地區下拉選單
        /// </summary>
        private void LoadArea()
        {
            //error 晚點刪
            //session賦值
            //string countryid = Session["ddlcountry"].ToString();

            string countryid = ddlcountry.SelectedValue;
            ddlarea.DataSource = dBhelper.GetAreabyCountryid(countryid);
            ddlarea.DataTextField = "area";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();
            //插入所有國家選單第一個位置
            ddlarea.Items.Insert(0, new ListItem("請選擇代理商的地區", "-1"));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddlarea.SelectedIndex > 0)
            {
                //宣告id變數(抓數據庫id)
                string id = Request.QueryString["id"];
                //地區下拉選單
                string areaid = ddlarea.SelectedValue; //地區的id
                //宣告輸入框變數
                string editagent = editAgent.Text;
                string editcontact = editContact.Text;
                string edittel = editTel.Text;
                string editfax = editFax.Text;
                string editaddress = editAddress.Text;
                string editemail = editEmail.Text;
                string pic = "";
                DateTime editAdtime = DateTime.Now;
                //取得UserData
                string strUserData =
                    ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                //反序列化為物件，其名為Myperson
                userinformation Myperson =
                    JsonConvert.DeserializeObject<userinformation>(strUserData);
                //取得UserData內的使用者資訊
                int adMypersonId = Myperson.id;
                string dotStirng = "編輯代理商-編輯者: " + Myperson.id + " 姓名: " + Myperson.name;

                if (FileUpload1.HasFile)
                {
                    //IMG上傳
                    string path = Server.MapPath("~/ckfinder/userfiles/images/"); //-- 網站的URL路徑。
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

                if (getemail(editemail))
                {
                    dBhelper.UpdateAgent(id, areaid, editagent, editcontact, edittel, pic, editfax, editaddress, editemail, editAdtime, adMypersonId, dotStirng);
                    string page = Request.QueryString["page"];
                    Session["ddlcountry"] = null;
                    //Session["ddlarea"] = null;
                    Response.Redirect("bank_agentList.aspx?page=" + page);
                }
                else
                {
                    Label2.Visible = true;
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'> alert('無法修改代理商~請先新增地區!');</script>");
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