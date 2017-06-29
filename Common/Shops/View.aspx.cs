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


public partial class Common_Shops_View : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //menu
        menu.GenerateMenu("Shops");
        //SetActionbar
        Repository.Menu.ActionBar(null, null,btnEdit, null);     


        //initialize 
        if (!Page.IsPostBack)
        {
            Session["Sid"] = 0;
             Repository.Delegate.GetDelegate_cb(DelegateID);
         
            if(Request.QueryString["id"] !=null)
            {
                Session["Sid"] = Request.QueryString["id"].ToString();                
                //Initialize Vales
                bool Isvalid = Repository.Shops.Select_Shop(ShopID, Name, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Sec_Address, Sec_City, Sec_State, Sec_Zip, Sec_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email, Sec_Phone, Sec_Fax, Sec_Email, DelegateID, OLPD, OLPH, LPD, LPH, Contract_Start, Contract_End, Session["Sid"].ToString(), BenefitType, Pri_Extn, Sec_Extn);
                BenefitType_SelectedIndexChanged(null, EventArgs.Empty);
              if (Isvalid)
              {                 
                  contactgrid.Rebind();
                  feegrid.Rebind();                
              }
            }  
        }  
      
    }


    protected void contactgrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.Browse_Contact(contactgrid, Session["Sid"].ToString());
            //contactgrid.MasterTableView.IsItemInserted = true;   
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

            }
            else
            {
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

    protected void feegrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.Browse_Fee(feegrid, Session["Sid"].ToString());
            //feegrid.MasterTableView.IsItemInserted = true;   
        }
    }   
    protected void feegrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Label lblsno = e.Item.FindControl("lblSno") as Label;
            lblsno.Text = (e.Item.ItemIndex + 1).ToString();
        }
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
    #endregion

    protected void BrowsegridNotes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Sid"] != null && Session["Sid"].ToString() != "0")
        {
            Repository.Shops.GetNotes(BrowsegridNotes, Session["Sid"].ToString());
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
            case "btnEdit":
                Repository.Redirector.Redirect("~/Common/Shops/Edit.aspx?id=" + Session["Sid"]);
                break;
        }
    }
}
