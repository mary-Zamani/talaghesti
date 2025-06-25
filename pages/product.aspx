<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="narsShop.pages.product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript" language="javascript">


      function rs_change(pishpardakht, id) {
          var mabkol = document.getElementById("kol_" + id).innerHTML;
          document.getElementById("pishp_" + id).innerText = Number(pishpardakht).toLocaleString();
          document.getElementById("ghest_" + id).innerText = (((mabkol - Number(pishpardakht)) * 1.25) / 5).toLocaleString();
          
          document.getElementById("hid_pishp").value = Number(pishpardakht);
          document.getElementById("hid_ghest").value = (((mabkol - pishpardakht) * 1.25) / 5);

      }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Shop Details Section S T A R T -->
    <div class="shop-details-section section-padding fix">
        <div class="shop-details bg-white">
            <div class="container">
               <h2><asp:Label runat="server" ID="Lbl_title" /> (<asp:label runat="server" ID="firstetiket" /> )</h2>
                <div class="row gx-60">
                    <div class="col-lg-6">
                         
                        <div class="product-big-img bg-color2"> 
                            
                           
                               <asp:Image runat="server" ID="productimage" />
                             
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="product-about">
                           
   <div class="product-details">
              <div   style="overflow:scroll">
              <div class="filter-control" style="text-align:center">

                  <table width="100%">
                      <tr>
                          <td>انتخاب سایز</td>
                          <td>انتخاب رنگ</td>
                          <td>تعداد</td>
                      </tr>
                      <tr>
                          <td><asp:DropDownList runat="server" ID="Drp_size"   CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                          <td><asp:DropDownList runat="server" ID="Drp_color"  CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                          <td><asp:DropDownList runat="server" ID="Drp_count"  CssClass="form-control dropdown dropdown-backdrop" AutoPostBack="true" OnSelectedIndexChanged="drp_size_SelectedIndexChanged"/></td>
                      </tr>

                  </table>
              </div>
<asp:Label runat="server" ID="lbl_product" />
    </div>
                          
                                
                            </div>
                            <div class="actions">
                        
                            </div> 
                            

                           <%-- <div class="product-details-footer">
                                <a class='theme-btn' href='/cart.html'>افزودن سبد خرید<i
                                        class="fa-regular fa-cart-shopping bg-transparent text-white"></i></a>
                                
                            </div>--%>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="product-description">
                            <h3>شرح محصول</h3>
                            <div class="desc">
                                <p> توضحیات
                                </p> <br>
                                
                            </div>
                        </div>
                     
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hid_pishp" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hid_ghest" runat="server" ClientIDMode="Static" />
  
</asp:Content>
