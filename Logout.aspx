<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" MasterPageFile="~/Master.master" Inherits="Logout" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">       
  <div id="content">
<div id="container">
<div class="logobar" >
<img src="images/logo_login.png" />
</div>
 
<label for="name">You have Logged Out Successfully!</label> 

  
<div id="lower">
<asp:Button ID="btn_Login" CssClass="btnsubmit"  runat="server" Text="Login" PostBackUrl="~/Default.aspx" />
</div>
</div> 
</div> 

</asp:Content> 