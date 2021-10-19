using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

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
    }
}
