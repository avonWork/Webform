using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 4) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                //show
                showPage();
            }
        }

        /// <summary>
        /// 編輯刪除事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "comButton1") //触发点击事件
            {
                int NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                Response.Redirect("bank_edit.aspx?id=" + NewsID);
                //Response.Redirect("bank_new.aspx");
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(NewsID);
            }
            //重新綁定控件上的內容
            showPage();
        }

        /// <summary>
        /// 刪除行內容
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            dBhelper.DeleteNew(intId);
        }

        /// <summary>
        /// 搜尋按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //search
            Session["statrdate"] = statrdate.Text;
            Session["enddate"] = enddate.Text;
            Session["title"] = newtitle.Text;
            Response.Redirect("bank_newsList.aspx");
        }

        /// <summary>
        /// 新聞列表(包含搜尋分頁)
        /// </summary>
        private void showPage()
        {
            statrdate.Text = (string)Session["statrdate"];
            enddate.Text = (string)Session["enddate"];
            newtitle.Text = (string)Session["title"];
            string newpage = Request.QueryString["page"];
            WebUserControl1.limit = 5;
            Repeater1.DataSource = dBhelper.SearchNewlistpage(statrdate.Text, enddate.Text, newtitle.Text, newpage, WebUserControl1.limit);
            Repeater1.DataBind();

            WebUserControl1.totalitems = dBhelper.SelectNewCount(statrdate.Text, enddate.Text, newtitle.Text);
            WebUserControl1.targetpage = "bank_newsList.aspx";
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
            newtitle.Text = "";
            Session["statrdate"] = "";
            Session["enddate"] = "";
            Session["title"] = "";
            //show
            showPage();
        }
    }
}