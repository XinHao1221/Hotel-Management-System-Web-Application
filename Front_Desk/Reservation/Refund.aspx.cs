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

            // Page TItle
            Page.Title = "Refund";

            if (!IsPostBack)
            {
                // Save link for previous page
                ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
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


            if(reservationFacilities.Count > 0)
            {
                RepeaterRentedFacility.DataSource = reservationFacilities;
                RepeaterRentedFacility.DataBind();

                lblNoItemFound.Visible = false;
            }
            else
            {
                lblNoItemFound.Visible = true;
            }
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
            // Redirect to previous page
            Response.Redirect(ViewState["PreviousPage"].ToString());
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
            lblTotal.Text = string.Format("{0:0.00}", totalPayment * -1);

            // Calculate grand total
            totalPayment += calcTaxCharges(totalPayment);

            // Display grand total
            lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment * -1);

        }

        private double calcTaxCharges(double totalPayment)
        {
            // Calculate tax

            double tax = 0;

            // Get the tax rate from application variable
            tax = totalPayment * (double)Application["TaxRate"];

            // Display total tax charges
            lblTax.Text = string.Format("{0:0.00}", tax * -1);

            return tax;
        }

        private void deletePaymentDetails()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string deletePaymentDetails = "DELETE FROM Payment WHERE ReservationID LIKE @ID";

            SqlCommand cmdDeletePaymentDetails = new SqlCommand(deletePaymentDetails, conn);

            cmdDeletePaymentDetails.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdDeletePaymentDetails.ExecuteNonQuery();

            conn.Close();
        }

        private void deleteReservationRoom()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string deleteReservationReservationRoom = "DELETE FROM ReservationRoom WHERE ReservationID LIKE @ID";

            SqlCommand cmdDeleteReservationRoom = new SqlCommand(deleteReservationReservationRoom, conn);

            cmdDeleteReservationRoom.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdDeleteReservationRoom.ExecuteNonQuery();

            conn.Close();
        }

        private void deleteReservationFacility()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string deleteReservationFacility = "DELETE FROM ReservationFacility WHERE ReservationID LIKE @ID";

            SqlCommand cmdDeleteReservationFacility = new SqlCommand(deleteReservationFacility, conn);

            cmdDeleteReservationFacility.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdDeleteReservationFacility.ExecuteNonQuery();

            conn.Close();
        }

        private void deleteReservationDetails()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string deleteReservationDetails = "DELETE FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdDeleteReservationDetails = new SqlCommand(deleteReservationDetails, conn);

            cmdDeleteReservationDetails.Parameters.AddWithValue("@ID", reservationID);

            int i = cmdDeleteReservationDetails.ExecuteNonQuery();

            conn.Close();
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            PopupRefund.Visible = true;
            PopupCover.Visible = true;
        }

        protected void formBtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reservation.aspx");
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupRefund.Visible = false;
        }

        protected void btnPopupConfirmCancelReservation_Click(object sender, EventArgs e)
        {
            deletePaymentDetails();
            deleteReservationFacility();
            deleteReservationRoom();
            deleteReservationDetails();

            Response.Redirect("Reservation.aspx");
        }
    }
}