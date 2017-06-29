using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Provider_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Providers");
        if (!Page.IsPostBack)
        {
           
            Session["Rid"] = 0;

            if (Request.QueryString["Rid"] != null && Request.QueryString["Rid"].ToString() != "0")
            {
                Session["Rid"] = Request.QueryString["Rid"].ToString();
            }
            bool Isvalid = Repository.Providers.Select_Provider(Convert.ToInt32(Session["Rid"].ToString()), txtname, Pri_Address, Pri_City, Pri_State, Pri_Zip, Pri_Zip_Plus4, Pri_Phone, Pri_Fax, Pri_Email);
        }
    }
    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {

            case "btnBrowse":
                Session["Sshop"] = 0;
                Repository.Redirector.Redirect("~/Common/Provider/Browse.aspx");
                break;
            case "btnEdit":
                Repository.Redirector.Redirect("~/Common/Provider/Edit.aspx?Rid=" + Session["Rid"]);
                break;
        }
    }
}
