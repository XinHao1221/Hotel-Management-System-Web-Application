using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Dashboard
{
    public class RoomType : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public string roomTypeID { get; set; }

        public string roomTypeName { get; set; }

        public List<RoomOccupancy> roomOccupancies = new List<RoomOccupancy>();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public RoomType()
        {

        }

        public RoomType(string roomTypeID)
        {
            this.roomTypeID = roomTypeID;
            getRoomTypeName();
            getRooms();
            
        }

        private void getRoomTypeName()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomTypeName = "SELECT Title FROM RoomType WHERE RoomTypeID LIKE @RoomTypeID";

            SqlCommand cmdGetRoomTypeName = new SqlCommand(getRoomTypeName, conn);

            cmdGetRoomTypeName.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            roomTypeName = (string)cmdGetRoomTypeName.ExecuteScalar();

            conn.Close();
        }

        private void getRooms()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRooms = "SELECT RoomID FROM Room WHERE RoomTypeID LIKE @RoomTypeID AND Status IN ('Active', 'Blocked')";

            SqlCommand cmdGetRooms = new SqlCommand(getRooms, conn);

            cmdGetRooms.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            SqlDataReader sdr = cmdGetRooms.ExecuteReader();

            while (sdr.Read())
            {
                roomOccupancies.Add(new RoomOccupancy(sdr["RoomID"].ToString()));
            }

            conn.Close();

        }

        public void getOverTimeReservation()
        {
            // Hold a list of reservationID
            List<String> reservationIDs = new List<string>();

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            conn = new SqlConnection(strCon);
            conn.Open();

            // Get overtime reservationID
            string getOvertimeReservation = "SELECT * FROM Reservation R WHERE R.CheckOutDate <= @Date " +
                                            "AND R.Status LIKE 'Checked In'";

            SqlCommand cmdGetOvertimeReservation = new SqlCommand(getOvertimeReservation, conn);

            cmdGetOvertimeReservation.Parameters.AddWithValue("@Date", todaysDate);

            SqlDataReader sdr = cmdGetOvertimeReservation.ExecuteReader();

            while (sdr.Read())
            {
                reservationIDs.Add(sdr["ReservationID"].ToString());
            }

            conn.Close();

            getOvertimeReservedRoom(reservationIDs);
        }

        private void getOvertimeReservedRoom(List<String> reservationIDs)
        {
            // Get room for overtime reservation
            for (int i = 0; i < reservationIDs.Count; i++)
            {
                string lastReservationDate = getLastReservationDate(reservationIDs[i]);

                // Hold current avialable room  temporary
                List<RoomOccupancy> temp = roomOccupancies;

                conn = new SqlConnection(strCon);
                conn.Open();

                string overtimeRooms = "SELECT * FROM ReservationRoom " +
                                        "WHERE ReservationID LIKE @ID AND Date LIKE @Date";

                SqlCommand cmdOvertimeRooms = new SqlCommand(overtimeRooms, conn);

                cmdOvertimeRooms.Parameters.AddWithValue("@ID", reservationIDs[i]);
                cmdOvertimeRooms.Parameters.AddWithValue("@Date", lastReservationDate);

                SqlDataReader sdr = cmdOvertimeRooms.ExecuteReader();

                while (sdr.Read())
                {
                    for (int j = 0; j < temp.Count; j++)
                    {
                        // If the overtime room is found
                        if (sdr["RoomID"].ToString() == temp[j].roomID)
                        {
                            
                            temp[j].available = false;

                            // check if room is overtime
                            if (isOvertime(lastReservationDate))
                            {
                                temp[j].overtime = true;
                            }
                            
                        }
                    }

                }

                conn.Close();

                // Assign the updated available room's list into available room
                roomOccupancies = temp;
            }
        }

        // Get last date for the reservation
        private string getLastReservationDate(string reservationID)
        {
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

        private Boolean isOvertime(string lastReservationDate)
        {
            // Get actual check out date
            DateTime checkOutDate = Convert.ToDateTime(lastReservationDate);
            checkOutDate = checkOutDate.AddDays(1);

            DateTime dateNow = DateTime.Now;


            // Get current date
            if (checkOutDate.Date < dateNow.Date)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
