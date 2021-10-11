using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public class ReservedRoom : IHttpModule
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

        public string reservationRoomID {get; set;}
        public string roomTypeID { get; set; }
        public string roomTypeName { get; set; }
        public double roomPrice { get; set; }
        public string roomNo { get; set; }
        public string roomID { get; set; }
        public int adults { get; set; }
        public int kids { get; set; }
        public double extraBedPrice { get; set; }
        public string date { get; set; }


        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public ReservedRoom()
        {

        }

        public ReservedRoom(string reservationRoomID, string roomTypeID, int adults, int kids, double roomPrice, double extraBedPrice, string date)
        {
            this.reservationRoomID = reservationRoomID;
            this.roomTypeID = roomTypeID;
            this.roomTypeName = getRoomTypeName(roomTypeID);
            this.adults = adults;
            this.kids = kids;
            this.roomPrice = roomPrice;
            this.extraBedPrice = extraBedPrice;
            this.date = date;
        }

        private string getRoomTypeName(string roomTypeID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomType = "SELECT Title FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            String roomType = (String)cmdGetRoomType.ExecuteScalar();

            conn.Close();

            return roomType;

        }

        public void getExtraBedPrice()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getExtraBedPrice = "SELECT ExtraBedPrice FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetExtraRoomPrice = new SqlCommand(getExtraBedPrice, conn);

            cmdGetExtraRoomPrice.Parameters.AddWithValue("@ID", roomTypeID);

            decimal extraBedPrice = (decimal)cmdGetExtraRoomPrice.ExecuteScalar();

            conn.Close();

            this.extraBedPrice = Convert.ToDouble(extraBedPrice);
        }



    }
}
