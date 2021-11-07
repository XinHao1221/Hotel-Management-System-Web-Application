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
                Session["GuestInHouse"] = new List<GuestInHouse>();

                displayTotalArrival();
                displayTotalDeparture();

                getTotalGuestInHouse();
                calcTotalAdultsAndKids();
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

        private void getTotalGuestInHouse()
        {
            List<GuestInHouse> guestInHouses = (List<GuestInHouse>)Session["GuestInHouse"];

            conn = new SqlConnection(strCon);
            conn.Open();

            string getGuestInHouse = "SELECT R.ReservationID, RR.Adults, RR.Kids, RR.RoomID " +
                                    "FROM Reservation R, ReservationRoom RR " +
                                    "WHERE R.Status LIKE 'Checked In' AND R.ReservationID LIKE RR.ReservationID";

            SqlCommand cmdGetGuestInHouse = new SqlCommand(getGuestInHouse, conn);

            SqlDataReader sdr = cmdGetGuestInHouse.ExecuteReader();

            // Hold the data into object temporary.
            List<GuestInHouse> temp = new List<GuestInHouse>();

            while (sdr.Read())
            {
                temp.Add(new GuestInHouse(sdr["ReservationID"].ToString(), sdr["RoomID"].ToString(), int.Parse(sdr["Adults"].ToString()),
                                        int.Parse(sdr["Kids"].ToString())));
            }

            conn.Close();

            // Set actual adults and kids into session object
            // Save the first record
            if (temp.Count > 0)
            {
                guestInHouses.Add(new GuestInHouse(temp[0].reservationID, temp[0].roomID, temp[0].adults, temp[0].kids));

                for (int i = 1; i < temp.Count; i++)
                {
                    Boolean exists = false;

                    for (int j = 0; j < guestInHouses.Count; j++)
                    {
                        if (temp[i].reservationID == guestInHouses[j].reservationID && temp[i].roomID == guestInHouses[j].roomID)
                        {
                            // If exits
                            exists = true;
                        }
                    }

                    // If record is not exists in the list
                    if (exists == false)
                    {
                        guestInHouses.Add(new GuestInHouse(temp[i].reservationID, temp[i].roomID, temp[i].adults, temp[i].kids));
                    }
                }
            }

        }
        private void calcTotalAdultsAndKids()
        {

            List<GuestInHouse> guestInHouses = (List<GuestInHouse>)Session["GuestInHouse"];

            totalAdults = 0;

            for (int i = 0; i < guestInHouses.Count; i++)
            {
                totalAdults += guestInHouses[i].adults;
                totalKids += guestInHouses[i].kids;
            }

        }

        private void displayTotalInHouseGuest()
        {
            lblTotalInHouseGuest.Text = (totalAdults + totalKids).ToString();
        }

        private void displayRoomType()
        {
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