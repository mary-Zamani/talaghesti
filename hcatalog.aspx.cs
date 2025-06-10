using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace narsweb
{
    public partial class hcatalog : System.Web.UI.Page
    {
        DataView dv,dv1;

        string root;
        protected void Page_Load(object sender, EventArgs e)
        {
            root = Request.Params["kala"].Trim();
            string respond = "";
            string ctype = "servername";
            DataTable goods = new DataTable("goods");
            goods.Columns.Add("tenantid", typeof(String));
            goods.Columns.Add("color", typeof(String));
            goods.Columns.Add("options", typeof(String));
            goods.Columns.Add("cert", typeof(String));
            goods.Columns.Add("vaznmande", typeof(decimal));

            string pt = System.Web.Hosting.HostingEnvironment.MapPath(HttpContext.Current.Request.ApplicationPath);
            DataTable dt = new DataTable();
            dt.ReadXml(pt + "/configs/narsweb.conf");
            dv1 = dt.DefaultView;
            string maindb = dv1[0]["maindb"].ToString();
            string dbhost = dv1[0]["dbhost"].ToString();
            try { ctype = dv1[0]["ctype"].ToString(); } catch { }

            SQLH sqlhand = new SQLH(dbhost, maindb);
            DataView tenants = sqlhand.SqlExecute("SELECT tenantid,dbsname," + ctype + " as servername FROM tenants where groupid=1", "dv");

            foreach (DataRowView dr in tenants)
            {

                string servername = dr["servername"].ToString();
                string dbname = dr["dbsname"].ToString();
                SQLH sqhand = new SQLH(servername, dbname);
                DataView g1 = sqhand.SqlExecute("select * FROM anbar_mandekala WHERE(mande > 0) AND(vaznmande > 0) AND(cert > ' ')  and left(kala," + root.Length + ")='" + root + "'", "dvt");
                foreach (DataRowView g in g1)
                {
                    DataRow gn = goods.NewRow();
                    gn["tenantid"] = dr["tenantid"].ToString();
                    gn["cert"] = g["cert"].ToString();
                    gn["vaznmande"] =myconvert.todecimal(g["vaznmande"]);

                    DataView dvr = sqhand.SqlExecute("select * FROM anbar WHERE(pro = 'R') and (cert = '" + g["cert"].ToString() + "')  and sherkat=0 ", "dvt");
                    gn["color"] = dvr[0]["color"].ToString();
                    gn["options"] = dvr[0]["options"].ToString();
                    goods.Rows.Add(gn);
                }



            }

            DataView dvk= sqlhand.SqlExecute("select * FROM kcodes WHERE kcode='" + root + "' ", "dv");
            //DataView dvt = sqlhand.SqlExecute("select * FROM anbar_mandekala WHERE(mande > 0) AND(vaznmande > 0) AND(cert > ' ')  and left(kala," + root.Length+")='"+root+ "' order by vaznmande", "dvt");
            long fitala  = myconvert.toint(sqlhand.SqlExecute("select top (1) * from fi_estandard  order by tebtal desc", "sqtb")[0]["fi"]);
            DataView dvt = goods.DefaultView;
            dvt.Sort = "vaznmande";


            foreach (DataRowView dr in dvk)
            {
                respond += "<figure>";
                respond += "<img src=\"\\attachments\\kcode\\"+ dr["kcode"].ToString().Trim()+".jpg\" style=\"width:100%;height:auto\"/>";
                respond += "<figcaption style=\"background-color: rgb(0,0,0,.3);color:white;padding:2px;text-align:center;margin-top:-25px;\">" + dr["fi_es"].ToString() + "  :  " + dr["persian"].ToString() + "</figcaption>";
                respond += "</figure>";                
            }

            respond += "<table border=\"1\" width=\"100%\" style=\"padding:15px\"> ";
            foreach (DataRowView dr in dvt)
            {
                decimal price = myconvert.todecimal(dr["vaznmande"]) * fitala;
                decimal mozdsakht = (price * myconvert.todecimal(dvk[0]["fi_es"])/100)+ myconvert.todecimal(dvk[0]["fi_fi"]) + myconvert.todecimal(dvk[0]["fi_td"]);
                decimal sood= price  * 7 / 100;
                decimal maliat = (mozdsakht+sood) * 9 / 100;
                price += mozdsakht+sood + maliat;
                price = Math.Round(price,0);
                respond += "<tr><td>" + dr["tenantid"].ToString() + "</td><td>" + dr["cert"].ToString()+ "</td><td>"+ dr["vaznmande"].ToString()+ "</td><td>" + dr["color"].ToString() + "</td><td>" + dr["options"].ToString() + "</td><td>"+
                  price.ToString("0,0")  +"</td></tr>";

            }
            this.Lbl_sharh.Text = respond;
        }


    }
}
