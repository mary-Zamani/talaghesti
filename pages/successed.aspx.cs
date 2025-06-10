using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TalaModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZarinPal;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace narsShop
{
    public partial class successed : System.Web.UI.Page
    {

        string disclaimer = @"اقساط خود را در حساب کاربری خود میتوانید ببینید 
در صورت تسویه زودتر از موعد مبلغ تسویه در روز درج شده در قسمت زیر دفتر قسط را پرداخت کنید. ( الباقی مبلغ تخفیف خوش حسابی به صورت اتوماتیک درج می‌گردد)
در صورت داشتن هر گونه سوال یا نیاز به پشتیبانی از قسمت حساب کاربری، تیکت پشتیبانی ارسال نمایید در اسرع وقت با شما تماس میگیریم
طلا خریداری شده بعد از تسویه کامل ارسال میگردد
نوسانات بازار طلا تاثیری در خرید و اقساط شما نخواهد داشت";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_title.Text = "فاکتور شما ثبت گردید";
            lbl_subtit.Text = disclaimer;

            
            


        }

        protected void gotoaccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("customeraccount.aspx");
        }
    }
}
