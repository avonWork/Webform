using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace yacht
{
    public class DBhelper
    {
        private static string ConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // <summary>
        /// 連接式的連接方式，和數據庫保持連接狀態
        /// 執行返回游標方式結果集
        /// </summary>
        /// <param name="sql">SQL腳本</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteRreader(string sql, params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddRange(paras);
            SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// 執行添加.刪除.修改的通用方法  註冊
        /// </summary>
        /// <param name="sql">SQL腳本</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] paras)//形參 //params可選參數
        {
            string ConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //創建數據庫連接對象
            SqlConnection conn = new SqlConnection(ConnStr);
            //打開數據庫連接
            conn.Open();
            //創建執行腳本的對象
            SqlCommand command = new SqlCommand(sql, conn);
            //添加參數
            command.Parameters.AddRange(paras);
            //執行腳本
            int result = command.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        /// <summary>
        /// 執行返回臨時表 DataTable
        /// </summary>
        /// <param name="sql">SQL腳本</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] paras)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                //創建執行腳本的對象
                SqlCommand command = new SqlCommand(sql, conn);
                //添加參數
                command.Parameters.AddRange(paras);
                //創建數據適配器
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                //填充
                adapter.Fill(table);
            }
            return table;
        }

        /// <summary>
        /// 查詢--執行返回第一行第一列
        /// </summary>
        /// <param name="sql">SQL腳本</param>
        /// <param name="paras">參數</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)//形參 //params可選參數
        {
            //創建數據庫連接對象
            SqlConnection conn = new SqlConnection(ConnStr);
            //打開數據庫連接
            conn.Open();
            //創建執行腳本的對象
            SqlCommand command = new SqlCommand(sql, conn);
            //添加參數
            command.Parameters.AddRange(paras);
            //執行腳本
            object obj = command.ExecuteScalar();
            conn.Close();
            return obj;
        }

        #region "登入"

        /// <summary>
        /// 依據登入帳號密碼查詢是否有管理者
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public DataRow Login(string account, string password)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@loginID",account),new SqlParameter("@loginpwd", MD5password(password))
            };
            //sql語法 把留言板秀出
            string sql = "select * from addadmin where loginId=@loginID and loginPwd=@loginpwd";
            DataTable dataTable = GetDataTable(sql, paras);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        #endregion "登入"

        #region "權限-管理者菜單"

        /// <summary>
        /// "查詢管理者權限
        /// </summary>
        /// <returns></returns>
        public DataTable Getauthority()
        {
            //宣告輸入框變數
            string sql2 = "select * from  authority";
            DataTable dataTable = GetDataTable(sql2);
            return dataTable;
        }

        #endregion "權限-管理者菜單"

        #region 查詢管理者帳號密碼

        public SqlDataReader SelectAdminGetpwd(string loginld, string oldpwd)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", loginld), new SqlParameter("@oldpwd",  MD5password(oldpwd))
            };
            //sql語法
            string sql2 = "select * from addadmin where loginPwd=@oldpwd and  loginId=@id";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql2, paras2);
            return reader;
        }

        #endregion 查詢管理者帳號密碼

        #region 更新管理者密碼

        public void UpdateAdminPwd(string loginld, string newpwd)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", loginld), new SqlParameter("@newpwd", MD5password(newpwd))
            };
            //sql語法 新增留言資料
            string sql = "update addadmin set loginPwd=@newpwd where loginId=@id ";
            DBhelper.ExecuteNonQuery(sql, paras);
        }

        #endregion 更新管理者密碼

        #region 更新管理者資料

        public void UpdateAdminData(string id, string name, string pic, int Authoritynum, DateTime editAdtime,
            int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", id), new SqlParameter("@name", name), new SqlParameter("@pic", pic),
                new SqlParameter("@Authoritynum", Authoritynum), new SqlParameter("@editAdtime", editAdtime),
                new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增留言資料
            string sql2 =
                "update addadmin set  adminName = @name, imgName=@pic,authority=@Authoritynum,editTime=@editAdtime,adminID=@adMypersonId,updothing=@dotStirng where (id=@id)";
            ExecuteNonQuery(sql2, paras2);
        }

        #endregion 更新管理者資料

        #region 更新管理者資料及密碼

        public void UpdateAdminDataPwd(string id, string name, string pic, int Authoritynum, string pwd, DateTime editAdtime,
            int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras3 = new SqlParameter[]
            {
                new SqlParameter("@id", id), new SqlParameter("@name", name), new SqlParameter("@pic", pic),
                new SqlParameter("@Authoritynum", Authoritynum), new SqlParameter("@pwd", MD5password(pwd)),
                new SqlParameter("@editAdtime", editAdtime), new SqlParameter("@adMypersonId", adMypersonId),
                new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增留言資料
            string sql3 =
                "update addadmin set  adminName = @name, imgName=@pic,authority=@Authoritynum,loginPwd=@pwd,editTime=@editAdtime,adminID=@adMypersonId,updothing=@dotStirng where (id=@id)";
            ExecuteNonQuery(sql3, paras3);
        }

        #endregion 更新管理者資料及密碼

        #region "查詢管理者帳號"

        /// <summary>
        /// 根據帳號判斷管理者帳號是否有人使用
        /// </summary>
        /// <param name="userid">管理者帳號</param>
        /// <returns></returns>
        public SqlDataReader GetselectAdminId(string userid)
        {
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@userid", userid)
            };
            string sql2 = "select * from  addadmin where loginId=@userid";
            SqlDataReader reader = ExecuteRreader(sql2, paras2);
            return reader;
        }

        #endregion "查詢管理者帳號"

        #region 查詢管理者id

        public SqlDataReader SelectAdminbyid(string id)
        {
            //參數化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法
            string sql = "select * from addadmin where id=@id";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql, paras);
            return reader;
        }

        #endregion 查詢管理者id

        #region "新增管理者"

        /// <summary>
        /// 新增管理者
        /// </summary>
        /// <param name="userid">管理者帳號</param>
        /// <param name="name">管理者姓名</param>
        /// <param name="pwd">管理者密碼</param>
        /// <param name="pic">管理者照片</param>
        /// <param name="Authoritynum">管理者權限</param>
        /// <returns></returns>
        public void insertAdmin(string userid, string name, string pwd, string pic, int Authoritynum, int adMypersonId, string dotStirng)
        {
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@userid", userid),new SqlParameter("@name", name),new SqlParameter("@pwd", MD5password(pwd)),new SqlParameter("@pic", pic),new SqlParameter("@Authoritynum",  Authoritynum),new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增留言資料
            string sql1 = "insert addadmin (loginId,adminName,loginPwd,imgName,authority,adminID,dothing) values(@userid,@name,@pwd,@pic,@Authoritynum,@adMypersonId,@dotStirng) ";
            ExecuteNonQuery(sql1, paras1);
        }

        #endregion "新增管理者"

        #region "刪除管理者"

        /// <summary>
        /// 刪除管理者
        /// </summary>
        /// <param name="intId">刪除當列id</param>
        public void deleteAdmin(int userid)
        {
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@id", userid)
            };
            //sql語法 新增留言資料
            string sql1 = "delete addadmin where id=@id";
            ExecuteNonQuery(sql1, paras1);
        }

        #endregion "刪除管理者"

        #region "管理者列表分頁"

        /// <summary>
        /// 查詢管理者列表分頁
        /// </summary>
        /// <param name="newpage">紀錄當下的頁數</param>
        /// <param name="floor"></param>
        /// <param name="ceiling"></param>
        /// <returns></returns>
        public DataTable GetauthorityListpage(string newpage, int limit)
        {
            int page = 0;
            if (string.IsNullOrEmpty(newpage))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(newpage);
            }
            var floor = (page - 1) * limit + 1;
            var ceiling = page * limit;
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@floor", floor),new SqlParameter("@ceiling", ceiling)
            };
            string sql = $@" with cteTable as(
                  select ROW_NUMBER() over(order by insertTime desc,id asc) as rowIndex,* from addadmin
                  )
                  select* from cteTable where rowIndex >=@floor and rowIndex <= @ceiling";
            //sql語法
            DataTable table = DBhelper.GetDataTable(sql, paras1);
            return table;
        }

        #endregion "管理者列表分頁"

        #region "管理者列表數量分頁"

        /// <summary>
        /// 查詢管理者總頁數
        /// </summary>
        /// <returns></returns>
        public int getadminCount()
        {
            string sql2 = " select count(*) from addadmin";
            int count = (int)ExecuteScalar(sql2);
            return count;
        }

        #endregion "管理者列表數量分頁"

        #region 地區下拉選單--根據國家id查詢地區

        public DataTable GetAreabyCountryid(string countryid)
        {
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@countryid", countryid)
            };
            //重新綁定控件上的內容
            string sql3 = "select * from addarea where countryid=@countryid and display=1";
            DataTable table = DBhelper.GetDataTable(sql3, paras2);
            return table;
        }

        #endregion 地區下拉選單--根據國家id查詢地區

        #region 秀地區列表

        public DataTable SelectAreaList(string countryid)
        {
            //data
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@countryid", countryid)
            };
            //重新綁定控件上的內容
            string sql2 = "select * from addarea where countryid=@countryid and display=1 order by insertTime desc,id asc";
            DataTable table = DBhelper.GetDataTable(sql2, paras2);
            return table;
        }

        #endregion 秀地區列表

        #region 新增地區

        public void InsertArea(string area, string countryid, int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras3 = new SqlParameter[]
            {
                new SqlParameter("@area", area), new SqlParameter("@countryid", countryid),
                new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法
            string sql3 =
                "insert addarea  (area,countryid,display,adminID,dothing) values(@area,@countryid,1,@adMypersonId,@dotStirng)";
            DBhelper.ExecuteNonQuery(sql3, paras3);
        }

        #endregion 新增地區

        #region 更新地區

        public void UpdateArea(int areaid, string area)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@areaid", areaid), new SqlParameter("@area", area)
            };
            //sql語法
            string sql4 = "update addarea set area=@area where id=@areaid";
            DBhelper.ExecuteNonQuery(sql4, paras4);
        }

        #endregion 更新地區

        #region 刪除地區--隱藏

        public void DeleteArea(int intId)
        {
            //參數化
            SqlParameter[] paras6 = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql6 = "update addarea set display=0 where id=@id";
            DBhelper.ExecuteNonQuery(sql6, paras6);
        }

        #endregion 刪除地區--隱藏

        #region 國家下拉選單--查詢國家

        public DataTable Getcountry()
        {
            string sql = "select * from addcountry where display=1";
            DataTable table = DBhelper.GetDataTable(sql);
            return table;
        }

        #endregion 國家下拉選單--查詢國家

        #region 秀國家列表

        public DataTable SelectCountrylist()
        {
            string sql = "select * from addcountry where display=1 order by insertTime desc,id asc";
            DataTable table = DBhelper.GetDataTable(sql);
            return table;
        }

        #endregion 秀國家列表

        #region 更新國家

        public void UpdateCountry(int NewsID, string country)
        {
            //參數化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", NewsID), new SqlParameter("@country", country)
            };
            //sql語法
            string sql2 = "update addcountry set country=@country where id=@id";
            DBhelper.ExecuteNonQuery(sql2, paras);
        }

        #endregion 更新國家

        #region 刪除國家--隱藏

        public void DeleteCountry(int intId)
        {
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql4 = "update addcountry set display=0 where id=@id";
            DBhelper.ExecuteNonQuery(sql4, paras2);
        }

        #endregion 刪除國家--隱藏

        #region 新增國家

        public void InsertCountry(string addcountry, int adMypersonId, string dotStirng)
        {
            SqlParameter[] paras3 = new SqlParameter[]
            {
                new SqlParameter("@country", addcountry), new SqlParameter("@adMypersonId", adMypersonId),
                new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法
            string sql5 = "insert addcountry  (country,display,adminID,dothing) values(@country,1,@adMypersonId,@dotStirng)";
            DBhelper.ExecuteNonQuery(sql5, paras3);
        }

        #endregion 新增國家

        #region 新增代理商

        public void InsertAgent(string agent, string contact, string tel, string fax, string address, string email, string pic,
            string areaid, int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@agent", agent), new SqlParameter("@contact", contact),
                new SqlParameter("@tel", tel), new SqlParameter("@fax", fax),
                new SqlParameter("@address", address), new SqlParameter("@email", email),
                new SqlParameter("@pic", pic), new SqlParameter("@areaid", areaid),
                new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            string sql =
                "insert addagent (agent,contact,tel,fax,address,email,img,areaid,display,adminID,dothing) values(@agent,@contact,@tel,@fax,@address,@email,@pic,@areaid,1,@adMypersonId,@dotStirng) ";
            DBhelper.ExecuteNonQuery(sql, paras1);
        }

        #endregion 新增代理商

        #region 刪除代理商

        public void NoneDisplayAgent(int intId)
        {
            //參數化
            SqlParameter[] paras6 = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql6 = "update addagent set display=0 where id=@id";
            DBhelper.ExecuteNonQuery(sql6, paras6);
        }

        #endregion 刪除代理商

        #region 查詢代理商

        public SqlDataReader SelectAgent(string id)
        {
            //參數化
            SqlParameter[] paras6 = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql
            string sql6 = "select * from addagent where id=@id and display=1";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql6, paras6);
            return reader;
        }

        #endregion 查詢代理商

        #region 更新代理商

        public void UpdateAgent(string id, string areaid, string editagent, string editcontact, string edittel, string pic,
            string editfax, string editaddress, string editemail, DateTime editAdtime, int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", id), new SqlParameter("@areaid", areaid),
                new SqlParameter("@editagent", editagent), new SqlParameter("@editcontact", editcontact),
                new SqlParameter("@edittel", edittel), new SqlParameter("@pic", pic),
                new SqlParameter("@editfax", editfax), new SqlParameter("@editaddress", editaddress),
                new SqlParameter("@editemail", editemail), new SqlParameter("@editAdtime", editAdtime),
                new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增留言資料
            string sql2 =
                "update addagent set agent=@editagent, areaid=@areaid, contact = @editcontact, tel = @edittel,fax=@editfax,address=@editaddress,email=@editemail,img=@pic,editTime=@editAdtime,adminID=@adMypersonId,updothing=@dotStirng where id =@id";
            DBhelper.ExecuteNonQuery(sql2, paras2);
        }

        #endregion 更新代理商

        #region 秀代理商

        public SqlDataReader ShowSelectAgent(string id)
        {
            //參數化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法
            string sql =
                "SELECT  a.*, b.id AS 地區id,b.area AS 地區,c.id AS 國家id,c.country AS 國家 FROM addagent AS a INNER JOIN addarea AS b ON a.areaid = b.id INNER JOIN addcountry AS c ON b.countryid = c.id Where a.id=@id";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql, paras);
            return reader;
        }

        #endregion 秀代理商

        #region 搜尋查詢代理商列表分頁(全部)

        public DataTable SearchAgentlistpage(string searchtext, string newpage, int limits)
        {
            int page = 0;
            string searchcmd = ""; //搜尋條件

            //分頁
            if (string.IsNullOrEmpty(newpage))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(newpage);
            }
            var floor = (page - 1) * limits + 1;
            var ceiling = page * limits;
            if (!string.IsNullOrEmpty(searchtext))
            {
                searchcmd += "and agent like '%' + @searchtext + '%'";
            }
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@searchtext",searchtext),new SqlParameter("@floor", floor),new SqlParameter("@ceiling", ceiling)
            };
            string sql7 = $@" with cteTable as(
               select ROW_NUMBER() over(order by addagent.insertTime desc,addagent.id asc) as rowIndex,addcountry.country, addarea.area,addagent.* from  addagent INNER JOIN
               addarea ON addagent.areaid = addarea.id INNER JOIN
               addcountry ON addarea.countryid = addcountry.id  where 1=1 and addagent.display=1 {searchcmd}
                  )
                  select * from cteTable where rowIndex >=@floor and rowIndex <= @ceiling;";
            DataTable table = DBhelper.GetDataTable(sql7, paras1);
            return table;
        }

        #endregion 搜尋查詢代理商列表分頁(全部)

        #region 搜尋查詢代理商列表分頁(地區選取後)

        public DataTable SearchagentlistbyAreapage(int ddlcountry, int ddlarea, string searchagent, string newpage, int limits)
        {
            int page = 0;
            string searchcmd = ""; //搜尋條件
            if (ddlcountry.ToString() != null && ddlcountry > 0)
            {
                searchcmd += "and addcountry.id=@ddlcountry";
            }
            if (ddlarea.ToString() != null && ddlarea > 0)
            {
                searchcmd += " and addagent.areaid=@ddlarea";
            }
            if (!string.IsNullOrEmpty(searchagent))
            {
                searchcmd += $@" and agent like  '%' + @searchtext + '%'";
            }
            //分頁
            if (string.IsNullOrEmpty(newpage))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(newpage);
            }

            var floor = (page - 1) * limits + 1;
            var ceiling = page * limits;
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@ddlcountry", ddlcountry), new SqlParameter("@ddlarea", ddlarea),
                new SqlParameter("@searchtext", searchagent), new SqlParameter("@floor", floor),
                new SqlParameter("@ceiling", ceiling)
            };
            string sql7 = $@" with cteTable as(
                 select ROW_NUMBER() over(order by addagent.insertTime desc,addagent.id asc) as rowIndex,addcountry.id as 國家id,addcountry.country,addarea.area,addagent.* from  addagent INNER JOIN
               addarea ON addagent.areaid = addarea.id INNER JOIN
               addcountry ON addarea.countryid = addcountry.id  where 1=1 and addagent.display=1 {searchcmd}
                  )
                  select * from cteTable where rowIndex >=@floor and rowIndex <=@ceiling ;";
            DataTable table = DBhelper.GetDataTable(sql7, paras1);
            return table;
        }

        #endregion 搜尋查詢代理商列表分頁(地區選取後)

        #region 搜尋代理商資料數量分頁

        public int GetAgentCount(string searchtext)
        {
            string searchcmd = ""; //搜尋條件
            if (!string.IsNullOrEmpty(searchtext))
            {
                searchcmd += "and agent like '%' + @searchtext + '%'";
            }
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@searchtext",searchtext)
            };
            string sql8 = $@" select count(*) from addagent where 1=1 and display=1 {searchcmd}";
            int count = (int)DBhelper.ExecuteScalar(sql8, paras1);
            return count;
        }

        #endregion 搜尋代理商資料數量分頁

        #region 搜尋代理商資料數量分頁(地區選取後)

        public int GetAgentCountbyArea(int ddlcountry, int ddlarea, string searchagent)
        {
            string searchcmd = ""; //搜尋條件
            if (ddlcountry.ToString() != null && ddlcountry > 0)
            {
                searchcmd += "and addcountry.id=@ddlcountry";
            }
            if (ddlarea.ToString() != null && ddlarea > 0)
            {
                searchcmd += " and addagent.areaid=@ddlarea";
            }
            if (!string.IsNullOrEmpty(searchagent))
            {
                searchcmd += $@" and agent like  '%' + @searchtext + '%'";
            }
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@ddlcountry", ddlcountry), new SqlParameter("@ddlarea", ddlarea),
                new SqlParameter("@searchtext", searchagent)
            };
            string sql8 = $@" select count(*) from  addagent INNER JOIN
               addarea ON addagent.areaid = addarea.id INNER JOIN
               addcountry ON addarea.countryid = addcountry.id  where 1=1 and  addagent.display=1 {searchcmd}";
            int count = (int)DBhelper.ExecuteScalar(sql8, paras2);
            return count;
        }

        #endregion 搜尋代理商資料數量分頁(地區選取後)

        #region 查詢一筆新聞

        public SqlDataReader SelectNewbyid(string id)
        {
            //參數化
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@id",id)
            };
            string sql = "select * from news where (id=@id)";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql, paras1);
            return reader;
        }

        #endregion 查詢一筆新聞

        #region 更新新聞置頂(唯一)

        public void UpdateNewTop()
        {
            string sql3 = "update news set sticky=0 where sticky=1"; //查詢check為true改false
            DBhelper.ExecuteNonQuery(sql3);
        }

        #endregion 更新新聞置頂(唯一)

        #region 更新新聞

        public void UpdateNew(string id, string editdate, string edittitle, string editdetail, string pic, string editcontent,
            int checkvalue)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", id), new SqlParameter("@editdate", editdate), new SqlParameter("@edittitle", edittitle),
                new SqlParameter("@editdetail", editdetail), new SqlParameter("@pic", pic),
                new SqlParameter("@editcontent", editcontent), new SqlParameter("@editsticky", checkvalue)
            };
            //sql語法 新增留言資料
            string sql2 =
                "update news set date=@editdate, title = @edittitle, detail = @editdetail,img=@pic,new_content=@editcontent,sticky=@editsticky where id =@id";
            DBhelper.ExecuteNonQuery(sql2, paras2);
        }

        #endregion 更新新聞

        #region 新增新聞

        public void InsertNew(string new_date, string new_title, string new_detail, string pic, string new_content,
            int checkvalue, int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@new_date", new_date), new SqlParameter("@new_title", new_title),
                new SqlParameter("@new_detail", new_detail), new SqlParameter("@pic", pic),
                new SqlParameter("@new_content", new_content), new SqlParameter("@checkvalue", checkvalue),
                new SqlParameter("@adMypersonId", adMypersonId),
                new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增文章
            string sql =
                "insert news (date,title,detail,img,new_content,sticky,adminID,dothing) values(@new_date,@new_title,@new_detail,@pic,@new_content,@checkvalue,@adMypersonId,@dotStirng) ";
            DBhelper.ExecuteNonQuery(sql, paras1);
        }

        #endregion 新增新聞

        #region 刪除新聞

        public void DeleteNew(int intId)
        {
            //參數化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql3 = "delete news where id=@id";
            DBhelper.ExecuteNonQuery(sql3, paras);
        }

        #endregion 刪除新聞

        #region 搜尋查詢新聞列表分頁

        public DataTable SearchNewlistpage(string strStatrdate, string strEnddate, string strTitle, string newpage, int limits)
        {
            string searchcmd = ""; //搜尋條件
            if (strStatrdate != null && strStatrdate != "")
            {
                if (strEnddate != null && strEnddate != "")
                {
                    searchcmd += $"and date between @strStatrdate AND DATEADD(day, 1, @strEnddate)";
                }
            }

            if (strTitle != null && strTitle != "")
            {
                searchcmd += $"and title like '%' + @strTitle + '%'";
            }

            int page = 0;
            if (string.IsNullOrEmpty(newpage))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(newpage);
            }

            var floor = (page - 1) * limits + 1;
            var ceiling = page * limits;
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@strStatrdate", strStatrdate), new SqlParameter("@strEnddate", strEnddate),
                new SqlParameter("@strTitle", strTitle), new SqlParameter("@floor", floor),
                new SqlParameter("@ceiling", ceiling)
            };
            string sql7 = $@" with cteTable as(
                  select ROW_NUMBER() over(order by sticky desc,insertTime desc) as rowIndex, * from news where 1=1 {searchcmd}
                  )
                  select * from cteTable where rowIndex >=@floor and rowIndex <= @ceiling";
            DataTable table = DBhelper.GetDataTable(sql7, paras2);
            return table;
        }

        #endregion 搜尋查詢新聞列表分頁

        #region 搜尋新聞數量分頁

        public int SelectNewCount(string strStatrdate, string strEnddate, string strTitle)
        {
            string searchcmd = ""; //搜尋條件
            if (strStatrdate != null && strStatrdate != "")
            {
                if (strEnddate != null && strEnddate != "")
                {
                    searchcmd += $"and date between @strStatrdate AND DATEADD(day, 1, @strEnddate)";
                }
            }

            if (strTitle != null && strTitle != "")
            {
                searchcmd += $"and title like '%' + @strTitle + '%'";
            }
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@strStatrdate", strStatrdate), new SqlParameter("@strEnddate", strEnddate),
                new SqlParameter("@strTitle", strTitle)
            };
            string sql8 = $@" select count(*) from news where 1=1 {searchcmd}";
            int count = (int)DBhelper.ExecuteScalar(sql8, paras1);
            return count;
        }

        #endregion 搜尋新聞數量分頁

        #region 查詢檔案上傳

        public DataTable SelectFileyacht(string id)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法 把檔案列表秀出
            string sql = $" select * from fileyacht where yachtid=@id order by fileorder";
            DataTable table = DBhelper.GetDataTable(sql, paras4);
            return table;
        }

        #endregion 查詢檔案上傳

        #region 更新檔案上傳

        public void UpdateFileyacht(int fileid, string text)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@fileid", fileid), new SqlParameter("@text", text)
            };
            //sql語法
            string sql4 = "update fileyacht set fileText=@text where id=@fileid";
            DBhelper.ExecuteNonQuery(sql4, paras4);
        }

        #endregion 更新檔案上傳

        #region 刪除檔案上傳

        public void DeleteFileyacht(int intId)
        {
            //參數化
            SqlParameter[] paras6 = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql6 = "delete fileyacht where id=@id";
            DBhelper.ExecuteNonQuery(sql6, paras6);
        }

        #endregion 刪除檔案上傳

        #region 新增檔案上傳

        public void InsertFileyacht(string file, string fileText, string id)
        {
            //參數化
            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@file", file), new SqlParameter("@fileText", fileText), new SqlParameter("@yachtid", id)
            };
            //sql語法 新增文章
            string sql =
                "insert fileyacht (fileName,fileText,yachtid,fileorder) values(@file,@fileText,@yachtid,(SELECT count(1) FROM fileyacht where yachtid=@yachtid)+1) ";
            DBhelper.ExecuteNonQuery(sql, paras1);
        }

        #endregion 新增檔案上傳

        #region 檔案排序更新

        public void UpdatFileorder(string no, string fid)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@no", no), new SqlParameter("@fileid", fid)
            };
            //sql語法
            string sql4 = "update fileyacht set fileorder=@no where id=@fileid;";
            DBhelper.ExecuteNonQuery(sql4, paras4);
        }

        #endregion 檔案排序更新

        #region 查詢圖片上傳

        public DataTable SelectImgyacht(string id)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法 把遊艇列表秀出
            string sql = $" select * from imgyacht where yachtid=@id order by imgorder";
            DataTable table = DBhelper.GetDataTable(sql, paras4);
            return table;
        }

        #endregion 查詢圖片上傳

        #region 更新圖片上傳

        public void UpdateImgyacht(int imgid, string text)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@imgid", imgid), new SqlParameter("@text", text)
            };
            //sql語法
            string sql4 = "update imgyacht set imgText=@text where id=@imgid";
            DBhelper.ExecuteNonQuery(sql4, paras4);
        }

        #endregion 更新圖片上傳

        #region 刪除圖片上傳

        public void DeleteImgyacht(int intId)
        {
            //參數化
            SqlParameter[] paras6 = new SqlParameter[]
            {
                new SqlParameter("@id", intId)
            };
            //sql語法
            string sql6 = "delete imgyacht where id=@id";
            DBhelper.ExecuteNonQuery(sql6, paras6);
        }

        #endregion 刪除圖片上傳

        #region 新增圖片上傳

        public void InsertImgyacht(string img, string imgText, string id)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@img", img), new SqlParameter("@imgText", imgText), new SqlParameter("@yachtid", id)
            };
            //sql語法 新增文章
            string sql2 =
                "insert imgyacht (imgName, imgText, imgorder, yachtid) values(@img,@imgText,(SELECT count(1) FROM imgyacht where yachtid=@yachtid)+1,@yachtid) ";
            DBhelper.ExecuteNonQuery(sql2, paras2);
        }

        #endregion 新增圖片上傳

        #region 圖片排序更新

        public void UpdateImgorder(string no, string fid)
        {
            //參數化
            SqlParameter[] paras4 = new SqlParameter[]
            {
                new SqlParameter("@no", no), new SqlParameter("@fileid", fid)
            };
            //sql語法
            string sql4 = "update imgyacht set imgorder=@no where id=@fileid;";
            DBhelper.ExecuteNonQuery(sql4, paras4);
        }

        #endregion 圖片排序更新

        #region 查詢遊艇id

        public SqlDataReader SelectYacht(string id)
        {
            //參數化
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法 把留言者內容秀出一條
            string sql = "select * from yachts where id=@id";
            SqlDataReader reader = DBhelper.ExecuteRreader(sql, paras);
            return reader;
        }

        #endregion 查詢遊艇id

        #region 查詢遊艇

        public DataTable GetYacht()
        {
            string sql8 = "select * from yachts";
            DataTable table = DBhelper.GetDataTable(sql8);
            return table;
        }

        #endregion 查詢遊艇

        #region 新增遊艇

        public void InsertYacht(string Overview, string Layout, string Specification, string yachttitle, string checkvalue, int adMypersonId,
            string dotStirng)
        {
            //參數化
            SqlParameter[] paras5 = new SqlParameter[]
            {
                new SqlParameter("@Overview", Overview), new SqlParameter("@Layout", Layout),
                new SqlParameter("@Specification", Specification), new SqlParameter("@Title", yachttitle),
                new SqlParameter("@checkvalue", checkvalue), new SqlParameter("@adMypersonId", adMypersonId),
                new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法 新增文章
            string sql5 =
                "insert yachts (overview, layout, specification, title, newcheck,adminID,dothing) values(@Overview,@Layout,@Specification,@Title,@checkvalue,@adMypersonId,@dotStirng);";
            DBhelper.ExecuteNonQuery(sql5, paras5);
        }

        #endregion 新增遊艇

        #region 更新遊艇

        public void UpdateYacht(string id, string YachtTitle, string Content, string Content1, string Content2, int checkvalue,
            DateTime editAdtime, int adMypersonId, string dotStirng)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", id), new SqlParameter("@YachtTitle", YachtTitle), new SqlParameter("@Content", Content),
                new SqlParameter("@Content1", Content1), new SqlParameter("@Content2", Content2),
                new SqlParameter("@checkvalue", checkvalue), new SqlParameter("@editAdtime", editAdtime),
                new SqlParameter("@adMypersonId", adMypersonId), new SqlParameter("@dotStirng", dotStirng)
            };
            //sql語法
            string sql2 =
                "update yachts set overview=@Content,layout=@Content1,specification=@Content2,title=@YachtTitle,newcheck=@checkvalue,editTime=@editAdtime,adminID=@adMypersonId,updothing=@dotStirng where id =@id";
            ExecuteNonQuery(sql2, paras2);
        }

        #endregion 更新遊艇

        #region 刪除本筆遊艇及檔案及圖片

        public void DeleteYachtFileImg(string id)
        {
            //參數化
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            //sql語法
            string sql2 = "delete yachts where id=@id;delete fileyacht where yachtid=@id;delete imgyacht where yachtid=@id;";
            DBhelper.ExecuteNonQuery(sql2, paras2);
        }

        #endregion 刪除本筆遊艇及檔案及圖片

        #region 搜尋查詢遊艇列表分頁

        public DataTable SearchYachtlistpage(string strStatrdate, string strEnddate, string strTitle, string newpage, int limits)
        {
            string searchcmd = ""; //搜尋條件
            if (strStatrdate != null && strStatrdate != "")
            {
                if (strEnddate != null && strEnddate != "")
                {
                    searchcmd += $"and insertTime between @strStatrdate AND DATEADD(day, 1, @strEnddate)";
                }
            }

            if (strTitle != null && strTitle != "")
            {
                searchcmd += $"and title like '%' + @strTitle + '%'";
            }

            int page = 0;
            if (string.IsNullOrEmpty(newpage))
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(newpage);
            }

            var floor = (page - 1) * limits + 1;
            var ceiling = page * limits;
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@strStatrdate", strStatrdate), new SqlParameter("@strEnddate", strEnddate),
                new SqlParameter("@strTitle", strTitle), new SqlParameter("@floor", floor),
                new SqlParameter("@ceiling", ceiling)
            };
            string sql7 = $@" with cteTable as(
                  select ROW_NUMBER() over(order by insertTime desc,id asc) as rowIndex, * from yachts where 1=1 {searchcmd}
                  )
                  select * from cteTable where rowIndex >=@floor and rowIndex <= @ceiling";
            DataTable table = DBhelper.GetDataTable(sql7, paras2);
            return table;
        }

        #endregion 搜尋查詢遊艇列表分頁

        #region 搜尋查詢遊艇數量分頁

        public int SelectYachtCount(string strStatrdate, string strEnddate, string strTitle)
        {
            string searchcmd = ""; //搜尋條件
            if (strStatrdate != null && strStatrdate != "")
            {
                if (strEnddate != null && strEnddate != "")
                {
                    searchcmd += $"and insertTime between @strStatrdate AND DATEADD(day, 1, @strEnddate)";
                }
            }

            if (strTitle != null && strTitle != "")
            {
                searchcmd += $"and title like '%' + @strTitle + '%'";
            }
            SqlParameter[] paras2 = new SqlParameter[]
            {
                new SqlParameter("@strStatrdate", strStatrdate), new SqlParameter("@strEnddate", strEnddate),
                new SqlParameter("@strTitle", strTitle)
            };
            string sql8 = $@" select count(*) from yachts where 1=1 {searchcmd}";
            int count = (int)DBhelper.ExecuteScalar(sql8, paras2);
            return count;
        }

        #endregion 搜尋查詢遊艇數量分頁

        #region MD5--學長提供

        protected static string MD5password(string str)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        #endregion MD5--學長提供
    }
}