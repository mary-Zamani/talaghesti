<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="weblogin.aspx.cs" Inherits="narsShop.weblogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            function opensignup() {
                $("#signin_pan").hide(); // مخفی کردن فرم ورود
                $("#signup_pan").show(); // نمایش فرم ثبت نام
            }

            function opensignin() {
                $("#signup_pan").hide(); // مخفی کردن فرم ثبت نام
                $("#signin_pan").show(); // نمایش فرم ورود
            }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <!-- Login Section start  -->
 <section id="sectionLogin" runat="server" class="login-section fix section-padding">
     <div class="container">
         <div class="login-wrapper">
             <div class="row gx-5"   ">
                 <div class="col-xl-6 offset-xl-0 col-md-8 offset-md-2" >
                     <div class="contact-info-area" >
                          <div id="signin_pan">
                         <div class="contact-content" >
                            
                                 <p><asp:Label ID="lbl_why" runat="server" /></p>
                             <h2 class="contact-content__title">اکنون شروع کنید</h2>
                             <p class="contact-content__subtitle">برای دسترسی به حساب کاربری خود اطلاعات کاربری خود را وارد کنید</p>
                          
                                 <div class="row g-4">
                                     <div class="col-lg-12 wow fadeInUp" data-wow-delay=".5s">
                                         <div class="form-clt">
                                            <span>نام کاربری*</span>
                                             <asp:TextBox runat="server" ID="txt_username" TextMode="SingleLine" 
     placeholder="شماره همراه" cssClass="form-control number" />
                                         </div>
                                     </div>
                                     <div class="col-lg-12 wow fadeInUp" data-wow-delay=".7s">
                                         <div class="form-clt">
                                             <span>رمز عبور*</span>
                                               <asp:TextBox runat="server" ID="txt_password" TextMode="Password" cssClass="form-control  number" placeholder="شماره ملی یا کد حساب"/>

                                         </div>
                                     </div>
                              
                                     <div class="col-lg-12 wow fadeInUp" data-wow-delay=".9s">
                                       <%--  <button type="submit" class="theme-btn style6">
                                        واردشوید
                                         </button>--%>

                                         <asp:Button runat="server" ID="btn_login"  
   class="theme-btn style6" type="button" Text="وارد شوید" OnClick="btn_login_Click"/>
                                     </div>
                                 </div>
                       

                             

                             <h5 class="contact-content__logtitle center">حساب کاربری ساخته شده ندارید؟ 
                                   <button type="button" class="btn btn-outline-danger" onclick="opensignup();" >ایجاد کنید</button></h5>
                                 </div>
                         </div>
                         <%-- ثبت نام --%>
     <div id="signup_pan" class="mt-2" style="display:none;">
    <h2 class="contact-content__title">اکنون شروع کنید</h2>
    <p class="contact-content__subtitle">برای دسترسی به حساب کاربری خود اطلاعات کاربری خود
        را وارد کنید</p>
   
        <div class="row g-4">

                 <div class="col-lg-12 wow fadeInUp" data-wow-delay=".3s">
         <div class="form-clt">
             <span>شماره همراه*</span>
           <asp:TextBox runat="server" ID="Txt_mobile" TextMode="SingleLine" 
     placeholder="شماره همراه" cssClass="form-control  number" />
         </div>
     </div>

            <div class="col-lg-12 wow fadeInUp" data-wow-delay=".5s">
                <div class="form-clt">
                    <span>شماره ملی*</span>
                     <asp:TextBox runat="server" ID="Txt_shmeli" TextMode="SingleLine" cssClass="form-control  number" placeholder="شماره ملی"/>

                </div>
            </div>
            <div class="col-lg-12 wow fadeInUp" data-wow-delay=".7s">
                <div class="form-clt">
                    <span>نام و نام خانوادگی*</span>
                    <asp:TextBox runat="server" ID="Txt_name" TextMode="SingleLine" cssClass="form-control" placeholder="نام و نام خانوادگی"/>

                </div>
            </div>
            <div class="col-lg-12 wow fadeInUp" data-wow-delay=".9s">
                <div class="form-clt">
                    <span>کد معرف - اختیاری</span>
                    <asp:TextBox runat="server" ID="Txt_moaref" TextMode="SingleLine" cssClass="form-control  number" placeholder="کد معرف - اختیاری"/>
                </div>
            </div>
        
            <div class="col-lg-12 wow fadeInUp" data-wow-delay=".9s">
           <%--     <asp:Button runat="server" ID="Btn_signup"  
     class="theme-btn style6" Text="ثبت نام" OnClick="Btn_signup_Click" />--%>
                <asp:Button runat="server" ID="Btn_signup"   class="theme-btn style6" 
      type="button" Text="ثبت نام" OnClick="Btn_signup_Click" />
            </div>
        </div>
 

   

   
    <h5 class="contact-content__logtitle center">حساب کاربری دارید؟   <button type="button" runat="server"  class="btn btn-outline-danger" onclick="opensignin();">وارد شوید</button></h5>
                <asp:Label ID="lbl_respond" CssClass="panel-warning" runat="server" ></asp:Label>
</div>
                         <%--end--%>
                     </div>
                 </div>
                <div class="col-xl-6 offset-xl-0 col-md-8 offset-md-2">
    <div class="login-thumb">
        <img src="assets/images/register/loginThumb.jpg" alt="register-thumb">
    </div>
</div>
             </div>
         </div>
     </div>
 </section>
</asp:Content>
