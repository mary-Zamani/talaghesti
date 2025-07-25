﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace narsShop
{

    public partial class VerficationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            var collection = HttpUtility.ParseQueryString(this.ClientQueryString);
            String Status = collection["Status"];


            if (Status != "OK")
            {
                Response.Write("<script>alert('Purchase unsuccessfully')</script>");
                return;
            }



            var zarinpal = ZarinPal.ZarinPal.Get();
            String Authority = collection["Authority"];
            String MerchantID = "c5f58444-f418-11ea-afe6-000c295eb8fc";
            long Amount = 5000;


            var verificationRequest = new ZarinPal.PaymentVerification(MerchantID, Amount, Authority);
            var verificationResponse = zarinpal.InvokePaymentVerification(verificationRequest);
            if (verificationResponse.Status == 100)
            {
                Response.Write(String.Format("<script>alert('Purchase successfully with ref transaction {0}')</script>", verificationResponse.RefID));
            }
            else
            {

                Response.Write(String.Format("<script>alert('Purchase unsuccessfully Error code is: {0}')</script>", verificationResponse.Status));

            }


        }
    }
}