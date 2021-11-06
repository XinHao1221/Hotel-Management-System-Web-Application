using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;
using System.Configuration;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class ViewReservation : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        private String reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Reservation Details";

            if (!IsPostBack)
            {
                Session["ReservationDetails"] = new ReservationDetail();

                Session["ReservedRoomType"] = new List<ReservedRoomType>();

                getReservationDetails();

                getReservedRoom();

                getRentedFacilityList();

                setStayDetails();

                setRentedRoomToRepeater();

                setRentedFacilityToRepeater();

                checkIfWithinDuration();

            }
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reservation.aspx");
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
                string temp = sdr.GetString(sdr.GetOrdinal("CheckInDate"));
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
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), sdr["RoomID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), Convert.ToDouble(sdr["ExtraBedCharges"]), sdr["Date"].ToString());
                }
                else
                {
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), sdr["RoomID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), -1, sdr["Date"].ToString());
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

        private void setStayDetails()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            setGuestInformation(reservation.guestID);

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(reservation.checkInDate);
            DateTime formatedCheckOutDate = Convert.ToDateTime(reservation.checkOutDate);

            lblCheckIn.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOut.Text = formatedCheckOutDate.ToShortDateString();

            string temp = formatedCheckOutDate.ToShortDateString();

            int durationOfStay = reservationUtility.getdurationOfStay(lblCheckIn.Text, lblCheckOut.Text);

            lblDurationOfStay.Text = durationOfStay.ToString() + " Night";
        }

        private void setGuestInformation(string guestID)
        {

            conn = new SqlConnection(strCon);
            conn.Open();

            // Get guest details for the selected guest
            String getGuestDetails = "SELECT * FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestDetails = new SqlCommand(getGuestDetails, conn);

            cmdGetGuestDetails.Parameters.AddWithValue("@ID", guestID);

            SqlDataReader sdr = cmdGetGuestDetails.ExecuteReader();

            // Set guest details into label
            if (sdr.Read())
            {
                lblGuestName.Text = sdr.GetString(sdr.GetOrdinal("Name"));
                lblIDNo.Text = sdr.GetString(sdr.GetOrdinal("IDNo"));
            }

            conn.Close();
        }

        private void setRentedFacilityToRepeater()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility
            List<ReservationFacility> reservationFacilities = reservation.rentedFacility;

            if (reservationFacilities.Count != 0)
            {
                // Set data to Facility Repeater
                RepeaterRentedFacility.DataSource = reservationFacilities;
                RepeaterRentedFacility.DataBind();

                lblNoFacilityFound.Visible = false;
            }
            else
            {
                lblNoFacilityFound.Visible = true;
            }


        }

        private void setRentedRoomToRepeater()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            // Hold Reserved Room
            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            // Set rented room to repeater
            RepeaterRentedRoomType.DataSource = reservedRooms;
            RepeaterRentedRoomType.DataBind();
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

        protected void RepeaterRentedRoomType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblDate = e.Item.FindControl("lblDate") as Label;
            Label lblExtraBedPrice = e.Item.FindControl("lblExtraBedPrice") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);
            lblDate.Text = formatedDate.ToShortDateString();

            // Format extraBedPrice
            if (lblExtraBedPrice.Text == "-1.00")
            {
                lblExtraBedPrice.Text = "0.00";
            }

        }


        protected void LBRefund_Click(object sender, EventArgs e)
        {
            // Redirect to refund page
            Response.Redirect("Refund.aspx?ID=" + en.encryption(reservationID));
        }

        private void checkIfWithinDuration()
        {
            DateTime todaysDate = DateTime.Now;
            DateTime checkInDate = Convert.ToDateTime(lblCheckIn.Text);
            DateTime checkOutDate = Convert.ToDateTime(lblCheckOut.Text);

            // Check if the reservation is within duration
            if (todaysDate.Date >= checkInDate.Date && todaysDate.Date < checkOutDate.Date)
            {
                LBCheckIn.Visible = true;
            }
            else
            {
                LBCheckIn.Visible = false;
            }
        }

        protected void LBCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../CheckIn/CheckInGuest.aspx?ID=" + en.encryption(reservationID));

        }
    }
}