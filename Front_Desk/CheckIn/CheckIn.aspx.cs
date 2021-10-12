﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.Reservation;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public partial class CheckIn : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        private String reservationID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //reservationID = Request.QueryString["ID"];
            //reservationID = en.decryption(roomID);

            reservationID = "RS10000002";

            if (!IsPostBack)
            {
                Session["ReservationDetails"] = new ReservationDetail();

                Session["ReservedRoomType"] = new List<ReservedRoomType>();

                getReservationDetails();

                getReservedRoom();

                getRentedFacilityList();

                setStayDetails();

                displayRentedRoomAndFacility();

                getRooms();

                // get facility availability
                Session["AvailableFacility"] = new List<AvailableFacility>();
                Session["AvailableFacility"] = getFacilityAvailability("");

                setFacility();

                // Check if no any facility rented
                checkRentedFacilityIsEmpty();
            }
            
        }

        private void checkRentedFacilityIsEmpty()
        {
            List<RentedFacility> rentedFacility = (List<RentedFacility>)Session["RentedFacilityList"];

            if (rentedFacility.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
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

            List<ReservedRoom> reservedRooms = new List<ReservedRoom>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservedRoom = "SELECT * FROM ReservationRoom WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservedRoom = new SqlCommand(getReservedRoom, conn);

            cmdGetReservedRoom.Parameters.AddWithValue("ID", reservationID);

            SqlDataReader sdr = cmdGetReservedRoom.ExecuteReader();

            ReservedRoom rr;

            while (sdr.Read())
            {
                if (sdr["ExtraBed"].ToString() == "True")
                {
                    rr = new ReservedRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), Convert.ToDouble(sdr["ExtraBedCharges"]), sdr["Date"].ToString());
                }
                else
                {
                    rr = new ReservedRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), -1, sdr["Date"].ToString());
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

            string rentDate, facilityID, returnDate;
            int qty = 0, group;
            int counter;

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

        private void displayRentedRoomAndFacility()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];


            RepeaterReservedRoom.DataSource = reservation.reservedRoom;
            RepeaterReservedRoom.DataBind();

            RepeaterRentedFacility.DataSource = reservation.rentedFacility;
            RepeaterRentedFacility.DataBind();

        }

        public void getRooms()
        {
            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];

            List<ReservedRoom> reservedRooms = reservation.reservedRoom;

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
            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if(reservedRoomTypes[i].roomTypeID == roomTypeID && reservedRoomTypes[i].date == date)
                {
                    condition = 1;
                }
            }

            // If room availability have not retrieved
            if(condition != 1)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                String getAvailableRoom = "(SELECT RoomID FROM Room WHERE RoomTypeID LIKE @RoomTypeID AND Status LIKE 'Active') " +
                                            "EXCEPT " +
                                            "(SELECT RoomID FROM ReservationRoom WHERE RoomTypeID LIKE @RoomTypeID AND Date LIKE @Date)";

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

        protected void LBBack_Click(object sender, EventArgs e)
        {

        }

        protected void IBDeleteRentedFacility_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get facility info for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String facilityName = (item.FindControl("lblFacilityName") as Label).Text;
            String rentDate = (item.FindControl("lblRentDate") as Label).Text;
            String returnDate = (item.FindControl("lblReturnDate") as Label).Text;

            // Set facilityID to ViewState
            ViewState["ItemIndex"] = itemIndex;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Facility: " + facilityName + "<br />" +
                "Rent Date: " + rentDate + "<br/>" + "Return Date:" + returnDate + "<br /><br />";

            PopupDelete.Visible = true;
            PopupCover.Visible = true;
        }

        protected void RepeaterReservedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();
        }

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            // Close Select Room's popup box
            PopupBoxSelectRoom.Visible = false;
            PopupCover.Visible = false;

            // Close facility availability popup box
            PopupBoxFacilityAvailability.Visible = false;
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
            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if(reservedRoomTypes[i].date == convertedDate && reservedRoomTypes[i].roomTypeID == roomTypeID)
                { 
                    availableRooms = reservedRoomTypes[i].availableRooms;
                }
            }

            // Get a list of available room
            List<AvailableRoom> ar = new List<AvailableRoom>();

            for(int i = 0; i < availableRooms.Count; i++)
            {
                if(availableRooms[i].selected == false)
                {
                    ar.Add(availableRooms[i]);
                }
            }

            // Set available room into the repeater
            RepeaterAvailableRoom.DataSource = ar;
            RepeaterAvailableRoom.DataBind();

            // ********
            // Assign only specific roomType and specific date

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

        protected void IBSelectRoom_Click(object sender, ImageClickEventArgs e)
        {
            // Get the reference of repeater
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get the reference of reservedRoomTypes
            List<ReservedRoomType> reservedRoomTypes = (List<ReservedRoomType>)Session["ReservedRoomType"];

            // Get reference of ReservationDetail from view state
            ReservationDetail reservation = (ReservationDetail)Session["ReservationDetails"];
            List<ReservedRoom> reservedRooms = new List<ReservedRoom>();

            // Get roomNo for the selected item
            string roomID = (item.FindControl("lblRoomID") as Label).Text;
            string roomNo = (item.FindControl("lblRoomNo") as Label).Text;

            reservedRooms = reservation.reservedRoom;

            // Set the selected room into reservedRooms list
            for(int i = 0; i < reservedRooms.Count; i++)
            {
                // Get the correct item from the list
                if(reservedRooms[i].reservationRoomID == ViewState["ReservationRoomID"].ToString())
                {
                    reservedRooms[i].roomID = roomID;
                    reservedRooms[i].roomNo = roomNo;
                }
            }

            // Assign the updated list into reservation object
            reservation.reservedRoom = reservedRooms;

            // Update room status to no available
            List<AvailableRoom> ar = new List<AvailableRoom>();

            for(int i = 0; i < reservedRoomTypes.Count; i++)
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
            if(ViewState["SelectedRoomID"].ToString() != "")
            {
                for(int i = 0; i < ar.Count; i++)
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

        protected void RepeaterRentedFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Label lblReservationFacilityID = e.Item.FindControl("lblReservationFacilityID") as Label;

            // Get control's reference
            ImageButton IBDeleteRentedFacility = e.Item.FindControl("IBDeleteRentedFacility") as ImageButton;

            if(lblReservationFacilityID.Text != "")
            {
                IBDeleteRentedFacility.Visible = false;
            }
        }

        protected void RepeaterRentedFacility_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void CVSelectedRoomNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Label lblSelectedRoomNo = (Label)((RepeaterItem)((Control)source).Parent).FindControl("lblSelectedRoomNo");

            if(lblSelectedRoomNo.Text == "")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        private List<AvailableFacility> getFacilityAvailability(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Query to get facility availability
            string getFacility = "SELECT F.FacilityID, F.FacilityName, F.Quantity, F.Price, F.PriceType " +
                                "FROM Facility F " +
                                "WHERE F.Status LIKE 'Active'";

            SqlCommand cmdGetFacility = new SqlCommand(getFacility, conn);

            // Hold the data read from database
            var sdr = cmdGetFacility.ExecuteReader();

            // Get reference of AvailableFacility stored inside session
            List<AvailableFacility> availableFacility = new List<AvailableFacility>();

            // set all data into list object 
            while (sdr.Read())
            {

                AvailableFacility af = new AvailableFacility(
                    sdr.GetString(sdr.GetOrdinal("FacilityID")),
                    sdr.GetString(sdr.GetOrdinal("FacilityName")),
                    sdr.GetInt32(sdr.GetOrdinal("Quantity")),
                    Convert.ToDouble(sdr.GetDecimal(sdr.GetOrdinal("Price"))),
                    sdr.GetString(sdr.GetOrdinal("PriceType")),
                    "Available",
                    "No");

                availableFacility.Add(af);
            }

            conn.Close();

            if (date != "")
            {
                // Reduce facility quantity there have already been selected by the user.
                for (int i = 0; i < availableFacility.Count; i++)
                {

                    conn = new SqlConnection(strCon);
                    conn.Open();

                    AvailableFacility af = availableFacility.ElementAt(i);

                    int qty = getFacilityRentedQty(af.facilityID, date);

                    af.availableQty -= qty;

                    if (af.availableQty == 0)
                    {
                        af.status = "Unavailable";
                    }

                    conn.Close();
                }
            }

            return availableFacility;
        }

        public int getFacilityRentedQty(string facilityID, string date)
        {
            // Query to get the quantity of facility have reserved by other guest
            String getReservedFacility = "SELECT FacilityID, COUNT(FacilityID) AS RentedQty " +
                                        "FROM ReservationFacility " +
                                        "WHERE DateRented LIKE @Date AND FacilityID LIKE @FacilityID " +
                                        "GROUP BY FacilityID ";

            SqlCommand cmdGetReservedFacility = new SqlCommand(getReservedFacility, conn);

            cmdGetReservedFacility.Parameters.AddWithValue("@FacilityID", facilityID);
            cmdGetReservedFacility.Parameters.AddWithValue("@Date", date);

            // Hold the data read from database
            SqlDataReader sdr = cmdGetReservedFacility.ExecuteReader();

            if (sdr.Read())
            {
                return sdr.GetInt32(sdr.GetOrdinal("RentedQty"));
            }

            // If not any reservation found under the category
            return 0;
        }

        protected void LBCheckFacilityAvailability_Click(object sender, EventArgs e)
        {
            PopupBoxFacilityAvailability.Visible = true;
            PopupCover.Visible = true;
            
            txtCheckFacilityDate.Text = reservationUtility.formatDate(lblCheckIn.Text);

            setItemToRepeaterFacilityAvailability(txtCheckFacilityDate.Text);
        }

        private void setItemToRepeaterFacilityAvailability(string date)
        {
            // getFacilityAvailability(date);

            // Set data into RepeaterFacilityAvailability
            RepeaterFacilityAvailability.DataSource = getFacilityAvailability(date);
            RepeaterFacilityAvailability.DataBind();
        }

        protected void txtCheckFacilityDate_TextChanged(object sender, EventArgs e)
        {
            setItemToRepeaterFacilityAvailability(txtCheckFacilityDate.Text);
        }

        protected void RepeaterFacilityAvailability_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's reference
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;

            if (lblStatus.Text == "Available")
            {
                lblStatus.Style["color"] = "#00ce1b";  // Assign green color
            }
            else
            {
                lblStatus.Style["color"] = "red";  // Assign red color
            }
        }

        // Set facility
        private void setFacility()
        {

            // Get facility availability from Session 
            List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

            // Clear all items
            ddlFacilityName.Items.Clear();
            ddlFacilityQty.Items.Clear();

            // Set first item
            ddlFacilityName.Items.Add(new ListItem("-- Please Select --"));

            for (int i = 0; i < availableFacility.Count; i++)
            {
                if (availableFacility[i].availableQty > 0)
                {
                    ddlFacilityName.Items.Add(new ListItem(availableFacility[i].facilityName, availableFacility[i].facilityID));
                }
            }

        }

        protected void ddlFacilityName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlFacilityName.SelectedIndex != 0)
            {
                // Get facility availability from Session 
                List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

                for (int i = 0; i < availableFacility.Count; i++)
                {
                    if (availableFacility[i].facilityID == ddlFacilityName.SelectedValue)
                    {
                        ddlFacilityQty.Items.Clear();
                        for (int j = 1; j <= availableFacility[i].availableQty; j++)
                        {
                            ddlFacilityQty.Items.Add(new ListItem(j.ToString()));
                        }

                        if (availableFacility[i].priceType == "Per Reservation")
                        {
                            txtRentDate.Text = reservationUtility.formatDate(lblCheckIn.Text);
                            txtReturnDate.Text = reservationUtility.formatDate(lblCheckOut.Text);

                            PNFacilityRentedDate.Visible = false;
                            CVFacilityRentedDate.Enabled = false;
                        }
                        else
                        {
                            txtRentDate.Text = reservationUtility.formatDate(lblCheckIn.Text);
                            txtReturnDate.Text = reservationUtility.formatDate(lblCheckOut.Text);

                            PNFacilityRentedDate.Visible = true;
                            CVFacilityRentedDate.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                ddlFacilityQty.Items.Clear();
                PNFacilityRentedDate.Visible = false;
                CVFacilityRentedDate.Enabled = false;
            }

        }

        protected void txtRentDate_TextChanged(object sender, EventArgs e)
        {
            // Run the comparevalidator
            CVFacilityRentedDate.Validate();

            // Check if rent date & return date is valid
            if (!CVFacilityRentedDate.IsValid)
            {

                // Style if error occurred
                txtRentDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtRentDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtReturnDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtReturnDate.Style["background-color"] = "rgb(255, 240, 240)";

            }
            else
            {
                // Style if no error occurred
                txtRentDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtRentDate.Style["background-color"] = "none";

                txtReturnDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtReturnDate.Style["background-color"] = "none";
            }
        }

        protected void txtReturnDate_TextChanged(object sender, EventArgs e)
        {
            // Run the comparevalidator
            CVFacilityRentedDate.Validate();

            // Check if rent date & return date is valid
            if (!CVFacilityRentedDate.IsValid)
            {

                // Style if error occurred
                txtRentDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtRentDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtReturnDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtReturnDate.Style["background-color"] = "rgb(255, 240, 240)";

            }
            else
            {
                // Style if no error occurred
                txtRentDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtRentDate.Style["background-color"] = "none";

                txtReturnDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtReturnDate.Style["background-color"] = "none";
            }
        }

        protected void btnAddFacility_Click(object sender, EventArgs e)
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Get facility availability from Session 
            List<AvailableFacility> availableFacility = (List<AvailableFacility>)Session["AvailableFacility"];

            // Hold ReservationFacility temporary
            List<ReservationFacility> reservationFacilities = reservationDetails.rentedFacility;

            for (int i = 0; i < availableFacility.Count; i++)
            {
                if (availableFacility[i].facilityID == ddlFacilityName.SelectedValue)
                {
                    double subTotal;

                    if (availableFacility[i].priceType == "Per Reservation")
                    {
                        subTotal = availableFacility[i].price * int.Parse(ddlFacilityQty.SelectedValue);
                    }
                    else
                    {
                        int durationOfStay = reservationUtility.getdurationOfStay(txtRentDate.Text, txtReturnDate.Text);

                        subTotal = availableFacility[i].price * (int.Parse(ddlFacilityQty.SelectedValue) * durationOfStay);
                    }

                    ReservationFacility rf = new ReservationFacility("", availableFacility[i].facilityID, int.Parse(ddlFacilityQty.SelectedValue),
                                                                    availableFacility[i].price,
                                                                    txtRentDate.Text,
                                                                    txtReturnDate.Text);


                    reservationFacilities.Add(rf);
                }
            }

            // Refresh the facility list
            RepeaterRentedFacility.DataSource = reservationFacilities;
            RepeaterRentedFacility.DataBind();

            // Reset rent facility form
            ddlFacilityName.SelectedIndex = 0;
            ddlFacilityQty.Items.Clear();

            txtRentDate.Text = "";
            txtReturnDate.Text = "";

            PNFacilityRentedDate.Visible = false;
            CVFacilityRentedDate.Enabled = false;

            lblNoItemFound.Visible = false;

            // Set the updated reservationFacilities list into ReservationDetails
            reservationDetails.rentedFacility = reservationFacilities;

        }
    }
}