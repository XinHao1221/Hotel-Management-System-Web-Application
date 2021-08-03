using Hotel_Management_System.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class Payment : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            setReservationDetails();
        }

        private void setReservationDetails()
        {
            Reservation reservation = (Reservation)Session["ReservationDetails"];

            lblGuestName.Text = reservation.guestName;
            lblCheckInDate.Text = reservation.checkInDate;
            lblCheckOutDate.Text = reservation.checkOutDate;
            lblDurationOfStay.Text = reservationUtility.getdurationOfStay(reservation.checkInDate, reservation.checkOutDate).ToString();
        }
    }
}