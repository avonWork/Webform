using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace yacht.bank
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        public string yachttitle; //遊艇標題
        public string yachtid; //遊艇預計新增id
        private DBhelper dBhelper = new DBhelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //驗證是否具有該權限 沒有回到首頁
            string authority = HttpContext.Current.User.Identity.Name;
            if ((int.Parse(authority) & 2) == 0)
            {
                Response.Redirect("bank_home.aspx");
            }
            yachttitle = Session["yachtTitle"].ToString();
            yachtid = Session["yachtID"].ToString();
            if (!IsPostBack)
            {
                showImages();
                Overview.Value = Context.Items["content"].ToString();
                Layout.Value = Context.Items["content1"].ToString();
                Specification.Value = Context.Items["content2"].ToString();
                checkvalue.Value = Context.Items["checkvalue"].ToString();
            }
        }

        private void showImages()
        {
            Repeater1.DataSource = dBhelper.SelectImgyacht(yachtid);
            Repeater1.DataBind();
        }

        private int imgid = 0; //圖片id

        /// <summary>
        /// 地區編輯取消更新事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit") //触发点击事件
            {
                imgid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
            }
            else if (e.CommandName == "Cancel")
            {
                imgid = -1;
            }
            else if (e.CommandName == "Update")
            {
                imgid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                string text = ((TextBox)this.Repeater1.Items[e.Item.ItemIndex].FindControl("ImgText")).Text.Trim();
                dBhelper.UpdateImgyacht(imgid, text);
                Response.Write("<script type='text/javascript'> alert('更新成功!');</script>");
                imgid = -1;
            }
            else if (e.CommandName == "Delete") //触发点击事件
            {
                int fileid = int.Parse(e.CommandArgument.ToString()); //获取回发的值
                this.DeleteRepeater(fileid);
            }
            //重新綁定控件上的內容
            showImages();
        }

        /// <summary>
        /// 地區刪除事件
        /// </summary>
        /// <param name="intId">刪除行所在內容的ID</param>
        private void DeleteRepeater(int intId)
        {
            dBhelper.DeleteImgyacht(intId);
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
                if (Id != imgid)
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
        /// 增加圖片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //檔案上傳
            string path = Server.MapPath("~/ckfinder/userfiles/moreimg/");   //-- 網站的URL路徑。
            string imgtime = DateTime.Now.ToString("yyyyMMdd");
            string imgname = imgtime + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(path + imgname);  //上傳
            string imgPath = path + imgname;
            string img = imgPath.Substring(imgPath.LastIndexOf('\\') + 1); //檔名
            GenerateThumbnailImage(img, path, "C:/Users/user/source/repos/yacht/yacht/ckfinder/userfiles/small/", "s", 59);
            //宣告輸入框變數
            string imgText = ImgTXT.Text;
            dBhelper.InsertImgyacht(img, imgText, yachtid);
            //顯示彈跳訊息
            Response.Write("<script type='text/javascript'> alert('新增圖片成功!');</script>");
            showImages();
        }

        ///
        protected void EditMode(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    (item.FindControl("imgTextNO") as Label).Visible = false;
                    (item.FindControl("imgNO") as TextBox).Visible = true;
                }
            }
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                string fid = (item.FindControl("HiddenField1") as HiddenField).Value;
                if (ischecked)
                {
                    string no = (item.FindControl("imgNO") as TextBox).Text;
                    dBhelper.UpdateImgorder(no, fid);
                }
            }
            showImages();
        }

        protected void cancelMode(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                bool ischecked = (item.FindControl("Chkbulk") as CheckBox).Checked;
                if (ischecked)
                {
                    (item.FindControl("imgTextNO") as Label).Visible = true;
                    (item.FindControl("imgNO") as TextBox).Visible = false;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //宣告輸入框變數
            string Overview = this.Overview.Value;
            string Layout = this.Layout.Value;
            string Specification = this.Specification.Value;
            string checkvalue = this.checkvalue.Value;

            //取得UserData
            string strUserData =
                ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            //反序列化為物件，其名為Myperson
            userinformation Myperson =
                JsonConvert.DeserializeObject<userinformation>(strUserData);
            //取得UserData內的使用者資訊
            int adMypersonId = Myperson.id;
            string dotStirng = "新增遊艇-創建者: " + Myperson.id + " 姓名: " + Myperson.name;
            try
            {
                dBhelper.InsertYacht(Overview, Layout, Specification, yachttitle, checkvalue, adMypersonId, dotStirng);
                //顯示彈跳訊息
                Response.Write("<script type='text/javascript'> alert('新增遊艇成功!回到遊艇列表');</script>");
                Response.Redirect("bank_yachtsList.aspx");
            }
            catch
            {
                Response.Write("<script type='text/javascript'> alert('遊艇標題重複!請更改標題~');</script>");
            }
        }

        protected void tab5a_OnClick(object sender, EventArgs e)
        {
            Context.Items["content"] = this.Overview.Value;
            Context.Items["content1"] = this.Layout.Value;
            Context.Items["content2"] = this.Specification.Value;
            Context.Items["checkvalue"] = this.checkvalue.Value;
            Server.Transfer("bank_filepage.aspx", true);
        }

        #region "舉世無敵縮圖程式"

        /// <summary>
        /// 舉世無敵縮圖程式(多載)
        /// 1.會自動判斷是比較高還是比較寬，以比較大的那一方決定要縮的尺寸
        /// 2.指定寬度，等比例縮小
        /// 3.指定高度，等比例縮小
        /// </summary>
        /// <param name="name">原檔檔名</param>
        /// <param name="source">來源路徑</param>
        /// <param name="target">目的路徑</param>
        /// <param name="suffix">縮圖辯識符號</param>
        /// <param name="maxWidth">指定要縮的寬度</param>
        /// <param name="maxHeight">指定要縮的高度</param>
        /// <remarks></remarks>
        public static void GenerateThumbnailImage(string name, string source, string target, string suffix, int maxWidth, int maxHeight)
        {
            System.Drawing.Image baseImage = System.Drawing.Image.FromFile(source + "\\" + name);
            var ratio = 0.0F; //存放縮圖比例
            float h = baseImage.Height;//圖像原尺寸高度
            float w = baseImage.Width;//圖像原尺寸寬度
            int ht;//圖像縮圖後高度
            int wt; //圖像縮圖後寬度
            if (w > h)
            {//圖像比較寬
                ratio = maxWidth / w;//計算寬度縮圖比例
                if (maxWidth < w)
                {
                    ht = Convert.ToInt32(ratio * h);
                    wt = maxWidth;
                }
                else
                {
                    ht = Convert.ToInt32(baseImage.Height);
                    wt = Convert.ToInt32(baseImage.Width);
                }
            }
            else
            {//比較高
                ratio = maxHeight / h;//計算寬度縮圖比例
                if (maxHeight < h)
                {
                    ht = maxHeight;
                    wt = Convert.ToInt32(ratio * w);
                }
                else
                {
                    ht = Convert.ToInt32(baseImage.Height);
                    wt = Convert.ToInt32(baseImage.Width);
                }
            }
            var filename = target + "\\" + suffix + name;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(wt, ht);
            System.Drawing.Graphics graphic = Graphics.FromImage(img);
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.DrawImage(baseImage, 0, 0, wt, ht);
            img.Save(filename);
            img.Dispose();
            graphic.Dispose();
            baseImage.Dispose();
        }

        /// <summary>
        /// 舉世無敵縮圖程式(多載)
        /// 1.會自動判斷是比較高還是比較寬，以比較大的那一方決定要縮的尺寸
        /// 2.指定寬度，等比例縮小
        /// 3.指定高度，等比例縮小
        /// </summary>
        /// <param name="name">原檔檔名</param>
        /// <param name="source">來源檔案的Stream,可接受上傳檔案</param>
        /// <param name="target">目的路徑</param>
        /// <param name="suffix">縮圖辯識符號</param>
        /// <param name="maxWidth">指定要縮的寬度</param>
        /// <param name="maxHeight">指定要縮的高度</param>
        /// <remarks></remarks>
        public static void GenerateThumbnailImage(string name, System.IO.Stream source, string target, string suffix, int maxWidth, int maxHeight)
        {
            System.Drawing.Image baseImage = System.Drawing.Image.FromStream(source);
            var ratio = 0.0F; //存放縮圖比例
            float h = baseImage.Height; //圖像原尺寸高度
            float w = baseImage.Width;  //圖像原尺寸寬度
            int ht; //圖像縮圖後高度
            int wt;//圖像縮圖後寬度
            if (w > h)
            {
                ratio = maxWidth / w; //計算寬度縮圖比例
                if (maxWidth < w)
                {
                    ht = Convert.ToInt32(ratio * h);
                    wt = maxWidth;
                }
                else
                {
                    ht = Convert.ToInt32(baseImage.Height);
                    wt = Convert.ToInt32(baseImage.Width);
                }
            }
            else
            {
                ratio = maxHeight / h; //計算寬度縮圖比例
                if (maxHeight < h)
                {
                    ht = maxHeight;
                    wt = Convert.ToInt32(ratio * w);
                }
                else
                {
                    ht = Convert.ToInt32(baseImage.Height);
                    wt = Convert.ToInt32(baseImage.Width);
                }
            }
            var filename = target + "\\" + suffix + name;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(wt, ht);
            System.Drawing.Graphics graphic = Graphics.FromImage(img);
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.DrawImage(baseImage, 0, 0, wt, ht);
            img.Save(filename);
            img.Dispose();
            graphic.Dispose();
            baseImage.Dispose();
        }

        /// <summary>
        /// 舉世無敵縮圖程式(指定寬度，等比例縮小)
        /// </summary>
        /// <param name="name">原檔檔名</param>
        /// <param name="source">來源路徑</param>
        /// <param name="target">目的路徑</param>
        /// <param name="suffix">縮圖辯識符號</param>
        /// <param name="maxWidth">指定要縮的寬度</param>
        /// <remarks></remarks>
        public static void GenerateThumbnailImage(int maxWidth, string name, string source, string target, string suffix)
        {
            System.Drawing.Image baseImage = System.Drawing.Image.FromFile(source + "\\" + name);
            var ratio = 0.0F; //存放縮圖比例
            float h = baseImage.Height; //圖像原尺寸高度
            float w = baseImage.Width; //圖像原尺寸寬度
            int ht; //圖像縮圖後高度
            int wt; //圖像縮圖後寬度
            ratio = maxWidth / w;//計算寬度縮圖比例
            if (maxWidth < w)
            {
                ht = Convert.ToInt32(ratio * h);
                wt = maxWidth;
            }
            else
            {
                ht = Convert.ToInt32(baseImage.Height);
                wt = Convert.ToInt32(baseImage.Width);
            }
            var filename = target + "\\" + suffix + name;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(wt, ht);
            System.Drawing.Graphics graphic = Graphics.FromImage(img);
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.DrawImage(baseImage, 0, 0, wt, ht);
            img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            img.Dispose();
            graphic.Dispose();
            baseImage.Dispose();
        }

        /// <summary>
        /// 舉世無敵縮圖程式(指定高度，等比例縮小)
        /// </summary>
        /// <param name="name">原檔檔名</param>
        /// <param name="source">來源路徑</param>
        /// <param name="target">目的路徑</param>
        /// <param name="suffix">縮圖辯識符號</param>
        /// <param name="maxHeight">指定要縮的高度</param>
        /// <remarks></remarks>
        public static void GenerateThumbnailImage(string name, string source, string target, string suffix, int maxHeight)
        {
            System.Drawing.Image baseImage = System.Drawing.Image.FromFile(source + "\\" + name);
            var ratio = 0.0F;//存放縮圖比例
            float h = baseImage.Height; //圖像原尺寸高度
            float w = baseImage.Width;  //圖像原尺寸寬度
            int ht; //圖像縮圖後高度
            int wt; //圖像縮圖後寬度
            ratio = maxHeight / h; //計算寬度縮圖比例
            if (maxHeight < h)
            {
                ht = maxHeight;
                wt = Convert.ToInt32(ratio * w);
            }
            else
            {
                ht = Convert.ToInt32(baseImage.Height);
                wt = Convert.ToInt32(baseImage.Width);
            }
            var filename = target + "\\" + suffix + name;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(wt, ht);
            System.Drawing.Graphics graphic = Graphics.FromImage(img);
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphic.DrawImage(baseImage, 0, 0, wt, ht);
            img.Save(filename);
            img.Dispose();
            graphic.Dispose();
            baseImage.Dispose();
        }

        #endregion "舉世無敵縮圖程式"
    }
}