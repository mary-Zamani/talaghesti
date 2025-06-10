<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pghestinfo.aspx.cs" Inherits="narsShop.pghestinfo"  MasterPageFile="mst.Master"%>
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

مشتری عزیز طلا قسطی - نشاط <br />
    لطفا پس از پرداخت قسط به حساب کارت طلافروشی، اطلاعات فرم زیر رو ارسال کن تا ما بتونیم سریعا این مبلغ رو در حساب شما ثبت کنیم.

</p>
                   </div>
    </div>
           </div>
      
 <div class="container" style="text-align:center;">
      <div class="benefit-items">
          <div class="row">
  
              <div class="col-lg-4  gradient-custom-2" >
                  <h4>ارسال مشخصات اقساط پرداختی</h4>
                  <asp:TextBox ID="txtmbl" placeholder="شماره موبایل" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox ID="txtname" placeholder="نام و نام خانوادگی" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox ID="Txtdaftar" placeholder="شماره دفتر" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox ID="txtkart" placeholder="چهار رقم آخر شماره کارت" runat="server"  style="width:100%"></asp:TextBox><br />
                  <asp:TextBox ID="txtprice" placeholder="مبلغ پرداختی به ریال" runat="server"  style="width:100%" CssClass="3dnumber"></asp:TextBox><br />
                  <asp:TextBox ID="txtdate" placeholder="تاریخ پرداخت" runat="server"  style="width:100%" ></asp:TextBox><br />

                   <asp:Image runat="server" id="capcha" src="JpegImage.aspx" style="width:100%" /><br />
                  <table width="100%"><tr><td><asp:label  ID="Lbl_entercap" runat="server" Text="عبارت امنیتی را وارد کنید" ></asp:label></td>
                      <td><asp:TextBox ID="txt_cap" runat="server" MaxLength="6" Width="100%"  ></asp:TextBox></td></tr></table>
                  <asp:Button ID="Btnsend" runat="server"  style="width:100%" Text="ارسال" OnClick="Btnsend_Click"/>
                  <asp:Label runat="server" ID="lbl_error" CssClass="errorLine" />
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
