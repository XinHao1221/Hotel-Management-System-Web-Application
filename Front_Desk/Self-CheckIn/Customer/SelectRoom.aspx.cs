using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class SelectRoom : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        string reservationID;
        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Select Room";

            if (!IsPostBack)
            {
                //// Save link for previous page
                //ViewState["PreviousPage"] = Request.UrlReferrer.ToString();

                //string previousPage = ViewState["PreviousPage"].ToString();

                //if(!(("https://localhost:" + Application["LocalHostID"].ToString() + "/Front_Desk/Self-CheckIn/Customer/RentFacility.aspx") == previousPage.Substring(0, 72)))
                //{
                    setStayDetails();

                    displayReservedRoom();

                    getRooms();
                //}
                
            }

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

        private void displayReservedRoom()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];


            RepeaterReservedRoom.DataSource = reservation.reservedRoom;
            RepeaterReservedRoom.DataBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("RentFacility.aspx?ID=" + en.encryption(reservationID));
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
                if (availableRooms[i].selected == false && availableRooms[i].houseKeepingStatus == "Clean")
                {
                    ar.Add(availableRooms[i]);
                }
            }

            // Set available room into the repeater
            if (ar.Count > 0)
            {
                RepeaterAvailableRoom.DataSource = ar;
                RepeaterAvailableRoom.DataBind();

                lblNoAvailableRoom.Visible = false;
            }
            else
            {
                RepeaterAvailableRoom.DataSource = null;
                RepeaterAvailableRoom.DataBind();

                lblNoAvailableRoom.Visible = true;
            }


            // ********
            // Assign only specific roomType and specific date

        }

        protected void CVSelectedRoomNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Label lblSelectedRoomNo = (Label)((RepeaterItem)((Control)source).Parent).FindControl("lblSelectedRoomNo");

            if (lblSelectedRoomNo.Text == "")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
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

                String getAvailableRoom = "(SELECT RoomID FROM Room WHERE RoomTypeID LIKE @RoomTypeID AND Status LIKE 'Active') " +
                                            "EXCEPT " +
                                            "(SELECT RR.RoomID FROM ReservationRoom RR, Reservation R " +
                                            "WHERE RR.RoomTypeID LIKE @RoomTypeID AND R.ReservationID LIKE RR.ReservationID AND " +
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

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            PopupCover.Visible = false;
            PopupBoxSelectRoom.Visible = false;
        }

        protected void RepeaterAvailableRoom_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void RepeaterAvailableRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblRoomID = e.Item.FindControl("lblRoomID") as Label;
            Label lblRoomFeatures = e.Item.FindControl("lblRoomFeatures") as Label;

            // Display room features
            lblRoomFeatures.Text = getRoomFeatures(lblRoomID.Text);
        }

        private string getRoomFeatures(string roomID)
        {
            string features;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomFeatures = "SELECT F.Title FROM Room R, Feature F WHERE R.RoomID LIKE F.RoomID AND R.RoomID LIKE @RoomID";

            SqlCommand cmdGetRoomFeatures = new SqlCommand(getRoomFeatures, conn);

            cmdGetRoomFeatures.Parameters.AddWithValue("@RoomID", roomID);

            SqlDataReader sdr = cmdGetRoomFeatures.ExecuteReader();

            features = "<ul>";

            while (sdr.Read())
            {
                features += "<li>" + sdr["Title"].ToString() + "</li>";
            }

            features += "</ul>";

            conn.Close();

            return features;
        }

        protected void LBSelectRoomNo_Click(object sender, EventArgs e)
        {
            // Get the reference of repeater
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

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

            // Reset SelectedRoomNo
            ViewState["SelectedRoomNo"] = "";
        }
    }
}