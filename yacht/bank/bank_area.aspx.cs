using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht.bank
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 1) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                LoadCountry();
            }
        }

        /// <summary>
        /// 國家下拉選單
        /// </summary>
        private void LoadCountry()
        {
            ddlcountry.DataSource = dBhelper.Getcountry();
            ddlcountry.DataTextField = "country";
            ddlcountry.DataValueField = "id";
            ddlcountry.DataBind();
            //插入所有國家選單第一個位置
            ddlcountry.Items.Insert(0, new ListItem("請選擇要新增地區的國家", "-1"));
        }

        /// <summary>
        /// 依據選取國家秀出地區表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //把國家 Session儲存起來
            Session["ddlcountry_area"] = ddlcountry.SelectedValue;
            ShowArealist();
        }

        private void ShowArealist()
        {
            //session賦值
            string countryid = (string)Session["ddlcountry_area"];
            Repeater1.DataSource = dBhelper.SelectAreaList(countryid);
            Repeater1.DataBind();
        }

        /// <summary>
        /// 新增地區
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //session賦值
            string countryid = (string)Session["ddlcountry_area"];
            string area = addArea.Text;
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "新增地區-" + Myperson.id + " 姓名: " + Myperson.name;
            dBhelper.InsertArea(area, countryid, adMypersonId, dotStirng);
            Response.Write("<script type='text/javascript'> alert('新增成功!');</script>");
            addArea.Text = "";
            ShowArealist();
        }

        private int areaid = 0; //抓取id

        /// <summary>
        /// 地區編輯取消更新事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发点击事件
            {
                areaid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
            }
            else if (e.CommandName == "Cancel")
            {
                areaid = -1;
            }
            else if (e.CommandName == "Update")
            {
                areaid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                string area = ((TextBox)this.Repeater1.Items[e.Item.ItemIndex].FindControl("updatearea")).Text.Trim();
                dBhelper.UpdateArea(areaid, area);
                Response.Write("<script type='text/javascript'> alert('更新成功!');</script>");
                areaid = -1;
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int areaid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(areaid);
            }
            //重新綁定控件上的內容
            string countryid = (string)Session["ddlcountry_area"];
            ShowArealist();
        }

        /// <summary>
        /// 地區刪除事件
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            dBhelper.DeleteArea(intId);
        }

        /// <summary>
        /// 切換模式--編輯轉更新
        /// </summary>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //獲取綁定的數據源，這裏要註意上面使用sqlReader的方法來綁定數據源，所以下面使用的DbDataRecord方法獲取的
                //如果綁定數據源是DataTable類型的使用下面的語句就會報錯.
                //System.Data.Common.DbDataRecord record = (System.Data.Common.DbDataRecord)e.Item.DataItem;

                //DataTable類型的數據源驗證方式
                System.Data.DataRowView record = (DataRowView)e.Item.DataItem;
                int userId = int.Parse(record["id"].ToString());
                if (userId != areaid)
                {
                    ((Panel)e.Item.FindControl("plItem")).Visible = true;
                    ((Panel)e.Item.FindControl("plEdit")).Visible = false;
                }
                else
                {
                    ((Panel)e.Item.FindControl("plItem")).Visible = false;
                    ((Panel)e.Item.FindControl("plEdit")).Visible = true;
                }
            }
        }
    }
}