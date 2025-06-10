<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weblogin_old.aspx.cs" Inherits="narsShop.weblogin_old" MasterPageFile="mst.Master" %>
<%@ MasterType VirtualPath="mst.Master" %> 

 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- <link type="text/css" rel="stylesheet" href="Styles/weblogin.css" /> -->
    <script type="text/javascript">
        function opensignup() {
            $("#signin_pan").css("visibility", "collapse");
            $("#signin_pan").css("height", "0px");
            $("#signup_pan").css("visibility", "visible");
            $("#signup_pan").css("height", "auto");
        }
        function opensignin() {
            $("#signin_pan").css("visibility", "visible");
            $("#signin_pan").css("height", "auto");
            $("#signup_pan").css("visibility", "collapse");
            $("#signup_pan").css("height", "0px");
        }
    </script>


    <section class="h-100 gradient-form" style="background-color: #eee;">
   <div class="container py-5 h-100">
    <div class="row justify-content-center align-items-center h-100">
       
        <div class="col-12">
        <div class="card rounded-3 text-black" style="margin-top:40px">
          <div class="row g-0 justify-content-center">
            <div class="col-lg-6 col-sm-12">
              <div class="card-body p-md-5 mx-md-4">

                <div class="text-center" style="display:none">
                  <img src="img/logo.png" style="width: 80px" alt="logo" />
                </div>

                
                    <div id="signin_pan">
                  <p><asp:Label ID="lbl_why" runat="server" /></p>

                  <div class="form-outline mb-4">
                    <label class="form-label" for="txt_username">نام کاربری</label>
                      <asp:TextBox runat="server" ID="txt_username" TextMode="SingleLine" 
                          placeholder="شماره همراه" cssClass="form-control number" />
                  </div>

                  <div class="form-outline mb-4">
                    <label class="form-label" for="txt_password">رمز عبور</label>
                      <asp:TextBox runat="server" ID="txt_password" TextMode="Password" cssClass="form-control  number" placeholder="شماره ملی یا کد حساب"/>

                  </div>

                  <div class="text-center pt-1 mb-5 pb-1">
                    <asp:Button runat="server" ID="btn_login" style="color:black"
                        cssClass="btn btn-primary btn-block fa-lg mb-3" type="button" Text="ورود" OnClick="btn_login_Click"/>
                    <a class="text-muted" href="#"> رمز عبور خود را فراموش کرده اید؟</a>
                  </div>

                  <div class="d-flex align-items-center justify-content-center pb-4">
                    <p class="mb-0 me-2">حساب کاربری ندارید؟</p>
                    <button type="button" class="btn btn-outline-danger" onclick="opensignup();" >ایجاد کنید</button>
                  </div>
                        </div>
                    <div id="signup_pan" class="mt-2" style="visibility:collapse;height:0px;">

  <div class="form-outline mb-4">
                      <asp:TextBox runat="server" ID="Txt_mobile" TextMode="SingleLine" 
                          placeholder="شماره همراه" cssClass="form-control  number" />
                    
                  </div>

                  <div class="form-outline mb-4">
                      <asp:TextBox runat="server" ID="Txt_shmeli" TextMode="SingleLine" cssClass="form-control  number" placeholder="شماره ملی"/>

                    
                  </div>

             <div class="form-outline mb-4">
                      <asp:TextBox runat="server" ID="Txt_name" TextMode="SingleLine" cssClass="form-control" placeholder="نام و نام خانوادگی"/>


                  </div>

             <div class="form-outline mb-4">
                      <asp:TextBox runat="server" ID="Txt_moaref" TextMode="SingleLine" cssClass="form-control  number" placeholder="کد معرف - اختیاری"/>
                  </div>


                        <div class="text-center pt-1 mb-5 pb-1">
                    <asp:Button runat="server" ID="Btn_signup" style="color:black"
                        cssClass="btn btn-primary btn-block fa-lg gradient-custom-2 mb-3" type="button" Text="ثبت نام" OnClick="Btn_signup_Click" />
                  </div>

                  <div class="d-flex align-items-center justify-content-center pb-4">
                    <p class="mb-0 me-2">قبلا ثبت نام کرده اید؟</p>
                    <button type="button" class="btn btn-outline-danger" onclick="opensignin();">وارد شوید</button>
                  </div>



                    </div>
                    <asp:Label ID="lbl_respond" CssClass="panel-warning" runat="server" ></asp:Label>
               

              </div>
            </div>
<!--            <div class="col-lg-6 col-sm-12 align-items-center gradient-custom-2">
              <div class="text-black px-3 py-4 p-md-5 mx-md-4">
                <h4 class="mb-4">چیزی بیش از یک طلا فروشی</h4>
                <p class="small mb-0 centered">
                    طلا فروشی نشاط با بیش از 15 سال سابقه در فروش طلا و جواهر در منطقه شهرری از 
                    فروشگاه های خوشنام و مردمی این منطقه است. مدیریت این طلا فروشی مشتری مداری
                    را سرمشق کلیه پرسنل خود قرارداده و طی این مدت اثبت کرده در این امر رقیبی ندارد.
                    طلا فروشی نشاط از اولین و مطرح ترین طلافروشی هایی ست که فروش قسطی طلا را بدون دردسر های 
                    معمول برای تهیه چک و ضامن و در جهت کمک به حفظ سرمایه های مردم در بازار تورمی آغاز کرد. 
                    طلافروشی نشاط اینک در دوشعبه شهرری و آریا شهر و شعبه آنلاین در خدمت شما عزیزان است.
                </p>
              </div>
            </div> -->
          </div>
        
   
    </div>
      </div>
    </div>
 
    </div>
    
</section>
     </asp:Content>
