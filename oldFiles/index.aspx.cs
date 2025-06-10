using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace narsShop
{
    public partial class index : System.Web.UI.Page
    {
        SQLH sqhand;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();

            //lbl_hero.Text = heroslider();
            lbl_banners.Text = banners();
            lbl_productrow1.Text = rowofproducts();
            lbl_productrow2.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            string respond = @"<div class=""row product-slider owl-carousel"" style=""display:flex !important"">";
            DataView dv = sqhand.SqlExecute("select * from newproducts a inner join kcodes on a.kala=kcodes.kcode inner join categories c on kcodes.category=c.categoryid", "dv");

            foreach(DataRowView dr in dv)
            {
                respond += @"<div class=""col product-item""><div class=""pi-pic""><img src=""";
                respond += "img/kcode/" + dr["kcode"].ToString().Trim() + @".jpg"" alt="""" />";
                if (myconvert.toint16(dr["sale"]) == 1) respond += @"<div class=""sale"">Sale</div>";
                respond += @"<div class=""icon""></div>";
                respond += @"<ul>
                              <li class=""quick-view""><a href=""#""> اطلاعات بیشتر </a></li>
                              
                          </ul></div>";
                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + dr["categoryname"].ToString().Trim() +"</div>";
                respond += @"<a href=""#""><h5>" + dr["persian"].ToString().Trim() +"</h5></a>";
                respond += @"<div class=""product-price"">
                          </div>
                      </div>
                  </div>";
            }
            respond += "</div>";
            return respond;
        }
        string heroslider()
        {
            DataView dv = sqhand.SqlExecute("select * from heros", "dv");


            string respond = @"<div class=""hero-items owl-carousel"" style=""text-align: center;text-shadow: 0px 0px 3px #e7ab3c,0px 0 5px darkgray;"">";
            foreach(DataRowView dr in dv)
            {
                respond += @"<div class=""single-hero-items set-bg"" data-setbg=""" + dr["img"].ToString().Trim() 
                    +@"""><div class=""container""><div class=""row""><div class=""col-lg-12""><span>" 
                    + dr["category"].ToString().Trim() + "</span><h1>" + dr["header"].ToString().Trim() + "</h1><p style=\"color:black\">" 
                    + dr["body"].ToString().Trim() +
                      @"</p><a href=""" + dr["link"].ToString().Trim() +@""" class=""primary-btn"">" + dr["action"].ToString().Trim() + @"</a></div> </div><div class=""off-card"">
                  <h2>" + dr["offcard1"].ToString().Trim() + "<span>" + dr["offcard2"].ToString().Trim() + "</span></h2></div></div></div>";
            }
 
            respond += "</div>";

            return respond;
        }
        string banners()
        {
            DataView dv = sqhand.SqlExecute("select * from banners", "dv");
            string respond = @"   <div class=""banner-section spad"">
  <div class=""container-fluid"">
      <div class=""row"">";
            foreach (DataRowView dr in dv)
            {
                respond += @"<div class=""col-lg-4""> <div class=""single-banner"">
                  <img src=""" + dr["img"].ToString().Trim() + @""" alt="""" />
                  <div class=""inner-text"">
                      <a href=""" + dr["link"].ToString().Trim() + @"""><h4>" + dr["header"].ToString().Trim() + @"</h4></a>
                  </div>
              </div>
          </div>";
            }

            respond += "</div>  </div></div>";

            return respond;
        }

        protected void dosearch_Click(object sender, EventArgs e)
        {
            decimal prepay = myconvert.todecimal(txt_prepay.Text);
            if (prepay>500000)
            Response.Redirect("pages/pricesearch.aspx?price=" + prepay.ToString());
        }
    }
}
