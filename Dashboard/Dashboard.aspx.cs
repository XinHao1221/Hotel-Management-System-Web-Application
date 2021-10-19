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

namespace Hotel_Management_System.Dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        // Used for date
        DateUtility dateUtility = new DateUtility();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        private string todaysDate = "2021-10-18";
        private int totalAdults;
        private int totalKids;
        private int totalRoom = 0;
        private int availableRoom = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current date
            //DateTime dateNow = DateTime.Now;
            //todaysDate = reservationUtility.formatDate(dateNow.ToString());

            if (!IsPostBack)
            {
                // Declare Session Variable
                Session["RoomType"] = new List<RoomType>();

                totalAdults = getTotalAdults();
                totalKids = getTotalKids();

                displayDate();

                displayArrivalInfo();

                displayDepartureInfo();

                displayTotalInHouseGuest();

                displayTotalBlockedRoom();

                getRooms();
                getAvailableRoom();
                getTotalRoom();
                getTotalAvailableRoom();
                displayRoomAvailability();

                displayOccupiedRoom();
            }
            
        }

        private void displayDate()
        {
            lblMonth.Text = dateUtility.getMonth();
            lblDay.Text = dateUtility.getDay();
            lblYear.Text = dateUtility.getYear();

            lblDate.Text = dateUtility.getDate() + ", " + dateUtility.getDayOfWeek();
        }

        private void displayArrivalInfo()
        {
            lblTotalArrival.Text = getTotalArrival();
            lblTotalArrived.Text = getTotalArrived();
        }

        private void displayDepartureInfo()
        {
            lblTotalDeparture.Text = getTotalDeparture();
            lblTotalDeparted.Text = getTotalDeparted();
        }

        private string getTotalArrival()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalArrival = "SELECT COUNT(*) FROM Reservation WHERE CheckInDate LIKE @todaysDate";

            SqlCommand cmdGetTotalArrival = new SqlCommand(getTotalArrival, conn);

            cmdGetTotalArrival.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalArrival = (int)cmdGetTotalArrival.ExecuteScalar();

            conn.Close();

            return totalArrival.ToString();
        }

        private string getTotalArrived()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalArrived = "SELECT COUNT(*) FROM Reservation WHERE CheckInDate LIKE @todaysDate AND Status LIKE 'Checked In'";

            SqlCommand cmdGetTotalArrived = new SqlCommand(getTotalArrived, conn);

            cmdGetTotalArrived.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalArrived = (int)cmdGetTotalArrived.ExecuteScalar();

            conn.Close();

            return totalArrived.ToString();
        }

        private string getTotalDeparture()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalDeparture = "SELECT COUNT(*) FROM Reservation WHERE CheckOutDate LIKE @todaysDate";

            SqlCommand cmdGetTotalDeparture = new SqlCommand(getTotalDeparture, conn);

            cmdGetTotalDeparture.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalDeparture = (int)cmdGetTotalDeparture.ExecuteScalar();

            conn.Close();

            return totalDeparture.ToString();
        }

        private string getTotalDeparted()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalDeparted = "SELECT COUNT(*) FROM Reservation WHERE CheckOutDate LIKE @todaysDate AND Status LIKE 'Checked Out'";

            SqlCommand cmdGetTotalDeparted = new SqlCommand(getTotalDeparted, conn);

            cmdGetTotalDeparted.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalDeparted = (int)cmdGetTotalDeparted.ExecuteScalar();

            conn.Close();

            return totalDeparted.ToString();
        }

        private int getTotalAdults()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalAdults = "SELECT SUM(RR.Adults) " +
                                    "FROM Reservation R, ReservationRoom RR " +
                                    "WHERE R.Status LIKE 'Checked In' AND R.ReservationID LIKE RR.ReservationID AND RR.Date LIKE @todaysDate";

            SqlCommand cmdGetTotalAdults = new SqlCommand(getTotalAdults, conn);

            cmdGetTotalAdults.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalAdults = (int)cmdGetTotalAdults.ExecuteScalar();

            conn.Close();

            return totalAdults;
        }

        private int getTotalKids()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalKids = "SELECT SUM(RR.Kids) " +
                                    "FROM Reservation R, ReservationRoom RR " +
                                    "WHERE R.Status LIKE 'Checked In' AND R.ReservationID LIKE RR.ReservationID AND RR.Date LIKE @todaysDate";

            SqlCommand cmdGetTotalKids = new SqlCommand(getTotalKids, conn);

            cmdGetTotalKids.Parameters.AddWithValue("@todaysDate", todaysDate);

            int totalKids = (int)cmdGetTotalKids.ExecuteScalar();

            conn.Close();

            return totalKids;
        }

        private void displayTotalInHouseGuest()
        {
            lblTotalInHouseGuest.Text = (totalAdults + totalKids).ToString();
        }

        private void getRooms()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            conn = new SqlConnection(strCon);
            conn.Open();

            string getAllRoomTypes = "SELECT RoomTypeID FROM RoomType";

            SqlCommand cmdGetAllRoomTypes = new SqlCommand(getAllRoomTypes, conn);

            SqlDataReader sdr = cmdGetAllRoomTypes.ExecuteReader();

            while (sdr.Read())
            {
                roomTypes.Add(new RoomType(sdr["RoomTypeID"].ToString()));
            }

            conn.Close();
        }

        private void getAvailableRoom()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            for(int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> roomOccupancy = roomTypes[i].roomOccupancies;

                for(int j = 0; j < roomOccupancy.Count; j++)
                {
                    string roomID = roomOccupancy[j].roomID;

                    string checkIfRented = "SELECT COUNT(*) FROM ReservationRoom WHERE RoomID LIKE @RoomID AND Date LIKE @todaysDate";

                    SqlCommand cmdCheckIfRented = new SqlCommand(checkIfRented, conn);

                    cmdCheckIfRented.Parameters.AddWithValue("@RoomID", roomID);
                    cmdCheckIfRented.Parameters.AddWithValue("@todaysDate", todaysDate);

                    int rented = (int)cmdCheckIfRented.ExecuteScalar();

                    if(rented > 0)
                    {
                        roomOccupancy[j].available = false;
                    }

                }
            }

            conn.Close();
        }

        private void getTotalRoom()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            for(int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> roomOccupancy = roomTypes[i].roomOccupancies;

                for(int j = 0; j < roomOccupancy.Count; j++)
                {
                    if (roomOccupancy[j].status == "Active")
                    {
                        totalRoom += 1;
                    }
                }
            }
        }

        private void getTotalAvailableRoom()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            for (int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> roomOccupancy = roomTypes[i].roomOccupancies;

                for (int j = 0; j < roomOccupancy.Count; j++)
                {
                    if (roomOccupancy[j].status == "Active" && roomOccupancy[j].available == true)
                    {
                        availableRoom += 1;
                    }
                }
            }
        }

        private void displayRoomAvailability()
        {
            lblTotalAvailableRoom.Text = availableRoom.ToString();
            lblRoomAvailability.Text = availableRoom.ToString() + "/" + totalRoom.ToString();
        }

        private void displayOccupiedRoom()
        {
            double occupancy = ((Convert.ToDouble(totalRoom) - Convert.ToDouble(availableRoom)) / Convert.ToDouble(totalRoom)) * 100.00;

            lblTotalOccupiedRoom.Text = (totalRoom - availableRoom).ToString();
            lblOccupancy.Text = string.Format("{0:0.00}", occupancy) + "%";
        }

        private string getTotalBlockedRoom()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalBlockedRoom = "SELECT COUNT(*) FROM Room WHERE Status LIKE 'Blocked'";

            SqlCommand cmdGetTotalBlockedRoom = new SqlCommand(getTotalBlockedRoom, conn);

            int totalBlockedRoom = (int)cmdGetTotalBlockedRoom.ExecuteScalar();

            conn.Close();

            return totalBlockedRoom.ToString();
        }

        private void displayTotalBlockedRoom()
        {
            lblTotalBlockedRoom.Text = getTotalBlockedRoom();
        }

    }
}