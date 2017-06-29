using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

public partial class Common_Submenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
       
    }

    public void GenerateMenu(string title)
    {
        sub1.Attributes.Remove("class");
        switch (title)
        {
            case "GPP":
                sub1.Attributes.Add("class", "active");
                break;
            case "HealthBenefit":
                sub2.Attributes.Add("class", "active");
                break;
            case "AflacCensus":
                sub3.Attributes.Add("class", "active");
                break;
            case "AflacNewadd":
                sub4.Attributes.Add("class", "active");
                break;
            case "Hartford":
                sub5.Attributes.Add("class", "active");
                break;
            case "Magnacare":
                //sub6.Attributes.Add("class", "active");
                break;
            case "Percapita-Active":
                sub7.Attributes.Add("class", "active");
                break;
            case "Percapita-Employer":
                sub8.Attributes.Add("class", "active");
                break;
            case "Percapita-Outgoing":
                sub9.Attributes.Add("class", "active");
                break;
            case "SSN":
                sub10.Attributes.Add("class", "active");
                break;
            case "ShopLabel":
                sub11.Attributes.Add("class", "active");
                break;
            case "MembersLabel":
                sub12.Attributes.Add("class", "active");
                break;
            case "Census Hartford":
                sub13.Attributes.Add("class", "active");
                break;
        }
        
    }

    
    
}
