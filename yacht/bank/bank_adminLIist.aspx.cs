using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm26 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 8) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                showData();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "comButton1") //触发点击事件
            {
                int NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                Response.Redirect("bank_editAdmin.aspx?id=" + NewsID + "&admin=" + 1);
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(NewsID);
            }
            //重新綁定控件上的內容
            showData();
        }

        /// <summary>
        /// 刪除行內容
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            #region 保留

            // //參數化
            // SqlParameter[] paras = new SqlParameter[]
            //{
            //     new SqlParameter("@id", intId)
            //};
            // //sql語法
            // string sql2 = "delete addadmin where id=@id";
            // DBhelper.ExecuteNonQuery(sql2, paras);

            #endregion 保留

            dBhelper.deleteAdmin(intId);
        }

        private void showData()
        {
            #region 保留

            //int page = 0;
            //if (string.IsNullOrEmpty(Request.QueryString["page"]))
            //{
            //    page = 1;
            //}
            //else
            //{
            //    page = Convert.ToInt32(Request.QueryString["page"]);
            //}

            //WebUserControl1.limit = 3;

            //var floor = (page - 1) * WebUserControl1.limit + 1;
            //var ceiling = page * WebUserControl1.limit;

            //string sql = $@" with cteTable as(
            //      select ROW_NUMBER() over(order by id) as rowIndex,* from addadmin where 1=1
            //      )
            //      select* from cteTable where rowIndex >={floor} and rowIndex <= {ceiling}";

            ////sql語法 把留言板秀出
            //DataTable table = DBhelper.GetDataTable(sql);

            #endregion 保留

            string newpage = Request.QueryString["page"];
            WebUserControl1.limit = 5;
            DataTable table = dBhelper.GetauthorityListpage(newpage, WebUserControl1.limit);
            Repeater1.DataSource = table;
            Repeater1.DataBind();

            string str = "";
            foreach (RepeaterItem item in Repeater1.Items)
            {
                Literal Literal1 = (Literal)item.FindControl("Literal1");
                if ((int.Parse(Literal1.Text) & 1) > 0)
                {
                    str = "<span class='badge badge-danger'>代理商列表</span> ";
                }
                if ((int.Parse(Literal1.Text) & 2) > 0)
                {
                    str += "<span class='badge badge-warning'>遊艇列表</span> ";
                }
                if ((int.Parse(Literal1.Text) & 4) > 0)
                {
                    str += "<span class='badge badge-success'>新聞列表</span> ";
                }
                if ((int.Parse(Literal1.Text) & 8) > 0)
                {
                    str += "<span class='badge badge-secondary'>管理者列表</span>";
                }
                (item.FindControl("Literal1") as Literal).Text = HttpUtility.HtmlDecode(str);
                str = "";
            }

            int count = dBhelper.getadminCount();
            WebUserControl1.totalitems = count;
            WebUserControl1.targetpage = "bank_adminLIist.aspx";
            WebUserControl1.showPageControls();
        }
    }
}