using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm7 : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                //切換國家紀錄下拉選單
                if (Session["ddlcountry"] != null && Convert.ToInt16(Session["ddlcountry"]) > 0)
                {
                    //SelectedIndex是下拉選單第幾位(抓到值還要轉換成下拉選單的第幾位)
                    LoadCountry();
                    ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(Session["ddlcountry"].ToString()));
                    LoadArea();
                    //紀錄搜尋值
                    if (Session["searchAgent"] != null)
                    {
                        searchAgent.Text = Session["searchAgent"].ToString();
                    }
                    //紀錄地區選取
                    if (Session["ddlarea"] != null)
                    {
                        ddlarea.SelectedIndex = ddlarea.Items.IndexOf(ddlarea.Items.FindByValue(Session["ddlarea"].ToString()));
                    }
                    showPage();
                }
                //國家下拉選單回到第一個 地區初始化 搜尋值要記錄
                else if (Convert.ToInt16(Session["ddlcountry"]) == -1)
                {
                    LoadCountry();
                    ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(Session["ddlcountry"].ToString()));
                    LoadArea();
                    searchAgent.Text = Session["searchAgent"].ToString();
                    //error 晚點刪
                    //if (Session["ddlarea"] != null)
                    //{
                    //    ddlarea.SelectedIndex = ddlarea.Items.IndexOf(ddlarea.Items.FindByValue(Session["ddlarea"].ToString()));
                    //}

                    showall();
                }
                else //第一次加載
                {
                    LoadCountry();
                    showall();
                }
            }
        }

        /// <summary>
        /// 代理商列表(只對搜尋值含分頁)
        /// </summary>
        private void showall()
        {
            //search

            searchAgent.Text = (string)Session["searchAgent"];
            string newpage = Request.QueryString["page"];

            WebUserControl1.limit = 5;

            Repeater1.DataSource = dBhelper.SearchAgentlistpage(searchAgent.Text, newpage, WebUserControl1.limit);
            Repeater1.DataBind();

            int count = dBhelper.GetAgentCount(searchAgent.Text);
            WebUserControl1.totalitems = count;
            WebUserControl1.targetpage = "bank_agentList.aspx";
            WebUserControl1.showPageControls();
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

        /// <summary>
        /// 依據選取國家秀出地區下拉選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadArea();

            #region 保留

            //if (int.Parse(ddlcountry.SelectedValue) > 0) //切換國家清除地區資料重跑
            //{
            //    Session["ddlcountry"] = ddlcountry.SelectedValue;
            //    Session["ddlarea"] = "";
            //    //Response.Redirect("bank_agentList.aspx");
            //}
            //else
            //{
            //    //如果重選國家就初始化網頁(代理商清除)
            //    Session["ddlcountry"] = null;
            //    Session["ddlarea"] = null;
            //    //Response.Redirect("bank_agentList.aspx");
            //}

            #endregion 保留
        }

        /// <summary>
        /// 地區下拉選單
        /// </summary>
        private void LoadArea()
        {
            //session賦值
            string countryid = ddlcountry.SelectedValue;
            ddlarea.DataSource = dBhelper.GetAreabyCountryid(countryid);
            ddlarea.DataTextField = "area";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();
            //插入所有國家選單第一個位置
            ddlarea.Items.Insert(0, new ListItem("請選擇代理商的地區", "-1"));
        }

        /// <summary>
        /// 編輯查看刪除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发編輯点击事件
            {
                int agentID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                string page = Request.QueryString["page"];
                Response.Redirect("bank_editagent.aspx?id=" + agentID + "&page=" + page);
            }
            else if (e.CommandName == "Look") //触发查看点击事件
            {
                int agentID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                modelid.Text = agentID.ToString();
                Modalagent();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (e.CommandName == "Delete") //触发刪除点击事件
            {
                int agentID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(agentID);
                //重新綁定控件上的內容
                if (Session["ddlcountry"] == null)
                {
                    showall();
                }
                else
                {
                    showPage();
                }
            }
        }

        /// <summary>
        /// Modal事件--代理商資訊
        /// </summary>
        private void Modalagent()
        {
            string id = modelid.Text;
            SqlDataReader reader = dBhelper.SelectAgent(id);
            if (reader.Read())
            {
                //TextBox
                username.Text = reader["contact"].ToString();
                tel.Text = reader["tel"].ToString();
                email.Text = reader["email"].ToString();
                address.Text = reader["address"].ToString();
            }
        }

        /// <summary>
        /// 地區刪除事件
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            dBhelper.NoneDisplayAgent(intId);
        }

        /// <summary>
        /// 搜尋代理商按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            //儲存搜尋
            Session["ddlcountry"] = ddlcountry.SelectedValue;

            Session["searchAgent"] = (string)searchAgent.Text;
            //如果沒選地區防異常
            if (ddlarea.SelectedValue != "")
            {
                Session["ddlarea"] = ddlarea.SelectedValue;
            }

            //show
            if (Session["ddlcountry"] != null && Session["ddlarea"] == null)
            {
                Response.Redirect("bank_agentList.aspx");
                showall();
            }
            else
            {
                Response.Redirect("bank_agentList.aspx");
                showPage();
            }
        }

        //分頁
        private int page = 0;

        /// <summary>
        /// 代理商列表(所有搜尋含分頁)
        /// </summary>
        private void showPage()
        {
            //search
            int ddlcountry = Convert.ToInt16(Session["ddlcountry"]);
            int ddlarea = Convert.ToInt16(Session["ddlarea"]);
            string searchagent = (string)Session["searchAgent"];
            string newpage = Request.QueryString["page"];
            WebUserControl1.limit = 5;

            Repeater1.DataSource =
                dBhelper.SearchagentlistbyAreapage(ddlcountry, ddlarea, searchagent, newpage, WebUserControl1.limit);
            Repeater1.DataBind();

            var count = dBhelper.GetAgentCountbyArea(ddlcountry, ddlarea, searchagent);
            WebUserControl1.totalitems = count;
            WebUserControl1.targetpage = "bank_agentList.aspx";
            WebUserControl1.showPageControls();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ddlcountry.SelectedIndex = -1;
            Session["searchAgent"] = null;
            searchAgent.Text = "";
            Session["ddlcountry"] = null;
            Session["ddlarea"] = null;
            LoadArea();
            if (Session["ddlcountry"] == null)
            {
                showall();
            }
            else
            {
                showPage();
            }
        }
    }
}