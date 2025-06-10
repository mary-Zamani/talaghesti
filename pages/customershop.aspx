<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customershop.aspx.cs" Inherits="narsShop.customershop"  MasterPageFile="~/mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="~/mst.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <br />
<div class="navbar navbar-nav">
<p class="btn-warning"></p>
    </div>

<!-- Page Container -->
<!-- The Grid -->
 <div class="row mb-f justify-content-between">
 
    <!-- Left Column -->
    <div class="col-lg-8 mb-6 mb-md-5" style="text-align:right">


    



<section class="h-100 h-custom" style="background-color: #eee;">
    <div class="row d-flex justify-content-center align-items-center h-100" style="vertical-align:top">
      <div class="col-lg-8">
        <div class="card">
          <div class="card-body p-0">
              <asp:Label runat="server" ID="list_kala" />

                

              <!--
                <div class="card mb-3 mb-lg-0">
                  <div class="card-body">
                    <div class="d-flex justify-content-between">
                      <div class="d-flex flex-row align-items-center">
                        <div>
                          <img
                            src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img4.webp"
                            class="img-fluid rounded-3" alt="Shopping item" style="width: 65px;">
                        </div>
                        <div class="ms-3">
                          <h5>پلاک پروانه</h5>
                          <p class="small mb-0">زرد</p>
                        </div>
                      </div>
                      <div class="d-flex flex-row align-items-center">
                        <div style="width: 50px;">
                          <h5 class="fw-normal mb-0">0.32 گرم</h5>
                        </div>
                        <div style="width: 80px;">
                          <h5 class="mb-0">16,700,000</h5>
                        </div>
                        <a href="#!" style="color: #cecece;"><i class="fas fa-trash-alt"></i></a>
                      </div>
                    </div>
                  </div>
                </div>
              -->

          </div>
        </div>
      </div>
    

      <!-- shop summery  and pay -->
        <div class="col">
                 <div class="card bg-secondary text-white rounded-3">
                  <div class="card-body">

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

                      <asp:Button runat="server" ID="finalizasale" OnClick="finalizasale_Click" Text="نهایی کردن فاکتور" CssClass="btn btn-info btn-block btn-lg" />

                  </div>
                </div>
            </div>
  </div>
</section>








</div>


     <div class="col mb-6 mb-md-5" style="float:right;text-align:right;max-width:400px">
      <!-- Profile -->
      <div class="card bg-primary shadow-inset border-light">
        <div class="card-body p-5">
         <h4 class="h4 card-title mb-3"><asp:Label runat="server" ID="lbl_customername" /> </h4>
       <!--  <p class="centered"><img src="/img/avatar3.png" class="card-img-top rounded-circle" style="height:106px;width:106px" alt="Avatar" /></p> -->
         <hr />
            <table border="0" width="100%" >
                <tr><td><i class="fa fa-pencil fa-fw "></i>کد مشتری</td><td><asp:Label runat="server" ID="lbl_customercode" /></td></tr>
                <tr><td><i class="fa fa-home fa-fw "></i>آدرس</td><td><asp:Label runat="server" ID="lbl_customeraddress" /></td></tr>
                <tr><td><i class="fa fa-phone fa-fw "></i>تلفن</td><td><asp:Label runat="server" ID="lbl_customerphone" /></td></tr>
                <tr><td><i class="fa fa-user fa-fw "></i>شماره ملی</td><td><asp:Label runat="server" ID="lbl_shmeli" /></td></tr>
                <tr><td colspan="2"><hr /></td></tr>
                <tr><td><i class="fa fa-dollar fa-fw "></i>کیف پول</td><td><asp:Label ID="lbl_kif" runat="server" /></td></tr>
                <tr><td><i class="fa fa-star fa-fw "></i>امتیاز</td><td><asp:Label ID="lbl_points" runat="server" /></td></tr>
                <tr><td><i class="fa fa-expand fa-fw "></i>کل بدهی</td><td><asp:Label ID="lbl_totalbed" runat="server" /></td></tr>
                <tr><td colspan="2"><hr /></td></tr>
            </table>
        </div>
      </div>

      <!-- Accordion -->
      <div class="card border-light shadow-soft" >
        <ul class="footer-links list-unstyled mt-2" >
          <li class="mb-1"> <a class="p-2" href="customeraccount.aspx"><i class="fa fa-shopping-cart fa-fw"></i> اقساط من</a></li>
          <li class="mb-1"><a class="p-2" href="customercharge.aspx?type=W"><i class="fa fa-dollar fa-fw"></i> شارژ کیف پول </a></li>
          <li class="mb-1"><a class="p-2" href="customerticket.aspx"><i class="fa fa-comment fa-fw"></i> پشتیبانی سایت </a>         </li>
          <li class="mb-1"><a class="p-2" href="/index.aspx"><i class="fa fa-shopping-cart fa-fw"></i> صفحه فروشگاه</a>         </li>
          <li class="mb-1"><a class="p-2" href="/pages/customerexit.aspx"><i class="fa fa-sign-out fa-fw"></i> خروج </a>         </li>
        </ul>      
      </div>
      
      
 
    <!-- End Left Column -->
    </div>

     </div>

</asp:Content>



