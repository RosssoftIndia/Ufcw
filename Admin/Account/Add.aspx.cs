using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Account_Add : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        menu.GenerateMenu("Account");
        if (!Page.IsPostBack)
        {           
            Repository.Roles.GetRole_cb(Drp_Role);
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        bool hasValue = Birthdate.SelectedDate.HasValue;
        string strBirthdate = (hasValue ? Birthdate.SelectedDate.Value.ToShortDateString() : null);
        Repository.Struct.SpResultset resultset = Repository.Users.Register(txtfirstname.Text, txtlastname.Text, txtemail.Text, txtusername.Text, Repository.En_De_cryptor.ComputeHash(txtpassword.Text, "", null), strBirthdate, Convert.ToInt32(Drp_Role.SelectedValue.ToString()));
        if (resultset.Isresult)
        {

        }
    }
}
