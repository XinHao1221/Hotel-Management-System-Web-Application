using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public partial class MakeReservation : System.Web.UI.Page
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Declare list of room availability
        List<AvailableRoom> availableRoom = new List<AvailableRoom>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set all guest into drop-down list
                setGuestList();
                ddlGuest.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));

                // Set left margin for lblDurationOfStay
                lblDurationOfStay.Style["margin-left"] = "30px";
                LBAddGuest.Style["margin-left"] = "30px";

                // No of PNRoomReservationForm that Visible = true
                ViewState["NoOfRoomReservationForm"] = 1;
            }

            CompareValidator1.Validate();
            
        }

        private void setGuestList()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get all guest
            String getGuest = "SELECT GuestID, Name, IDNo FROM Guest";

            SqlCommand cmdGetGuest = new SqlCommand(getGuest, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetGuest);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            // Set guest into drop-down list

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlGuest.Items.Add(new ListItem((dt.Rows[i]["Name"].ToString() + " - " + dt.Rows[i]["IDNo"].ToString()), dt.Rows[i]["GuestID"].ToString()));
            }


            conn.Close();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {

        }

        protected void ddlGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display guest details
            PNStayDetails.Visible = true;

            // Get selected guestID
            String guestID = ddlGuest.SelectedValue;

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

        protected void txtCheckInDate_TextChanged(object sender, EventArgs e)
        {
            // Check if user have select any checkout date
            if (txtCheckOutDate.Text == "")
            {
                // Set check out date automatically
                DateTime checkInDate = Convert.ToDateTime(txtCheckInDate.Text);

                txtCheckOutDate.Text = reservationUtility.getNextDate(checkInDate.ToString());
            }

            // Run the comparevalidator
            CompareValidator1.Validate();

            // Check if check-in and check-out date is valid
            if (!CompareValidator1.IsValid)
            {
                lblDurationOfStay.Text = "";

                // Style if error occurred
                txtCheckInDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtCheckInDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtCheckOutDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtCheckOutDate.Style["background-color"] = "rgb(255, 240, 240)";

                lblDurationOfStay.Text = "";

                PNReserveRoom.Visible = false;
            }
            else
            {
                // Style if no error occurred
                txtCheckInDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtCheckInDate.Style["background-color"] = "none";

                txtCheckOutDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtCheckOutDate.Style["background-color"] = "none";

                PNReserveRoom.Visible = true;

                // Set duration of stay
                lblDurationOfStay.Text = reservationUtility.getdurationOfStay(txtCheckInDate.Text, txtCheckOutDate.Text).ToString() + " Night";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void txtCheckOutDate_TextChanged(object sender, EventArgs e)
        {
            // Run the comparevalidator
            CompareValidator1.Validate();

            // Check if check-in and check-out date is valid
            if (!CompareValidator1.IsValid)
            {
                lblDurationOfStay.Text = "";

                // Style if error occurred
                txtCheckInDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtCheckInDate.Style["background-color"] = "rgb(255, 240, 240)";

                txtCheckOutDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
                txtCheckOutDate.Style["background-color"] = "rgb(255, 240, 240)";

                lblDurationOfStay.Text = "";

                PNReserveRoom.Visible = false;
            }
            else
            {
                // Style if no error occurred
                txtCheckInDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtCheckInDate.Style["background-color"] = "none";

                txtCheckOutDate.Style["border-bottom"] = "border-bottom: 2px solid rgb(128, 128, 128);";
                txtCheckOutDate.Style["background-color"] = "none";

                // Set duration of stay
                lblDurationOfStay.Text = reservationUtility.getdurationOfStay(txtCheckInDate.Text, txtCheckOutDate.Text).ToString() + " Night";

                PNReserveRoom.Visible = true;
            }

            
        }

        protected void LBAddGuest_Click(object sender, EventArgs e)
        {
            // Navigate to add guest
            Response.Redirect("~/Front_Desk/Guest/AddGuest.aspx");
        }

        protected void AddReservationForm(object sender, EventArgs e)
        {
            // Get reference of LinkButton current clicked
            LinkButton lbAdd = (LinkButton)sender;

            // Get ID of LinkButton
            String id = lbAdd.ID;

            // Get last value of ID
            // To identify which panel is being click
            int index = id.Last() - '0';

            // Get ID for next panel
            String nextReservationPanelID = "PNReservationForm" + (index + 1).ToString();

            // Get reference of next panel
            Panel PNReservationForm = PNReserveRoom.FindControl(nextReservationPanelID) as Panel;

            // Make the panel visible to the user
            PNReservationForm.Visible = true;

            // Get 
            LinkButton currentLBAdd = PNReserveRoom.FindControl("LBAddReservationForm" + index.ToString()) as LinkButton;

            currentLBAdd.Visible = false;

            int i = (int)ViewState["NoOfRoomReservationForm"];

            i++;

            ViewState["NoOfRoomReservationForm"] = i;
        }

        protected void DeleteReservationForm(object sender, EventArgs e)
        {
            // Get reference of LinkButton current clicked
            LinkButton lbDelete = (LinkButton)sender;

            // Get ID of LinkButton
            String id = lbDelete.ID;

            // Get last value of ID
            // To identify which panel is being click
            int index = id.Last() - '0';

            String currentReservationPanelID;

            int noOfRoomReservationForm = (int)ViewState["NoOfRoomReservationForm"];

            // Check if the panel is last panel
            if (index < noOfRoomReservationForm)
            {
                // If no
                // shift the content forward on step
                // Before making the panel invisible to user
                shiftPanelContent(index);

                currentReservationPanelID = "PNReservationForm" + noOfRoomReservationForm.ToString();

                LinkButton currentLBAdd = PNReserveRoom.FindControl("LBAddReservationForm" + index.ToString()) as LinkButton;

                currentLBAdd.Visible = true;
            }
            else
            {
                currentReservationPanelID = "PNReservationForm" + index.ToString();

                LinkButton currentLBAdd = PNReserveRoom.FindControl("LBAddReservationForm" + (index - 1).ToString()) as LinkButton;

                currentLBAdd.Visible = true;
            }

            // Make the panel invisible to user
            Panel PNReservationForm = PNReserveRoom.FindControl(currentReservationPanelID) as Panel;

            PNReservationForm.Visible = false;

            noOfRoomReservationForm--;

            ViewState["NoOfRoomReservationForm"] = noOfRoomReservationForm;

        }

        private void shiftPanelContent(int currentIndex)
        {
            int noOfRoomReservationForm = (int)ViewState["NoOfRoomReservationForm"];

            // Shift panel one step forward
            for (int i = currentIndex; i < noOfRoomReservationForm; i++)
            {
                // Get reference for ddlRoomType
                DropDownList ddlRoomType = PNReserveRoom.FindControl("ddlRoomType" + currentIndex.ToString()) as DropDownList;

                // Get reference for next ddlRoomType
                DropDownList nextDDLRoomType = PNReserveRoom.FindControl("ddlRoomType" + (currentIndex + 1).ToString()) as DropDownList;

                // Shift the content from previous dropdown to current dropdown
                ddlRoomType.DataSource = nextDDLRoomType.Items;

                ddlRoomType.DataBind();

                // Set the option currently selected
                ddlRoomType.SelectedIndex = nextDDLRoomType.SelectedIndex;

            }
        }

        protected void DropDownSelectRoomType(object sender, EventArgs e)
        {
            // Get reference of DropDownList current selected
            DropDownList ddlRoomType = (DropDownList)sender;

            // Get the ID of DropDownList
            String id = ddlRoomType.ID;

            // Get last value of ID
            // To identify which panel is being click
            int index = id.Last() - '0';

            // Get the id of link button for current panel
            String currrentLBAddReservationForm = "LBAddReservationForm" + index.ToString();

            // Get reference to the link button add
            LinkButton addReservationForm = PNReserveRoom.FindControl(currrentLBAddReservationForm) as LinkButton;

            int noOfRoomReservationForm = (int)ViewState["NoOfRoomReservationForm"];
            // If the it is not the last panel
            if (noOfRoomReservationForm < 3)
            {
                // DropDownList already been selected 
                if (ddlRoomType.SelectedIndex != 0)
                {
                    // Make the add next panel button visible to user
                    addReservationForm.Visible = true;
                }
                else
                {
                    // Else
                    // Make it invisible to the user
                    addReservationForm.Visible = false;
                }
            }
            
        }

        protected void LBCheckAvailability_Click(object sender, EventArgs e)
        {
            PopupBoxRoomAvailability.Visible = true;
            PopupCover.Visible = true;
            setItemToRepeater1();
        }

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            PopupCover.Visible = false;
            PopupBoxRoomAvailability.Visible = false;
        }

        private void setItemToRepeater1()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomType = "SELECT (RT.RoomTypeID) AS RoomTypeID, (RT.Title) AS Title, " +
                                "coalesce(R.Count, 0) AS NumberOfRoom " +
                                "from RoomType RT " +
                                "left join ( " +
                                "select RoomTypeID, count(*) as Count " +
                                "from Room " +
                                "Where Status IN('Active', 'Blocked') " +
                                "group by RoomTypeID " +
                                ") R on(RT.RoomTypeID = R.RoomTypeID) " +
                                "WHERE RT.Status LIKE 'Active'";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            // Hold the data read from database
            var sdr = cmdGetRoomType.ExecuteReader();

         

            while(sdr.Read())
            {

                string roomTypeID = sdr.GetString(sdr.GetOrdinal("RoomTypeID"));


                AvailableRoom ar = new AvailableRoom(
                    sdr.GetString(sdr.GetOrdinal("RoomTypeID")),
                    sdr.GetString(sdr.GetOrdinal("Title")),
                    sdr.GetInt32(sdr.GetOrdinal("NumberOfRoom")),
                    "Available");

                availableRoom.Add(ar);

            }

            conn.Close();

            for(int i = 0; i < availableRoom.Count; i++)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                AvailableRoom ar = availableRoom.ElementAt(i);

                int qty = getReservedRoomTypeQty(ar.roomTypeID);

                ar.quantity -= qty;

                if(ar.quantity == 0)
                {
                    ar.status = "Unavailable";
                }

                conn.Close();
            }

            
            Repeater1.DataSource = availableRoom;
            Repeater1.DataBind();

            
        }

        private int getReservedRoomTypeQty(string roomTypeID)
        {

            String getReservedRoomType = "SELECT COUNT(RR.RoomTypeID) AS TotalReserved, RR.RoomTypeID AS RoomTypeID " +
                 "FROM Reservation R, ReservationRoom RR " +
                 "WHERE R.ReservationID LIKE RR.ReservationID AND R.Status LIKE 'Created' AND RR.RoomTypeID LIKE @ID AND RR.Date LIKE @Date " +
                 "GROUP BY RR.RoomTypeID ";

            SqlCommand cmdGetReservedRoomType = new SqlCommand(getReservedRoomType, conn);

            cmdGetReservedRoomType.Parameters.AddWithValue("@ID", roomTypeID);
            cmdGetReservedRoomType.Parameters.AddWithValue("Date", "2021-07-17");

            // Hold the data read from database
            SqlDataReader sdr = cmdGetReservedRoomType.ExecuteReader();

            if (sdr.Read())
            {
                return sdr.GetInt32(sdr.GetOrdinal("TotalReserved"));
            }

            return 0;
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
    }
}