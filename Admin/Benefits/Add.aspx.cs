using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Benefits_Add : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Benefits");
        if (!Page.IsPostBack)
        {          
            Repository.Providers.GetProviders_cb(Drp_Provider);
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        Repository.Struct.SpResultset resultset = Repository.Benefits.Add_Benefits(txtname.Text, txtdescription.Text, Convert.ToInt32(Drp_Provider.SelectedValue.ToString()));
        if (resultset.Isresult)
        {

        }
    }
}
