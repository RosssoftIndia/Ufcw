<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/Master.master"  Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">       
<div id="content">
<div id="container">
<div class="logobar" >
<img src="images/logo_login.png" />
</div>
 
<label for="name">Username:<asp:RequiredFieldValidator ID="txt_UsernameValidator" runat="server" ControlToValidate="txt_Username"   ErrorMessage="UserName Required!">*</asp:RequiredFieldValidator></label>

<asp:TextBox ID="txt_Username" TabIndex="1"  runat="server"  AutoCompleteType="Disabled"></asp:TextBox>
<label for="username">Password:<asp:RequiredFieldValidator ID="txt_PasswordValidator" runat="server" ControlToValidate="txt_Password"  ErrorMessage="Password Required!">*</asp:RequiredFieldValidator></label>

<p><a href="#" tabindex="4" >Forgot your password?</a></p> 

<asp:TextBox ID="txt_Password" TabIndex="2" runat="server" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
<asp:Label ID="lblmsg" CssClass="msg"  runat="server" ></asp:Label>
<asp:ValidationSummary ID="ValidationSummary1" Font-Size="11px"  runat="server" /> 
  
<div id="lower">
<asp:Button ID="btn_Login" CssClass="btnsubmit" TabIndex="3"   runat="server" Text="Login" onclick="btn_Login_Click" />
</div>
</div> 
</div> 

</asp:Content> 