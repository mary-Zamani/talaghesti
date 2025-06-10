using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using ExtensionMethods;
using System.Windows.Documents;

namespace narsShop
{
    public partial class takhfiflist : System.Web.UI.Page
    {
        SQLH sqhand;
        //string parent;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
  
            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            decimal pishpp = 100;
            string parentname = "";
            string respond = "";
            string openrow = "<div class=\"row mb-f justify-content-between\">";//@"<div class=""row owl-carousel"" style=""display:flex !important"">"
            string opencol = "<div class=\"col-12 col-md-6 col-lg-4 mb-6 mb-md-5\">";//@"<div class=""col-3 product-item table-bordered"">
            string cardbody = "<div class=\"card bg-primary border-light shadow-soft\"> {image} <div class=\"card-body\" style=\"text-align:center\">" +
                              "<span class=\"fas fa-medal mr-2\"></span><span class=\"alert alert-warning\">فقط نقدی</span>" +
                "<h3 class=\"h5 card-title mt-3\">{caption}</h3> <p class=\"card-text\"><strong>{sline1}</strong></p> <p class=\"card-text\">{sline2}</p>" +
                "<p><div class=\"btn btn-block btn-lg\">{info}</div></p></div> </div>";
            int rownumber = 0;
            int colnumber = 0;

            List<etiket> listetiket = callapi();
            if (listetiket.Count == 0)
            {
                string resp = @"
<br><br>
<h3>به بخش طلای تخفیفی سایت طلاقسطی خوش اومدی </h3>
<br>
یکی از مطمئن ترین راه های حفظ سرمایه تبدیل کردن اون به طلا هست. 
و یکی از دغدغه های کسانی که به طلا فقط و فقط به دید سرمایه نگاه می کنن این هست که بتونن طلاهایی تهیه کنن که موقع فروش و تبدیل مجدد به پول افت کمتری داشته باشه
<br>
ما در این بخش قصد داریم برای جلب این بخش از بازار طلا و جهت رضایتمندی مشتریان عزیزمون بخش محدودی از طلاهای موجود رو به شکل طلای تخفیفی و با حداقل سود ممکن عرضه کنیم. بالطبع تعداد این  آیتم ها محدود هست 
و اینکه با توجه به سود حداقلی اونها امکان فروش اونها به صورت اقساطی رو نداریم.

";

                return resp;
            }
            List<string> kcodes=new List<string>();
            foreach(etiket item in listetiket)
            {
                if (!kcodes.Contains(item.kcode))
                {
                    kcodes.Add(item.kcode);
                }
            }

            int colno = 0;
            kcodes.Sort();

          


            foreach (string dr in kcodes)
            {
                if (colnumber == 0)
                    respond += openrow;

                List<etiket> lst = listetiket.Where(et => et.kcode == dr).ToList<etiket>();
                string a_respond = "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td>وزن</td><td>قیمت</td><td><span class=\"alert-warning\">تخفیف</span></td></tr>";
                etiket Aetiket = lst[0];
                a_respond += "<tr>" + "<td>" + Aetiket.vaznmande.ToString() + "</td><td>" + (Math.Ceiling(Aetiket.price / 10000) * 10000).ToString("0,0") + "</td><td><span class=\"alert-warning\">" + (Math.Ceiling(Aetiket.mablaghtakhfif / 10000) * 10000).ToString("0,0") + "</span></td></tr>";
                a_respond += "<tr><td>مبلغ بعد از تخفیف</td><td colspan=2>" +(Aetiket.price-Aetiket.mablaghtakhfif).ToString("0,0")+ "</td></tr>";
                a_respond += "</table>";
                //"<input type=\"button\" onclick=\"addtobasket('" + Aetiket.cert + ":" + (Math.Ceiling(Aetiket.price / (pishpp * 10000)) * 10000).ToString() + ":" + (Math.Ceiling(Aetiket.price /(pishpp* 10000)) * 60 * 125).ToString() + "')\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/>" + 

                respond += opencol;
                respond += cardbody.Replace("{caption}", parentname)
                    .Replace("{image}", "<a href=\"productt.aspx?cert=" + Aetiket.cert + "&kcode="+dr+"\">"+
                    "<img src=\"../img/kcode/" + dr.Trim() + ".jpg\" style=\"width:100%;height:400px;\"  alt=\""+ decode.k2name(dr).Trim() + "\" /></a>")
                    .Replace("{sline1}", decode.k2name(dr).Trim())
                    .Replace("{sline2}", Aetiket.cert)
                    .Replace("{info}", "<a href=\"productt.aspx?cert=" + Aetiket.cert + "&kcode="+dr+"\">" + a_respond+ "</a>");
                respond += "</div>";


                colnumber++;
                if (colnumber == 3)
                {
                    respond += "</div>";
                    colnumber = 0;
                }
            }

            return respond;
        }

        List<etiket> callapi()
        {
            List<etiket> json = new List<etiket>();

            string apiUrl = Session["apiurl"]+ "/api/etiket/getetiketofbiojrat/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync("0");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<List<etiket>>(readTask.Result);
            }

            return json;
        }


        List<etiket> callapi2(string kcode)
        {
            List<etiket> json = new List<etiket>();
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/etiket/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(kcode);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<List<etiket>>(readTask.Result);
            }

            return json;
        }


/*        etiket callapi3(string etiket)
        {
            etiket json = new etiket();
            DataTable dt = new DataTable();
            string apiUrl = "http://37.32.126.10:8000/api/etiket/getetiketinfo/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(etiket);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<etiket>(readTask.Result);
            }

            return json;
        }
        protected void btn_add_Click(object sender, EventArgs e,string aetiket)
        {
            sqhand = new SQLH();
            etiket et = callapi3(aetiket);
            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

            if (tn.Token == null) { 
                sqhand.SqlExecute("insert into basket (sessionid,etiket,kcode,vazn,price) values ('" + Session.SessionID + "','" + aetiket + "','"+et.kcode+"',"+et.vaznmande.ToString()+","+(Math.Ceiling(et.price/10000)*10000).ToString()+")");
            }
            else
            {
                sqhand.SqlExecute("insert into basket (tokenid,etiket,kcode,vazn,price) values ('" + tn.Token + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ")");
            }

        }
*/    }
}
