<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_fcv.aspx.cs" Inherits="narsShop._fcv" MasterPageFile="mst.Master" %>
<%@ MasterType VirtualPath="mst.Master" %> 

 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link type="text/css" rel="stylesheet" href="Styles/weblogin.css" />

    <section class="h-100 gradient-form" style="background-color: #eee;">
   <div class="container py-5 h-100">
    <div class="row justify-content-center align-items-center h-100">
       
        <div class="col-10">
        <div class="card rounded-3 text-black">
          <div class="row g-0">
            <div class="col-lg-6 col-sm-12">
              <div class="card-body p-md-5 mx-md-4">

                <div class="text-center">
                  <img src="img/logo.png" style="width: 185px;" alt="logo" />
                  <h5 class="mt-1 mb-5 pb-1"> طلا فروشی نشاط - طلا قسطی</h5>
                </div>

                
                    <div id="signin_pan">
                        <table class="table table-condensed table-active">
                            <thead>
                                <td colspan="2"><asp:Label ID="lbl_fcno" runat="server" />  </td>
                            </thead>

                            <tr><td>تاریخ</td><td><asp:Label ID="lbl_tarikh" runat="server" /></td></tr>
                                <tr><td>نام مشتری</td><td><asp:Label ID="lbl_name" runat="server" /></td></tr>
                                <tr><td>کالا</td><td><asp:Label ID="lbl_kala" runat="server" /></td></tr>
                                <tr><td>وزن</td><td><asp:Label ID="lbl_vazn" runat="server" /></td></tr>
                                <tr><td>وضعیت</td><td><asp:Label ID="lbl_stat" runat="server" /></td></tr>
                            
                        </table>
                    </div>
                    <div id="signin_pan2">
 <asp:Label ID="Label1" runat="server" />
                    </div>
 
               

              </div>
            </div>
  
          </div>
        
   
    </div>
      </div>
    </div>
 
    </div>
    
</section>
     </asp:Content>
