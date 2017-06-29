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


public partial class Common_Provider_Browse : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Providers");
    }

    protected void BrowseProviders_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        Repository.Providers.Browse_Providers(BrowseProviders);
    }
    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnAdd":
                Repository.Redirector.Redirect("~/Common/Provider/Add.aspx");
                break;
        }
    }
    protected void BrowseProviders_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"];

        Repository.Struct.SpResultset resultset = Repository.Providers.Remove_Providers(id);


    }
}
