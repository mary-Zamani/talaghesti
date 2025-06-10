using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace narsShop
{
    public partial class anj : System.Web.UI.MasterPage
    {
        protected string s1, s2, s3, s4;
        protected void Page_Load(object sender, EventArgs e)
        {
         lbl_loginpart.Text = loadpart("login");
            //  lbl_roydad.Text = loadnews("رویداد");
            lbl_shopcart.Text = shopcart();
        }

        string loadpart(string newstype)
        {
            string respond = "";

            TalaModelLibrary.token tn = (TalaModelLibrary.token)Session["token"];

            if (tn.Token == null)
            {
                respond += "<a href=\"weblogin.aspx\" class=\"login-panel\">ورود<i class=\"fa fa-user\"></i></a>";
            }
            else
            {
                respond += "<a href=\"pages/customeraccount.aspx\" class=\"login-panel\">حساب من<i class=\"fa fa-user\"></i></a>";

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
                stsql = "select * from basket where sessionid='" + Session.SessionID + "' or tokenid='"+ Session["token"].ToString()+"'";
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
                                                      <h6>" + dr["etiket"].ToString() +@"</h6>
                                                  </div>
                                              </td>
                                              <td class=""si-close"">
                                                  <i class=""ti-close""></i>
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


      
    }
}

