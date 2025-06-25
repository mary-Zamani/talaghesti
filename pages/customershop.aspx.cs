using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;

namespace narsShop.pages
{
    public partial class customershop : System.Web.UI.Page
    {
        SQLH sqhand;
        token tn;
        string paytype = "";
        string orderid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        async Task<savefactorrespond> Callapi_sale(shopcart shop_card)
        {
            string json = string.Empty;
            DataTable dt = new DataTable();
            string apiUrl = Session["apiurl"] + "/api/saveforosh";
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

            savefactorrespond respond = new savefactorrespond();
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
            List<shopcartitem> aghlam = new List<shopcartitem>();
            string stsql = "select * from basket left join basket_main on basket.basket_id = basket_main.basket_id and " +
                " basket.tokenid = basket_main.tokenid where basket.synced=0 and basket.sessionid='" + Session.SessionID + "' or basket.tokenid='" + tn.vas + "'";
            string _listkala = "";
            DataView basket = sqhand.SqlExecute(stsql, "dv");
            decimal total_price = 0;
            decimal total_ghest = 0;
            decimal total_pish = 0;
            foreach (DataRowView dr in basket)
            {
                shopcartitem spci = new shopcartitem();
                spci.etiket = dr["etiket"].ToString();
                spci.kala = dr["kcode"].ToString();
                aghlam.Add(spci);
            }

            _shopcart.aghlam = aghlam;

            var callresp = Callapi_sale(_shopcart);
            callresp.Wait();
            if (myconvert.toint(callresp.Result.respondcode) >= 0)
            {

                sqhand.SqlExecute("update basket set synced=2 where basket.sessionid='" + Session.SessionID + "' or basket.tokenid='" + tn.vas + "'");

                Response.Redirect("successed.aspx");
            }
            else
            {
                Response.Write(String.Format("<script>alert('فاکتور ثبت نشد" + callresp.Result.respondsharh + "')</script>"));
                if (callresp.Result.respondcode.Equals("-1"))
                {
                    Response.Redirect("customercharge.aspx?type=W&value=" + myconvert.toint(Math.Floor(callresp.Result.addvalue)).ToString().Trim());
                }
            }
        }
    }
}