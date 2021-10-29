using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace yacht
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (IsPostBack)
                    Label1.Text = HttpUtility.HtmlEncode(Request.Form["TextBox1"].ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = HttpUtility.HtmlEncode(TextBox1.Text);
        }
    }
}