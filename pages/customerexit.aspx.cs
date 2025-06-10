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
    public partial class customerexit : System.Web.UI.Page
    {
        SQLH sqhand;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            TalaModelLibrary.token tn = new token();

            Session["token"]=tn;

            Response.Cookies.Clear();
            HttpCookie TNcookie = new HttpCookie("Token");
            TNcookie.Value = JsonConvert.SerializeObject(tn);
            TNcookie.Expires = DateTime.Now;
            Response.Cookies.Add(TNcookie);



            Response.Redirect(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 10)));
        }

    }
}
