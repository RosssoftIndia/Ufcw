<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CensusHartford.aspx.cs" Inherits="Common_Reports_CensusHartford" MasterPageFile="~/Common/Common.master" %>

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
   <td class="pgHeading" >LIFE</td>
   <td style="float:right;"  ><uc2:Submenu ID="Submenu" runat="server" /></td>
  </tr>
  </table>  
    <div class="tabwrapper" >     
   <table class="full" style="margin-bottom:20px;margin-right :60px;">
           <tr><td width="150px">Shop</td><td width="150px">Date</td><td></td></tr>
           <tr>
           <td width="150px"><telerik:RadScriptManager ID="script" runat="server"></telerik:RadScriptManager>
        <telerik:RadComboBox ID="Drp_shop" Width="220px" runat="server" 
            ValidationGroup="tab2" AutoPostBack="true" 
            onselectedindexchanged="Drp_shop_SelectedIndexChanged" ></telerik:RadComboBox> </td>
            <td width="150px">            
            <telerik:RadMonthYearPicker runat="server" ID="Monthyear" Height="22px" ShowPopupOnFocus="true"
            AutoPostBack="true" onselecteddatechanged="Monthyear_SelectedDateChanged" MinDate="01/01/1900" ></telerik:RadMonthYearPicker>
            </td>
            <td><asp:Label ID="Monthstartlbl" runat="server" ></asp:Label> -
        <asp:Label ID="Monthendlbl" runat="server" ></asp:Label>  
        <%--<asp:Button ID="Button1" runat="server" Text="Apply Date Range" onclick="Button1_Click" />--%></td>
           </tr>
            </table>
   </div> 
   
            <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr><td>
            <telerik:ReportViewer ID="ReportViewer1" runat="server" Skin="Orange" 
            SkinsPath="~/Skins" Height="1200px" Width="100%" 
            ondatabinding="ReportViewer1_DataBinding">
    </telerik:ReportViewer>
           
</td>
</tr>
</table>
</div>
</div>
</div> 
    
</asp:Content> 