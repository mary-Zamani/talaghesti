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
using ZarinPal;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace narsShop
{
    public partial class customershop_old : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;
        string paytype="";
        string orderid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
           
            tn = (token)Session["token"];

            if (tn.Token == null)
                Response.Redirect("~");

            
            string stsql = "select * from basket left join basket_main on basket.basket_id = basket_main.basket_id and " +
                " basket.tokenid = basket_main.tokenid where basket.sessionid='" + Session.SessionID + "' or basket.tokenid='" + tn.vas + "'";
            string _listkala = "";
            DataView basket = sqhand.SqlExecute(stsql, "dv");
            decimal total_price = 0;
            decimal total_ghest = 0;
            decimal total_pish = 0;
            int tedadghest = 0;
            decimal totaltakhfif = 0;
            foreach (DataRowView dr in basket)
            {




                _listkala += $"<div class=\"card mb-3\">" +
                      $" <div class=\"d-flex flex-row align-items-center\"> <table width=\"100%\"><tr><td rowspan=4>" +
                      $" <img src=\"/img/kcode/" + dr["kcode"].ToString().Trim() + $".jpg\" class=\"img-fluid rounded-3\" alt=\"" + decode.k2name(dr["kcode"].ToString()) + "\" style=\"width: 170px;\"></td><td>" +
                      $" <h5> " + decode.k2name(dr["kcode"].ToString()) + "</h5></td>"+
                      $" <td rowspan=4> <button class=\"btn btn-danger-outline\" onclick=\"removefrombasket(" + dr["etiket"].ToString().Trim() + ");\" style=\"color: #cecece;\"><i style=\"font-size:25px;color:red;\" class=\"fa fa-trash\"></i></button></td>" +
                      $"</tr><tr><td>" +
                      $"   "+ dr["etiket"].ToString() + "</td></tr><tr><td>" +
                      $"  <h5 class=\"fw-normal mb-0\">" + dr["vazn"].ToString().Trim() + $" گرم</h5></td>" +
                      
                      $" </tr><tr><td><h5 class=\"mb-0\">" + (myconvert.todecimal(dr["price"])- myconvert.todecimal(dr["takhfif"])).ToString("0,0") + "</h5></td></tr></table>" +
                      
                      $"</div></div>";


                total_price += myconvert.todecimal(dr["price"])- myconvert.todecimal(dr["takhfif"]);
                total_ghest += myconvert.todecimal(dr["ghest"]);
                total_pish += myconvert.todecimal(dr["prepay"]);
                totaltakhfif += myconvert.todecimal(dr["takhfif"]);
            }
            list_kala.Text= _listkala;
            l_ghest.Text = total_ghest.ToString("0,0");
            l_prepay.Text = total_pish.ToString("0,0");
            l_price.Text = total_price.ToString("0,0");
            if (totaltakhfif > 0) l_tedadghest.Text = "نقدی"; else l_tedadghest.Text = "5";

            lbl_customername.Text = tn.Name;
            lbl_customerphone.Text = tn.mobileno;
            lbl_customercode.Text = tn.vas;
            Dictionary<string, string> customerdic = decode.customerinfo(tn.Token, Session["apiurl"].ToString());
            lbl_kif.Text = myconvert.todecimal(customerdic["walet"]).ToString("0,0");
            lbl_totalbed.Text = myconvert.todecimal(customerdic["mande"]).ToString("0,0");
            lbl_points.Text = customerdic["point"];
            lbl_customeraddress.Text = customerdic["address"];
            lbl_shmeli.Text = customerdic["shmeli"];

        }







        async Task<savefactorrespond> Callapi_sale(shopcart shop_card)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"]+"/api/saveforosh";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            string resp = JsonConvert.SerializeObject(shop_card);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(apiUrl),
                Content = new StringContent(resp, Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = response;

            savefactorrespond respond= new savefactorrespond();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                respond = JsonConvert.DeserializeObject<savefactorrespond>(readTask.Result);
                
            }

            return respond;
        }

        protected void finalizasale_Click(object sender, EventArgs e)
        {

            shopcart _shopcart = new shopcart();
            _shopcart.tokenid = tn.Token;
            _shopcart.totalprice = myconvert.todecimal(l_price.Text);
            _shopcart.kif = myconvert.todecimal(l_prepay.Text);
            _shopcart.dpcount = 5;
            _shopcart.takhfif = 0;
            List<shopcartitem> aghlam= new List<shopcartitem>();
            string stsql = "select * from basket left join basket_main on basket.basket_id = basket_main.basket_id and " +
                " basket.tokenid = basket_main.tokenid where basket.synced=0 and basket.sessionid='" + Session.SessionID + "' or basket.tokenid='" + tn.vas + "'";
            string _listkala = "";
            DataView basket = sqhand.SqlExecute(stsql, "dv");
            decimal total_price = 0;
            decimal total_ghest = 0;
            decimal total_pish = 0;
            foreach (DataRowView dr in basket)
            {
                shopcartitem spci= new shopcartitem();
                spci.etiket= dr["etiket"].ToString();
                spci.kala = dr["kcode"].ToString();
                aghlam.Add(spci);
            }

                _shopcart.aghlam= aghlam;

            var callresp= Callapi_sale(_shopcart);
            callresp.Wait();
            if (myconvert.toint(callresp.Result.respondcode)>=0)
            {

                sqhand.SqlExecute("update basket set synced=2 where basket.sessionid='" + Session.SessionID + "' or basket.tokenid='" + tn.vas + "'");

                Response.Redirect("successed.aspx");
            }
            else
            {
                Response.Write(String.Format("<script>alert('فاکتور ثبت نشد"+ callresp.Result.respondsharh + "')</script>"));
                if (callresp.Result.respondcode.Equals("-1"))
                {
                    Response.Redirect("customercharge.aspx?type=W&value=" + myconvert.toint(Math.Floor(callresp.Result.addvalue)).ToString().Trim());
                }
            }
        }
    }


 

}
