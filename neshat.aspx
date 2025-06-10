<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="neshat.aspx.cs" Inherits="narsShop.neshat"  MasterPageFile="mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="mst.Master" %> 

<asp:Content ID="c2" ContentPlaceHolderID="cphPageMetaData" runat="server">
     <link type="text/css" rel="stylesheet" href="Styles/weblogin.css" />
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 
          <div class="col-lg-3 text-right col-md-3">
                  <asp:Label runat="server" ID="lbl1" />
              </div>

<section class="hero-section">
  <asp:label runat="server" ID="lbl_hero" />
</section>

  <asp:label runat="server" ID="lbl_banners" />

<section class="women-banner spad">
  <div class="container-fluid">
      <div class="row">       
          <div class="col-lg-12 offset-lg-1">
              <div class="filter-control">
             
<p style="  margin-left: 25%;
  margin-right: 25%;
  direction: rtl;
  text-align: right;">
این روزا دارم بیشتر به نوسانات طلا و شرایط اقتصادی جامعه فکر میکنم چون هر روز که بیدار میشم قیمت طلا یه پله صعود کرده...

یکی میخواد  ازدواج کنه نمیتونه حلقه ازدواج بخره، یکی میخواد برای عروسش زیرلفظی طلا بخره قیمت طلا رو که میبینه پشیمون میشه، یکی پای آبروش وسطه و....

حالا مسئولیت خوب کردن حال دل تو با منه....!

یه لحظه یاد این شعر حافظ افتادم که میگه :
آسمان بار امانت نتوانست کشید / قرعه کار به نام من دیوانه زدند

روزی روزگاری جوون بودم و این طلافروشی رو افتتاح کردم ! ۲۵ سال میگذره و الان دیگه گرد پختگی رو سرم نشسته! ایشالا که ته دیگ نشم 😉!

الان دغدغم شده رسیدن تو به آرزو هات.... دغدغه تو.... آبروت.... ازدواجت.... آیندت.... پروازیست به اوج تنهایی خودم... موقع پریدنت خوشحالم داری میپری... خیالم راحته داری سر و سامون می‌گیری و میری..... .

حس هام قاطی پاتی شده...

تویی که داری اینو میخونی بی معرفت نشی هرازچندگاهی که دلت برای ما تنگ شد احوالی بپرس و سری به پیج و طلافروشی نشاط بزن.

به قول شاعر میگه:
طلوع بی شمار معرفت باش/ به شهری که رسمش بی وفائیست

جوون تر ها ازدواج کردند و پیرتر ها مردند و بچه ها تنها تر شدند و ما هنوز 25 ساله اینجاییم... !

هیچکس از فردا خبر نداره، قدر امروز و این لحظه رو بدون و از داشته هات لذت ببر...

فروش اقساطی طلا از فروشگاه طلاقسطی ( طلافروشی نشاط ) با یه کادر توپ بهت کمک میکنه تا از امروزت بهتر استفاده کنی و آیندت و تضمین کنی😉

در نهایت خدای عزوجل همین حوالیه و حواسش به همه ما هست...  !


</p>
                   </div>
    </div>
           </div>
      
 <div class="container" style="text-align:center;">
      <div class="benefit-items">
          <div class="row">
              <div class="col-lg-3  gradient-custom-2">
                  برای ارتباط با ما می تونی به شماره های زیر پیام بدی <br />
                  
    09903636483<br />
    09333691761<br />
    09903637429<br />
    09903636428<br />
    09905700240<br />

                  </div>
              <div class="col-lg-1" ></div>
              <div class="col-lg-4  gradient-custom-2" >
                  یا اینکه فرم زیر رو پر کنی و در نهایت دکمه ارسال رو بزنی <br />
                  فقط اینکه دقت کن شماره موبایلت رو درست وارد کنی و اسم نازنینت رو هم کامل برامون بفرستی <br />
                  <asp:TextBox ID="txtmbl" placeholder="شماره موبایل" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox ID="txtname" placeholder="نام و نام خانوادگی" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox TextMode="MultiLine" ID="txtpayam" placeholder="متن پیام" runat="server"  style="width:100%"></asp:TextBox><br />
                   <asp:Image runat="server" id="capcha" src="JpegImage.aspx" style="width:100%" /><br />
                  <table width="100%"><tr><td><asp:label  ID="Lbl_entercap" runat="server" Text="عبارت امنیتی را وارد کنید" ></asp:label></td>
                      <td><asp:TextBox ID="txt_cap" runat="server" MaxLength="6" Width="100%"  ></asp:TextBox></td></tr></table>
                  <asp:Button ID="Btnsend" runat="server"  style="width:100%" Text="ارسال" OnClick="Btnsend_Click"/>
                  <asp:Label runat="server" ID="lbl_error" CssClass="errorLine" />
                  </div>
              <div class="col-lg-1" ></div>
              <div class="col-lg-3  gradient-custom-2" >
                  یا اینکه در ساعت های کاری به یکی از این آدرس های زیر مراجعه کنی :

                  
    آدرس : شهر ری ابتدای فداییان اسلام پلاک 28  <br />
    تلفن: 02155901823        <br />
    شعبه دوم : فلکه دوم صادقیه پاساژ طلا بالای بانک ملت پلاک 23   <br />

                  </div>
              </div>
          </div>
      </div>
 </div>
</section>





<section class="latest-blog spad">
  <div class="container">
      <div class="benefit-items">
          <div class="row">
              <div class="col-lg-4">
                  <div class="single-benefit">
                      <div class="sb-icon">
                          <img src="img/checkpoint.png" alt=""  style="max-width: 50px;"/>
                      </div>
                      <div class="sb-text">
                          <h6>تضمین 18 عیار</h6>
                          <p>برای تمام طلاهای تمام شعب<pre wp-pre-tag-1=""></pre>
                      </div>
                  </div>
              </div>
              <div class="col-lg-4">
                  <div class="single-benefit">
                      <div class="sb-icon">
                          <img src="img/checkpoint.png" alt=""  style="max-width: 50px;"/>
                      </div>
                      <div class="sb-text">
                          <h6>تضمین بازگشت وجه</h6>
                          <p>به مدت یک هفته از تاریخ فاکتور</p>
                      </div>
                  </div>
              </div>
              <div class="col-lg-4">
                  <div class="single-benefit">
                      <div class="sb-icon">
                          <img src="img/checkpoint.png" alt="" style="max-width: 50px;" />
                      </div>
                      <div class="sb-text">
                          <h6>گارانتی معاضه</h6>
                          <p>به مدت یک ماه از تاریخ فاکتور</p>
                      </div>
                  </div>
              </div>
          </div>
      </div>
  </div>
</section>





</asp:Content>
