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
    public partial class WebForm20 : System.Web.UI.Page
    {
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //驗證是否具有該權限 沒有回到首頁
                string authority = HttpContext.Current.User.Identity.Name;
                if ((int.Parse(authority) & 2) == 0)
                {
                    Response.Redirect("bank_home.aspx");
                }
                showeditFile();
            }
        }

        private void showeditFile()
        {
            //抓遊艇id(抓數據庫id)
            string id = Request.QueryString["id"];
            Repeater1.DataSource = dBhelper.SelectFileyacht(id);
            Repeater1.DataBind();
        }

        private int fileid = 0; //檔案id

        /// <summary>
        /// 地區編輯取消更新事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发点击事件
            {
                fileid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
            }
            else if (e.CommandName == "Cancel")
            {
                fileid = -1;
            }
            else if (e.CommandName == "Update")
            {
                fileid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                //string no = ((TextBox)this.Repeater1.Items[e.Item.ItemIndex].FindControl("FileNO")).Text.Trim();
                string text = ((TextBox)this.Repeater1.Items[e.Item.ItemIndex].FindControl("FileText")).Text.Trim();
                dBhelper.UpdateFileyacht(fileid, text);
                Response.Write("<script type='text/javascript'> alert('更新成功!');</script>");
                fileid = -1;
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int fileid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(fileid);
            }
            //重新綁定控件上的內容
            showeditFile();
        }

        /// <summary>
        /// 地區刪除事件
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            dBhelper.DeleteFileyacht(intId);
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
                int Id = int.Parse(record["id"].ToString());
                if (Id != fileid)
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
        /// 增加檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //檔案上傳
            string path = Server.MapPath("~/ckfinder/userfiles/Files/"); //-- 網站的URL路徑。
            string filetime = DateTime.Now.ToString("yyyyMMdd");
            string filename = filetime + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(path + filename); //上傳
            string filePath = path + filename;
            string file = filePath.Substring(filePath.LastIndexOf('\\') + 1); //檔名
            //抓遊艇id(抓數據庫id)
            string id = Request.QueryString["id"];
            //宣告輸入框變數
            string fileText = fileTXT.Text;
            dBhelper.InsertFileyacht(file, fileText, id);
            //顯示彈跳訊息
            Response.Write("<script type='text/javascript'> alert('新增檔案成功!');</script>");
            showeditFile();
        }

        ///
        protected void EditMode(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    (item.FindControl("TextNO") as Label).Visible = false;
                    (item.FindControl("FileNO") as TextBox).Visible = true;
                }
            }
        }

        /// <summary>
        /// 檔案排序更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUpdate(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                string fid = (item.FindControl("HiddenField1") as HiddenField).Value;
                if (ischecked)
                {
                    string no = (item.FindControl("FileNO") as TextBox).Text;
                    dBhelper.UpdatFileorder(no, fid);
                }
            }
            showeditFile();
        }

        protected void cancelMode(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    (item.FindControl("TextNO") as Label).Visible = true;
                    (item.FindControl("FileNO") as TextBox).Visible = false;
                }
            }
        }
    }
}