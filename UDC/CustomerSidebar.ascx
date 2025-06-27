<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSidebar.ascx.cs" Inherits="narsShop.UDC.CustomerSidebar" %>
                 <div class="col-xl-3">
                    <div class="dashboard-navigation-sidebar">
                        <h3><asp:Label runat="server" ID="lbl_customername" /></h3>
                        <div>
                            <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist"
                                aria-orientation="vertical">

                                <table border="0">
                                    <tr>
                                        <td><i class="fa fa-pencil fa-fw "></i>کد مشتری</td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_customercode" /></td>
                                    </tr>
                                    <tr>
                                        <td><i class="fa fa-home fa-fw "></i>آدرس</td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_customeraddress" /></td>
                                    </tr>

                                    <tr>
                                        <td><i class="fa fa-phone fa-fw "></i>تلفن</td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_customerphone" /></td>
                                    </tr>
                                    <tr>
                                        <td><i class="fa fa-user fa-fw "></i>شماره ملی</td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_shmeli" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>

                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>

                                    <tr>
                                        <td><i class="fa fa-dollar fa-fw "></i>کیف پول</td>
                                        <td>
                                            <asp:Label ID="lbl_kif" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td><i class="fa fa-star fa-fw "></i>امتیاز</td>
                                        <td>
                                            <asp:Label ID="lbl_points" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td><i class="fa fa-expand fa-fw "></i>کل بدهی</td>
                                        <td>
                                            <asp:Label ID="lbl_totalbed" runat="server" /></td>
                                    </tr>
                                </table>

                                <button class="nav-link" id="v-pills-wishlist-tab" aria-selected="false">
                                    <i class="fa-light fa fa-shopping-cart"></i>اقساط من</button>

                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link" PostBackUrl="/pages/customercharge.aspx?type=W">
                                    <i class="fa-light fa fa-dollar"></i>شارژ کیف پول</asp:LinkButton>
                                
                                <asp:LinkButton ID="btnSoport" runat="server" CssClass="nav-link" PostBackUrl="/pages/customerticket.aspx">
                                    <i class="fa-light fa fa-comment"></i>پشتیبانی سایت </asp:LinkButton>


                                <asp:LinkButton ID="btnShop" runat="server" CssClass="nav-link" PostBackUrl="/index.aspx">
                                <i class="fa-light fa fa-shopping-cart"></i> فروشگاه 
                                 </asp:LinkButton>




                                <asp:LinkButton ID="btnExists" runat="server" CssClass="nav-link" OnClick="btnExist_Click">
                                 <i class="fa-solid fa-sign-out-alt"></i> خروج از حساب
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
