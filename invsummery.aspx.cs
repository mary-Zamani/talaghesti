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

namespace narsShop
{
    public partial class invsummery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string invc = Request["invc"];
            if (invc != null && invc.Length>4)
            {
                var tk = Callapi_getinvsum(invc);

                var soo = Callapi_getinvsarmaye(invc);

                lbl_name.Text = tk.name;
                lbl_tarikh.Text = tk.lastupdate;
                lbl_forosh.Text = tk.hajmforosh.ToString("0,0");
               
                
                lbl_sood.Text = tk.sood.ToString("0,0");
                string soorathesab = "واریز ها :<br>";
                soorathesab += "<table class=\"table table-condensed table-active\">";
                foreach(sooratsarmaye ssr in soo)
                {
                    if (ssr.sta.Equals("D"))
                    soorathesab += "<tr><td>" + ssr.totalbed.ToString("0,0") + "</td><td>" + ssr.tarikh + "</td></tr>";
                }
                soorathesab += "</table>";

                decimal totbardasht = 0;
                soorathesab += "برداشت ها :<br>";
                soorathesab += "<table class=\"table table-condensed table-active\">";
                foreach (sooratsarmaye ssr in soo)
                {
                    if (!ssr.sta.Equals("D"))
                    {
                        soorathesab += "<tr><td>" + ssr.totalbed.ToString("0,0") + "</td><td>" + ssr.tarikh + "</td></tr>";
                        totbardasht += ssr.totalbed;
                    }
                }
                soorathesab += "</table>";

                Label1.Text= soorathesab;
                lbl_sarmaye.Text = (tk.sarmaye+ totbardasht).ToString("0,0");
            }

        }


        investorsummery Callapi_getinvsum(string Txt_username)
        {
            investorsummery respond = new investorsummery();

            string apiUrl = Session["apiurl"] + "/api/investorsummery/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(Txt_username);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                respond = JsonConvert.DeserializeObject<investorsummery>(readTask.Result); 
            }

            return respond;

        }

        List<sooratsarmaye> Callapi_getinvsarmaye(string Txt_username)
        {
            List<sooratsarmaye> respond = new List<sooratsarmaye>();

            string apiUrl = Session["apiurl"]+"/api/investorpayments/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(Txt_username);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                respond = JsonConvert.DeserializeObject<List<sooratsarmaye>>(readTask.Result);
            }

            return respond;

        }
    }
}