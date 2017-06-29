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

public partial class Common_Members_Notes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void submit_Click(object sender, EventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.Notes(txtNotes.Text, chkFlag.Checked, Session["Mid"].ToString());
            BrowsegridNotes.Rebind();
            txtNotes.Text = ""; 
            chkFlag.Checked = false;
        }
      
    }
    protected void BrowsegridNotes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["Mid"] != null && Session["Mid"].ToString() != "0")
        {
            Repository.Members.GetNotes(BrowsegridNotes, Session["Mid"].ToString());
        }
    }

    protected void BrowsegridNotes_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridDataItem DeletedItem = (GridDataItem)e.Item;
        Label lblRecord = (Label)DeletedItem.FindControl("lblNoteRecord");
        //int id = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RecordID"];

        string ReferenceID = lblRecord.Text.ToString();
        Repository.Members.DeleteNotes(ReferenceID);
        BrowsegridNotes.Rebind();

    }
    protected void Update_btn_Click(object sender, EventArgs e)
    {
        if (BrowsegridNotes.Items.Count != 0)
        {

            foreach (GridDataItem dataItem in BrowsegridNotes.Items)
            {
                Label lblRecordID = (Label)dataItem.FindControl("lblNoteRecord");
                CheckBox chkflag = (CheckBox)dataItem.FindControl("chk_Flag");

                Repository.Members.UpdateNotes(lblRecordID.Text.ToString(), chkflag.Checked);

            }
        }
    }
    protected void BrowsegridNotes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridDataItem item = (GridDataItem)BrowsegridNotes.SelectedItems[0];
        string name = item["Note"].Text;
        txtNotes.Text = name;

    }
    protected void clear_Click(object sender, EventArgs e)
    {
        txtNotes.Enabled = true;
        submit.Enabled = true;
        txtNotes.Text = "";
        chkFlag.Checked = false;
        chkFlag.Enabled = true;
    }
}
