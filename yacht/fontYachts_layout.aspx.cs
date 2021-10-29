using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showall();
            }
        }

        private string id; //抓遊艇id
        private string titleid; //初始化遊艇(資料庫遊艇第0位)

        /// <summary>
        /// 判斷初始化id是否有值
        /// </summary>
        private void showall()
        {
            id = Request.QueryString["id"];
            if ((!string.IsNullOrEmpty(id)))
            {
                show();
            }
            else
            {
                id = titleid;
                show();
            }
        }

        /// <summary>
        /// 遊艇
        /// </summary>
        private void show()
        {
            //sql語法 秀遊艇
            string sql = "select * from yachts";
            DataTable table = DBhelper.GetDataTable(sql);
            titleid = table.Rows[0][0].ToString();
            Repeater1.DataSource = table;
            Repeater1.DataBind();

            //秀標題連結
            id = Request.QueryString["id"];
            if ((string.IsNullOrEmpty(id)))
            {
                id = titleid;
            }
            DataView dv = table.DefaultView;
            dv.RowFilter = $@"id={id}"; //where 條件
            foreach (DataRowView dataRowView in dv)
            {
                Label1.Text = dataRowView["title"].ToString();
                HyperLink1.Text = dataRowView["title"].ToString();
                Literal1.Text = dataRowView["layout"].ToString();
                HyperLink3.NavigateUrl = "fontYachts_overview.aspx?id=" + dataRowView["id"].ToString();
                HyperLink4.NavigateUrl = "fontYachts_layout.aspx?id=" + dataRowView["id"].ToString();
                HyperLink5.NavigateUrl = "fontYachts_specification.aspx?id=" + dataRowView["id"].ToString();
            }
            //sql語法 圖片輪播秀出
            string sql3 = $"select * from imgyacht where yachtid={id} order by imgorder";
            DataTable table3 = DBhelper.GetDataTable(sql3);
            string picstr = "<ul class='ad - thumb - list' style='width: 1570px;'>";
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                if (table3.Rows[i]["imgName"].ToString() == "")
                {
                    picstr = "";
                }
                else
                {
                    picstr += $@"<li><a href='ckfinder/userfiles/moreimg/{table3.Rows[i]["imgName"]}'><img src='ckfinder/userfiles/small/s{table3.Rows[i]["imgName"]}' class='image0' /></a></li>";
                }
            }
            picstr += "</ul>";
            Literal2.Text = HttpUtility.HtmlDecode(picstr);
        }
    }
}