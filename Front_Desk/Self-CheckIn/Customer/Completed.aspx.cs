using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class Completed : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        string reservationID;

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            displayReservedRoom();

            displayRentedFacility();

            displayAmountDue();

            calculateTotalCharges();
        }

        private void displayReservedRoom()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];


            RepeaterReservedRoom.DataSource = reservation.reservedRoom;
            RepeaterReservedRoom.DataBind();
        }

        private void displayRentedFacility()
        {

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            if(reservation.rentedFacility.Count > 0)
            {
                RepeaterRentedFacility.DataSource = reservation.rentedFacility;
                RepeaterRentedFacility.DataBind();

                lblNoFacility.Visible = false;
            }
            else
            {
                RepeaterRentedFacility.DataSource = null;
                RepeaterRentedFacility.DataBind();

                lblNoFacility.Visible = true;
            }
            
        }

        protected void RepeaterReservedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();

            // Check if any extra bed rented
            Label lblExtraBed = e.Item.FindControl("lblExtraBed") as Label;
            Label lblTrueFalse = e.Item.FindControl("lblTrueFalse") as Label;

            string temp = lblExtraBed.Text;

            if (lblExtraBed.Text != "-1")
            {
                lblTrueFalse.Text = "True";
            }
            else
            {
                lblTrueFalse.Text = "";
            }
        
        }

        protected void RepeaterRentedFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // If the Facility is not newly added, then hide delete button
            Label lblReservationFacilityID = e.Item.FindControl("lblReservationFacilityID") as Label;

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

        private void displayAmountDue()
        {

            List<ReservationFacility> reservationFacilities = getAddOnFacility();

            // Display rented facility
            if(reservationFacilities.Count > 0)
            {
                RepeaterAmountDue.DataSource = reservationFacilities;
                RepeaterAmountDue.DataBind();

                lblNoAmountDue.Visible = false;
            }
            else
            {
                RepeaterAmountDue.DataSource = null;
                RepeaterAmountDue.DataBind();

                lblNoAmountDue.Visible = true;
            }
            
        }

        private List<ReservationFacility> getAddOnFacility()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            List<ReservationFacility> addOns = new List<ReservationFacility>();

            // Find the newly rented facility 
            for (int i = 0; i < reservationFacilities.Count; i++)
            {
                if (reservationFacilities[i].reservationFacilityID == "")
                {
                    addOns.Add(reservationFacilities[i]);
                }
            }

            return addOns;
        }

        private void calculateTotalCharges()
        {
            List<ReservationFacility> reservationFacilities = getAddOnFacility();

            double totalPayment = 0;

            for (int i = 0; i < reservationFacilities.Count; i++)
            {
                totalPayment += reservationFacilities[i].subTotal;
            }

            if (totalPayment != 0.00)
            {
                // Display total
                lblTotal.Text = string.Format("{0:0.00}", totalPayment);

                // Calculate grand total
                totalPayment += calcTaxCharges(totalPayment);

                // Display grand total
                lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment);
            }
            else
            {   
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

        protected void RepeaterRentedFacility_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RentFacility.aspx?ID=" + en.encryption(reservationID));
        }

        protected void RepeaterAmountDue_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // If the Facility is not newly added, then hide delete button
            Label lblFacilityID = e.Item.FindControl("lblFacilityID") as Label;
            Label lblQuantity = e.Item.FindControl("lblQuantity") as Label;

            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            DateTime checkInDate = Convert.ToDateTime(reservationDetails.checkInDate);
            DateTime checkOutDate = Convert.ToDateTime(reservationDetails.checkOutDate);

            int durationOfStaty = reservationUtility.getdurationOfStay(checkInDate.ToShortDateString(), checkOutDate.ToShortDateString());

            if(durationOfStaty > 1)
            {
                getPurchasedQty(lblFacilityID.Text, durationOfStaty, lblQuantity);
            }
            
        }

        private void getPurchasedQty(string facilityID, int durationOfStay, Label lblQuantity)
        {
            string priceType;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getPriceType = "SELECT PriceType FROM Facility WHERE FacilityID LIKE @FacilityID";

            SqlCommand cmdGetPriceType = new SqlCommand(getPriceType, conn);

            cmdGetPriceType.Parameters.AddWithValue("@FacilityID", facilityID);

            priceType = (string)cmdGetPriceType.ExecuteScalar();

            conn.Close();

            if(priceType != "Per Reservation")
            {
                lblQuantity.Text = (durationOfStay * int.Parse(lblQuantity.Text)).ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            // Check if any facility rented
            if (reservationFacilities.Count > 0)
            {
                saveRentedFacility();
            }

            saveRentedRoom();

            updateReservationStatus();

            Response.Redirect("CheckedIn.html");

        }

        private void saveRentedRoom()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = reservationDetails.reservedRoom;

            for (int i = 0; i < reservedRooms.Count; i++)
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

            string updateReservationStatus = "UPDATE Reservation SET Status = 'Check In' " +
                                                "WHERE ReservationID LIKE @ID";

            SqlCommand cmdUpdateReservationStatus = new SqlCommand(updateReservationStatus, conn);

            cmdUpdateReservationStatus.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdUpdateReservationStatus.ExecuteNonQuery();

            conn.Close();

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
                group = (int)cmdGetGroupNumber.ExecuteScalar();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }

            conn.Close();

            return group;
        }
    }
}