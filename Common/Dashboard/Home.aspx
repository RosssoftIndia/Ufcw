<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Home.aspx.cs" Inherits="Common_Dashboard_Home" MasterPageFile="~/Common/Dashboard.master"   %>

<%@ Register src="~/Common/menu.ascx" tagname="menu" tagprefix="uc1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ContentPlaceHolderID="MenuPlaceHolder"  ID="Menublk" runat="server">   
    <uc1:menu ID="menu" runat="server" />
</asp:Content> 


<asp:Content ContentPlaceHolderID="ContentPlaceHolder"  ID="Contentblk" runat="server">       
    <div id="content">
            
            <div id="start">Dashboard</div>
            
    <%--        <div id="user">
                <div id="name">
                    <div id="firstname">Test</div>  
                    <div id="lastname">test</div>
                </div>
                <div id="photo">
                    <img src="img/User No-Frame.png" width="40" height="40" />
                </div>
            </div>--%>
            
            
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
         
             <asp:Timer ID="Timer" runat="server" Interval="60000" ontick="Timer_Tick">
             
             </asp:Timer>
              <div id="metro-sections-container" class="metro">
                <div class="metro-sections" >
                  <div class="metro-section" style="width:100%!important;" >
                        
                               <div class="tile tile-double bg-color-blue" style="height:100%;">      
                               
                  <asp:DetailsView ID="Member_statistics" CssClass="metrodetailview" runat="server" AutoGenerateRows="False">
                      <Fields>
                      <asp:TemplateField ShowHeader="false"   ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Members</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Total" />
                          <asp:BoundField DataField="Active" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Active" />
                          <asp:BoundField DataField="InActive" HeaderText="In Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="InActive" />
                                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Last 7days</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Last7daysHired" HeaderText="Hired"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="Last7daysHired" />
                          <asp:BoundField DataField="Last7daysTerminated" HeaderText="Terminated"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Last7daysTerminated" />
                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Today</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="TodayHired" HeaderText="Hired"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="TodayHired" />
                          <asp:BoundField DataField="TodayTerminated" HeaderText="Terminated"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="TodayTerminated" />
                      </Fields>                   
                  </asp:DetailsView>                   
             
 <telerik:RadChart ID="Member_Chart" runat="server"  Height="230px" Skin="Mac" Width="310px"  
                                       ChartTitle-Visible="false">
                      <Series>
                          <telerik:ChartSeries Name="Series 1">
                              <Appearance>
                                  <FillStyle MainColor="55, 167, 226" SecondColor="22, 85, 161">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Series 2">
                              <Appearance>
                                  <FillStyle MainColor="223, 87, 60" SecondColor="200, 38, 37">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                      </Series>
                      <PlotArea>
                          <XAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                  <MajorGridLines Color="209, 222, 227" PenStyle="Solid" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </XAxis>
                          <YAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" 
                                  MinorTick-Color="134, 134, 134" MinorTick-Width="0">
                                  <MajorGridLines Color="209, 222, 227" />
                                  <MinorGridLines Color="233, 239, 241" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </YAxis>
                          <Appearance>
                              <FillStyle FillType="Solid" MainColor="White">
                              </FillStyle>
                              <Border Color="134, 134, 134" />
                          </Appearance>
                      </PlotArea>
                      <Appearance Corners="Round, Round, Round, Round, -1">
                          <FillStyle FillType="Image">
                              <FillSettings BackgroundImage="{chart}" ImageDrawMode="Flip" ImageFlip="FlipX">
                              </FillSettings>
                          </FillStyle>
                          <Border Color="138, 138, 138" />
                      </Appearance>
                      <ChartTitle Visible="False">
                          <Appearance Visible="False" Position-AlignedPosition="Top">
                              <FillStyle MainColor="">
                              </FillStyle>
                          </Appearance>
                          <TextBlock>
                              <Appearance TextProperties-Font="Tahoma, 13pt">
                              </Appearance>
                          </TextBlock>
                      </ChartTitle>
                      <Legend>
                          <Appearance Dimensions-Margins="15.4%, 3%, 1px, 1px" 
                              Position-AlignedPosition="TopRight">
                              <ItemMarkerAppearance Figure="Square">
                                  <Border Color="134, 134, 134" />
                              </ItemMarkerAppearance>
                              <FillStyle MainColor="">
                              </FillStyle>
                              <Border Color="Transparent" />
                          </Appearance>
                      </Legend>
                  </telerik:RadChart>

    
                        </div>                 
                        
                          <div class="tile tile-double bg-color-pink" style="height:100%;">
                             <asp:DetailsView ID="Shop_statistics" CssClass="metrodetailview" runat="server" AutoGenerateRows="False">
                      <Fields>
                      <asp:TemplateField ShowHeader="false"   ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Shops</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Total" />
                          <asp:BoundField DataField="Active" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Active" />
                          <asp:BoundField DataField="InActive" HeaderText="In Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="InActive" />
                                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Last 7days</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Last7daysHired" HeaderText="Hired"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="Last7daysHired" />
                          <asp:BoundField DataField="Last7daysTerminated" HeaderText="Terminated"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Last7daysTerminated" />
                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Today</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="TodayHired" HeaderText="Hired"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="TodayHired" />
                          <asp:BoundField DataField="TodayTerminated" HeaderText="Terminated"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="TodayTerminated" />
                      </Fields>                   
                  </asp:DetailsView>                   
             
 <telerik:RadChart ID="Shop_Chart" runat="server"  Height="230px" Skin="Mac" Width="310px"  
                                  ChartTitle-Visible="false">
                      <Series>
                          <telerik:ChartSeries Name="Series 1">
                              <Appearance>
                                  <FillStyle MainColor="55, 167, 226" SecondColor="22, 85, 161">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Series 2">
                              <Appearance>
                                  <FillStyle MainColor="223, 87, 60" SecondColor="200, 38, 37">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                      </Series>
                      <PlotArea>
                          <XAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                  <MajorGridLines Color="209, 222, 227" PenStyle="Solid" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </XAxis>
                          <YAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" 
                                  MinorTick-Color="134, 134, 134" MinorTick-Width="0">
                                  <MajorGridLines Color="209, 222, 227" />
                                  <MinorGridLines Color="233, 239, 241" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </YAxis>
                          <Appearance>
                              <FillStyle FillType="Solid" MainColor="White">
                              </FillStyle>
                              <Border Color="134, 134, 134" />
                          </Appearance>
                      </PlotArea>
                      <Appearance Corners="Round, Round, Round, Round, -1">
                          <FillStyle FillType="Image">
                              <FillSettings BackgroundImage="{chart}" ImageDrawMode="Flip" ImageFlip="FlipX">
                              </FillSettings>
                          </FillStyle>
                          <Border Color="138, 138, 138" />
                      </Appearance>
                      <ChartTitle Visible="False">
                          <Appearance Visible="False" Position-AlignedPosition="Top">
                              <FillStyle MainColor="">
                              </FillStyle>
                          </Appearance>
                          <TextBlock>
                              <Appearance TextProperties-Font="Tahoma, 13pt">
                              </Appearance>
                          </TextBlock>
                      </ChartTitle>
                      <Legend>
                          <Appearance Dimensions-Margins="15.4%, 3%, 1px, 1px" 
                              Position-AlignedPosition="TopRight">
                              <ItemMarkerAppearance Figure="Square">
                                  <Border Color="134, 134, 134" />
                              </ItemMarkerAppearance>
                              <FillStyle MainColor="">
                              </FillStyle>
                              <Border Color="Transparent" />
                          </Appearance>
                      </Legend>
                  </telerik:RadChart>
                                                       
                        </div>
                         <div class="tile tile-double bg-color-green" style="height:100%;display:none;"  >
                          <asp:DetailsView ID="Provider_statistics" CssClass="metrodetailview" runat="server" AutoGenerateRows="False">
                      <Fields>
                      <asp:TemplateField ShowHeader="false"   ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Providers</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Total" />
                          <asp:BoundField DataField="Active" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Active" />
                          <asp:BoundField DataField="InActive" HeaderText="In Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="InActive" />
                                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Last 7days</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Last7daysHired" HeaderText="Active"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="Last7daysHired" />
                          <asp:BoundField DataField="Last7daysTerminated" HeaderText="In Active" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Last7daysTerminated" />
                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Today</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="TodayHired" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="TodayHired" />
                          <asp:BoundField DataField="TodayTerminated" HeaderText="In Active"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="TodayTerminated" />
                      </Fields>                   
                  </asp:DetailsView>     
                           <telerik:RadChart ID="Provider_Chart" runat="server"  Height="230px" Skin="Mac" Width="310px"  
                                       ChartTitle-Visible="false">
                      <Series>
                          <telerik:ChartSeries Name="Series 1">
                              <Appearance>
                                  <FillStyle MainColor="55, 167, 226" SecondColor="22, 85, 161">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Series 2">
                              <Appearance>
                                  <FillStyle MainColor="223, 87, 60" SecondColor="200, 38, 37">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                      </Series>
                      <PlotArea>
                          <XAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                  <MajorGridLines Color="209, 222, 227" PenStyle="Solid" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </XAxis>
                          <YAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" 
                                  MinorTick-Color="134, 134, 134" MinorTick-Width="0">
                                  <MajorGridLines Color="209, 222, 227" />
                                  <MinorGridLines Color="233, 239, 241" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </YAxis>
                          <Appearance>
                              <FillStyle FillType="Solid" MainColor="White">
                              </FillStyle>
                              <Border Color="134, 134, 134" />
                          </Appearance>
                      </PlotArea>
                      <Appearance Corners="Round, Round, Round, Round, -1">
                          <FillStyle FillType="Image">
                              <FillSettings BackgroundImage="{chart}" ImageDrawMode="Flip" ImageFlip="FlipX">
                              </FillSettings>
                          </FillStyle>
                          <Border Color="138, 138, 138" />
                      </Appearance>
                      <ChartTitle Visible="False">
                          <Appearance Visible="False" Position-AlignedPosition="Top">
                              <FillStyle MainColor="">
                              </FillStyle>
                          </Appearance>
                          <TextBlock>
                              <Appearance TextProperties-Font="Tahoma, 13pt">
                              </Appearance>
                          </TextBlock>
                      </ChartTitle>
                      <Legend>
                          <Appearance Dimensions-Margins="15.4%, 3%, 1px, 1px" 
                              Position-AlignedPosition="TopRight">
                              <ItemMarkerAppearance Figure="Square">
                                  <Border Color="134, 134, 134" />
                              </ItemMarkerAppearance>
                              <FillStyle MainColor="">
                              </FillStyle>
                              <Border Color="Transparent" />
                          </Appearance>
                      </Legend>
                  </telerik:RadChart> 
                     <%--     
                  <asp:GridView ID="Provider_grid" runat="server" CssClass="grid"  AutoGenerateColumns="False" >
                  <Columns>
                  <asp:TemplateField>
                  <ItemTemplate>
                      <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                  </ItemTemplate>              
                  <ItemStyle  BorderWidth="0px"  />  
                  <HeaderStyle  BorderWidth="0px" CssClass="text-left" Font-Bold="true" ForeColor="orange"  />  
                  </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Benefits" ShowHeader="false" Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="Label2" runat="server" Text='<%#Eval("Benefits") %>'></asp:Label>
                  </ItemTemplate>              
                  <ItemStyle  BorderWidth="0px" CssClass="text-center"  />
                  <HeaderStyle  BorderWidth="0px" CssClass="text-left" Font-Bold="true" ForeColor="orange" />  
                  </asp:TemplateField> 
                 
                  </Columns> 
                  </asp:GridView>--%>
                            
                        </div>
                          <div class="tile tile-double bg-color-purple" style="height:100%;display:none;"  >
                          <asp:DetailsView ID="Benefit_statistics" CssClass="metrodetailview" runat="server" AutoGenerateRows="False">
                      <Fields>
                      <asp:TemplateField ShowHeader="false"   ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Benefits</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Total" />
                          <asp:BoundField DataField="Active" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Active" />
                          <asp:BoundField DataField="InActive" HeaderText="In Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="InActive" />
                                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Last 7days</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="Last7daysHired" HeaderText="Active"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="Last7daysHired" />
                          <asp:BoundField DataField="Last7daysTerminated" HeaderText="In Active" HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="Last7daysTerminated" />
                      <asp:TemplateField ShowHeader="false"  ItemStyle-CssClass="header">
                      <ItemTemplate>
                       <div class="header"> 
                             <h3>Today</h3>
                                <hr />
                                </div>   
                      </ItemTemplate>                       
                      </asp:TemplateField> 
                          <asp:BoundField DataField="TodayHired" HeaderText="Active"  HeaderStyle-CssClass="hd" ReadOnly="True" 
                              SortExpression="TodayHired" />
                          <asp:BoundField DataField="TodayTerminated" HeaderText="In Active"  HeaderStyle-CssClass="hd"
                              ReadOnly="True" SortExpression="TodayTerminated" />
                      </Fields>                   
                  </asp:DetailsView> 
                   <telerik:RadChart ID="Benefit_Chart" runat="server"  Height="230px" Skin="Mac" Width="310px"  
                                       ChartTitle-Visible="false">
                      <Series>
                          <telerik:ChartSeries Name="Series 1">
                              <Appearance>
                                  <FillStyle MainColor="55, 167, 226" SecondColor="22, 85, 161">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Series 2">
                              <Appearance>
                                  <FillStyle MainColor="223, 87, 60" SecondColor="200, 38, 37">
                                      <FillSettings GradientMode="Vertical">
                                      </FillSettings>
                                  </FillStyle>
                                  <TextAppearance TextProperties-Color="Black">
                                  </TextAppearance>
                              </Appearance>
                          </telerik:ChartSeries>
                      </Series>
                      <PlotArea>
                          <XAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                  <MajorGridLines Color="209, 222, 227" PenStyle="Solid" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </XAxis>
                          <YAxis>
                              <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" 
                                  MinorTick-Color="134, 134, 134" MinorTick-Width="0">
                                  <MajorGridLines Color="209, 222, 227" />
                                  <MinorGridLines Color="233, 239, 241" />
                                  <TextAppearance TextProperties-Color="51, 51, 51">
                                  </TextAppearance>
                              </Appearance>
                              <AxisLabel>
                                  <TextBlock>
                                      <Appearance TextProperties-Color="51, 51, 51">
                                      </Appearance>
                                  </TextBlock>
                              </AxisLabel>
                          </YAxis>
                          <Appearance>
                              <FillStyle FillType="Solid" MainColor="White">
                              </FillStyle>
                              <Border Color="134, 134, 134" />
                          </Appearance>
                      </PlotArea>
                      <Appearance Corners="Round, Round, Round, Round, -1">
                          <FillStyle FillType="Image">
                              <FillSettings BackgroundImage="{chart}" ImageDrawMode="Flip" ImageFlip="FlipX">
                              </FillSettings>
                          </FillStyle>
                          <Border Color="138, 138, 138" />
                      </Appearance>
                      <ChartTitle Visible="False">
                          <Appearance Visible="False" Position-AlignedPosition="Top">
                              <FillStyle MainColor="">
                              </FillStyle>
                          </Appearance>
                          <TextBlock>
                              <Appearance TextProperties-Font="Tahoma, 13pt">
                              </Appearance>
                          </TextBlock>
                      </ChartTitle>
                      <Legend>
                          <Appearance Dimensions-Margins="15.4%, 3%, 1px, 1px" 
                              Position-AlignedPosition="TopRight">
                              <ItemMarkerAppearance Figure="Square">
                                  <Border Color="134, 134, 134" />
                              </ItemMarkerAppearance>
                              <FillStyle MainColor="">
                              </FillStyle>
                              <Border Color="Transparent" />
                          </Appearance>
                      </Legend>
                  </telerik:RadChart> 
                        </div>
                    </div>              
                </div>
            </div>
         </ContentTemplate> 
        </asp:UpdatePanel> 
            
        </div>    

</asp:Content> 