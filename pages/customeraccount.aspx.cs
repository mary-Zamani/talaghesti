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
using TalaModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace narsShop
{
    public partial class customeraccount : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;

        protected void Page_Load(object sender, EventArgs e)
        {
            tn = (token)Session["token"];

            if (tn.Token == null)
                Response.Redirect("~");

            lbl_customername.Text = tn.Name;
            lbl_customerphone.Text = tn.mobileno;
            lbl_customercode.Text = tn.vas;
            Dictionary<string, string> customerdic = decode.customerinfo(tn.Token, Session["apiurl"].ToString());
            if (customerdic.Count > 0)
            {
                lbl_kif.Text = myconvert.todecimal(customerdic["walet"]).ToString("0,0");
                lbl_totalbed.Text = myconvert.todecimal(customerdic["mande"]).ToString("0,0");
                lbl_points.Text = customerdic["point"];
                lbl_customeraddress.Text = customerdic["address"];
                lbl_shmeli.Text = customerdic["shmeli"];
            }
            foreach (string factorno in dplist())
            {

                lbl_customerdps.Text += dptable(factorno, tn.vas);
            }
        }

        string dptable(string thisfactor, string vascode, bool Cb_showalldp = false)
        {
            factorinfo factorinfo1 = dpdetail(thisfactor);
            string respond = "";

           if (factorinfo1.koldaftar.Count == 0 && persiandate.datediff("1400/11/20", factorinfo1.solddate) > 0) return respond;
           if (factorinfo1.tasvierooz == 0 && factorinfo1.khoshhesabirooz == 0) return respond;
           if (factorinfo1.Rdetails != null) return respond;

            if (factorinfo1.factortype == 2) return respond; // نوع نقدی

            decimal peyed = myconvert.todecimal(factorinfo1.details["kif"]) +
                 myconvert.todecimal(factorinfo1.details["kart"]) +
                 myconvert.todecimal(factorinfo1.details["naghd"]) +
                 myconvert.todecimal(factorinfo1.details["buyprice"]);
            decimal mablaghghesti = myconvert.todecimal(factorinfo1.details["ghesti"]);
            decimal mablaghnaghdi = myconvert.todecimal(factorinfo1.details["naghdi"]);
            decimal hazine = myconvert.todecimal(factorinfo1.details["haml"]);
            decimal takhfif = myconvert.todecimal(factorinfo1.details["takhfif"]);

            respond = $"<div class=\"mb-f justify-content-between\"> " +
                      $"<div class=\"\">  " +
                      $"<div class=\"card bg-primary border-light shadow-soft\">" +
                      $"<div class=\"card-body\">";
            respond += "<h5 class=\"h5 card-title mt-3\">دفتر قسط شماره : " + thisfactor + "<br>";
            if (factorinfo1.details["sta"].ToString().Equals("S"))
            {
                respond += " مربوط به ";
                if (factorinfo1.kalaha != null)
                    foreach (Dictionary<string, object> drtmp in factorinfo1.kalaha)
                    {
                        respond += myconvert.todecimal(drtmp["meghdar"]).ToString().Trim() + " گرم " + decode.k2name(drtmp["mtcod"].ToString()).Trim() + "  ";
                    }

            }
            if (factorinfo1.details["sta"].ToString().Equals("R"))
            {
                respond += "تجدید قسط از دفتر شماره : " + factorinfo1.details["shomar2"].ToString() + " مربوط به<br>";
                factorinfo factorinfo2 = dpdetail(factorinfo1.details["shomar2"].ToString());
                if (factorinfo2.kalaha != null)
                    foreach (Dictionary<string, object> drtmp in factorinfo2.kalaha)
                    {
                        respond += myconvert.todecimal(drtmp["meghdar"]).ToString().Trim() + " گرم " + decode.k2name(drtmp["mtcod"].ToString()).Trim() + "  ";
                    }

            }
            respond += "</h5>";

            respond += "<div class=\"row mb-f justify-content-between\">";
            respond += "<div class=\"col mb-1 \" style=\"text-align:right;max-width:300px\">";
            respond += "<h6>دفتر قسط</h6>";
            respond += "<table class=\"table table-hover shadow-inset rounded\">" +
                "<tr><td>تاریخ</td><td>مبلغ</td></tr>";

            long comulative = 0;
            foreach (Dictionary<string, object> dr in factorinfo1.koldaftar)
            {
                respond += "<tr>";
                respond += "<td>" + dr["tres"].ToString() + "</td><td>" + myconvert.toint(dr["ghest"]).ToString("0,0") + "</td>";
                respond += "</tr>";
                comulative += myconvert.toint(dr["ghest"]);
            }
            respond += "<tr><td colspan=2>"+"جمع قسط ها: " + factorinfo1.jameaghsat.ToString("0,0")+"</td></tr>";

            respond += "</table>";
            respond += "</div>";

            respond += "<div class=\"col mb-1 \" style=\"float:right;text-align:right\">";
            respond += "<h6>اقساط پرداختی </h6>";
            respond += "<table class=\"table table-hover shadow-inset rounded\">" +
                "<tr><td>تاریخ</td><td>مبلغ</td><td>توضیحات</td></tr>";

            foreach (Dictionary<string, object> dr in factorinfo1.daryaftiha)
            {
                respond += "<tr ";
                if (dr["sta"].ToString().Equals("W")) respond += "style=\"color:red\"";
                respond += ">";
                respond += "<td>" + dr["solddate"].ToString() + "</td><td>" + myconvert.todecimal(dr["totalbed"]).ToString("0,0") + "</td><td>";
                if (dr["sta"].ToString().Equals("U")) respond += "مرجوعی";
                if (dr["sta"].ToString().Equals("W")) respond += "انتقال به کیف پول";
                string sharh = "";
                if (dr["sta"].ToString().Equals("D"))
                {


                    sharh = dr["sharh"].ToString().Trim();
                    if (myconvert.todecimal(dr["khoshhesabi"]) > 0) sharh += myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0") + " خوش حسابی  |";
                    if (myconvert.todecimal(dr["kif"]) > 0) sharh += myconvert.todecimal(dr["kif"]).ToString("0,0") + " از کیف |";
                    if (myconvert.todecimal(dr["kart"]) > 0)
                    {
                        sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                    }
                    if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی |";
                    if (myconvert.todecimal(dr["buyprice"]) > 0)
                    {
                        sharh += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                    }
                    if (myconvert.todecimal(dr["takhfif"]) > 0) sharh += myconvert.todecimal(dr["takhfif"]).ToString("0,0") + " تخفیف ویژه |";

                }

                if (dr["sta"].ToString().Equals("U"))
                {
                    sharh = dr["sharh"].ToString().Trim();
                    if (myconvert.todecimal(dr["buyhno"]) > 0)
                    {

                    }
                    if (myconvert.todecimal(dr["takhfif"]) != 0) sharh += " | برگشت از تخفیف" + myconvert.todecimal(dr["takhfif"]).ToString("0,0");
                    if (myconvert.todecimal(dr["khoshhesabi"]) != 0) sharh += " | برگشت سود تقسیط" + myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0");
                    if (myconvert.todecimal(dr["haml"]) != 0) sharh += " | برگشت از هزینه" + myconvert.todecimal(dr["haml"]).ToString("0,0");

                    if (dr["sta2"].ToString().Equals("4")) sharh += " وزن به وزن ";
                    if (dr["sta2"].ToString().Equals("2")) sharh += " مرجوع توافقی";
                    if (dr["sta2"].ToString().Equals("1")) sharh += " وزن به وزن ";
                    if (dr["sta2"].ToString().Equals("0")) sharh += " مرجوع کامل";

                }
                //respond += "<input type=\"button\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + sharh + "\" value=\"توضیحات\" class=\"btn btn-secondary\"/>";
                respond +="<span style=\"font-size:10px\">"+ sharh +"</span>";
                respond += "</td></tr>";
            }

            respond += "<tr><td colspan=3>" + "جمع پرداختی ها :" + factorinfo1.jamedaryaft.ToString("0,0") + "</td></tr>";
            respond += "</table>";
            respond += "</div></div>";

            respond += "<div class=\"row mb-f justify-content-between\">";
            respond += "<div class=\"col-lg-6 col-sm-8 mb-1\" style=\"float:right;text-align:right\">";
            respond += $"<div class=\"card pr-2 bg-primary shadow-soft border-light\">";
            respond += "بدهی از این فاکتور: " + factorinfo1.aslbedehi.ToString("0,0") + "<br>";
            if (factorinfo1.Fdetails == null && factorinfo1.Rdetails == null)
            {
                if (persiandate.datediff("1400/11/20", factorinfo1.solddate) > 0)
                {

                    respond += "خوش حسابی روزشمار:" + factorinfo1.khoshhesabirooz.ToString("0,0");

                    respond += "<br>";
                    respond += "مبلغ تسویه در روز:" + (factorinfo1.tasvierooz).ToString("0,0");

                }
                else
                {

                    respond += "خوش حسابی روزشمار:" + factorinfo1.khoshhesabirooz.ToString("0,0");

                    respond += "<br>";
                    respond += "مبلغ تسویه در روز:";

                    if (factorinfo1.tasvierooz >= 0)
                        respond += factorinfo1.tasvierooz.ToString("0,0")+ "<br>";
                    else
                        respond += "<span class=\"label label-danger\">" + factorinfo1.tasvierooz.ToString("0,0") + "</span><br>";
                    respond += "تاریخ محاسبه:"+persiandate.datef();
                }


            }
            respond += "</div> </div>";
            respond += "<div class=\"col-lg-6 col-sm-4 mb-1 \" style=\"text-align:right\">";
            respond += $"<a href=\"customercharge.aspx?type=D&factor=" + thisfactor + "\" class=\"btn btn-info btn-block\"><i class=\"fa fa-pencil\"></i> پرداخت قسط</a> ";
            respond += "</div></div>";
/*
            respond += "<table class=\"table table-advance table-bordered\">" +
                "<tr><td style=\"width:50%\">" + "" + "</td><td style=\"width:50%\">" + "" + "</td></tr>";
            respond += "<theaad><tr><td></td><td></td></tr></thead>";
            respond += "<tr><td>";
            respond += "</td><td>";

                        

            respond += "</td><td>";
            respond += "</td></tr></tfoot>";


            respond += "</table>";

*/
            respond+=    $" </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n<br>";




            return respond;

        }

        string[] dplist()
        {

            string[] json = null;
            string apiUrl = Session["apiurl"]+"/api/customer/dplist/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(tn.Token);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonConvert.DeserializeObject<string[]>(readTask.Result); ;
            }

            return json;


        }

        factorinfo dpdetail(string factorno)
        {
            factorinfo respond = new factorinfo();

            string apiUrl = Session["apiurl"]+"/api/customer/factorinfo/" + tn.Token + "/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(factorno);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                respond = JsonConvert.DeserializeObject<factorinfo>(readTask.Result); ;
            }

            return respond;

        }


    
    }
}
