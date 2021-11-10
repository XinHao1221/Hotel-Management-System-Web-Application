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
using System.Net.Mail;
using System.IO;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class Payment : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Get ReservationDetail object from Session variable
        Reservation reservation = new Reservation();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Payment";

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
            Reservation reservation = (Reservation)Session["ReservationDetails"];

            string nextReservationID = saveReservationDetails();
            saveReservedRoom(nextReservationID);
            saveRentedFacility(nextReservationID);
            savePayment(nextReservationID);

            // Set Session Variable to null
            Session["AvailableRoom"] = null;
            Session["AvailableFacility"] = null;
            Session["AvailableFacility"] = null;
            Session["RentedFacilityList"] = null;

            // Send Self-check-in link
            // Check if check in date same as today's date
            // Only send when the check in date is not today's date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            if (todaysDate != reservationUtility.formatDate(reservation.checkInDate))
            {
                sendSelfCheckInLink(nextReservationID);
            }

            // Show popup Success Message
            lblPopupReservationSuccess.Text = "Reservation Saved Successfully.";
            PopupReservationSuccess.Visible = true;
            PopupCover.Visible = true;
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
                                    "@GuetID, @ReservationDate, @SecretPassword, @Status, @Feedback)";

            SqlCommand cmdAddReservation = new SqlCommand(addReservation, conn);

            cmdAddReservation.Parameters.AddWithValue("@ReservationID", nextReservationID);
            cmdAddReservation.Parameters.AddWithValue("@CheckInDate", reservation.checkInDate);
            cmdAddReservation.Parameters.AddWithValue("@CheckOutDate", reservation.checkOutDate);
            cmdAddReservation.Parameters.AddWithValue("@GuetID", reservation.guestID);
            cmdAddReservation.Parameters.AddWithValue("@ReservationDate", todaysDate);
            cmdAddReservation.Parameters.AddWithValue("@SecretPassword", generateSecretPassword());
            cmdAddReservation.Parameters.AddWithValue("@Status", "Created");
            cmdAddReservation.Parameters.AddWithValue("@Feedback", "");

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
                cmdAddReservationRoom.Parameters.AddWithValue("@ReservationRoomID", nextReservationRoomID);

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

                if (rentedFacilities[i].priceType == "Per Night" || rentedFacilities[i].priceType == "Per Person")
                {
                    string []date = reservationUtility.getReservationDate(rentedFacilities[i].rentDate, rentedFacilities[i].returnDate);

                    int rentedDuration = date.Length;

                    // Duration of rent
                    for(int j = 0; j < rentedDuration; j++)
                    {
                        // Rented quantity
                        for(int q = 0; q < rentedFacilities[i].quantity; q++)
                        {
                            // get next reservationRoomID
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationFacilityID", "ReservationFacility", "RF");

                            //double facilityRentedPrice = rentedFacilities[i].price / rentedDuration / rentedFacilities[i].quantity;
                            double facilityRentedPrice = rentedFacilities[i].price;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date and quantity
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID, @Group)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", rentedFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", nextReservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@Group", i + 1);

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
                            String nextReservationFacilityID = idGenerator.getNextID("ReservationFacilityID", "ReservationFacility", "RF");

                            // Calculate rent price for the facility
                            //double facilityRentedPrice = rentedFacilities[i].price / rentedFacilities[i].quantity;
                            double facilityRentedPrice = rentedFacilities[i].price;

                            // Open connection
                            conn = new SqlConnection(strCon);
                            conn.Open();

                            // query to add rented facility 
                            // Add one-by-one 
                            // Each date
                            addRentedFacility = "INSERT INTO ReservationFacility VALUES (@FacilityID, @ReservationID, @price, " +
                                "@DateRented, @DateCreated, @ReservationFacilityID, @Group)";

                            SqlCommand cmdAddRentedFacility = new SqlCommand(addRentedFacility, conn);

                            cmdAddRentedFacility.Parameters.AddWithValue("@FacilityID", rentedFacilities[i].facilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationID", nextReservationID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@price", facilityRentedPrice);
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateRented", date[j].ToString());
                            cmdAddRentedFacility.Parameters.AddWithValue("@DateCreated", todaysDate);
                            cmdAddRentedFacility.Parameters.AddWithValue("@ReservationFacilityID", nextReservationFacilityID);
                            cmdAddRentedFacility.Parameters.AddWithValue("@Group", i + 1);

                            int success = cmdAddRentedFacility.ExecuteNonQuery();

                            // Close connection
                            conn.Close();
                        }
                    }
                    
                }
            }
        }

        private void savePayment(string nextReservationID) { 

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
            cmdAddPayment.Parameters.AddWithValue("@ReservationID", nextReservationID);
            cmdAddPayment.Parameters.AddWithValue("@Date", todaysDate);

            int i = cmdAddPayment.ExecuteNonQuery();

            conn.Close();

        }

        private string generateSecretPassword()
        {
            return (_random.Next(100000, 999999)).ToString();
        }

        private void sendSelfCheckInLink(string nextReservationID)
        {
            // **** Important Note *****
            // Please set up this three variable b4 sending email
            // Set receiver email
            string emailTo = getGuestEmailAddress();

            // Check if guest have any emailAddress
            if (emailTo.Length > 0)
            {
                // Set sender and receiver email
                string emailFrom = "hmsagent1221@gmail.com";
                string password = "Asdfg12345@";
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = "Self-Check-In";
                        mail.Body = CreateEmailBody(nextReservationID);
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                            lblEmailStatus.Text = "Self-Check-In Link Emailed.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblEmailStatus.Text = ex.Message;
                }
            }

        }

        private string CreateEmailBody(string nextReservationID)
        {
            Reservation reservation = (Reservation)Session["ReservationDetails"];

            IDEncryption en = new IDEncryption();

            string encryptedReservationID = en.encryption(nextReservationID);

            string emailBody = String.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("SelfCheckIn (Email).html")))
            {
                emailBody = reader.ReadToEnd();
            }

            // Replace the text in Template.html
            emailBody = emailBody.Replace("{fname}", reservation.guestName);
            emailBody = emailBody.Replace("{fPassword}", getPasswordFromDatabase(nextReservationID));
            emailBody = emailBody.Replace("{link}", "https://localhost:" + Application["LocalHostID"].ToString() + "/Front_Desk/Self-CheckIn/Customer/SelfCheckIn.aspx?ID=" + encryptedReservationID);

            return emailBody;
        }

        private string getGuestEmailAddress()
        {
            string guestID = Session["GuestID"].ToString();

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getEmailAddress = "SELECT Email FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetEmailAddress = new SqlCommand(getEmailAddress, conn);

            cmdGetEmailAddress.Parameters.AddWithValue("@ID", guestID);

            string emailAddress = "";
            try
            {
                emailAddress = (string)cmdGetEmailAddress.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }

            conn.Close();

            return emailAddress;
        }

        private string getPasswordFromDatabase(string nextReservationID)
        {
            string password = "";

            conn = new SqlConnection(strCon);
            conn.Open();

            string getPassowrd = "SELECT SecretPassword FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetPassword = new SqlCommand(getPassowrd, conn);

            cmdGetPassword.Parameters.AddWithValue("@ID", nextReservationID);

            password = (string)cmdGetPassword.ExecuteScalar();

            conn.Close();

            return password;
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
                totalPayment += rentedFacilities[j].subTotal;
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reservation.aspx");
        }
    }
}