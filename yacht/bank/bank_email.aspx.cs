using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showdata();
        }

        private void showdata()
        {
            //sql語法 把留言板秀出
            string sql = "SELECT  a.country, b.* FROM addcountry as a INNER JOIN email as b ON a.id = b.id";
            DataTable table = DBhelper.GetDataTable(sql);
            GridView1.DataSource = table;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();//取得點擊這列的id
            SqlParameter[] paras2 = new SqlParameter[]
           {
                new SqlParameter("@id", id)
           };
            //sql語法
            string sql2 = "delete email where id=@id";
            DBhelper.ExecuteNonQuery(sql2, paras2);
            showdata();
        }

        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Look")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                modelid.Text = id.ToString();
                //參數化
                SqlParameter[] paras6 = new SqlParameter[]
                 {
                    new SqlParameter("@id", id)
                 };
                //sql
                string sql6 = "select * from email where id=@id";

                SqlDataReader reader = DBhelper.ExecuteRreader(sql6, paras6);
                if (reader.Read())
                {
                    //TextBox
                    username.Text = reader["ename"].ToString();
                    email.Text = reader["eemail"].ToString();
                }
            }
        }

        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("LinkButton2");
                if (!IsPostBack)
                {
                }
                else
                {
                    lnk.Attributes["onclick"] = "JavaScript:openModal()";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }
    }
}