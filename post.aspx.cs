using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


namespace narsShop
{
    public partial class post : System.Web.UI.Page
    {
        public SQLH sqlhand;
        DataView dv,dv1,dv2,dv3,dv4;
        string s_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            s_id = Request["s_id"];

                bind_gride();

          //  Master.addbullet(new ListItem("صفحه اصلي", "~\\mainmenu.aspx"));
          //  Master.addbullet(new ListItem("لیست اخبار ", "~\\mainmenu.aspx"));
          //  Master.setpagename("نمایش خبر");
        }

        private void bind_gride() 
        {
            

            SQLH sqlhand = new SQLH();
            DataView dv = sqlhand.SqlExecute("SELECT * FROM dt_mesa where s_id=" + s_id, "dv");
            onvan.Text = dv[0]["onvan"].ToString().Trim();
            noe.Text= dv[0]["grouh"].ToString().Trim();
            sharh.Text= dv[0]["sharh"].ToString().Trim();
            //imgnews.ImageUrl = "/Attachments/news/" + s_id.ToString().Trim() + ".jpg";
        }
        




        protected void Btn_save_Click(object sender, EventArgs e)
        {
          
        }


    }
}