using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing; 

public partial class Common_Members_View : SessionTracker
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //menu
        menu.GenerateMenu("Members");
        //SetActionbar
        Repository.Menu.ActionBar(null, null, btnEdit, null);     

        if (!Page.IsPostBack)
        {
            Session["Rid"] = 0;
            Session["Mid"] = 0;
            Repository.Shops.GetShop_cb(Drp_shop);
            if (Request.QueryString["Rid"] != null && Request.QueryString["Rid"].ToString() != "0")
            {
                Session["Rid"] = Request.QueryString["Rid"].ToString();
            }
            if (Request.QueryString["Mid"] != null && Request.QueryString["Mid"].ToString() != "0")
            { 
                Session["Mid"] = Request.QueryString["Mid"].ToString();
                Repository.Members.View_Members(Session["Mid"].ToString(), MemberID, firstname, lastname,initial, SSN, DOB, Genderoption, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, lblAletter, lblBcertificate, lblMcertificate, lblauthorize, Drp_shop, Hireddate, BenefitType, Feeoption,ApplicableTo,Pri_Extn,Sec_Extn,AffDate,OrigHiredDate);
                BenefitType_SelectedIndexChanged(null, EventArgs.Empty);
                FeeoptionCheck(); 
                if (lblauthorize.Text == "True")
                {
                    Authorize.Checked = true;
                }
                else { Authorize.Checked = false; }

                if (lblAletter.Text == "True")
                {
                    lbtnletter.NavigateUrl = "~/Common/Resource/Docs/Authorization/" + Session["Mid"].ToString()+".pdf";
                }
                else { lbtnletter.Visible = false; }
                        
            }
           

  
        }
        if (Request.QueryString["Rid"] != null && Request.QueryString["Rid"].ToString() != "0")
        {
                int count = Repository.Members.GetSSNCount(Session["Rid"].ToString());
                if (count > 0)
                {
                    SSNGrid.Style["display"] = "block";
                }
                else
                {
                    SSNGrid.Style["display"] = "none";
                }
        }
        //Load links
        //lnkAdd.HRef = "Add.aspx";
        //lnkBrowse.HRef = "Browse.aspx";
        //lnkEdit.HRef = "Edit.aspx?Rid=" + Session["Rid"] + "&Mid=" + Session["Mid"];

        
    }
    #region RateList
    protected void rategrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.Browse_Rate(rategrid, Session["Mid"].ToString());
        }
    }  
    protected void rategrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();

            if (e.Item.IsInEditMode)
            {
                if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
                {
                    Label lblReferenceID = e.Item.FindControl("lblReferenceID") as Label;
                    lblReferenceID.Text = Session["Mid"].ToString();
                }
            }
        }
    }
    #endregion
    #region Benefit
    protected void BenefitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BenefitType.SelectedItem.ToString() == "No Benefits")
        {
            Benefits.Visible = false;
            lblNA.Visible = true;
            Ratesection.Visible = false; 
        }
        else { Benefits.Visible = true; lblNA.Visible = false; Ratesection.Visible = true; }
        Benefits.Rebind();
        rategrid.Rebind(); 
    }
    protected void Benefits_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Benefits.BenefitsByShop(Benefits, Session["Mid"].ToString());
        }
    }
    protected void Benefits_DataBound(object sender, EventArgs e)
    {

        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            DataSet ds = Repository.Members.Select_MemberBenefits(Session["Mid"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridDataItem Item in Benefits.Items)
                {
                    CheckBox Chk = (CheckBox)Item.FindControl("Chk");
                    Label id = (Label)Item.FindControl("lblBenefitID");
                    RadNumericTextBox ndate = (RadNumericTextBox)Item.FindControl("WaitingPeriod");
                    RadNumericTextBox ndate1 = (RadNumericTextBox)Item.FindControl("WaitingPeriod1");
                    RadNumericTextBox ndate2 = (RadNumericTextBox)Item.FindControl("WaitingPeriod2");
                    RadNumericTextBox ndate3 = (RadNumericTextBox)Item.FindControl("WaitingPeriod3");

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (id.Text == ds.Tables[0].Rows[i]["BenefitID"].ToString())
                        {
                            Chk.Checked = true;
                            ndate.Text = ds.Tables[0].Rows[i]["Single_Partial"].ToString();
                            ndate1.Text = ds.Tables[0].Rows[i]["Single_FullTime"].ToString();
                            ndate2.Text = ds.Tables[0].Rows[i]["Family_Partial"].ToString();
                            ndate3.Text = ds.Tables[0].Rows[i]["Family_FullTime"].ToString();
                            break;
                        }
                        else
                        {
                            Chk.Checked = false;
                            ndate.Text = "";
                            ndate1.Text = "";
                            ndate2.Text = "";
                            ndate3.Text = "";
                        }
                    }

                }          
            }
        }
    }   
    #endregion
    #region dependency
    protected void dependencygrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
        {
            Repository.Members.Browse_Dependency(dependencygrid, Session["Rid"].ToString());           
        }

    }   
    protected void dependencygrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();

            RadComboBox list = e.Item.FindControl("drpRelationship") as RadComboBox;
            Repository.Members.GetRelationship_cb(list);
            Label lbltype = e.Item.FindControl("lblRelationship") as Label;
            list.SelectedValue = lbltype.Text;


            RadComboBox gender = e.Item.FindControl("drpGender") as RadComboBox;
            Label lblgender = e.Item.FindControl("lblGender") as Label;
            gender.SelectedValue = lblgender.Text;


            HyperLink lnk = e.Item.FindControl("lbtncertificate") as HyperLink;
            Label lblupload = e.Item.FindControl("lblupload") as Label;
            if (lblupload.Text == "True")
            {
                lnk.Visible = true;
            }
            else { lnk.Visible = false; }

            if (e.Item.IsInEditMode)
            {
                if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
                {
                    Label lblReferenceID = e.Item.FindControl("lblReferenceID") as Label;
                    lblReferenceID.Text = Session["Rid"].ToString();


                }
               
            }
            else
            {
                //Label lblSSN = e.Item.FindControl("lblSSN") as Label;
                //if (lblSSN.Text.ToString() != null && lblSSN.Text.ToString() != "" && lblSSN.Text.ToString() != "&nbsp;")
                //{
                //    double dbl = Double.Parse(lblSSN.Text.ToString());
                //    string str = String.Format("{0:###-##-####}", dbl);
                //    lblSSN.Text = str;
                //}
            }
        }


    }
    #endregion
    #region Initiation   
    protected void Initiationgrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        switch (Feeoption.SelectedValue)
        {
            case "0":
                break;
            case "1":
                if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
                {
                    Repository.Members.Browse_Initiation(Initiationgrid, Session["Mid"].ToString(), "Full");
                    Initiationgrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
                break;

            case "2":
                if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
                {
                    if (Repository.Members.IsDisable(Session["Mid"].ToString()))
                    {
                        Initiationgrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    }
                    else { Initiationgrid.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top; }
                    Repository.Members.Browse_Initiation(Initiationgrid, Session["Mid"].ToString(), "Partial");
                }
                break;


        }


    }   
    protected void Initiationgrid_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            //serial no
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();


            if (e.Item.IsInEditMode)
            {
                TextBox txtMode = e.Item.FindControl("txtMode") as TextBox;
                txtMode.Enabled = false;
                switch (Feeoption.SelectedValue)
                {
                    case "0":
                        break;
                    case "1":
                        txtMode.Text = "Full";
                        break;
                    case "2":
                        txtMode.Text = "Partial";
                        break;
                }

                if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
                {
                    Label lblReferenceID = e.Item.FindControl("lblReferenceID") as Label;
                    lblReferenceID.Text = Session["Mid"].ToString();


                }
            }
           
        }

    }
    protected void Feeoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        Repository.Members.UpdateInitiationType(Feeoption.SelectedValue.ToString(), Session["Mid"].ToString());      
        Repository.Members.NoFee(Session["Mid"].ToString());
        waivertab.Visible = false;
        switch (Feeoption.SelectedValue)
        {
            case "0":
                Initiationgrid.Visible = false;
                break;

            case "1":
                Initiationgrid.Visible = true;
                Repository.Members.FullFee(Session["Mid"].ToString());
                Initiationgrid.Rebind();
                break;
            case "2":
                Initiationgrid.Visible = true;
                Initiationgrid.Rebind();
                break;

            case "3":
                waivertab.Visible = true;
                Initiationgrid.Visible = false;
                break;
        }




    }
    protected void FeeoptionCheck()
    {
        waivertab.Visible = false;
        switch (Feeoption.SelectedValue)
        {
            case "0":
                Initiationgrid.Visible = false;
                break;

            case "1":
                Initiationgrid.Visible = true;
                Initiationgrid.Rebind();
                break;
            case "2":
                Initiationgrid.Visible = true;
                Initiationgrid.Rebind();
                break;
            case "3":
                waivertab.Visible = true;
                Initiationgrid.Visible = false;
                break;
        }

    }
    #endregion

    protected void BrowsegridNotes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.GetNotes(BrowsegridNotes, Session["Mid"].ToString());
        }
    }
    protected void SSNGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
        {
            Repository.Members.GetSSN(SSNGrid, Session["Rid"].ToString());
        }
    }

    protected void BrowsegridHistory_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
        {
            Repository.Members.HiredMembersbyID(BrowsegridHistory, Session["Rid"].ToString());
        }
    }
    protected void BrowsegridHistory_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            string strstatus = item["Status"].Text.ToString();
            if (strstatus == "Active")
            {
                item.BackColor = ColorTranslator.FromHtml("#7CFC00");
               
            }
            else
            {
                item.BackColor = ColorTranslator.FromHtml("#E86850");
            }
        }
    }

    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnBack":
                Repository.Redirector.Redirect("~/Common/Members/Browse.aspx");
                break;
            case "btnBrowse":
                Session["Smember"] = 0;
                Repository.Redirector.Redirect("~/Common/Members/Browse.aspx");
                break;
            case "btnEdit":
                Repository.Redirector.Redirect("~/Common/Members/Edit.aspx?Rid=" + Session["Rid"] + "&Mid=" + Session["Mid"]);
                break;
        }
    }
}
   


   



  
