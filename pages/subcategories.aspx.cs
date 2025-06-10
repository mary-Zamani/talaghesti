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
using System.Collections.Generic;
using TalaModelLibrary;
using System.Text.Json;

namespace narsShop
{
    public partial class subcategories : System.Web.UI.Page
    {
        SQLH sqhand;
        string parent;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
            parent = Request["parent"].ToString();
            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            string parentname = "";
            string respond = "";

            List<productgroup> scs = new List<productgroup>();

            scs = callapi(parent);

            int colno = 0;

            foreach(productgroup dr in scs)
            {
                if (colno == 0)
                    respond += @"<div class=""row owl-carousel"" style=""display:flex !important"">";

                respond += @"<div class=""col product-item""><div class=""pi-pic""><img src=""";
                respond += "../img/category/" + dr.category + @".jpg"" alt="""" />";
                respond += @"<div class=""icon""></div>";
                respond += @"<ul><li class=""quick-view""><a href=""productlist.aspx?parent=" + dr.category + @""">+ اطلاعات بیشتر </a></li>
                              
                          </ul></div>";
                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + parentname + "</div>";
                respond += @"<a href=""productlist.aspx?parent=" + dr.category + @"""><h5>" + dr.name +"</h5></a>";
                respond += @"<div class=""product-price"">";
                respond += dr.itemcount;
                respond += @"<span></span></div></div></div>";
                ++colno;
                if (colno == 4)
                {
                    respond += "</div>";
                    colno = 0;
                }
            }
            
            return respond;
        }

        List<productgroup> callapi(string kcode)
        {
            List<productgroup> json = new List<productgroup>();
            
            string apiUrl = Session["apiurl"]+"/api/etiket/subcategoriesinfo/";
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

                json = JsonSerializer.Deserialize<List<productgroup>>(readTask.Result);
            }

            return json;
        }
    }
}
