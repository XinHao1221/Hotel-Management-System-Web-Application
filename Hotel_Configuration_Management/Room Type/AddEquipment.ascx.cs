﻿using System;
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
                if (!IsPostBack)
                {
                    Session["EquipmentList"] = new List<Equipment>();
                }

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

            lblNoItemFound.Visible = false;
        }

        protected void IBDeleteEquipment_Click(object sender, ImageClickEventArgs e)
        {
            // When user click on delete icon
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get item no for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String equipmentName = (item.FindControl("lblEquipmentName") as Label).Text;
            String fineCharges = (item.FindControl("lblFineCharges") as Label).Text;

            lblPopupDeleteContent.Text = "Equipment: " + equipmentName + "<br />" +
                "Fine Charhes: " + fineCharges + "<br /><br />";

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