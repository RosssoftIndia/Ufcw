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


public partial class Common_Benefits_Browse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Benefits");
    }

    protected void BrowseBenefits_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        Repository.Benefits.Browse_Benefits(BrowseBenefits);      
    }
    protected void Pageaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (btn.ID)
        {
            case "btnAdd":
                Repository.Redirector.Redirect("~/Common/Benefits/Add.aspx");
                break;
        }
    }
    protected void BrowseBenefits_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;

        int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"];

        string ReferenceID = id.ToString();
        Repository.Struct.SpResultset resultset = Repository.Benefits.Remove_Benefits(id);
        

    }
}
