<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="productlist.aspx.cs" Inherits="narsShop.pages.productlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- ProductList -->
        <section id="sectionShop" runat="server" class="shop-section section-padding2 fix">
            <div class="container">
                <div class="row g-4">


                    <asp:Repeater ID="rptProducts" runat="server">
                        <ItemTemplate>
                            <div class="col-xl-3 col-md-6">
                                <div class="best-seller-product-items-two item-border">
                                    <div class="icon-box2">
                                        <button data-bs-toggle="modal" data-bs-target="#exampleModal2">
                                            <i class="fa-regular fa-eye"></i>
                                        </button>
                                    </div>

                                    <div class="best-seller-product-items-two__thumb">
                                        <a href='<%# Eval("DetailsUrl") %>'><img src='<%# Eval("ImageUrl") %>' alt="thumb"></a>
                                    </div>

                                    <div class="best-seller-product-items-two__content">
                                        <div class="best-seller-product-items-two__details">
                                            <p class="best-seller-product-items-two__details--subtitle">
                                                <a href='<%# Eval("DetailsUrl") %>'><%# Eval("Subtitle") %></a>
                                            </p>
                                            <h6 class="best-seller-product-items-two__details--title">
                                                <a href='<%# Eval("DetailsUrl") %>'><%# Eval("Title") %></a>
                                            </h6>

                                            <div class="best-seller-product-items-two__details--price">
                                                <a href='<%# Eval("DetailsUrl") %>'><span class="d-block">قیمت: <%# Eval("Price") %> تومان</span>
                                                <span class="d-block">پیش پرداخت: <%# Eval("PrePayment") %> تومان</span>
                                                <span class="d-block">وزن: <%# Eval("Weight") %></span></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <div class="pagination" id="customPagination" runat="server" visible="false">
                    <asp:Literal ID="litPagination" runat="server"></asp:Literal>
                </div>

            </div>
        </section>

       
    <%--  <div class="sub-file">

<asp:Label runat="server" ID="lbl_productrow1" ClientIDMode="Static" />

        </div>

<asp:Button ID="btn_add" runat="server" style="visibility:hidden"/>--%>
</asp:Content>
