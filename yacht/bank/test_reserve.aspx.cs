using System;
using System.IO;

namespace yacht.bank
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            System.Text.StringBuilder strmsg = new System.Text.StringBuilder("");
            string[] rd = Request.Form[3].Split(',');//獲得圖片描述的文字框字串陣列,為對應的圖片的描述
                                                     // string albumid = ddlAlbum.SelectedValue.Trim();
            int ifile;
            for (ifile = 0; ifile < files.Count; ifile++)
            {
                if (files[ifile].FileName.Length > 0)
                {
                    System.Web.HttpPostedFile postedfile = files[ifile];

                    //if (postedfile.ContentLength / 1024 > 1024)//單個檔案不能大於1024k
                    //{
                    //    strmsg.Append(Path.GetFileName(postedfile.FileName) + "---不能大於1024k");
                    //    break;
                    //}

                    //抓附檔名格式驗證
                    //string fex = Path.GetExtension(postedfile.FileName);
                    //if (fex != ".jpg" && fex != ".JPG" && fex != ".gif" && fex != ".GIF")
                    //{
                    //    strmsg.Append(Path.GetFileName(postedfile.FileName) + "---圖片格式不對,只能是jpg或gif");
                    //    break;
                    //}
                }
            }
            //if (strmsg.Length <= 0)//說明圖片大小和格式都沒問題
            //{
                //以下為建立相簿目錄
                //string dirname = "pic00" + ddlAlbum.SelectedValue.Trim();
            string dirpath = Server.MapPath("~/ckfinder/userfiles/test/");   //-- 網站的URL路徑。
                                                                              //dirpath = dirpath + "/" + dirname;
                                                                              //創建資料夾
                                                                              //if (Directory.Exists(dirpath) == false)
                                                                              //{
                                                                              //    Directory.CreateDirectory(dirpath);
                                                                              //}
            //Random ro = new Random();
                //int name = 1;
                for (int i = 0; i < files.Count; i++)
                {
                    System.Web.HttpPostedFile myFile = files[i];
                    string FileName = "";
                    //string FileExtention = "";
                    //string PicPath = "";
                    FileName = System.IO.Path.GetFileName(myFile.FileName);
                    //string stro = name.ToString();//產生一個隨機數用於新命名的圖片
                    //string NewName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                    if (FileName.Length > 0)//有檔案才執行上傳操作再儲存到資料庫
                    {
                        //FileExtention = System.IO.Path.GetExtension(myFile.FileName);
                    string ppath = dirpath + "/" + FileName;
                        myFile.SaveAs(ppath);
                        //string FJname = FileName;
                        //PicPath = "PicBase" + "/" + FileName + FileExtention;
                    }
                //AddPicture(PicPath, rd[i], "albumid");//將圖片資訊儲存到資料庫
                //if (name == 1)//如果為每次更新的第一張圖片,postedfile
                //    upFirstimg("albumid", PicPath);
                //}
                //name = name + 1;//用來重新命名規則的變數
            }
            //}
            //else
            //{
            //    lblMessage.Text = strmsg.ToString();
            //    lblMessage.Visible = true;
            //}
        }

        private void AddPicture(string imgpath, string imgnote, string albumid)
        {
            string sql = "insert WB_AlbumImges(ImgPath,ImgNote,AlbumID) values('" + imgpath + "','" + imgnote + "','" + albumid + "')";
            //DB mydb = new DB();
            //mydb.RunProc(sql);
        }
        //相簿本
        //private void upFirstimg(string albumid, string firstimg)
        //{
            //string sql = "update WB_Album set FirstImg='" + firstimg + "' where AlbumID=" + albumid;
            //DB mydb = new DB();
            //mydb.RunProc(sql);
        //}
    }
}