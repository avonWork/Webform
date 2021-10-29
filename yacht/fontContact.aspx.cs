using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCountry();
            Loadyacht();
        }

        /// <summary>
        /// 遊艇下拉選單
        /// </summary>
        private void Loadyacht()
        {
            //sql語法
            string sql = "select * from yachts";
            DataTable table = DBhelper.GetDataTable(sql);
            fontyacht.DataSource = table;
            fontyacht.DataTextField = "title";
            fontyacht.DataValueField = "id";
            fontyacht.DataBind();
            //插入所有國家選單第一個位置
            fontyacht.Items.Insert(0, new ListItem("請選擇遊艇", ""));
        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //取國家下拉選單id(隱藏TextBox)
            string countryid = txt_Test.Text;
            fontcountry.SelectedIndex = fontcountry.Items.IndexOf(fontcountry.Items.FindByValue(countryid));
            //取遊艇下拉選單id(隱藏TextBox)
            string yachtid = txt2_Test.Text;
            fontyacht.SelectedIndex = fontyacht.Items.IndexOf(fontyacht.Items.FindByValue(yachtid));
            //email格式檢查
            if (getemail(email.Text))
            {
                Label2.Visible = false;
                //驗證碼
                if (TxtVCode.Text == (string)Session["CheckCode"])
                {
                    //新增email資料
                    insertEmail();
                    string js = @"swal('已成功寄出!', '請至您的信箱查收', 'success');";
                    ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), js, true);
                    //寄信給顧客
                    SendEmail(ReceiveEmail, ReceiveName + "你好:感謝您的留言~你索取的遊艇型號" + fontyacht.SelectedItem.Text + "會有專人盡快為您服務");
                    //寄信給管理者
                    SendEmail("avonworktest@gmail.com", "管理者你好:顧客索取的遊艇型號: " + fontyacht.SelectedItem.Text + "<br/>顧客姓名: " + ReceiveName + "<br/>顧客email: " + ReceiveEmail + "<br/>顧客留言內容: " + comments.Text);
                    Label1.Text = "";
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "*圖片驗證失敗!請重新輸入";
                }
            }
            else
            {
                Label2.Visible = true;
            }
        }

        private string ReceiveEmail;//收信者email
        private string ReceiveName;//收信者名字

        private void insertEmail()
        {
            //宣告輸入框變數
            ReceiveName = name.Text;
            ReceiveEmail = email.Text;

            string Phone = phone.Text;
            string Comments = comments.Text;
            int countryid = fontcountry.SelectedIndex;
            string country = fontcountry.SelectedItem.Text;
            int yachtid = fontyacht.SelectedIndex;
            string yacht = fontyacht.SelectedItem.Text;
            //參數化
            SqlParameter[] paras1 = new SqlParameter[]
           {
                       new SqlParameter("@Name", ReceiveName),new SqlParameter("@Email", ReceiveEmail),new SqlParameter("@Phone",Phone),new SqlParameter("@Comments", Comments),new SqlParameter("@countryid", countryid),new SqlParameter("@country", country),new SqlParameter("@yachtid", yachtid),new SqlParameter("@yacht", yacht)
           };
            //sql語法 新增文章
            string sql = "insert email (ename, eemail, ephone, ecomments, countryid,country, yachtid,yacht) values(@Name,@Email,@Phone,@Comments,@countryid,@country,@yachtid,@yacht) ";
            DBhelper.ExecuteNonQuery(sql, paras1);
        }

        /// <summary>
        /// 國家下拉選單
        /// </summary>
        private void LoadCountry()
        {
            //sql語法
            string sql = "select * from addcountry where display=1";
            DataTable table = DBhelper.GetDataTable(sql);
            fontcountry.DataSource = table;
            fontcountry.DataTextField = "country";
            fontcountry.DataValueField = "id";
            fontcountry.DataBind();
            //插入所有國家選單第一個位置
            fontcountry.Items.Insert(0, new ListItem("請選擇國家", ""));
        }

        public void SendEmail(string receiveEmail, string bodycontent)
        {
            //設定smtp主機
            string smtpAddress = "smtp.gmail.com";
            //設定Port
            int portNumber = 587;
            bool enableSSL = true;
            //填入寄送方email和密碼
            string emailFrom = "avonworktest@gmail.com";
            string password = "avon201012";
            //收信方email 可以用逗號區分多個收件人
            string emailTo = receiveEmail; //參數1
            //主旨
            string filetime = DateTime.Now.ToString("yyyy.MM.dd");
            string subject = filetime + " 遊艇回覆:";
            //內容
            string yacht = fontyacht.SelectedItem.Text;
            string body = bodycontent;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = true;

                //如果需要夾帶檔案
                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail); //如果有錯記得打開低安全
                }
            }
        }

        //email格式檢查
        private static bool getemail(string s)
        {
            //1@y.a
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool result = Regex.IsMatch(s, pattern);
            return result;
        }
    }
}