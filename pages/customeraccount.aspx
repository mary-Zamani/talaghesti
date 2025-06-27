<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customeraccount.aspx.cs" Inherits="narsShop.pages.customeraccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Dashboard Section Start -->
    <div class="dashboard-section section-padding fix">
        <div class="container">
            <div class="row">
                <div class="col-xl-3">
                    <div class="dashboard-navigation-sidebar">
                        <h3>
                            <asp:Label runat="server" ID="lbl_customername" /></h3>
                        <div>
                            <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist"
                                aria-orientation="vertical">
                                <button class="nav-link active" id="v-pills-dashboard-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-dashboard" type="button" role="tab"
                                    aria-controls="v-pills-dashboard" aria-selected="true">
                                    <i
                                        class="fa-sharp fa-solid fa-grid-2"></i>داشبورد</button>

                                <button class="nav-link" id="v-pills-order-history-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-order-history" type="button" role="tab"
                                    aria-controls="v-pills-order-history" aria-selected="false">
                                    <i
                                        class="fa-solid fa-sync"></i>تاریخچه سفارش</button>

                                <button class="nav-link" id="v-pills-order-details-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-order-details" type="button" role="tab"
                                    aria-controls="v-pills-order-details" aria-selected="false">
                                    <i
                                        class="fa-solid fa-list"></i>جزئیات سفارش </button>

                                <button class="nav-link" id="v-pills-wishlist-tab"   aria-selected="false">
                                    <i
                                        class="fa-light fa fa-shopping-cart"></i>اقساط من</button>
                               

                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link" PostBackUrl="/pages/customercharge.aspx?type=W">
                                    
                                    <i class="fa-light fa fa-dollar"></i>شارژ کیف پول</asp:LinkButton>


                                 <asp:LinkButton ID="btnSoport" runat="server" CssClass="nav-link" PostBackUrl="/pages/customerticket.aspx">
                                    <i class="fa-light fa fa-comment"></i>پشتیبانی سایت </asp:LinkButton>


                                <asp:LinkButton ID="btnShop" runat="server" CssClass="nav-link" PostBackUrl="/index.aspx">
                                <i class="fa-light fa fa-shopping-cart"></i> فروشگاه 
                                </asp:LinkButton>



                                <button class="nav-link" id="v-pills-settings-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-settings" type="button" role="tab"
                                    aria-controls="v-pills-settings" aria-selected="false">
                                    <i
                                        class="fa-regular fa-gear"></i>تنظیمات</button>
                                <asp:LinkButton ID="btnExists" runat="server" CssClass="nav-link" OnClick="btnExist_Click">
                                 <i class="fa-solid fa-sign-out-alt"></i> خروج از حساب
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-9">

                    <div class="tab-content" id="v-pills-tabContent">
                        <div class="tab-pane fade show active" id="v-pills-dashboard" role="tabpanel"
                            aria-labelledby="v-pills-dashboard-tab" tabindex="0">
                            <div class="dashboard-wrapper">
                                <div class="dashboard-top">
                                    <div class="row">
                                        <div class="col-xl-7">
                                            <div class="dashboard-profile">
                                                <%-- <div class="thumb"><img
                                                    src="/assets/images/dashboard/dashboard-profileThumb.jpg"
                                                     ></div>--%>
                                                <%-- <h3>مشتری نام</h3>--%>
                                                <table border="0">
                                                    <tr>
                                                        <td><i class="fa fa-pencil fa-fw "></i>کد مشتری</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lbl_customercode" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-home fa-fw "></i>آدرس</td>
                                                        <td>
                                                          <asp:Label runat="server" ID="lbl_customeraddress" /></td>
                                                    </tr>
                                             
                                                   <tr>
                                                        <td>  <i class="fa fa-phone fa-fw "></i>تلفن</td>
                                                        <td>
                                                          <asp:Label runat="server" ID="lbl_customerphone" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-user fa-fw "></i>شماره ملی</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lbl_shmeli" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                </table>
                                                <%--  <p>مشتری</p>
                                            <a href="#">ویرایش پروفایل</a>--%>
                                            </div>
                                        </div>
                                        <div class="col-xl-5">
                                            <div class="dashboard-profile-info">
                                                
                                                 <table>
                                                     <tr>
                                                        <td><i class="fa fa-dollar fa-fw "></i>کیف پول</td>
                                                        <td>
                                                            <asp:Label ID="lbl_kif" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-star fa-fw "></i>امتیاز</td>
                                                        <td>
                                                            <asp:Label ID="lbl_points" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-expand fa-fw "></i>کل بدهی</td>
                                                        <td>
                                                            <asp:Label ID="lbl_totalbed" runat="server" /></td>
                                                    </tr>
                                                 </table>
                                                
                                                <%--<button class="edit">ویرایش ادرس</button>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="order-history">
                                    <div class="header">
                                        <h2>اقساط من</h2>
                                        <a href="#" class="view-all">مشاهده همه</a>
                                    </div>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>شناسه سفارش</th>
                                                <th>تاریخ</th>
                                                <th>مجموع</th>
                                                <th>وضعیت</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>#738</td>
                                                <td>8 دی ,1403</td>
                                                <td>350.000&nbsp;تومان (5 محصول)</td>
                                                <td><span class="status processing">لغو شده</span> <a href="#">مشاهده
                                                    کنید
                                                    جزئیات</a></td>
                                            </tr>
                                            <tr>
                                                <td>#703</td>
                                                <td>8 دی ,1403</td>
                                                <td>250.000&nbsp;تومان (1 محصول)</td>
                                                <td><span class="status on-the-way">تکمیل نشده</span> <a href="#">مشاهده
                                                    کنید
                                                    جزئیات</a></td>
                                            </tr>
                                            <tr>
                                                <td>#130</td>
                                                <td>8 دی ,1403</td>
                                                <td>250.000&nbsp;تومان (4 محصول)</td>
                                                <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                    کنید
                                                    جزئیات</a></td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="v-pills-order-history" role="tabpanel"
                            aria-labelledby="v-pills-order-history-tab" tabindex="0">
                            <div class="order-history2">
                                <div class="header">
                                    <h2>سابقه سفارش</h2>
                                </div>
                                <table>
                                    <thead>
                                        <tr>
                                            <th>شناسه سفارش</th>
                                            <th>تاریخ</th>
                                            <th>مجموع</th>
                                            <th>وضعیت</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>#3933</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (5 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5045</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (1 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5028</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#4600</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#3933</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (5 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5045</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (1 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5028</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#4600</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#3933</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (5 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5045</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (1 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#5028</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>
                                        <tr>
                                            <td>#4600</td>
                                            <td>8 دی ,1403</td>
                                            <td>570.000&nbsp;تومان (4 محصول)</td>
                                            <td><span class="status completed">تکمیل شد</span> <a href="#">مشاهده
                                                کنید
                                                جزئیات</a></td>
                                        </tr>

                                        <!-- Add more rows as necessary -->
                                    </tbody>
                                </table>
                                <div class="pagination">
                                    <a href="#" class="prev"><i class="fa-solid fa-chevron-left"></i></a>
                                    <a href="#" class="page active">01</a>
                                    <a href="#" class="page">02</a>
                                    <a href="#" class="page">03</a>
                                    <a href="#" class="page">04</a>
                                    <a href="#" class="next"><i class="fa-solid fa-chevron-right moz"></i></a>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="v-pills-order-details" role="tabpanel"
                            aria-labelledby="v-pills-order-details-tab" tabindex="0">
                            <div class="order-details">
                                <div class="header">
                                    <h2>جزئیات سفارش <span>14مهر• 1403 • 3 محصول</span></h2>
                                    <a href="#" class="back-to-list">برگشت به لیست</a>
                                </div>
                                <div class="details-wrapper">
                                    <div class="address-section">
                                        <div class="box">
                                            <h4>آدرس صورتحساب</h4>
                                            <div class="content">
                                                <h5><asp:Label runat="server" ID="lblName" /></h5>
                                                <p> <asp:Label runat="server" ID="lblAddress" /></p>
                                                
                                                <p>موبایل: <asp:Label runat="server" ID="lblMobile" /></p>
                                            </div>
                                        </div>
                                        <div class="box">
                                            <h4>آدرس حمل و نقل</h4>
                                            <div class="content">
                                                <h5>طلافروشی نشاط</h5>
                                                <p>شهر ری-ابتدای فداییان اسلام-پلاک 28</p>
                                               
                                                <p>تلفن: <a href="tel: 021-55901823 "> 021-55901823 </a></p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="summary-section">
                                        <h5>خلاصه سفارش</h5>
                                        <p><strong>شناسه سفارش:</strong> #4152</p>
                                        <p><strong>روش پرداخت:</strong> پی پال</p>
                                        <p><strong>جمع:</strong>365.000&nbsp;تومان</p>
                                        <p><strong>تخفیف:</strong> 20%</p>
                                        <p><strong>ارسال:</strong> رایگان</p>
                                        <p>
                                            <strong>مبلغ پرداختی:</strong> <span
                                                class="total-amount">292.000&nbsp;تومان</span>
                                        </p>
                                    </div>
                                </div>
                                <div class="progress-bar">
                                    <div class="step completed">
                                        <i class="fa-solid fa-check"></i>
                                        <span>سفارش دریافت شد</span>
                                    </div>
                                    <div class="step active">
                                        <span class="circle">02</span>
                                        <span>در حال پردازش</span>
                                    </div>
                                    <div class="step">
                                        <span class="circle">03</span>
                                        <span>در راه</span>
                                    </div>
                                    <div class="step">
                                        <span class="circle">04</span>
                                        <span>تحویل شد</span>
                                    </div>
                                </div>
                                <div class="product-table">
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>محصول</th>
                                                <th>قیمت</th>
                                                <th>کیفیت</th>
                                                <th>مبلغ پرداختی</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <img src="/assets/images/dashboard/dashboard-order-product1.png"
                                                        alt="Product">
                                                    ژاکت یقه بلند
                                                </td>
                                                <td>314.000&nbsp;تومان</td>
                                                <td>x5</td>
                                                <td>370.000&nbsp;تومان</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img src="/assets/images/dashboard/dashboard-order-product2.png"
                                                        alt="Product">
                                                    لباس روان که اغلب
                                                </td>
                                                <td>314.000&nbsp;تومان</td>
                                                <td>x2</td>
                                                <td>328.000&nbsp;تومان</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img src="/assets/images/dashboard/dashboard-order-product3.png"
                                                        alt="Product">
                                                    لباس نزدیک
                                                </td>
                                                <td>326.000&nbsp;تومان</td>
                                                <td>x10</td>
                                                <td>367.000&nbsp;تومان</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                   <%--     <div class="tab-pane fade" id="v-pills-wishlist" role="tabpanel"
                            aria-labelledby="v-pills-wishlist-tab" tabindex="0">
                            <!-- Wishlist Section Start -->
                            <div class="wishlist-wrapper fix bg-white">
                                <div class="container">
                                    <!-- Faq Section S T A R T  -->
                                    <section class="faq-section fix section-padding">
                                        <div class="container">
                                            <div class="faq-wrapper">
                                                <div class="faq-content style-2">
                                                    <div class="faq-accordion mt-4 mt-md-0">
                                                        <div class="accordion" id="accordion">
                                                            <div class="accordion-item mb-3 wow fadeInUp" data-wow-delay=".3s">
                                                                <h5 class="accordion-header">
                                                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                                                        data-bs-target="#faq1" aria-expanded="true" aria-controls="faq1">
                                                                        درگاه بانکی
                                                                    </button>
                                                                </h5>
                                                                <div id="faq1" class="accordion-collapse collapse" data-bs-parent="#accordion">
                                                                    <div class="accordion-body">
                                                                         
                                                                        
                                                                        <div id="panel-3">
                                                                            <div class="pt-3">
                                                                                <asp:RadioButtonList runat="server" ID="rb_dargah">
                                                                                    <asp:ListItem Text="صادرات بدون کارمزد" Value="1" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="زرین پال کارمزد حداکثر 10 هزار تومان" Value="0" Enabled="false"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                                <label for="txt_mablagh">مبلغ</label>
                                                                                <asp:TextBox runat="server" ID="txt_mablagh" CssClass="3dnumber form-control" ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:Button runat="server" ID="linkdargah" CssClass="btn btn-info btn-block" OnClick="linkdargah_Click" Text="انتقال به صفحه پرداخت"></asp:Button>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="accordion-item mb-3 wow fadeInUp" data-wow-delay=".5s">
                                                            <h5 class="accordion-header">
                                                                <button class="accordion-button" type="button" data-bs-toggle="collapse"
                                                                    data-bs-target="#faq2" aria-expanded="false" aria-controls="faq2">
                                                                    پرداخت از کیف پول
                                                                </button>
                                                            </h5>
                                                            <div id="faq2" class="accordion-collapse show" data-bs-parent="#accordion">
                                                                <div class="accordion-body">
                                                                    ما اعلان‌های ذخیره مجدد داریم! اگر محصولی در انبار باز می گردد، به سادگی روی
                                   اندازه محصولی که می خواهید از آن مطلع شوید 
                                       
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </section>
                                </div>
                                
   <!-- Faq Section E N D  -->
                            </div>
                        </div>--%>
                    </div>
                    <div class="tab-pane fade" id="v-pills-settings" role="tabpanel"
                        aria-labelledby="v-pills-settings-tab" tabindex="0">
                        <div class="container">
                        

                            <!-- Billing Address -->
                            <div class="billing-address mt-4">
                                <h4 class="section-title">آدرس صورتحساب</h4>
                                <div class="form-wrapper">
                                    <div class="row mb-4">
                                        <form class="row g-3">
                                            <div class="col-md-6">
                                                 
                                                <input type="text" aria-multiline="true" class="form-control" id="billingFirstName"
                                                    placeholder="آدرس...">
                                            </div>
                                            <div class="col-12">
                                                <button type="submit" class="theme-btn mt-3">ذخیره تنظیمات</button>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>

                          
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
  
        <asp:Label runat="server" ID="lbl_customerdps" />

  



</asp:Content>
