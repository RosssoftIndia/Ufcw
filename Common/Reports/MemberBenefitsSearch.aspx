<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberBenefitsSearch.aspx.cs" Inherits="Common_Reports_MemberBenefitsSearch" MasterPageFile="~/Common/Common.master" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.12.1017, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 

   
<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
     <div class="bodycontainer">  
  <table class="submenu_wrapper" >
  <tr>
   <td class="pgHeading" >Report - MemberBenefits</td>
  </tr>
  </table>  
    <div class="tabwrapper" >   
    <telerik:RadScriptManager ID="script" runat="server"></telerik:RadScriptManager>  
   <table class="full" style="margin-bottom:20px;margin-right :60px;">
           <tr><td style="width:200px">Mode</td><td style="padding-left:20px;" >
               <asp:RadioButtonList ID="Rd_btnMode" runat="server" CssClass="radio">
               <asp:ListItem Text="Any" Value="Any" ></asp:ListItem>
               <asp:ListItem Text="All Selected" Value="All"  ></asp:ListItem>
               </asp:RadioButtonList>
           </td></tr>
           <tr>
           <td >Benefits</td>
            <td>     
            <asp:CheckBoxList ID="chklst_Benefits" runat="server" CssClass="checkbox" CellSpacing="25"   RepeatColumns="5">
            
                </asp:CheckBoxList>
            </td>
           
           </tr>
           <tr>
           <td></td><td style="padding-left:20px;">
               <asp:Button ID="Search_btn" runat="server" Text="Search" 
                   onclick="Search_btn_Click" /></td>
           </tr>
            </table>
   </div> 
   
            <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr><td>
            <telerik:ReportViewer ID="ReportViewer1" runat="server" Skin="Orange" 
            SkinsPath="~/Skins" Height="1200px" Width="100%">
    </telerik:ReportViewer>
           
</td>
</tr>
</table>
</div>
</div>
</div> 
    
</asp:Content> 
