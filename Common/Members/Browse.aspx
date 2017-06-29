<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Browse.aspx.cs" Inherits="Common_Members_Browse" MasterPageFile="~/Common/Common.master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
  <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
                   function AddEvent(e) {
                       var calendar = $get("<%=RadCalendar.ClientID %>");
                      
                if (typeof (calendar.addEventListener) != "undefined") //gecko browsers
                {
                   
                    calendar.addEventListener("click", SelectWholeWeek, false);
                }
                else if (calendar.attachEvent) //IE
                {
                    calendar.attachEvent("onclick", SelectWholeWeek);
                }
               
            }
           
            
            function SelectWholeWeek(args) {

                var target = FindTarget(args); //find the clicked cell	  
                if (target.DayId && target.className)//if the clicked cell is a date cell it has a DayId
                {

                    if ((target.className == "radCalSelect_Default") || (target.className == "rcSelected")) {
                        var calendar = $find("<%= RadCalendar.ClientID %>");
             
                        calendar.unselectDates(calendar.get_selectedDates());

                        var row = target.parentNode;
                        while (row.tagName != "TR") {
                            row = row.parentNode;
                        }
                        var rowCells = row.getElementsByTagName("td"); //get all the cells of the row
                        for (var i = 0; i < rowCells.length; i++)
                            if (rowCells[i].DayId) //if the cell is a selectable Date cell it has DayId  
                        {
                            var date = GetDateFromID(rowCells[i].DayId, calendar.get_id());
                            calendar.selectDate(date);
                        }
                    }
                }
                
            }

            function FindTarget(e) {

                var target;
                if (e && e.target) //other browsers
                {
                    target = e.target;
                }
                else if (window.event && window.event.srcElement) //IE
                {
                    target = window.event.srcElement;
                }

                if (!target) {
                    return null;
                }

                while (target != null) {
                    if (target.tagName.toLowerCase() == 'td') {
                        break;
                    }
                    target = target.parentNode;
                }

                if (target.tagName.toLowerCase() != 'td') {
                    return null;
                }
                return target;
            }

            function GetDateFromID(ID, calendarId) {
                var name = ID.split(calendarId + "_")[1].split("_");
                var date = [parseInt(name[0]), parseInt(name[1]), parseInt(name[2])];
                return date;
            }
        </script>
    </telerik:RadCodeBlock>
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
   <td class="pgHeading" >Browse Members</td>
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
            </table>
   </div> 
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"  EnableEmbeddedSkins="true"
         MultiPageID="RadMultiPage1" AutoPostBack="true" 
            SelectedIndex="0" CssClass="tabStrip" 
         ontabclick="RadTabStrip1_TabClick">
            <Tabs>       
            <telerik:RadTab Text="Search Result" >
                </telerik:RadTab>         
                <telerik:RadTab Text="Active">
                </telerik:RadTab> 
                  <telerik:RadTab Text="InActive">
                </telerik:RadTab>                                       
            </Tabs>
        </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="multiPage"> 
        <telerik:RadPageView ID="RadPageView0" runat="server">           
          <div class="tabwrapper" >
            <div class="tabContent" >
            <table class="outter full">             
            <tr>
            <td class="note" style="float:right" > <span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Member with <img class="note-icon" /> are having Flagged Notes</li>
             </ul> 
             </td>
            </tr>          
            <tr><td>
            <telerik:RadGrid ID="BrowseGridSearch" runat="server" CellSpacing="0" AllowSorting="True" OnSortCommand="BrowseGridSearch_SortCommand"
