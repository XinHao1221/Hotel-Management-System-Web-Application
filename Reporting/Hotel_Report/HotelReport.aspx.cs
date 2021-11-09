using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotel_Management_System.Utility;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Hotel_Management_System.Dashboard;

namespace Hotel_Management_System.Reporting.Hotel_Report
{
    public partial class HotelReport : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Hold today's date
        //private string todaysDate = "2021-10-18";
        private string todaysDate;
        private int totalAdults;
        private int totalKids;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current date
            DateTime dateNow = DateTime.Now;
            todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Display current date

            // Page TItle
            Page.Title = "Hotel Report";

            if (!IsPostBack)
            { 
                // Display cuurent hotel's information
                displayTotalArrival();
                displayTotalDeparture();

                getTotalGuestInHouse();
                displayTotalInHouseGuest();

                displayRoomType();
                displayFacility();
            }

            lblReportDate.Text = Convert.ToDateTime(todaysDate).ToShortDateString();
        }

        private void displayTotalArrival()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get total arrival from database
            string getTotalArrival = "SELECT COUNT(*) FROM Reservation WHERE CheckInDate LIKE @todaysDate AND Status IN ('Created', 'Checked In', 'Check In')";

            SqlCommand cmdGetTotalArrival = new SqlCommand(getTotalArrival, conn);

            cmdGetTotalArrival.Parameters.AddWithValue("@todaysDate", todaysDate);

            lblTotalArrival.Text = ((int)cmdGetTotalArrival.ExecuteScalar()).ToString();

            conn.Close();

        }

        private void displayTotalDeparture()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalDeparture = "SELECT COUNT(*) FROM Reservation WHERE CheckOutDate LIKE @todaysDate AND Status IN ('Checked In', 'Checked Out')";

            SqlCommand cmdGetTotalDeparture = new SqlCommand(getTotalDeparture, conn);

            cmdGetTotalDeparture.Parameters.AddWithValue("@todaysDate", todaysDate);

            lblTotalDeparture.Text = ((int)cmdGetTotalDeparture.ExecuteScalar()).ToString();

            conn.Close();

        }

        private List<String> getInHouseReservationID()
        {
            // Get a list of reservation ID which is current in the hotel
            List<String> reservationID = new List<string>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getInHouseReservationID = "SELECT ReservationID FROM Reservation WHERE Status LIKE 'Checked In'";

            SqlCommand cmdGetInHouseReservationID = new SqlCommand(getInHouseReservationID, conn);

            SqlDataReader sdr = cmdGetInHouseReservationID.ExecuteReader();

            while (sdr.Read())
            {
                reservationID.Add(sdr["ReservationID"].ToString());
            }

            conn.Close();

            return reservationID;
        }

        private string getLastReservationDate(string reservationID)
        {
            // Get last reservation Date (ie: CheckOutDate - 1)

            conn = new SqlConnection(strCon);
            conn.Open();

            string getCheckOutDate = "SELECT CheckOutDate FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetCheckOutDate = new SqlCommand(getCheckOutDate, conn);

            cmdGetCheckOutDate.Parameters.AddWithValue("@ID", reservationID);

            string checkOutDate = (string)cmdGetCheckOutDate.ExecuteScalar();

            conn.Close();

            // Get last reservation date
            DateTime previousDate = Convert.ToDateTime(checkOutDate);
            previousDate = previousDate.AddDays(-1);

            // Return last reservation date
            return reservationUtility.formatDate(previousDate.ToShortDateString());
        }

        private void getTotalGuestInHouse()
        {
            // Use last reservation date to sum up total adults and kids

            // Hold in house reservation id
            List<String> reservationID = getInHouseReservationID();

            for (int i = 0; i < reservationID.Count; i++)
            {
                string lastReservationDate = getLastReservationDate(reservationID[i]);

                conn = new SqlConnection(strCon);
                conn.Open();

                string getGuestInHouse = "SELECT * FROM ReservationRoom WHERE ReservationID LIKE @ID AND Date LIKE @Date";

                SqlCommand cmdGetGuestInHouse = new SqlCommand(getGuestInHouse, conn);

                cmdGetGuestInHouse.Parameters.AddWithValue("@ID", reservationID[i]);
                cmdGetGuestInHouse.Parameters.AddWithValue("@Date", lastReservationDate);

                SqlDataReader sdr = cmdGetGuestInHouse.ExecuteReader();

                while (sdr.Read())
                {
                    totalAdults += int.Parse(sdr["Adults"].ToString());
                    totalKids += int.Parse(sdr["Kids"].ToString());
                }

                conn.Close();
            }

        }

        private void displayTotalInHouseGuest()
        {
            // Calc and dispay total in house guest
            lblTotalInHouseGuest.Text = (totalAdults + totalKids).ToString();
        }

        private void displayRoomType()
        {
            // Get all hotel's room type from the database

            conn = new SqlConnection(strCon);
            conn.Open();

            string getAllRoomType = "SELECT DISTINCT(RT.RoomTypeID), RT.Title FROM RoomType RT, Room R WHERE RT.RoomTypeID LIKE R.RoomTypeID";

            SqlCommand cmdGetAllRoomType = new SqlCommand(getAllRoomType, conn);

            SqlDataReader sdr = cmdGetAllRoomType.ExecuteReader();

            RepeaterRoom.DataSource = sdr;
            RepeaterRoom.DataBind();

            conn.Close();
        }

        protected void RepeaterRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's references from the repeater
            Label lblRoomTypeID = e.Item.FindControl("lblRoomTypeID") as Label;
            Label lblCheckIn = e.Item.FindControl("lblCheckIn") as Label;
            Label lblInHouse = e.Item.FindControl("lblInHouse") as Label;
            Label lblCheckOut = e.Item.FindControl("lblCheckOut") as Label;

            // Display quantity
            lblCheckIn.Text = getTotalRoomCheckIn(lblRoomTypeID.Text).ToString();
            lblInHouse.Text = getTotalRoomInHouse(lblRoomTypeID.Text).ToString();
            lblCheckOut.Text = getTotalRoomCheckOut(lblRoomTypeID.Text).ToString();

        }

        private int getTotalRoomCheckIn(string roomTypeID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalRoomCheckIn = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " +
                                        "WHERE RR.Date LIKE @Date AND R.CheckInDate LIKE @Date " +
                                        "AND RR.RoomTypeID LIKE @RoomTypeID AND R.ReservationID LIKE RR.ReservationID";

            SqlCommand cmdGetTotalRoomCheckIn = new SqlCommand(getTotalRoomCheckIn, conn);

            cmdGetTotalRoomCheckIn.Parameters.AddWithValue("@Date", todaysDate);
            cmdGetTotalRoomCheckIn.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            int total = (int)cmdGetTotalRoomCheckIn.ExecuteScalar();

            conn.Close();

            return total;
        }

        private int getTotalRoomInHouse(string roomTypeID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalRoomInHouse = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " +
                                        "WHERE RR.Date LIKE @Date AND RR.RoomTypeID LIKE @RoomTypeID " +
                                        "AND R.ReservationID LIKE RR.ReservationID AND R.Status LIKE 'Checked In'";

            SqlCommand cmdGetTotalRoomInHouse = new SqlCommand(getTotalRoomInHouse, conn);

            cmdGetTotalRoomInHouse.Parameters.AddWithValue("@Date", todaysDate);
            cmdGetTotalRoomInHouse.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            int total = (int)cmdGetTotalRoomInHouse.ExecuteScalar();

            conn.Close();

            total += getOvertimeReservedRoomQty(roomTypeID);

            return total;
        }

        public List<String> getOverTimeReservationID()
        {
            // Hold a list of reservationID
            List<String> reservationIDs = new List<string>();

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            conn = new SqlConnection(strCon);
            conn.Open();

            // Get overtime reservationID
            string getOvertimeReservation = "SELECT * FROM Reservation R WHERE R.CheckOutDate < @Date " +
                                            "AND R.Status LIKE 'Checked In'";

            SqlCommand cmdGetOvertimeReservation = new SqlCommand(getOvertimeReservation, conn);

            cmdGetOvertimeReservation.Parameters.AddWithValue("@Date", todaysDate);

            SqlDataReader sdr = cmdGetOvertimeReservation.ExecuteReader();

            while (sdr.Read())
            {
                reservationIDs.Add(sdr["ReservationID"].ToString());
            }

            conn.Close();

            return reservationIDs;
        }

        private int getOvertimeReservedRoomQty(string roomTypeID)
        {
            // Get no of overtime reservation for the room type id provided

            int total = 0;

            List<String> reservationIDs = getOverTimeReservationID();

            for(int i = 0; i < reservationIDs.Count; i++)
            {
                string lastReservationDate = getLastReservationDate(reservationIDs[i]);

                conn = new SqlConnection(strCon);
                conn.Open();

                string getTotalRoom = "SELECT COUNT(*) FROM ReservationRoom WHERE ReservationID LIKE @ID AND Date LIKE @Date " +
                    "AND RoomTypeID LIKE @RoomTypeID";

                SqlCommand cmdGetTotalRoom = new SqlCommand(getTotalRoom, conn);

                cmdGetTotalRoom.Parameters.AddWithValue("@ID", reservationIDs[i]);
                cmdGetTotalRoom.Parameters.AddWithValue("@Date", lastReservationDate);
                cmdGetTotalRoom.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

                total += (int)cmdGetTotalRoom.ExecuteScalar();

                conn.Close();

            }

            return total;
        }

        private int getTotalRoomCheckOut(string roomTypeID)
        {
            // Get yesterday's date
            DateTime date = Convert.ToDateTime(todaysDate);
            string yesterday = (date.AddDays(-1)).ToShortDateString();
            yesterday = reservationUtility.formatDate(yesterday);

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalRoomCheckOut = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " +
                                        "WHERE R.CheckOutDate LIKE @CurrentDate " +
                                        "AND RR.Date LIKE @Yesterday AND RR.RoomTypeID LIKE @RoomTypeID AND R.ReservationID LIKE RR.ReservationID";

            SqlCommand cmdGetTotalCheckOut = new SqlCommand(getTotalRoomCheckOut, conn);

            cmdGetTotalCheckOut.Parameters.AddWithValue("@CurrentDate", todaysDate);
            cmdGetTotalCheckOut.Parameters.AddWithValue("@Yesterday", yesterday);
            cmdGetTotalCheckOut.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            int total = (int)cmdGetTotalCheckOut.ExecuteScalar();

            conn.Close();

            return total;

        }

        private void displayFacility()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getAllFacility = "SELECT FacilityID, FacilityName FROM Facility";

            SqlCommand cmdGetAllFacility = new SqlCommand(getAllFacility, conn);

            SqlDataReader sdr = cmdGetAllFacility.ExecuteReader();

            RepeaterFacility.DataSource = sdr;
            RepeaterFacility.DataBind();

            conn.Close();
        }

        protected void RepeaterFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblFacilityID = e.Item.FindControl("lblFacilityID") as Label;
            Label lblCheckIn = e.Item.FindControl("lblCheckIn") as Label;
            Label lblInHouse = e.Item.FindControl("lblInHouse") as Label;
            Label lblCheckOut = e.Item.FindControl("lblCheckOut") as Label;

            // Display quantity
            lblCheckIn.Text = getTotalFacilityCheckIn(lblFacilityID.Text).ToString();
            lblInHouse.Text = getTotalFacilityInHouse(lblFacilityID.Text).ToString();
            lblCheckOut.Text = getTotalFacilityCheckOut(lblFacilityID.Text).ToString();
        }

        private int getTotalFacilityCheckIn(string facilityID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalFacilityCheckIn = "SELECT COUNT(*) FROM ReservationFacility RF, Reservation R " +
                                                "WHERE RF.DateRented LIKE @Date AND R.CheckInDate LIKE @Date " +
                                                "AND RF.FacilityID LIKE @FacilityID AND R.ReservationID LIKE RF.ReservationID";

            SqlCommand cmdGetTotalFacilityCheckIn = new SqlCommand(getTotalFacilityCheckIn, conn);

            cmdGetTotalFacilityCheckIn.Parameters.AddWithValue("@Date", todaysDate);
            cmdGetTotalFacilityCheckIn.Parameters.AddWithValue("@FacilityID", facilityID);

            int total = (int)cmdGetTotalFacilityCheckIn.ExecuteScalar();

            conn.Close();

            return total;
        }

        private int getTotalFacilityInHouse(string facilityID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalFacilityInHouse = "SELECT COUNT(*) " +
                                            "FROM ReservationFacility RF, Reservation R " +
                                            "WHERE RF.DateRented LIKE @Date " +
                                            "AND RF.FacilityID LIKE @FacilityID AND R.ReservationID LIKE RF.ReservationID " +
                                            "AND R.Status LIKE 'Checked In'";

            SqlCommand cmdGetTotalFacilityInHouse = new SqlCommand(getTotalFacilityInHouse, conn);

            cmdGetTotalFacilityInHouse.Parameters.AddWithValue("@Date", todaysDate);
            cmdGetTotalFacilityInHouse.Parameters.AddWithValue("@FacilityID", facilityID);

            int total = (int)cmdGetTotalFacilityInHouse.ExecuteScalar();

            conn.Close();

            return total;
        }

        private int getTotalFacilityCheckOut(string facilityID)
        {
            // Get yesterday's date
            DateTime date = Convert.ToDateTime(todaysDate);
            string yesterday = (date.AddDays(-1)).ToShortDateString();
            yesterday = reservationUtility.formatDate(yesterday);

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalFacilityCheckOut = "SELECT COUNT(*) " +
                                            "FROM ReservationFacility RF, Reservation R " +
                                            "WHERE R.CheckOutDate LIKE @Date " +
                                            "AND RF.DateRented LIKE @Yesterday AND " +
                                            "RF.FacilityID LIKE @FacilityID AND R.ReservationID LIKE RF.ReservationID";

            SqlCommand cmdGetTotalFacilityCheckOut = new SqlCommand(getTotalFacilityCheckOut, conn);

            cmdGetTotalFacilityCheckOut.Parameters.AddWithValue("@Date", todaysDate);
            cmdGetTotalFacilityCheckOut.Parameters.AddWithValue("@Yesterday", yesterday);
            cmdGetTotalFacilityCheckOut.Parameters.AddWithValue("@FacilityID", facilityID);

            int total = (int)cmdGetTotalFacilityCheckOut.ExecuteScalar();

            conn.Close();

            return total;
        }

    }
}