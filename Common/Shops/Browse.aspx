<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse.aspx.cs" MasterPageFile="~/Common/Common.master"   Inherits="Common_Shops_Browse" %>

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
   <td class="pgHeading" >Browse Shops</td>
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
   <table class="full" style="margin-bottom:20px;margin-right :60px;">
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
        <telerik:RadComboBoxItem  Text="Shop ID" />
        <telerik:RadComboBoxItem  Text="Name" />
        <telerik:RadComboBoxItem  Text="Delegate" /> 
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
            </table>
   </div> 
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"  EnableEmbeddedSkins="true" 
         MultiPageID="RadMultiPage1" AutoPostBack="True" 
            SelectedIndex="1" CssClass="tabStrip" 
         ontabclick="RadTabStrip1_TabClick">
            <Tabs>
            <telerik:RadTab Text="Search Result" >
                </telerik:RadTab>
                <telerik:RadTab Text="Active">
                </telerik:RadTab>
                <telerik:RadTab Text="InActive" Selected="True">
                </telerik:RadTab>                          
            </Tabs>
        </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="1" 
         CssClass="multiPage">
               <telerik:RadPageView ID="RadPageView0" runat="server">  
                 <div class="tabwrapper" >
            <div class="tabContent" >            
            <table class="outter full">                     
            <tr>
            <td class="note" style="float:right" ><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Shop with <img class="note-icon" /> are having Flagged Notes</li>
             </ul>
             </td>
            </tr>                  
            <tr>
            <td>             
<telerik:RadGrid ID="BrowseGridSearch" runat="server" CellSpacing="0" OnSortCommand="BrowseGridSearch_SortCommand" 
GridLines="None" onneeddatasource="BrowseGridSearch_NeedDataSource"  AllowSorting="true" 
                    onitemdatabound="BrowseGridSearch_ItemDataBound" AllowPaging="True" 
                    PageSize="10" onpageindexchanged="BrowseGridSearch_PageIndexChanged" >                    
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
                    
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true" FileName="ActiveShops" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<GroupingSettings CaseSensitive="false" /> 
<MasterTableView AutoGenerateColumns="False" DataKeyNames="RecordID" ClientDataKeyNames="RecordID" CommandItemDisplay="Top"  CommandItemSettings-ShowAddNewRecordButton="False" >
<CommandItemSettings ShowExportToExcelButton="true" ></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="RecordID" DataType="System.Int32" 
FilterControlAltText="Filter RecordID column" HeaderText="RecordID" 
ReadOnly="True" SortExpression="RecordID" UniqueName="RecordID" Visible="false" >
</telerik:GridBoundColumn>  
<%--<telerik:GridTemplateColumn HeaderText="RecordID" Visible="false">
<ItemTemplate>
<asp:Label ID="lblUnique" runat="server" Text='<%# Eval("RecordID", "{0}") %>'></asp:Label>
</ItemTemplate>
</telerik:GridTemplateColumn>--%>
<%--<telerik:GridBoundColumn DataField="ShopID" 
FilterControlAltText="Filter ShopID column" HeaderText="Shop ID" 
SortExpression="ShopID" UniqueName="ShopID">
</telerik:GridBoundColumn>     --%>
<telerik:GridTemplateColumn DataField="ShopID" HeaderText="Shop ID" FilterControlWidth="80px" ShowFilterIcon="false" AutoPostBackOnFilter="true"
 UniqueName="ShopID" SortExpression="ShopID" FilterControlAltText="Filter ShopID column">
 <ItemTemplate>
 <%--<a href="#" onclick="ShowEditForm();" ><asp:Label ID="lbllink" runat="server" Text='<%# Eval("ShopID", "{0}") %>'></asp:Label></a>--%>
     <asp:HyperLink id="link" runat="server" Text='<%# Eval("ShopID", "{0}") %>' ></asp:HyperLink>
     
</ItemTemplate>
  </telerik:GridTemplateColumn>
<telerik:GridBoundColumn DataField="Name" 
FilterControlAltText="Filter Name column" HeaderText="Name" 
SortExpression="Name" UniqueName="Name">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="Delegate" AllowSorting="false"
FilterControlAltText="Filter Delegate column" HeaderText="Delegate" 
SortExpression="Delegate" UniqueName="Delegate">
</telerik:GridBoundColumn>      
<telerik:GridBoundColumn DataField="LPD" AllowFiltering="false" AllowSorting="false"
FilterControlAltText="Filter LPD column" HeaderText="Last Paid Dues" 
SortExpression="LPD" UniqueName="LPD">
</telerik:GridBoundColumn>   

 <%--<telerik:GridTemplateColumn DataField="LPH" 
