using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public class AvailableRoom : IHttpModule
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

        public string roomID {get; set;}
        public string roomNo { get; set; }
        public string houseKeepingStatus { get; set; }

        Boolean selected { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public AvailableRoom()
        {

        }

        public AvailableRoom(string roomID)
        {
            this.roomID = roomID;
            this.selected = false;
            getRoomNo();
            getHouseKeepingStatus();
        }

        private void getRoomNo()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomNo = "SELECT RoomNumber FROM Room WHERE RoomID LIKE @ID";

            SqlCommand cmdGetRoomNo = new SqlCommand(getRoomNo, conn);

            cmdGetRoomNo.Parameters.AddWithValue("@ID", roomID);

            roomNo = (string)cmdGetRoomNo.ExecuteScalar();

            conn.Close();
        }

        private void getRoomFeature()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomFeature = "SELECT * FROM Feature WHERE RoomID LIKE @ID";

            SqlCommand cmdGetRoomFeature = new SqlCommand(getRoomFeature, conn);

            cmdGetRoomFeature.Parameters.AddWithValue("@ID", roomID);

            SqlDataReader sdr = cmdGetRoomFeature.ExecuteReader();

            while (sdr.Read())
            {
                //roomFeatures += sdr["Title"].ToString() + ", ";
            }

            conn.Close();

            // Remove ", " at the back of the string
            //if(roomFeatures.Length > 0)
            //{
            //    roomFeatures.Remove((roomFeatures.Length - 3), 2);
            //}
        }

        private void getHouseKeepingStatus()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getHouseKeepingStatus = "SELECT HousekeepingStatus FROM Room WHERE RoomID LIKE @ID";

            SqlCommand cmdGetHouseKeepingStatus = new SqlCommand(getHouseKeepingStatus, conn);

            cmdGetHouseKeepingStatus.Parameters.AddWithValue("@ID", roomID);

            houseKeepingStatus = (string)cmdGetHouseKeepingStatus.ExecuteScalar();

            conn.Close();
        }
    }
}
