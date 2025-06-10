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

namespace narsShop
{
    public partial class productlist_old : System.Web.UI.Page
    {
        SQLH sqhand;
        string parent;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
            parent = Request["parent"].ToString();

/*            string parameter = Request["__EVENTARGUMENT"]; // parameter
            if (parameter != null)
            {
                btn_add_Click(btn_add, EventArgs.Empty,parameter);
            }
*/   
            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            decimal pishpp = decode.getvarb("pish5").ToDecimal();
            string parentname = "";
            string respond = "";
            //string openrow = "<div class=\"row mb-f justify-content-between\">";//@"<div class=""row owl-carousel"" style=""display:flex !important"">"
            //string opencol = "<div class=\"col-12 col-md-6 col-lg-4 mb-6 mb-md-5\">";//@"<div class=""col-3 product-item table-bordered"">

            string cardbody = "<div class=\"product-card-css\"> <a href=\"{link}\"> <img src=\"{image}\" alt=\"\"> " +
                              "<div><h3>{caption}</h3> <p>{sline2}{sline1}</p> {badage}{info}</div> </a>  </div>";
            int rownumber = 0;
            int colnumber = 0;

            List<etiket> listetiket = callapi(parent);

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
                //if (colnumber == 0)
                //    respond += openrow;

                List<etiket> lst = listetiket.Where(et => et.kcode == dr).ToList<etiket>();
                string a_respond = "<table \"><tr><td>وزن</td><td>قیمت</td><td>پیش پرداخت</td></tr>";
                etiket Aetiket = lst[0];
                a_respond += "<tr>" + "<td>" + Aetiket.vaznmande.ToString().Trim() + "</td><td>" + (Math.Ceiling(Aetiket.price / 10000) * 10000).ToString("0,0") + "</td><td>" + (Math.Ceiling(Aetiket.price / ((Aetiket.faghatnaghdi?1:pishpp)* 10000)) * 10000).ToString("0,0") + "</td></tr>";
                a_respond += "</table>";

               // respond += opencol;
                respond += cardbody.Replace("{caption}", parentname.Trim())
                    .Replace("{image}", "../img/kcode/" + dr.Trim() + ".jpg")
                    .Replace("{sline1}", Aetiket.kalaname.Trim())
                    .Replace("{sline2}", "اتیکت"+":"+Aetiket.cert)
                    .Replace("{badage}",Aetiket.faghatnaghdi ? "<span class=\"fas fa-medal mr-2\"></span><span class=\"alert alert-warning\">فقط نقدی</span>":"")
                    .Replace("{info}", a_respond);

               // respond += "</div>";


                //colnumber++;
               // if (colnumber == 3)
               // {
               //     respond += "</div>";
               //     colnumber = 0;
               // }
            }

            return respond;
        }

        List<etiket> callapi(string kcode)
        {
            List<etiket> json = new List<etiket>();

            string apiUrl = Session["apiurl"]+"/api/etiket/getetiketofmaincategory/";
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