FilterControlAltText="Filter LPH column" HeaderText="Last Paid Health" 
SortExpression="LPH" UniqueName="LPH">
          <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text= '<%# Eval("LPH", "{0:MM/yyyy}") %>'></asp:Label>
   </ItemTemplate></telerik:GridTemplateColumn>--%>
<telerik:GridBoundColumn DataField="LPH" DataFormatString="{0:MM/yyyy}" AllowSorting="false"
FilterControlAltText="Filter LPH column" HeaderText="Last Paid Health" 
SortExpression="LPH" UniqueName="LPH">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="OpenMonths"  AllowFiltering="false" AllowSorting="false"
FilterControlAltText="Filter OpenMonths column" HeaderText="Open Months" 
SortExpression="OpenMonths" UniqueName="OpenMonths">
</telerik:GridBoundColumn>     
<telerik:GridBoundColumn DataField="Contract_End" DataType="System.DateTime" AllowSorting="false"
FilterControlAltText="Filter Contract_End column" HeaderText="Contract End" 
SortExpression="Contract_End" UniqueName="Contract_End">
</telerik:GridBoundColumn>    
<telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" >
<HeaderStyle Width="130px" HorizontalAlign="Right"  />
<ItemStyle Width="130px"  HorizontalAlign="Right"  />
<ItemTemplate> 
<%--<table style="width:128px!important; height:10px;">
<tr style="height:10px">--%>
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" />
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Shops/View.aspx?id="+DataBinder.Eval(Container.DataItem,"RecordID")%>' class="view-icon"   runat="server"></asp:HyperLink>

<%--</tr>
</table>--%>
</ItemTemplate> 
</telerik:GridTemplateColumn>     
</Columns>
<SortExpressions>
<telerik:GridSortExpression FieldName="Name"></telerik:GridSortExpression>
</SortExpressions>
</MasterTableView>
</telerik:RadGrid>
                  </td>
                  </tr>
                  </table>  
            </div> 
            </div>
               </telerik:RadPageView> 
            <telerik:RadPageView ID="RadPageView1" runat="server">  
            <div class="tabwrapper" >
            <div class="tabContent" >            
            <table class="outter full">                     
            <tr>
            <td class="note" style="float:right" ><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Shop with <img class="note-icon" /> are having Flagged Notes</li>
             </ul>
             </td>
            </tr>                  
            <tr>
            <td>             
<telerik:RadGrid ID="BrowseGridActive" runat="server" CellSpacing="0" OnSortCommand="BrowseGridActive_SortCommand" 
GridLines="None" onneeddatasource="BrowseGridActive_NeedDataSource" 
ondeletecommand="BrowseGridActive_DeleteCommand" AllowSorting="true" OnItemCreated="BrowseGridActive_ItemCreated"
                    onitemdatabound="BrowseGridActive_ItemDataBound" AllowPaging="True" 
                    PageSize="10" onpageindexchanged="BrowseGridActive_PageIndexChanged" >                    
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
                    
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true" FileName="ActiveShops" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<GroupingSettings CaseSensitive="false" /> 
<MasterTableView AutoGenerateColumns="False" DataKeyNames="RecordID" ClientDataKeyNames="RecordID" CommandItemDisplay="Top"  CommandItemSettings-ShowAddNewRecordButton="False" >
<CommandItemSettings ShowExportToExcelButton="true" ></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="RecordID" DataType="System.Int32" 
FilterControlAltText="Filter RecordID column" HeaderText="RecordID" 
ReadOnly="True" SortExpression="RecordID" UniqueName="RecordID" Visible="false" >
</telerik:GridBoundColumn>  
<%--<telerik:GridTemplateColumn HeaderText="RecordID" Visible="false">
<ItemTemplate>
<asp:Label ID="lblUnique" runat="server" Text='<%# Eval("RecordID", "{0}") %>'></asp:Label>
</ItemTemplate>
</telerik:GridTemplateColumn>--%>
<%--<telerik:GridBoundColumn DataField="ShopID" 
FilterControlAltText="Filter ShopID column" HeaderText="Shop ID" 
SortExpression="ShopID" UniqueName="ShopID">
</telerik:GridBoundColumn>     --%>
<telerik:GridTemplateColumn DataField="ShopID" HeaderText="Shop ID" FilterControlWidth="80px" ShowFilterIcon="false" AutoPostBackOnFilter="true"
 UniqueName="ShopID" SortExpression="ShopID" FilterControlAltText="Filter ShopID column">
 <ItemTemplate>
 <%--<a href="#" onclick="ShowEditForm();" ><asp:Label ID="lbllink" runat="server" Text='<%# Eval("ShopID", "{0}") %>'></asp:Label></a>--%>
     <asp:HyperLink id="link" runat="server" Text='<%# Eval("ShopID", "{0}") %>' ></asp:HyperLink>
     
</ItemTemplate>
  </telerik:GridTemplateColumn>
