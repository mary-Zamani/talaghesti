using ExtensionMethods;
using narsShop.component;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TalaModelLibrary;

namespace narsShop
{
    public partial class productt : System.Web.UI.Page
    {
        SQLH sqhand;
        string kcode,cert;
        etiket etikets;
        decimal pishpp = 1.0M;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            kcode = Request["kcode"].Trim();
            cert = Request["cert"].Trim();
            sqhand = new SQLH();

            showproduct();
            
            
        }


        string callapi(string etiket)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+ "/api/etiket/getetiketinfo_biojrat/";
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

                json = readTask.Result;
            }

            return json;
        }

        void showproduct()
        {
           
            DataView dv = sqhand.SqlExecute("select * from kcodes where kcode ='" + kcode + "'", "dv");
            if (dv.Count > 0)
            {
                DataRowView dr = dv[0];
                Lbl_title.Text = dr["persian"].ToString().Trim();

                Master.settitle("طلافروشی نشاط - طلا قسطی- طلای تخفیفی-" + dv[0]["persian"].ToString().Trim());
                Master.setmeta("description", "description", dv[0]["persian"].ToString().Trim());
                Master.setmeta("author", "author", "seenseen");
                Master.setmetaproperty("og:image", "../img/kcode/" + dr["kcode"].ToString().Trim() + ".jpg");
                Master.setmetaproperty("og:title", "طلافروشی نشاط - طلا قسطی- طلای تخفیفی-" + dv[0]["persian"].ToString().Trim());
                Master.setmetaproperty("og:description", dv[0]["persian"].ToString().Trim());
                Master.setmetaproperty("og:url", Request.Url.AbsoluteUri);

                productimage.ImageUrl = "../img/kcode/" + dr["kcode"].ToString().Trim() + ".jpg";

            }


            int rownum = 0;
            int colnum = 0;
            

            string respstr = callapi(cert);
            etikets = JsonSerializer.Deserialize<etiket>(respstr);

            List<string> colors = new List<string>();
            List<string> options = new List<string>();


            string selectredoption = " ";
            string selectedcolor = " ";
            string selectedcount = "1";
            
            if (IsPostBack)
            {
                if (Drp_size.Items.Count > 0 && Drp_size.SelectedIndex>0)
                    selectredoption = Drp_size.SelectedValue;

                if (Drp_color.Items.Count > 0 && Drp_color.SelectedIndex > 0)
                    selectedcolor = Drp_color.SelectedValue;

                if (Drp_count.Items.Count > 0 && Drp_count.SelectedIndex > 0)
                    selectedcount = Drp_count.SelectedValue;
            }

            Drp_color.Items.Clear();
            etiket item = etikets;
                {
                    if (!colors.Contains(item.color)) // &&(item.options.Equals(selectredoption) || selectredoption.Equals(""))
                    colors.Add(item.color);
                    if (!options.Contains(item.options)) //&& (item.color.Equals(selectedcolor) || selectedcolor.Equals(""))
                    options.Add(item.options);
                }
                if (colors.Count < 2)
                {
                    Drp_color.Visible = false;
                }
                else
                {
                    Drp_color.Visible = true;
                    Drp_color.Items.Add(new ListItem("همه رنگها", " "));
                    foreach (string citem in colors)
                        if (!citem.Trim().Equals(""))
                            Drp_color.Items.Add(new ListItem(citem.Trim(), citem.Trim()));
                }
            Drp_size.Items.Clear();
            if (options.Count < 2)
                {
                    Drp_size.Visible = false;
                }
                else
                {
                    Drp_size.Visible = true;
                    //Drp_size.Items.Add(new ListItem("همه سایزها", " "));
                    foreach (string oitem in options)
                        if (!oitem.Trim().Equals(""))
                            Drp_size.Items.Add(new ListItem(oitem.Trim(), oitem.Trim()));
                }

            if (Drp_size.Items.Count > 0 && !selectredoption.Trim().Equals(""))
                Drp_size.SelectedValue = selectredoption;
            if (Drp_color.Items.Count > 0)
                Drp_color.SelectedValue = selectedcolor;

                int counter = 1;

            Drp_count.Items.Clear();
                for (int i=1;i<= counter; ++i)
            {
                Drp_count.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            if (Drp_count.Items.FindByValue(selectedcount) != null)
                Drp_count.SelectedValue = selectedcount;

            display_items(etikets);



        }

        void display_items(etiket etikets)
        {
            string respond = "";
            respond += @"<div class=""row owl-carousel"" style=""display:flex !important"">";
            respond += @"<div class=""col"">";
            respond += @"<div class=""pi-text""><div class=""catagory-name"">" + "" + "</div>";
            respond += @"<div class=""product-price""><span></span></div>";
            respond += productdetailstr(etikets);
            respond += "</div></div></div>";
            lbl_product.Text = respond;
        } 
        string productdetailstr(etiket etikets)
        {
           
            string respond = "";
//            if (etikets.Length > 0)
            {

                int counter = 1;
                decimal totalweight = 0;
                decimal totalprice = 0;
                decimal price = 0;
                decimal takhfif = 0;
                string etiket_list = "";
                etiket Aetiket = etikets;
                {
                    ++counter;
                    totalweight += Aetiket.vaznmande;
                    totalprice += Aetiket.price-Aetiket.mablaghtakhfif;
                    etiket_list += Aetiket.cert + ",";
                    price += Aetiket.price;
                    takhfif += Aetiket.mablaghtakhfif;
                }

                respond = "<lable id =\"kol_" + kcode + "\" style=\"visibility:hidden\" >" + (Math.Ceiling(totalprice / 10000) * 10000).ToString() + "</lable>";
                respond += "<table class=\"table table-advance table-hover\" style=\"text-align:center\">";
                respond += "<tr style=\"background:golden\"><th>قیمت نقد</th><th>تعداد</th><th>جمع وزن</th></tr>";
                respond += "<tr style=\"background:silver\"><th>"+ (Math.Ceiling(totalprice / 10000) * 10000).ToString("0,0") + "</th><th>" + (counter-1).ToString() + "</th><th>"+ totalweight+"</th></tr>";
                respond += "<tr><td>مبلغ اصلی </td><td>";
                respond += "<lable id=\"pishp_" + kcode + "\" >" + (Math.Ceiling(price / 10000) * 10000).ToString("0,0") + "</lable></td><td>";
                respond += "<input  readonly=readonly onchange=\"rs_change(this.value," + kcode + ");\" id=\"RS_" + kcode + "\" type=\"range\" class=\"form-range\" min=\"" + (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000) + "\" max=\"" + (Math.Ceiling(totalprice / 10000) * 10000) + "\" step=\"10000\"  value=\"" + (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000) + "\"/>";
                respond += "</td></tr><tr><td> مبلغ تخفیف";
                respond += "</td><td>" + "<lable id=\"ghest_" + kcode + "\" >" + Math.Ceiling((takhfif / 10000) * 10000).ToString("0,0") + "</lable></td></tr>";
                respond += "<tr><td colspan=3><input type=\"button\" onclick=\"addtobasket('" + etiket_list + "')\" class=\"btn btn-sm btn-block\" title=\"اضافه به سبد خرید\" value=\"اضافه به سبد خرید\"/></td></tr>";
                respond += "</table>";

                hid_pishp.Value = (Math.Ceiling(totalprice / 10000) * 10000).ToString();
                hid_ghest.Value = "0";

                counter = 1;
                respond += "<table class=\"table table-advance table-hover\" style=\"text-align:center\">";
                respond += "<tr><td> اتیکت</td><td>انبار</td><td>سایز</td><td>رنگ</td><td>وزن</td></tr>";
                Aetiket = etikets;
                {
                    ++counter;

                    respond += "<tr><td>" + Aetiket.cert + "</td><td>";
                    if (Aetiket.anbar == "31002")
                        respond += "آریاشهر";
                    else
                        respond += "شهرری";

                    respond += "</td>";
                    respond += "<td>" + Aetiket.options + "</td><td>"+Aetiket.color+ "</td><td>" + Aetiket.vaznmande.ToString() + "</td></tr>";
                }
                respond += "</table>";
            }

            return respond;
        }

        protected void drp_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_items(etikets);
        }


        /*    protected void btn_add_Click(object sender, EventArgs e, string aetiket)
            {
                sqhand = new SQLH();
                etiket et = callapi3(aetiket);
                TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

                if (tn.Token == null)
                {
                    sqhand.SqlExecute("insert into basket (sessionid,etiket,kcode,vazn,price) values ('" + Session.SessionID + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ")");
                }
                else
                {
                    sqhand.SqlExecute("insert into basket (tokenid,etiket,kcode,vazn,price) values ('" + tn.Token + "','" + aetiket + "','" + et.kcode + "'," + et.vaznmande.ToString() + "," + (Math.Ceiling(et.price / 10000) * 10000).ToString() + ")");
                }

            }*/

        /*    etiket callapi3(string etiket)
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
            }*/
    }
}
