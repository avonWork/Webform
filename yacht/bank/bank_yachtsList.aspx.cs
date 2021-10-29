using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 2) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                showPage();
            }
        }

        private int msid = 0; //抓取id

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发点击事件
            {
                int yachtid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                Response.Redirect("bank_edityacht.aspx?id=" + yachtid);
            }
            else if (e.CommandName == "Delete")
            {
                msid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                modelid.Text = msid.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            //重新綁定控件上的內容
            showPage();
        }

        /// <summary>
        /// 刪除事件
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = modelid.Text;
            dBhelper.DeleteYachtFileImg(id);
            //重新綁定控件上的內容
            showPage();
        }

        /// <summary>
        /// 搜尋按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            //search
            Session["statrdate"] = statrdate.Text;
            Session["enddate"] = enddate.Text;
            Session["title"] = yachtitle.Text;
            Response.Redirect("bank_yachtsList.aspx");
        }

        /// <summary>
        /// 新聞列表(包含搜尋分頁)
        /// </summary>
        private void showPage()
        {
            statrdate.Text = (string)Session["statrdate"];
            enddate.Text = (string)Session["enddate"];
            yachtitle.Text = (string)Session["title"];
            string newpage = Request.QueryString["page"];
            WebUserControl1.limit = 5;
            //sql語法
            Repeater1.DataSource = dBhelper.SearchYachtlistpage(statrdate.Text, enddate.Text, yachtitle.Text, newpage, WebUserControl1.limit);
            Repeater1.DataBind();

            WebUserControl1.totalitems = dBhelper.SelectYachtCount(statrdate.Text, enddate.Text, yachtitle.Text);
            WebUserControl1.targetpage = "bank_yachtsList.aspx";
            WebUserControl1.showPageControls();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            statrdate.Text = "";
            enddate.Text = "";
            yachtitle.Text = "";
            Session["statrdate"] = "";
            Session["enddate"] = "";
            Session["title"] = "";
            //show
            showPage();
        }
    }
}