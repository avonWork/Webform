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
    public partial class WebForm18 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showfile();
            }
        }

        private void showfile()
        {
            //sql語法 把遊艇列表秀出
            string sql = $"select * from fileyacht  order by fileorder";
            // string sql = $@"SELECT  b.*, a.id AS 檔案id,a.* FROM fileyacht AS a INNER JOIN yachts AS b ON a.yachtTitle = b.title where b.id ='{yachtid}' order by a.fileorder";
            DataTable table = DBhelper.GetDataTable(sql);
            repCustomers.DataSource = table;
            repCustomers.DataBind();
        }

        protected void EditMode(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repCustomers.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    this.ToggleElements(item, true);
                }
            }
        }

        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            (item.FindControl("lblCountry") as Label).Visible = !isEdit;
            (item.FindControl("lblCustomerName") as Label).Visible = !isEdit;
            (item.FindControl("no") as TextBox).Visible = isEdit;
            (item.FindControl("name") as TextBox).Visible = isEdit;
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in repCustomers.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    string fileid = (item.FindControl("lblCustomerId") as Label).Text;
                    string no = (item.FindControl("no") as TextBox).Text;
                    string name = (item.FindControl("name") as TextBox).Text;

                    //參數化
                    SqlParameter[] paras4 = new SqlParameter[]
                   {
                new SqlParameter("@no", no),new SqlParameter("@name", name),new SqlParameter("@fileid", fileid)
                   };
                    //sql語法
                    string sql4 = "update fileyacht set fileText=@name,fileorder=@no where id=@fileid";
                    DBhelper.ExecuteNonQuery(sql4, paras4);
                }
            }
            showfile();
        }
    }
}