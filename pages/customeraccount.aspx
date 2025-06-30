<%@ Register TagPrefix="uc" TagName="CustomerSidebar" Src="~/UDC/CustomerSidebar.ascx" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="customeraccount.aspx.cs" Inherits="narsShop.pages.customeraccount" %>

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
                                    
                                         اقساط من 
                                   
                                       <asp:Label runat="server" ID="lbl_customerdps" />
                               
                                </div>
                            </div>
                        </div>
                 
                    </div>
                 
                    
                </div>
            </div>
        </div>
    </div>
  
     

  



</asp:Content>
