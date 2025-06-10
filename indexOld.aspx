<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="indexOld.aspx.cs" Inherits="narsShop.indexOld"  MasterPageFile="mst.Master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ MasterType VirtualPath="mst.Master" %> 


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 
          <div class="col-lg-3 text-right col-md-3">
                  <asp:Label runat="server" ID="lbl1" />
              </div>

<section class="hero-section">
  <asp:label runat="server" ID="lbl_hero" />
</section>


<%--قسمت بالایی سایت --%>
  <asp:label runat="server" ID="lbl_banners" />
<%--قسمت پایینی سایت --%>
<asp:Label runat="server" ID="lbl_productrow1" />


    <div class="main-page2  ">

         <div id="1" class="slider-css">
              <h3>سرویس و نیم ست</h3>
               <section>
                   <table>
                      <tr><td><img src="./Image/servis.jpg" alt="servis-img"></td></tr>
                      <tr><td><a href="./pages/productlist.aspx?parent=10">سرویس</a></td></tr>
                   <table>
                       <tr><td><img src="./Image/nim-set.jpg" alt="nim-set-img"></td></tr>
                       <tr><td><a href="./pages/productlist.aspx?parent=10">نیم ست</a></td></tr>
                   </table>
               </section>
         </div>
         


 
         <div id="2" class="slider-css">
             <h3>گردنبند و پلاک</h3>
              <section>
                  <table>
                      <tr><td><img src="./Image/gardanband.jpg" alt="gardanband-img"></td></tr>
                       <tr><td><a href="./productlist.aspx?parent=14">گردنبند</a></td></tr>
                   </table>
                    <table>
                        <tr><td><img src="./Image/zanjir.jpg" alt="zanjir-img"></td></tr>
                        <tr><td><a href="./pages/productlist.aspx?parent=17">زنجیر</a></td></tr>
                    </table>
                   <table>
                         <tr><td><img src="./Image/ru-lebasi.jpg" alt="ru-lebasi-img"></td></tr>
                         <tr><td><a href="./pages/productlist.aspx?parent=36">رولباسی</a></td></tr>
                    </table>
                    <table>
                        <tr><td><img src="./Image/pelak.jpg" alt="pelak-img"></td></tr>
                       <tr><td><a href="http://localhost:1107/sub-file.html">پلاک</a></td></tr>
                  </table>
              </section>
          </div>  
          
          


        <div id="3" class="slider-css">
            <h3>انواع دستبند</h3>
            <section>
                <table>
                    <tr><td><img src="./Image/dastband.jpg" alt="dastband-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">دستبند</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/alango.jpg" alt="alango-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">النگو</a></td></tr>
                </table>
            </section>
        </div>




        <div id="4" class="slider-css">
            <h3>انگشتر و حلقه</h3>
            <section>
                <table>
                    <tr><td><img src="./Image/halghe-set.jpg" alt="halghe-set-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">حلقه ست</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/soliter.jpg" alt="soliter-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">حلقه سولیتر</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/ring.jpg" alt="ring-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">حلقه رینگی</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/angoshtar.jpg" alt="angoshtar-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">انگشتر</a></td></tr>
                </table>
            </section>
        </div>


        
        <div id="5" class="slider-css">
            <h3>اکسسوری های دیگر</h3>
            <section>
                  <table>
                    <tr><td><img src="./Image/aviz-saat.jpg" alt="aviz-saat-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">آویز ساعت</a></td></tr>
                 </table>
                <table>
                    <tr><td><img src="./Image/gushvare.jpg" alt="gushvare-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">گوشواره</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/pierce.jpg" alt="pierce-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">پیرسینگ</a></td></tr>
                </table>
                <table>
                    <tr><td><img src="./Image/paband.jpg" alt="paband-img"></td></tr>
                    <tr><td><a href="http://localhost:1107/sub-file.html">پابند</a></td></tr>
                </table>
            </section>
        </div>
    </div>
    




















<!--
<div class="container" >

          <div class="row justify-content-between">
              <div class="col-4">
                  <div class="row single-benefit">
                      <div class="sb-icon">
                          <img src="img/checkpoint.png" alt=""  style="max-width: 50px;"/>
                      </div>
                      <div class="sb-text">
                          <h6>تضمین 18 عیار</h6>
                          <p>برای تمام طلاهای تمام شعب<pre wp-pre-tag-1=""></pre>
                      </div>
                  </div>
              </div>
              <div class="col-4">
                  <div class="row single-benefit">
                      <div class="sb-icon">
                          <img src="img/checkpoint.png" alt=""  style="max-width: 50px;"/>
                      </div>
                      <div class="sb-text">
                          <h6>تضمین بازگشت وجه</h6>
                          <p>به مدت یک هفته از تاریخ فاکتور</p>
                      </div>
                  </div>
              </div>
              <div class="col-4">
                  <div class="row single-benefit">
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

-->


</asp:Content>
