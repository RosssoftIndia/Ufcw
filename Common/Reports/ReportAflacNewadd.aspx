<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportAflacNewadd.aspx.cs" Inherits="ReportAflacNewadd" MasterPageFile="~/Common/Common.master" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.12.1017, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>
<%@ Register src="~/Common/Submenu.ascx" tagname="Submenu" tagprefix="uc2" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
    
    <div class="bodycontainer">  
  <table class="submenu_wrapper" >
  <tr>
   <td class="pgHeading" >Report - Aflac New Add</td>
    <td style="float:right;"  ><uc2:Submenu ID="Submenu" runat="server" /></td>
  </tr>
  </table>  
    <div class="tabwrapper" >     
   <table class="full" style="margin-bottom:20px;margin-right :60px;">
           <tr><td>Shop</td></tr>
           <tr>
           <td><telerik:RadScriptManager ID="script" runat="server"></telerik:RadScriptManager>
           <telerik:RadComboBox ID="Drp_shop" Width="220px" runat="server" 
        onselectedindexchanged="Drp_shop_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox></td>
            
           </tr>
            </table>
   </div> 
   
            <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr><td>
            <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1200px" Skin="Orange" SkinsPath="Skins"></telerik:ReportViewer>
           
</td>
</tr>
</table>
</div>
</div>
</div> 
    
</asp:Content> 

