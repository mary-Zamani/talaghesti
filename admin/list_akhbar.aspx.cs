using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


namespace narsShop.admin
{
    public partial class list_akhbar : System.Web.UI.Page
    {
        public static SQLH sqlhand;
        DataView dv;
        protected void Page_Load(object sender, EventArgs e)
        {
            bind_gride();

        }

        private void bind_gride() 
        {
            SQLH sqlhand = new SQLH();
            string sqlstr;
           

            
                sqlstr = "SELECT dt_mesa.* FROM dt_mesa  " +
                " where  (kind='I030') order by senddate desc";
                
            this.dv = sqlhand.SqlExecute(sqlstr, "dv");
 
            GridView1.DataSource = dv;
            GridView1.DataBind();

        }
        

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            dv.Sort = e.SortExpression;
            this.GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        protected void D2d_NeedRequery(object se, EventArgs ea)
        {
            bind_gride();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }



        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFF00'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }
      

      

      
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("list_akhbar_add.aspx?s_id=0");
        }

        




    }
}