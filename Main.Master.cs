using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace narsShop
{
    public partial class Main : System.Web.UI.MasterPage
    {
 
        public bool ShowBaner
        {
            get { return SectionBaner.Visible; }
            set { SectionBaner.Visible = value; }
        }
        public bool ShowCategory
        {
            get { return SectionCategory.Visible; }
            set { SectionCategory.Visible = value; }

        }
        public bool ShowBbreadcumb
        {
            get { return sectionBbreadcumb.Visible; }
            set { sectionBbreadcumb.Visible = value; }
        }
        public bool showsectionShop
        {
            get { return sectionShop.Visible; }
            set { sectionShop.Visible = value; }
        }
   
        public Repeater RepeaterProducts
        {
            get { return rptProducts; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}