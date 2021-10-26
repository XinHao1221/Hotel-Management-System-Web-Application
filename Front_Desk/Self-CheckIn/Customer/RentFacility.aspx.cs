using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotel_Management_System.Front_Desk.CheckIn;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.Reservation;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class RentFacility : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        string reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Rent Facility";

            if (!IsPostBack)
            {
                displayRentedFacility();

                setFacility();
            }
        }


        private void displayRentedFacility() {

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            RepeaterRentedFacility.DataSource = reservation.rentedFacility;
            RepeaterRentedFacility.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectRoom.aspx?ID=" + en.encryption(reservationID));
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("Completed.aspx?ID=" + en.encryption(reservationID));
            
        }

        private void setFacility()
        {

            // Get facility availability from Session 
            List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

            // Clear all items
            ddlFacilityName.Items.Clear();
            ddlFacilityQty.Items.Clear();

            // Set first item
            ddlFacilityName.Items.Add(new ListItem("-- Please Select --"));

            for (int i = 0; i < availableFacility.Count; i++)
            {
                if (availableFacility[i].availableQty > 0)
                {
                    ddlFacilityName.Items.Add(new ListItem(availableFacility[i].facilityName, availableFacility[i].facilityID));
                }
            }

        }

        protected void ddlFacilityName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            if (ddlFacilityName.SelectedIndex != 0)
            {
                // Get facility availability from Session 
                List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

                for (int i = 0; i < availableFacility.Count; i++)
                {
                    if (availableFacility[i].facilityID == ddlFacilityName.SelectedValue)
                    {
                        ddlFacilityQty.Items.Clear();
                        for (int j = 1; j <= availableFacility[i].availableQty; j++)
                        {
                            ddlFacilityQty.Items.Add(new ListItem(j.ToString()));
                        }

                        if (availableFacility[i].priceType == "Per Reservation")
                        {
                            txtRentDate.Text = reservationUtility.formatDate(reservation.checkInDate);
                            txtReturnDate.Text = reservationUtility.formatDate(reservation.checkOutDate);

                            PNFacilityRentedDate.Visible = false;
                            CVFacilityRentedDate.Enabled = false;
                        }
                        else
                        {
                            txtRentDate.Text = reservationUtility.formatDate(reservation.checkInDate);
                            txtReturnDate.Text = reservationUtility.formatDate(reservation.checkOutDate);

                            PNFacilityRentedDate.Visible = false;
                            CVFacilityRentedDate.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                ddlFacilityQty.Items.Clear();
                PNFacilityRentedDate.Visible = false;
                CVFacilityRentedDate.Enabled = false;
            }

        }

        protected void txtRentDate_TextChanged(object sender, EventArgs e)
        {
            // Run the comparevalidator
            CVFacilityRentedDate.Validate();

            // Check if rent date & return date is valid
            if (!CVFacilityRentedDate.IsValid)
            {

                // Style if error occurred
                txtRentDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtRentDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtReturnDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtReturnDate.Style["background-color"] = "rgb(255, 240, 240)";

            }
            else
            {
                // Style if no error occurred
                txtRentDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtRentDate.Style["background-color"] = "none";

                txtReturnDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtReturnDate.Style["background-color"] = "none";
            }
        }

        protected void txtReturnDate_TextChanged(object sender, EventArgs e)
        {
            // Run the comparevalidator
            CVFacilityRentedDate.Validate();

            // Check if rent date & return date is valid
            if (!CVFacilityRentedDate.IsValid)
            {

                // Style if error occurred
                txtRentDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtRentDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtReturnDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtReturnDate.Style["background-color"] = "rgb(255, 240, 240)";

            }
            else
            {
                // Style if no error occurred
                txtRentDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtRentDate.Style["background-color"] = "none";

                txtReturnDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtReturnDate.Style["background-color"] = "none";
            }
        }

        protected void btnAddFacility_Click(object sender, EventArgs e)
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Get facility availability from Session 
            List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            if(facilityAvailable())
            {
                for (int i = 0; i < availableFacility.Count; i++)
                {
                    if (availableFacility[i].facilityID == ddlFacilityName.SelectedValue)
                    {
                        double subTotal;

                        if (availableFacility[i].priceType == "Per Reservation")
                        {
                            subTotal = availableFacility[i].price * int.Parse(ddlFacilityQty.SelectedValue);
                        }
                        else
                        {
                            int durationOfStay = reservationUtility.getdurationOfStay(txtRentDate.Text, txtReturnDate.Text);

                            subTotal = availableFacility[i].price * (int.Parse(ddlFacilityQty.SelectedValue) * durationOfStay);
                        }

                        ReservationFacility rf = new ReservationFacility("", availableFacility[i].facilityID, int.Parse(ddlFacilityQty.SelectedValue),
                                                                        availableFacility[i].price,
                                                                        txtRentDate.Text,
                                                                        txtReturnDate.Text);


                        reservationFacilities.Add(rf);
                    }
                }

                // Refresh the facility list
                RepeaterRentedFacility.DataSource = reservationFacilities;
                RepeaterRentedFacility.DataBind();
            }

            // Reset rent facility form
            ddlFacilityName.SelectedIndex = 0;
            ddlFacilityQty.Items.Clear();

            txtRentDate.Text = "";
            txtReturnDate.Text = "";

            PNFacilityRentedDate.Visible = false;
            CVFacilityRentedDate.Enabled = false;

            lblNoItemFound.Visible = false;

            // Set the updated reservationFacilities list into ReservationDetails
            reservationDetails.rentedFacility = reservationFacilities;

        }

        public int getFacilityRentedQty(string facilityID, string date)
        {

            // Query to get the quantity of facility have reserved by other guest
            String getReservedFacility = "SELECT FacilityID, COUNT(FacilityID) AS RentedQty " +
                                        "FROM ReservationFacility " +
                                        "WHERE DateRented LIKE @Date AND FacilityID LIKE @FacilityID " +
                                        "GROUP BY FacilityID ";

            SqlCommand cmdGetReservedFacility = new SqlCommand(getReservedFacility, conn);

            cmdGetReservedFacility.Parameters.AddWithValue("@FacilityID", facilityID);
            cmdGetReservedFacility.Parameters.AddWithValue("@Date", date);

            // Hold the data read from database
            SqlDataReader sdr = cmdGetReservedFacility.ExecuteReader();

            if (sdr.Read())
            {
                return sdr.GetInt32(sdr.GetOrdinal("RentedQty"));
            }

            // If not any reservation found under the category
            return 0;
        }

        private Boolean facilityAvailable()
        {
            string facilityID = ddlFacilityName.SelectedValue;

            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Get facility availability from Session 
            List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            int availableQty = 0;

            // Minus remaining facility's quantity with newly added facility
            for (int i = 0; i < availableFacility.Count; i++)
            {
                if(availableFacility[i].facilityID == facilityID)
                {
                    availableQty = availableFacility[i].availableQty;

                    for(int j = 0; j < reservationFacilities.Count; j++)
                    {
                        if(reservationFacilities[j].facilityID == facilityID && reservationFacilities[j].reservationFacilityID == "")
                        {
                            availableQty -= reservationFacilities[j].quantity;
                        }
                    }
                }
            }

            availableQty -= int.Parse(ddlFacilityQty.Text);

            Boolean available = true;

            
            if (availableQty < 0)
            {
                PopupFacilityNoAvailable.Visible = true;
                PopupCover.Visible = true;

                return false;
            }
            else
            {
                // Check facility availability from database
                DateTime checkInDate = Convert.ToDateTime(reservationDetails.checkInDate);
                DateTime checkOutDate = Convert.ToDateTime(reservationDetails.checkOutDate);

                int durationOfStay = reservationUtility.getdurationOfStay(checkInDate.ToShortDateString(), checkOutDate.ToShortDateString());

                for(int i = 0; i < durationOfStay; i++)
                {
                    conn = new SqlConnection(strCon);
                    conn.Open();

                    available = (availableQty - getFacilityRentedQty(facilityID, reservationUtility.formatDate(checkInDate.AddDays(i).ToShortDateString()))) >= 0;

                    conn.Close();
                }

                if(available == false)
                {
                    PopupFacilityNoAvailable.Visible = true;
                    PopupCover.Visible = true;

                    return false;
                }
            }

            return true;
        }

        protected void IBDeleteRentedFacility_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get facility info for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String facilityName = (item.FindControl("lblFacilityName") as Label).Text;
            String rentDate = (item.FindControl("lblRentDate") as Label).Text;
            String returnDate = (item.FindControl("lblReturnDate") as Label).Text;

            // Set facilityID to ViewState
            ViewState["ItemIndex"] = itemIndex;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Facility: " + facilityName + "<br />" +
                "Rent Date: " + rentDate + "<br/>" + "Return Date:" + returnDate + "<br /><br />";

            PopupDelete.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnPopupDelete_Click(object sender, EventArgs e)
        {

            int itemIndex = int.Parse(ViewState["ItemIndex"].ToString());

            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            reservationFacilities.RemoveAt(itemIndex - 1);

            // Set the ReservationFacility into repeater
            RepeaterRentedFacility.DataSource = reservationFacilities;
            RepeaterRentedFacility.DataBind();

            checkRentedFacilityIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;

        }

        private void checkRentedFacilityIsEmpty()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Get ReservationFacility
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            if (reservationFacilities.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void RepeaterRentedFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // If the Facility is not newly added, then hide delete button
            Label lblReservationFacilityID = e.Item.FindControl("lblReservationFacilityID") as Label;

            // Get control's reference
            ImageButton IBDeleteRentedFacility = e.Item.FindControl("IBDeleteRentedFacility") as ImageButton;

            if (lblReservationFacilityID.Text != "")
            {
                IBDeleteRentedFacility.Visible = false;
            }

            // Format date according to user's computer
            // Get control's refernce
            Label lblRentDate = e.Item.FindControl("lblRentDate") as Label;
            Label lblReturnDate = e.Item.FindControl("lblReturnDate") as Label;

            // Format date base on date format on user's computer
            DateTime formatedRentDate = Convert.ToDateTime(lblRentDate.Text);
            DateTime formatedReturnDate = Convert.ToDateTime(lblReturnDate.Text);

            // Display the formated date
            lblRentDate.Text = formatedRentDate.ToShortDateString();
            lblReturnDate.Text = formatedReturnDate.ToShortDateString();
        }

        protected void RepeaterRentedFacility_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            PopupFacilityNoAvailable.Visible = false;
            PopupCover.Visible = false;
        }
    }
}