using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalaModelLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using ExtensionMethods;
using System.Text;

namespace narsShop.pages
{
    public partial class productlist : System.Web.UI.Page
    {
        SQLH sqhand;
        string parent;
        const int pageSize = 12;

        int currentPage
        {
            get
            {
                object obj = ViewState["CurrentPage"];
                return (obj == null) ? 0 : (int)obj;
            }
            set { ViewState["CurrentPage"] = value; }
        }

        List<ProductViewModel> allProducts; // همه محصولات

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Main)this.Master).ShowBaner = false;
            ((Main)this.Master).ShowCategory = false;
          
          
            parent = Request["parent"]?.ToString() ?? "10";
            currentPage = GetPageFromQuery();

            if (!IsPostBack)
            {
                allProducts = GetProductList(parent);
                ViewState["AllProducts"] = allProducts;
            }
            else
            {
                allProducts = ViewState["AllProducts"] as List<ProductViewModel> ?? new List<ProductViewModel>();
            }

            BindRepeater();
        }


        void BindRepeater()
        {
            var master = (Main)this.Master;
            PagedDataSource paged = new PagedDataSource();
            paged.DataSource = allProducts.Skip(currentPage * pageSize).Take(pageSize).ToList();
            paged.AllowPaging = true;
            paged.PageSize = pageSize;
            paged.CurrentPageIndex = 0;

            master.RepeaterProducts.DataSource = paged;
            master.RepeaterProducts.DataBind();

            GeneratePagination();
        }


        int GetTotalPages()
        {
            if (allProducts == null || allProducts.Count == 0)
                return 0;

            return (int)Math.Ceiling((double)allProducts.Count / pageSize);
        }

        private void GeneratePagination()
        {
            var master = (Main)this.Master;
            int totalPages = GetTotalPages();

            int currentPage = GetPageFromQuery(); // تابعی که از QueryString صفحه فعلی رو می‌خونه

            StringBuilder paginationHtml = new StringBuilder();
            int prevPage = Math.Max(currentPage - 1, 0);
            int nextPage = Math.Min(currentPage + 1, totalPages - 1);

            paginationHtml.Append($"<a href='productlist.aspx?parent={parent} &page={prevPage}' class='prev'><i class='fa-solid fa-chevron-left'></i></a>");

            for (int i = 0; i < totalPages; i++)
            {
                string activeClass = (i == currentPage) ? "active" : "";
                paginationHtml.Append($"<a href='productlist.aspx?parent={parent} &page={i}' class='page {activeClass}'>{(i + 1).ToString("00")}</a>");
            }

            paginationHtml.Append($"<a href='productlist.aspx?={parent} &page={nextPage}' class='next'><i class='fa-solid fa-chevron-right moz'></i></a>");

            var litPagination = master.FindControl("litPagination") as Literal;
            if (litPagination != null)
            {
                litPagination.Text = paginationHtml.ToString();
            }

            var customPagination = master.FindControl("customPagination");
            if (customPagination != null)
                customPagination.Visible = totalPages > 1;
        }
        private int GetPageFromQuery()
        {
            string pageStr = Request.QueryString["page"];
            if (int.TryParse(pageStr, out int page) && page >= 0)
                return page;
            return 0;
        }


        List<ProductViewModel> GetProductList(string parent)
        {
            decimal pishpp = decode.getvarb("pish5").ToDecimal();
            List<etiket> etikets = callapi(parent);

            return etikets
                .GroupBy(e => e.kcode)
                .Select(g =>
                {
                    var et = g.First();
                    decimal fullPrice = Math.Ceiling(et.price / 10000) * 10000;
                    decimal prepay = Math.Ceiling(et.price / ((et.faghatnaghdi ? 1 : pishpp) * 10000)) * 10000;

                    return new ProductViewModel
                    {
                        ImageUrl = "https://talaghesti.com/img/kcode/" + et.kcode + ".jpg",
                        Title = et.kalaname,
                        Subtitle = "اتیکت: " + et.cert + (et.faghatnaghdi ? " (فقط نقدی)" : ""),
                        Price = fullPrice.ToString("0,0"),
                        PrePayment = prepay.ToString("0,0"),
                        Weight = et.vaznmande.ToString("0.##") + " گرم",
                        DetailsUrl = "/pages/product.aspx?kcode="+et.kcode
                    };
                }).ToList();
        }

        List<etiket> callapi(string kcode)
        {
            List<etiket> json = new List<etiket>();

            string apiUrl = Session["apiurl"] + "/api/etiket/getetiketofmaincategory/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(kcode);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonSerializer.Deserialize<List<etiket>>(readTask.Result);
            }

            return json;
        }
        [Serializable]
        public class ProductViewModel
        {
            public string ImageUrl { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string Price { get; set; }          // قیمت کل
            public string PrePayment { get; set; }     // پیش پرداخت
            public string Weight { get; set; }         // وزن
            public string DetailsUrl { get; set; }
        }
    }
}