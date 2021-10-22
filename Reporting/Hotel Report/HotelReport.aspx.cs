using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Reporting.Hotel_Report
{
    public partial class HotelReport : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Hold today's date
        string todaysDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current date
            DateTime dateNow = DateTime.Now;
            todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Display current date
            lblReportDate.Text = Convert.ToDateTime(todaysDate).ToShortDateString();
        }
    }
}