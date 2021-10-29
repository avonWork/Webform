using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace yacht
{
    public partial class recaptcha3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool result;
            string email = Request.Form["email"];
            string recaptcha = Request.Form["token"];
            if (recaptcha == null || recaptcha == "") return;
            string secretKey = "6LdqKVwaAAAAALusjKkkAXVjM3jdqamBs_oEynk2";
            string apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            string requestUrl = string.Format(apiUrl, secretKey, recaptcha);
            WebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    bool isSuccess = jResponse.Value<bool>("success");
                    string hostname = jResponse.Value<string>("hostname");
                    string action = jResponse.Value<string>("action");
                    string score = jResponse.Value<string>("score");

                    DateTime challenge_ts = jResponse.Value<DateTime>("challenge_ts");
                    challenge_ts = challenge_ts.ToLocalTime();
                    result = (isSuccess) ? true : false;
                    if (result)
                    {
                        Response.Write("hi" + email + "<br>");
                        Response.Write("you are from" + hostname + " " + challenge_ts + "<br>");
                        Response.Write("action" + action + "<br>");
                        Response.Write("score" + score + "<br>");
                    }
                }
            }
        }
    }
}