<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" MasterPageFile="~/Common/Common.master"   Inherits="Common_Shops_Add" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
<uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">
  <script type="text/javascript">
	function onClientTabSelecting(sender, eventArgs)
	{
	    eventArgs.set_cancel(true);
	}
	
</script>
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
  <td class="pgHeading" >Add Shop</td>
  <td>
    <div class="buttons">      
       
         <asp:LinkButton ID="btnBrowse" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="browseimg" runat="server" ImageUrl="~/images/Browse.ico"  /> 
        <b>Browse</b>
        
        </asp:LinkButton>
         <asp:LinkButton ID="btnBack" runat="server" onclick="Pageaction_Click"> 
        <asp:Image ID="backimg" runat="server" ImageUrl="~/images/back.ico"  /> 
        <b>Back</b>
        </asp:LinkButton> 
</div>     
  </td>
  </tr>
  </table>  

  <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Black" MultiPageID="RadMultiPage1"
            SelectedIndex="0" CssClass="tabStrip" OnClientTabSelecting="onClientTabSelecting" >
            <Tabs>
              <telerik:RadTab Text="Information">
                </telerik:RadTab>
                 <telerik:RadTab Text="Contacts">
                </telerik:RadTab>
                <telerik:RadTab Text="Dues & Init">
                </telerik:RadTab> 
                 <telerik:RadTab Text="Benefits">
                </telerik:RadTab>    
                <telerik:RadTab Text="Upload" PostBack="false">
                </telerik:RadTab>                             
            </Tabs>
        </telerik:RadTabStrip>
   <div class="tabwrapper" >
            <div class="tabContent" >                
           <div id="btnNotes" runat="server" style="float:right;margin:10px;" ><a href="#" onclick="openRadWin(); return false;" ><img alt="" class="note-icon"/>Note</a> </div>   
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="multiPage">
            <telerik:RadPageView ID="RadPageView1" runat="server">                
             <table class="outter full">  
            <tr>
             <td class="note"><span style="color:#FF0000">Note:</span><br />
             <ul>
             <li>Field marked with <span style="color:#FF0000">*</span> are compulsory fields</li>
             <li>Field marked with <span style="color:#00F500">*</span> are Automated fields</li>
             <li>Submit Information Tab to proceed with other tabs.</li>             
             </ul>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"  
                     ValidationGroup="Tab1" />
             </td>
            </tr>  
            <tr><td class="space" >
            
            </td></tr>       
            <tr>
            <td>        
              <table class="section">	               
		    <tr><td style="width:100px">Shop-ID<span style="color:#00F500">*</span></td><td>
                     <telerik:RadNumericTextBox ID="ShopID"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Enabled="false">
                            </telerik:RadNumericTextBox>
                    </td></tr>
			<tr><td>Name<span style="color:#FF0000">*</span></td><td><asp:TextBox ID="Name"  runat="server" Width="350px" CssClass="uptext" ValidationGroup="Required" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameValidator" runat="server" 
                    ControlToValidate="Name"  ErrorMessage="Shop Name Required!" Text="*" 
                    ValidationGroup="Tab1"></asp:RequiredFieldValidator>
			</td></tr>		
            </table>              
            </td>
            </tr>
            <tr>
            <td class="break" ></td>
            </tr>
            <tr>
            <td>
            <table class="section half"  >
            <tr><td colspan="2">Primary Address</td></tr>
            <tr><td style="width:100px">Address<span style="color:#FF0000">*</span></td>
            <td><asp:TextBox ID="Pri_Address" runat="server" Width="350px" CssClass="uptext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="addressValidator" runat="server" 
            ControlToValidate="Pri_Address"  ErrorMessage="Primary Address Required!" 
            Text="*"  ValidationGroup="Tab1"></asp:RequiredFieldValidator>            
            </td></tr>
            <tr><td>City<span style="color:#FF0000">*</span></td>
            <td><asp:TextBox ID="Pri_City" runat="server" CssClass="uptext" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="CityValidator" runat="server" 
            ControlToValidate="Pri_City"  ErrorMessage="Primary City Required!" 
            Text="*"  ValidationGroup="Tab1"></asp:RequiredFieldValidator>  
            </td></tr>           
            <tr><td colspan="2" style="padding-left: 0px;">
            <table class="section">
            <tr><td style="width:100px"></td><td>State<span style="color:#FF0000">*</span></td><td>Zip<span style="color:#FF0000">*</span></td><td>Zip+4</td></tr>
            <tr>
            <td></td>
            <td><asp:TextBox ID="Pri_State" runat="server" CssClass="uptext" Width="20px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="StateValidator" runat="server" 
            ControlToValidate="Pri_State"  ErrorMessage="Primary State Required!" 
            Text="*"  ValidationGroup="Tab1"></asp:RequiredFieldValidator> 
            </td>
            <td><asp:TextBox ID="Pri_Zip" runat="server" Width="40px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ZipValidator" runat="server" 
            ControlToValidate="Pri_Zip"  ErrorMessage="Primary Zip Required!" 
            Text="*"  ValidationGroup="Tab1"></asp:RequiredFieldValidator> 
            </td>
            <td><asp:TextBox ID="Pri_Zip_Plus4" runat="server" Width="40px"></asp:TextBox></td>
            </tr>  
            </table> 
            </td></tr>           
            <tr><td>Phone</td><td><telerik:RadMaskedTextBox ID="Pri_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td><td>Extn</td><td><asp:TextBox ID="Pri_Extn" runat="server"></asp:TextBox></td></tr>
            <tr><td>Fax</td><td>  <telerik:RadMaskedTextBox ID="Pri_Fax" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td>Email</td><td><asp:TextBox ID="Pri_Email" runat="server"></asp:TextBox></td></tr>
            </table>
                     
            <table class="section half">
            <tr><td colspan="2">Secondary Address</td></tr>
            <tr><td style="width:100px">Address</td><td><asp:TextBox ID="Sec_Address" runat="server" Width="350px" CssClass="uptext"></asp:TextBox></td></tr>
            <tr><td>City</td><td><asp:TextBox ID="Sec_City" runat="server" CssClass="uptext"></asp:TextBox></td></tr>
            <tr><td colspan="2" style="padding-left: 0px;">
            <table class="section">
            <tr><td style="width:100px"></td><td>State</td><td>Zip</td><td>Zip+4</td></tr>
            <tr>
            <td></td>
            <td><asp:TextBox ID="Sec_State" runat="server" CssClass="uptext" Width="20px" ></asp:TextBox></td>
            <td><asp:TextBox ID="Sec_Zip" runat="server" Width="40px"></asp:TextBox></td>
            <td><asp:TextBox ID="Sec_Zip_Plus4" runat="server" Width="40px"></asp:TextBox></td>          
            </tr>  
            </table>  
            </td> 
            </tr>    
            <tr><td>Phone</td><td><telerik:RadMaskedTextBox ID="Sec_Phone" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td><td>Extn</td><td><asp:TextBox ID="Sec_Extn" runat="server"></asp:TextBox></td></tr>
            <tr><td>Fax</td><td><telerik:RadMaskedTextBox ID="Sec_Fax" runat="server" 
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox></td></tr>
            <tr><td>EMail</td><td ><asp:TextBox ID="Sec_Email" runat="server"></asp:TextBox></td> </tr>               
            </table>
            </td>
            </tr>
            <tr>
            <td class="break"></td>
            </tr>
            <tr>
            <td>
               <table class="section">
               <tr><td style="width:100px">Delegate<span style="color:#FF0000">*</span></td><td>
                   <telerik:RadComboBox ID="DelegateID" runat="server" Width="200px"  ValidationGroup="Required"></telerik:RadComboBox>
                   <asp:CompareValidator runat="server" ID="Comparevalidator1" 
                       ValueToCompare="Select"  Operator="NotEqual" ControlToValidate="DelegateID" 
                       ErrorMessage="Delegate Required!" Text="*"  ValidationGroup="Tab1"/>
               </td></tr>      
                <tr><td>Last Paid Dues</td><td>
               <%-- <telerik:RadDatePicker ID="LPD" runat="server" Width="200px" Height="25px"   ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput runat="server" DisplayDateFormat="MM/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker> --%>
                       <telerik:RadMonthYearPicker  Width="200px" Height="25px" runat="server" ID="LPD" ShowPopupOnFocus="True"> <DateInput runat="server"  DisplayDateFormat="MM/yyyy"   LabelWidth="40%"></DateInput> </telerik:RadMonthYearPicker>
                        </td>
                <td>Last Paid Health</td> <td>
                <%--<telerik:RadDatePicker ID="LPH" runat="server" Width="200px" Height="25px"    ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>--%>
                        <telerik:RadMonthYearPicker runat="server" Width="200px" Height="25px" ID="LPH" ShowPopupOnFocus="True"> <DateInput runat="server" LabelWidth="40%"  DisplayDateFormat="MM/yyyy"></DateInput> </telerik:RadMonthYearPicker>
                        </td>
                </tr> 
                 <tr>                            
                  <td>Open Paid Dues</td><td><asp:TextBox ID="OLPD" runat="server" Width="164px"></asp:TextBox></td>
                <td>Open Paid Health</td><td><asp:TextBox ID="OLPH"  runat="server" Width="164px"></asp:TextBox></td>             
                </tr>        
                  <tr>
                <td>Contract Start</td><td><telerik:RadDatePicker ID="Contract_Start" runat="server" Width="200px" Height="25px"    ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker></td>
                <td>Contract End</td> <td><telerik:RadDatePicker ID="Contract_End" runat="server" Width="200px" Height="25px"    ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker></td>
                </tr>     
               </table>  
               </td>
               </tr>
               </table>                             
                  <div class="btn_bar">                                      
                    <asp:Button ID="btn_tab1" CssClass="btnsubmit" runat="server" Text="Next"  
                          ValidationGroup="Tab1" CausesValidation="true" onclick="btn_tab1_Click" />                                                     
                    
               </div>      
            </telerik:RadPageView>       
             <telerik:RadPageView ID="RadPageView2" runat="server" CssClass="pageViewEducation">     
             <table class="outter full">
               <tr><td>
            <telerik:radgrid runat="server" ID="contactgrid" ShowStatusBar="True"    AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0"  onneeddatasource="contactgrid_NeedDataSource"  
                       oninsertcommand="contactgrid_InsertCommand" 
                       onupdatecommand="contactgrid_UpdateCommand" 
                       ondeletecommand="contactgrid_DeleteCommand" 
                       onitemdatabound="contactgrid_ItemDataBound">
         <PagerStyle Mode="NumericPages"></PagerStyle>
        <MasterTableView AllowMultiColumnSorting="True"  Width="100%"  EditMode="InPlace" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="False">         
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns> 
            <telerik:GridTemplateColumn HeaderText="Sno" UniqueName="Sno" Visible="false"  >
            <ItemTemplate>           
             <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>        
            <asp:Label ID="lblRecordID" runat="server" Text='<%#Eval("RecordID") %>'  Visible="false" ></asp:Label>
             <asp:Label ID="lblReferenceID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </EditItemTemplate> 
            </telerik:GridTemplateColumn>                   
            <telerik:GridTemplateColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
            <ItemTemplate>             
            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>           
            <asp:TextBox ID="txtName" Width="160px" runat="server" CssClass="uptext" Text='<%#Eval("Name") %>' ></asp:TextBox>
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn DataField="Type"  FilterControlAltText="Filter Type column" HeaderText="Type"  SortExpression="Type" UniqueName="Type">
                    <ItemTemplate>
                      <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Type") %>' Visible="false"></asp:Label>
          <telerik:RadComboBox ID="drpType" Width="160px" Enabled="false"   runat="server"  ></telerik:RadComboBox>                     
                         <asp:Label ID="lblother" runat="server" Text='<%#Eval("Other") %>' Visible="false"></asp:Label>     
                        </ItemTemplate>
            <EditItemTemplate>
             <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Type") %>' Visible="false"></asp:Label>
                <telerik:RadComboBox ID="drpType" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged"></telerik:RadComboBox>     
                 <asp:TextBox ID="txtother" runat="server" Text='<%#Eval("Other") %>' Visible="false"></asp:TextBox>           
            </EditItemTemplate>
            </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="Phone" 
                    FilterControlAltText="Filter Phone column" HeaderText="Phone" 
                    SortExpression="Phone" UniqueName="Phone">
                      <ItemTemplate>
                      
            <%--<asp:Label ID="lblPhone" runat="server"  Text='<%#Eval("Phone") %>'></asp:Label>--%>
            <telerik:RadMaskedTextBox ID="lblPhone" runat="server" Width="100px"
                    Mask="(###) ###-####" BorderStyle="None" Text='<%#Eval("Phone") %>' Enabled="false"></telerik:RadMaskedTextBox>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadMaskedTextBox ID="txtPhone" runat="server" Width="100px"
                    Mask="(###) ###-####" BorderStyle="None" Text='<%#Eval("Phone") %>'></telerik:RadMaskedTextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn DataField="Extn" 
                    FilterControlAltText="Filter Extn column" HeaderText="Extn" 
                    SortExpression="Extn" UniqueName="Extn">
                      <ItemTemplate>                      
            <asp:Label ID="lblExtn" runat="server"  Text='<%#Eval("Extn") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtExtn" Width="40px" runat="server" CssClass="uptext" Text='<%#Eval("Extn") %>' ></asp:TextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn DataField="Fax" 
                    FilterControlAltText="Filter Fax column" HeaderText="Fax" SortExpression="Fax" 
                    UniqueName="Fax">
                    <ItemTemplate>
            <%--<asp:Label ID="lblFax" runat="server" Text='<%#Eval("Fax") %>'></asp:Label>--%>
            <telerik:RadMaskedTextBox ID="lblFax" runat="server" Width="100px" Text='<%#Eval("Fax") %>'
                    Mask="(###) ###-####" BorderStyle="None" Enabled="false"></telerik:RadMaskedTextBox>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadMaskedTextBox ID="txtFax" runat="server" Width="100px" Text='<%#Eval("Fax") %>'
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="Mobile" 
                    FilterControlAltText="Filter Mobile column" HeaderText="Mobile" 
                    SortExpression="Mobile" UniqueName="Mobile">
                    <ItemTemplate>
            <%--<asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>--%>
            <telerik:RadMaskedTextBox ID="lblMobile" runat="server" Width="100px"  Text='<%#Eval("Mobile") %>'
                    Mask="(###) ###-####" BorderStyle="None" Enabled="false"></telerik:RadMaskedTextBox>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadMaskedTextBox ID="txtMobile" runat="server" Width="100px"  Text='<%#Eval("Mobile") %>'
                    Mask="(###) ###-####" BorderStyle="None"></telerik:RadMaskedTextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="Email" 
                    FilterControlAltText="Filter Email column" HeaderText="Email" 
                    SortExpression="Email" UniqueName="Email">
                    <ItemTemplate>
            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtEmail" runat="server" Width="160px" Text='<%#Eval("Email") %>'></asp:TextBox>
            </EditItemTemplate>
                </telerik:GridTemplateColumn>               
      <telerik:GridTemplateColumn HeaderText="Action" >
        <ItemStyle Width="50px" />       
              <ItemTemplate>                  
                  <asp:LinkButton ID="btnEdit" CommandName="Edit"  CssClass="edit-icon"   runat="server"></asp:LinkButton>
                  <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="del-icon"  OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?\\n" + ((string)Eval("Name"))+ "\");"%>'  runat="server"></asp:LinkButton>
              </ItemTemplate> 
              <EditItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="Update" CausesValidation="true" ValidationGroup="Required"  CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </EditItemTemplate> 
              <InsertItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="PerformInsert" CausesValidation="true" ValidationGroup="Required" CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </InsertItemTemplate>               
              </telerik:GridTemplateColumn> 
              
            </Columns>            
           <EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
