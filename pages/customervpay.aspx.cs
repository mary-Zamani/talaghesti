using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TalaModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZarinPal;
using System.Text;
using System.Threading.Tasks;

namespace narsShop
{
    public partial class customervpay : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;

        protected void Page_Load(object sender, EventArgs e)
        {

            var collection = HttpUtility.ParseQueryString(this.ClientQueryString);
            String Status = collection["Status"];


            if (Status != "OK")
            {
                Response.Write("<script>alert('Purchase unsuccessfully')</script>");
                return;
            }



            var zarinpal = ZarinPal.ZarinPal.Get();
            String Authority = collection["Authority"];
            String MerchantID = "c5f58444-f418-11ea-afe6-000c295eb8fc";
            Dictionary<string, string> payinfo = getpayinfo(Authority);
            long Amount = myconvert.toint(payinfo["amount"]);


            

            var verificationRequest = new ZarinPal.PaymentVerification(MerchantID, Amount, Authority);
            var verificationResponse = zarinpal.InvokePaymentVerification(verificationRequest);
            if (verificationResponse.Status == 100)
            {
                var resp = Callapi_vpay(payinfo["token"], Authority,  verificationResponse.RefID);
                resp.Wait();

                string apirespond = JsonConvert.DeserializeObject<string>(resp.Result);


                Response.Write(String.Format("<script>alert('Purchase successfully with ref transaction {0}')</script>", verificationResponse.RefID));
            }
            else
            {

                Response.Write(String.Format("<script>alert('Purchase unsuccessfully Error code is: {0}')</script>", verificationResponse.Status));

            }


        }


        Dictionary<string, string> getpayinfo(string authority)
        {
            Dictionary<string, string> respond = new Dictionary<string, string>();

            string apiUrl = Session["apiurl"]+"/api/customer/payinfo/" + authority + "/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(authority);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                respond = JsonConvert.DeserializeObject<Dictionary<string,string>>(readTask.Result); ;
            }

            return respond;

        }


        async Task<string> Callapi_vpay(string Token, string authority, string rfid)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/payaccept/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "token", Token },
                  { "authority", authority },
                  { "rfid",rfid  }
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




    }
}
