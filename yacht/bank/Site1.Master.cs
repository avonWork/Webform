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

namespace yacht
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string authority;

        protected void Page_Init(object sender, EventArgs e)
        {
            //確認使用者是否有驗證票，現在是否登入，假如沒有就跳回Login頁面。
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("../signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //要取得驗證票內所存的使用者的資料，先將UserData反序列化成物件才能控制
                //取得UserData
                string strUserData =
                    ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                //反序列化為物件，其名為Myperson
                userinformation Myperson =
                    JsonConvert.DeserializeObject<userinformation>(strUserData);
                //取得票卷上的username

                //取得UserData內的使用者資訊
                Label1.Text = Myperson.name;
                Label2.Text = Myperson.name;
                Label1.Text = Myperson.name;
                Image1.ImageUrl = Myperson.photo;
                Image2.ImageUrl = Myperson.photo;
                authority = Myperson.permission;
                HyperLink1.NavigateUrl = "bank_editAdmin.aspx?id=" + Myperson.id;
                HyperLink2.NavigateUrl = "bank_changePwd.aspx?id=" + Myperson.id;
                HyperLink3.NavigateUrl = "bank_editAdmin.aspx?id=" + Myperson.id;
                HyperLink4.NavigateUrl = "bank_changePwd.aspx?id=" + Myperson.id;
                HyperLink5.NavigateUrl = "bank_editAdmin.aspx?id=" + Myperson.id;
                HyperLink6.NavigateUrl = "bank_changePwd.aspx?id=" + Myperson.id;
                showList();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            //登出表單驗證票卷
            FormsAuthentication.SignOut();
            Response.Redirect("../signin.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            //登出表單驗證票卷
            FormsAuthentication.SignOut();
            Response.Redirect("../signin.aspx");
        }

        private void showList()
        {
            DBhelper dBhelper = new DBhelper();
            DataTable table = dBhelper.Getauthority();
            string str = "";
            string[] dtext = null;
            string[] durl = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string detailtext = table.Rows[i]["detailtext"].ToString();
                dtext = detailtext.Split(',');
                string detailurl = table.Rows[i]["detailurl"].ToString();
                durl = detailurl.Split(',');

                if ((int.Parse(authority) & Convert.ToInt16(table.Rows[i]["score"])) > 0)
                {
                    if (dtext.Length == 1)
                    {
                        str += $@"<li class='nav-item'><a href ='{table.Rows[i]["url"]}' class='nav-link '><span class='pcoded-micon'>{table.Rows[i]["icon"]}</span><span class='pcoded-mtext'>{table.Rows[i]["heading"]}</span></a></li>";
                    }
                    else
                    {
                        str += $@"<li class='nav-item pcoded-hasmenu'>
                        <a href='#!' class='nav-link '><span class='pcoded-micon'><i class='feather icon-box'></i></span><span class='pcoded-mtext'>{table.Rows[i]["heading"]}</span></a>
                        <ul class='pcoded-submenu'>";
                        for (int j = 0; j < dtext.Length; j++)
                        {
                            str += $@"<li><a href = '{durl[j]}'>{dtext[j]}</a></li>";
                        }
                        str += "</ul>";
                    }
                }
            }
            Literal1.Text = HttpUtility.HtmlDecode(str);
        }
    }
}