</EditFormSettings>
        </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
  
        </telerik:radgrid>                                
        </td>
        </tr> 
        </table>
            <div class="btn_bar">                                      
                    <asp:Button ID="btn_tab2" CssClass="btnsubmit" runat="server" Text="Next"  CausesValidation="true" onclick="btn_tab2_Click"  />         
                    </div> 
            </telerik:RadPageView>   
   <telerik:RadPageView ID="RadPageView3" runat="server" CssClass="pageViewEducation"> 
                  <table class="outter full">
               <tr><td>                 
            <telerik:radgrid runat="server" ID="feegrid" ShowStatusBar="True"    AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0" onneeddatasource="feegrid_NeedDataSource" 
                       oninsertcommand="feegrid_InsertCommand" 
                       onupdatecommand="feegrid_UpdateCommand" 
                       ondeletecommand="feegrid_DeleteCommand" 
                       onitemdatabound="feegrid_ItemDataBound" >
         <PagerStyle Mode="NumericPages"></PagerStyle>
         <MasterTableView AllowMultiColumnSorting="True"  Width="100%"  EditMode="InPlace" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="False">         
        
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns> 
            <telerik:GridTemplateColumn HeaderText="Sno" UniqueName="Sno" Visible="false" >
            <ItemTemplate>           
             <asp:Label ID="lblReferenceID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>        
            <asp:Label ID="lblRecordID" runat="server" Text='<%#Eval("RecordID") %>'  Visible="false" ></asp:Label>
             <asp:Label ID="lblReferenceID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblSno" runat="server"></asp:Label>
            </EditItemTemplate> 
            </telerik:GridTemplateColumn> 
                <telerik:GridTemplateColumn SortExpression="Due_FullTime" HeaderText="Due Full Time" HeaderButtonType="TextButton"
                    DataField="Due_FullTime" UniqueName="Due_FullTime">
                     <ItemTemplate>
            <asp:Label ID="lblDue_FullTime" runat="server" Text='<%#Eval("Due_FullTime") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <telerik:RadNumericTextBox ID="txtDue_FullTime"  Width="100px" height="25px" Text='<%#Eval("Due_FullTime") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                                          
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn SortExpression="Due_PartTime" HeaderText="Due Part Time" HeaderButtonType="TextButton"
                    DataField="Due_PartTime" UniqueName="Due_PartTime">
                     <ItemTemplate>
            <asp:Label ID="lblDue_PartTime" runat="server" Text='<%#Eval("Due_PartTime") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <telerik:RadNumericTextBox ID="txtDue_PartTime"  Width="100px" height="25px" Text='<%#Eval("Due_PartTime") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                                                      
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="Init_FullTime" HeaderText="InitFee Full Time" HeaderButtonType="TextButton"
                    DataField="Init_FullTime" UniqueName="Init_FullTime">
                     <ItemTemplate>  
                       <asp:Label ID="lblInit_FullTime" runat="server" Text='<%#Eval("Init_FullTime") %>'></asp:Label>                      
            </ItemTemplate>
            <EditItemTemplate>            
            <telerik:RadNumericTextBox ID="txtInit_FullTime"  Width="100px" height="25px" Text='<%#Eval("Init_FullTime") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                              
            </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn SortExpression="Init_PartTime" HeaderText="InitFee Part Time" HeaderButtonType="TextButton"
                    DataField="Init_PartTime" UniqueName="Init_PartTime">       
                     <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Init_PartTime") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <telerik:RadNumericTextBox ID="txtInit_PartTime"  Width="100px" height="25px" Text='<%#Eval("Init_PartTime") %>' runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>                                          
            </EditItemTemplate>                             
                </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="Effective_Date" HeaderText="Effective From" HeaderButtonType="TextButton"
                    DataField="Effective_Date" UniqueName="Effective_Date">
                     <ItemTemplate>
            <asp:Label ID="lblEffective_Date" runat="server" Text='<%#Eval("Effective_Date") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <telerik:RadDatePicker ID="txtEffective_Date" runat="server"  Height="25px"  DbSelectedDate='<%# Bind("Effective_Date") %>'  ShowPopupOnFocus="True">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%"></DateInput>
                        </telerik:RadDatePicker>          
            </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                       <telerik:GridTemplateColumn HeaderText="Action" >
        <ItemStyle Width="50px" />       
              <ItemTemplate>                  
                  <asp:LinkButton ID="btnEdit" CommandName="Edit"  CssClass="edit-icon"   runat="server"></asp:LinkButton>
                  <asp:LinkButton ID="btnDelete" CommandName="Delete" CssClass="del-icon"  OnClientClick='<%# "javascript:return confirm(\"Are you sure want to delete this Record ?"+ "\");"%>'  runat="server"></asp:LinkButton>
              </ItemTemplate> 
              <EditItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="Update"   CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </EditItemTemplate> 
              <InsertItemTemplate>
               <asp:LinkButton ID="btnEdit" CommandName="PerformInsert"  CssClass="ok-icon"  runat="server"></asp:LinkButton>
               <asp:LinkButton ID="btnCancel" CommandName="CancelAll" CssClass="cancel-icon"  runat="server"></asp:LinkButton>
              </InsertItemTemplate>               
              </telerik:GridTemplateColumn> 
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" 
                    UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
        
        </telerik:radgrid>     
        </td></tr>  </table> 
         <div class="btn_bar">                                      
                    <asp:Button ID="btn_tab3" CssClass="btnsubmit" runat="server" Text="Next"   CausesValidation="true" onclick="btn_tab3_Click"   />         
                    </div> 
           
            </telerik:RadPageView>   
           <telerik:RadPageView ID="RadPageView4" runat="server" CssClass="pageViewEducation"> 
                  <table class="outter full">  
                  <tr>
                  <td>
                     <asp:RadioButtonList ID="BenefitType" runat="server" 
                          RepeatDirection="Horizontal" CssClass="removestyle" AutoPostBack="true" 
                          onselectedindexchanged="BenefitType_SelectedIndexChanged"  >                      
                          <asp:ListItem Value="1">Default</asp:ListItem>
                          <asp:ListItem Value="0" Selected="True">No Benefits</asp:ListItem>                      
                      </asp:RadioButtonList>
                      <br />
                  </td></tr>                
               <tr><td>                           
                   <telerik:RadGrid runat="server" ID="Benefits" ShowStatusBar="True"    
                       AutoGenerateColumns="False"  GridLines="None" 
                      Width="100%" CellSpacing="0" onneeddatasource="Benefits_NeedDataSource" 
                       ondatabound="Benefits_DataBound">
                       <MasterTableView AllowMultiColumnSorting="True"  Width="100%">         
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                Visible="True">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
            </ExpandCollapseColumn>
            <Columns>          
            <telerik:GridTemplateColumn>
            <ItemTemplate>             
                <asp:CheckBox ID="Chk" runat="server" />
                  <asp:Label ID="lblBenefitID" runat="server" Text='<%#Eval("RecordID") %>' Visible="false" ></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
            <ItemTemplate>             
            <asp:Label ID="lblName" runat="server" CssClass="uptext" Text='<%#Eval("Name") %>'></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
              <telerik:GridTemplateColumn DataField="Description" FilterControlAltText="Filter Name column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
            <ItemTemplate>             
            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
            </ItemTemplate>          
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn DataField="providedby" FilterControlAltText="Filter Name column" HeaderText="Provider" SortExpression="providedby" UniqueName="Description">
            <ItemTemplate>             
            <asp:Label ID="lblprovider" runat="server" Text='<%#Eval("providedby") %>'></asp:Label>
            </ItemTemplate>           
            </telerik:GridTemplateColumn>  
            <telerik:GridTemplateColumn DataField="Eligibility" FilterControlAltText="Filter Name column" HeaderText="Eligibility" SortExpression="Eligibility" UniqueName="Eligibility">
            <ItemTemplate> 
            <telerik:RadNumericTextBox ID="Eligibility"  Width="160px" height="25px" runat="server" Type="Number" MinValue="0"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
            </telerik:RadNumericTextBox>     
            </ItemTemplate>         
            </telerik:GridTemplateColumn>   
            </Columns> 
            </MasterTableView>   
                   </telerik:RadGrid>
        </td></tr> <tr><td align"center" style="font-weight:bold">
            <asp:Label ID="lblNA" runat="server" Text="N/A"></asp:Label> </td></tr>  </table> 
      <div class="btn_bar">                                    
                    <asp:Button ID="btn_tab4" CssClass="btnsubmit" runat="server"   Text="Next" onclick="btn_tab4_Click"  />                 
               </div>      
            </telerik:RadPageView>  
            <telerik:RadPageView ID="RadPageView5" runat="server" CssClass="pageViewEducation"> 
                  <table class="outter full">  
                  <tr>
                  <td>
                  <telerik:RadGrid ID="Doc_Grid" runat="server" AllowSorting="false" 
                          AllowPaging="false" AutoGenerateColumns="false" 
                          onneeddatasource="Doc_Grid_NeedDataSource" OnItemDataBound="Doc_Grid_ItemDataBound">
                  <MasterTableView>
                  <Columns>
                    <telerik:GridTemplateColumn DataField="ReferenceID" UniqueName="ReferenceID" HeaderText="ReferenceID" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="ReferenceID" runat="server" Text='<%#Eval("ReferenceID")%>'></asp:Label>
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                 
                  <telerik:GridBoundColumn DataField="Document" UniqueName="Document" HeaderText="FileName">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Date">
                  </telerik:GridBoundColumn>
                  <telerik:GridTemplateColumn UniqueName="RecordID" HeaderText="File">
                  <ItemTemplate>
                  <asp:HyperLink ID="File" ToolTip='<%#Eval("RecordID")%>' runat="server" ><span class="subitem"><asp:ImageMap ID="ImgHyper" runat="server"></asp:ImageMap></span></asp:HyperLink>
                  </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn Visible="false" UniqueName="Extension">
                 <ItemTemplate>
                 <asp:Label ID="FileExt" runat="server" Text='<%#Eval("Extension")%>'></asp:Label>
                 </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  </Columns>
                  </MasterTableView>             
                  </telerik:RadGrid>
                  </td>
                  </tr>
                  <tr>
                  <td><hr /></td>
                  </tr>
                  <tr><td>Document:</td></tr>
                  <tr>
                  <td>
                  <telerik:RadAsyncUpload ID="Doc_Upload" runat="server" MultipleFileSelection="Automatic"></telerik:RadAsyncUpload>
                  </td>
                  </tr>
                  <%--<tr>
                  <td><asp:Button ID="Button1" CssClass="btnsubmit" runat="server"   Text="Upload" onclick="btn_tab5_Click"  /></td>
                  </tr>--%>
                  </table>
                   <div class="btn_bar">                                    
                    <asp:Button ID="btn_tab5" CssClass="btnsubmit" runat="server"   Text="Done" onclick="btn_tab5_Click"  />                 
               </div> 
                  </telerik:RadPageView>  
        </telerik:RadMultiPage>
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="Textarea">
        </telerik:RadFormDecorator>
              
            </div>            
        </div> 
</div> 
 <telerik:RadWindowManager ID="RadWindowManager" runat="server">
</telerik:RadWindowManager>
<telerik:RadWindow ID="RadWindow" runat="server"  NavigateUrl="Notes.aspx" Width="850" Height="650" Header="Notes" Modal="true">
</telerik:RadWindow>
    <script type="text/javascript">
        //<![CDATA[
        function openRadWin() {

            var oWnd = $find("<%= RadWindow.ClientID %>");
            oWnd.show();
        }
        //]]>
    </script>
</asp:Content> 
