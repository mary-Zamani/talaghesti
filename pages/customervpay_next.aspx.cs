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
using RestSharp;

namespace narsShop
{
    public partial class customervpay_next : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand= new SQLH();
            var collection = HttpUtility.ParseQueryString(this.ClientQueryString);
            String trans_id = collection["trans_id"];
            String order_id = collection["order_id"];
            String np_status = collection["np_status"];


            if (np_status.ToUpper().StartsWith("UN"))
            {
                Response.Write("<script>alert('تراکنش ناموفق')</script>");
                return;
            }

            Dictionary<string, string> payinfo = getpayinfo(trans_id);

            if (payinfo is null || payinfo.Count == 0)
            {
                Response.Write("<script>alert('سوابق تراکنش یافت نشد')</script>");
                return;
            }
            if (!payinfo["respond"].Trim().Equals(""))
            {
                Response.Write("<script>alert('تراکنش تکراری')</script>");
                return;
            }

            long Amount = myconvert.toint(payinfo["amount"]);

            var client = new RestClient("https://nextpay.org/nx/gateway/verify");            
            var request = new RestRequest("https://nextpay.org/nx/gateway/verify", RestSharp.Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("api_key", "803a4a6d-65a6-4583-8660-01859617f427");
            request.AddParameter("amount", Amount.ToString());
            request.AddParameter("trans_id", trans_id);
            RestResponse verificationResponse = client.Execute(request);

            if (verificationResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                nextverify nxr = JsonConvert.DeserializeObject<nextverify>(verificationResponse.Content);
                var resp = Callapi_vpay(payinfo["token"], trans_id,  nxr.Shaparak_Ref_Id);
                resp.Wait();

                string apirespond = resp.Result;

                if (resp.Result.ToLower().StartsWith("ok"))
                {

                    Response.Write(String.Format("<script>alert('پرداخت موفق {0}')</script>", nxr.Shaparak_Ref_Id));                    
                    string stsql = "select * from basket where tokenid='" + payinfo["token"] + "'";                
                    DataView basket = sqhand.SqlExecute(stsql, "dv");
                    if (basket.Count == 0)
                        Response.Redirect("customeraccount.aspx");
                    else
                        Response.Redirect("customershop.aspx");
                } else
                {
                    Response.Write(String.Format("<script>alert('خطا در ثبت تراکنش')</script>"));
                }
            }
            else
            {

                Response.Write(String.Format("<script>alert('پرداختت ناموفق {0}')</script>", verificationResponse.StatusCode));

            }


        }


        Dictionary<string, string> getpayinfo(string authority)
        {
            Dictionary<string, string> respond = new Dictionary<string, string>();

            string apiUrl = Session["apiurl"]+"/api/payamount/" + authority;
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
            string apiUrl = Session["apiurl"]+"/api/payaccept/"; //
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


    class nextverify
    {
        public int code { get; set; }
        public int amount { get; set; }
        public string order_id { get; set; }
        public string card_holder { get; set; }
        public string customer_phone { get; set; }
        public string Shaparak_Ref_Id { get; set; }
        public string custom { get; set; }
        public string created_at { get; set;}
    }

}
