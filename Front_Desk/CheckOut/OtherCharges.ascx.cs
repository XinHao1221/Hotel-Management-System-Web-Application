using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Front_Desk.CheckOut
{
    public partial class OtherCharges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["ServiceCharges"] = new List<ServiceCharges>();

                PopupCover.Visible = false;
                PopupDelete.Visible = false;
            }

            checkIsEmpty();
        }

        private void checkIsEmpty()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            if (serviceCharges.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void btnSaveServiceCharges_Click(object sender, EventArgs e)
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            String charges = txtCharges.Text;

            // If user doesn't enter equipment price
            if (charges == "")
            {
                charges = "0";  // Set it to zero
            }

            // Add to Equipment class
            serviceCharges.Add(new ServiceCharges() { service = txtService.Text, charges = Convert.ToDouble(charges) });

            RepeaterServiceCharges.DataSource = serviceCharges;
            RepeaterServiceCharges.DataBind();

            txtCharges.Text = null;
            txtCharges.Text = null;

            lblNoItemFound.Visible = false;
        }

        protected void IBDeleteEquipment_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get service details for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String service = (item.FindControl("lblService") as Label).Text;
            String charges = (item.FindControl("lblCharges") as Label).Text;

            // Set index of current selected item to ViewState
            ViewState["ItemIndex"] = itemIndex;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Service: " + service + "<br />" +
                "Charges: " + charges + "<br /><br />";

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

            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            serviceCharges.RemoveAt(itemIndex - 1);

            RepeaterServiceCharges.DataSource = serviceCharges;
            RepeaterServiceCharges.DataBind();

            checkIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }
    }
}