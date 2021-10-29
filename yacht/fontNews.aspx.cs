using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showData();
        }

        /// <summary>
        /// 分頁
        /// </summary>
        private void showData()
        {
            int page = 0;
            if (string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            WebUserControl1.limit = 5;

            var floor = (page - 1) * WebUserControl1.limit + 1;
            var ceiling = page * WebUserControl1.limit;

            string sql4 = $@" with cteTable as(
                  select ROW_NUMBER() over(order by date desc) as rowIndex, * from news
                  )
                  select* from cteTable where rowIndex >={floor} and rowIndex <= {ceiling };";

            //sql語法 把新聞資料秀出
            DataTable table = DBhelper.GetDataTable(sql4);
            Repeater1.DataSource = table;
            Repeater1.DataBind();

            string sql5 = " select count(*) from news";
            int count = (int)DBhelper.ExecuteScalar(sql5);

            WebUserControl1.totalitems = count;
            WebUserControl1.targetpage = "fontNews.aspx";
            WebUserControl1.showPageControls();
        }
    }
}