<telerik:GridBoundColumn DataField="Name" 
FilterControlAltText="Filter Name column" HeaderText="Name" 
SortExpression="Name" UniqueName="Name">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="Delegate" AllowSorting="false"
FilterControlAltText="Filter Delegate column" HeaderText="Delegate" 
SortExpression="Delegate" UniqueName="Delegate">
</telerik:GridBoundColumn>      
<telerik:GridBoundColumn DataField="LPD" AllowFiltering="false" AllowSorting="false"
FilterControlAltText="Filter LPD column" HeaderText="Last Paid Dues" 
SortExpression="LPD" UniqueName="LPD">
</telerik:GridBoundColumn>   

 <%--<telerik:GridTemplateColumn DataField="LPH" 
FilterControlAltText="Filter LPH column" HeaderText="Last Paid Health" 
SortExpression="LPH" UniqueName="LPH">
          <ItemTemplate>
               <asp:Label ID="Label1" runat="server" Text= '<%# Eval("LPH", "{0:MM/yyyy}") %>'></asp:Label>
   </ItemTemplate></telerik:GridTemplateColumn>--%>
<telerik:GridBoundColumn DataField="LPH" DataFormatString="{0:MM/yyyy}" AllowSorting="false"
FilterControlAltText="Filter LPH column" HeaderText="Last Paid Health" 
SortExpression="LPH" UniqueName="LPH">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="OpenMonths"  AllowFiltering="false" AllowSorting="false"
FilterControlAltText="Filter OpenMonths column" HeaderText="Open Months" 
SortExpression="OpenMonths" UniqueName="OpenMonths">
</telerik:GridBoundColumn>     
<telerik:GridBoundColumn DataField="Contract_End" DataType="System.DateTime" AllowSorting="false"
FilterControlAltText="Filter Contract_End column" HeaderText="Contract End" 
SortExpression="Contract_End" UniqueName="Contract_End">
</telerik:GridBoundColumn>    
<telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" >
<HeaderStyle Width="130px" HorizontalAlign="Right"  />
<ItemStyle Width="130px"  HorizontalAlign="Right"  />
<ItemTemplate> 
<%--<table style="width:128px!important; height:10px;">
<tr style="height:10px">--%>
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" />
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Shops/View.aspx?id="+DataBinder.Eval(Container.DataItem,"RecordID")%>' class="view-icon"   runat="server"></asp:HyperLink>
<asp:HyperLink ID="lnkedit" NavigateUrl='<%#"~/Common/Shops/Edit.aspx?id="+DataBinder.Eval(Container.DataItem,"RecordID")%>' class="edit-icon"   runat="server"></asp:HyperLink>
<asp:LinkButton ID="btndelete"  runat="server" class="del-icon" CommandName="Delete" OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?\\n" + Eval("ShopID")+ "\");"%>' > </asp:LinkButton>
<%--</tr>
</table>--%>
</ItemTemplate> 
</telerik:GridTemplateColumn>     
</Columns>

<%--<CommandItemTemplate>
    <div style="display:inline-block; width:100%;">
        <div style="float:left;">
            <!--my custom button-->
     <asp:Button ID="btnpager" runat="server" Text="Show All:Off" onclick="btnpager_Click" />
    <!--end custom button-->
    </div>
    <div style="float:right;">
        <!--add default buttons back-->
    Export: 
    <asp:Button ID="ExportToExcelButton" runat="server" CommandName="ExportToExcel" CssClass="rgExpXLS"/>
      
    <asp:Button ID="ExportToWordButton" runat="server" CommandName="ExportToWord" CssClass="rgExpDOC" />
      
    <asp:Button ID="ExportToPdfButton" runat="server" CommandName="ExportToPdf" CssClass="rgExpPDF" />
      
    <asp:Button ID="ExportToCsvButton" runat="server" CommandName="ExportToCsv" CssClass="rgExpCSV" />
        <!--end of add-->
    </div>
    </div>
</CommandItemTemplate>--%>
<SortExpressions>
<telerik:GridSortExpression FieldName="Name"></telerik:GridSortExpression>
</SortExpressions>
</MasterTableView>
</telerik:RadGrid>
                  </td>
                  </tr>
                  </table>  
            </div> 
            </div>
           </telerik:RadPageView> 
            <telerik:RadPageView ID="RadPageView2" runat="server"> 
                <div class="tabwrapper" >
            <div class="tabContent" >            
            <table class="outter full">             
            <tr>
            <td class="note" style="float:right" ><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Shop with <img class="note-icon" /> are having Flagged Notes</li>
             </ul>
             </td>
            </tr>             
            <tr>
            <td>
