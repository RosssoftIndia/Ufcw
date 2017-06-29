using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Benefits_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Benefits");
        if (!Page.IsPostBack)
        {
            Repository.Providers.GetProviders_cb(Drp_Provider);
            Session["Rid"] = 0;

            if (Request.QueryString["Rid"] != null && Request.QueryString["Rid"].ToString() != "0")
            {
                Session["Rid"] = Request.QueryString["Rid"].ToString();
            }
            bool Isvalid = Repository.Benefits.Select_Benefits(Convert.ToInt32(Session["Rid"].ToString()),txtname, Drp_Provider, txtdescription);
        }
    }
    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
           
            case "btnBrowse":
                Session["Sshop"] = 0;
                Repository.Redirector.Redirect("~/Common/Benefits/Browse.aspx");
                break;
            case "btnEdit":
                Repository.Redirector.Redirect("~/Common/Benefits/Edit.aspx?Rid=" + Session["Rid"]);
                break;
        }
    }
}
