<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customershop.aspx.cs" Inherits="narsShop.pages.customershop" %>
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
                             
                                    <table border="0">
                                                    <tr>
                                                        <td><i class="fa fa-pencil fa-fw "></i>کد مشتری</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-home fa-fw "></i>آدرس</td>
                                                        <td>
                                                          <asp:Label runat="server" ID="Label2" /></td>
                                                    </tr>
                                             
                                                   <tr>
                                                        <td>  <i class="fa fa-phone fa-fw "></i>تلفن</td>
                                                        <td>
                                                          <asp:Label runat="server" ID="Label3" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-user fa-fw "></i>شماره ملی</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label4" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                
                                                     <tr>
                                                        <td><i class="fa fa-dollar fa-fw "></i>کیف پول</td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-star fa-fw "></i>امتیاز</td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td><i class="fa fa-expand fa-fw "></i>کل بدهی</td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" /></td>
                                                    </tr>
                                                 </table>
                                <button class="nav-link" id="v-pills-wishlist-tab"   aria-selected="false">
                                    <i
                                        class="fa-light fa fa-shopping-cart"></i>اقساط من</button>
                                <button class="nav-link" id="v-pills-wishlist-tab1" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-wishlist" type="button" role="tab"
                                    aria-controls="v-pills-wishlist" aria-selected="false">
                                    <i
                                        class="fa-light fa fa-dollar"></i>شارژ کیف پول</button>
                                <button class="nav-link" id="v-pills-wishlist-tab2"  
                                    aria-controls="v-pills-wishlist" aria-selected="false">
                                    <i
                                        class="fa-light fa fa-comment"></i>پشتیبانی سایت</button>
                                <asp:LinkButton ID="btnShop" runat="server" CssClass="nav-link" PostBackUrl="/index.aspx">
    <i class="fa-light fa fa-shopping-cart"></i> فروشگاه 
</asp:LinkButton>



                              
                                <asp:LinkButton ID="btnExists" runat="server" CssClass="nav-link" >
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
                              
                                <div class="order-history2">
                                  
                                    <!-- Cart Section Start -->
    <div class="cart-wrapper  section-padding fix bg-white">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                    <div class="table_desc">
                        <div class="table_page table-responsive">
                            <table>
                                <!-- Start Cart Table Head -->
                                <thead>
                                    <tr>
                                        <th class="product_name text-start">محصول</th>
                                        <th class="product-price">شناسه</th>
                                        <th class="product_quantity">گرم</th>
                                        <th class="product_total">قیمت</th>
                                        <th class="product_remove">حذف</th>
                                    </tr>
                                </thead> <!-- End Cart Table Head -->
                                <tbody>
                                    <!-- Start Cart Single Item-->
                                    <tr>
                                        <td class="product_thumb">
                                            <a href="#!">
                                                <img src="/assets/images/cart/cart-thumb1_1.jpg" alt="img"></a>
                                            <a class="product-name" href="#!">لباس کوتاه</a>
                                        </td>
                                        <td class="product-price">150.000&nbsp;تومان</td>
                                        <td class="product_quantity">
                                            <div class="plus-minus-input">
                                              
                                            </div>
                                        </td>
                                        <td class="product_total">30.000&nbsp;تومان</td>
                                        <td class="product_remove"><a href="#"><svg xmlns="http://www.w3.org/2000/svg"
                                                    width="24" height="24" viewBox="0 0 24 24" fill="none">
                                                    <g clip-path="url(#clip0_229_10585)">
                                                        <path
                                                            d="M12 23C18.0748 23 23 18.0748 23 12C23 5.92525 18.0748 1 12 1C5.92525 1 1 5.92525 1 12C1 18.0748 5.92525 23 12 23Z"
                                                            stroke="#E5E5E5" stroke-miterlimit="10" />
                                                        <path d="M16 8L8 16" stroke="#5F5F5F" stroke-width="1.5"
                                                            stroke-linecap="round" stroke-linejoin="round" />
                                                        <path d="M16 16L8 8" stroke="#5F5F5F" stroke-width="1.5"
                                                            stroke-linecap="round" stroke-linejoin="round" />
                                                    </g>
                                                    <defs>
                                                        <clipPath id="clip0_229_105852244">
                                                            <rect width="24" height="24" fill="white" />
                                                        </clipPath>
                                                    </defs>
                                                </svg></a></td>
                                    </tr> <!-- End Cart Single Item-->
                                   
                                   
                                </tbody>
                            </table>
                        </div>
                    </div>
                 
                </div>
            </div>
            <div class="cart-checkout-wrapper">
                <div class="coupon_code right" data-aos="fade-up" data-aos-delay="400">
                   
                    <div class="coupon_inner">
                  
                        <div class="cart_subtotal ">
                              <div class="d-flex justify-content-between">
                      <p class="mb-2">جمع</p>
                      <p class="mb-2"><asp:Label ID="l_price_a" runat="server"></asp:Label></p>
                    </div>

                    <div class="d-flex justify-content-between">
                      <p class="mb-2"></p>
                      <p class="mb-2"></p>
                    </div>

                    <div class="d-flex justify-content-between">
                      <p class="mb-2">جمع کل</p>
                      <p class="mb-2"><asp:Label ID="l_price" runat="server"></asp:Label></p>
                    </div>
                    <div class="d-flex justify-content-between">
                      <p class="mb-2">تعداد اقساط</p>
                      <p class="mb-2"><asp:Label ID="l_tedadghest" runat="server" /></p>
                    </div>

                    <div class="d-flex justify-content-between">
                      <p class="mb-2">پیش پرداخت</p>
                      <p class="mb-2"><asp:Label ID="l_prepay" runat="server"></asp:Label></p>
                    </div>
                    <div class="d-flex justify-content-between mb-4">
                      <p class="mb-2">مبلغ هر قسط</p>
                      <p class="mb-2"><asp:Label ID="l_ghest" runat="server"></asp:Label></p>
                    </div>
                        </div>

                      
                        <div class="checkout-btn">
                            <a href="checkout.html" class="theme-btn style6">نهایی کردن فاکتور</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
               
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
