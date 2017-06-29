using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Globalization;
using System.IO;
using System.Text;

public partial class Common_Members_Browse : SessionTracker
{    
    int weekNumber;
    Repository.Struct.Searchmember  criteria = new Repository.Struct.Searchmember();   
    protected void Page_Load(object sender, EventArgs e)
    {
        //session
        Session["Sshop"] = 0;    
        //menu
        menu.GenerateMenu("Members");
        //SetActionbar
        Repository.Menu.ActionBar(null, btnAdd, null, null);     

        weekNumber = GetNumber(DateTime.Now.Date);
        if (!Page.IsPostBack)
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Sunday;
            DateTime start = DateTime.Now.AddDays(-days);
            DateTime end = start.AddDays(6);

            RadCalendar.RangeSelectionStartDate = start;
            RadCalendar.RangeSelectionEndDate = end;
            startdate.Text = start.ToString("MM/dd/yyyy");
            enddate.Text = end.ToString("MM/dd/yyyy");

            if (Session["Smember"] != null && Session["Smember"].ToString() != "0")
            {
                criteria = (Repository.Struct.Searchmember)Session["Smember"];
                setcriteria("Fromsession");

            }
            else
            {
                resetcriteria();
            }

        }
        else
        {
            if (Session["Smember"] != null && Session["Smember"].ToString() != "0")
            {
                criteria = (Repository.Struct.Searchmember)Session["Smember"];
              //  setcriteria("Fromsession");
            }
        }
      
    }


    protected void BrowseGridSearch_PreRender(object sender, EventArgs e)
    {
        if (BrowseGridSearch.EditIndexes.Count > 0)
        {
            BrowseGridSearch.MasterTableView.GetColumn("CreateDate").HeaderText = "Terminated Date";
            BrowseGridSearch.Rebind();
        }
        else
        {
            BrowseGridSearch.MasterTableView.GetColumn("CreateDate").HeaderText = "Create Date";
            BrowseGridSearch.Rebind();
        }
    }
    protected void BrowseGridSearch_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridSearch.AllowPaging = criteria.allowpaging;
        BrowseGridSearch.PageSize = criteria.pagerno;
        BrowseGridSearch.CurrentPageIndex = criteria.pageno;

        Repository.Members.Browse_MembersbyStatus(BrowseGridSearch, "", criteria);
    }  
    protected void BrowseGridSearch_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            if (!e.Item.IsInEditMode)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //if (dataItem["SSN"].Text.ToString() != null && dataItem["SSN"].Text.ToString() != "" && dataItem["SSN"].Text.ToString() != "&nbsp;")
                //{
                //    double dbl = Double.Parse(dataItem["SSN"].Text.ToString());
                //    string str = String.Format("{0:###-##-####}", dbl);
                //    dataItem["SSN"].Text = str;
                //}

                //GridBar
                HyperLink View = (HyperLink)e.Item.FindControl("lnkview");
                HyperLink Edit = (HyperLink)e.Item.FindControl("lnkedit");
                LinkButton Delete = (LinkButton)e.Item.FindControl("btndelete");
                Repository.Menu.GridBar(View, Edit, Delete);

                //Notes SectionMid
                Image ImgNote = (Image)e.Item.FindControl("Img_Note");

                bool Notes = (dataItem["Mid"].Text != "&nbsp;") ? Repository.Members.GetNotes_byMember(dataItem["Mid"].Text) : false;

                if (Notes)
                {
                    ImgNote.Visible = true;
                }
                else
                {
                    ImgNote.Visible = false;
                }
            }
        }

        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Smember"] = criteria;

            PageSizeCombo.Items.Clear();
            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("20"));
            PageSizeCombo.FindItemByText("20").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.FindItemByText(criteria.pagerno.ToString()).Selected = true;
        }

    }
    protected void BrowseGridSearch_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        criteria.pageno = e.NewPageIndex;
        Session["Smember"] = criteria;
    }
    protected void BrowseGridSearch_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("FirstName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 1;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("LastName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 2;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("HiredDate".Equals(e.CommandArgument))
        {
            criteria.sortmode = 3;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("CreateDate".Equals(e.CommandArgument))
        {
            criteria.sortmode = 4;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
    }

    protected void BrowseGridActive_PreRender(object sender, EventArgs e)
    {
        if (BrowseGridActive.EditIndexes.Count > 0)
        {
            BrowseGridActive.MasterTableView.GetColumn("CreateDate").HeaderText = "Terminated Date";
            BrowseGridActive.Rebind();
        }
        else
        {
            BrowseGridActive.MasterTableView.GetColumn("CreateDate").HeaderText = "Create Date";
            BrowseGridActive.Rebind();
        }
    }
    protected void BrowseGridActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridActive.AllowPaging = criteria.allowpaging;
        BrowseGridActive.PageSize = criteria.pagerno;
        BrowseGridActive.CurrentPageIndex = criteria.pageno;
        
        Repository.Members.Browse_MembersbyStatus(BrowseGridActive, "Active",criteria);            
    }
    protected void BrowseGridActive_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Mid"];
        bool hasValue = (DeletedItem["CreateDate"].Controls[0] as RadDatePicker).SelectedDate.HasValue;
        string strDate = (hasValue ? (DeletedItem["CreateDate"].Controls[0] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);

        string ReferenceID = id.ToString();
        Repository.Struct.SpResultset resultset = Repository.Members.Members_InActive(ReferenceID, strDate);
        if (resultset.Isresult)
        {
            refresh();
        }
    }
    protected void BrowseGridActive_ItemDataBound(object sender, GridItemEventArgs e)
    {       
        if (e.Item is GridDataItem)
        {
            if (!e.Item.IsInEditMode)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //if (dataItem["SSN"].Text.ToString() != null && dataItem["SSN"].Text.ToString() != "" && dataItem["SSN"].Text.ToString() != "&nbsp;")
                //{
                //    double dbl = Double.Parse(dataItem["SSN"].Text.ToString());
                //    string str = String.Format("{0:###-##-####}", dbl);
                //    dataItem["SSN"].Text = str;
                //}

                //GridBar
                HyperLink View = (HyperLink)e.Item.FindControl("lnkview");
                HyperLink Edit = (HyperLink)e.Item.FindControl("lnkedit");
                LinkButton Delete = (LinkButton)e.Item.FindControl("btndelete");
                Repository.Menu.GridBar(View, Edit, Delete);

                //Notes SectionMid
                Image ImgNote = (Image)e.Item.FindControl("Img_Note");

                bool Notes = (dataItem["Mid"].Text != "&nbsp;" ) ? Repository.Members.GetNotes_byMember(dataItem["Mid"].Text) : false;
                
                if (Notes)
                {
                    ImgNote.Visible = true;
                }
                else
                {
                    ImgNote.Visible = false;
                }
            }          
        }

        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Smember"] = criteria;

            PageSizeCombo.Items.Clear();
            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("20"));
            PageSizeCombo.FindItemByText("20").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);          
            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", BrowseGridActive.MasterTableView.ClientID);
            PageSizeCombo.FindItemByText(criteria.pagerno.ToString()).Selected = true;      
        } 

    }
    protected void BrowseGridActive_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        criteria.pageno = e.NewPageIndex;
        Session["Smember"] = criteria;
    }
    protected void BrowseGridActive_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("FirstName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 1;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("LastName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 2;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
        }
        if ("HiredDate".Equals(e.CommandArgument))
        {
            criteria.sortmode = 3;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("CreateDate".Equals(e.CommandArgument))
        {
            criteria.sortmode = 4;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
    }

    protected void BrowseGridInActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridInActive.AllowPaging = criteria.allowpaging;
        BrowseGridInActive.PageSize = criteria.pagerno;
        BrowseGridInActive.CurrentPageIndex = criteria.pageno;
        
        Repository.Members.Browse_MembersbyStatus(BrowseGridInActive, "InActive",criteria);

       
    }
    protected void BrowseGridInActive_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Mid"];
        bool hasValue = (DeletedItem["TerminatedDate"].Controls[0] as RadDatePicker).SelectedDate.HasValue;
        string strDate = (hasValue ? (DeletedItem["TerminatedDate"].Controls[0] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);

        string ReferenceID = id.ToString();
        Repository.Struct.SpResultset resultset = Repository.Members.Members_InActive(ReferenceID, strDate);
        if (resultset.Isresult)
        {
            refresh();
        }
    }
    protected void BrowseGridInActive_ItemDataBound(object sender, GridItemEventArgs e)
    {     

        if (e.Item is GridDataItem)
        {        

            if (!e.Item.IsInEditMode)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //if (dataItem["SSN"].Text.ToString() != null && dataItem["SSN"].Text.ToString() != "" && dataItem["SSN"].Text.ToString() != "&nbsp;")
                //{
                //    double dbl = Double.Parse(dataItem["SSN"].Text.ToString());
                //    string str = String.Format("{0:###-##-####}", dbl);
                //    dataItem["SSN"].Text = str;
                //}

                //GridBar
                HyperLink View = (HyperLink)e.Item.FindControl("lnkview");
                LinkButton Delete = (LinkButton)e.Item.FindControl("btndelete");
                Repository.Menu.GridBar(View, null,Delete);   

                Image ImgNote = (Image)e.Item.FindControl("Img_Note");

                bool Notes = (dataItem["Mid"].Text !="&nbsp;") ? Repository.Members.GetNotes_byMember(dataItem["Mid"].Text) : false;

                if (Notes)
                {
                    ImgNote.Visible = true;
                }
                else
                {
                    ImgNote.Visible = false;
                }
            }
        }

        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Smember"] = criteria;

            PageSizeCombo.Items.Clear();
            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", BrowseGridInActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("20"));
            PageSizeCombo.FindItemByText("20").Attributes.Add("ownerTableViewId", BrowseGridInActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", BrowseGridInActive.MasterTableView.ClientID);
            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", BrowseGridInActive.MasterTableView.ClientID);
            PageSizeCombo.FindItemByText(criteria.pagerno.ToString()).Selected = true;      
        } 

       
    }
    protected void BrowseGridInActive_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        criteria.pageno = e.NewPageIndex; 
        Session["Smember"] = criteria;
    }
    protected void BrowseGridInActive_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("FirstName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 1;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
        if ("LastName".Equals(e.CommandArgument))
        {
            criteria.sortmode = 2;
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";

                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";

                    break;
            }
            Session["Smember"] = criteria;
        }
    }


    public void refresh()
    {
       
        BrowseGridActive.Rebind();
        BrowseGridInActive.Rebind(); 
  
    }
    
    protected void SearchFilter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        txtshopid.Visible = false;
        txtsearchtag.Visible = false;
        RadFilter.Enabled = true;
        RadComboBox combo = (RadComboBox)sender;
        switch (combo.ID)
        {
            case "RadFilter":
                break;

            case "RadColumn":
                switch (combo.SelectedItem.Text)
                {
                    case "Member ID":
                        txtshopid.Visible = true;
                        RadFilter.Enabled = false;
                        break;
                    case "First Name":
                        txtsearchtag.Visible = true;
                        break;
                    case "Last Name":
                        txtsearchtag.Visible = true;
                        break;
                    case "Shop":
                        txtsearchtag.Visible = true;
                        break;
                }
                break;


        }


    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        setcriteria("Intosession");
        overrideSearchclick();
    }

    protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
    {
       // resetcriteria();
        criteria.tab = RadTabStrip1.SelectedIndex;
        Session["Smember"] = criteria;
    }

    public void resetcriteria()
    {
        //if (!Page.IsPostBack)
        //{
        //    criteria.tab = 0;
        //}
        //else
        //{
        //    criteria.tab = RadTabStrip1.SelectedIndex;
        //}   
        criteria.tab = 0;
        criteria.Column = 0;
        criteria.Filter = 0;
        criteria.tag = "";
        criteria.ShowAll = false;
        criteria.allowpaging = true;
        criteria.IsCalendar = false;
        criteria.startdate = startdate.Text;
        criteria.enddate = enddate.Text;
        criteria.pagerno = 10;
        criteria.sortmode = 1;
        criteria.sortorder = "Asc";
        Session["Smember"] = criteria;
        Repository.Redirector.Redirect("~/Common/Members/Browse.aspx");
    }

    public void setcriteria(string type)
    {
        if (type == "Intosession")
        {
            criteria.tab = RadTabStrip1.SelectedIndex;
            criteria.Column = RadColumn.SelectedIndex;
            criteria.Filter = RadFilter.SelectedIndex;
            if (RadColumn.SelectedIndex == 1)
            {
                criteria.tag = txtshopid.Text;
            }
            else { criteria.tag = txtsearchtag.Text; }
            if (btnpager.Text == "Show All:Off")
            {
                criteria.ShowAll = false;
                criteria.allowpaging = true;
            }
            else
            {
                criteria.ShowAll = true;
                criteria.allowpaging = false;
            }
            criteria.IsCalendar = Convert.ToBoolean(Rbtncal.SelectedValue);
            criteria.startdate = startdate.Text;
            criteria.enddate = enddate.Text;         
            criteria.pageno = 0;
            criteria.pagerno = 10;

            if (BrowseGridActive.MasterTableView.SortExpressions.GetSortString() != null)
            {
                string gridorder = BrowseGridActive.MasterTableView.SortExpressions[0].SortOrder.ToString();
                string gridExp = BrowseGridActive.MasterTableView.SortExpressions[0].FieldName.ToString();
                if (gridExp == "FirstName")
                {
                    criteria.sortmode = 1;
                }
                else if (gridExp == "LastName")
                {
                    criteria.sortmode = 2;
                }
                if (gridorder == "Descending")
                {
                    criteria.sortorder = "Desc";
                }
                else
                {
                    criteria.sortorder = "Asc";
                }
                
            }
            if (BrowseGridInActive.MasterTableView.SortExpressions.GetSortString() != null)
            {
                string gridorder = BrowseGridInActive.MasterTableView.SortExpressions[0].SortOrder.ToString();
                string gridExp = BrowseGridInActive.MasterTableView.SortExpressions[0].FieldName.ToString();

                if (gridExp == "FirstName")
                {
                    criteria.sortmode = 1;
                }
                else if (gridExp == "LastName")
                {
                    criteria.sortmode = 2;
                }
                if (gridorder == "Descending")
                {
                    criteria.sortorder = "Desc";
                }
                else
                {
                    criteria.sortorder = "Asc";
                }
                
            }

            Session["Smember"] = criteria;
            Repository.Redirector.Redirect("~/Common/Members/Browse.aspx");
        }
        else if (type == "Fromsession")
        {
            txtshopid.Visible = false;
            txtsearchtag.Visible = false;
            RadTabStrip1.SelectedIndex = criteria.tab;
            RadMultiPage1.SelectedIndex = criteria.tab;
            RadColumn.SelectedIndex = criteria.Column;
            RadFilter.SelectedIndex = criteria.Filter;
            
            if (criteria.Column != 0)
            {
                if (criteria.Column == 1)
                {
                    txtshopid.Text = criteria.tag;
                    txtshopid.Visible = true;
                    RadFilter.Enabled = false; 
                }
                else
                {
                    txtsearchtag.Text = criteria.tag;
                    txtsearchtag.Visible = true;
                }
            }
            if (criteria.ShowAll)
            {
                searchpanel.Enabled = false;
                Rbtncal.Enabled = false;
                Radslidingzone3.Enabled = false; 
                btnpager.Text = "Show All:On";              
            }
            else
            {
                searchpanel.Enabled = true;
                btnpager.Text = "Show All:Off";              
            }

            GridSortExpression expression = new GridSortExpression();
            if (criteria.sortmode == 1)
            {
                expression.FieldName = "FirstName";
            }
            else if (criteria.sortmode == 2)
            {
                expression.FieldName = "LastName";
            }
            if (criteria.sortorder == "Desc")
            {
                expression.SortOrder = GridSortOrder.Descending;
            }
            else
            {
                expression.SortOrder = GridSortOrder.Ascending;
            }
            BrowseGridActive.MasterTableView.SortExpressions.AddSortExpression(expression);
            BrowseGridInActive.MasterTableView.SortExpressions.AddSortExpression(expression);            

            Rbtncal.SelectedValue = criteria.IsCalendar.ToString();
            startdate.Text=  criteria.startdate;
            enddate.Text = criteria.enddate;                 
        }

    }

    protected void btnpager_Click(object sender, EventArgs e)
    {

        if (btnpager.Text == "Show All:Off")
        {
            btnpager.Text = "Show All:On";
            RadColumn.SelectedIndex = 0;
            RadFilter.SelectedIndex = 0;
            txtshopid.Text = "";
            txtsearchtag.Text = "";
            Rbtncal.SelectedValue = "False";
            Rbtncal.Enabled = false;
            Radslidingzone3.Enabled = false;
        }
        else
        {
            btnpager.Text = "Show All:Off";
        }
        setcriteria("Intosession");


    }

 
    protected void RadCalendar_DayRender(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
    {
        
    }
    protected void RadCalendar_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
    {
       
        for (int i = 0; i < e.SelectedDates.Count; i++)
        {
            if (i == 0)
            {

                startdate.Text = (e.SelectedDates[i]).Date.ToString("MM/dd/yyyy");
            }
            if (i == 6)
            {
                enddate.Text = (e.SelectedDates[i]).Date.ToString("MM/dd/yyyy");
            }

        }
      
             

    }
    public static int GetNumber(DateTime dtDate)
    {
        System.Globalization.CultureInfo ciGetNumber = System.Globalization.CultureInfo.CurrentCulture;
        int returnNumber = ciGetNumber.Calendar.GetWeekOfYear(dtDate, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return returnNumber;
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        resetcriteria();       
    }

    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnAdd":
                Repository.Redirector.Redirect("~/Common/Members/Add.aspx");
                break;
        }
    }

    protected void overrideSearchclick()
    {
        criteria.tab = 0;
        Session["Smember"] = criteria;
    }
}
