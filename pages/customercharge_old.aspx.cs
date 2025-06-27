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
using Telerik.Web.UI.PageLayout;


namespace narsShop
{
    public partial class customercharge_old : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;
        string paytype="";
        string orderid = "";
        string source = "";
        //const string banksite = "https://abshode.net";
        const string banksite = "https://ghestitala.com";
        protected void Page_Load(object sender, EventArgs e)
        {

           
            tn = (token)Session["token"];

            if (tn.Token == null)
                Response.Redirect("~");


            if (Request["type"].Equals("W"))
            {
                lbl_title.Text = "شارژ کیف پول ";
                paytype = "W";
                Btn_fkif.Enabled = false;
                txt_pr.Enabled = false;
            }
            if (Request["type"].Equals("D"))
            {
                lbl_title.Text = " پرداخت قسط ";
                paytype= "D";
                Btn_fkif.Enabled = true;
                txt_pr.Enabled = true;
            }
            if (Request.Params.AllKeys.Contains("value"))
            {
                txt_pr.Text = Request["value"];
                txtprice.Text = Request["value"];
                txt_mablagh.Text = Request["value"];
            }



            lbl_customername.Text = tn.Name;
            lbl_customerphone.Text = tn.mobileno;
            lbl_customercode.Text = tn.vas;
            Dictionary<string, string> customerdic = decode.customerinfo(tn.Token, Session["apiurl"].ToString());
            lbl_kif.Text = myconvert.todecimal(customerdic["walet"]).ToString("0,0");
            lbl_totalbed.Text = myconvert.todecimal(customerdic["mande"]).ToString("0,0");
            lbl_points.Text = customerdic["point"];
            lbl_customeraddress.Text = customerdic["address"];
            lbl_shmeli.Text = customerdic["shmeli"];

            if (!IsPostBack)
            {
                txtdate.Text = persiandate.datef();
            }

        
            //linkshargkif.NavigateUrl = "https://ghestitala.com/pages/customercharge_dargah2.aspx?type=W&tokenid=" + tn.Token;
        }

        protected void Btnsend_Click(object sender, EventArgs e)
        {
            string orderid = "W";
            try
            {
                orderid = Request["factor"].ToString().Trim();
            }
            catch { }

            if (myconvert.toint(txtkart.Text) <1 ||  txtkart.Text.Trim().Length < 4)
            {
                Response.Write(String.Format("<script>alert('اشکال در شماره کارت. پیام ارسال نشد!')</script>"));
                return;
            }
            if (txtpeigiri.Text.Trim().Length < 4 || txtpeigiri.Text.Trim().Equals(tn.vas) || txtpeigiri.Text.Trim().Equals(orderid))
            {
                Response.Write(String.Format("<script>alert('اشکال در شماره پیگیری. لطفا بعد از انجام کارت به کارت شماره پیگیری اعلام شده از بانک را وارد کنید!')</script>"));
                return;
            }
            if (txtprice.Text.Trim().Length < 5)
            {
                Response.Write(String.Format("<script>alert('اشکال در مبلغ. پیام ارسال نشد!')</script>"));
                return;
            }
            if (txtdate.Text.Trim().Length < 5 || !persiandate.isvalid(txtdate.Text) || persiandate.datediff(persiandate.datef(), txtdate.Text)<0)
            {
                Response.Write(String.Format("<script>alert('اشکال در تاریخ. پیام ارسال نشد!')</script>"));
                return;
            }



            string payam = "$$$ : " + orderid.Trim() + " ~ " + txtkart.Text.Trim() + " ~ " + txtprice.Text.Trim() + " ~ " + txtdate.Text.Trim()+" ~ "+txtpeigiri.Text.Trim();

            var resp = Callapi_pay(tn.Token, payam);
            resp.Wait();

            if (!resp.Result.ToUpper().Equals("ERROR") && !resp.Result.StartsWith("-"))
            {
                Response.Write(String.Format("<script>alert('اطلاعات واریزی شما با موفقیت دریافت شد و در حال بررسی می باشد. پس از تایید از طریق پیامک به شما اطلاع داده خواهد شد')</script>"));
                txtkart.Text = "";
                txtprice.Text = "";
                txtdate.Text = "";
            }
            else
            {
                if (resp.Result.Trim().Equals("-1"))
                {
                    Response.Write(String.Format("<script>alert('این واریزی در حال بررسی است، منتظر پیامک تایید باشید')</script>"));
                } else if (resp.Result.Trim().Equals("-2"))
                {
                    Response.Write(String.Format("<script>alert('این واریزی قبلا در صورت حساب شما اعمال شده است')</script>"));
                }
                else
                {
                    Response.Write(String.Format("<script>alert('اشکال در ثبت')</script>"));
                }
            }
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

        protected void Btn_fkif_Click(object sender, EventArgs e)
        {
            long Amount = myconvert.toint(txt_pr.Text.Trim()) ;
            string orderid = Request["factor"].ToString().Trim();

            if (Amount < 100000) {
                Response.Write(String.Format("<script>alert('حداقل مبلغ پرداخت 10.000 تومان می باشد')</script>"));
                return; 
            }

            if (Amount > myconvert.toint(lbl_kif.Text))
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

        protected void linkdargah_Click(object sender, EventArgs e)
        {
            string redirecturl = "";
if (myconvert.todecimal(txt_mablagh.Text)<10000) return;

            if (paytype == "D")
                redirecturl=(banksite+"/pages/customercharge_dargah2.aspx?tokenid=" + tn.Token + "&type=" + paytype + (myconvert.todecimal(txt_mablagh.Text) > 0 ? "&value=" + txt_mablagh.Text.Replace(",","") : "") + (paytype.Equals("D") ? "&factor=" + Request["factor"].ToString().Trim() : "")+ "&dargah="+rb_dargah.SelectedValue);
            if (paytype == "W")
                redirecturl=(banksite+"/pages/customercharge_dargah2.aspx?tokenid=" + tn.Token + "&type=" + paytype + (myconvert.todecimal(txt_mablagh.Text) > 0 ? "&value=" + txt_mablagh.Text.Replace(",","") : "") + (paytype.Equals("D") ? "&factor=" + Request["factor"].ToString().Trim() : "") + "&dargah=" + rb_dargah.SelectedValue);
    Response.Redirect(redirecturl);
        }
    }


    

}
