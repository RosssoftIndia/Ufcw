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

public partial class Common_Shops_Document : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Doc_Grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            if (Request.QueryString["Rid"].ToString() != null && Request.QueryString["Rid"].ToString() != "" && Request.QueryString["Rid"].ToString() != "0")
            {
                Repository.Shops.GetShops_Doc(Request.QueryString["Rid"].ToString(), Doc_Grid);
            }
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
}
