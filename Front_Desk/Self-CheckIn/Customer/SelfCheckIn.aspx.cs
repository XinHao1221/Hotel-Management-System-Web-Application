using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;
using Hotel_Management_System.Front_Desk.Reservation;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class SelfCheckIn : System.Web.UI.Page
    {
        private string reservationID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Welcome";

            displayGuestName();

        }

        private void displayGuestName()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getGuestName = "SELECT G.Title, G.Name " +
                                "FROM Reservation R, Guest G " +
                                "WHERE R.GuestID LIKE G.GuestID " +
                                "AND ReservationID LIKE @ReservationID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ReservationID", reservationID);

            SqlDataReader sdr = cmdGetGuestName.ExecuteReader();

            while (sdr.Read())
            {
                lblGuestName.Text = sdr["Title"].ToString() + " " + sdr["Name"].ToString();
            }

            conn.Close();

        }

        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Check if no input entered
            if(txtSecretPassword.Text == "")
            {
                CVPassword.Text = "This field is required.";
                txtSecretPassword.Style["border"] = "2px solid red";
                args.IsValid = false;
            }
            else
            {
                // If password entered valid
                if (validatePassword())
                {
                    txtSecretPassword.Style["border"] = "2px solid rgb(192 192 192)";
                    args.IsValid = true;
                }
                else
                {
                    CVPassword.Text = "Invalid password.";
                    txtSecretPassword.Style["border"] = "2px solid red";
                    args.IsValid = false;
                }
                
            }
        }

        private Boolean validatePassword()
        {
            Boolean valid = false;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getPassword = "SELECT SecretPassword FROM Reservation WHERE ReservationID LIKE @ReservationID";

            SqlCommand cmdGetPassword = new SqlCommand(getPassword, conn);

            cmdGetPassword.Parameters.AddWithValue("@ReservationID", reservationID);

            string temp = (string)cmdGetPassword.ExecuteScalar();

            conn.Close();

            // Validate password
            if(temp == txtSecretPassword.Text)
            {
                valid = true;
            }

            return valid;

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (isToday())
                {
                    if (!isCheckedIn())
                    {
                        Session["ReservationDetails"] = new ReservationDetail();

                        Session["ReservedRoomType"] = new List<ReservedRoomType>();

                        getReservationDetails();

                        getReservedRoom();

                        getRentedFacilityList();

                        // get facility availability
                        Session["AvailableFacility"] = new List<AvailableFacility>();
                        Session["AvailableFacility"] = getFacilityAvailability("");

                        Response.Redirect("SelectRoom.aspx?ID=" + en.encryption(reservationID));
                    }
                    else
                    {
                        Response.Redirect("CheckedIn.html");
                    }
                }
                else
                {
                    Response.Redirect("SelfCheckIn(Error).aspx?Date=" + ViewState["CheckInDate"].ToString());
                }
                
                
            }
            
        }

        private Boolean isToday()
        {
            // Check if check in date same as today's date

            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Get Check in date
            conn = new SqlConnection(strCon);
            conn.Open();

            string getCheckInDate = "SELECT CheckInDate FROM Reservation WHERE ReservationID LIKE @ReservationID";

            SqlCommand cmdGetCheckInDate = new SqlCommand(getCheckInDate, conn);

            cmdGetCheckInDate.Parameters.AddWithValue("@ReservationID", reservationID);

            string checkInDate = (string)cmdGetCheckInDate.ExecuteScalar();

            conn.Close();

            if(todaysDate == checkInDate)
            {
                return true;
            }
            else
            {
                // Store check in date into ViewState
                ViewState["CheckInDate"] = Convert.ToDateTime(checkInDate).ToShortDateString().ToString();

                return false;
            }
        }

        private Boolean isCheckedIn()
        {
            // Check if guest already checked in
            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservationStatus = "SELECT Status FROM Reservation WHERE ReservationID LIKE @ReservationID";

            SqlCommand cmdGetReservationStatus = new SqlCommand(getReservationStatus, conn);

            cmdGetReservationStatus.Parameters.AddWithValue("@ReservationID", reservationID);

            string status = (string)cmdGetReservationStatus.ExecuteScalar();

            conn.Close();

            if(status == "Check In")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void getReservationDetails()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            conn = new SqlConnection(strCon);
            conn.Open();

            String getReservationDetails = "SELECT * FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservationDetails = new SqlCommand(getReservationDetails, conn);

            cmdGetReservationDetails.Parameters.AddWithValue("ID", reservationID);

            SqlDataReader sdr = cmdGetReservationDetails.ExecuteReader();

            // Set details into ReservationDetail object
            if (sdr.Read())
            {
                reservation.checkInDate = sdr.GetString(sdr.GetOrdinal("CheckInDate"));
                reservation.checkOutDate = sdr.GetString(sdr.GetOrdinal("CheckOutDate"));
                reservation.guestID = sdr.GetString(sdr.GetOrdinal("GuestID"));
            }

            conn.Close();
        }

        private void getReservedRoom()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = new List<ReservationRoom>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservedRoom = "SELECT * FROM ReservationRoom WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservedRoom = new SqlCommand(getReservedRoom, conn);

            cmdGetReservedRoom.Parameters.AddWithValue("ID", reservationID);

            SqlDataReader sdr = cmdGetReservedRoom.ExecuteReader();

            ReservationRoom rr;

            while (sdr.Read())
            {
                if (sdr["ExtraBed"].ToString() == "True")
                {
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), Convert.ToDouble(sdr["ExtraBedCharges"]), sdr["Date"].ToString());
                }
                else
                {
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), -1, sdr["Date"].ToString());
                }

                reservedRooms.Add(rr);

            }
            conn.Close();

            reservation.reservedRoom = reservedRooms;
        }

        // Get reservation facility from database
        private List<ReservationFacility> getReservationFacility()
        {
            List<ReservationFacility> reservationFacility = new List<ReservationFacility>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservedRoom = "SELECT * FROM ReservationFacility WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservedRoom = new SqlCommand(getReservedRoom, conn);

            cmdGetReservedRoom.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetReservedRoom.ExecuteReader();

            ReservationFacility rf;

            while (sdr.Read())
            {
                rf = new ReservationFacility(sdr["ReservationFacilityID"].ToString(), sdr["FacilityID"].ToString(), Convert.ToDouble(sdr["Price"]), sdr["DateRented"].ToString(), Convert.ToInt32(sdr["Group"]));

                reservationFacility.Add(rf);
            }

            conn.Close();

            // Get one last record, contain nothing
            // To let program know, the list is ended
            rf = new ReservationFacility("", "", 0.00, "", 0);
            reservationFacility.Add(rf);

            return reservationFacility;
        }

        // Format reservation facility into a list
        private void getRentedFacilityList()
        {
            // A list of reservation facility from database
            List<ReservationFacility> temp = getReservationFacility();

            // Formated facility list to be displayed on the screen
            List<ReservationFacility> reservationFacilities = new List<ReservationFacility>();

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

#pragma warning disable CS0168 // The variable 'facilityID' is declared but never used
            string rentDate, facilityID, returnDate;
#pragma warning restore CS0168 // The variable 'facilityID' is declared but never used
            int qty = 0, group;
#pragma warning disable CS0219 // The variable 'counter' is assigned but its value is never used
            int counter;
#pragma warning restore CS0219 // The variable 'counter' is assigned but its value is never used

            // Check if it contains at least one element
            // Got one default element
            // Therefore start with 1
            if (temp.Count > 1)
            {
                int i = 0;

                while ((i < temp.Count) && (temp[i].group != 0))
                {

                    int test = temp[i].group;

                    counter = 0;

                    rentDate = temp[i].rentDate;
                    returnDate = temp[i].rentDate;

                    group = temp[i].group;

                    qty = 1;

                    if ((i < temp.Count - 1))
                    {
                        if (temp[i + 1].group == group)
                        {
                            do
                            {
                                if (temp[i + 1].rentDate == rentDate)
                                {
                                    qty++;
                                    i++;

                                }
                                else if ((temp[i + 1].group == group))
                                {
                                    returnDate = temp[i + 1].rentDate;
                                    i++;
                                }

                            } while ((temp[i + 1].group == group));


                        }
                    }

                    // Add one into return date
                    returnDate = reservationUtility.getNextDate(returnDate);

                    ReservationFacility rf = new ReservationFacility(temp[i].reservationFacilityID, temp[i].facilityID,
                                                                            qty, temp[i].price, rentDate, returnDate);

                    reservationFacilities.Add(rf);

                    i++;

                }


            }

            reservation.rentedFacility = reservationFacilities;
        }

        private List<AvailableFacility> getFacilityAvailability(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Query to get facility availability
            string getFacility = "SELECT F.FacilityID, F.FacilityName, F.Quantity, F.Price, F.PriceType " +
                                "FROM Facility F " +
                                "WHERE F.Status LIKE 'Active'";

            SqlCommand cmdGetFacility = new SqlCommand(getFacility, conn);

            // Hold the data read from database
            var sdr = cmdGetFacility.ExecuteReader();

            // Get reference of AvailableFacility stored inside session
            List<AvailableFacility> availableFacility = new List<AvailableFacility>();

            // set all data into list object 
            while (sdr.Read())
            {

                AvailableFacility af = new AvailableFacility(
                    sdr.GetString(sdr.GetOrdinal("FacilityID")),
                    sdr.GetString(sdr.GetOrdinal("FacilityName")),
                    sdr.GetInt32(sdr.GetOrdinal("Quantity")),
                    Convert.ToDouble(sdr.GetDecimal(sdr.GetOrdinal("Price"))),
                    sdr.GetString(sdr.GetOrdinal("PriceType")),
                    "Available",
                    "No");

                availableFacility.Add(af);
            }

            conn.Close();

            if (date != "")
            {
                // Reduce facility quantity there have already been selected by the user.
                for (int i = 0; i < availableFacility.Count; i++)
                {

                    conn = new SqlConnection(strCon);
                    conn.Open();

                    AvailableFacility af = availableFacility.ElementAt(i);

                    int qty = getFacilityRentedQty(af.facilityID, date);

                    af.availableQty -= qty;

                    if (af.availableQty == 0)
                    {
                        af.status = "Unavailable";
                    }

                    conn.Close();
                }
            }

            return availableFacility;
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
    }
}