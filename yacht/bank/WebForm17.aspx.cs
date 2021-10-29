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
    public partial class WebForm17 : System.Web.UI.Page
    {
        public object Repeater1 { get; private set; }

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
            gvCustomers.DataSource = table;
            gvCustomers.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCountries = (e.Row.FindControl("ddlCountries") as DropDownList);
                string sql = "select * from fileyacht  order by fileorder";
                DataTable table = DBhelper.GetDataTable(sql);
                ddlCountries.DataSource = table;
                ddlCountries.DataTextField = "fileName";
                ddlCountries.DataValueField = "fileName";
                ddlCountries.DataBind();

                string country = (e.Row.FindControl("lblCountry") as Label).Text;
                ddlCountries.Items.FindByValue(country).Selected = true;
            }
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            bool isUpdateVisible = false;
            CheckBox chk = (sender as CheckBox);
            if (chk.ID == "chkAll")
            {
                foreach (GridViewRow row in gvCustomers.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked = chk.Checked;
                    }
                }
            }
            CheckBox chkAll = (gvCustomers.HeaderRow.FindControl("chkAll") as CheckBox);
            chkAll.Checked = true;
            foreach (GridViewRow row in gvCustomers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    for (int i = 1; i < row.Cells.Count; i++)
                    {
                        row.Cells[i].Controls.OfType<Label>().FirstOrDefault().Visible = !isChecked;
                        if (row.Cells[i].Controls.OfType<TextBox>().ToList().Count > 0)
                        {
                            row.Cells[i].Controls.OfType<TextBox>().FirstOrDefault().Visible = isChecked;
                        }
                        if (row.Cells[i].Controls.OfType<DropDownList>().ToList().Count > 0)
                        {
                            row.Cells[i].Controls.OfType<DropDownList>().FirstOrDefault().Visible = isChecked;
                        }
                        if (isChecked && !isUpdateVisible)
                        {
                            isUpdateVisible = true;
                        }
                        if (!isChecked)
                        {
                            chkAll.Checked = false;
                        }
                    }
                }
            }
            //btnUpdate.Visible = isUpdateVisible;
        }
    }
}