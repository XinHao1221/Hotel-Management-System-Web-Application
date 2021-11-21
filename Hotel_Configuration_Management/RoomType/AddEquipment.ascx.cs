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


namespace Hotel_Management_System.Hotel_Configuration_Management.Room_Type
{
    public partial class AddEquipment : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Session["EquipmentList"] = new List<Equipment>();

                PopupCover.Visible = false;
                PopupDelete.Visible = false;
            }

            checkIsEmpty();
        }

        private void checkIsEmpty()
        {
            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];

            if (equipmentList.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void btnSaveEquipment_Click(object sender, EventArgs e)
        {
            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];

            String fineCharges = txtEquipmentPrice.Text;

            // If user doesn't enter equipment price
            if(fineCharges == "")
            {
                fineCharges = "0";  // Set it to zero
            }

            // Add to Equipment class
            equipmentList.Add(new Equipment() { equipmentName = txtEquipment.Text, fineCharges = Convert.ToDouble(fineCharges) });

            Repeater1.DataSource = equipmentList;
            Repeater1.DataBind();

            txtEquipment.Text = null;
            txtEquipmentPrice.Text = null;

            lblNoItemFound.Visible = false;
        }

        protected void IBDeleteEquipment_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get equipment name for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String equipmentName = (item.FindControl("lblEquipmentName") as Label).Text;
            String fineCharges = (item.FindControl("lblFineCharges") as Label).Text;

            // Set index of current selected item to ViewState
            ViewState["ItemIndex"] = itemIndex;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Equipment: " + equipmentName + "<br />" +
                "Fine Charges: " + fineCharges + "<br /><br />";

            PopupCover.Visible = true;
            PopupDelete.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int itemIndex = int.Parse(ViewState["ItemIndex"].ToString());

            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];

            equipmentList.RemoveAt(itemIndex - 1);

            Repeater1.DataSource = equipmentList;
            Repeater1.DataBind();

            checkIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }
    }
}