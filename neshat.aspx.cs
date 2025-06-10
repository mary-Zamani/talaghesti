using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MSCaptcha;
using Newtonsoft.Json;
using TalaModelLibrary;

namespace narsShop
{
    public partial class neshat : System.Web.UI.Page
    {
        SQLH sqhand;
        private Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["oldCaptchaImageText"] = this.Session["CaptchaImageText"];
            this.Session["CaptchaImageText"] = GenerateRandomCode();

            //sqhand = new SQLH();

            //lbl_hero.Text = heroslider();
            // lbl_banners.Text = banners();
            //lbl_productrow1.Text = rowofproducts();
            // lbl_productrow2.Text = rowofproducts();


        }
        private string GenerateRandomCode()
        {
            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, this.random.Next(10).ToString());
            return s;
        }


        async Task<string> Callapi_savemessage()
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/savemesage/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "telno", txtmbl.Text.Trim()  },   //your user name
                  { "uname", txtname.Text.Trim() },
                  { "messagebody",txtpayam.Text.Trim() },
                  { "IP",GetIp()                 }
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

        string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;

        }

        protected void Btnsend_Click(object sender, EventArgs e)
        {
            if (txtmbl.Text.Trim().Length!=11 || !txtmbl.Text.Trim().StartsWith("09") || myconvert.toint(txtmbl.Text) == 0)
            {
                lbl_error.Text = "اشکال در شماره موبایل. پیام ارسال نشد!";
                return;
            }

            if (txtname.Text.Trim().Length < 5)
            {
                lbl_error.Text = "اشکال در نام. پیام ارسال نشد!";
                return;
            }

            if (txtpayam.Text.Trim().Length < 10)
            {
                lbl_error.Text = "اشکال در متن پیام. پیام ارسال نشد!";
                return;
            }
            string captcha = this.Session["oldCaptchaImageText"].ToString();
            if (!captcha.Equals(txt_cap.Text))
            {
                lbl_error.Text = "اشکال در عبارت امنیتی. پیام ارسال نشد!";
                return;

            }

            var resp = Callapi_savemessage();
            resp.Wait();
            string i = JsonConvert.DeserializeObject<string>(resp.Result);

            if (myconvert.toint(i) > 0)
            {
                txtpayam.Text = "";
                lbl_error.Text = "";
                txtmbl.Text = "";
                txtname.Text = "";
            } else
            {
                lbl_error.Text = "اشکال در ثبت";
            }
        }

        /* string heroslider()
         {
             DataView dv = sqhand.SqlExecute("select * from heros", "dv");


             string respond = @"<div class=""hero-items owl-carousel"" style=""text-align: center;text-shadow: 0px 0px 3px #a454b5,0px 0 5px darkgray;"">";
             foreach(DataRowView dr in dv)
             {
                 respond += @"<div class=""single-hero-items set-bg"" data-setbg=""" + dr["img"].ToString().Trim() 
                     +@"""><div class=""container""><div class=""row""><div class=""col-lg-12""><span>" 
                     + dr["category"].ToString().Trim() + "</span><h1>" + dr["header"].ToString().Trim() + "</h1><p style=\"color:black\">" 
                     + dr["body"].ToString().Trim() +
                       @"</p><a href=""" + dr["link"].ToString().Trim() +@""" class=""primary-btn"">" + dr["action"].ToString().Trim() + @"</a></div> </div><div class=""off-card"">
                   <h2>" + dr["offcard1"].ToString().Trim() + "<span>" + dr["offcard2"].ToString().Trim() + "</span></h2></div></div></div>";
             }

             respond += "</div>";

             return respond;
         }
         string banners()
         {
             DataView dv = sqhand.SqlExecute("select * from banners", "dv");
             string respond = @"   <div class=""banner-section spad"">
   <div class=""container-fluid"">
       <div class=""row"">";
             foreach (DataRowView dr in dv)
             {
                 respond += @"<div class=""col-lg-4""> <div class=""single-banner"">
                   <img src=""" + dr["img"].ToString().Trim() + @""" alt="""" />
                   <div class=""inner-text"">
                       <a href=""" + dr["link"].ToString().Trim() + @"""><h4>" + dr["header"].ToString().Trim() + @"</h4></a>
                   </div>
               </div>
           </div>";
             }

             respond += "</div>  </div></div>";

             return respond;
         }*/


    }
}
