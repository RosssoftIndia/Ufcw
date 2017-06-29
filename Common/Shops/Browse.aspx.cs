using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using Telerik.Web.UI;


public partial class Common_Shops_Browse : SessionTracker 
{
    Repository.Struct.Searchshop criteria = new Repository.Struct.Searchshop();   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //session
        Session["Smember"] = 0;
        //menu
        menu.GenerateMenu("Shops");
        //SetActionbar
        Repository.Menu.ActionBar(null, btnAdd, null, null);     
        


         
        if (!Page.IsPostBack)
        {
            if (Session["Sshop"] != null && Session["Sshop"].ToString() != "0")
            {
                criteria = (Repository.Struct.Searchshop)Session["Sshop"];
                setcriteria("Fromsession");

            }
            else
            {
                resetcriteria();
            }
        }
        else
        {
            if (Session["Sshop"] != null && Session["Sshop"].ToString() != "0")
            {
                criteria = (Repository.Struct.Searchshop)Session["Sshop"];               
            }
        }
        
    }

    protected void BrowseGridSearch_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridSearch.AllowPaging = criteria.allowpaging;
        BrowseGridSearch.PageSize = criteria.pagerno;
        BrowseGridSearch.CurrentPageIndex = criteria.pageno;
        Repository.Shops.Browse_Shop(BrowseGridSearch, "", criteria);
        //string srot = BrowseGridActive.MasterTableView.SortExpressions[0].SortOrder.ToString();
        //string slt = srot;
    }
    protected void BrowseGridSearch_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            //HyperLink url = (HyperLink)e.Item.FindControl("link");
            //url.CssClass = "link";
            //string myurl = "~/Common/Resource/Contracts/" + url.Text + ".pdf";
            //if (File.Exists(Server.MapPath(myurl)))
            //{
            //    url.CssClass = "link";
            //    url.NavigateUrl = myurl;
            //    url.Target = "_new";
            //}
            //else
            //{
            //    url.CssClass = "dislnk";
            //    url.NavigateUrl = "#";
            //    url.Enabled = false;
            //}

            GridDataItem item = (GridDataItem)e.Item;
            string strTxt = item["OpenMonths"].Text.ToString();
            if (strTxt == "-")
            {
                item["OpenMonths"].Text = "";
            }

            //GridBar
            HyperLink View = (HyperLink)e.Item.FindControl("lnkview");
            HyperLink Edit = (HyperLink)e.Item.FindControl("lnkedit");
            LinkButton Delete = (LinkButton)e.Item.FindControl("btndelete");
            Repository.Menu.GridBar(View, Edit, Delete);

            //Notes Section
            Image ImgNote = (Image)e.Item.FindControl("Img_Note");
            //Label lblUnique = (Label)e.Item.FindControl("lblUnique");
            bool Notes = (item["RecordID"].Text != "&nbsp;") ? Repository.Shops.GetNotes_byShop(item["RecordID"].Text) : false;
            if (Notes)
            {
                ImgNote.Visible = true;
            }
            else
            {
                ImgNote.Visible = false;
            }

        }



        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Sshop"] = criteria;

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
        Session["Sshop"] = criteria;
    }
    protected void BrowseGridSearch_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("Name".Equals(e.CommandArgument))
        {
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";
                    Session["Sshop"] = criteria;
                    break;
            }
        }
    }
   

   
    protected void BrowseGridActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridActive.AllowPaging = criteria.allowpaging;
        BrowseGridActive.PageSize = criteria.pagerno;
        BrowseGridActive.CurrentPageIndex = criteria.pageno;
        Repository.Shops.Browse_Shop(BrowseGridActive, "false",criteria);
        //string srot = BrowseGridActive.MasterTableView.SortExpressions[0].SortOrder.ToString();
        //string slt = srot;
    }
    protected void BrowseGridActive_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            
            //HyperLink url = (HyperLink)e.Item.FindControl("link");
            //url.CssClass = "link";
            //string myurl = "~/Common/Resource/Contracts/" + url.Text + ".pdf";
            //if (File.Exists(Server.MapPath(myurl)))
            //{
            //    url.CssClass = "link";
            //    url.NavigateUrl = myurl;
            //    url.Target = "_new";
            //}
            //else
            //{
            //    url.CssClass = "dislnk";
            //    url.NavigateUrl = "#";
            //    url.Enabled = false;
            //}
            
            GridDataItem item = (GridDataItem)e.Item;
            string strTxt = item["OpenMonths"].Text.ToString();
            if (strTxt == "-")
            {
                item["OpenMonths"].Text = "";
            }

            //GridBar
            HyperLink View = (HyperLink)e.Item.FindControl("lnkview");
            HyperLink Edit = (HyperLink)e.Item.FindControl("lnkedit");
            LinkButton Delete = (LinkButton)e.Item.FindControl("btndelete");
            Repository.Menu.GridBar(View, Edit, Delete);

            //Notes Section
            Image ImgNote = (Image)e.Item.FindControl("Img_Note");
            //Label lblUnique = (Label)e.Item.FindControl("lblUnique");
            bool Notes =(item["RecordID"].Text != "&nbsp;") ?  Repository.Shops.GetNotes_byShop(item["RecordID"].Text) : false;
            if (Notes)
            {
                ImgNote.Visible = true;
            }
            else
            {
                ImgNote.Visible = false;
            }

        }


      
        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Sshop"] = criteria;          

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
    protected void BrowseGridActive_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"];

        string ReferenceID = id.ToString();
        Repository.Struct.SpResultset resultset = Repository.Shops.Shops_InActive(ReferenceID);
        if (resultset.Isresult)
        {
            BrowseGridActive.Rebind();
            BrowseGridInActive.Rebind();
        }

    }
    protected void BrowseGridActive_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        criteria.pageno = e.NewPageIndex;
        Session["Sshop"] = criteria;
    }
    protected void BrowseGridActive_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("Name".Equals(e.CommandArgument))
        {
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";
                    Session["Sshop"] = criteria;
                    break;
            }
        }
    }
    protected void BrowseGridActive_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = (HyperLink)e.Item.FindControl("link");
            editLink.Attributes["href"] = "javascript:void(0);";
            editLink.Attributes["onclick"] = String.Format("return ShowActiveForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"], e.Item.ItemIndex);
        }
    }

    protected void BrowseGridInActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BrowseGridInActive.AllowPaging = criteria.allowpaging;
        BrowseGridInActive.PageSize = criteria.pagerno;
        BrowseGridInActive.CurrentPageIndex = criteria.pageno;
        Repository.Shops.Browse_Shop(BrowseGridInActive, "true", criteria);  
    }
    protected void BrowseGridInActive_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //HyperLink url = (HyperLink)e.Item.FindControl("link");
            //string myurl = "~/Common/Resource/Contracts/" + url.Text + ".pdf";
            //if (File.Exists(Server.MapPath(myurl)))
            //{
            //    url.CssClass = "link";
            //    url.NavigateUrl = myurl;
            //    url.Target = "_new";
            //}
            //else
            //{
            //    url.CssClass = "dislnk";
            //    url.NavigateUrl = "#";
            //    url.Enabled = false;
            //}
            GridDataItem item = (GridDataItem)e.Item;
            string strTxt = item["OpenMonths"].Text.ToString();
            if (strTxt == "-")
            {
                item["OpenMonths"].Text = "";
            }

            //GridBar
            HyperLink View = (HyperLink)e.Item.FindControl("lnkview");           
            Repository.Menu.GridBar(View, null, null);      

            //Notes Section
            Image ImgNote = (Image)e.Item.FindControl("Img_Note");
            //Label lblUnique = (Label)e.Item.FindControl("lblUnique");
