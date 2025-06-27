<%@ Register TagPrefix="uc" TagName="CustomerSidebar" Src="~/UDC/CustomerSidebar.ascx" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customershop.aspx.cs" Inherits="narsShop.pages.customershop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Dashboard Section Start -->
    <div class="dashboard-section section-padding fix">
        <div class="container">
            <div class="row">
         
                      <uc:CustomerSidebar ID="CustomerSidebar1" runat="server" />

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
                        <div class="table_page table-responsive"  style="overflow-x: auto;">
                           <table class="table w-100" style="min-width: 600px;">
    <!-- Start Cart Table Head -->
    <thead>
        <tr>
            <th class="product_name text-start">محصول</th>
            <th class="product-price">شناسه</th>
            <th class="product_quantity">گرم</th>
            <th class="product_total">قیمت</th>
            <th class="product_remove">حذف</th>
        </tr>
    </thead>
    <!-- End Cart Table Head -->

    <!-- ---------- Repeater ---------- -->
    <asp:Repeater ID="rptCart" runat="server">
        <HeaderTemplate>
            <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td class="product_thumb">
                    <a href='<%# Eval("ImageUrl") %>' target="_blank">
                        <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>' />
                    </a>
                    <a class="product-name" href="#"><%# Eval("ProductName") %></a>
                </td>

                <td class="product-price">
                    <%# Eval("etiket") %>
                </td>

                <td class="product_quantity">
                    <div class="plus-minus-input">
                         <%# Eval("vazn") %>
                     
                    </div>
                </td>

                <td class="product_total">
                    <%# Eval("price", "{0:N0}") %>&nbsp;ریال
                </td>

                <td class="product_remove">
                  
                     <button class="btn btn-danger-outline" onclick="removefrombasket('<%# Eval("etiket") %>')" style="color: #cecece;"><i style="font-size:25px;color:red;" class="fa fa-trash"></i></button>
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </tbody>
        </FooterTemplate>
    </asp:Repeater>
    <!-- ---------- End Repeater ---------- -->
</table>
                        </div>
                    </div>
                 
                </div>
            </div>
            <div class="cart-checkout-wrapper">
                <div class="coupon_code right" data-aos="fade-up" data-aos-delay="400">
                   
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
                      
                        <div class="checkout-btn">
                           
                        <asp:Button runat="server" ID="finalizasale" OnClick="finalizasale_Click" Text="نهایی کردن فاکتور" CssClass="theme-btn style6" />

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
