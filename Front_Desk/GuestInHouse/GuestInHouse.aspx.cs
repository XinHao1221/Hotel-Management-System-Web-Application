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

namespace Hotel_Management_System.Front_Desk.GuestInHouse
{
    public partial class GuestInHouse : System.Web.UI.Page
    {
        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RepeaterTableUltility
        RepeaterTableUtility ultility = new RepeaterTableUtility();

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        ReservationUtility reservation = new ReservationUtility();

        int page;
        int offset = 0; // start index of data
        int fetch;      // Number of data to display per page

        protected void Page_Load(object sender, EventArgs e)
        {
            fetch = int.Parse(ddlItemPerPage.SelectedValue.ToString());
            page = getTotalNumberOfPage();


            // Execute when page is first loaded
            if (!IsPostBack)
            {
                if (page != 0)
                {
                    txtPage.Text = "1";
                    setItemToRepeater1();
                }
                else
                {   // Execute when no data is found
                    lblNoItemFound.Visible = true;
                    txtPage.Enabled = false;
                    ddlItemPerPage.AutoPostBack = false;
                }

                //PopupCover.Visible = false;
                //PopupDelete.Visible = false;

            }
        }

        private int getTotalNumberOfItem()
        {

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(*) " +
                                            "FROM Reservation R, Guest G " +
                                            "WHERE R.GuestID LIKE G.GuestID AND R.Status LIKE 'Checked In' ";

            SqlCommand cmdGetItemCount = new SqlCommand(getTotalNumebrOfItem, conn);

            int noOfItem = (int)cmdGetItemCount.ExecuteScalar();

            conn.Close();

            lblTotalNoOfItem.Text = noOfItem.ToString();


            return noOfItem;
        }

        private int getTotalNumberOfPage()
        {
            // Calculate total page of data

            double temp = (((double)getTotalNumberOfItem() / (double)fetch));

            double page = Math.Ceiling(temp);

            lblPage.Text = page.ToString();

            return (int)page;
        }

        private void setNoOfDisplayedItem()
        {
            // Display data range that currently displayed

            // Eg. 1 - 10
            if (!((offset + fetch) > (int)getTotalNumberOfItem()))
            {
                lblItemDisplayed.Text = (offset + 1).ToString() + " - " + (offset + fetch).ToString();
            }
            else
            {
                lblItemDisplayed.Text = (offset + 1).ToString() + " - " + getTotalNumberOfItem().ToString();
            }

        }

        private void setItemToRepeater1()
        {

            setNoOfDisplayedItem();

            conn = new SqlConnection(strCon);
            conn.Open();

            String getReservation = "SELECT R.ReservationID, R.CheckInDate, R.CheckOutDate, R.ReservationDate, G.Name, G.IDNo, R.Status " + 
                                    "FROM Reservation R, Guest G " + 
                                    "WHERE R.GuestID LIKE G.GuestID AND R.Status LIKE 'Checked In' " + 
                                    "ORDER BY R.ReservationID DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            SqlCommand cmdGetReservation = new SqlCommand(getReservation, conn);
            cmdGetReservation.Parameters.AddWithValue("@offset", offset);   // Assign start index
            cmdGetReservation.Parameters.AddWithValue("@fetch", fetch);     // Assign no of data to be read

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetReservation);


            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            // Bind data into repeater to display
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            // Display message it no item was found
            if (dt.Rows.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }

            conn.Close();
        }

        protected void IBArrowLeft_Click(object sender, EventArgs e)
        {
            // Execute when user request for previous page

            int nextOffset = int.Parse(txtPage.Text) - 1;

            // when current page is first page
            if (nextOffset == 0)
            {
                nextOffset = page;

                offset = (nextOffset * fetch) - fetch;

                txtPage.Text = nextOffset.ToString();
            }
            else
            {
                offset = (nextOffset * fetch) - fetch;
                txtPage.Text = nextOffset.ToString();
            }

            setItemToRepeater1();

        }

        protected void IBArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            // Execute when user request for next page

            int nextOffset = int.Parse(txtPage.Text) + 1;

            // When next page is last page
            if (nextOffset > page)
            {
                offset = 0;
                txtPage.Text = "1";
            }
            else
            {
                offset = (nextOffset * fetch) - fetch;
                txtPage.Text = nextOffset.ToString();
            }

