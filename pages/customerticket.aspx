<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerticket.aspx.cs" Inherits="narsShop.customerticket"  MasterPageFile="~/mst.Master"%>
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
  

              <!-- Right Column -->
    <div class="col mb-6 mb-md-5" style="text-align:right">
        
                      <div class="card  border-light shadow-soft">
                      <div class="card-body">
<h5><asp:Label ID="lbl_title" runat="server" /></h5>


          <table width="100%" >
              <tr>
                  <td><asp:Label ID="lbl_subtit" runat="server" /></td>
                  </tr>
          
              <tr>
                      <td><asp:TextBox TextMode="MultiLine" runat="server" ID="txt_cnt" CssClass="form-control" /></td>
              </tr>
              <tr style="text-align:center">
                  <td><asp:Button ID="Btn_save"  runat="server" Text=" ارسال " OnClick="Btn_save_Click" CssClass="btn btn-info btn-block"  /> </td>

                 </tr>
                  <tr><td>
                      <asp:Label ID="lbl_tbl" runat="server"></asp:Label>
        </td></tr>
          </table>
           

<asp:Label runat="server" ID="lbl_notif" CssClass="label label-danger" />


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
