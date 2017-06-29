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

public partial class Common_Members_Edit : SessionTracker
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Members");      
        
        if (!Page.IsPostBack)
        {
            Session["Rid"] = 0;
            Session["Mid"] = 0;
            Repository.Shops.GetShop_cb(Drp_shop);
            txtFee.Text = Math.Round(Repository.Members.GetFee(), 2).ToString(); 
            if (Request.QueryString["Rid"] != null && Request.QueryString["Rid"].ToString() != "0")
            {
                Session["Rid"] = Request.QueryString["Rid"].ToString();
            }
            if (Request.QueryString["Mid"] != null && Request.QueryString["Mid"].ToString() != "0")
            { 
                Session["Mid"] = Request.QueryString["Mid"].ToString();
                refresh("Shop");
                refresh("Authorization");
                refresh("Type"); 
                lblSSNState.Text = "True";
                
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
        //lnkEdit.Visible = false;
        
    }


    protected void SSN_TextChanged(object sender, EventArgs e)
    {
        if (SSNlbl_hidden.Text != SSN.Text)
        {
        string Member_ID = Repository.Members.checkExistingMemberbySSN(SSN.Text);

        if (Member_ID != "0")
        {
            Repository.Struct.CheckMember memberinfo = Repository.Members.Members_Prepopulate(Member_ID, MemberID, firstname, lastname, SSN, DOB, Genderoption, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, OrigHiredDate);
            if (!memberinfo.IsActive)
            {
                    
            }
            else
            {
                SSN.Text = SSNlbl_hidden.Text;

                string msg = "<table>";
                msg += "<tr><td><strong><u>Member Exist!</u></strong></td></tr>";
                msg += "<tr><td><strong>MemberID</strong>: <h6>" + memberinfo.MemberID + "</h6></td></tr>";
                msg += "<tr><td><strong>Name</strong>: <h6>" + memberinfo.Name + "</h6></td></tr>";
                msg += "<tr><td><strong>Shop</strong>: <h6>" + memberinfo.Shop + "</h6></td></tr>";
                msg += "<tr><td><strong>HiredOn</strong>: <h6>" + memberinfo.HiredDate + "</h6></td></tr></table>";

                RadWindowManager.RadAlert(msg, 300, 200, "My Alert", "");
                    lblSSNState.Text = "False";
                }

            }
            else
            {
                lblSSNState.Text = "True";
            }
            
        }
        else
        {
            lblSSNState.Text = "True";
        }

    }

    public void action()
    {
        bool hasValue = false; bool hasAffValue = false;

        hasValue = DOB.SelectedDate.HasValue;
        string strDOB = (hasValue ? DOB.SelectedDate.Value.ToShortDateString() : null);
        hasValue = false;

        hasValue = Hireddate.SelectedDate.HasValue;
        string strhireddate = (hasValue ? Hireddate.SelectedDate.Value.ToShortDateString() : null);

        Repository.Struct.SpResultset resultset = Repository.Members.Members_Update(Session["Rid"].ToString(), MemberID.Text, firstname.Text.ToUpper(), lastname.Text.ToUpper(),initial.Text.ToUpper(), SSN.Text, strDOB, Genderoption.SelectedValue.ToString(), Pri_Address.Text.ToUpper(), Pri_City.Text.ToUpper(), Pri_State.Text.ToUpper(), Pri_Zip.Text, Pri_Zip_Plus4.Text, Sec_Address.Text, Sec_City.Text
     , Sec_State.Text, Sec_Zip.Text, Sec_Zip_Plus4.Text, Pri_Phone.Text, Pri_Fax.Text, Pri_Email.Text, Sec_Phone.Text, Sec_Fax.Text, Sec_Email.Text, Pri_Extn.Text, Sec_Extn.Text, strhireddate);

        if (resultset.Isresult)
        {
            hasValue = false;

            hasValue = Hireddate.SelectedDate.HasValue;
            strhireddate = (hasValue ? Hireddate.SelectedDate.Value.ToShortDateString() : null);

            hasAffValue = AffDate.SelectedDate.HasValue;
            string strAffdate = (hasAffValue ? AffDate.SelectedDate.Value.ToShortDateString() : null);

            resultset = Repository.Members.Update_Members_shop(Session["Mid"].ToString(), Session["Rid"].ToString(), Drp_shop.SelectedValue.ToString(), strhireddate, strAffdate);
            if (resultset.Isresult)
            {
                refresh("Shop");
                refresh("Type");
                RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
                SSNGrid.Rebind();
            }    
        }
    }

    protected void btn_tab1_Click(object sender, EventArgs e)
    {
        if (lblSSNState.Text == "True")
        {
            if (SSNlbl_hidden.Text != SSN.Text)
            {
                string Member_ID = Repository.Members.checkExistingMemberbySSN(SSN.Text);
                if (Member_ID != "0")
                {
                    Repository.Struct.CheckMember memberinfo = Repository.Members.Members_Prepopulate(Member_ID, MemberID, firstname, lastname, SSN, DOB, Genderoption, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email,OrigHiredDate);
                    if (!memberinfo.IsActive)
                    {

                    }
                    else
                    {
                        SSN.Text = SSNlbl_hidden.Text;

                        string msg = "<table>";
                        msg += "<tr><td><strong><u>SSN Exist!</u></strong></td></tr>";
                        msg += "<tr><td><strong>MemberID</strong>: <h6>" + memberinfo.MemberID + "</h6></td></tr>";
                        msg += "<tr><td><strong>Name</strong>: <h6>" + memberinfo.Name + "</h6></td></tr>";
                        msg += "<tr><td><strong>Shop</strong>: <h6>" + memberinfo.Shop + "</h6></td></tr>";
                        msg += "<tr><td><strong>HiredOn</strong>: <h6>" + memberinfo.HiredDate + "</h6></td></tr></table>";
        
                        RadWindowManager.RadAlert(msg, 300, 200, "My Alert", "");
                    }
                }
                else
                {
                    action();
                }
            }
            else
            {
                action();
            }
            
        }
       
        //RadTabStrip1.Tabs[1].Selected = true;
        //RadMultiPage1.PageViews[1].Selected = true;

    }
    #region RateList
    protected void rategrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.Browse_Rate(rategrid, Session["Mid"].ToString());
        }
    }
    protected void rategrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridDataInsertItem insertedItem = (GridDataInsertItem)e.Item;

        string ReferenceID = (insertedItem["Sno"].Controls[3] as Label).Text;
        string Rate = (insertedItem["Rate"].Controls[1] as RadNumericTextBox).Text;
        string Family = (insertedItem["Family"].Controls[1] as RadNumericTextBox).Text;
        string Fringe = (insertedItem["Fringe"].Controls[1] as RadNumericTextBox).Text;
        bool hasValue = (insertedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strEffective_Date = (hasValue ? (insertedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string Effective_Date = strEffective_Date;
        Repository.Struct.SpResultset resultset = Repository.Members.InsertRate(ReferenceID, Isdecimal(Rate), Isdecimal(Family), Isdecimal(Fringe), Effective_Date);
        if (resultset.Isresult)
        {
            rategrid.Rebind();
            e.Item.OwnerTableView.IsItemInserted = false;
        }
    }
    protected static decimal Isdecimal(string var)
    {
        decimal result = 0;
        if (!string.IsNullOrEmpty(var))
        {
            result = Convert.ToDecimal(var);
        }
        return result;
    }
    protected void rategrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {

        GridDataItem updatedItem = (GridDataItem)e.Item;

        string ReferenceID = (updatedItem["Sno"].Controls[1] as Label).Text;
        string Rate = (updatedItem["Rate"].Controls[1] as RadNumericTextBox).Text;
        string Family = (updatedItem["Family"].Controls[1] as RadNumericTextBox).Text;
        string Fringe = (updatedItem["Fringe"].Controls[1] as RadNumericTextBox).Text;
        bool hasValue = (updatedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strEffective_Date = (hasValue ? (updatedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string Effective_Date = strEffective_Date;
        Repository.Struct.SpResultset resultset = Repository.Members.UpdateRate(ReferenceID, Isdecimal(Rate), Isdecimal(Family), Isdecimal(Fringe), Effective_Date);
        if (resultset.Isresult)
        {
            rategrid.Rebind();
        }

    }
    protected void rategrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;
        string ReferenceID = (DeletedItem["Sno"].Controls[1] as Label).Text;
        Repository.Struct.SpResultset resultset = Repository.Members.DeleteRate(ReferenceID);
        if (resultset.Isresult)
        {
            rategrid.Rebind();
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
    #region Benefits
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


            DataSet ds = new DataSet();
            //select existing
            ds = Repository.Members.Select_MemberBenefits(Session["Mid"].ToString());
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
            switch (BenefitType.SelectedValue.ToString())
            {
                case "0"://no benefits
                    break;

                case "1":
                    foreach (GridDataItem Item in Benefits.Items)
                    {
                        CheckBox chkbox = (CheckBox)Item.FindControl("Chk");
                        chkbox.Checked = true;
                        chkbox.Enabled = false;
                    }

                    break;

                case "2":
                    ds = Repository.Benefits.PartialBenefits();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (GridDataItem Item in Benefits.Items)
                        {
                            Label id = (Label)Item.FindControl("lblBenefitID");
                            CheckBox chkbox = (CheckBox)Item.FindControl("Chk");
                            RadNumericTextBox ndate = (RadNumericTextBox)Item.FindControl("WaitingPeriod");
                            RadNumericTextBox ndate1 = (RadNumericTextBox)Item.FindControl("WaitingPeriod1");
                            RadNumericTextBox ndate2 = (RadNumericTextBox)Item.FindControl("WaitingPeriod2");
                            RadNumericTextBox ndate3 = (RadNumericTextBox)Item.FindControl("WaitingPeriod3");

                            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                if (id.Text == ds.Tables[0].Rows[i]["BenefitID"].ToString())
                                {
                                    chkbox.Checked = false;
                                    chkbox.Enabled = false;
                                    ndate.Enabled = false;
                                    ndate.Text = "";
                                    ndate1.Enabled = false;
                                    ndate1.Text = "";
                                    ndate2.Enabled = false;
                                    ndate2.Text = "";
                                    ndate3.Enabled = false;
                                    ndate3.Text = "";
                                    Item.BackColor = ColorTranslator.FromHtml("#E86850");
                                    Item.Style.Value = "text-decoration:line-through;";
                                    break;
                                }
                                else
                                {
                                    chkbox.Enabled = true;
                                }
                            }

                        }
                    }
                    break;
            }
        }
    }
    public string InsXMLBenefit()
    {

        string XmlData = "";
        if (Benefits.Items.Count != 0)
        {
            StringBuilder XMLBenefit = new StringBuilder();
            XMLBenefit.Append("<Root>");
            foreach (GridDataItem dataItem in Benefits.Items)
            {
                CheckBox Chk = (CheckBox)dataItem.FindControl("Chk");
                if (Chk.Checked)
                {
                    Label id = (Label)dataItem.FindControl("lblBenefitID");
                    RadNumericTextBox ndate = (RadNumericTextBox)dataItem.FindControl("WaitingPeriod");
                    RadNumericTextBox ndate1 = (RadNumericTextBox)dataItem.FindControl("WaitingPeriod1");
                    RadNumericTextBox ndate2 = (RadNumericTextBox)dataItem.FindControl("WaitingPeriod2");
                    RadNumericTextBox ndate3 = (RadNumericTextBox)dataItem.FindControl("WaitingPeriod3");
                    XMLBenefit.Append("<Benefit");
                    XMLBenefit.Append(" BenefitID=\"");
                    XMLBenefit.Append(id.Text + "\"");
                    XMLBenefit.Append(" Single_Partial=\"");
                    XMLBenefit.Append(ndate.Text + "\"");
                    XMLBenefit.Append(" Single_FullTime=\"");
                    XMLBenefit.Append(ndate1.Text + "\"");
                    XMLBenefit.Append(" Family_Partial=\"");
                    XMLBenefit.Append(ndate2.Text + "\"");
                    XMLBenefit.Append(" Family_FullTime=\"");
                    XMLBenefit.Append(ndate3.Text + "\"");
                    XMLBenefit.Append("/>");
                }

            }

            XMLBenefit.Append("</Root>");
            XmlData = XMLBenefit.ToString();
        }
        if (XmlData == "<Root></Root>")
        {
            //  XmlData = "";
        }
        return XmlData;
    }
    #endregion
    protected void btn_tab2_Click(object sender, EventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.UpdateApplicableTo(ApplicableTo.SelectedValue.ToString(), Session["Mid"].ToString());
            //update Benefit type
            Repository.Members.UpdateBenefitType(BenefitType.SelectedValue.ToString(), Session["Mid"].ToString());
            if (BenefitType.SelectedValue.ToString() != "0")
            {
                string Members_Benefits = InsXMLBenefit();
                if (Members_Benefits != "")
                {
                    Repository.Struct.SpResultset resultset = Repository.Members.InsertBenefit(Members_Benefits, Session["Mid"].ToString());

                    if (resultset.Isresult)
                    {
                        RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
                    }

                }
                else
                {
                    Repository.Members.NoBenefit(Session["Mid"].ToString());
                RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");  
                } 
            }
            else
            {
                Repository.Members.NoBenefit(Session["Mid"].ToString());
            RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");  
            } 
            //  else { RadWindowManager.RadAlert("No Benefits Selected with Eligibility!", 300, 150, "My Alert", "callBackFn"); }
            // RadWindowManager.RadAlert("Benefits Saved!", 300, 150, "My Alert", "callBackFn");
            //RadTabStrip1.Tabs[3].Selected = true;
            //RadMultiPage1.PageViews[3].Selected = true;

        }
    }
    #region dependency
    protected void dependencygrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
        {
            Repository.Members.Browse_Dependency(dependencygrid, Session["Rid"].ToString());
            Repository.Members.GetDependence_cb(dpdependence, Session["Rid"].ToString());
        }

    }
    protected void dependencygrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridDataInsertItem insertedItem = (GridDataInsertItem)e.Item;

        string ReferenceID = (insertedItem["Sno"].Controls[3] as Label).Text;
        string FirstName = (insertedItem["FirstName"].Controls[1] as TextBox).Text.ToUpper();
        string LastName = (insertedItem["LastName"].Controls[1] as TextBox).Text.ToUpper();
        string Gender = (insertedItem["Gender"].Controls[3] as RadComboBox).SelectedValue;
        bool hasValue = (insertedItem["BirthDate"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strBirthDate = (hasValue ? (insertedItem["BirthDate"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string SSN = (insertedItem["SSN"].Controls[1] as RadMaskedTextBox).Text;
        string Relationship = (insertedItem["Relationship"].Controls[3] as RadComboBox).SelectedValue;
        bool Beneficiary = (insertedItem["Beneficiary"].Controls[3] as CheckBox).Checked;

        Repository.Struct.SpResultset resultset = Repository.Members.InsertDependency(ReferenceID, FirstName, LastName, strBirthDate, SSN, Relationship, Gender,Beneficiary);

        if (resultset.Isresult)
        {
            dependencygrid.Rebind();
            e.Item.OwnerTableView.IsItemInserted = false;

        }

    }
    protected void dependencygrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem updatedItem = (GridDataItem)e.Item;

        string ReferenceID = (updatedItem["Sno"].Controls[1] as Label).Text;
        string FirstName = (updatedItem["FirstName"].Controls[1] as TextBox).Text.ToUpper();
        string LastName = (updatedItem["LastName"].Controls[1] as TextBox).Text.ToUpper();
        string Gender = (updatedItem["Gender"].Controls[3] as RadComboBox).SelectedValue;
        bool hasValue = (updatedItem["BirthDate"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strBirthDate = (hasValue ? (updatedItem["BirthDate"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string SSN = (updatedItem["SSN"].Controls[1] as RadMaskedTextBox).Text;
        string Relationship = (updatedItem["Relationship"].Controls[3] as RadComboBox).SelectedValue;
        bool Beneficiary = (updatedItem["Beneficiary"].Controls[3] as CheckBox).Checked;

        Repository.Struct.SpResultset resultset = Repository.Members.UpdateDependency(ReferenceID, FirstName, LastName, strBirthDate, SSN, Relationship, Gender,Beneficiary);
        if (resultset.Isresult)
        {
            dependencygrid.Rebind();
        }
    }
    protected void dependencygrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        string ReferenceID = (DeletedItem["Sno"].Controls[1] as Label).Text;
        Repository.Struct.SpResultset resultset = Repository.Members.DeleteDependency(ReferenceID);
        if (resultset.Isresult)
        {
            dependencygrid.Rebind();
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
                    
                    Label lblBeneficiary = e.Item.FindControl("lblBeneficiary") as Label;
                    CheckBox chkBeneficiary = e.Item.FindControl("Beneficiary_chk") as CheckBox;
                    if (lblBeneficiary.Text != "" && lblBeneficiary.Text != null)
                    {
                        chkBeneficiary.Checked = Convert.ToBoolean(lblBeneficiary.Text.ToString());
                    }

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
    protected void btn_tab3_Click(object sender, EventArgs e)
    {
        RadTabStrip1.Tabs[4].Selected = true;
        RadMultiPage1.PageViews[4].Selected = true;
    }
    #region Initiation
    protected void onchange_TextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox textbox = (RadNumericTextBox)sender;
        GridDataItem editItem = (GridDataItem)textbox.NamingContainer;
        string ReferenceID = (editItem["Sno"].Controls[3] as Label).Text;

        decimal TotalAmount = Convert.ToDecimal(txtFee.Text);  
        decimal Paidamount = Repository.Members.GetPaidAmount(ReferenceID, "Partial");
        decimal Partialamount = Convert.ToDecimal(textbox.Text);
        decimal outstanding = TotalAmount - Paidamount;
        txtoutstanding.Text = outstanding.ToString();
        txtpartial.Text = Partialamount.ToString();
         
    }
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
                    Initiationgrid.MasterTableView.IsItemInserted = false;  
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
    protected void Initiationgrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridDataInsertItem insertedItem = (GridDataInsertItem)e.Item;
        string ReferenceID = (insertedItem["Sno"].Controls[3] as Label).Text;
        string Date = (insertedItem["Date"].Controls[1] as RadDatePicker).SelectedDate.ToString();
        string Amount = (insertedItem["Amount"].Controls[1] as RadNumericTextBox).Text;

        switch (Feeoption.SelectedValue)
        {
            case "1":
                Repository.Members.InsertFullFee(ReferenceID, Date);
                Initiationgrid.Rebind();
                break;
            case "2":
                Repository.Struct.SpResultset resultset = Repository.Members.InsertPartialFee(ReferenceID, Amount, Date);
                if (resultset.Isresult)
                {
                    Initiationgrid.Rebind();
                    e.Item.OwnerTableView.IsItemInserted = false;
                }
                break;

        }
    }
    protected void Initiationgrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem updatedItem = (GridDataItem)e.Item;

        string ReferenceID = (updatedItem["Sno"].Controls[1] as Label).Text;
        string Date = (updatedItem["Date"].Controls[1] as RadDatePicker).SelectedDate.ToString();
        string Amount = (updatedItem["Amount"].Controls[1] as RadNumericTextBox).Text;

        Repository.Struct.SpResultset resultset = Repository.Members.UpdatePartialFee(ReferenceID, Amount, Date);
        if (resultset.Isresult)
        {
            Initiationgrid.Rebind();
        }
    }
    protected void Initiationgrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        string ReferenceID = (DeletedItem["Sno"].Controls[1] as Label).Text;
        Repository.Struct.SpResultset resultset = Repository.Members.DeletePartialFee(ReferenceID);
        if (resultset.Isresult)
        {
            Initiationgrid.Rebind();
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
                RadNumericTextBox txtAmount = e.Item.FindControl("txtAmount") as RadNumericTextBox;
                TextBox txtMode = e.Item.FindControl("txtMode") as TextBox;
                txtMode.Enabled = false;
                switch (Feeoption.SelectedValue)
                {
                    case "0":
                        break;
                    case "1":
                        txtAmount.Enabled = false;
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
            else
            {
                LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
                LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
                switch (Feeoption.SelectedValue)
                {
                    case "1":
                        btnDelete.Visible = false;
                        break;

                    case "2":
                        btnEdit.Visible = false;
                        break;
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
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Feeoption.SelectedValue.ToString() != "1")
        {
            if (Convert.ToDecimal(txtpartial.Text) > Convert.ToDecimal(txtoutstanding.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }


    #endregion
    protected void btn_tab4_Click(object sender, EventArgs e)
    {
       // RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
        //RadTabStrip1.Tabs[5].Selected = true;
        //RadMultiPage1.PageViews[5].Selected = true;
    }
    protected void btn_tab5_Click(object sender, EventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            bool @Contribution = false;
            bool @AuthorizationLetter = false;

            string path = Server.MapPath("~/Common/Resource/Docs/Authorization/");
            if (Authorize.Checked)
            {
                @Contribution = true;
            }

            if (lblAletter.Text == "True")
            {
                @AuthorizationLetter = true;
                lbtnletter.Visible = true;
            }
            
            if (Authorizationuploader.HasFile)
            {
                @AuthorizationLetter = upload(Authorizationuploader, path, Session["Mid"].ToString());
            }

            Repository.Struct.SpResultset resultset = Repository.Members.Add_upload(Session["Mid"].ToString(), @Contribution, @AuthorizationLetter,false,false);
            if (resultset.Isresult)
            {
                refresh("Authorization"); 
                RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
            }
            
            // RadWindowManager.RadAlert("No Benefits Selected with Eligibility!", 300, 150, "My Alert", "callBackFn");
            //Repository.Redirector.Redirect("~/Common/Members/Edit.aspx?Rid=" + Session["Rid"] + "&Mid=" + Session["Mid"]);
        }
    }
    public bool upload(FileUpload docuploader, string path, string Filename)
    {
        bool result = false;
        if (docuploader.HasFile)
        {

            if (docuploader.PostedFile.ContentLength < 11534336)
            {
                string fileExt = System.IO.Path.GetExtension(docuploader.FileName);

                //root folder
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    docuploader.SaveAs(path + "/" + Filename + ".pdf");
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }

            }
            else
            {

                result = false;

            }
        }
        return result;

    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string filename = dpdependence.SelectedValue.ToString();
        string path = Server.MapPath("~/Common/Resource/Docs/Dependence/");
        bool result = upload(dependenceUpload, path, filename);
        if (result)
        {
            lbluploadmsg.Text = "Uploaded Successfully!";
            Repository.Members.docupload("True", filename);
            dependencygrid.Rebind();
        }
        else { lbluploadmsg.Text = "Upload Failed!"; }
    }
    public void refresh(string type)
    {
        switch (type)
        {
            case "Shop":
                Repository.Members.Load_Information(Session["Mid"].ToString(), MemberID, firstname, lastname, initial, SSN, DOB, Genderoption, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, Drp_shop, Hireddate, ApplicableTo, SSNlbl_hidden, Pri_Extn, Sec_Extn, AffDate, OrigHiredDate);             
                break;
            case "Authorization":
                Repository.Members.Load_Authorization(Session["Mid"].ToString(), lblAletter, lblBcertificate, lblMcertificate, lblauthorize);
                if (lblauthorize.Text == "True")
                {
                    Authorize.Checked = true;
                }
                else { Authorize.Checked = false; }

                if (lblAletter.Text == "True")
                {
                    lbtnletter.NavigateUrl = "~/Common/Resource/Docs/Authorization/" + Session["Mid"].ToString()+ ".pdf";
                    lbtnletter.Visible = true;
                }
                else { lbtnletter.Visible = false; }
                
                break;

            case "Type":
                Repository.Members.Load_Type(Session["Mid"].ToString(), BenefitType, Feeoption);
                BenefitType_SelectedIndexChanged(null, EventArgs.Empty);
                FeeoptionCheck(); 
                break;

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
            case "btnview":
                Repository.Redirector.Redirect("~/Common/Members/view.aspx?Rid=" + Session["Rid"] + "&Mid=" + Session["Mid"]);
                break;
        }
    }
    protected void SSNGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Rid"] != null && Session["Rid"].ToString() != "0")
        {
            Repository.Members.GetSSN(SSNGrid, Session["Rid"].ToString());
        }
    }

    protected void Hireddate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        bool hasValue = Hireddate.SelectedDate.HasValue;
        DateTime DateHired = Hireddate.SelectedDate.Value;
        if (DateHired != null)
        {

            AffDate.SelectedDate = DateHired.AddDays(31);
        }


    }
}
   


   



  
