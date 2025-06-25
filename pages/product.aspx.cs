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

namespace narsShop.pages
{
    public partial class product : System.Web.UI.Page
    {
  
 
        SQLH sqhand;
        string kcode;
        etiket[] etikets;
        decimal pishpp;
        string etiket_list = "";
        protected void Page_Load(object sender, EventArgs e)
        {
        
            ((Main)this.Master).showsectionShop = false;
            firstetiket.Text = "";
            kcode = Request["kcode"].Trim();
            sqhand = new SQLH();
            pishpp = decode.getvarb("pish5").ToDecimal();
         
            showproduct();


        }


        string callapi(string kcode)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/etiket/";
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

                json = readTask.Result;
            }

            return json;
        }

        void showproduct()
        {

            string respstr = callapi(kcode);
            etikets = JsonSerializer.Deserialize<etiket[]>(respstr);

          
            if (etikets.Length > 0)
            {
                etiket dr = etikets[0];
                Lbl_title.Text = dr.kalaname;


                productimage.ImageUrl = "https://talaghesti.com/img/kcode/" + dr.kcode + ".jpg";

            }


            int rownum = 0;
            int colnum = 0;



            List<string> colors = new List<string>();
            List<string> options = new List<string>();


            string selectredoption = " ";
            string selectedcolor = " ";
            string selectedcount = "1";

            if (IsPostBack)
            {
                if (Drp_size.Items.Count > 0 && Drp_size.SelectedIndex > 0)
                    selectredoption = Drp_size.SelectedValue;

                if (Drp_color.Items.Count > 0 && Drp_color.SelectedIndex > 0)
                    selectedcolor = Drp_color.SelectedValue;

                if (Drp_count.Items.Count > 0 && Drp_count.SelectedIndex > 0)
                    selectedcount = Drp_count.SelectedValue;
            }

            Drp_color.Items.Clear();
            foreach (etiket item in etikets)
            {
                if (!colors.Contains(item.color))  
                    colors.Add(item.color);
                if (!options.Contains(item.options))  
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
                foreach (string item in colors)
                    if (!item.Trim().Equals(""))
                        Drp_color.Items.Add(new ListItem(item.Trim(), item.Trim()));
            }
            Drp_size.Items.Clear();
            if (options.Count < 2)
            {
                Drp_size.Visible = false;
            }
            else
            {
                Drp_size.Visible = true;
              
                foreach (string item in options)
                    if (!item.Trim().Equals(""))
                        Drp_size.Items.Add(new ListItem(item.Trim(), item.Trim()));
            }

            if (Drp_size.Items.Count > 0 && !selectredoption.Trim().Equals(""))
                Drp_size.SelectedValue = selectredoption;
            if (Drp_color.Items.Count > 0)
                Drp_color.SelectedValue = selectedcolor;

            int counter = 0;
            foreach (etiket Aetiket in etikets)
            {
                if (Drp_size.Items.Count > 0 && !Drp_size.SelectedValue.Trim().Equals(""))
                    if (!Drp_size.SelectedValue.Trim().Equals(Aetiket.options.Trim()))
                        continue;
                if (Drp_color.Items.Count > 0 && !Drp_color.SelectedValue.Trim().Equals(""))
                    if (!Drp_color.SelectedValue.Trim().Equals(Aetiket.color.Trim()))
                        continue;
                ++counter;
            }

            Drp_count.Items.Clear();
            for (int i = 1; i <= counter; ++i)
            {
                Drp_count.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            if (Drp_count.Items.FindByValue(selectedcount) != null)
                Drp_count.SelectedValue = selectedcount;

            display_items(etikets);



        }

        void display_items(etiket[] etikets)
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
        string productdetailstr(etiket[] etikets)
        {
            bool faghatnaghdi = false;
            string respond = "";
            if (etikets.Length > 0)
            {

                int counter = 1;
                decimal totalweight = 0;
                decimal totalprice = 0;
               

                foreach (etiket Aetiket in etikets)
                {
                    if (Drp_size.Items.Count > 0 && !Drp_size.SelectedValue.Trim().Equals(""))
                        if (!Drp_size.SelectedValue.Trim().Equals(Aetiket.options.Trim()))
                            continue;
                    if (Drp_color.Items.Count > 0 && !Drp_color.SelectedValue.Trim().Equals(""))
                        if (!Drp_color.SelectedValue.Trim().Equals(Aetiket.color.Trim()))
                            continue;
                    if (counter > myconvert.toint16(Drp_count.SelectedValue)) continue;
                    ++counter;
                    totalweight += Aetiket.vaznmande;
                    totalprice += Aetiket.price;
                    etiket_list += Aetiket.cert + ",";
                    if (Aetiket.faghatnaghdi) faghatnaghdi = true;
                }
                if (faghatnaghdi) pishpp = 1;

                respond = "<lable id =\"kol_" + kcode + "\" style=\"visibility:hidden\" >" + (Math.Ceiling(totalprice / 10000) * 10000).ToString() + "</lable>";
                respond += "<table class=\"table table-advance table-hover\" style=\"text-align:center\">";
                respond += "<tr style=\"background:golden\"><th>قیمت نقد</th><th>تعداد</th><th>جمع وزن</th></tr>";
                respond += "<tr style=\"background:silver\"><th>" + (Math.Ceiling(totalprice / 10000) * 10000).ToString("0,0") + "</th><th>" + (counter - 1).ToString() + "</th><th>" + totalweight + "</th></tr>";
                respond += "<tr><td>پیش پرداخت </td><td>";
                respond += "<lable id=\"pishp_" + kcode + "\" >" + (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000).ToString("0,0") + "</lable></td><td>";
                respond += "<input onchange=\"rs_change(this.value," + kcode + ");\" id=\"RS_" + kcode + "\" type=\"range\"   min=\"" + (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000) + "\" max=\"" + (Math.Ceiling(totalprice / 10000) * 10000) + "\" step=\"10000\"  value=\"" + (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000) + "\" " + (faghatnaghdi ? "readonly=true" : "") + " />";
                respond += "</td></tr><tr><td> اقسط 5 ماهه";
                respond += "</td><td>" + "<lable id=\"ghest_" + kcode + "\" >" + (Math.Ceiling(((totalprice - (totalprice / pishpp)) * (decimal)1.25 / 5) / 10000) * 10000).ToString("0,0") + "</lable></td></tr>";
                respond += "<tr><td colspan=3><a class='theme-btn' href='javascript:void(0)' onclick=\"addtobasket('" + etiket_list + "')\">افزودن سبد خرید<i class='fa-regular fa-cart-shopping bg-transparent text-white'></i></a></td></tr>";

                //respond += "<tr><td colspan=3><input type=\"button\"  onclick=\"addtobasket('" + etiket_list + "')\" class=\"theme-btn fa-regular fa-cart-shopping bg-transparent text-white\" title=\"اضافه به سبد خرید\" value=\"اضافه به سبد خرید\"/></td></tr>";
                respond += "</table>";

                hid_pishp.Value = (Math.Ceiling(totalprice / (pishpp * 10000)) * 10000).ToString();
                hid_ghest.Value = (Math.Ceiling(((totalprice - (totalprice / pishpp)) * (decimal)1.25 / 5) / 10000) * 10000).ToString();

                counter = 1;
                respond += "<table class=\"table table-advance table-hover\" style=\"text-align:center\">";
                respond += "<tr><td></td><td> اتیکت</td><td>انبار</td><td>سایز</td><td>رنگ</td><td>وزن</td></tr>";
                foreach (etiket Aetiket in etikets)
                {
                    if (Drp_size.Items.Count > 0 && !Drp_size.SelectedValue.Trim().Equals(""))
                        if (!Drp_size.SelectedValue.Trim().Equals(Aetiket.options.Trim()))
                            continue;
                    if (Drp_color.Items.Count > 0 && !Drp_color.SelectedValue.Trim().Equals(""))
                        if (!Drp_color.SelectedValue.Trim().Equals(Aetiket.color.Trim()))
                            continue;
                    if (counter > myconvert.toint16(Drp_count.SelectedValue)) continue;
                    ++counter;

                    if (firstetiket.Text.Trim().Equals("")) firstetiket.Text = Aetiket.cert.ToString();
                    respond += "<tr><td>" + Aetiket.color2 + "</td><td>" + Aetiket.cert + "</td><td>";
                    if (Aetiket.anbar == "31002")
                        respond += "آریاشهر";
                    else
                    {
                        if (Aetiket.anbar == "33002")
                            respond += "شهرری.";
                        else
                            respond += "شهرری";
                    }
                    respond += "</td>";
                    respond += "<td>" + Aetiket.options + "</td><td>" + Aetiket.color + "</td><td>" + Aetiket.vaznmande.ToString() + "</td></tr>";
                }
                respond += "</table>";
            }
            else
            {
                respond = "<p>" + "این کالا موجود نیست. می توانید سفارش دهید" + "</p>";
            }

            return respond;
        }

        protected void drp_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_items(etikets);
        }


    }
}