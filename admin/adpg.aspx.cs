using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;
using System.Text.Json;
using System.IO;
using System.Net;

namespace narsShop.admin
{
    public partial class adpg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            updatecategories_Click(null, null);
            updatekcodes_Click(null,null);
            updatekcodepics_Click(null, null);
            updatecategorypics_Click(null, null);

        }

        protected void updatekcodes_Click(object sender, EventArgs e)
        {
            SQLH sqhand = new SQLH();

            List<products> lp = callapi<products>("getnewproducts");
            foreach (products pr in lp)
            {
                if (sqhand.SqlExecute("select * from kcodes where kcode='" + pr.kcode + "'", "dv").Count > 0)
                    sqhand.SqlExecute("update kcodes set persian=N'"+pr.persian+ "',category='"+pr.category+"',grp1='" + pr.grp1 + "',grp2='" + pr.grp2 + "',grp3=N'" + pr.grp3 + "',grp4=N'" + pr.grp4 + "',brand='" + pr.brand + "',fi_es=" + pr.fi_es + ",fi_fi=" + pr.fi_fi + ",fi_td=" + pr.fi_td + " where kcode='" + pr.kcode + "'");
                else
                    sqhand.SqlExecute("insert into kcodes( kcode,persian,category,grp1,grp2,grp3,grp4,brand,fi_es,fi_fi,fi_td) values ('" + pr.kcode + "',N'" + pr.persian+ "','"+pr.category+"','"+pr.grp1+"','"+pr.grp2+"',N'"+pr.grp3+"',N'"+pr.grp4+"','"+pr.brand+"'," + pr.fi_es+","+pr.fi_fi+","+pr.fi_td+")");
            }

            callapi_long("setproductupdated");
        }

        /*   List<products> callapi1(string basedate)
           {
               List<products> json = new List<products>();

               string apiUrl = "http://37.32.126.10:8000/api/anbar/getnewproducts/";
               HttpClient client = new HttpClient();
               client.BaseAddress = new Uri(apiUrl);
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               var responseTask = client.GetAsync(basedate);
               responseTask.Wait();
               var result = responseTask.Result;
               if (result.IsSuccessStatusCode)
               {
                   var readTask = result.Content.ReadAsStringAsync();
                   readTask.Wait();

                   json = JsonSerializer.Deserialize<List<products>>(readTask.Result);
               }

               return json;
           }*/

        protected void updatekcodepics_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<string> lp = callapi<string>("getnewproductphotos");
            foreach (string pr in lp)
            {
                if (File.Exists("../img/kcode/" + pr.Trim() + ".jpg"))
                    File.Delete("../img/kcode/" + pr.Trim() + ".jpg");

                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        wc.DownloadFile(Session["siteurl"] +"/attachments/kcode/" + pr.Trim() + ".jpg", MapPath("../img/kcode/") + pr.Trim() + ".jpg");
                    }
                    catch { }
                }
                ++count;
               
            }
            callapi_long("setproductpicsupdated");
            Response.Write(count);
        }

        protected void updatecategorypics_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<string> lp = callapi<string>("getnewcategoryphotos");
            foreach (string pr in lp)
            {
                  if (File.Exists("../img/category/" + pr.Trim() + ".jpg"))
                      File.Delete("../img/category/" + pr.Trim() + ".jpg");

                                using (WebClient wc = new WebClient())
                                {

                                    try
                                    {
                                        wc.DownloadFile(Session["siteurl"] +"/attachments/categories/" + pr.Trim() + ".jpg", MapPath("../img/category/") + pr.Trim() + ".jpg");
                                    }
                                    catch { }
                                }
                ++count;
            }

            callapi_long("setcategorypicupdated");
            Response.Write(count);
        }

        protected void updatecategories_Click(object sender, EventArgs e)
        {
            SQLH sqhand = new SQLH();

            List<productgroup> lp = callapi<productgroup>("getnewcategories");
            foreach (productgroup pr in lp)
            {
                if (sqhand.SqlExecute("select * from categories where categoryid='" + pr.category + "'", "dv").Count > 0)
                    sqhand.SqlExecute("update categories set categoryname=N'" + pr.name + "',parentid='" + pr.parent + "',catalogview=" + pr.catalogview + " where categoryid='" + pr.category + "'");
                else
                    sqhand.SqlExecute("insert into categories( categoryid,categoryname,parentid,catalogview) values ('" + pr.category + "',N'" + pr.name + "','" + pr.parent + "'," + pr.catalogview + ")");
            }
            callapi_long("setcategoryupdated");
        }

        List<T> callapi<T>(string webapiname)
        {
            List<T> json = new List<T>();
            string basedate = "0";
            string apiUrl = Session["apiurl"]+"/api/anbar/" + webapiname.Trim() + "/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(basedate);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<List<T>>(readTask.Result);
            }

            return json;
        }


        long callapi_long(string webapiname)
        {
            long json=0;
            
            string basedate = "0";
            string apiUrl = Session["apiurl"]+"/api/anbar/" + webapiname.Trim() + "/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(basedate);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<long>(readTask.Result);
            }

            return json;
        }

        protected void sbtminpish_Click(object sender, EventArgs e)
        {
            if (minpass.Text.Trim().Equals("0442") && myconvert.todecimal(minpishpart.Text)>1)
            {
                SQLH sqhand = new SQLH();
                sqhand.SqlExecute("update varbs set varb_mgd='" + minpishpart.Text.Trim() + "' where varb_name='pish5'");
            }
        }
    }
}