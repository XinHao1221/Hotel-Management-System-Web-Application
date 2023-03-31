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
using System.Configuration;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public partial class CheckInConfirmation : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        string reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Save Check In";

            reservationID = en.decryption(Request.QueryString["ID"]);

            setReservationDetails();

            setAddOnsFacilityToRepeater();

            calculateTotalCharges();

            // Navigate to previous page
            // If back button clicked
            this.formBtnBack.OnClientClick = "javascript:window.history.go(-1);return false;";
        }

        private void setReservationDetails()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Set the reservation details into label
            lblGuestName.Text = getGuestName(reservationDetails.guestID);
            lblCheckInDate.Text = reservationDetails.checkInDate;
            lblCheckOutDate.Text = reservationDetails.checkOutDate;
            lblDurationOfStay.Text = reservationUtility.getdurationOfStay(reservationDetails.checkInDate, reservationDetails.checkOutDate).ToString();

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();
        }

        private string getGuestName(string guestID)
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getGuestName = "SELECT Name FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ID", guestID);

            string guestName = (string)cmdGetGuestName.ExecuteScalar();

            conn.Close();

            return guestName;
        }

        private void setAddOnsFacilityToRepeater()
        {
            List<ReservationFacility> reservationFacilities = getAddOnFacility();

            if(reservationFacilities.Count != 0)
            {
                // Set data to facility repeater
                RepeaterRentedFacility.DataSource = reservationFacilities;
                RepeaterRentedFacility.DataBind();

                lblNoItemFound.Visible = false;
            }
            else
            {
                lblNoItemFound.Visible = true;
            }
            
        }

        private void calculateTotalCharges()
        {
            List<ReservationFacility> reservationFacilities = getAddOnFacility();

            double totalPayment = 0;

            for(int i = 0; i < reservationFacilities.Count; i++)
            {
                totalPayment += reservationFacilities[i].subTotal;
            }

            if(totalPayment != 0.00)
            {
                // Display total
                lblTotal.Text = string.Format("{0:0.00}", totalPayment);

                // Calculate grand total
                totalPayment += calcTaxCharges(totalPayment);

                // Display grand total
                lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment);
            }
            else
            {   // If no facility add on

                // Disable the text box
                txtReferenceNo.Enabled = false;
                ddlPaymentMethod.Enabled = false;

                // Set the total to 0
                lblTotal.Text = "0.00";
                lblTax.Text = "0.00";
                lblGrandTotal.Text = "0.00";
            }
            
        }

        private double calcTaxCharges(double totalPayment)
        {
            // Calculate tax

            double tax = 0;

            // Get the tax rate from application variable
            tax = totalPayment * (double)Application["TaxRate"];

            // Display total tax charges
            lblTax.Text = string.Format("{0:0.00}", tax);

            return tax;
        }

        private List<ReservationFacility> getAddOnFacility()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            List<ReservationFacility> addOns = new List<ReservationFacility>();

            // Find the newly rented facility 
            for(int i = 0; i < reservationFacilities.Count; i++)
            {
                if(reservationFacilities[i].reservationFacilityID == "")
                {
                    addOns.Add(reservationFacilities[i]);
                }
            }

            return addOns;
        }

        private void saveRentedFacility()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = getAddOnFacility();

            String addRentedFacility;

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Get last group id
            int group = getLastGroupNumber();

            // Total rented facility record
            for (int i = 0; i < reservationFacilities.Count; i++)
            {

                if (reservationFacilities[i].priceType == "Per Night" || reservationFacilities[i].priceType == "Per Person")
                {
                    string[] date = reservationUtility.getReservationDate(reservationFacilities[i].rentDate, reservationFacilities[i].returnDate);

                    int rentedDuration = date.Length;

                    // Duration of rent
                    for (int j = 0; j < rentedDuration; j++)
                    {
                        // Rented quantity
                        for (int q = 0; q < reservationFacilities[i].quantity; q++)
                        {
                            // get next reservationRoomID
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationFacilityID", "ReservationFacility", "RF");

                            //double facilityRentedPrice = reservationFacilities[i].price / rentedDuration / reservationFacilities[i].quantity;
                            double facilityRentedPrice = reservationFacilities[i].price;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date and quantity
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID, @Group)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", reservationFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", reservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@Group", group + 1);

                            int success = cmdAddRentedFacility.ExecuteNonQuery();

                            conn.Close();
                        }

                    }

                    group++;
                }
                else if (reservationFacilities[i].priceType == "Per Reservation")
                {
                    // Get list of date from duration of stay
                    string[] date = reservationUtility.getReservationDate(reservationDetails.checkInDate, reservationDetails.checkOutDate);

                    // Duration of rent
                    for (int j = 0; j < date.Length; j++)
                    {
                        // quantity
                        for (int q = 0; q < reservationFacilities[i].quantity; q++)
                        {
                            // get next reservationRoomID
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationFacilityID", "ReservationFacility", "RF");

                            // Calculate rent price for the facility
                            //double facilityRentedPrice = reservationFacilities[i].price / reservationFacilities[i].quantity;
                            double facilityRentedPrice = reservationFacilities[i].price;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID, @Group)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", reservationFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", reservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@Group", group + 1);

                            int success = cmdAddRentedFacility.ExecuteNonQuery();

                            // Close connection
                            conn.Close();
                        }
                    }

                    group++;

                }
            }
        }

        private int getLastGroupNumber()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getLastGroupNumber = "SELECT TOP 1 [Group] FROM ReservationFacility RF WHERE RF.ReservationID LIKE @ReservationID ORDER BY[Group] DESC";

            SqlCommand cmdGetGroupNumber = new SqlCommand(getLastGroupNumber, conn);

            cmdGetGroupNumber.Parameters.AddWithValue("@ReservationID", reservationID);

            int group = 0;

            try
            {
               group  = (int)cmdGetGroupNumber.ExecuteScalar();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }

            conn.Close();

            return group;
        }

        private void saveRentedRoom()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = reservationDetails.reservedRoom;

            for(int i = 0; i < reservedRooms.Count; i++)
            {
                // Open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                string addRentedRoom = "UPDATE ReservationRoom " +
                                        "SET RoomID = @RoomID " +
                                        "WHERE ReservationRoomID LIKE @ReservationRoomID";

                SqlCommand cmdAddRentedRoom = new SqlCommand(addRentedRoom, conn);

                cmdAddRentedRoom.Parameters.AddWithValue("@RoomID", reservedRooms[i].roomID);
                cmdAddRentedRoom.Parameters.AddWithValue("@ReservationRoomID", reservedRooms[i].reservationRoomID);

                int success = cmdAddRentedRoom.ExecuteNonQuery();

                conn.Close();
            }
        }

        private void updateReservationStatus()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string updateReservationStatus = "UPDATE Reservation SET Status = 'Checked In' " +
                                                "WHERE ReservationID LIKE @ID";

            SqlCommand cmdUpdateReservationStatus = new SqlCommand(updateReservationStatus, conn);

            cmdUpdateReservationStatus.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdUpdateReservationStatus.ExecuteNonQuery();

            conn.Close();


        }

        private void savePayment()
        {
            // get next PaymentID
            String nextPaymentID = idGenerator.getNextID("PaymentID", "Payment", "P");

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String addPayment = "INSERT INTO Payment VALUES (@PaymentID, @PaymentMethod, @ReferenceNo, @Amount, @ReservationID, @Date)";

            SqlCommand cmdAddPayment = new SqlCommand(addPayment, conn);

            cmdAddPayment.Parameters.AddWithValue("@PaymentID", nextPaymentID);
            cmdAddPayment.Parameters.AddWithValue("@PaymentMethod", ddlPaymentMethod.SelectedValue);
            cmdAddPayment.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text);
            cmdAddPayment.Parameters.AddWithValue("@Amount", Convert.ToDecimal(lblGrandTotal.Text));
            cmdAddPayment.Parameters.AddWithValue("@ReservationID", reservationID);
            cmdAddPayment.Parameters.AddWithValue("@Date", todaysDate);

            int i = cmdAddPayment.ExecuteNonQuery();

            conn.Close();

        }

        protected void RepeaterRentedFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            // Check if any facility rented
            if(reservationFacilities.Count > 0)
            {
                saveRentedFacility();

                // If there is no any pending payment, skip this 
                if (ddlPaymentMethod.Enabled == true)
                {
                    savePayment();
                }
            }

            saveRentedRoom();

            updateReservationStatus();

            // Show popup Success Message
            lblPopupCheckedIn.Text = "Check In Success.";
            PopupCheckIn.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckIn.aspx");
        }
    }
}