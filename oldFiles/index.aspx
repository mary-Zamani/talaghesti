<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="narsShop.index"  MasterPageFile="anj.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="anj.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="container">
      <div class="inner-header">
          <div class="row" style="align-items:center">
            <div class="col-lg-7 col-md-7 centered">
                 <div class="input-group">    
                <h4>جستجو بر اساس مبلغ</h4>
                     </div>
                </div>
              
                <div class="col-lg-7 col-md-7 centered">
                  <div class="advanced-search">

                      <div class="input-group">    
                          <table width="100%">
                              <tr>
                                  <td><asp:TextBox runat="server" id="txt_prepay" CssClass="3dnumber" placeholder="مبلغ پیش پرداخت به ریال" /></td>
                                  <td><asp:Button CssClass="btn" BackColor="#e7ab3c" ForeColor="#000000" runat="server" ID="dosearch" Text="بیاب" OnClick="dosearch_Click" /></td>                                 
                              </tr>
                            </table>
                       </div>
                                        </div>
              </div>
             


            
          </div>
      </div>
  </div>
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
<h2>جدید ترین طلاهای ویترین</h2>
              </div>
<asp:Label runat="server" ID="lbl_productrow1" />
    </div>
          
      </div>
  </div>
</section>

<section class="women-banner spad">
  <div class="container-fluid">
      <div class="row">       
          <div class="col-lg-12 offset-lg-1">
              <div class="filter-control">
<h2>جدید ترین طلاهای ویترین</h2>
              </div>
<asp:Label runat="server" ID="lbl_productrow2" />
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
                          <img src="img/icon-1.png" alt="" />
                      </div>
                      <div class="sb-text">
                          <h6>ردگیری ارسال</h6>
                          <p>For all order over 99<pre wp-pre-tag-1=""></pre>
                      </div>
                  </div>
              </div>
              <div class="col-lg-4">
                  <div class="single-benefit">
                      <div class="sb-icon">
                          <img src="img/icon-2.png" alt="" />
                      </div>
                      <div class="sb-text">
                          <h6>سفارش</h6>
                          <p>If good have prolems</p>
                      </div>
                  </div>
              </div>
              <div class="col-lg-4">
                  <div class="single-benefit">
                      <div class="sb-icon">
                          <img src="img/icon-1.png" alt="" />
                      </div>
                      <div class="sb-text">
                          <h6>پرداخت اقساط</h6>
                          <p>100% secure payment</p>
                      </div>
                  </div>
              </div>
          </div>
      </div>
  </div>
</section>





</asp:Content>
