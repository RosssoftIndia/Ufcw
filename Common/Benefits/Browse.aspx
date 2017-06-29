<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse.aspx.cs" Inherits="Common_Benefits_Browse" MasterPageFile="~/Common/Common.master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>
<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 

<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
</telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
</Scripts>
    </telerik:RadScriptManager>  
    
     <div class="bodycontainer">  
  <table class="submenu_wrapper" >
  <tr>
   <td class="pgHeading" >Browse Benefits</td>
  <td>
    <div class="buttons">   
  <asp:LinkButton ID="btnAdd" runat="server" onclick="Pageaction_Click"> 
       <asp:Image ID="addimg" runat="server" ImageUrl="~/images/Add.ico"  /> 
     <b>Add</b>        
        </asp:LinkButton>
</div>     
  </td>
  </tr>
  </table>  
    <div class="tabwrapper" >     
<%--   <table class="full" style="margin-bottom:20px;margin-right :60px;">
            <tr>
            <td width="50%">
                <asp:Panel ID="searchpanel"  runat="server"  DefaultButton="btnsearch">
            
            <table>
            <tr><td>Column</td><td>Filter</td><td></td></tr>
            <tr>
            <td>
                <telerik:RadComboBox ID="RadColumn" runat="server" onselectedindexchanged="SearchFilter_SelectedIndexChanged" AutoPostBack="true" >
           <Items>              
        <telerik:RadComboBoxItem  Text="Select" />
        <telerik:RadComboBoxItem  Text="Member ID" />
        <telerik:RadComboBoxItem  Text="First Name" />
         <telerik:RadComboBoxItem  Text="Last Name" />
        <telerik:RadComboBoxItem  Text="Shop" /> 
        </Items> 
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox ID="RadFilter" runat="server" 
                    onselectedindexchanged="SearchFilter_SelectedIndexChanged">
           <Items>                    
        <telerik:RadComboBoxItem  Text="Contains" />
        <telerik:RadComboBoxItem  Text="Starts With" />
        <telerik:RadComboBoxItem  Text="Ends With" /> 
        </Items> 
                </telerik:RadComboBox>
            </td>        
            <td>
             <telerik:RadNumericTextBox ID="txtshopid"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Visible="false"></telerik:RadNumericTextBox>
             <asp:TextBox ID="txtsearchtag"  Width="160px" height="15px" runat="server" Visible="false"></asp:TextBox>              
            </td>
            <td>
                <asp:LinkButton ID="btnsearch" CssClass="search-icon"  runat="server" 
                    onclick="btnsearch_Click"></asp:LinkButton>
               <asp:LinkButton ID="btnclear" CssClass="refresh-icon"  runat="server" 
                    onclick="btnclear_Click"></asp:LinkButton>
            </td>
            </tr>
            </table>
                </asp:Panel>
            </td>
            <td>
            <table>
            <tr>
            <td>
            <table>
            <tr>
            <td>Calendar&nbsp;&nbsp;</td>
            <td>
            <asp:RadioButtonList ID="Rbtncal" runat="server" CssClass="removestyle" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="True">Include</asp:ListItem>   
            <asp:ListItem Value="False">Exclude</asp:ListItem>   
            </asp:RadioButtonList>
            </td></tr></table>
            </td>
            </tr>
            <tr>
            <td>
            <table>
               <tr>
            <td><div  class="cal" ><asp:Label ID="startdate" runat="server"></asp:Label> - <asp:Label ID="enddate" runat="server"></asp:Label></div></td>
            <td>
            <telerik:RadSplitter ID="radspliter" runat="server" Height="20" Width="235px" Orientation="Horizontal" LiveResize="true" BorderSize="0">
        <telerik:RadPane ID="Radpane3" runat="server" Scrolling="none" >
            <telerik:RadSlidingZone ID="Radslidingzone3" runat="server" SlideDirection="Bottom" ClickToOpen="false">               
                 <telerik:RadSlidingPane ID="Radslidingpane9" IconUrl="~/images/cal.png" CssClass="calreset" runat="server" Height="211" EnableDock="false">
                  <telerik:RadCalendar ID="RadCalendar" runat="server"  AutoPostBack="true"  Width="231px" 
                    ShowRowHeaders="false"  UseColumnHeadersAsSelectors="false" onselectionchanged="RadCalendar_SelectionChanged"  ondayrender="RadCalendar_DayRender"  >
    <ClientEvents OnLoad="AddEvent" />
    </telerik:RadCalendar>                
                    </telerik:RadSlidingPane>               
            </telerik:RadSlidingZone>
            
        </telerik:RadPane>
            
    </telerik:RadSplitter>
            </td>
            </tr>
            </table>
            </td>
            </tr>
         
            </table>  
            </td>
            <td>
            <table class="full" >
             <tr>
            <td><br /></td>
            </tr>
            <tr>
            <td>
             <span style="float:right;"> <asp:Button ID="btnpager" runat="server" 
                    Text="Show All:Off" onclick="btnpager_Click" />  </span>  
            </td>
            </tr>
            </table>
                           
            </td>
            </tr>
            </table>--%>
   </div> 
   
         
          
            <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr><td>
            <telerik:RadGrid ID="BrowseBenefits" runat="server" CellSpacing="0" 
GridLines="None"  AllowPaging="True" PageSize="10" 
                    onneeddatasource="BrowseBenefits_NeedDataSource"  ondeletecommand="BrowseBenefits_DeleteCommand"              >
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true"  FileName="ActiveMembers" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" DataKeyNames="RecordID" TableLayout="Fixed" CommandItemDisplay="Top" EditMode="InPlace"  CommandItemSettings-ShowAddNewRecordButton="False">
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="RecordID" DataType="System.Int32" 
FilterControlAltText="Filter RecordID column" HeaderText="RecordID" 
ReadOnly="True" SortExpression="RecordID" UniqueName="RecordID" Visible="false" >
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Name" ReadOnly="True"
FilterControlAltText="Filter Name column" HeaderText="Name" 
SortExpression="Name" UniqueName="Name" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="Description" ReadOnly="True"
FilterControlAltText="Filter Description column" HeaderText="Description" 
SortExpression="Description" UniqueName="Description">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="providedby" ReadOnly="True"
FilterControlAltText="Filter providedby column" HeaderText="Providedby" 
SortExpression="providedby" UniqueName="providedby">
</telerik:GridBoundColumn> 
<telerik:GridTemplateColumn HeaderText="Action" >
<HeaderStyle Width="100px" CssClass="text-center" />
<ItemStyle Width="100px" CssClass="text-center" />
<ItemTemplate>
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Benefits/View.aspx?Rid="+DataBinder.Eval(Container.DataItem,"RecordID") %>' class="view-icon"   runat="server"></asp:HyperLink>
<asp:HyperLink ID="lnkedit" NavigateUrl='<%#"~/Common/Benefits/Edit.aspx?Rid="+DataBinder.Eval(Container.DataItem,"RecordID") %>' class="edit-icon"   runat="server"></asp:HyperLink>
<asp:LinkButton ID="btndelete" Visible="false" runat="server" class="del-icon" CommandName="Delete" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?\\n" + Eval("Name")+ "\");"%>' > </asp:LinkButton>
</ItemTemplate>

</telerik:GridTemplateColumn> 
</Columns>
</MasterTableView>
</telerik:RadGrid>
</td>
</tr>
</table>
</div>
</div>

</div> 
</asp:Content>