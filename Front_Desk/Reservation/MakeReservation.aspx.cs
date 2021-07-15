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
            }

            CompareValidator1.Validate();
            
            // Check if check-in and check-out date is valid
            //if(!CompareValidator1.IsValid)
            //{
            //    lblDurationOfStay.Text = "";

            //    // Style if error occurred
            //    txtCheckInDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
            //    txtCheckInDate.Style["background-color"] = "rgb(255, 240, 240)";

            //    txtCheckOutDate.Style["border-bottom"] = "2px solid rgb(255, 0, 0)";
            //    txtCheckOutDate.Style["background-color"] = "rgb(255, 240, 240)";
            //}
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
    }
}