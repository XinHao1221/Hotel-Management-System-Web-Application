/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public class ReservationDetail : IHttpModule
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

        public string guestID { get; set; }
        public string guestName { get; set; }
        public string checkInDate { get; set; }
        public string checkOutDate { get; set; }
        public List<ReservationRoom> reservedRoom { get; set; }
        public List<ReservationFacility> rentedFacility { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public ReservationDetail()
        {

        }

        public ReservationDetail(string guestID, string checkInDate, string checkOutDate, List<ReservationRoom> reservedRoom, List<ReservationFacility> rentedFacility)
        {
            this.guestID = guestID;
            this.guestName = setGuestName();
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.reservedRoom = reservedRoom;
            this.rentedFacility = rentedFacility;
        }

        public string setGuestName()
        {
            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get Guest Name
            string getGuestName = "SELECT Name FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ID", guestID);

            string guestName = (string)cmdGetGuestName.ExecuteScalar();

            conn.Close();

            return guestName;
        }


    }
}
