using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using System.Configuration;
using Hotel_Management_System.Front_Desk.CheckIn;

namespace Hotel_Management_System.Front_Desk.GuestInHouse
{
    public partial class RoomMove : System.Web.UI.Page
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
            Page.Title = "Room Move";

            if (!IsPostBack)
            {
                // Save link for previous page
                ViewState["PreviousPage"] = Request.UrlReferrer.ToString();

                Session["ReservationDetails"] = new ReservationDetail();

                Session["ReservedRoomType"] = new List<ReservedRoomType>();

                getReservationDetails();

                getReservedRoom();

                setStayDetails();

                setRentedRoomToRepeater();

                getRooms();

                setCurrentRentedRoomIntoRoomAvailability();
            }
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Redirect to previous page
            Response.Redirect(ViewState["PreviousPage"].ToString());
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

        private void setRentedRoomToRepeater()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            // Hold Reserved Room
            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            // Set rented room to repeater
            RepeaterReservedRoom.DataSource = reservedRooms;
            RepeaterReservedRoom.DataBind();
        }

        protected void RepeaterReservedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();

            // Check if any extra bed rented
            Label lblExtraBed = e.Item.FindControl("lblExtraBed") as Label;
            Label lblTrueFalse = e.Item.FindControl("lblTrueFalse") as Label;

            string temp = lblExtraBed.Text;

            if (lblExtraBed.Text != "-1")
            {
                lblTrueFalse.Text = "True";
            }
            else
            {
                lblTrueFalse.Text = "";
            }
        }

        public void getRooms()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            for (int i = 0; i < reservedRooms.Count; i++)
            {
                getAvailableRooms(reservedRooms[i].roomTypeID, reservedRooms[i].date);
            }
        }

        private void getAvailableRooms(string roomTypeID, string date)
        {
            List<ReservedRoomType> reservedRoomTypes = (List<ReservedRoomType>)Session["ReservedRoomType"];

            int condition = 0;

            // Check if the room avaiability has been retrieved
            for (int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if (reservedRoomTypes[i].roomTypeID == roomTypeID && reservedRoomTypes[i].date == date)
                {
                    condition = 1;
                }
            }

            // If room availability have not retrieved
            if (condition != 1)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                //String getAvailableRoom = "(SELECT RoomID FROM Room WHERE RoomTypeID LIKE @RoomTypeID AND Status LIKE 'Active') " +
                //                            "EXCEPT " +
                //                            "(SELECT RoomID FROM ReservationRoom WHERE RoomTypeID LIKE @RoomTypeID AND Date LIKE @Date)";

                String getAvailableRoom = "(SELECT RoomID FROM Room WHERE RoomTypeID LIKE @RoomTypeID AND Status LIKE 'Active') " +
                                            "EXCEPT " +
                                            "(SELECT RR.RoomID FROM ReservationRoom RR, Reservation R " +
                                            "WHERE RR.RoomTypeID LIKE @RoomTypeID AND RR.Date LIKE @Date AND R.ReservationID LIKE RR.ReservationID AND " +
                                            "R.Status IN ('Checked In', 'Check In'))";

                SqlCommand cmdGetAvailableRoom = new SqlCommand(getAvailableRoom, conn);

                cmdGetAvailableRoom.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
                cmdGetAvailableRoom.Parameters.AddWithValue("@Date", date);

                SqlDataReader sdr = cmdGetAvailableRoom.ExecuteReader();

                List<AvailableRoom> availableRooms = new List<AvailableRoom>();

                ReservedRoomType rrt = null;

                while (sdr.Read())
                {
                    AvailableRoom ar = new AvailableRoom(sdr["RoomID"].ToString());

                    availableRooms.Add(ar);
                }

                rrt = new ReservedRoomType(roomTypeID, date, availableRooms);

                reservedRoomTypes.Add(rrt);

                conn.Close();
            }


        }

        protected void LBSelectRoom_Click(object sender, EventArgs e)
        {
            // Display popup box
            PopupBoxSelectRoom.Visible = true;
            PopupCover.Visible = true;

            // Declare AvailableRoom object
            List<AvailableRoom> availableRooms = new List<AvailableRoom>();

            // Get the reference of repeater
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Cache ReservationRoomTypeID
            ViewState["ReservationRoomID"] = (item.FindControl("lblReservationRoomID") as Label).Text;

            // Get roomType for the selected item
            string roomType = (item.FindControl("lblRoomType") as Label).Text;
            string roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;
            String date = (item.FindControl("lblDate") as Label).Text;
            string convertedDate = reservationUtility.formatDate(date);
            ViewState["SelectedRoomID"] = (item.FindControl("lblSelectedRoomID") as Label).Text;

            lblPopupBoxRoomType.Text = roomType;
            lblPopupBoxDate.Text = date;
            lblPopupBoxRoomTypeID.Text = roomTypeID;

            List<ReservedRoomType> reservedRoomTypes = (List<ReservedRoomType>)Session["ReservedRoomType"];


            // Get roomTypeID
            for (int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if (reservedRoomTypes[i].date == convertedDate && reservedRoomTypes[i].roomTypeID == roomTypeID)
                {
                    availableRooms = reservedRoomTypes[i].availableRooms;
                }
            }

            // Get a list of available room
            List<AvailableRoom> ar = new List<AvailableRoom>();

            for (int i = 0; i < availableRooms.Count; i++)
            {
                if (availableRooms[i].selected == false)
                {
                    ar.Add(availableRooms[i]);
                }
            }

            // Set available room into the repeater
            if(ar.Count > 0)
            {
                RepeaterAvailableRoom.DataSource = ar;
                RepeaterAvailableRoom.DataBind();

                lblNoAvailableRoom.Visible = false;
            }
            else
            {
                RepeaterAvailableRoom.DataSource = null ;
                RepeaterAvailableRoom.DataBind();

                lblNoAvailableRoom.Visible = true;
            }
            

            // ********
            // Assign only specific roomType and specific date
        }

        protected void RepeaterAvailableRoom_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void RepeaterAvailableRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Assign data from RepeaterView
            DataRowView drv = e.Item.DataItem as DataRowView;

            // Get control's reference
            Label lblHousekeepingStatus = e.Item.FindControl("lblHousekeepingStatus") as Label;
            Label lblRoomID = e.Item.FindControl("lblRoomID") as Label;
            Label lblRoomFeatures = e.Item.FindControl("lblRoomFeatures") as Label;

            // Set room's features
            string roomFeatures = "";
            roomFeatures = getRoomFeature(lblRoomID.Text);
            lblRoomFeatures.Text = roomFeatures.ToString();
            lblRoomFeatures.ToolTip = roomFeatures.ToString();

            // Set color to housekeeping status
            if (lblHousekeepingStatus.Text == "Clean")
            {
                lblHousekeepingStatus.Style["color"] = "#00ce1b";  // Assign green color
            }
            else
            {
                lblHousekeepingStatus.Style["color"] = "red";  // Assign red color
            }
        }

        private string getRoomFeature(string roomID)
        {
            string roomFeatures = "";

            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomFeature = "SELECT * FROM Feature WHERE RoomID LIKE @ID";

            SqlCommand cmdGetRoomFeature = new SqlCommand(getRoomFeature, conn);

            cmdGetRoomFeature.Parameters.AddWithValue("@ID", roomID);

            SqlDataReader sdr = cmdGetRoomFeature.ExecuteReader();

            while (sdr.Read())
            {
                roomFeatures += sdr["Title"].ToString() + ", ";
            }

            conn.Close();

            //Remove ", " at the back of the string
            if (roomFeatures.Length > 0)
            {
                roomFeatures = roomFeatures.Substring(0, roomFeatures.Length - 2);
            }

            return roomFeatures;
        }

        protected void IBSelectRoom_Click(object sender, ImageClickEventArgs e)
        {
            // Get the reference of repeater
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get the reference of reservedRoomTypes
            List<ReservedRoomType> reservedRoomTypes = (List<ReservedRoomType>)Session["ReservedRoomType"];

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];
            List<ReservationRoom> reservedRooms = new List<ReservationRoom>();

            // Get roomNo for the selected item
            string roomID = (item.FindControl("lblRoomID") as Label).Text;
            string roomNo = (item.FindControl("lblRoomNo") as Label).Text;

            reservedRooms = reservation.reservedRoom;

            // Set the selected room into reservedRooms list
            for (int i = 0; i < reservedRooms.Count; i++)
            {
                // Get the correct item from the list
                if (reservedRooms[i].reservationRoomID == ViewState["ReservationRoomID"].ToString())
                {
                    reservedRooms[i].roomID = roomID;
                    reservedRooms[i].roomNo = roomNo;
                }
            }

            // Assign the updated list into reservation object
            reservation.reservedRoom = reservedRooms;

            // Update room status to no available
            List<AvailableRoom> ar = new List<AvailableRoom>();

            for (int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if (reservedRoomTypes[i].roomTypeID == lblPopupBoxRoomTypeID.Text && reservedRoomTypes[i].date == reservationUtility.formatDate(lblPopupBoxDate.Text))
                {
                    ar = reservedRoomTypes[i].availableRooms;

                    for (int j = 0; j < ar.Count; j++)
                    {
                        if (ar[j].roomID == roomID)
                        {
                            ar[j].selected = true;
                        }
                    }

                    reservedRoomTypes[i].availableRooms = ar;
                }
            }

            // Reset previous selected roomNo to available
            if (ViewState["SelectedRoomID"].ToString() != "")
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    if (ar[i].roomID == ViewState["SelectedRoomID"].ToString())
                    {
                        ar[i].selected = false;
                    }
                }
            }

            // Close popup box
            PopupBoxSelectRoom.Visible = false;
            PopupCover.Visible = false;

            // Refresh Reservation Room Repeater
            RepeaterReservedRoom.DataSource = reservation.reservedRoom;
            RepeaterReservedRoom.DataBind();

        }

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            PopupBoxSelectRoom.Visible = false;
            PopupCover.Visible = false;
        }

        private void setCurrentRentedRoomIntoRoomAvailability()
        {
            // Get current rented room
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];
            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            // Room availability for each room types
            List<ReservedRoomType> reservedRoomTypes = (List<ReservedRoomType>)Session["ReservedRoomType"];

            // Reserved Room
            for(int i = 0; i < reservedRooms.Count; i++)
            {
                // Reserved Room Type
                for(int j = 0; j < reservedRoomTypes.Count; j++)
                {
                    if (reservedRooms[i].roomTypeID == reservedRoomTypes[j].roomTypeID && reservedRooms[i].date == reservedRoomTypes[j].date)
                    {
                        List<AvailableRoom> ar = reservedRoomTypes[j].availableRooms;

                        ar.Add(new AvailableRoom(reservedRooms[i].roomID, true));

                        reservedRoomTypes[j].availableRooms = ar;
                    }
                }
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get current rented room
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];
            List<ReservationRoom> reservedRooms = reservation.reservedRoom;

            string updateReservedRoomNo = "UPDATE ReservationRoom SET RoomID = @RoomID WHERE ReservationRoomID LIKE @ReservationRoomID";

            // Update room no
            for(int i = 0; i < reservedRooms.Count; i++)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                SqlCommand cmdUpdateReservedRoomNo = new SqlCommand(updateReservedRoomNo, conn);

                cmdUpdateReservedRoomNo.Parameters.AddWithValue("@RoomID", reservedRooms[i].roomID);
                cmdUpdateReservedRoomNo.Parameters.AddWithValue("@ReservationRoomID", reservedRooms[i].reservationRoomID);

                int success = cmdUpdateReservedRoomNo.ExecuteNonQuery();

                conn.Close();
            }

            // Show success message
            PopupRoomMove.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReservationDetails.aspx?ID=" + en.encryption(reservationID));
        }
    }

}