using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalaModelLibrary;

namespace narsShop.component
{
    public class productitem
    {
        public static string productcard(etiket dr)
        {
            decimal pishpp = decode.getvarb("pish5").ToDecimal();
            string respond = "";
            respond += @"<div class=""col-3 product-item table-bordered"">";
            respond += @"<div class=""pi-text""><a href=""#""><h4>" + decode.k2name(dr.kcode).Trim() + "</h4></a></div>";
            respond += "<div class=\"pi-pic\"><a href=\"product.aspx?kcode="+ dr.kcode.Trim() + "\">";
            respond += "<img src=\"../img/kcode/" + dr.kcode + @".jpg"" alt="""" /></a>";
            //if (myconvert.toint16(dr["sale"]) == 1) respond += @"<div class=""sale"">Sale</div>";
            // respond += @"<div class=""icon""><i class=""icon_heart_alt""></i></div>";
            respond += @"<ul><li class=""quick-view""><a href=""#""> " + "اتیکت :" + dr.cert.ToString().Trim()+ "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "وزن :"+ dr.vaznmande.ToString()+"</a></li></ul>";
            respond += "</div>";
            respond += @"<div class=""pi-text"">";//<div class=""catagory-name"">" + decode.category2name(dr.category) + "</div>";
            //respond += @"<a href=""#""><h5>" + decode.k2name(dr.kcode).Trim() + "</h5></a>";
            respond += @"<div class=""product-price"">";
            respond += "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\"><tr><td>مبلغ هر قسط</td><td>تعداد قسط</td><td>پیش پرداخت</td></tr>";
            respond += "<tr><td>" +   "</td><td>نقد</td><td>" + (Math.Ceiling(dr.price / 10000) * 10000).ToString("0,0") + "</td></tr>";
            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 4)) * (decimal)1.25 / 5) / 10000) * 10000).ToString("0,0") + "</td><td>" +  " 5 " + "</td><td>" + (Math.Ceiling(dr.price / 40000) * 10000).ToString("0,0") + "</td></tr>";
            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 4)) * (decimal)1.30 / 6) / 10000) * 10000).ToString("0,0") + "</td><td>" + " 6 " + "</td><td>" + (Math.Ceiling(dr.price / 40000) * 10000).ToString("0,0")  + "</td></tr>";
            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 2)) * (decimal)1.30 / 10) / 10000) * 10000).ToString("0,0") + "</td><td>" + " 10 " + "</td><td>" + (Math.Ceiling(dr.price / 20000) * 10000).ToString("0,0") + "</td></tr>";
            respond += "<tr><td colspan=3><input type=\"button\" onclick=\"addtobasket(" + dr.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/></td></tr>";
            respond += "</table></div></div></div>";

            return respond;
        }


        public static string productpricetable(etiket dr)
        {
            decimal pishpp = decode.getvarb("pish5").ToDecimal();
            string respond = "";

            respond += @"<div class=""pi-text""><div class=""product-price"">";
            respond += "<table class=\"table table-advance table-bordered table-sm\" style=\"font-size:12px\">";
            respond += "<tr><td>قیمت نقد</td><td>" + (Math.Ceiling(dr.price / 10000) * 10000).ToString("0,0") + "</td><td><lable id=\"kol_"+dr.cert+"\" style=\"visibility:hidden\" >"+ (Math.Ceiling(dr.price / 10000) * 10000).ToString() + "</lable></td></tr>";
            respond += "<tr><td>پیش پرداخت </td><td>";
            respond += "<lable id=\"pishp_" + dr.cert + "\" >" + (Math.Ceiling(((dr.price - (dr.price / pishpp)) * (decimal)1.25 / 5) / 10000) * 10000).ToString("0,0") + "</lable></td><td>";
            respond += "<input onchange=\"rs_change(this.value," + dr.cert + ");\" id=\"RS_" + dr.cert + "\" type=\"range\" class=\"form-range\" min=\"" + (Math.Ceiling(((dr.price - (dr.price / pishpp)) * (decimal)1.25 / 5) / 10000) * 10000) + "\" max=\"" + (Math.Ceiling(dr.price / 10000) * 10000) + "\" step=\"10000\"  value=\""+ (Math.Ceiling(((dr.price - (dr.price / pishpp)) * (decimal)1.25 / 5) / 10000) * 10000) + "\"/>";
            respond += "</td></tr><tr><td> اقسط 5 ماهه";
           respond += "</td><td>" +"<lable id=\"ghest_" + dr.cert + "\" >"+ (Math.Ceiling(dr.price / 40000) * 10000).ToString("0,0") + "</lable></td></tr>";
            respond += "<tr><td colspan=3><input type=\"button\" onclick=\"addtobasket(" + dr.cert + ")\" class=\"btn btn-sm btn-success\" title=\"اضافه به سبد خرید\" value=\"+\"/></td></tr>";
            respond += "</table></div></div>";


/*            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 4)) * (decimal)1.25 / 5) / 10000) * 10000).ToString("0,0") + "</td><td>" + " 5 " + "</td><td>" + (Math.Ceiling(dr.price / 40000) * 10000).ToString("0,0") + "</td></tr>";
            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 4)) * (decimal)1.30 / 6) / 10000) * 10000).ToString("0,0") + "</td><td>" + " 6 " + "</td><td>" + (Math.Ceiling(dr.price / 40000) * 10000).ToString("0,0") + "</td></tr>";
            respond += "<tr><td>" + (Math.Ceiling(((dr.price - (dr.price / 2)) * (decimal)1.30 / 10) / 10000) * 10000).ToString("0,0") + "</td><td>" + " 10 " + "</td><td>" + (Math.Ceiling(dr.price / 20000) * 10000).ToString("0,0") + "</td></tr>";
*/
            return respond;
        }
    }
}