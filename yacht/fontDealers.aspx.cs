using System;
using System.Data;

namespace yacht
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showleft(); //左選單
                showright(); //右資料
                //showtitle(); //右標題
            }
        }

        /// <summary>
        /// 右標題連結
        /// </summary>
        //private void showtitle()
        //{
        //    //sql語法
        //    string sql = $@"select * from addcountry where id={id} and display=1";
        //    DataTable table = DBhelper.GetDataTable(sql);
        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        Label1.Text = table.Rows[i]["country"].ToString();
        //        HyperLink1.Text = table.Rows[i]["country"].ToString();
        //    }
        //}

        private string id; //抓國家id

        /// <summary>
        /// 右資料
        /// </summary>
        private void showright()
        {
            id = Request.QueryString["id"];
            if ((!string.IsNullOrEmpty(id)))
            {
                showAgent();
            }
            else
            {
                id = countryid;
                showAgent();
            }
        }

        /// <summary>
        ///地區&代理商
        /// </summary>
        private void showAgent()
        {
            //分頁
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
            //秀資料
            string sql = $@"with cteTable as(
                  select ROW_NUMBER() over(order by b.id) as rowIndex,c.id as 國家id, c.country as 國家, b.area as 地區, a.* FROM addagent as a INNER JOIN addarea as b ON a.areaid = b.id INNER JOIN addcountry as c ON b.countryid = c.id where c.id={id} and b.display = 1 and a.display=1
				)
                 select* from cteTable where rowIndex >={floor} and rowIndex <= {ceiling}";
            DataTable table = DBhelper.GetDataTable(sql);
            Repeater2.DataSource = table;
            Repeater2.DataBind();
            //總分頁數
            string sql5 = $@"SELECT count(*) FROM addagent as a INNER JOIN addarea as b ON a.areaid = b.id INNER JOIN addcountry as c ON b.countryid = c.id where c.id={id} and b.display = 1 and a.display=1";
            int count = (int)DBhelper.ExecuteScalar(sql5);
            WebUserControl1.totalitems = count;
            WebUserControl1.targetpage = "fontDealers.aspx";
            WebUserControl1.showPageControls();
        }

        private string countryid; //初始化國家(資料庫國家第0位)

        /// <summary>
        /// 國家左選單(包含右標題及連結)
        /// </summary>
        private void showleft()
        {
            //sql語法 秀國家選單
            string sql = "select * from addcountry where display=1";
            DataTable table = DBhelper.GetDataTable(sql);
            countryid = table.Rows[0][0].ToString();
            Repeater1.DataSource = table;
            Repeater1.DataBind();

            //秀標題連結
            id = Request.QueryString["id"];
            if ((string.IsNullOrEmpty(id)))
            {
                id = countryid;
            }
            DataView dv = table.DefaultView;
            dv.RowFilter = $@"id={id}"; //where 條件
            foreach (DataRowView dataRowView in dv)
            {
                Label1.Text = dataRowView["country"].ToString();
                HyperLink1.Text = dataRowView["country"].ToString();
            }
        }
    }
}