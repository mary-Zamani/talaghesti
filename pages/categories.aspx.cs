using System;
using System.Collections;
using System.Data;
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
    public partial class categories : System.Web.UI.Page
    {
        SQLH sqhand;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqhand = new SQLH();
            
            lbl_productrow1.Text = rowofproducts();
        }

       

        string rowofproducts()
        {
            string parentname = "";
            string respond = "";
            DataView dv = sqhand.SqlExecute("select * from categories where parentid is null and catalogview=1 order by categoryid", "dv");
            int rownumber = 0;
            int colnumber = 0;
            foreach(DataRowView dr in dv)
            {
                if (colnumber==0)
                    respond+= @"<div class=""row owl-carousel"" style=""display:flex !important"">";

                respond += @"<div class=""col product-item""><div class=""pi-pic""><img src=""";
                respond += "../img/category/" + dr["categoryid"].ToString().Trim() + @".jpg"" alt="""" />";
                //if (myconvert.toint16(dr["sale"]) == 1) respond += @"<div class=""sale"">Sale</div>";
                respond += @"<div class=""icon""></div>";
                respond += @"<ul>
                              <li class=""quick-view""><a href=""subcategories.aspx?parent=" + dr["categoryid"].ToString().Trim() + @""">+ اطلاعات بیشتر </a></li>
                              
                          </ul></div>";
                respond += @"<div class=""pi-text""><div class=""catagory-name"">" + parentname + "</div>";
                respond += @"<a href=""subcategories.aspx?parent=" + dr["categoryid"].ToString().Trim() + @"""><h5>" + dr["categoryname"].ToString().Trim() +"</h5></a>";
                respond += @"<div class=""product-price"">
                             
                              
                          </div> </div></div>";
                colnumber++;
                if (colnumber == 4)
                {
                    respond += "</div>";
                    colnumber = 0;
                }
            }
            return respond;
        }
    }
}