GridLines="None" onneeddatasource="BrowseGridSearch_NeedDataSource" 
                    onitemdatabound="BrowseGridSearch_ItemDataBound"                   
                    onprerender="BrowseGridSearch_PreRender"  AllowPaging="True" PageSize="10" 
                    onpageindexchanged="BrowseGridSearch_PageIndexChanged" >
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true"  FileName="ActiveMembers" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" AllowCustomSorting="true" DataKeyNames="Mid" TableLayout="Fixed" CommandItemDisplay="Top" EditMode="InPlace"  CommandItemSettings-ShowAddNewRecordButton="False">
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<%--<telerik:GridTemplateColumn HeaderText="RecordID" Visible="false">
<ItemTemplate>
<asp:Label ID="lblUnique" runat="server" Text='<%# Eval("Mid", "{0}") %>'></asp:Label>
</ItemTemplate>
</telerik:GridTemplateColumn>--%>
<telerik:GridBoundColumn DataField="Mid" DataType="System.Int32" AllowSorting="false"
FilterControlAltText="Filter Mid column" HeaderText="Mid" 
ReadOnly="True" SortExpression="Mid" UniqueName="Mid" Visible="false" >
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="MemberID" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter MemberID column" HeaderText="Member ID" 
SortExpression="MemberID" UniqueName="MemberID" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="LastName" ReadOnly="True"
FilterControlAltText="Filter LastName column" HeaderText="Last Name" 
SortExpression="LastName" UniqueName="LastName">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="FirstName" ReadOnly="True"
FilterControlAltText="Filter FirstName column" HeaderText="First Name" 
SortExpression="FirstName" UniqueName="FirstName">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Initial" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter Initial column" HeaderText="Initial" 
SortExpression="Initial" UniqueName="Initial">
</telerik:GridBoundColumn>      
<telerik:GridBoundColumn DataField="SSN" ReadOnly="True" AllowSorting="false" DataFormatString="{0:###-##-####}"
FilterControlAltText="Filter SSN column" HeaderText="SSN" SortExpression="SSN" UniqueName="SSN" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="Shop" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter Shop column" HeaderText="Shop" 
SortExpression="Shop" UniqueName="Shop">
</telerik:GridBoundColumn>   
<telerik:GridBoundColumn DataField="HiredDate" DataType="System.DateTime" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter HiredDate column" HeaderText="Hired Date" 
SortExpression="HiredDate" UniqueName="HiredDate">
</telerik:GridBoundColumn>  
<telerik:GridTemplateColumn UniqueName="CreateDate"  >
<ItemTemplate><asp:Label ID="lblDate" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label></ItemTemplate>
<EditItemTemplate><telerik:RadDatePicker ID="Terminateddate" runat="server"  Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900"><Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar><DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton><DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput></telerik:RadDatePicker>
</EditItemTemplate>
</telerik:GridTemplateColumn>
<telerik:GridTemplateColumn HeaderText="Action" >
<HeaderStyle Width="100px" HorizontalAlign="Right"/>
<ItemStyle Width="100px" HorizontalAlign="Right" />
<ItemTemplate>
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" />
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Members/View.aspx?Rid="+DataBinder.Eval(Container.DataItem,"Rid")+"&Mid="+DataBinder.Eval(Container.DataItem,"Mid")%>' class="view-icon"   runat="server"></asp:HyperLink>
</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="btnEdit" CommandName="Update"   CssClass="ok-icon"  runat="server"></asp:LinkButton>
<asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
</EditItemTemplate>
</telerik:GridTemplateColumn> 
</Columns>
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
            <td class="note" style="float:right" > <span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Member with <img class="note-icon" /> are having Flagged Notes</li>
             </ul> 
             </td>
            </tr>          
            <tr><td>
            <telerik:RadGrid ID="BrowseGridActive" runat="server" CellSpacing="0" AllowSorting="True" OnSortCommand="BrowseGridActive_SortCommand"
