using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;

namespace narsShop
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected string s1, s2, s3, s4;
      
    
        public bool ShowBbreadcumb
        {
            get { return sectionBbreadcumb.Visible; }
            set { sectionBbreadcumb.Visible = value; }
        }
        public bool showsectionShop
        {
            get { return sectionShop.Visible; }
            set { sectionShop.Visible = value; }
        }
   
        public Repeater RepeaterProducts
        {
            get { return rptProducts; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //lbl_loginpart.Text = loadpart("login");
         

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
        

            HttpCookie cookie = Request.Cookies["Token"];
            if (cookie != null && tn.Token == null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    token tk = JsonConvert.DeserializeObject<token>(cookie.Value);
                    if (!string.IsNullOrEmpty(tk.mobileno) && !string.IsNullOrEmpty(tk.shmeli))
                        Response.Redirect(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 10)) + "/weblogin.aspx?r=r&tok=" + tk.mobileno + tk.shmeli);
                }

            }
            string parameter = Request["__EVENTARGUMENT"]; // parameter
            if (parameter != null && Request["__EVENTTARGET"].Equals("btn_rem"))
            {
                btn_remove_Click(null, EventArgs.Empty, parameter);
            }
            if (parameter != null && Request["__EVENTTARGET"].Equals("btn_add"))
            {
                btn_add_Click(null, EventArgs.Empty, parameter);
            }

            lbl_shopcart.Text = shopcart();
            hd_tok.Value = tn.mobileno;
        }
        string loadpart(string newstype)
        {
            string respond = "";
            string olgoo = $"<a href=\"[link]\"class=\"login-link-css\">[esm]</a>";

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
            if (tn.Token == null)

                return olgoo.Replace("[link]", "/weblogin.aspx").Replace("[esm]", "ورود");
            else
                return olgoo.Replace("[link]", "/pages/customeraccount.aspx").Replace("[esm]", "حساب من");

        }
        string shopcart()
        {
            SQLH sqhand = new SQLH();
            string respond = "";
            string stsql = "";
           
            string olgoo = $" <span id=\"cart-count\" class=\"cart-count\">[count]</span>";


            if (Session["token"].ToString().Equals(""))
                stsql = "select * from basket where sessionid='" + Session.SessionID + "'";
            else
            {
                TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
                stsql = "select * from basket where sessionid='" + Session.SessionID + "' or tokenid='" + tn.vas + "'";
            }
            DataView basket = sqhand.SqlExecute(stsql, "dv");





            if (basket.Count == 0)
                return olgoo.Replace("[count]", "");
            else
                return olgoo.Replace("[count]", basket.Count.ToString().Trim());

          
        }
        protected void btn_remove_Click(object sender, EventArgs e, string aetiket)
        {
            SQLH sqhand = new SQLH();
            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

            if (tn.Token == null)
            {
                sqhand.SqlExecute("delete from basket where sessionid='" + Session.SessionID + "' and etiket= '" + aetiket + "'");
            }
            else
            {
                sqhand.SqlExecute("delete from basket where tokenid='" + tn.vas + "' and etiket='" + aetiket + "'");
            }
            Response.Redirect("/pages/customershop.aspx");
        }
        protected void btn_add_Click(object sender, EventArgs e, string paramlist)
        {
            SQLH sqhand = new SQLH();



            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

            if (tn.Token == null)
            {

                //sqhand.SqlExecute("insert into basket (sessionid,etiket,kcode,vazn,price) values ('" + Session.SessionID + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ")");
                Response.Redirect("../weblogin.aspx?why=1");
            }
            else
            {
                string[] paramlists = paramlist.Split(new char[] { ':' });
                string etikets = paramlists[0];
                string[] etiket_list = etikets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                long bs_no = myconvert.toint(sqhand.SqlExecute("select max(basket_id+1) from basket_main", "dv")[0][0]);
                long prepay = myconvert.toint(paramlists[1]);
                long ghest = myconvert.toint(paramlists[2]);
                int inserted = 0;
                foreach (string aetiket in etiket_list)
                {
                    etiket et = callapi_getetiketinfo(aetiket);
                    if (sqhand.SqlExecute("select * from basket where etiket='" + aetiket + "' and tokenid='" + tn.vas + "'", "dv").Count == 0)
                    {
                        sqhand.SqlExecute("insert into basket (basket_id,tokenid,etiket,kcode,vazn,price,takhfif) values (" + bs_no.ToString() + ",N'" + tn.vas + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + "," + et.mablaghtakhfif.ToString() + ")");
                        ++inserted;
                    }
                }
                if (inserted > 0)
                    sqhand.SqlExecute("insert into basket_main (basket_id,prepay,ghest,tokenid,tarikh,saat) values(" + bs_no.ToString() + "," + prepay.ToString() + "," + ghest.ToString() + ",N'" + tn.vas + "','" + persiandate.datef() + "','" + persiandate.timef() + "')");

                //hd_showpop.Value = "1";
            }

        }
        etiket callapi_getetiketinfo(string etiket)
        {
            etiket json = new etiket();
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/etiket/getetiketinfo/";
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

                json = System.Text.Json.JsonSerializer.Deserialize<etiket>(readTask.Result);
            }

            return json;
        }

        //string loadpart(string newstype)
        //{
        //    string respond = "";
        //    string olgoo = $"<a href=\"[link]\"class=\"login-link-css\">[esm]</a>";

        //    TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
        //    if (tn.Token == null)

        //        return olgoo.Replace("[link]", "/weblogin.aspx").Replace("[esm]", "ورود");
        //    else
        //        return olgoo.Replace("[link]", "/pages/customeraccount.aspx").Replace("[esm]", "حساب من");

        //}
    }
}