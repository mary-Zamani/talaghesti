<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="narsShop.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <!-- Hero Section start  -->
        <section id="SectionBaner" runat="server" class="hero-section hero-5 bg-cover"
            style="background-image: url('/assets/images/home-5/hero/bg-image.jpg');">
            <div class="discount-shape">
               <a href="#"><img src="assets/images/home-5/hero/discount.png" alt="img"></a> 
            </div>
            <div class="array-button">
                <button class="array-prev"><i class="far fa-chevron-up"></i></button>
                <button class="array-next"><i class="far fa-chevron-down"></i></button>
            </div>
            <div class="container">
                <div class="swiper hero-slider-2">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <div class="row g-4 align-items-center">
                                <div class="col-lg-6">
                                    <div class="hero-content">
                                        <span>زیبای خودرا بروز دهید</span>
                                        <h1>
                                            <!--مجموعه جواهرات لوکس منحصر به فرد-->
                                            فروش ویژه طلاهای تخفیفی

                                    </h1>
                                        <p>
                                            طلای سرمایه‌ای، انتخاب هوشمندانه!
                                        </p>
                                        <p>
                                            عرضه‌ی محدود با کمترین حاشیه سود
                                        </p>
                                        <p>
                                            📌 فقط فروش نقدی – بدون اقساط
                                           
                                        </p>
                                        <p>
                                            ⏳ موجودی محدود – فرصت رو از دست نده!
                                   
                                        </p>
                                        <!--<a href="shop-details-one.html" class="theme-btn">خرید</a>-->
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="hero-image">
                                        <img src="/assets/images/home-5/hero/01.png" alt="img">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-slide">
                            <div class="row g-4 align-items-center">
                                <div class="col-lg-6">
                                    <div class="hero-content">
                                        <span>تجربه یک خرید پر نشاط با نشاط</span>
                                        <h1>دوشنبه قرطی

                                    </h1>
                                        <p>
                                            طلای سرمایه‌ای، انتخاب هوشمندانه!
                                        عرضه‌ی محدود با کمترین حاشیه سود
                                        📌 فقط فروش نقدی – بدون اقساط
                                        </p>
                                        <p>
                                            ⏳ موجودی محدود – فرصت رو از دست نده!
                                   
                                        </p>
                                        <a href="#" class="theme-btn">خرید</a>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="hero-image">
                                        <img src="/assets/images/home-5/hero/33.png" alt="img">
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        

      <%--   <!-- Jewelry Category Section start  -->
 <section class="jewelry-category-section section-padding2 fix" id="category">
     <div class="container">
         <div class="row gy-5">
             <div class="section-title text-center style-5">
                 <div class="subtitle">افتخارات  ما</div>
                
             </div>
             <div class="swiper jewelry-category-slider">
                 <div class="swiper-wrapper">
                     <div class="swiper-slide">
                         <div class="jewelry-category-box">
                             <div class="thumb">
                                 <img src="assets/images/home-5/category/01.png" alt="img">
                             </div>
                             <h4><a href="shop-details-one.html">ستارگان</a></h4>
                         </div>
                     </div>
                     <div class="swiper-slide">
                         <div class="jewelry-category-box">
                             <div class="thumb">
                                 <img src="assets/images/home-5/category/02.png" alt="img">
                             </div>
                             <h4><a href="shop-details-one.html">ستارگان</a></h4>
                         </div>
                     </div>
                     <div class="swiper-slide">
                         <div class="jewelry-category-box">
                             <div class="thumb">
                                 <img src="assets/images/home-5/category/03.png" alt="img">
                             </div>
                             <h4><a href="shop-details-one.html">ستارگان</a></h4>
                         </div>
                     </div>
                     <div class="swiper-slide">
                         <div class="jewelry-category-box">
                             <div class="thumb">
                                 <img src="assets/images/home-5/category/04.png" alt="img">
                             </div>
                             <h4><a href="shop-details-one.html">ستارگان</a></h4>
                         </div>
                     </div>
                     <div class="swiper-slide">
                         <div class="jewelry-category-box">
                             <div class="thumb">
                                 <img src="assets/images/home-5/category/05.png" alt="img">
                             </div>
                             <h4><a href="shop-details-one.html">ستارگان</a></h4>
                         </div>
                     </div>
                 </div>
             </div>
         </div>
     </div>
 </section>--%>
     
        <!-- Best seller product Section start  -->
        <section id="SectionCategory" runat="server" class="best-seller-product-items-section section-padding2 bg-color3 fix">
            <div class="best-seller-product-items-container-wrapper style4">
                <div class="container">
                    <div class="best-seller-product-items-wrapper style1 text-center mb-30">
                        <div class="section-title">
                        </div>
                    </div>
                    <div class="feature-flex-tab-wrapper">
                        <div class="feature-tab-btn-wrapper">
                            <ul class="nav nav-pills" id="pills-tab" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="pills-one-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-one" type="button" role="tab" aria-controls="pills-one"
                                        aria-selected="true">
                                        همه
                               
                                    </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="pills-two-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-two" type="button" role="tab" aria-controls="pills-two"
                                        aria-selected="false">
                                        سرویس
                               
                                    </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="pills-three-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-three" type="button" role="tab" aria-controls="pills-three"
                                        aria-selected="false">
                                        گردنبند و پلاک
                               
                                    </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="pills-four-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-four" type="button" role="tab" aria-controls="pills-four"
                                        aria-selected="true">
                                        انواع دستبند
                               
                                    </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="pills-five-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-five" type="button" role="tab" aria-controls="pills-five"
                                        aria-selected="false">
                                        انگشتر و حلقه
                               
                                    </button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="pills-six-tab" data-bs-toggle="pill"
                                        data-bs-target="#pills-six" type="button" role="tab" aria-controls="pills-six"
                                        aria-selected="false">
                                        اکسسوری ها
                               
                                    </button>
                                </li>
                            </ul>
                        </div>

                    </div>

                    <div class="tab-content" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills-one" role="tabpanel"
                            aria-labelledby="pills-one-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=13">
                                                <img src="/assets/images/image/servis.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=13">سرویس ها</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=10">
                                                <img src="/assets/images/image/nim-set.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=10">نیم ست ها</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=14">
                                                <img src="/assets/images/image/gardanband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=14">گردنبند </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=17">
                                                <img src="/assets/images/image/zanjir.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=17">زنجیر </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=36">
                                                <img src="/assets/images/image/ru-lebasi.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=36">رولباسی </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=18">
                                                <img src="/assets/images/image/pelak.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=18">پلاک </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=15">
                                                <img src="/assets/images/image/dastband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=15">دستبند</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=12">
                                                <img src="/assets/images/image/alango.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=12">النگو</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=20">
                                                <img src="/assets/images/image/halghe-set.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=20">حلقه ست</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=19">
                                                <img src="/assets/images/image/soliter.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=19">حلقه سولیتر</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>


                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=23">حلقه رینگی
                                                <img src="/assets/images/image/ring.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=23">حلقه رینگی</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=11">انگشتر
                                                <img src="/assets/images/image/angoshtar.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=11">انگشتر
                                                </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=21">
                                                <img src="/assets/images/image/aviz-saat.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=21">آویز ساعت</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=16">
                                                <img src="/assets/images/image/gushvare.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=16">گوشواره</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>


                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=37">
                                                <img src="/assets/images/image/pierce.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=37">پیرسینگ</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=34">
                                                <img src="/assets/images/image/paband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=34">پابند
                                                </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-two" role="tabpanel" aria-labelledby="pills-two-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=13">
                                                <img src="/assets/images/image/servis.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=13">سرویس ها</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=10">
                                                <img src="/assets/images/image/nim-set.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=10">نیم ست ها</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-three" role="tabpanel" aria-labelledby="pills-three-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=14">
                                                <img src="/assets/images/image/gardanband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=14">گردنبند </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=17">
                                                <img src="/assets/images/image/zanjir.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=17">زنجیر </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=36">
                                                <img src="/assets/images/image/ru-lebasi.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=36">رولباسی </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=18">
                                                <img src="/assets/images/image/pelak.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=18">پلاک </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-four" role="tabpanel" aria-labelledby="pills-four-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>
                                           
                                           
                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=15">
                                                <img src="/assets/images/image/dastband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=15">دستبند</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=12">
                                                <img src="/assets/images/image/alango.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=12">النگو</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-five" role="tabpanel" aria-labelledby="pills-five-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=20">
                                                <img src="/assets/images/image/halghe-set.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=20">حلقه ست</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=19">
                                                <img src="/assets/images/image/soliter.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=19">حلقه سولیتر</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>


                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=23">
                                                <img src="/assets/images/image/ring.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=23">حلقه رینگی</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=11">
                                                <img src="/assets/images/image/angoshtar.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=11">انگشتر
                                                </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-six" role="tabpanel" aria-labelledby="pills-six-tab">
                            <div class="row g-4">
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=21">
                                                <img src="/assets/images/image/aviz-saat.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=21">آویز ساعت</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=16">
                                                <img src="/assets/images/image/gushvare.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=16">گوشواره</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>


                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                             <a href="/pages/productlist.aspx?parent=37">
                                                 <img src="/assets/images/image/pierce.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=37">پیرسینگ</a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="best-seller-product-items-two">
                                        <div class="icon-box2">
                                            <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                                <i class="fa-regular fa-eye"></i>
                                            </button>

                                        </div>

                                        <div class="best-seller-product-items-two__thumb">
                                            <a href="/pages/productlist.aspx?parent=34">
                                                <img src="/assets/images/image/paband.jpg" alt="thumb"></a>
                                        </div>
                                        <div class="best-seller-product-items-two__content">
                                            <div class="best-seller-product-items-two__details">

                                                <h6 class="best-seller-product-items-two__details--title">
                                                    <a href="/pages/productlist.aspx?parent=34">پابند
                                                </a>
                                                </h6>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>


</asp:Content>