GridLines="None" onneeddatasource="BrowseGridActive_NeedDataSource" 
                    onitemdatabound="BrowseGridActive_ItemDataBound" 
                      onupdatecommand="BrowseGridActive_UpdateCommand" 
                    onprerender="BrowseGridActive_PreRender"  AllowPaging="True" PageSize="10" 
                    onpageindexchanged="BrowseGridActive_PageIndexChanged" >
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true"  FileName="ActiveMembers" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" AllowCustomSorting="true" DataKeyNames="Mid" TableLayout="Fixed" CommandItemDisplay="Top" EditMode="InPlace"  CommandItemSettings-ShowAddNewRecordButton="False">
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<%--<telerik:GridTemplateColumn HeaderText="RecordID" Visible="false">
<ItemTemplate>
<asp:Label ID="lblUnique" runat="server" Text='<%# Eval("Mid", "{0}") %>'></asp:Label>
</ItemTemplate>
</telerik:GridTemplateColumn>--%>
<telerik:GridBoundColumn DataField="Mid" DataType="System.Int32" AllowSorting="false"
FilterControlAltText="Filter Mid column" HeaderText="Mid" 
ReadOnly="True" SortExpression="Mid" UniqueName="Mid" Visible="false" >
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="MemberID" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter MemberID column" HeaderText="Member ID" 
SortExpression="MemberID" UniqueName="MemberID" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="LastName" ReadOnly="True"
FilterControlAltText="Filter LastName column" HeaderText="Last Name" 
SortExpression="LastName" UniqueName="LastName">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="FirstName" ReadOnly="True"
FilterControlAltText="Filter FirstName column" HeaderText="First Name" 
SortExpression="FirstName" UniqueName="FirstName">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Initial" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter Initial column" HeaderText="Initial" 
SortExpression="Initial" UniqueName="Initial">
</telerik:GridBoundColumn>      
<telerik:GridBoundColumn DataField="SSN" ReadOnly="True" AllowSorting="false" DataFormatString="{0:###-##-####}"
FilterControlAltText="Filter SSN column" HeaderText="SSN" SortExpression="SSN" UniqueName="SSN" >
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="Shop" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter Shop column" HeaderText="Shop" 
SortExpression="Shop" UniqueName="Shop">
</telerik:GridBoundColumn>   
<telerik:GridBoundColumn DataField="HiredDate" DataType="System.DateTime" ReadOnly="True" AllowSorting="true" ShowSortIcon="true" 
FilterControlAltText="Filter HiredDate column" HeaderText="Hired Date" 
SortExpression="HiredDate" UniqueName="HiredDate">
</telerik:GridBoundColumn>  
<telerik:GridTemplateColumn UniqueName="CreateDate"  DataField="CreateDate" DataType="System.DateTime" SortExpression="CreateDate"  ShowSortIcon="true" AllowFiltering="false" >
<ItemTemplate><%#Eval("CreateDate") %></ItemTemplate>
<EditItemTemplate><telerik:RadDatePicker ID="Terminateddate" runat="server"  Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900"><Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar><DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton><DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput></telerik:RadDatePicker>
</EditItemTemplate>
</telerik:GridTemplateColumn>
<telerik:GridTemplateColumn HeaderText="Action" >
<HeaderStyle Width="100px" HorizontalAlign="Right"/>
<ItemStyle Width="100px" HorizontalAlign="Right" />
<ItemTemplate>
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" />
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Members/View.aspx?Rid="+DataBinder.Eval(Container.DataItem,"Rid")+"&Mid="+DataBinder.Eval(Container.DataItem,"Mid")%>' class="view-icon"   runat="server"></asp:HyperLink>
<asp:HyperLink ID="lnkedit" NavigateUrl='<%#"~/Common/Members/Edit.aspx?Rid="+DataBinder.Eval(Container.DataItem,"Rid")+"&Mid="+DataBinder.Eval(Container.DataItem,"Mid") %>' class="edit-icon"   runat="server"></asp:HyperLink>
<asp:LinkButton ID="btndelete"  runat="server" class="del-icon" CommandName="Edit"  > </asp:LinkButton>
</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="btnEdit" CommandName="Update"   CssClass="ok-icon"  runat="server"></asp:LinkButton>
<asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
</EditItemTemplate>
</telerik:GridTemplateColumn> 
</Columns>
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
             <li>Member with <img class="note-icon" /> are having Flagged Notes</li>
             </ul> 
             </td>
            </tr>               
            <tr>
            <td>
<telerik:RadGrid ID="BrowseGridInActive" runat="server" CellSpacing="0"  AllowSorting="True" OnSortCommand="BrowseGridInActive_SortCommand"
GridLines="None" onneeddatasource="BrowseGridInActive_NeedDataSource"   
                    onitemdatabound="BrowseGridInActive_ItemDataBound" AllowPaging="True" 
                    PageSize="10" onupdatecommand="BrowseGridInActive_UpdateCommand" 
                    onpageindexchanged="BrowseGridInActive_PageIndexChanged" >
                     <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom"  ></PagerStyle>
