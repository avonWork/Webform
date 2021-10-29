using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //宣告id變數(抓數據庫id)
            string id = Request.QueryString["id"];
            //參數化
            SqlParameter[] paras = new SqlParameter[]
           {
                new SqlParameter("@id",id)
           };
            //sql語法 把新聞內容秀出一條
            string sql = "select * from news where (id=@id)";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql, paras);
            if (reader.Read())
            {
                Label1.Text = reader["new_content"].ToString();
                Label2.Text = reader["title"].ToString();
            }
        }
    }
}