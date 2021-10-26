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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

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

        //private string todaysDate = "2021-10-18";
        private string todaysDate;
        private int totalAdults;
        private int totalKids;
        private int totalRoom = 0;
        private int availableRoom = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current date
            DateTime dateNow = DateTime.Now;
            todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Page TItle
            Page.Title = "Dashboard";

            //if (!IsPostBack)
            //{
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

                setItemToRepeaterRoomAvailability();

                displayGuestInHouseChart();
            //}
            
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

            string getTotalArrival = "SELECT COUNT(*) FROM Reservation WHERE CheckInDate LIKE @todaysDate AND Status IN ('Created', 'Checked In')";

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

            string getTotalDeparture = "SELECT COUNT(*) FROM Reservation WHERE CheckOutDate LIKE @todaysDate AND Status IN ('Checked In', 'Checked Out')";

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
            totalAdults = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalAdults = "SELECT SUM(RR.Adults) " +
                                    "FROM Reservation R, ReservationRoom RR " +
                                    "WHERE R.Status LIKE 'Checked In' AND R.ReservationID LIKE RR.ReservationID";

            SqlCommand cmdGetTotalAdults = new SqlCommand(getTotalAdults, conn);

            cmdGetTotalAdults.Parameters.AddWithValue("@todaysDate", todaysDate);

            try
            {
                totalAdults = (int)cmdGetTotalAdults.ExecuteScalar();
            }
            catch
            {

            }

            conn.Close();

            return totalAdults;
        }

        private int getTotalKids()
        {
            totalKids = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalKids = "SELECT SUM(RR.Kids) " +
                                    "FROM Reservation R, ReservationRoom RR " +
                                    "WHERE R.Status LIKE 'Checked In' AND R.ReservationID LIKE RR.ReservationID";

            SqlCommand cmdGetTotalKids = new SqlCommand(getTotalKids, conn);

            cmdGetTotalKids.Parameters.AddWithValue("@todaysDate", todaysDate);

            try
            {
                totalKids = (int)cmdGetTotalKids.ExecuteScalar();
            }
            catch
            {

            }
            

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

            string getAllRoomTypes = "SELECT RoomTypeID FROM RoomType WHERE Status LIKE 'Active'";

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

                    string checkIfRented = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " +
                                            "WHERE RR.RoomID LIKE @RoomID AND RR.Date LIKE @todaysDate " +
                                            "AND R.ReservationID LIKE RR.ReservationID AND R.Status IN ('Created', 'Checked In')";

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

        private void setItemToRepeaterRoomAvailability()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            RepeaterRoomAvailability.DataSource = roomTypes;
            RepeaterRoomAvailability.DataBind();
        }

        protected void RepeaterRoomAvailability_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int total = 0, sold = 0;

            // Get control's reference
            Label lblRoomTypeID = e.Item.FindControl("lblRoomTypeID") as Label;
            Label lblAvailableRoom = e.Item.FindControl("lblAvailableRoom") as Label;
            Label lblSold = e.Item.FindControl("lblSold") as Label;
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;

            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            for(int i = 0; i < roomTypes.Count; i++)
            {
                if(roomTypes[i].roomTypeID == lblRoomTypeID.Text)
                {
                    List<RoomOccupancy> roomOccupancies = roomTypes[i].roomOccupancies;

                    for (int j = 0; j < roomOccupancies.Count; j++)
                    {
                        if(roomOccupancies[j].status == "Active")
                        {
                            total += 1;
                        }

                    }

                    conn = new SqlConnection(strCon);
                    conn.Open();

                    string getSoldRoom = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " +
                                            "WHERE RR.RoomTypeID LIKE @RoomTypeID AND RR.Date LIKE @todaysDate AND " +
                                            "R.ReservationID LIKE RR.ReservationID " +
                                            "AND R.Status IN ('Created', 'Checked In')";

                    SqlCommand cmdGetSoldRoom = new SqlCommand(getSoldRoom, conn);

                    cmdGetSoldRoom.Parameters.AddWithValue("@RoomTypeID", roomTypes[i].roomTypeID);
                    cmdGetSoldRoom.Parameters.AddWithValue("@todaysDate", todaysDate);

                    sold = (int)cmdGetSoldRoom.ExecuteScalar();

                    conn.Close();
                }
            }

            lblAvailableRoom.Text = (total - sold).ToString();
            lblSold.Text = sold.ToString();

            if(total > sold)
            {
                lblStatus.Text = "Available";
                lblStatus.Style["color"] = "rgb(0, 206, 27)";
            }
            else
            {
                lblStatus.Text = "Sold Out";
                lblStatus.Style["color"] = "red";
            }
            
        }

        private void displayGuestInHouseChart()
        {

            String[] x = { "Adults", "Kids" };
            int[] y = { totalAdults, totalKids };

            ChartGuestInHouse.Series[0].Points.DataBindXY(x, y);

            ChartGuestInHouse.Series[0].BorderWidth = 5;
            ChartGuestInHouse.Series[0].ChartType = SeriesChartType.Pie;

            ChartGuestInHouse.Legends[0].Enabled = true;

            // Set chart's tooltip
            foreach(Series s in ChartGuestInHouse.Series)
            {
                s.Label = "#VALX #VALY";
                s["PieLabelStyle"] = "Outside";
                s.ToolTip = "#VALX\n\nPercentage: #PERCENT\n\nTotal: #VALY";
            }

            // Code to customize color of pie chart
            foreach (Series charts in ChartGuestInHouse.Series)
            {
                foreach (DataPoint dp in charts.Points)
                {
                    switch (dp.AxisLabel)
                    {
                        case "Adults": dp.Color = System.Drawing.Color.FromArgb(255, 203, 59); break;
                        case "Kids": dp.Color = System.Drawing.Color.FromArgb(255, 72, 0); break;
                    }

                }
            }
        }
    }
}