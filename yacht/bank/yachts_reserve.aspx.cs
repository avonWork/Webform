using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace yacht.bank
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //寫好的文字檔
            yachtcontent();
        }

        private void yachtcontent()
        {
            //Tayana 37-1
            //string filegoblank1 = @"C:\Users\user\Desktop\Tayana\Tayana 64_1.txt";
            //content.Text = Goblank(filegoblank1);
            ////Tayana 37-2
            //string filegoblank2 = @"C:\Users\user\Desktop\Tayana\ISARA 50_1.txt";
            //content1.Text = Goblank(filegoblank2);
            ////Tayana 37-3
            //string filegoblank3 = @"C:\Users\user\Desktop\Tayana\Tayana 58_1.txt";
            //content2.Text = Goblank(filegoblank3);
        }

        /// <summary>
        /// Html編碼解碼(ckeditor)
        /// </summary>
        /// <param name="filegoblank"></param>
        /// <returns></returns>
        private string Goblank(string filegoblank)
        {
            //.Replace("\"","\'")
            string filename = File.ReadAllText(filegoblank);
            string file = HttpUtility.HtmlEncode(filename).Replace(" \r\n\t", "");
            string result = HttpUtility.HtmlDecode(file).Replace("\t", "").Replace("\r\n\r\n", "");
            return result;
        }

        /// <summary>
        /// 留著(多圖片上傳)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string file = "";
        //    string img = "";
        //    if (FileUpload1.HasFile)
        //    {
        //        //檔案上傳
        //        string path = Server.MapPath("~/ckfinder/userfiles/Files/");   //-- 網站的URL路徑。
        //        string filetime = DateTime.Now.ToString("yyyyMMdd");
        //        string filename = filetime + FileUpload1.FileName;
        //        FileUpload1.PostedFile.SaveAs(path + filename);  //ckedit儲存
        //        string filePath = path + filename;
        //        file = filePath.Substring(filePath.LastIndexOf('\\') + 1);
        //    }
        //    string imgs = "";
        //    //多圖片上傳
        //    string path2 = Server.MapPath("~/ckfinder/userfiles/moreimg/");   //-- 網站的URL路徑。
        //    string imgtime = DateTime.Now.ToString("yyyyMMdd");
        //    foreach (HttpPostedFile fileimg in FileUpload2.PostedFiles)
        //    {
        //        string imgname = imgtime + fileimg.FileName;
        //        fileimg.SaveAs(path2 + imgname);

        //        string imgPath = path2 + imgname;
        //        img = imgPath.Substring(imgPath.LastIndexOf('\\') + 1);
        //        imgs += img + ",";
        //    }
        //    //宣告輸入框變數
        //    string Overview = content.Text;
        //    string Layout = content1.Text;
        //    string Specification = content2.Text;
        //    string Title = yachttile.Text;
        //    string day = DateTime.Now.ToString("yyyyMMdd");
        //    int checkvalue = 0;
        //    //多圖片去尾
        //    imgs = imgs.TrimEnd(',');
        //    if (CheckBox1.Checked == true)
        //    {
        //        checkvalue = 1;
        //    }
        //    //參數化
        //    SqlParameter[] paras1 = new SqlParameter[]
        //   {
        //               new SqlParameter("@Overview", Overview),new SqlParameter("@Layout", Layout),new SqlParameter("@Specification",Specification),new SqlParameter("@file", file),new SqlParameter("@Title", Title),new SqlParameter("@checkvalue", checkvalue),new SqlParameter("@day", day),new SqlParameter("@imgs", imgs)
        //   };
        //    //sql語法 新增文章
        //    string sql = "insert yachts (overview, layout, specification, title, fileyacht,newcheck,insertday,image) values(@Overview,@Layout,@Specification,@Title,@file,@checkvalue,@day,@imgs) ";
        //    DBhelper.ExecuteNonQuery(sql, paras1);
        //    //顯示彈跳訊息
        //    Response.Write("<script type='text/javascript'> alert('新增遊艇成功!回到遊艇列表');</script>");
        //    Response.Redirect("bank_yachtsList.aspx");
        //}

        protected void Button2_Click(object sender, EventArgs e)
        {
            string[] filenames = Request.Form[6].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
            string[] filetexts = Request.Form[7].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
            string[] fileorders = Request.Form[8].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
            string[] imgnames = Request.Form[9].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
            string[] imgtexts = Request.Form[10].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
            string[] imgorders = Request.Form[11].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述

            //for (int i = 0; i < files.AllKeys.Length; i++)
            //{
            //    if (files.AllKeys[i] == "ctl00$ContentPlaceHolder1$FileUpload1")
            //    {
            //            string field = files.AllKeys[i]; //The 'name' attribute of the html form
            //            string fileName = files[i].FileName; //The path to the file on the client computer
            //            string type = files[i].ContentType; //The file's MIME type
            //        }
            //}

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            for (int x = 0; x < files.Count; x++)
            {
                if (files[x].FileName.Length > 0)
                {
                    string filetime = DateTime.Now.ToString("yyyyMMdd");
                    //檔案上傳
                    if (files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl00" || files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl02")
                    {
                        string fileName = filetime + files[x].FileName;
                        string dirpath = Server.MapPath("~/ckfinder/userfiles/Files/");   //-- 網站的URL路徑。
                        System.Web.HttpPostedFile myFile = files[x];
                        string ppath = dirpath + "/" + fileName;
                        myFile.SaveAs(ppath);
                    }

                    //圖片上傳
                    if (files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl01" || files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl03")
                    {
                        string fileName = filetime + files[x].FileName;
                        string dirpath = Server.MapPath("~/ckfinder/userfiles/moreimg/");   //-- 網站的URL路徑。
                        System.Web.HttpPostedFile myFile = files[x];
                        string ppath = dirpath + "/" + fileName;
                        myFile.SaveAs(ppath);
                    }
                }
            }
        }

        /// <summary>
        /// 上傳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            string[] filetexts = Request.Form[6].Split(',');//文字框字串陣列
            string[] fileorders = Request.Form[7].Split(',');//文字框字串陣列
            string[] imgtexts = Request.Form[8].Split(',');//文字框字串陣列
            string[] imgorders = Request.Form[9].Split(',');//文字框字串陣列
            string filename = ""; //檔案
            string filetext = "";
            string fileorder = "";
            string imgname = "";//圖片
            string imgtext = "";
            string imgorder = "";
            int fileNamecount = 0; //幾個上傳檔案
            int fileImgNamecount = 0; //幾個上傳圖片
            string fileNamestr = ""; //上傳檔案字串
            string imgNamestr = ""; //上傳圖片字串
            //宣告輸入框變數
            string Overview = content.Text;
            string Layout = content1.Text;
            string Specification = content2.Text;
            string Title = yachttile.Text;
            string day = DateTime.Now.ToString("yyyyMMdd");
            int checkvalue = 0;
            if (CheckBox1.Checked == true)
            {
                checkvalue = 1;
            }
            //參數化 Ckedit
            SqlParameter[] paras5 = new SqlParameter[]
           {
             new SqlParameter("@Overview", Overview),new SqlParameter("@Layout", Layout),new SqlParameter("@Specification",Specification),new SqlParameter("@Title", Title),new SqlParameter("@checkvalue", checkvalue),new SqlParameter("@day", day)
           };
            //sql語法 新增文章
            string sql5 = "insert yachts (overview, layout, specification, title,newcheck,insertday) values(@Overview,@Layout,@Specification,@Title,@checkvalue,@day) ";
            DBhelper.ExecuteNonQuery(sql5, paras5);

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //所有fileupload集合(上傳)
            for (int x = 0; x < files.Count; x++)
            {
                if (files[x].FileName.Length > 0)
                {
                    //檔案上傳
                    if (files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl00" || files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl02")
                    {
                        string fileName = day + files[x].FileName;
                        string dirpath = Server.MapPath("~/ckfinder/userfiles/Files/");   //-- 網站的URL路徑。
                        System.Web.HttpPostedFile myFile = files[x];
                        string ppath = dirpath + "/" + fileName;
                        myFile.SaveAs(ppath);
                        fileNamestr += fileName + ",";
                        fileNamecount++;
                    }
                    //圖片上傳
                    if (files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl01" || files.AllKeys[x].Substring(files.AllKeys[x].LastIndexOf('$') + 1) == "ctl03")
                    {
                        string fileImgName = day + files[x].FileName;
                        string dirpath = Server.MapPath("~/ckfinder/userfiles/moreimg/");   //-- 網站的URL路徑。
                        System.Web.HttpPostedFile myFile = files[x];
                        string ppath = dirpath + "/" + fileImgName;
                        myFile.SaveAs(ppath);
                        imgNamestr += fileImgName + ",";
                        fileImgNamecount++;
                    }
                }
            }
            //檔案SQL
            string[] oldfile = fileNamestr.Split(',');
            for (int i = 0; i < fileNamecount; i++)
            {
                filename = oldfile[i];
                filetext = filetexts[i];
                fileorder = fileorders[i];

                //參數化 新增檔案
                SqlParameter[] paras3 = new SqlParameter[]
               {
             new SqlParameter("@filename", filename),new SqlParameter("@filetext", filetext),new SqlParameter("@fileorder",fileorder),new SqlParameter("@Title",Title)
               };
                //sql語法
                string sql3 = "insert fileyacht (fileName, fileText, fileorder, yachtTitle) values(@filename,@filetext,@fileorder,@Title) ";
                DBhelper.ExecuteNonQuery(sql3, paras3);
            }

            ///圖片SQL
            string[] oldimg = imgNamestr.Split(',');
            for (int j = 0; j < fileImgNamecount; j++)
            {
                imgname = oldimg[j];
                imgtext = imgtexts[j];
                imgorder = imgorders[j];

                //參數化 新增圖案
                SqlParameter[] paras4 = new SqlParameter[]
               {
            new SqlParameter("@imgname", imgname),new SqlParameter("@imgtext", imgtext),new SqlParameter("@imgorder",imgorder),new SqlParameter("@Title", Title)
               };
                //sql語法 新增文章
                string sql4 = "insert imgyacht (imgName, imgText, imgorder, yachtTitle) values(@imgname,@imgtext,@imgorder,@Title)";
                DBhelper.ExecuteNonQuery(sql4, paras4);
            }

            //顯示彈跳訊息
            Response.Write("<script type='text/javascript'> alert('新增遊艇成功!回到遊艇列表');</script>");
            Response.Redirect("bank_yachtsList.aspx");
        }
    }
}