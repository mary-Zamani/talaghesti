using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace narsweb
{
    public partial class gcatalog : System.Web.UI.Page
    {
        DataView dv,dv1;
        DataTable goods= new DataTable("goods");
        string root;
        protected void Page_Load(object sender, EventArgs e)
        {
            root = Request.Params["root"].Trim();
            string respond = "";
            string ctype = "servername";
            string pt = System.Web.Hosting.HostingEnvironment.MapPath(HttpContext.Current.Request.ApplicationPath);
            DataTable dt = new DataTable();
            dt.ReadXml(pt + "/configs/narsweb.conf");
            dv1 = dt.DefaultView;
            string maindb = dv1[0]["maindb"].ToString();
            string dbhost = dv1[0]["dbhost"].ToString();
            try { ctype = dv1[0]["ctype"].ToString(); } catch { }

            goods.Columns.Add("kala", typeof(String));
            goods.Columns.Add("mx", typeof(decimal));
            goods.Columns.Add("mn", typeof(decimal));


            SQLH sqlhand = new SQLH(dbhost, maindb);
            DataView tenants = sqlhand.SqlExecute("SELECT tenantid,dbsname,"+ctype+" as servername FROM tenants where groupid=1", "dv");
            foreach (DataRowView dr in tenants)
            {
                string servername = dr["servername"].ToString();
                string dbname = dr["dbsname"].ToString();
                SQLH sqhand = new SQLH(servername, dbname);
                DataView g1 = sqhand.SqlExecute("select kala,max(vaznmande) as mx,min(vaznmande) as mn  FROM anbar_mandekala WHERE(mande > 0) AND(vaznmande > 0) AND(cert > ' ')  and left(kala," + root.Length + ")='" + root + "' group by kala order by kala", "dvt");

                foreach (DataRowView g in g1)
                {
                    DataRow[] findedrows = goods.Select("kala='" + g["kala"].ToString().Trim() + "'");
                    if (findedrows.Count() == 0)
                    {
                        DataRow g0 = goods.NewRow();
                        g0["kala"] = g["kala"];
                        g0["mn"] = g["mn"];
                        g0["mx"] = g["mx"];

                        goods.Rows.Add(g0);
                    } else
                    {
                        if (myconvert.todecimal(findedrows[0]["mn"]) > myconvert.todecimal(g["mn"])) findedrows[0]["mn"] = g["mn"];
                        if (myconvert.todecimal(findedrows[0]["mx"]) < myconvert.todecimal(g["mx"])) findedrows[0]["mx"] = g["mx"];
                    }
                }


            }
        


        DataView dvt = goods.DefaultView;
            dvt.Sort = "kala";
            int a = 1;
            
            respond += "<table border=\"1\" width=\"100%\"> ";
            foreach (DataRowView dr in dvt)
            {
 
                respond += "<tr><td>";
                respond += "<figure>";
                respond += "<a href=hcatalog.aspx?kala="+dr["kala"].ToString().Trim()+"><img src=\"\\attachments\\kcode\\"+ dr["kala"].ToString().Trim()+".jpg\" style=\"width:100%;height:auto\"/></a>";
                respond += "<figcaption style=\"background-color: rgb(0,0,0,.5);color:white;padding:2px;text-align:center;margin-top:-25px;\">" + dr["mn"].ToString() + "  :  " + dr["mx"].ToString() + "</figcaption>";
                respond += "</figure>";

               respond += "<br></td></tr>";

                ++a;
            }
            respond += "</table>";
            this.Lbl_sharh.Text = respond;
        }


    }
}
