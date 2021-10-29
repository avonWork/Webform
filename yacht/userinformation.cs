using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yacht
{
    public class userinformation
    {
        public Int32 id { get; set; } //id
        public string userid { get; set; }//帳號
        public string password { get; set; }//密碼
        public string name { get; set; }//使用者名稱
        public string permission { get; set; }//權限
        public string photo { get; set; } //照片
    }
}