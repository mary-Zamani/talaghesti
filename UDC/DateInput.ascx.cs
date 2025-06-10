using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace narsShop
{

    public partial class DateInput : System.Web.UI.UserControl
    {

        int _defaultistoday;
        string _mystyle, _myclass;
        public int Defaultistoday
        {
            get { return _defaultistoday; }
            set { _defaultistoday = value; }
        }
        public string MyStyle
        {
            get { return _mystyle; }
            set { _mystyle = value; }
        }
        public string MyClass
        {
            get { return _myclass; }
            set { _myclass = value; }
        }
        public string Text
        {
            get
            {
                if (this.DI_TextBox.Text.Trim().Equals("____/__/__") )
                    return "";
                    return this.DI_TextBox.Text.Trim();
            }
            set { 
                this.DI_TextBox.Text = value.Trim();
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DI_TextBox.Attributes.Add("onchange", this.ClientID+"_di_textvalidator();");
            if (_defaultistoday == 1 && this.Text.Equals(""))
                this.Text = persiandate.datef();
            if (_mystyle!=null && _mystyle.Length > 3)
                this.DI_TextBox.Style.Add(_mystyle.Split(new char[] { ':' })[0], _mystyle.Split(new char[] { ':' })[1]);
            if (_myclass != null && _myclass.Length > 3)
                this.DI_TextBox.Attributes.Add("class", _myclass);
        }


    }
}