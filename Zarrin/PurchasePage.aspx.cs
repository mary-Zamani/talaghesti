using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZarinPal; //import ZarinPal Assembly for us ZarinPal Classes

namespace narsShop
{

    public partial class PurchasePage : System.Web.UI.Page
    {

        protected void Button1_Click(object sender, EventArgs e)
        {
            ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();

            String MerchantID = "c5f58444-f418-11ea-afe6-000c295eb8fc";
            String CallbackURL = "http://talaghesti.com/zarrin/VerficationPage.aspx";
            long Amount = 5000;
            String Description = "صفحه پرداخت امتحانی";

            ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);


            var res = zarinpal.InvokePaymentRequest(pr);
            if (res.Status == 100)
            {
                Response.Redirect(res.PaymentURL);
            }

        }
    }
}