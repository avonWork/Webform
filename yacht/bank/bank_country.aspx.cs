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
    public partial class WebForm3 : System.Web.UI.Page
    {
        private int NewsID = 0; //抓取id
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
                ShowCountry();
            }
        }

        private void ShowCountry()
        {
            Repeater1.DataSource = dBhelper.SelectCountrylist();
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发点击事件
            {
                NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
            }
            else if (e.CommandName == "Cancel")
            {
                NewsID = -1;
            }
            else if (e.CommandName == "Update")
            {
                NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                string country = ((TextBox)this.Repeater1.Items[e.Item.ItemIndex].FindControl("updatecountry")).Text.Trim();
                dBhelper.UpdateCountry(NewsID, country);
                Response.Write("<script type='text/javascript'> alert('更新成功!');location.href = 'bank_country.aspx';</script>");
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int NewsID = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(NewsID);
            }
            ShowCountry();
        }

        /// <summary>
        /// 刪除行內容
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            //參數化
            dBhelper.DeleteCountry(intId);
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
                if (userId != NewsID)
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

        /// <summary>
        /// 新增國家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string addcountry = addCountry.Text;
            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "新增國家-" + Myperson.id + " 姓名: " + Myperson.name;
            //參數化
            dBhelper.InsertCountry(addcountry, adMypersonId, dotStirng);
            Response.Write("<script type='text/javascript'> alert('新增成功!');location.href = 'bank_country.aspx';</script>");
        }
    }
}