<%@ Register TagPrefix="uc" TagName="CustomerSidebar" Src="~/UDC/CustomerSidebar.ascx" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customercharge.aspx.cs" Inherits="narsShop.pages.customercharge" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Dashboard Section Start -->
    <div class="dashboard-section section-padding fix">
        <div class="container">
            <div class="row">
            <uc:CustomerSidebar ID="CustomerSidebar1" runat="server" />
                <div class="col-xl-9">
                 <div class="order-history2">
                              <h3> <asp:Label ID="lbl_title" runat="server" /></h3>
                        <h5>
                            <asp:Label ID="lbl_subtit" runat="server" Text="انتخاب شیوه پرداخت:" /></h5>
                            <!-- Wishlist Section Start -->
                            <div class="wishlist-wrapper fix bg-white">
                                <div class="container">
                                    <!-- Faq Section S T A R T  -->
                                    <section class="faq-section fix section-padding">
                                        <div class="container">
                                            <div class="faq-wrapper">
                                                <div class="faq-content style-2">
                                                    <div class="faq-accordion mt-4 mt-md-0">
                                                        <div class="accordion" id="accordion">
                                                            <div class="accordion-item mb-3 wow fadeInUp" data-wow-delay=".3s">
                                                                <h5 class="accordion-header">
                                                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                                                        data-bs-target="#faq3" aria-expanded="true" aria-controls="faq2">
                                                                        کارت به کارت
                                                                    </button>
                                                                </h5>
                                                                <div id="faq3" class="accordion-collapse  collapse" data-bs-parent="#accordion">
                                                                    <div class="accordion-body">
                                                                        <p  >  6037691632092243   </p>
    <p  >محمد امین سربازی </p>
 

           

<asp:Label runat="server" ID="lbl_notif" CssClass="label label-danger" />


                           <p>لطفا جهت تسریع و تسهیل فرایند حساب، از طریق درگاه بانکی پرداخت خود را انجام دهید. 
                               در صورت بروز هر نوع مشکل و لزوم پرداخت بصورت کارت به کارت لطفا
     پس از هماهنگی با مدیر فروش و انجام کارت به کارت بخش زیر را پر نمایید</p>
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
             <td><%--<uc1:DateInput runat="server" ID="txtdate" MyStyle="width:100%" MyClass="number form-control" />--%></td>
         </tr>
                                        </table>
                            

     <asp:Button ID="Btnsend" runat="server"  Text="ارسال" OnClick="Btnsend_Click" CssClass="theme-btn style6"/>
     <asp:Label runat="server" ID="lbl_error" CssClass="errorLine" />
                                       
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="accordion-item mb-3 wow fadeInUp" data-wow-delay=".5s">
                                                                <h5 class="accordion-header">
                                                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                                                        data-bs-target="#faq1" aria-expanded="true" aria-controls="faq1">
                                                                        درگاه بانکی
                                                                    </button>
                                                                </h5>
                                                                <div id="faq1" class="accordion-collapse collapse" data-bs-parent="#accordion">
                                                                    <div class="accordion-body">


                                                                        <div id="panel-3">
                                                                            <div class="pt-3">
                                                                                <asp:RadioButtonList runat="server" ID="rb_dargah">
                                                                                    <asp:ListItem Text="صادرات بدون کارمزد" Value="1" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="زرین پال کارمزد حداکثر 10 هزار تومان" Value="0" Enabled="false"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                                <label for="txt_mablagh">مبلغ</label>
                                                                                <asp:TextBox runat="server" ID="txt_mablagh" CssClass="3dnumber form-control" ClientIDMode="Static"></asp:TextBox><br />
                                                                                <asp:Button runat="server" ID="linkdargah" CssClass="theme-btn style6" OnClick="linkdargah_Click" Text="انتقال به صفحه پرداخت"></asp:Button>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="accordion-item mb-3 wow fadeInUp" data-wow-delay=".7s">
                                                            <h5 class="accordion-header">
                                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                                                    data-bs-target="#faq2" aria-expanded="true" aria-controls="faq2">
                                                                    پرداخت از کیف پول
                                                                </button>
                                                            </h5>
                                                            <div id="faq2" class="accordion-collapse collapse " data-bs-parent="#accordion">
                                                                <div class="accordion-body">
                                                                    <table width="100%"  >
                                                                        <tr>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="txt_pr" CssClass="3dnumber form-control" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>  <asp:Button ID="Btn_fkif" runat="server" Text="پرداخت از موجودی کیف پول مشتری" OnClick="Btn_fkif_Click" CssClass="theme-btn style6" /> 
</td>
                                                                        </tr>
                                                                    </table>

                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </div>

                                <!-- Faq Section E N D  -->
                            </div>
                                       </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