//            bool Notes = Repository.Shops.GetNotes_byShop(item["RecordID"].Text);
            bool Notes = (item["RecordID"].Text != "&nbsp;") ? Repository.Shops.GetNotes_byShop(item["RecordID"].Text) : false;
            if (Notes)
            {
                ImgNote.Visible = true;
            }
            else
            {
                ImgNote.Visible = false;
            }
        }

        if (e.Item is GridPagerItem)
        {
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

            criteria.pagerno = Convert.ToInt32(PageSizeCombo.SelectedValue.ToString());
            Session["Sshop"] = criteria;

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
        Session["Sshop"] = criteria;
    }
    protected void BrowseGridInActive_SortCommand(object source, GridSortCommandEventArgs e)
    {
        if ("Name".Equals(e.CommandArgument))
        {
            switch (e.NewSortOrder)
            {
                case GridSortOrder.None:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Ascending:
                    criteria.sortorder = "Asc";
                    Session["Sshop"] = criteria;
                    break;
                case GridSortOrder.Descending:
                    criteria.sortorder = "Desc";
                    Session["Sshop"] = criteria;
                    break;
            }
        }
    }
    protected void BrowseGridInActive_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            HyperLink editLink = (HyperLink)e.Item.FindControl("link");
            editLink.Attributes["href"] = "javascript:void(0);";
            editLink.Attributes["onclick"] = String.Format("return ShowInActiveForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"], e.Item.ItemIndex);
        }
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
                switch(combo.SelectedItem.Text) 
                {
                    case "Shop ID":
                        txtshopid.Visible = true;                     
                        RadFilter.Enabled = false; 
                        break;
                    case "Name":
                        txtsearchtag.Visible = true;
                        break;
                    case "Delegate":
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
        Session["Sshop"] = criteria;
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
        criteria.pagerno = 10;
        criteria.sortorder = "Asc";
        Session["Sshop"] = criteria;
        Repository.Redirector.Redirect("~/Common/Shops/Browse.aspx");
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
            if (BrowseGridActive.MasterTableView.SortExpressions.GetSortString() != null)
            {
                string sort = BrowseGridActive.MasterTableView.SortExpressions[0].SortOrder.ToString();

                if(sort=="Descending")
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
                string sort = BrowseGridInActive.MasterTableView.SortExpressions[0].SortOrder.ToString();

                if (sort == "Descending")
                {
                    criteria.sortorder = "Desc";
                }
                else
                {
                    criteria.sortorder = "Asc";
                }
            }
            Session["Sshop"] = criteria;
            Repository.Redirector.Redirect("~/Common/Shops/Browse.aspx");
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
                btnpager.Text = "Show All:On";
              // criteria.allowpaging = false;
            }
            else
            {
                searchpanel.Enabled = true;
                btnpager.Text = "Show All:Off";
               // criteria.allowpaging = true;
            }              
           
            GridSortExpression expression = new GridSortExpression();
            expression.FieldName = "Name";
            if (criteria.sortorder == "Desc")
            {

                expression.SortOrder = GridSortOrder.Descending;
                BrowseGridActive.MasterTableView.SortExpressions.AddSortExpression(expression);
                BrowseGridInActive.MasterTableView.SortExpressions.AddSortExpression(expression);
            }
            else
            {
                expression.SortOrder = GridSortOrder.Ascending;
                BrowseGridActive.MasterTableView.SortExpressions.AddSortExpression(expression);
                BrowseGridInActive.MasterTableView.SortExpressions.AddSortExpression(expression);
            }
          
           
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
        }
        else
        {
            btnpager.Text = "Show All:Off";
        }
        setcriteria("Intosession");

       
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
                Repository.Redirector.Redirect("~/Common/Shops/Add.aspx");
                break;         
        }
    }

    protected void overrideSearchclick()
    {
        criteria.tab = 0;
        Session["Sshop"] = criteria;
    }
    





//    protected void Shop_Delete_Click(object sender, EventArgs e)
//    {       

//        LinkButton lbl = (LinkButton)sender;
//        GridDataItem item = (GridDataItem)lbl.NamingContainer;
//        Label lbl1 = (Label)item.FindControl("hiddenfield");    
//        String shopID = lbl1.Text;
//        Repository.Shops.ShopDelete(shopID);
              
//}
  


}
