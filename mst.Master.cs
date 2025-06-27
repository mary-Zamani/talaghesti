using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TalaModelLibrary;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;

namespace narsShop
{
    public partial class mst : System.Web.UI.MasterPage
    {
        protected string s1, s2, s3, s4;
        protected void Page_Load(object sender, EventArgs e)
        {
         lbl_loginpart.Text = loadpart("login");
            //  lbl_roydad.Text = loadnews("رویداد");

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
            //if (txt_mblno.Text.Trim().Length > 8 && tn.Token == null)
            //{ tn.Token = txt_mblno.Text.Trim();
            //    tn.mobileno = txt_mblno.Text.Trim(); }

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
                        sqhand.SqlExecute("insert into basket (basket_id,tokenid,etiket,kcode,vazn,price,takhfif) values (" + bs_no.ToString() + ",N'" + tn.vas + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ","+et.mablaghtakhfif.ToString()+")");
                        ++inserted;
                    }
                }
                if (inserted > 0)
                    sqhand.SqlExecute("insert into basket_main (basket_id,prepay,ghest,tokenid,tarikh,saat) values(" + bs_no.ToString() + "," + prepay.ToString() + "," + ghest.ToString() + ",N'" + tn.vas + "','" + persiandate.datef() + "','" + persiandate.timef() + "')");

                //hd_showpop.Value = "1";
            }

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

        etiket callapi_getetiketinfo(string etiket)
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

                json = System.Text.Json.JsonSerializer.Deserialize<etiket>(readTask.Result);
            }

            return json;
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

            string olgoo = $" <a href=\"[link]\" class=\"shopping-cart-css\"><i class=\"ri-shopping-bag-line\"></i>سبد خرید <p class=\"shopping-cart-count-css\">[count]</p></a>";


            if (Session["token"].ToString().Equals(""))
                stsql = "select * from basket where sessionid='" + Session.SessionID + "'";
            else
            {
                TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
                stsql = "select * from basket where sessionid='" + Session.SessionID + "' or tokenid='" + tn.vas + "'";
            }
            DataView basket = sqhand.SqlExecute(stsql, "dv");





            if (basket.Count == 0)
                return olgoo.Replace("[link]", "#").Replace("[count]", "");
else
                return olgoo.Replace("[link]", "/pages/customershop.aspx").Replace("[count]", basket.Count.ToString().Trim());

/*            respond = @"<div class=""navbar-nav navbar-nav-hover align-items-lg-center shadow-soft rounded border border-light"" title=""سبد خرید""><a class=""nav-item dropdown mega-dropdown"" href=""/pages/customershop.aspx"">

<a href=""/pages/customershop.aspx"" class=""nav-link"" data-toggle=""dropdown"">
<span class=""nav-link-inner-text""><i class=""fa fa-shopping-cart""></i></span> <span class=""badge badge-danger text-uppercase"">" + basket.Count + @"</span></a>

                        <div class=""dropdown-menu table-responsive-sm shadow-soft rounded"">
                    
                                  <table class=""table table-hover shadow-inset rounded"">
                                      <tbody>";
            foreach (DataRowView dr in basket)
            {
                respond += @"<tr><td>"+decode.k2name(dr["kcode"].ToString()) + @"</td><td class=""si-pic""><img src=""/img/kcode/"+ dr["kcode"].ToString().Trim() + @".jpg"" style=""max-width:150px"" /></td>
                                              <td class=""si-text"">
                                                      <p>وزن : " + dr["vazn"].ToString() + @"</p>
</td><td>
                                                      <h6>اتیکت :" + dr["etiket"].ToString() + @"</h6>
                                              </td>
                                              <td class=""si-close""> <button  class=""btn"" onclick=""removefrombasket(" + dr["etiket"].ToString().Trim()+ @")"">
                                                  <i class=""fa fa-close""></i> حذف</button>
                                              </td>
                                          </tr> ";
            }

            decimal vaznbasket = myconvert.todecimal(basket.Table.Compute("sum(vazn)", ""));
            decimal pricebasket = myconvert.todecimal(basket.Table.Compute("sum(price)", ""));
            respond += @" </tbody>
                                  </table>
                              
                              <div class=""row select-total"" style=""text-align: right;"">
                                  <div class=""col""><span>جمع:</span> </div>
                                  <div class=""col""><h5>" + vaznbasket.ToString().Trim() + @" گرم</h5></div>
                                  <div class=""col""><h5>" + pricebasket.ToString("0,0") + @" ریال</h5></div>
                                  <div class=""col select-button""> <a href = ""/pages/customershop.aspx"" class=""btn btn-primary text-success"">پرداخت</a>  </div>
                          </div></div>";


            return respond;
*/
        }

        protected void dosearch_Click(object sender, EventArgs e)
        {
            decimal prepay = myconvert.todecimal(txt_prepay.Text);
            if (prepay > 500000)
                Response.Redirect("~/pages/pricesearch.aspx?price=" + prepay.ToString());
       }


        public void settitle(string _title)
        {
            this.Page.Title = _title;
        }
        public void setmeta(string _HttpEquiv, string _Name, string _Content)
        {


            //Add Keywords Meta Tag.
            HtmlMeta keywords = new HtmlMeta();
            keywords.HttpEquiv = _HttpEquiv;
            keywords.Name = _Name;
            keywords.Content = _Content;

            cphPageMetaData.Controls.Add(keywords);

           

        }
        public void setmetaproperty(string _property,string _content)
        {
            HtmlMeta tag = new HtmlMeta();
            tag.Attributes.Add("property", _property);
            tag.Content = _content; // don't HtmlEncode() string. HtmlMeta already escapes characters.
            cphPageMetaData.Controls.Add(tag);
        }
    }
}

