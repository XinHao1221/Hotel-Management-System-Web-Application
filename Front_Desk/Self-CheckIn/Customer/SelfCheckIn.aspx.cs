using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class SelfCheckIn : System.Web.UI.Page
    {
        private string reservationID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = "RS10000009";

            displayGuestName();
        }

        private void displayGuestName()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getGuestName = "SELECT G.Title, G.Name " +
                                "FROM Reservation R, Guest G " +
                                "WHERE R.GuestID LIKE G.GuestID " +
                                "AND ReservationID LIKE @ReservationID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ReservationID", reservationID);

            SqlDataReader sdr = cmdGetGuestName.ExecuteReader();

            while (sdr.Read())
            {
                lblGuestName.Text = sdr["Title"].ToString() + " " + sdr["Name"].ToString();
            }

            conn.Close();

        }
    }
}