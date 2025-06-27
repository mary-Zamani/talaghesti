<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customercharge_old.aspx.cs" Inherits="narsShop.customercharge_old"  MasterPageFile="~/mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UDC/DateInput.ascx" TagPrefix="uc1" TagName="DateInput" %>

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
  

              <!-- Right Column -->
    <div class="col mb-6 mb-md-5" style="text-align:right">
        
<h3><asp:Label ID="lbl_title" runat="server" /></h3>
<h5><asp:Label ID="lbl_subtit" runat="server" Text="انتخاب شیوه پرداخت:" /></h5>

         
<div id="acord" class="accordion shadow-soft rounded" >

<div class="card card-sm card-body bg-primary border-light mb-0">
    <a href="#panel-3" data-bs-target="#panel-3" class="accordion-panel-header" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="panel-3">
        <span class="h6 mb-0 font-weight-bold">درگاه بانکی</span> 
        <span class="icon"><span class="fa fa-plus"></span></span></a>
    <div id="panel-3">
        <div class="pt-3">
            <asp:RadioButtonList runat="server" ID="rb_dargah">
                <asp:ListItem Text="صادرات بدون کارمزد" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="زرین پال کارمزد حداکثر 10 هزار تومان" Value="0" Enabled="false"></asp:ListItem>
            </asp:RadioButtonList>
            <label for="txt_mablagh">مبلغ</label>
            <asp:TextBox runat="server" ID="txt_mablagh" CssClass="3dnumber form-control"  ClientIDMode="Static" ></asp:TextBox>
            <asp:Button runat="server" ID="linkdargah" CssClass="btn btn-info btn-block" OnClick="linkdargah_Click" Text="انتقال به صفحه پرداخت" ></asp:Button>

        </div></div></div>

  <div class="card card-sm card-body bg-primary border-light mb-0" style="visibility:hidden" >
       <a href="#panel-1" data-bs-target="#panel-1" class="accordion-panel-header collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="panel-1"><span class="h6 mb-0 font-weight-bold">کارت به کارت</span> <span class="icon"><span class="fa fa-plus"></span></span></a>
       <div class="collapse" id="panel-1" style="">
           <div class="pt-3">
               
 
<h3 style="visibility:collapse">  6037691632092243   </h3>
    <h4 style="visibility:collapse">محمد امین سربازی </h4>
 

           

<asp:Label runat="server" ID="lbl_notif" CssClass="label label-danger" />


                           <h4>لطفا جهت تسریع و تسهیل فرایند حساب، از طریق درگاه بانکی پرداخت خود را انجام دهید. 
                               در صورت بروز هر نوع مشکل و لزوم پرداخت بصورت کارت به کارت لطفا
     پس از هماهنگی با مدیر فروش و انجام کارت به کارت بخش زیر را پر نمایید</h4>
                               <table>
         <tr>
             <td>چهار رقم آخر کارت واریز کننده</td>
             <td><asp:TextBox ID="txtkart" runat="server"  style="width:100%" CssClass="form-control number" MaxLength="4"  ></asp:TextBox></td>
         </tr>
         <tr>
             <td>مبلغ پرداختی به ریال</td>
             <td><asp:TextBox ID="txtprice" runat="server"  style="width:100%" CssClass="3dnumber form-control" MaxLength="15" ></asp:TextBox></td>
         </tr>
         <tr>
             <td>کد پیگیری فیش کارت به کارت</td>
             <td><asp:TextBox ID="txtpeigiri" runat="server"  style="width:100%" CssClass="form-control number" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr>
             <td>تاریخ پرداخت</td>
             <td><uc1:DateInput runat="server" ID="txtdate" MyStyle="width:100%" MyClass="number form-control" /></td>
         </tr>
                                        </table>
                            

     <asp:Button ID="Btnsend" runat="server"  Text="ارسال" OnClick="Btnsend_Click" CssClass="btn btn-info btn-block"/>
     <asp:Label runat="server" ID="lbl_error" CssClass="errorLine" />
    </div></div>
                               
                           </div>




    <div class="card card-sm card-body bg-primary border-light mb-0">
      <a href="#panel-2" data-bs-target="#panel-2" class="accordion-panel-header" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="panel-2"><span class="h6 mb-0 font-weight-bold">پرداخت از کیف پول</span> <span class="icon"><span class="fa fa-plus"></span></span></a>
      <div class="collapse" id="panel-2"><div class="pt-3">
           <table width="100%" style="max-width:500px">
     <tr>
         <td></td>
         </tr><tr>
             <td><asp:TextBox runat="server" ID="txt_pr" CssClass="3dnumber form-control" /></td>
     </tr>
     </table>
       <asp:Button ID="Btn_fkif"  runat="server" Text="پرداخت از موجودی کیف پول مشتری" OnClick="Btn_fkif_Click" CssClass="btn btn-info btn-block"  /> 
   </div>
          </div>
      </div>

  </div>



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
          <li class="mb-1"><a class="p-2" href="customerticket.aspx"><i class="fa fa-comment fa-fw"></i> پشتیبانی سایت </a>         </li>
          <li class="mb-1"><a class="p-2" href="/index.aspx"><i class="fa fa-shopping-cart fa-fw"></i> صفحه فروشگاه</a>         </li>
          <li class="mb-1"><a class="p-2" href="/pages/customerexit.aspx"><i class="fa fa-sign-out fa-fw"></i> خروج </a>         </li>

        </ul>      
      </div>
      
      
 
    <!-- End Left Column -->
    </div>
</div>
    







</asp:Content>
