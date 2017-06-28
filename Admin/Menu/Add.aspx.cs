using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Menu_Add : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Menu");
        if (!Page.IsPostBack)
        {            
            Repository.Roles.GetRole_cb(Drp_Role);           
        }
    }
   
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        Repository.Struct.SpResultset resultset = Repository.Menu.Add_MenuItem(txtname.Text, txturl.Text, txtpriority.Text, Convert.ToInt32(Drp_Role.SelectedValue.ToString()));
       if (resultset.Isresult)
       {

       }

    }
    protected void Drp_Role_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (Drp_Role.SelectedValue.ToString() != "0")
        {
            txtpriority.Text = Repository.Menu.GetPriority(Drp_Role.SelectedValue.ToString()).ToString();
        }
    }
   
}