            setItemToRepeater1();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When user select no of item per page. The displayed data will start from index 1
            txtPage.Text = "1";
            setItemToRepeater1();
        }

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            refreshPage();
        }

        private void refreshPage()
        {
            page = getTotalNumberOfPage();

            try
            {   // Update offset and fetch according to page number entered
                int number = int.Parse(txtPage.Text);

                if (number <= page && number > 0)
                {
                    // If user enter valid page number
                    offset = (number * fetch) - fetch;
                }
                else
                {
                    // If user enter page number which isn't within range
                    number = 1;

                    txtPage.Text = number.ToString();
                }


            }
#pragma warning disable CS0168 // The variable 'i' is declared but never used
            catch (Exception i)
#pragma warning restore CS0168 // The variable 'i' is declared but never used
            {
                // If user input is not an integer
                int number = 1;

                txtPage.Text = number.ToString();
            }

            // Set item to repeater
            setItemToRepeater1();
        }

        protected void LBMenuSearchBar_Click(object sender, EventArgs e)
        {
            searchReservation();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchReservation();
        }

        private void searchReservation()
        {

            String idNo = txtSearch.Text;

            if (idNo == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {

                conn = new SqlConnection(strCon);
                conn.Open();

                String searchReservation = "SELECT R.ReservationID, R.CheckInDate, R.CheckOutDate, R.ReservationDate, G.Name, G.IDNo, R.Status " + 
                                            "FROM Reservation R, Guest G " +
                                            "WHERE R.GuestID LIKE G.GuestID AND R.Status LIKE 'Checked In' AND G.IDNo LIKE '%" + idNo + "%' " +
                                            "ORDER BY R.ReservationID DESC";

                SqlCommand cmdSearchReservation = new SqlCommand(searchReservation, conn);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchReservation);

                DataTable dt = new DataTable();

                // Assign the data from database into dataTable
                sda.Fill(dt);

                // Bind data into repeater to display
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblNoItemFound.Visible = true;
                }
                else
                {
                    lblNoItemFound.Visible = false;
                }

                conn.Close();

                displayItemTotal("SELECT COUNT(*) " +
                                            "FROM Reservation R, Guest G " +
                                            "WHERE R.GuestID LIKE G.GuestID AND R.Status LIKE 'Checked In' AND G.IDNo LIKE '%" + idNo + "%' ");
            }
        }

        private void displayItemTotal(String query)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            SqlCommand cmdGetTotalItem = new SqlCommand(query, conn);

            int noOfItem = (int)cmdGetTotalItem.ExecuteScalar();

            conn.Close();

            lblItemDisplayed.Text = "1 - " + noOfItem.ToString();
            lblTotalNoOfItem.Text = noOfItem.ToString();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblCheckInDate = e.Item.FindControl("lblCheckInDate") as Label;
            Label lblCheckOutDate = e.Item.FindControl("lblCheckOutDate") as Label;
            Label lblAdults = e.Item.FindControl("lblAdults") as Label;
            Label lblKids = e.Item.FindControl("lblKids") as Label;
            Label lblReservationID = e.Item.FindControl("lblReservationID") as Label;

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();

            List<ReservationRoom> reservationRooms = getReservationRoom(lblReservationID.Text);
            int durationOfStay = reservation.getdurationOfStay(lblCheckInDate.Text, lblCheckOutDate.Text);

            // Get total Adults
            lblAdults.Text = getTotalAdults(reservationRooms, durationOfStay);

            // Get total Kids
            lblKids.Text = getTotalKids(reservationRooms, durationOfStay);
        }

        private List<ReservationRoom> getReservationRoom(string reservationID)
        {
            List<ReservationRoom> reservationRooms = new List<ReservationRoom>();

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservationRoom = "SELECT* FROM ReservationRoom WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetReservationRoom = new SqlCommand(getReservationRoom, conn);

            cmdGetReservationRoom.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetReservationRoom.ExecuteReader();

            while (sdr.Read())
            {
                reservationRooms.Add(new ReservationRoom(sdr["ReservationRoomID"].ToString(), sdr["RoomTypeID"].ToString(), 
                    Convert.ToInt32(sdr["Adults"]), Convert.ToInt32(sdr["Kids"]), Convert.ToDouble(sdr["RoomPrice"]), 
                    Convert.ToDouble(sdr["ExtraBedCharges"]), sdr["Date"].ToString()));
            }

            conn.Close();

            return reservationRooms;
        }

        private string getTotalAdults(List<ReservationRoom> reservationRooms, int durationOfStay)
        {
            int totalAdults = 0;

            int item = 0;

            for(int i = 1; i <= (reservationRooms.Count / durationOfStay); i++)
            {
                totalAdults += reservationRooms[item].adults;
                item += durationOfStay;
            }

            return totalAdults.ToString();
        }

        private string getTotalKids(List<ReservationRoom> reservationRooms, int durationOfStay)
        {
            int totalKids = 0;

            int item = 0;

            for (int i = 1; i <= (reservationRooms.Count / durationOfStay); i++)
            {
                totalKids += reservationRooms[item].kids;
                item += durationOfStay;
            }

            return totalKids.ToString();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // DIslay more option
            Panel panel = (Panel)e.Item.FindControl("TableOptionMenu");

            if (panel.Visible == false)
            {
                resetPanelVisibility();
                panel.Visible = true;

                var item = e.Item;
            }
            else
            {
                panel.Visible = false;
            }
        }

        private void resetPanelVisibility()
        {
            // Ensure only one panel(More option) is displayed
            for (int i = 0; i < 20; i++)
            {

                try
                {
                    Panel panel = Repeater1.Items[i].FindControl("TableOptionMenu") as Panel;

                    panel.Visible = false;


                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {

                }

            }
        }

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get ReservationID of the selected item
            String reservationID = (item.FindControl("lblReservationID") as Label).Text;

            reservationID = en.encryption(reservationID);

            // Redirect to view page
            Response.Redirect("ReservationDetails.aspx?ID=" + reservationID);
        }

        protected void IBMoreOption_Click(object sender, ImageClickEventArgs e)
        {
            // Execute when user click on "More Option" button
            ImageButton btn = (ImageButton)sender;

            RepeaterItem repeater = (RepeaterItem)btn.NamingContainer;


            Panel panel = (Panel)repeater.FindControl("TableOptionMenu");

            // Set margin-top for first item on repeater to 40px
            if (repeater.ItemIndex == 0)
            {
                panel.Style["margin-top"] = "90px";
            }
        }

        protected void LBRoomMove_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get reservationID for the selected item
            string reservationID = (item.FindControl("lblReservationID") as Label).Text;

            // Redirect to edit page
            Response.Redirect("RoomMove.aspx?ID=" + en.encryption(reservationID));
        }

        protected void LBCheckOut_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get reservationID for the selected item
            string reservationID = (item.FindControl("lblReservationID") as Label).Text;

            // Redirect to edit page
            Response.Redirect("../CheckOut/CheckOutGuest.aspx?ID=" + en.encryption(reservationID));

        }
    }
}