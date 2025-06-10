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

namespace narsShop
{
    public partial class biojratlist : System.Web.UI.Page
    {
        SQLH sqhand;
        string parent;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
           

            string parameter = Request["__EVENTARGUMENT"]; // parameter
            if (parameter != null)
            {
                btn_add_Click(btn_add, EventArgs.Empty,parameter);
            }
            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            string parentname = "";
            string respond = "";
            List<etiket> listetiket = callapi();

           

            int colno = 0;

            foreach(etiket dr in listetiket)
            {
                if (colno==0)
                    respond += @"<div class=""row owl-carousel"" style=""display:flex !important"">";
                respond += @"<div class=""col product-item""><div class=""pi-pic""><img src=""";
                respond += "../img/kcode/" + dr + @".jpg"" alt="""" />";
                //if (myconvert.toint16(dr["sale"]) == 1) respond += @"<div class=""sale"">Sale</div>";
                respond += @"<div class=""icon""><i class=""icon_heart_alt""></i></div>";
                respond += @"<ul><li class=""quick-view""><a href=""#"">+ خرید </a></li></ul></div>";
                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + dr.category + "</div>";
                respond += @"<a href=""#""><h5>" + decode.k2name(dr.kcode).Trim() +"</h5></a>";
                respond += @"<div class=""product-price"">";

               

                respond += "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td></td><td>وزن</td><td>قیمت</td></tr>";
              
                    respond += "<tr><td>"+"<input type=\"button\" onclick=\"addtobasket("+ dr.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/>"+"</td><td>" + dr.vaznmande.ToString() + "</td><td>"+ (Math.Ceiling(dr.price/10000)*10000).ToString("0,0")+"</td></tr>";
               
                respond +="</table></div></div></div>";
                ++colno;
                if (colno == 4)
                {
                    respond += "</div>";
                    colno = 0;
                }
            }
            
            return respond;
        }

        List<etiket> callapi()
        {
            string kcode = "0";
            List<etiket> json = new List<etiket>();

            string apiUrl = Session["apiurl"] + "/api/etiket/getetiketofbiojrat/";
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




        etiket callapi3(string etiket)
        {
            etiket json = new etiket();
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/etiket/getetiketinfo/";
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
    }
}
