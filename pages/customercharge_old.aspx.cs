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
    public partial class customercharge_old : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;
        string paytype="";
        string orderid = "";
        string source = "";
        protected void Page_Load(object sender, EventArgs e)
        {

           
            tn = (token)Session["token"];

            if (tn.Token == null)
                Response.Redirect("~");


            if (Request["type"].Equals("W"))
            {
                lbl_title.Text = "شارژ کیف پول";
                lbl_subtit.Text = "لطفا مبلغ مورد نظر جهت شارژ کیف پول را وارد نمایید";
                paytype = "W";
                Btn_fkif.Enabled = false;
            }
            if (Request["type"].Equals("D"))
            {
                lbl_title.Text = "پرداخت قسط";
                lbl_subtit.Text = "لطفا مبلغ قسط مورد نظر جهت پرداخت را وارد کنید";
                paytype= "D";
                Btn_fkif.Enabled = true;
            }
            if (Request.Params.AllKeys.Contains("value"))
                txt_pr.Text = Request["value"];
          



            lbl_customername.Text = tn.Name;
            lbl_customerphone.Text = tn.mobileno;
            lbl_customercode.Text = tn.vas;
            Dictionary<string, string> customerdic = decode.customerinfo(tn.Token, Session["apiurl"].ToString());
            lbl_kif.Text = myconvert.todecimal(customerdic["walet"]).ToString("0,0");
            lbl_totalbed.Text = myconvert.todecimal(customerdic["mande"]).ToString("0,0");
            lbl_points.Text = customerdic["point"];
            lbl_customeraddress.Text = customerdic["address"];
            lbl_shmeli.Text = customerdic["shmeli"];

        }



        async Task<string> Callapi_pay(string Token, string authority,long amount,string ptype,string factor)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/payrequest/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "token", Token },  
                  { "authority", authority },
                  { "amount",amount.ToString()  },
                  { "paytype",ptype },
                {"factor",factor }
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

        protected void Btn_next_Click(object sender, EventArgs e)
        {
            long Amount = myconvert.toint(txt_pr.Text.Trim()) / 10;
            if (Amount < 1000)
            {
                Response.Write(String.Format("<script>alert('حداقل مبلغ پرداخت 1000 تومان می باشد')</script>"));
                return;
            }
            if (paytype.Equals("W"))
            {
                orderid = GetRandomAlphaNumeric();
            }else
            {
                orderid = Request["factor"].ToString().Trim();
            }
            
            var client = new RestClient("https://nextpay.org/nx/gateway/token");
            var request = new RestRequest("https://nextpay.org/nx/gateway/token",RestSharp.Method.Post);
            
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("api_key", "803a4a6d-65a6-4583-8660-01859617f427");
            request.AddParameter("amount", Amount.ToString());
            request.AddParameter("order_id", orderid);
            request.AddParameter("customer_phone", tn.mobileno);
            //request.AddParameter("custom_json_fields", "{ \"productName\":\"شارژ کیف پول\" , \"id\":"+tn.vas+" }");
            request.AddParameter("callback_uri", "http://talaghesti.com/pages/customervpay_next.aspx");
            RestResponse response = client.Execute(request);

            nextrespond nr = JsonConvert.DeserializeObject<nextrespond>(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = Callapi_pay(tn.Token, nr.trans_id, Amount, paytype, orderid);
                resp.Wait();

                if (resp.Result.ToUpper().Equals("OK"))
                    Response.Redirect("https://nextpay.org/nx/gateway/payment/" + nr.trans_id);
                else
                {
                    Response.Write(String.Format("<script>alert('به علت اشکال در سرور امکان اتصال به درگاه پرداخت نمی باشد')</script>"));
                }
                    

            }
        }

        protected void Btn_zarin_Click(object sender, EventArgs e)
        {
            ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();

            String MerchantID = "c5f58444-f418-11ea-afe6-000c295eb8fc";
            String CallbackURL = "http://talaghesti.com/pages/customervpay.aspx";
            long Amount = myconvert.toint(txt_pr.Text.Trim()) / 10;
            if (Amount < 1000)
            {
                return;
            }
            String Description = "شارژ کیف پول - طلافروشی نشاط";

            ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);



            var res = zarinpal.InvokePaymentRequest(pr);

            if (res.Status == 100)
            {

                var resp = Callapi_pay(tn.Token, res.Authority, Amount, "W", "0");
                resp.Wait();

                string respond = JsonConvert.DeserializeObject<string>(resp.Result);
                if (respond.Equals("OK"))
                    Response.Redirect(res.PaymentURL);
            }

        }

        public static string GetRandomAlphaNumeric()
        {
            var random= new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(12).ToArray());
        }

        protected void Btn_fkif_Click(object sender, EventArgs e)
        {
            long Amount = myconvert.toint(txt_pr.Text.Trim()) / 10;
            string orderid = Request["factor"].ToString().Trim();

            if (Amount < 1000) {
                Response.Write(String.Format("<script>alert('حداقل مبلغ پرداخت 1000 تومان می باشد')</script>"));
                return; 
            }

            if (Amount*10 > myconvert.toint(lbl_kif.Text))
            {
                Response.Write(String.Format("<script>alert('.مبلغ نمی تواند بیش از موجودی کیف پول باشد. موجودی کیف پول شما "+ lbl_kif.Text.Trim()+"')</script>"));
                return; 
            }

            var resp = Callapi_payfromkif(tn.Token, Amount, orderid);
            resp.Wait();
            if (resp.Result.ToLower().StartsWith("ok"))
            {

                Response.Write(String.Format("<script>alert('پرداخت موفق {0}')</script>", resp.Result));
                Response.Redirect("customeraccount.aspx");
            }
            else
            {
                Response.Write(String.Format("<script>alert('خطا در ثبت تراکنش')</script>"));
            }
        }


        async Task<string> Callapi_payfromkif(string Token, long amount, string factor)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/payacceptfromkif/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var values = new Dictionary<string, string>
              {
                  { "token", Token },
                  { "amount",amount.ToString()  },
                  {"factor",factor }
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


    public class nextrespond
    {
       public  int code { get; set; }
       public string trans_id { get; set; }
    }

}
