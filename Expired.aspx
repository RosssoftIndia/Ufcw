<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expired.aspx.cs" MasterPageFile="~/Master.master" Inherits="Expired" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">       
  <div id="content">
<div id="container">
<div class="logobar" >
<img src="images/logo_login.png" />
</div>
 
<label for="name">Session Expired!</label> 

  
<div id="lower">
<asp:Button ID="btn_Login" CssClass="btnsubmit"  runat="server" Text="Login" PostBackUrl="~/Default.aspx" />
</div>
</div> 
</div> 

</asp:Content> 