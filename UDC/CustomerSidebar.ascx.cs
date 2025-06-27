using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;

namespace narsShop.UDC
{
    public partial class CustomerSidebar : System.Web.UI.UserControl
    {
        SQLH sqhand;
        token tn;
        string paytype = "";
        string orderid = "";
        string source = "";
         
        const string banksite = "https://ghestitala.com";
        protected void Page_Load(object sender, EventArgs e)
        {
            tn = (token)Session["token"];
            if (tn.Token == null)
                Response.Redirect("~");

            


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
        protected void btnExist_Click(object sender, EventArgs e)
        {
 
            TalaModelLibrary.token tn = new token();
            Session["token"] = tn;
            Response.Cookies.Clear();
            HttpCookie TNcookie = new HttpCookie("Token");
            TNcookie.Value = JsonConvert.SerializeObject(tn);
            TNcookie.Expires = DateTime.Now;
            Response.Cookies.Add(TNcookie);
            Response.Redirect(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 10)));
        }
    }
}