using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class Refund : System.Web.UI.Page
    {
        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        string reservationID;

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            if (!IsPostBack)
            {
                setReservationDetails();

                setItemToRepeaterRentedRoomType();

                setItemToRepeaterRentedFacility();

                calcTotalPayment();
            }
            
        }

        private void setReservationDetails()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Set the reservation details into label
            lblGuestName.Text = getGuestName(reservationDetails.guestID);
            lblCheckInDate.Text = reservationDetails.checkInDate;
            lblCheckOutDate.Text = reservationDetails.checkOutDate;
            lblDurationOfStay.Text = reservationUtility.getdurationOfStay(reservationDetails.checkInDate, reservationDetails.checkOutDate).ToString();

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();
        }

        private string getGuestName(string guestID)
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getGuestName = "SELECT Name FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ID", guestID);

            string guestName = (string)cmdGetGuestName.ExecuteScalar();

            conn.Close();

            return guestName;
        }

        private void setItemToRepeaterRentedRoomType()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservationRooms = reservationDetails.reservedRoom;

            RepeaterReservedRoom.DataSource = reservationRooms;
            RepeaterReservedRoom.DataBind();
        }

        private void setItemToRepeaterRentedFacility()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            RepeaterRentedFacility.DataSource = reservationFacilities;
            RepeaterRentedFacility.DataBind();
        }

        protected void RepeaterReservedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblDate = e.Item.FindControl("lblDate") as Label;
            Label lblExtraBedPrice = e.Item.FindControl("lblExtraBedPrice") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);
            lblDate.Text = formatedDate.ToShortDateString();

            // Format extraBedPrice
            if (lblExtraBedPrice.Text == "-1.00")
            {
                lblExtraBedPrice.Text = "0.00";
            }
        }

        protected void RepeaterRentedFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblRentDate = e.Item.FindControl("lblRentDate") as Label;
            Label lblReturnDate = e.Item.FindControl("lblReturnDate") as Label;

            // Format date base on date format on user's computer
            DateTime formatedRentDate = Convert.ToDateTime(lblRentDate.Text);
            DateTime formatedReturnDate = Convert.ToDateTime(lblReturnDate.Text);

            // Display the formated date
            lblRentDate.Text = formatedRentDate.ToShortDateString();
            lblReturnDate.Text = formatedReturnDate.ToShortDateString();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Redirect to view page
            Response.Redirect("ViewReservation.aspx?ID=" + en.encryption(reservationID));
        }

        private void calcTotalPayment()
        {
            // Calculate total payment and display on screen
            
            double totalPayment = 0;

            // Get the list of rented room and facility entered previously
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            List<ReservationRoom> reservationRooms = reservationDetails.reservedRoom;

            // Accumulate total room price
            for (int i = 0; i < reservationRooms.Count; i++)
            {
                totalPayment += reservationRooms[i].roomPrice;

                if (reservationRooms[i].extraBedPrice != -1)
                {
                    totalPayment += reservationRooms[i].extraBedPrice;
                }
            }

            // Accumulate facility details
            for (int j = 0; j < reservationFacilities.Count; j++)
            {
                totalPayment += reservationFacilities[j].subTotal;
            }

            // Display total
            lblTotal.Text = string.Format("{0:0.00}", totalPayment);

            // Calculate grand total
            totalPayment += calcTaxCharges(totalPayment);

            // Display grand total
            lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment);

        }

        private double calcTaxCharges(double totalPayment)
        {
            // Calculate tax

            double tax = 0;

            // Get the tax rate from application variable
            tax = totalPayment * (double)Application["TaxRate"];

            // Display total tax charges
            lblTax.Text = string.Format("{0:0.00}", tax);

            return tax;
        }
    }
}