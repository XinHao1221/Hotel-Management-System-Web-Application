using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Hotel_Management_System.Dashboard
{
    public class RoomOccupancy : IHttpModule
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

        public string roomID { get; set; }

        public string roomNo { get; set; }

        public string status { get; set; }

        public string floor { get; set; }

        public string houseKeepingStatus { get; set; }

        public Boolean available { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public RoomOccupancy()
        {

        }

        public RoomOccupancy(string roomID)
        {
            this.roomID = roomID;
            getRoomInformation();
            available = true;
        }

        public RoomOccupancy(string roomID, Boolean available)
        {
            this.roomID = roomID;
            this.available = available;
            getRoomInformation();
        }

        private void getRoomInformation()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomInformation = "SELECT RoomNumber, CONVERT(NVARCHAR(10), F.FloorNumber) + '  -  ' + F.FloorName AS FloorNo, " +
                                        "R.Status, R.HousekeepingStatus " +
                                        "FROM Room R, Floor F " +
                                        "WHERE RoomID LIKE @RoomID  AND R.FloorID LIKE F.FloorID";

            SqlCommand cmdGetRoomInformation = new SqlCommand(getRoomInformation, conn);

            cmdGetRoomInformation.Parameters.AddWithValue("@RoomID", roomID);

            SqlDataReader sdr = cmdGetRoomInformation.ExecuteReader();

            while (sdr.Read())
            {
                roomNo = sdr["RoomNumber"].ToString();
                floor = sdr["FloorNo"].ToString();
                status = sdr["Status"].ToString();
                houseKeepingStatus = sdr["HousekeepingStatus"].ToString();
            }

            conn.Close();

        }
    }
}
