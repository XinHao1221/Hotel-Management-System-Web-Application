/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Front_Desk.Guest
{
    public partial class AddPreference : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["PreferenceList"] = new List<Preference>();

                PopupCover.Visible = false;
                PopupDelete.Visible = false;
            }

            checkIsEmpty();
        }

        private void checkIsEmpty()
        {
            List<Preference> preferenceList = (List<Preference>)Session["PreferenceList"];

            if (preferenceList.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void btnSavePreference_Click(object sender, EventArgs e)
        {
            List<Preference> equipmentList = (List<Preference>)Session["PreferenceList"];

            // Get current date
            DateTime dateTimeNow = DateTime.Now;
           

            // Add to Equipment class
            equipmentList.Add(new Preference() { preference = txtPreference.Text, date = dateTimeNow.ToShortDateString()});

            RepeaterPreferences.DataSource = equipmentList;
            RepeaterPreferences.DataBind();

            txtPreference.Text = null;

            lblNoItemFound.Visible = false;
        }

        protected void IBDeletePreference_Click(object sender, ImageClickEventArgs e)
        {
            // When user click on delete icon
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get item no for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String preferece = (item.FindControl("lblPreference") as Label).Text;
            String date = (item.FindControl("lblDate") as Label).Text;

            lblPopupDeleteContent.Text = "Preference: " + preferece + "<br /><br />";
                //"Date Added: " + date + "<br /><br />";

            ViewState["ItemIndex"] = itemIndex;

            PopupDelete.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int itemIndex = int.Parse(ViewState["ItemIndex"].ToString());

            List<Preference> preferneceList = (List<Preference>)Session["PreferenceList"];

            preferneceList.RemoveAt(itemIndex - 1);

            RepeaterPreferences.DataSource = preferneceList;
            RepeaterPreferences.DataBind();

            checkIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void RepeaterPreferences_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Format date
            Label lblDate = e.Item.FindControl("lblDate") as Label;
            lblDate.Text = Convert.ToDateTime(lblDate.Text).ToShortDateString();
        }
    }
}