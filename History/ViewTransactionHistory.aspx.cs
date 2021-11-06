using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;
using Hotel_Management_System.Front_Desk.CheckOut;
using System.Configuration;
using Hotel_Management_System.History;

namespace Hotel_Management_System.Cashiering
{
    public partial class ViewTransactionHistory : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        private String reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Transaction History Details";

            if (!IsPostBack)
            {
                if (surveyResponseExists())
                {
                    LBSurveyResponse.Visible = true;
                }
                else
                {
                    LBSurveyResponse.Visible = false;
                }

                Session["ReservationDetails"] = new ReservationDetail();

                Session["ReservedRoomType"] = new List<ReservedRoomType>();

                Session["ServiceCharges"] = new List<ServiceCharges>();

                Session["MissingEquipments"] = new List<MissingEquipment>();

                Session["Payments"] = new List<Payment>();

                getReservationDetails();

                getReservedRoom();

                getRentedFacilityList();

                getServiceCharges();

                getMissingeEquipment();

                getPayment();

                setStayDetails();

                setRentedRoomToRepeater();

                setPaymentDetailsToRepeater();

                setRentedFacilityToRepeater();

                setServiceChargesToRepeater();

                setMissingEquipmentToRepeater();

                setRoomMoveHistory();
            }
            
        }

        private Boolean surveyResponseExists()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalSurveyAnswer = "SELECT COUNT(*) " +
                                            "FROM SurveyAnswer SA, Survey S " +
                                            "WHERE S.ReservationID LIKE @ID AND S.SurveyID LIKE SA.SurveyID";

            SqlCommand cmdGetTotalSurveyAnswer = new SqlCommand(getTotalSurveyAnswer, conn);

            cmdGetTotalSurveyAnswer.Parameters.AddWithValue("@ID", reservationID);

            int count = (int)cmdGetTotalSurveyAnswer.ExecuteScalar();

            conn.Close();

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("History.aspx");
        }

        private void getReservationDetails()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            conn = new SqlConnection(strCon);
            conn.Open();

            String getReservationDetails = "SELECT * FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservationDetails = new SqlCommand(getReservationDetails, conn);

            cmdGetReservationDetails.Parameters.AddWithValue("ID", reservationID);

            SqlDataReader sdr = cmdGetReservationDetails.ExecuteReader();

            // Set details into ReservationDetail object
            if (sdr.Read())
            {
                reservation.checkInDate = sdr.GetString(sdr.GetOrdinal("CheckInDate"));
                string temp = sdr.GetString(sdr.GetOrdinal("CheckInDate"));
                reservation.checkOutDate = sdr.GetString(sdr.GetOrdinal("CheckOutDate"));
                reservation.guestID = sdr.GetString(sdr.GetOrdinal("GuestID"));
            }

            conn.Close();
        }

        private void getReservedRoom()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = new List<ReservationRoom>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservedRoom = "SELECT * FROM ReservationRoom WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservedRoom = new SqlCommand(getReservedRoom, conn);

            cmdGetReservedRoom.Parameters.AddWithValue("ID", reservationID);

            SqlDataReader sdr = cmdGetReservedRoom.ExecuteReader();

            ReservationRoom rr;

            while (sdr.Read())
            {
                if (sdr["ExtraBed"].ToString() == "True")
                {
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), sdr["RoomID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), Convert.ToDouble(sdr["ExtraBedCharges"]), sdr["Date"].ToString());
                }
                else
                {
                    rr = new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), sdr["RoomID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), -1, sdr["Date"].ToString());
                }

                reservedRooms.Add(rr);

            }
            conn.Close();

            reservation.reservedRoom = reservedRooms;
        }

        // Get reservation facility from database
        private List<ReservationFacility> getReservationFacility()
        {
            List<ReservationFacility> reservationFacility = new List<ReservationFacility>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservedRoom = "SELECT * FROM ReservationFacility WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservedRoom = new SqlCommand(getReservedRoom, conn);

            cmdGetReservedRoom.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetReservedRoom.ExecuteReader();

            ReservationFacility rf;

            while (sdr.Read())
            {
                rf = new ReservationFacility(sdr["ReservationFacilityID"].ToString(), sdr["FacilityID"].ToString(), Convert.ToDouble(sdr["Price"]), sdr["DateRented"].ToString(), Convert.ToInt32(sdr["Group"]));

                reservationFacility.Add(rf);
            }

            conn.Close();

            // Get one last record, contain nothing
            // To let program know, the list is ended
            rf = new ReservationFacility("", "", 0.00, "", 0);
            reservationFacility.Add(rf);

            return reservationFacility;
        }

        // Format reservation facility into a list
        private void getRentedFacilityList()
        {
            // A list of reservation facility from database
            List<ReservationFacility> temp = getReservationFacility();

            // Formated facility list to be displayed on the screen
            List<ReservationFacility> reservationFacilities = new List<ReservationFacility>();

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

#pragma warning disable CS0168 // The variable 'facilityID' is declared but never used
            string rentDate, facilityID, returnDate;
#pragma warning restore CS0168 // The variable 'facilityID' is declared but never used
            int qty = 0, group;
#pragma warning disable CS0219 // The variable 'counter' is assigned but its value is never used
            int counter;
#pragma warning restore CS0219 // The variable 'counter' is assigned but its value is never used

            // Check if it contains at least one element
            // Got one default element
            // Therefore start with 1
            if (temp.Count > 1)
            {
                int i = 0;

                while ((i < temp.Count) && (temp[i].group != 0))
                {

                    int test = temp[i].group;

                    counter = 0;

                    rentDate = temp[i].rentDate;
                    returnDate = temp[i].rentDate;

                    group = temp[i].group;

                    qty = 1;

                    if ((i < temp.Count - 1))
                    {
                        if (temp[i + 1].group == group)
                        {
                            do
                            {
                                if (temp[i + 1].rentDate == rentDate)
                                {
                                    qty++;
                                    i++;

                                }
                                else if ((temp[i + 1].group == group))
                                {
                                    returnDate = temp[i + 1].rentDate;
                                    i++;
                                }

                            } while ((temp[i + 1].group == group));


                        }
                    }

                    // Add one into return date
                    returnDate = reservationUtility.getNextDate(returnDate);

                    ReservationFacility rf = new ReservationFacility(temp[i].reservationFacilityID, temp[i].facilityID,
                                                                            qty, temp[i].price, rentDate, returnDate);

                    reservationFacilities.Add(rf);

                    i++;

                }


            }

            reservation.rentedFacility = reservationFacilities;
        }

        private void getMissingeEquipment()
        {
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            conn = new SqlConnection(strCon);
            conn.Open();

            string getMissingEquipment = "SELECT * FROM OtherCharges WHERE ReservationID LIKE @ID AND Category LIKE 'Fine'";

            SqlCommand cmdGetMissingEquipment = new SqlCommand(getMissingEquipment, conn);

            cmdGetMissingEquipment.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetMissingEquipment.ExecuteReader();

            MissingEquipment me;

            while (sdr.Read())
            {
                me = new MissingEquipment(sdr["EquipmentID"].ToString(), sdr["Title"].ToString(), Convert.ToDouble(sdr["Amount"]));

                missingEquipments.Add(me);
            }

            conn.Close();
        }

        private void getServiceCharges()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            conn = new SqlConnection(strCon);
            conn.Open();

            string getServiceCharges = "SELECT * FROM OtherCharges WHERE ReservationID LIKE @ID AND Category LIKE 'Service'";

            SqlCommand cmdGetServiceCharges = new SqlCommand(getServiceCharges, conn);

            cmdGetServiceCharges.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetServiceCharges.ExecuteReader();

            ServiceCharges sc;

            while (sdr.Read())
            {
                sc = new ServiceCharges(sdr["Title"].ToString(), Convert.ToDouble(sdr["Amount"]));

                serviceCharges.Add(sc);
            }

            conn.Close();
        }

        public void getPayment()
        {
            List<Payment> payments = (List<Payment>)Session["Payments"];

            conn = new SqlConnection(strCon);
            conn.Open();

            string getPaymentDetails = "SELECT * FROM Payment WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetPaymentDetails = new SqlCommand(getPaymentDetails, conn);

            cmdGetPaymentDetails.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetPaymentDetails.ExecuteReader();

            Payment pay;

            while (sdr.Read())
            {
                pay = new Payment(sdr["PaymentID"].ToString(), sdr["PaymentMethod"].ToString(), sdr["ReferenceNo"].ToString(), Convert.ToDouble(sdr["Amount"]), sdr["Date"].ToString());

                payments.Add(pay);
            }

            conn.Close();
        }

        private void setStayDetails()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            setGuestInformation(reservation.guestID);

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(reservation.checkInDate);
            DateTime formatedCheckOutDate = Convert.ToDateTime(reservation.checkOutDate);

            lblCheckIn.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOut.Text = formatedCheckOutDate.ToShortDateString();

            string temp = formatedCheckOutDate.ToShortDateString();

            int durationOfStay = reservationUtility.getdurationOfStay(lblCheckIn.Text, lblCheckOut.Text);

            lblDurationOfStay.Text = durationOfStay.ToString() + " Night";
        }

        private void setGuestInformation(string guestID)
        {

            conn = new SqlConnection(strCon);
            conn.Open();

            // Get guest details for the selected guest
            String getGuestDetails = "SELECT * FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestDetails = new SqlCommand(getGuestDetails, conn);

            cmdGetGuestDetails.Parameters.AddWithValue("@ID", guestID);

            SqlDataReader sdr = cmdGetGuestDetails.ExecuteReader();

            // Set guest details into label
            if (sdr.Read())
            {
                lblGuestName.Text = sdr.GetString(sdr.GetOrdinal("Name"));
                lblIDNo.Text = sdr.GetString(sdr.GetOrdinal("IDNo"));
            }

            conn.Close();
        }

        private void setRentedFacilityToRepeater()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            // Hold ReservationFacility
            List<ReservationFacility> reservationFacilities = reservation.rentedFacility;

            if (reservationFacilities.Count != 0)
            {
                // Set data to Facility Repeater
                RepeaterRentedFacility.DataSource = reservationFacilities;
                RepeaterRentedFacility.DataBind();

                lblNoFacilityFound.Visible = false;
            }
            else
            {
                lblNoFacilityFound.Visible = true;
            }

        }

        private void setRentedRoomToRepeater()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            // Hold Reserved Room
            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            // Set rented room to repeater
            RepeaterRentedRoomType.DataSource = reservedRooms;
            RepeaterRentedRoomType.DataBind();
        }

        private void setServiceChargesToRepeater()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            if (serviceCharges.Count != 0)
            {
                // Set data to Facility Repeater
                RepeaterServiceCharges.DataSource = serviceCharges;
                RepeaterServiceCharges.DataBind();

                lblNoServiceCharges.Visible = false;
            }
            else
            {
                lblNoServiceCharges.Visible = true;
            }
        }

        private void setMissingEquipmentToRepeater()
        {
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            if(missingEquipments.Count != 0)
            {
                RepeaterMissingEquipment.DataSource = missingEquipments;
                RepeaterMissingEquipment.DataBind();

                lblNoMissingEquipmentFound.Visible = false;
            }
            else
            {
                lblNoMissingEquipmentFound.Visible = true;
            }
        }

        private void setPaymentDetailsToRepeater()
        {
            List<Payment> payments = (List<Payment>)Session["Payments"];

            if(payments.Count != 0)
            {
                RepeaterPayment.DataSource = payments;
                RepeaterPayment.DataBind();

                lblNoPaymentDetails.Visible = false;
            }
            else
            {
                lblNoPaymentDetails.Visible = true;
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

        protected void RepeaterRentedRoomType_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void IBEditPaymentDetails_Click(object sender, ImageClickEventArgs e)
        {
            // When user click on delete icon
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get value from repeatear
            String paymentID = (item.FindControl("lblPaymentID") as Label).Text;
            String paymentDate = (item.FindControl("lblPaymentDate") as Label).Text;
            String paymentMethod = (item.FindControl("lblPaymentMethod") as Label).Text;
            String referenceNo = (item.FindControl("lblReferenceNo") as Label).Text;
            String amount = (item.FindControl("lblAmount") as Label).Text;

            // Set value to popup box
            lblPopupBoxPaymentID.Text = paymentID;

            DateTime formatedPaymentDate = Convert.ToDateTime(paymentDate);
            lblPopupBoxDate.Text = formatedPaymentDate.ToShortDateString();

            lblPopupBoxAmount.Text = amount;
            ddlPaymentMethod.SelectedValue = paymentMethod;
            txtReferenceNo.Text = referenceNo;

            // Show Popup Box
            PopupBoxEditPaymentDetails.Visible = true;
            PopupCover.Visible = true;
        }

        protected void IBPopupBoxCloseIcon_Click(object sender, ImageClickEventArgs e)
        {
            PopupBoxEditPaymentDetails.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnPopupBoxSave_Click(object sender, EventArgs e)
        {
            // Update payment details
            updatePaymentDetailsToDatabase();
            updateRepeaterPayment();

            // Close Repeater
            PopupBoxEditPaymentDetails.Visible = false;
            PopupCover.Visible = false;
  
        }

        private void updatePaymentDetailsToDatabase()
        {
            // Update payment method and reference no
            conn = new SqlConnection(strCon);
            conn.Open();

            string updatePaymentDetails = "UPDATE Payment SET PaymentMethod = @PaymentMethod, ReferenceNo = @ReferenceNo " +
                "WHERE PaymentID LIKE @ID";

            SqlCommand cmdUpdatePaymentDetails = new SqlCommand(updatePaymentDetails, conn);

            cmdUpdatePaymentDetails.Parameters.AddWithValue("@PaymentMethod", ddlPaymentMethod.SelectedValue);
            cmdUpdatePaymentDetails.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text);
            cmdUpdatePaymentDetails.Parameters.AddWithValue("@ID", lblPopupBoxPaymentID.Text);

            int i = cmdUpdatePaymentDetails.ExecuteNonQuery();

            conn.Close();
        }

        private void updateRepeaterPayment()
        {
            List<Payment> payments = (List<Payment>)Session["Payments"];

            // Find the specific paymentID from list
            for(int i = 0; i < payments.Count; i++)
            {
                if(payments[i].paymentID == lblPopupBoxPaymentID.Text)
                {
                    payments[i].paymentMethod = ddlPaymentMethod.SelectedValue;
                    payments[i].referenceNo = txtReferenceNo.Text;
                }
            }

            // Set data to RepeaterPayment
            RepeaterPayment.DataSource = payments;
            RepeaterPayment.DataBind();
        }

        protected void RepeaterPayment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblPaymentDate = e.Item.FindControl("lblPaymentDate") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblPaymentDate.Text);
            lblPaymentDate.Text = formatedDate.ToShortDateString();

        }

        protected void LBSurveyResponse_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewSurveyResponse.aspx?ID=" + en.encryption(reservationID));
        }

        private void setRoomMoveHistory()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomMoveHistory = "SELECT * FROM RoomMove WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetRoomMoveHistory = new SqlCommand(getRoomMoveHistory, conn);

            cmdGetRoomMoveHistory.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetRoomMoveHistory.ExecuteReader();

            if (sdr.HasRows)
            {
                RepeaterRoomMoveHistory.DataSource = sdr;
                RepeaterRoomMoveHistory.DataBind();

                lblNoRoomMoveHistory.Visible = false;
            }
            else
            {
                RepeaterRoomMoveHistory.DataSource = null;
                RepeaterRoomMoveHistory.DataBind();

                lblNoRoomMoveHistory.Visible = true;
            }

            conn.Close();
        }

        protected void RepeaterRoomMoveHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);
            lblDate.Text = formatedDate.ToShortDateString();
        }
    }
}