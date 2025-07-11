﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;
using System.Text.Json;

namespace narsShop.pages
{
    public partial class customerticket : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;
        string paytype = "";
        string orderid = "";
        string source = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            tn = (token)Session["token"];

            if (tn.Token == null)
                Response.Redirect("~");


            lbl_subtit.Text = "لطفا در صوررت هرگونه مشکل آن را با ما درمیان بگزارید تا در اسرع وقت پیگیری شود";

            List<ticket> tickets = callapi(tn.Token);
            string resp = "<table class=\"table table-advance table-bordered\">";
            foreach (ticket t in tickets)
            {
                resp += "<tr><td class=\"card  shadow-inset border-light\">" + t.request + "<br>" + t.respond + "</td></tr>";
            }
            resp += "</table>";
            lbl_tbl.Text = resp;

           
            
        }
        async Task<string> Callapi_pay(string Token, string contnt)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/tickets/send";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "token", Token },
                  { "contnt", contnt }
            };

            var content = new FormUrlEncodedContent(values);


            string resp = Newtonsoft.Json.JsonConvert.SerializeObject(values);
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

        protected void Btn_save_Click(object sender, EventArgs e)
        {
            var resp = Callapi_pay(tn.Token, txt_cnt.Text.Trim());
            resp.Wait();

            if (!resp.Result.ToUpper().Equals("ERROR"))
                Response.Write(String.Format("<script>alert('تیکت ثبت شد')</script>"));
            else

                Response.Write(String.Format("<script>alert('اشکال در ثبت تیکت')</script>"));



        }


        List<ticket> callapi(string token)
        {
            List<ticket> json = new List<ticket>();

            string apiUrl = Session["apiurl"] + "/api/tickets/getlist/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(token);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<List<ticket>>(readTask.Result);
            }

            return json;
        }
    }
}