<ExportSettings HideStructureColumns="true" Excel-Format="ExcelML"  ExportOnlyData="true" IgnorePaging="true" FileName="InActiveMembers" ></ExportSettings>
<ClientSettings EnableRowHoverStyle="true"></ClientSettings> 
<MasterTableView AutoGenerateColumns="False" AllowCustomSorting="true" DataKeyNames="Mid" CommandItemDisplay="Top" EditMode="InPlace"  CommandItemSettings-ShowAddNewRecordButton="False">
<CommandItemSettings ShowExportToExcelButton="true"></CommandItemSettings>
<Columns>
<telerik:GridBoundColumn DataField="Mid" DataType="System.Int32" AllowSorting="false" 
FilterControlAltText="Filter Mid column" HeaderText="Mid" 
ReadOnly="True" SortExpression="Mid" UniqueName="Mid" Visible="false" >
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="MemberID" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter MemberID column" HeaderText="Member ID" 
SortExpression="MemberID" UniqueName="MemberID" >
</telerik:GridBoundColumn>    
<telerik:GridBoundColumn DataField="LastName"  ReadOnly="True" 
FilterControlAltText="Filter LastName column" HeaderText="Last Name" 
SortExpression="LastName" UniqueName="LastName">
</telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="FirstName"  ReadOnly="True"
FilterControlAltText="Filter FirstName column" HeaderText="First Name" 
SortExpression="FirstName" UniqueName="FirstName">
</telerik:GridBoundColumn>  
<telerik:GridBoundColumn DataField="Initial" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter Initial column" HeaderText="Initial" 
SortExpression="Initial" UniqueName="Initial">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="SSN"  ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter SSN column" HeaderText="SSN" 
SortExpression="SSN" UniqueName="SSN" DataFormatString="{0:###-##-####}">
</telerik:GridBoundColumn> 
<telerik:GridBoundColumn DataField="Shop"  AllowSorting="false"
FilterControlAltText="Filter Shop column" HeaderText="Shop" ReadOnly="True"
SortExpression="Shop" UniqueName="Shop">
</telerik:GridBoundColumn>  
 <telerik:GridTemplateColumn UniqueName="TerminatedDate" HeaderText="Terminated Date" >
<ItemTemplate><asp:Label ID="lblDate" runat="server" Text='<%#Eval("TerminatedDate") %>'></asp:Label></ItemTemplate>
<EditItemTemplate><telerik:RadDatePicker ID="Terminateddate" DbSelectedDate='<%# Bind("TerminatedDate") %>' runat="server"  Height="25px"    ShowPopupOnFocus="True" MinDate="1/1/1900"><Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar><DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton><DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput></telerik:RadDatePicker>
</EditItemTemplate>
</telerik:GridTemplateColumn>  
<telerik:GridBoundColumn DataField="CreateDate" DataType="System.DateTime" ReadOnly="True" AllowSorting="false"
FilterControlAltText="Filter CreateDate column" HeaderText="Create Date" 
SortExpression="CreateDate" UniqueName="CreateDate">
</telerik:GridBoundColumn>  
<telerik:GridTemplateColumn HeaderText="Action" >
<HeaderStyle Width="100px" HorizontalAlign="Right" />
<ItemStyle Width="100px" HorizontalAlign="Right" />
<ItemTemplate>  
<asp:Image ID="Img_Note" runat="server" CssClass="note-icon" /> 
<asp:HyperLink ID="lnkview" NavigateUrl='<%#"~/Common/Members/View.aspx?Rid="+DataBinder.Eval(Container.DataItem,"Rid")+"&Mid="+DataBinder.Eval(Container.DataItem,"Mid")%>' class="view-icon"   runat="server"></asp:HyperLink>
<asp:LinkButton ID="btndelete"  runat="server" class="edit-icon" CommandName="Edit"  > </asp:LinkButton>
</ItemTemplate>
<EditItemTemplate>
<asp:LinkButton ID="btnEdit" CommandName="Update"   CssClass="ok-icon"  runat="server"></asp:LinkButton>
<asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
</EditItemTemplate>
</telerik:GridTemplateColumn>    
</Columns>
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


</asp:Content> 
