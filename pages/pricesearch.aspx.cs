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
using narsShop.component;
using ExtensionMethods;

namespace narsShop
{
    public partial class pricesearch : System.Web.UI.Page
    {
        SQLH sqhand;
        string parent;
        decimal price;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
             price = myconvert.todecimal(Request.Params["price"])* decode.getvarb("pish5").ToDecimal();

            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            string openrow = "<div class=\"w3-row\">";//@"<div class=""row owl-carousel"" style=""display:flex !important"">"
            string opencol = "<div class=\"w3-col m4\">";//@"<div class=""col-3 product-item table-bordered"">
            string cardbody = "<div class=\"w3-card w3-round w3-white w3-center\"> <div class=\"w3-container\">" +
                "<p>{caption}</p>{image} <p><strong>{sline1}</strong></p> <p>{sline2}</p>" +
                "<p><button class=\"w3-button w3-block w3-theme-l4\">{info}</button></p></div> </div>";
            int rownumber = 0;
            int colnumber = 0;
            string parentname = "";
            string respond = "";
            List<etiket> listetiket = callapi(price);

            List<string> kcodes=new List<string>();
            foreach(etiket item in listetiket)
            {
                if (!kcodes.Contains(item.kcode))
                {
                    kcodes.Add(item.kcode);
                }
            }
            kcodes.Sort();
            int colno = 0;
            decimal pishpp = decode.getvarb("pish5").ToDecimal();
            foreach (string dr in kcodes)
            {
                List<etiket> lst = listetiket.Where(et => et.kcode == dr).ToList<etiket>();
                string a_respond = "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td width=\"15%\"></td><td>وزن</td><td>قیمت</td><td>پیش پرداخت</td></tr>";
                etiket Aetiket = lst[0];
                a_respond += "<tr><td>" + "<input type=\"button\" onclick=\"addtobasket(" + Aetiket.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/>" + "</td><td>" + Aetiket.vaznmande.ToString() + "</td><td>" + (Math.Ceiling(Aetiket.price / 10000) * 10000).ToString("0,0") + "</td><td>" + (Math.Ceiling(Aetiket.price / (pishpp * 10000)) * 10000).ToString("0,0") + "</td></tr>";
                a_respond += "</table>";

                if (colnumber == 0)
                    respond += openrow;
                respond += opencol;
                respond += cardbody.Replace("{caption}", parentname)
                    .Replace("{image}", "<a href=\"product.aspx?kcode=" + dr + "\">" +
                    "<img src=\"../img/kcode/" + dr.Trim() + ".jpg\" style=\"width:100%;height:400px;\"  alt=\"" + decode.k2name(dr).Trim() + "\" /></a>")
                    .Replace("{sline1}", decode.k2name(dr).Trim())
                    .Replace("{sline2}", dr.ToString().Trim())
                    .Replace("{info}", a_respond);
                respond += "</div>";

                /*                respond += @"<div class=""col-3 product-item table-bordered""><div class=""pi-pic"">";
                                respond += "<a href=\"product.aspx?kcode=" + dr + "\"><img src=\"";
                                respond += "../img/kcode/" + dr + @".jpg"" alt="""" /></a>";
                                respond += @"<div class=""icon""><i class=""icon_heart_alt""></i></div>";
                                respond += @"<ul><li class=""quick-view""><a href=""product.aspx?kcode=" + dr + @"""> اطلاعات بیشتر </a></li></ul></div>";
                                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + parentname + "</div>";
                                respond += @"<a href=""#""><h5>" + decode.k2name(dr).Trim() + "</h5></a>";
                                respond += @"<div class=""product-price"">";
                */

                /*                List<etiket> lst = listetiket.Where(et => et.kcode == dr).ToList<etiket>();
                                respond += "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td width=\"15%\"></td><td>وزن</td><td>قیمت</td><td>پیش پرداخت</td></tr>";
                                etiket Aetiket = lst[0];
                                             respond += "<tr><td>" + "<input type=\"button\" onclick=\"addtobasket(" + Aetiket.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/>" + "</td><td>" + Aetiket.vaznmande.ToString() + "</td><td>" + (Math.Ceiling(Aetiket.price / 10000) * 10000).ToString("0,0") + "</td><td>" + (Math.Ceiling(Aetiket.price / 40000) * 10000).ToString("0,0") + "</td></tr>";
                                respond += "</table></div></div></div>";
                */

                colnumber++;
                if (colnumber == 3)
                {
                    respond += "</div>";
                    colnumber = 0;
                }

/*                ++colno;
                if (colno == 4)
                {
                    respond += "</div>";
                    colno = 0;
                }
*/            }





      /*      foreach(etiket dr in listetiket)
            {
                if (colno==0)
                    respond += @"<div class=""row owl-carousel"" style=""display:flex !important"">";
                *//*                respond += @"<div class=""col product-item""><div class=""pi-pic""><img src=""";
                                respond += "../img/kcode/" + dr.kcode + @".jpg"" alt="""" />";
                                //if (myconvert.toint16(dr["sale"]) == 1) respond += @"<div class=""sale"">Sale</div>";
                                respond += @"<div class=""icon""><i class=""icon_heart_alt""></i></div>";
                                respond += @"<ul><li class=""quick-view""><a href=""#"">+ خرید </a></li></ul></div>";
                                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + dr.category + "</div>";
                                respond += @"<a href=""#""><h5>" + decode.k2name(dr.kcode).Trim() +"</h5></a>";
                                respond += @"<div class=""product-price"">";
                                respond += "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td></td><td>وزن</td><td>قیمت</td></tr>";
                                respond += "<tr><td>"+"<input type=\"button\" onclick=\"addtobasket("+ dr.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/>"+"</td><td>" + dr.vaznmande.ToString() + "</td><td>"+ (Math.Ceiling(dr.price/10000)*10000).ToString("0,0")+"</td></tr>";
                                respond += "<tr><td>" +  dr.cert + "</td><td>" + (Math.Ceiling(((dr.price- (dr.price/4))*(decimal)1.25/5) / 10000) * 10000).ToString("0,0") +" و 5 قسط"+ "</td><td>" + (Math.Ceiling(dr.price/40000) * 10000).ToString("0,0") +"پیش پرداخت : "+ "</td></tr>";

                                respond +="</table></div></div></div>";
                *//*
                respond += productitem.productcard(dr);
                ++colno;
                if (colno == 4)
                {
                    respond += "</div>";
                    colno = 0;
                }
            }*/
            
            return respond;
        }

        List<etiket> callapi(decimal pr1)
        {
            
            List<etiket> json = new List<etiket>();

            string apiUrl = Session["apiurl"]+"/api/etiket/getetiketofprice/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(pr1.ToString());
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


   /*     etiket callapi3(string etiket)
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

        }*/
    }
}
