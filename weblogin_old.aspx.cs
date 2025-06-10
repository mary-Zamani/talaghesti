using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.IO;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TalaModelLibrary;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Interop;
using System.Numerics;

namespace narsShop
{
    public partial class weblogin_old : System.Web.UI.Page
    {
        protected static string callerurl = "~/pages/customeraccount.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            string tok = Request["tok"];
            string r = Request["r"];

            if (tok != null && tok.Length > 16)
            {


                string Txt_username = tok.Substring(0, 11);
                string Txt_password = tok.Substring(11, 10);


                if (!IsPostBack && !IsCallback)
                {
                    try
                    {
                        callerurl = Request.UrlReferrer.ToString();
                    }
                    catch
                    {
                        callerurl = "~/pages/customeraccount.aspx";
                    }
                }
                Page.Response.Write("<script>console.log('" + callerurl + "');</script>");



                var resp = Callapi_login(Txt_username, Txt_password);
                resp.Wait();

                token tk = JsonConvert.DeserializeObject<token>(resp.Result);
                Session["token"] = tk;

                if (tk.Token != null)
                {
                    string w = Request["w"];
                    if (string.IsNullOrEmpty(w))
                        if (string.IsNullOrEmpty(r))
                            Response.Redirect("pages/customeraccount.aspx");
                        else
                            Response.Redirect(callerurl);
                    else
                        Response.Redirect("pages/customercharge.aspx?type=W&value=" + w);
                }
                else
                {
                    Response.Redirect("weblogin.aspx");
                }

            }

            int why = 0;



            //callerurl.Contains("index.aspx") || callerurl.Trim().EndsWith("talaghesti.com/") ||

            if ( callerurl.Contains("weblogin"))
                callerurl = "~/pages/customeraccount.aspx";

            if (Request.Params.AllKeys.Contains("why"))
                why = myconvert.toint16(Request["why"].ToString());

            if (why == 0)
            {
                lbl_why.Text = " لطفا به حساب کاربری خود وارد شوید";
                lbl_why.BackColor = Color.Transparent;
            }
            else
            {
                lbl_why.Text = "برای خرید ابتدا وارد حساب کاربری خود شوید";
                lbl_why.BackColor = Color.LightYellow;
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            var resp = Callapi_login(txt_username.Text.Trim(), txt_password.Text.Trim());
            resp.Wait();

            token tk = JsonConvert.DeserializeObject<token>(resp.Result);
            Session["token"] = tk;

            if (tk.Token != null)
            {
                SQLH sqhand = new SQLH();
                sqhand.SqlExecute("update basket set tokenid='" + tk.vas + "' where sessionid='" + Session.SessionID + "'");

                HttpCookie TNcookie = new HttpCookie("Token");
                TNcookie.Value = JsonConvert.SerializeObject(tk);
                
                TNcookie.Expires = DateTime.Now.AddYears(1);
                
                Response.Cookies.Add(TNcookie);

                Response.Redirect(callerurl);
            }
        }


        async Task<string> Callapi_login(string Txt_username,string Txt_password)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/myauth/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "username", Txt_username },   //your user name
                  { "password", Txt_password },
                  { "sessionid",Session.SessionID        },
                  { "IP",GetIp()                         }
            };

            var content = new FormUrlEncodedContent(values);


            string resp = JsonConvert.SerializeObject(values);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl),
                Content = new StringContent(resp, Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = response;


            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = readTask.Result;
            }

            return json;
        }

        async Task<string> Callapi_signup()
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/myauth/register/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "username", Txt_mobile.Text.Trim() },   //your user name
                  { "password", Txt_shmeli.Text.Trim() },
                  { "fname", Txt_name.Text.Trim() },
                  { "moaref", Txt_moaref.Text.Trim() }
            };

            //,
            //      { "sessionid",Session.SessionID        },
            //      { "IP",GetIp()                         }

            var content = new FormUrlEncodedContent(values);


            string resp = JsonConvert.SerializeObject(values);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl),
                Content = new StringContent(resp, Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = response;


            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = readTask.Result;
            }

            return json;
        }
        string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;

        }

        protected void Btn_signup_Click(object sender, EventArgs e)
        {
            if (!shomaremali.isvalid(Txt_shmeli.Text.Trim()))
            {
                lbl_respond.Text = "شماره ملی معتبر نیست";
                return;
            }
            string mobileno = Txt_mobile.Text.Trim();

            if (!mobileno.StartsWith("09") || mobileno.Length != 11)
            {
                lbl_respond.Text = "شماره همراه معتبر نیست";
                return;
            }
            if (Txt_name.Text.Trim().Length<6 )
            {
                lbl_respond.Text = "لطفا نام و نام خانوادگی خود را وارد کنید";
                return;
            }

            var resp = Callapi_signup();
            resp.Wait();

            string vascode = JsonConvert.DeserializeObject<string>(resp.Result);
            

            if (vascode == "-1")
            {
                lbl_respond.Text = "شماره موبایل تکراری";
                return;
            }
            if (vascode == "-2")
            {
                lbl_respond.Text = "شماره ملی تکراری";
                return;
            }
            lbl_respond.Text = "شما با کد کاربری "+vascode+"ثبت نام شدید" +"<br>"+
                "لطفا به صفحه لاگین مراجعه کرده و وارد سایت شوید";

            var resp2 = Callapi_login(mobileno.Trim(), vascode.Trim());
            resp2.Wait();

            token tk = JsonConvert.DeserializeObject<token>(resp2.Result);
            Session["token"] = tk;

            if (tk.Token != null)
            {
                SQLH sqhand = new SQLH();
                sqhand.SqlExecute("update basket set tokenid='" + tk.vas + "' where sessionid='" + Session.SessionID + "'");
                Response.Redirect("index.aspx");
            }


        }
    }
}