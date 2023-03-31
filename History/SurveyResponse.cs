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

namespace Hotel_Management_System.History
{
    public class SurveyResponse : IHttpModule
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

        public string reservationID { get; set; }

        public string guestName { get; set; }

        public string idNo { get; set; }

        public string checkInDate { get; set; }

        public string checkOutDate { get; set; }

        public List<SurveyAnswer> surveyAnswers { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public SurveyResponse()
        {

        }

        public SurveyResponse(string reservationID)
        {
            this.reservationID = reservationID;
            getGuestNameAndIDNo();
            getCheckInCheckOutDate();
        }

        private void getGuestNameAndIDNo()
        {
            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getGuestName = "SELECT G.Name, G.IDNo FROM Guest G, Reservation R WHERE R.ReservationID LIKE @ID AND " +
                                    "R.GuestID LIKE G.GuestID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetGuestName.ExecuteReader();

            while (sdr.Read())
            {
                guestName = sdr["Name"].ToString();
                idNo = sdr["IDNo"].ToString();
            }

            conn.Close();
        }

        private void getCheckInCheckOutDate()
        {
            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getCheckInCheckOutDate = "SELECT CheckInDate, CheckOutDate FROM Reservation WHERE ReservationID LIkE @ID";

            SqlCommand cmdGetCheckInCheckOutDate = new SqlCommand(getCheckInCheckOutDate, conn);

            cmdGetCheckInCheckOutDate.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetCheckInCheckOutDate.ExecuteReader();

            while (sdr.Read())
            {
                checkInDate = sdr["CheckInDate"].ToString();
                checkOutDate = sdr["CheckOutDate"].ToString();
            }

            conn.Close();
        }
    }
}