<telerik:RadGrid ID="BrowseGridInActive" runat="server" CellSpacing="0" 
GridLines="None" onneeddatasource="BrowseGridInActive_NeedDataSource" AllowSorting="true" OnSortCommand="BrowseGridInActive_SortCommand"
                    onitemdatabound="BrowseGridInActive_ItemDataBound"  AllowPaging="True" OnItemCreated="BrowseGridInActive_ItemCreated"
                    PageSize="10" onpageindexchanged="BrowseGridInActive_PageIndexChanged">
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
                     <GroupingSettings CaseSensitive="false" /> 
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true" FileName="InActiveShops" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" DataKeyNames="RecordID" CommandItemDisplay="Top"  CommandItemSettings-ShowAddNewRecordButton="False" >
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="RecordID" DataType="System.Int32" 
FilterControlAltText="Filter RecordID column" HeaderText="RecordID" 
ReadOnly="True" SortExpression="RecordID" UniqueName="RecordID" Visible="false" >
</telerik:GridBoundColumn>  
<%--<telerik:GridBoundColumn DataField="ShopID" 
FilterControlAltText="Filter ShopID column" HeaderText="Shop ID" 
SortExpression="ShopID" UniqueName="ShopID">
</telerik:GridBoundColumn> --%>    
<telerik:GridTemplateColumn DataField="ShopID" HeaderText="Shop ID" FilterControlWidth="80px" ShowFilterIcon="false" AutoPostBackOnFilter="true"
 UniqueName="ShopID" SortExpression="ShopID" FilterControlAltText="Filter ShopID column">
 <ItemTemplate>
     <asp:HyperLink id="link" runat="server" Text='<%# Eval("ShopID", "{0}") %>' ></asp:HyperLink>
</ItemTemplate>
  </telerik:GridTemplateColumn>
<telerik:GridBoundColumn DataField="Name" 
FilterControlAltText="Filter Name column" HeaderText="Name" 
SortExpression="Name" UniqueName="Name">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="Delegate" AllowSorting="false"
FilterControlAltText="Filter Delegate column" HeaderText="Delegate" 
SortExpression="Delegate" UniqueName="Delegate">
</telerik:GridBoundColumn>      
<telerik:GridBoundColumn DataField="LPD" DataType="System.DateTime"   AllowSorting="false"
FilterControlAltText="Filter LPD column" HeaderText="Last Paid Dues" 
SortExpression="LPD" UniqueName="LPD">
</telerik:GridBoundColumn>   
<telerik:GridBoundColumn DataField="LPH" DataType="System.DateTime"  AllowSorting="false"
FilterControlAltText="Filter LPH column" HeaderText="Last Paid Health" 
SortExpression="LPH" UniqueName="LPH">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="OpenMonths"  AllowFiltering="false" AllowSorting="false"
FilterControlAltText="Filter OpenMonths column" HeaderText="Open Months" 
SortExpression="OpenMonths" UniqueName="OpenMonths">
</telerik:GridBoundColumn>     
<telerik:GridBoundColumn DataField="Contract_End" DataType="System.DateTime"  AllowSorting="false"
FilterControlAltText="Filter Contract_End column" HeaderText="Contract End" 
SortExpression="Contract_End" UniqueName="Contract_End">
</telerik:GridBoundColumn> 
<telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" >
<HeaderStyle Width="100px" HorizontalAlign="Right" />
<ItemStyle Width="100px" HorizontalAlign="Right" />
<ItemTemplate>  
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" />
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Shops/View.aspx?id="+DataBinder.Eval(Container.DataItem,"RecordID")%>' class="view-icon"   runat="server"></asp:HyperLink>
</ItemTemplate> 
</telerik:GridTemplateColumn> 

</Columns>
<SortExpressions>
<telerik:GridSortExpression FieldName="Name"></telerik:GridSortExpression>  </SortExpressions>   
</MasterTableView>
</telerik:RadGrid>
                  </td>
                  </tr>
                  </table>  
            </div>
            </div> 
            </telerik:RadPageView> 
           </telerik:RadMultiPage> 
</div> 
 <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="UserListDialog" runat="server" Title="Document" Width="850px" Height="650px" Left="150px" ReloadOnShow="true" ShowContentDuringLoad="false"
                Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
 <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowActiveForm(id, rowIndex) {
                var grid = $find("<%= BrowseGridActive.ClientID %>");

                var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
                grid.get_masterTableView().selectItem(rowControl, true);

                window.radopen("Document.aspx?Rid=" + id, "UserListDialog");
                return false;
            }
            
            function ShowInActiveForm(id, rowIndex) {
                var grid = $find("<%= BrowseGridInActive.ClientID %>");

                var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
                grid.get_masterTableView().selectItem(rowControl, true);

                window.radopen("Document.aspx?Rid=" + id, "UserListDialog");
                return false;
            }
            
        </script>
    </telerik:RadCodeBlock>
</asp:Content> 
