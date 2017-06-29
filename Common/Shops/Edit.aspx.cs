using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Permissions;



public partial class Common_Shops_Edit : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //menu
        menu.GenerateMenu("Shops");
        //Tabbar
        Repository.Menu.HideTab(RadTabStrip1);
        Repository.Menu.disableControls(ShopID, Name, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, DelegateID, OLPD, OLPH, LPD, LPH, Contract_Start, Contract_End,Pri_Extn,Sec_Extn);
   
        //initialize 
        if (!Page.IsPostBack)
        {            
           Session["Sid"]=0;
           Repository.Delegate.GetDelegate_cb(DelegateID);
          
            if (Request.QueryString["id"] != null)
            {
                Session["Sid"] = Request.QueryString["id"].ToString();
                bool Isvalid = Repository.Shops.Select_Shop(ShopID, Name, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, DelegateID, OLPD, OLPH, LPD, LPH, Contract_Start, Contract_End, Session["Sid"].ToString(), BenefitType,Pri_Extn,Sec_Extn);
                BenefitType_SelectedIndexChanged(null, EventArgs.Empty);
                if (Isvalid)
                {                    
                    contactgrid.Rebind();
                    feegrid.Rebind();
                    Benefits.Rebind();
                }
            }
        }   
      
    }  
    protected void btn_tab1_Click(object sender, EventArgs e)
    {
        bool hasValue = false;
      
        hasValue = LPD.SelectedDate.HasValue;
        string strLPD = (hasValue ? LPD.SelectedDate.Value.ToShortDateString() : null);
        hasValue = LPH.SelectedDate.HasValue;
        string strLPH = (hasValue ? LPH.SelectedDate.Value.ToShortDateString() : null);
        hasValue = Contract_Start.SelectedDate.HasValue;
        string strContract_Start = (hasValue ? Contract_Start.SelectedDate.Value.ToShortDateString() : null);
        hasValue = Contract_End.SelectedDate.HasValue;
        string strContract_End = (hasValue ? Contract_End.SelectedDate.Value.ToShortDateString() : null);

       if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Struct.SpResultset resultset = Repository.Shops.Shops_Update(Session["Sid"].ToString(), ShopID.Text, Name.Text.ToUpper(), Pri_Address.Text.ToUpper(), Pri_City.Text.ToUpper(), Pri_State.Text.ToUpper(), Pri_Zip.Text, Pri_Zip_Plus4.Text, Sec_Address.Text.ToUpper(), Sec_City.Text.ToUpper(), Sec_State.Text.ToUpper(), Sec_Zip.Text, Sec_Zip_Plus4.Text, Pri_Phone.Text, Pri_Fax.Text, Pri_Email.Text, Sec_Phone.Text, Sec_Fax.Text, Sec_Email.Text, Convert.ToInt32(DelegateID.SelectedValue.ToString()), OLPD.Text, OLPH.Text, strLPD, strLPH, strContract_Start, strContract_End,Pri_Extn.Text,Sec_Extn.Text);

            if (resultset.Isresult)
            {               
                RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");  
            }           
            //RadTabStrip1.Tabs[1].Selected = true;
            //RadMultiPage1.PageViews[1].Selected = true;            
        }

    }
    #region ContactList
    protected void drpType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox combo = (RadComboBox)sender;
        GridEditableItem dataItem = combo.NamingContainer as GridEditableItem;
        TextBox txtother = dataItem.FindControl("txtother") as TextBox;
        if (combo.SelectedItem.Text.ToString() == "Other")
        {
            txtother.Visible = true;
        }
        else { txtother.Visible = false; }
    }
    protected void contactgrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.Browse_Contact(contactgrid, Session["Sid"].ToString());
            //contactgrid.MasterTableView.IsItemInserted = true;   
        }

    }
    protected void contactgrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridDataInsertItem insertedItem = (GridDataInsertItem)e.Item;

        string ReferenceID = (insertedItem["Sno"].Controls[3] as Label).Text;
        string Name = (insertedItem["Name"].Controls[1] as TextBox).Text.ToUpper();
        string Type = (insertedItem["Type"].Controls[3] as RadComboBox).SelectedValue;
        string Other = (insertedItem["Type"].Controls[5] as TextBox).Text.ToUpper();
        string Phone = (insertedItem["Phone"].Controls[1] as RadMaskedTextBox).Text;
        string Fax = (insertedItem["Fax"].Controls[1] as RadMaskedTextBox).Text;
        string Mobile = (insertedItem["Mobile"].Controls[1] as RadMaskedTextBox).Text;
        string Email = (insertedItem["Email"].Controls[1] as TextBox).Text;
        string Extn = (insertedItem["Extn"].Controls[1] as TextBox).Text;
        Repository.Struct.SpResultset resultset = Repository.Shops.InsertContact(ReferenceID, Name, Type, Other, Phone, Fax, Mobile, Email, Extn);

        if (resultset.Isresult)
        {
            contactgrid.Rebind();
            e.Item.OwnerTableView.IsItemInserted = false;
        }

    }
    protected void contactgrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem updatedItem = (GridDataItem)e.Item;

        string ReferenceID = (updatedItem["Sno"].Controls[1] as Label).Text;
        string Name = (updatedItem["Name"].Controls[1] as TextBox).Text.ToUpper();
        string Type = (updatedItem["Type"].Controls[3] as RadComboBox).SelectedValue;
        string Other = (updatedItem["Type"].Controls[5] as TextBox).Text.ToUpper();
        string Phone = (updatedItem["Phone"].Controls[1] as RadMaskedTextBox).Text;
        string Fax = (updatedItem["Fax"].Controls[1] as RadMaskedTextBox).Text;
        string Mobile = (updatedItem["Mobile"].Controls[1] as RadMaskedTextBox).Text;
        string Email = (updatedItem["Email"].Controls[1] as TextBox).Text;
        string Extn = (updatedItem["Extn"].Controls[1] as TextBox).Text;
        Repository.Struct.SpResultset resultset = Repository.Shops.UpdateContact(ReferenceID, Name, Type, Other, Phone, Fax, Mobile, Email, Extn);
        if (resultset.Isresult)
        {
            contactgrid.Rebind();
        }
    }
    protected void contactgrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        string ReferenceID = (DeletedItem["Sno"].Controls[1] as Label).Text;
        Repository.Struct.SpResultset resultset = Repository.Shops.DeleteContact(ReferenceID);
        if (resultset.Isresult)
        {
            contactgrid.Rebind();
        }

    }
    protected void contactgrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();

            RadComboBox list = e.Item.FindControl("drpType") as RadComboBox;
            Repository.ContType.GetContactType_cb(list);
            Label lbltype = e.Item.FindControl("lbltype") as Label;
            list.SelectedValue = lbltype.Text;

            if (e.Item.IsInEditMode)
            {
                if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
                {
                    Label lblReferenceID = e.Item.FindControl("lblReferenceID") as Label;
                    lblReferenceID.Text = Session["Sid"].ToString();
                }
                TextBox txtother = e.Item.FindControl("txtother") as TextBox;
                if (list.SelectedItem.Text.ToString() == "Other")
                {
                    txtother.Visible = true;
                }
                else { txtother.Visible = false; }

            }
            else {
                Label lblother = e.Item.FindControl("lblother") as Label;
                if (list.SelectedItem.Text.ToString() == "Other")
                {
                    lblother.Visible = true;
                }
                else { lblother.Visible = false; }

                //Label lblPhone = e.Item.FindControl("lblPhone") as Label;
                //Label lblFax = e.Item.FindControl("lblFax") as Label;
                //Label lblMobile = e.Item.FindControl("lblMobile") as Label;

                //if ((lblPhone.Text.ToString() != null && lblPhone.Text.ToString() != "" && lblPhone.Text.ToString() != "&nbsp;") || (lblFax.Text.ToString() != null && lblFax.Text.ToString() != "" && lblFax.Text.ToString() != "&nbsp;") || (lblMobile.Text.ToString() != null && lblMobile.Text.ToString() != "" && lblMobile.Text.ToString() != "&nbsp;"))
                //{

                //    string strPhone = String.Format("{0:(###)###-####}", Convert.ToInt64(lblPhone.Text));
                //    string strFax = String.Format("{0:(###)###-####}", Convert.ToInt64(lblFax.Text));
                //    string strMobile = String.Format("{0:(###)###-####}", Convert.ToInt64(lblMobile.Text));

                //    lblPhone.Text = strPhone;
                //    lblFax.Text = strFax;
                //    lblMobile.Text = strMobile;
                //}

            }


        }


    }
    #endregion           
    protected void btn_tab2_Click(object sender, EventArgs e)
    {       
        RadTabStrip1.Tabs[2].Selected = true;
        RadMultiPage1.PageViews[2].Selected = true;
    }
    #region FeeList
    protected void feegrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.Browse_Fee(feegrid, Session["Sid"].ToString());
            //feegrid.MasterTableView.IsItemInserted = true;   
        }
    }
    protected void feegrid_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridDataInsertItem insertedItem = (GridDataInsertItem)e.Item;

        string ReferenceID = (insertedItem["Sno"].Controls[3] as Label).Text;
        string Init_FullTime = (insertedItem["Init_FullTime"].Controls[1] as RadNumericTextBox).Text;
        string Init_PartTime = (insertedItem["Init_PartTime"].Controls[1] as RadNumericTextBox).Text;
        string Due_FullTime = (insertedItem["Due_FullTime"].Controls[1] as RadNumericTextBox).Text;
        string Due_PartTime = (insertedItem["Due_PartTime"].Controls[1] as RadNumericTextBox).Text;
        bool hasValue = (insertedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strEffective_Date = (hasValue ? (insertedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string Effective_Date = strEffective_Date;
        Repository.Struct.SpResultset resultset = Repository.Shops.InsertFee(ReferenceID, Isdecimal(Init_FullTime), Isdecimal(Init_PartTime), Isdecimal(Due_FullTime), Isdecimal(Due_PartTime), Effective_Date);
        if (resultset.Isresult)
        {
            feegrid.Rebind();
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
    protected void feegrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {

        GridDataItem updatedItem = (GridDataItem)e.Item;

        string ReferenceID = (updatedItem["Sno"].Controls[1] as Label).Text;
        string Init_FullTime = (updatedItem["Init_FullTime"].Controls[1] as RadNumericTextBox).Text;
        string Init_PartTime = (updatedItem["Init_PartTime"].Controls[1] as RadNumericTextBox).Text;
        string Due_FullTime = (updatedItem["Due_FullTime"].Controls[1] as RadNumericTextBox).Text;
        string Due_PartTime = (updatedItem["Due_PartTime"].Controls[1] as RadNumericTextBox).Text;
        bool hasValue = (updatedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.HasValue;
        string strEffective_Date = (hasValue ? (updatedItem["Effective_Date"].Controls[1] as RadDatePicker).SelectedDate.Value.ToShortDateString() : null);
        string Effective_Date = strEffective_Date;
        Repository.Struct.SpResultset resultset = Repository.Shops.UpdateFee(ReferenceID, Isdecimal(Init_FullTime), Isdecimal(Init_PartTime), Isdecimal(Due_FullTime), Isdecimal(Due_PartTime), Effective_Date);
        if (resultset.Isresult)
        {
            feegrid.Rebind();
        }

    }
    protected void feegrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;
        string ReferenceID = (DeletedItem["Sno"].Controls[1] as Label).Text;
        Repository.Struct.SpResultset resultset = Repository.Shops.DeleteFee(ReferenceID);
        if (resultset.Isresult)
        {
            feegrid.Rebind();
        }
    }
    protected void feegrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();

            if (e.Item.IsInEditMode)
            {
                if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
                {
                    Label lblReferenceID = e.Item.FindControl("lblReferenceID") as Label;
                    lblReferenceID.Text = Session["Sid"].ToString();
                }
            }
        }
    }

    #endregion
    protected void btn_tab3_Click(object sender, EventArgs e)
    {
        RadTabStrip1.Tabs[3].Selected = true;
        RadMultiPage1.PageViews[3].Selected = true;
    }
    #region Benefits
    protected void BenefitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (BenefitType.SelectedItem.ToString() == "Default")
        {
            Benefits.Visible = true;
            lblNA.Visible = false;
        }
        else { Benefits.Visible = false; lblNA.Visible = true; }
        Benefits.Rebind();
    }
    protected void Benefits_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Benefits.Browse_Benefits(Benefits);
        }
    }
    protected void Benefits_DataBound(object sender, EventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            DataSet ds = Repository.Shops.Select_ShopBenefits(Session["Sid"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridDataItem Item in Benefits.Items)
                {
                    CheckBox Chk = (CheckBox)Item.FindControl("Chk");
                    Label id = (Label)Item.FindControl("lblBenefitID");
                    RadNumericTextBox ndate = (RadNumericTextBox)Item.FindControl("Eligibility");

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (id.Text == ds.Tables[0].Rows[i]["BenefitID"].ToString())
                        {
                            Chk.Checked = true;
                            ndate.Text = ds.Tables[0].Rows[i]["Eligibility"].ToString();
                            break;
                        }
                        else
                        {
                            Chk.Checked = false;
                            ndate.Text = "";
                        }
                    }

                }
            }
        }
    }
    public string InsXMLBenefit()
    {

        string XmlData = "";
        if (Benefits.Items.Count != 0)
        {
            bool hasValue = false;
            StringBuilder XMLBenefit = new StringBuilder();
            XMLBenefit.Append("<Root>");
            foreach (GridDataItem dataItem in Benefits.Items)
            {
                CheckBox Chk = (CheckBox)dataItem.FindControl("Chk");
                if (Chk.Checked)
                {
                    Label id = (Label)dataItem.FindControl("lblBenefitID");
                    RadNumericTextBox ndate = (RadNumericTextBox)dataItem.FindControl("Eligibility");
                    XMLBenefit.Append("<Benefit");
                    XMLBenefit.Append(" BenefitID=\"");
                    XMLBenefit.Append(id.Text + "\"");
                    XMLBenefit.Append(" Eligibility=\"");
                    XMLBenefit.Append(ndate.Text + "\"");
                    XMLBenefit.Append("/>");
                }
            }

            XMLBenefit.Append("</Root>");
            XmlData = XMLBenefit.ToString();
        }
        if (XmlData == "<Root></Root>")
        {
            XmlData = "";
        }
        return XmlData;
    }

    #endregion
    protected void btn_tab4_Click(object sender, EventArgs e)
    {

        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            //update Benefit type
            Repository.Shops.UpdateBenefitType(BenefitType.SelectedValue.ToString(), Session["Sid"].ToString());
            if (BenefitType.SelectedValue.ToString() != "0")
            {
                string Shops_Benefits = InsXMLBenefit();
                if (Shops_Benefits != "")
                {
                    Repository.Struct.SpResultset resultset = Repository.Shops.InsertBenefit(Shops_Benefits, Session["Sid"].ToString());
                    if (resultset.Isresult)
                    {
                        RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
                    }
                }
                else
                {
                    Repository.Shops.NoBenefit(Session["Sid"].ToString());
                    RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
                }
            }
            else
            {
                Repository.Shops.NoBenefit(Session["Sid"].ToString());
                RadWindowManager.RadAlert("Information Saved!", 300, 150, "Alert", "");
                
            }
            Benefits.Rebind();
        }
        //RadTabStrip1.Tabs[4].Selected = true;
        //RadMultiPage1.PageViews[4].Selected = true;
    }
    protected void btn_tab5_Click(object sender, EventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            bool result = false;
            string Uppath = Server.MapPath("~/Common/Resource/Contracts/") + Session["Sid"].ToString();
            string webpath = "~/Common/Resource/Contracts/" + Session["Sid"].ToString() + "/";
            string path = Server.MapPath("~/Common/Resource/Contracts/" + Session["Sid"].ToString() + "/");

            string Rootpath = Server.MapPath("~/Common/Resource/Contracts/");

            if (checkfolder(Rootpath))
            {
                result = true;
            }
            else { folder(Rootpath); result = true; }


            if (!Directory.Exists(Uppath))
            {
                Directory.CreateDirectory(Uppath);
            }

            if (Doc_Upload.UploadedFiles.Count > 0)
            {
                foreach (var file in from UploadedFile file in Doc_Upload.UploadedFiles select file)
                {

                    string filesname = file.GetName();
                    string filesExt = file.GetExtension();
                    string str_Result = Repository.Shops.Shops_Upload(filesname, filesExt, Session["Sid"].ToString());

                    string destname = Uppath + "/" + str_Result + filesExt;

                    if (!File.Exists(destname))
                    {
                        file.SaveAs(destname);
                    }

                }
            }
        }
        Doc_Grid.Rebind();
        //Repository.Redirector.Redirect("~/Common/Shops/Browse.aspx");
    }
    protected bool checkfolder(string path)
    {
        bool result = false;

        if (Directory.Exists(path))
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    protected bool folder(string path)
    {
        bool result = false;

        Directory.CreateDirectory(path);
        if (Directory.Exists(path))
        {
            result = true;
        }
        else { result = false; }

        return result;
    }


    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnBack":
                Repository.Redirector.Redirect("~/Common/Shops/Browse.aspx");
                break;
            case "btnBrowse":
                Session["Sshop"] = 0;
                Repository.Redirector.Redirect("~/Common/Shops/Browse.aspx");
                break;
            case "btnview":
                Repository.Redirector.Redirect("~/Common/Shops/view.aspx?id=" + Session["Sid"]);
                break;
        }
    }
    protected void Doc_Grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.GetShops_Doc(Session["Sid"].ToString(), Doc_Grid);
        }
    }
    protected void Doc_Grid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblExt = (Label)e.Item.FindControl("FileExt");
            Label lblReference = (Label)e.Item.FindControl("ReferenceID");
            HyperLink url = (HyperLink)e.Item.FindControl("File");
            ImageMap Img = (ImageMap)e.Item.FindControl("ImgHyper");

            string myurl = "~/Common/Resource/Contracts/" + lblReference.Text + "/" + url.ToolTip + lblExt.Text;
            if (File.Exists(Server.MapPath(myurl)))
            {
                url.CssClass = "link";
                url.NavigateUrl = myurl;
                Img.ImageUrl = "~/images/Fileimage.jpg";

                //switch (lblExt.Text)
                //{
                //    case ".doc":
                //        Img.ImageUrl = "~/images/Doc.jpg";
                //        break;
                //    case ".docx":
                //        Img.ImageUrl = "~/images/Doc.jpg";
                //        break;
                //    case ".xls":
                //        Img.ImageUrl = "~/images/excel.jpg";
                //        break;
                //    case ".xlsx":
                //        Img.ImageUrl = "~/images/excel.jpg";
                //        break;
                //    case ".pdf":
                //        Img.ImageUrl = "~/images/pdf.jpg";
                //        break;
                //    case ".txt":
                //        Img.ImageUrl = "~/images/txt.jpg";
                //        break;
                //    case ".dwg":
                //        Img.ImageUrl = "~/images/dwg.jpg";
                //        break;
                //    case ".png":
                //        Img.ImageUrl = "~/images/Png.png";
                //        break;
                //    case ".tiff":
                //        Img.ImageUrl = "~/images/tiff.jpg";
                //        break;
                //    case ".jpg":
                //        Img.ImageUrl = "~/images/jpeg.jpg";
                //        break;
                //    default:
                //        Img.ImageUrl = "~/images/Fileimage.jpg";
                //        break;
                //}
                url.Target = "_new";
            }
            else
            {
                url.CssClass = "dislnk";
                url.NavigateUrl = "#";
                url.Enabled = false;
            }


        }
    }
    protected void Doc_Grid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
            {
                string RecID = e.CommandArgument.ToString();
                Repository.Shops.DeleteShops_Doc(Session["Sid"].ToString(), RecID);
                if (e.Item is GridDataItem)
                {
                    Label lblExt = (Label)e.Item.FindControl("FileExt");
                    Label lblReference = (Label)e.Item.FindControl("ReferenceID");
                    HyperLink url = (HyperLink)e.Item.FindControl("File");
                    ImageMap Img = (ImageMap)e.Item.FindControl("ImgHyper");

                    string sourceurl = Server.MapPath("~/Common/Resource/Contracts/" + lblReference.Text + "/" + url.ToolTip + lblExt.Text);
                    string destfolderurl = Server.MapPath("~/Common/Resource/Archive/Contracts/" + lblReference.Text);
                    string desturl = Server.MapPath("~/Common/Resource/Archive/Contracts/" + lblReference.Text + "/" + url.ToolTip + lblExt.Text);
                    bool result = false;
                    if (checkfolder(destfolderurl))
                    {
                        result = true;
                    }
                    else { folder(destfolderurl); result = true; }
                    
                    if (File.Exists(sourceurl))
                    {
                        File.Move(sourceurl, desturl);
                        
                    }
                }
            
               
            }
            
        }
    }
}
