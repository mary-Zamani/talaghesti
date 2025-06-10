using Newtonsoft.Json;
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
using TalaModelLibrary;

namespace narsShop
{
    public partial class indexOld : System.Web.UI.Page
    {
        SQLH sqhand;
        //string openrow = "<div class=\"row mb-f justify-content-between\">";//@"<div class=""row owl-carousel"" style=""display:flex !important"">"
        //string opencol = "<div class=\"col-12 col-md-6 col-lg-4 mb-6 mb-md-5\">";//@"<div class=""col-3 product-item table-bordered"">
        //string cardbody = "<div class=\"card bg-primary border-light shadow-soft\"> {image} <div class=\"card-body\">" +
        //    "<h3 class=\"h5 card-title mt-3\"></h3> <p class=\"card-text\"><strong></strong></p> <p class=\"card-text\"></p>" +
        //    "<p><div class=\"btn btn-primary btn-block\">{info}</div></p></div> </div>";









        protected void Page_Load(object sender, EventArgs e)
        {

            string tok = Request["tok"];
            if (tok!= null)
            {
                string chargeamount = Request["chargeamount"];
                if (string.IsNullOrEmpty(chargeamount))
                {
                    Response.Redirect("weblogin.aspx?tok=" + tok);
                }else
                {
                    Response.Redirect("weblogin.aspx?tok=" + tok+"&w="+chargeamount);
                }
            }

            string invc = Request["invc"];
            if (invc!= null)
            {
                Response.Redirect("invsummery.aspx?invc=" + invc);
            }

            string fcno = Request["fcno"];
            if (fcno != null)
            {
                Response.Redirect("_fcv.aspx?fcno=" + fcno);
            }


            HttpCookie cookie = Request.Cookies["Token"];
            if (cookie!=null && ((token)Session["token"]).Token==null)
            {
                if (!string.IsNullOrEmpty(cookie.Value))
                {
                    token tk = JsonConvert.DeserializeObject<token>(cookie.Value);
                    if (!string.IsNullOrEmpty(tk.mobileno) && !string.IsNullOrEmpty(tk.shmeli))
                    {
                             Response.Redirect(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf('/', 10)) + "/weblogin.aspx?r=r&tok=" + tk.mobileno+tk.shmeli);
                    }
                }

            }

            sqhand = new SQLH();

            //lbl_hero.Text = heroslider();
           lbl_banners.Text = banners();
            //lbl_productrow1.Text = rowofproducts();
           // lbl_productrow2.Text = rowofproducts();

        }

        //string rowofproducts()
        //{
        //    string parentname = "";
        //    string respond = "";
        //    DataView dv = sqhand.SqlExecute("select * from categories where (parentid is null or parentid='') and catalogview=1 order by categoryid", "dv");
        //    int rownumber = 0;
        //    int colnumber = 0;
        //    foreach (DataRowView dr in dv)
        //    {
        //        if (colnumber == 0)
        //            respond += openrow;

        //        respond += opencol;
        //        respond += cardbody.Replace("{caption}", dr["categoryname"].ToString().Trim())
        //            .Replace("{image}", @"<a href=""pages/productlist.aspx?parent=" + dr["categoryid"].ToString().Trim() + @""">"+"<img src=\"../img/category/" + dr["categoryid"].ToString().Trim() + ".jpg\" style=\"width:100%;height:400px;\"  alt=\""+ dr["categoryname"].ToString().Trim() + "\" /></a>")
        //            .Replace("{sline1}", dr["categoryname"].ToString().Trim())
        //            .Replace("{sline2}", dr["categoryid"].ToString().Trim())
        //            .Replace("{info}", @"<a href=""pages/productlist.aspx?parent=" + dr["categoryid"].ToString().Trim() + @"""><h5>" + dr["categoryname"].ToString().Trim() + "</h5></a>");
        //        respond += "</div>";


        //        colnumber++;
        //        if (colnumber == 3)
        //        {
        //            respond += "</div>";
        //            colnumber = 0;
        //        }
        //    }
        //    return respond;
        //}

        /* string rowofproducts()
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
         }*/
        string heroslider()
        {
            DataView dv = sqhand.SqlExecute("select * from heros", "dv");


            string respond = @"<div class=""hero-items owl-carousel"" style=""text-align: center;text-shadow: 0px 0px 3px #a454b5,0px 0 5px darkgray;"">";
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
            string respond = @"   ";
            foreach (DataRowView dr in dv)
            {
                
                respond += @"<div class=""main-page-container-css"">
               <div class=""banner-slider-container-css"">
                   <div class=""banner-slider-css"">
                       <a href = ""{link}"" class=""slider-next-btn-css""><i class=""ri-arrow-right-s-line""></i></a>
                       <a href = ""{link}"" ><img src=""{image}"" alt=""banner-img""/></a>
                       <a href = ""{link}"" class=""slider-prev-btn-css""><i class=""ri-arrow-left-s-line""></i></a>
                   </div>
                   <div class=""slider-btns-css"">
                       <a href = ""{link}"" ><i class=""ri-circle-line""></i></a>
                       <a href = ""{link}"" ><i class=""ri-circle-line""></i></a>
                       <a href = ""{link}"" ><i class=""ri-circle-line""></i></a>
                   </div>
               </div>".Replace("{link}", dr["link"].ToString().Trim()).Replace("{image}", dr["img"].ToString().Trim());

            }

            respond += "";

            return respond;
        }



       








       
    }
}
