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

namespace narsShop
{
    public partial class anj : System.Web.UI.MasterPage
    {
        protected string s1, s2, s3, s4;
        protected void Page_Load(object sender, EventArgs e)
        {
         lbl_loginpart.Text = loadpart("login");
            //  lbl_roydad.Text = loadnews("رویداد");

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
            if (txt_mblno.Text.Trim().Length > 8 && tn.Token == null)
            { tn.Token = txt_mblno.Text.Trim(); tn.mobileno = txt_mblno.Text.Trim(); }

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
                foreach (string aetiket in etiket_list)
                {
                    etiket et = callapi_getetiketinfo(aetiket);
                    sqhand.SqlExecute("insert into basket (basket_id,tokenid,etiket,kcode,vazn,price) values (" + bs_no.ToString() + ",N'" + tn.vas + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ")");
                }

                sqhand.SqlExecute("insert into basket_main (basket_id,prepay,ghest,tokenid,tarikh,saat) values("+ bs_no.ToString() + ","+prepay.ToString()+","+ghest.ToString()+ ",N'" + tn.vas + "','"+persiandate.datef()+"','"+persiandate.timef()+"')");

                hd_showpop.Value = "1";
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

                json = JsonSerializer.Deserialize<etiket>(readTask.Result);
            }

            return json;
        }

        string loadpart(string newstype)
        {
            string respond = "";

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

            if (tn.Token == null)
            {
                respond += "<a href=\"/weblogin.aspx\" class=\"login-panel\">ورود<i class=\"fa fa-user\"></i></a>";
            }
            else
            {
                respond += "<a href=\"/pages/customeraccount.aspx\" class=\"login-panel\">حساب من<i class=\"fa fa-user\"></i></a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp";
                respond += "<a href=\"/pages/customerexit.aspx\" class=\"login-panel\">خروج<i class=\"fa fa-close\"></i></a>";

            }
            return respond;
        }


        string shopcart()
        {
            SQLH sqhand = new SQLH();
            string respond = "";
            string stsql = "";

            if (Session["token"].ToString().Equals(""))
                stsql = "select * from basket where sessionid='" + Session.SessionID + "'";
            else
            {
                TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];
                stsql = "select * from basket where sessionid='" + Session.SessionID + "' or tokenid='" + tn.vas + "'";
            }
            DataView basket = sqhand.SqlExecute(stsql, "dv");
            if (basket.Count == 0)
                return @"<ul class=""nav-right"" style=""list-style-type: none"">
                         <li class=""cart-icon""><a href=""#""><i class=""icon_bag_alt""></i></a></li>
                         </ul>";

            respond = @"<ul class=""nav-right"">
                    
                      <li class=""cart-icon"">
                          <a href=""#"">
                              <i class=""icon_bag_alt""></i>
                              <span>" + basket.Count + @"</span>
                          </a>
                          <div class=""cart-hover"">
                              <div class=""select-items"">
                                  <table>
                                      <tbody>";
            foreach (DataRowView dr in basket)
            {
                respond += @"<tr><td class=""si-pic""><img src=""/img/kcode/"+ dr["kcode"].ToString().Trim() + @".jpg"" /></td>
                                              <td class=""si-text"">
                                                  <div class=""product-selected"">
                                                      <p>" + dr["vazn"].ToString() + @"</p>
                                                      <h6>" + dr["etiket"].ToString() + @"</h6>
                                                  </div>
                                              </td>
                                              <td class=""si-close""> <button  class=""btn"" onclick=""removefrombasket(" + dr["etiket"].ToString().Trim()+ @")"">
                                                  <i class=""ti-close""></i></button>
                                              </td>
                                          </tr> ";
            }

            decimal vaznbasket = myconvert.todecimal(basket.Table.Compute("sum(vazn)", ""));
            decimal pricebasket = myconvert.todecimal(basket.Table.Compute("sum(price)", ""));
            respond += @" </tbody>
                                  </table>
                              </div>
                              <div class=""select-total"">
                                  <span>جمع:</span>
                                  <h5>"+ vaznbasket.ToString().Trim() + @" گرم</h5>
                                  <h5>"+ pricebasket.ToString("0,0") + @" ریال</h5>
                              </div>
                              <div class=""select-button"">
                                  <a href = ""#"" class=""primary-btn view-card"">سبد خرید</a>
                                  <a href = ""#"" class=""primary-btn checkout-btn"">پرداخت</a>
                              </div></div></li></ul>";


            return respond;

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

