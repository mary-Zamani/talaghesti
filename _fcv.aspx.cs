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
    public partial class _fcv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fcno = Request["fcno"];
            if (fcno != null && fcno.Length>6)
            {
                string vas= fcno.Substring(0,6);
                string factor = fcno.Substring(6);

                var tk = Callapi_getinvoice(vas,factor);


                lbl_fcno.Text = factor;
                lbl_tarikh.Text = tk.solddate;
                lbl_name.Text = vas;

                if (tk.tasvierooz >0 )
                {
                    lbl_stat.Text = "دفترچه اقساط تسویه نشده و بصورت امانی می باشد";
                }else
                {
                    lbl_stat.Text = "این فاکتور تسویه شده و کالا تحویل مشتری شده است";
                }

                if (tk.details["sta"].ToString().Equals("S"))
                {
                    
                    if (tk.kalaha != null)
                        foreach (Dictionary<string, object> drtmp in tk.kalaha)
                        {
                            lbl_kala.Text += myconvert.todecimal(drtmp["meghdar"]).ToString().Trim() + " گرم " + decode.k2name(drtmp["mtcod"].ToString()).Trim() + "  ";
                        }

                }



            }

        }


        factorinfo Callapi_getinvoice(string vas,string factor)
        {
            factorinfo respond = new factorinfo();

            string apiUrl = Session["apiurl"] + "/api/customer/factorinfo/"+vas+"/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(factor);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                respond = JsonConvert.DeserializeObject<factorinfo>(readTask.Result); 
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