using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //sql語法
            string sql = "SELECT TOP 3 * FROM news order by sticky desc,date desc";
            DataTable table = DBhelper.GetDataTable(sql);
            Repeater1.DataSource = table;
            Repeater1.DataBind();

            //sql語法 圖片輪播秀出
            string sql2 = $"SELECT  yachts.id AS 遊艇id, yachts.newcheck, yachts.title, imgyacht.* FROM imgyacht INNER JOIN yachts ON imgyacht.yachtid = yachts.id WHERE imgyacht.imgorder = 1";
            DataTable table2 = DBhelper.GetDataTable(sql2);
            Repeater2.DataSource = table2;
            Repeater2.DataBind();
            //小圖
            string sql3 = $"SELECT  yachts.id AS 遊艇id, yachts.newcheck, yachts.title, imgyacht.* FROM imgyacht INNER JOIN yachts ON imgyacht.yachtid = yachts.id WHERE imgyacht.imgorder = 1";
            DataTable table3 = DBhelper.GetDataTable(sql3);
            string picstr = "<ul>";
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                if (table3.Rows[i]["imgname"].ToString() == "")
                {
                    picstr = "";
                }
                else
                {
                    picstr += $@"<li><div><p class='bannerimg_p'><img src='ckfinder/userfiles/moreimg/{table3.Rows[i]["imgname"]}' alt='{table3.Rows[i]["imgtext"]}'/></p></div></li>";
                }
            }
            picstr += "</ul>";
            Literal2.Text = HttpUtility.HtmlDecode(picstr);
        }
    }
}