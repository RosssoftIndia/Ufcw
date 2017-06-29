using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Dashboard_Home : SessionTracker 
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        menu.GenerateMenu("Home");
        UpdateTile(); 
     
    }
    protected void Timer_Tick(object sender, EventArgs e)
    {
        UpdateTile();
    }

   public void UpdateTile()
   {
       Repository.dashboard.dash_Members(Member_statistics,Member_Chart);
       Repository.dashboard.dash_Shops(Shop_statistics,Shop_Chart);
       Repository.dashboard.dash_Providers(Provider_statistics,Provider_Chart);
       Repository.dashboard.dash_Benefits(Benefit_statistics,Benefit_Chart);   
   }
}
