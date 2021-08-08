using Hotel_Management_System.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class Payment : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Get reservation object from Session variable
        Reservation reservation = new Reservation();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            reservation = (Reservation)Session["ReservationDetails"];

            setReservationDetails();

            setItemToRepeaterRentedRoomType();

            setItemToRepeaterRentedFacility();

            calcTotalPayment();
        }

        private void setReservationDetails()
        {
            Reservation reservation = (Reservation)Session["ReservationDetails"];

            // Set the reservation details into label
            lblGuestName.Text = reservation.guestName;
            lblCheckInDate.Text = reservation.checkInDate;
            lblCheckOutDate.Text = reservation.checkOutDate;
            lblDurationOfStay.Text = reservationUtility.getdurationOfStay(reservation.checkInDate, reservation.checkOutDate).ToString();

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();
        }

        private void setItemToRepeaterRentedRoomType()
        {
            List<ReservedRoom> reservedRooms = reservation.reservedRoom;

            RepeaterRentedRoomType.DataSource = reservedRooms;
            RepeaterRentedRoomType.DataBind();
        }

        private void setItemToRepeaterRentedFacility()
        {
            List<RentedFacility> rentedFacilities = reservation.rentedFacility;

            if (rentedFacilities.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                RepeaterRentedFacility.DataSource = rentedFacilities;
                RepeaterRentedFacility.DataBind();

                lblNoItemFound.Visible = false;
            }
        }

        protected void RepeaterRentedRoomType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblDate = e.Item.FindControl("lblDate") as Label;
            Label lblExtraBedPrice = e.Item.FindControl("lblExtraBedPrice") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);
            lblDate.Text = formatedDate.ToShortDateString();

            // Format extraBedPrice
            if(lblExtraBedPrice.Text == "-1.00")
            {
                lblExtraBedPrice.Text = "0.00";
            }
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

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string nextReservationID = saveReservationDetails();
            saveReservedRoom(nextReservationID);
            saveRentedFacility(nextReservationID);

            // ***** Redirect etc do here
        }

        private string saveReservationDetails()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // get next reservation id
            String nextReservationID = idGenerator.getNextID("ReservationID", "Reservation", "RS");

            String addReservation = "INSERT INTO Reservation VALUES (@ReservationID, @CheckInDate, @CheckOutDate, " +
                                    "@GuetID, @ReservationDate, @SecretPassword, @Status)";

            SqlCommand cmdAddReservation = new SqlCommand(addReservation, conn);

            cmdAddReservation.Parameters.AddWithValue("@ReservationID", nextReservationID);
            cmdAddReservation.Parameters.AddWithValue("@CheckInDate", reservation.checkInDate);
            cmdAddReservation.Parameters.AddWithValue("@CheckOutDate", reservation.checkOutDate);
            cmdAddReservation.Parameters.AddWithValue("@GuetID", reservation.guestID);
            cmdAddReservation.Parameters.AddWithValue("@ReservationDate", todaysDate);
            cmdAddReservation.Parameters.AddWithValue("@SecretPassword", generateSecretPassword());
            cmdAddReservation.Parameters.AddWithValue("@Status", "Created");

            int i = cmdAddReservation.ExecuteNonQuery();

            conn.Close();

            return nextReservationID;
        }

        private void saveReservedRoom(string nextReservationID)
        {
            // Get the list of reserved room
            List<ReservedRoom> reservedRooms = reservation.reservedRoom;

            for (int i = 0; i < reservedRooms.Count; i++)
            {
                // get next reservationRoomID
                String nextReservationRoomID = idGenerator.getNextID("ReservationRoomID", "ReservationRoom", "RR");

                // Open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                String addReservedRoom = "INSERT INTO ReservationRoom (Adults, Kids, RoomPrice, ExtraBedCharges, ExtraBed, Date, " +
                    "RoomTypeID, ReservationID, ReservationRoomID) " +
                    "VALUES (@Adults, @Kids, @RoomPrice, @ExtraBedCharges, " +
                    "@ExtraBed, @Date, @RoomTypeID, @ReservationID, @ReservationRoomID)";

                SqlCommand cmdAddReservationRoom = new SqlCommand(addReservedRoom, conn);

                cmdAddReservationRoom.Parameters.AddWithValue("@Adults", reservedRooms[i].adults);
                cmdAddReservationRoom.Parameters.AddWithValue("@Kids", reservedRooms[i].kids);
                cmdAddReservationRoom.Parameters.AddWithValue("@RoomPrice", Convert.ToDecimal(reservedRooms[i].roomPrice));

                // check if user have request extraBed
                if(reservedRooms[i].extraBedPrice != -1)
                {
                    // If yes
                    cmdAddReservationRoom.Parameters.AddWithValue("@ExtraBedCharges", Convert.ToDecimal(reservedRooms[i].extraBedPrice));
                    cmdAddReservationRoom.Parameters.AddWithValue("@ExtraBed", "True");
                }
                else
                {
                    // If no
                    cmdAddReservationRoom.Parameters.AddWithValue("@ExtraBedCharges", 0.00);
                    cmdAddReservationRoom.Parameters.AddWithValue("@ExtraBed", "False");
                }

                cmdAddReservationRoom.Parameters.AddWithValue("@Date", reservedRooms[i].date);
                cmdAddReservationRoom.Parameters.AddWithValue("@RoomTypeID", reservedRooms[i].roomTypeID);
                cmdAddReservationRoom.Parameters.AddWithValue("@ReservationID", nextReservationID);
                cmdAddReservationRoom.Parameters.AddWithValue("@ReservationRoomID", nextReservationID);

                int j = cmdAddReservationRoom.ExecuteNonQuery();

                conn.Close();
            }
            
        }

        private void saveRentedFacility(string nextReservationID)
        {
            List<RentedFacility> rentedFacilities = reservation.rentedFacility;

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            String addRentedFacility;

            // Total rented facility record
            for (int i = 0; i < rentedFacilities.Count; i++)
            {

                if (rentedFacilities[i].priceType == "Per Night" && rentedFacilities[i].priceType == "Per Person")
                {
                    string []date = reservationUtility.getReservationDate(rentedFacilities[i].rentDate, rentedFacilities[i].returnDate);

                    int rentedDuration = date.Length;

                    // Duration of rent
                    for(int j = 0; j < rentedDuration; j++)
                    {
                        // Rented quantity
                        for(int q = 0; q < rentedFacilities[i].quantity; i++)
                        {
                            // get next reservationRoomID
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationRoomID", "ReservationFacility", "RF");

                            double facilityRentedPrice = rentedFacilities[i].price / rentedDuration / rentedFacilities[i].quantity;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date and quantity
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", rentedFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", nextReservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);

                            int success = cmdAddRentedFacility.ExecuteNonQuery();

                            conn.Close();
                        }
                        
                    }
                }
                else if (rentedFacilities[i].priceType == "Per Reservation")
                {
                    // Get list of date from duration of stay
                    string[] date = reservationUtility.getReservationDate(reservation.checkInDate, reservation.checkOutDate);

                    // Duration of rent
                    for(int j = 0; j < date.Length; j++)
                    {
                        // quantity
                        for(int q = 0; q < rentedFacilities[i].quantity; q++)
                        {
                            // get next reservationRoomID
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationRoomID", "ReservationFacility", "RF");

                            // Calculate rent price for the facility
                            double facilityRentedPrice = rentedFacilities[i].price / rentedFacilities[i].quantity;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", rentedFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", nextReservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);

                            int success = cmdAddRentedFacility.ExecuteNonQuery();

                            // Close connection
                            conn.Close();
                        }
                    }
                    
                }
            }
        }

        private string generateSecretPassword()
        {
            return "ABC";
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = true;
            PopupCancel.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupCancel.Visible = false;
            
        }

        protected void btnPopupConfirmCancelReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakeReservation.aspx");
        }

        private void calcTotalPayment()
        {
            // Calculate total payment and display on screen

            double totalPayment = 0;

            // Get the list of rented room and facility entered previously
            List<ReservedRoom> reservedRooms = reservation.reservedRoom;
            List<RentedFacility> rentedFacilities = reservation.rentedFacility;

            // Accumulate total room price
            for(int i = 0; i < reservedRooms.Count; i++)
            {
                totalPayment += reservedRooms[i].roomPrice;

                if(reservedRooms[i].extraBedPrice != -1)
                {
                    totalPayment += reservedRooms[i].extraBedPrice;
                }
            }

            // Accumulate facility details
            for(int j = 0; j < rentedFacilities.Count; j++)
            {
                totalPayment += rentedFacilities[j].price;
            }

            // Display total
            lblTotal.Text = string.Format("{0:0.00}", totalPayment);

            // Calculate grand total
            totalPayment += calcTaxCharges(totalPayment);

            // Display grand total
            lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment);

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
    }
}