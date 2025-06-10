using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

namespace narsShop.admin
{
    public partial class list_akhbar_add : System.Web.UI.Page
    {
        public static SQLH sqlhand;
        DataView dv,dv2,dv3,dv4;
        string s_id = "";

        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("list_akhbar.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            s_id = Request["s_id"];

            if (!IsPostBack && !s_id.Equals("0"))
            {
                bind_gride();
            }
      
        }

        private void bind_gride() 
        {
            SQLH sqlhand = new SQLH();
            DataView dv = sqlhand.SqlExecute("SELECT * FROM dt_mesa where s_id=" + s_id, "dv");
            Txt_onvan.Text = dv[0]["onvan"].ToString().Trim();
            Drp_grouh.SelectedValue= dv[0]["grouh"].ToString().Trim();
            RadEditor1.Content= dv[0]["sharh"].ToString().Trim().Replace('|',';');
        }
        




        protected void Btn_save_Click(object sender, EventArgs e)
        {
            SQLH sqhand = new SQLH();
            string sql="";

            if (s_id.Equals("0"))
            {
                sql = "insert into dt_mesa(kind, onvan, senddate, sharh,grouh) values ('I030',N'" + Txt_onvan.Text +
                   "','" + persiandate.datef() + "',N'" + RadEditor1.Content.Trim().Replace(';', '|') + "',N'" + Drp_grouh.SelectedValue + "')";
                s_id=sqhand.SqlExecute(sql).ToString();
            }
            else
            {
                sql = "update dt_mesa set sharh=N'" + RadEditor1.Content.Trim().Replace(';', '|') + "',grouh=N'" + Drp_grouh.Text.Trim() + "',onvan=N'" + Txt_onvan.Text + "' where s_id=" + s_id;
                sqhand.SqlExecute(sql);
            }

            

            

/*            if (AsyncUpload1.UploadedFiles.Count>0)
            {
                string svpath = Server.MapPath("~/Attachments/News/");
                string physicalname = s_id.ToString().Trim()+ ".jpg";
                string path = string.Format(CultureInfo.InvariantCulture, Strings.NewObjectPath, svpath, physicalname);
                AsyncUpload1.UploadedFiles[0].SaveAs(path);

            }
*/


           
            
            Response.Redirect("list_akhbar.aspx");
        }


    }
}