/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

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

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public partial class CheckIn1 : System.Web.UI.Page
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

        private string todaysDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Check In";

            // Get current date
            DateTime dateNow = DateTime.Now;
            todaysDate = reservationUtility.formatDate(dateNow.ToString());

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
            String getTotalNumebrOfItem = "SELECT COUNT(*) FROM Reservation R, " +
                                    "Guest G WHERE R.GuestID LIKE G.GuestID AND R.CheckInDate LIKE '" + todaysDate + "' AND Status IN ('Created', 'Checked In', 'Check In')";

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

            String getReservation = "SELECT R.ReservationID, R.CheckInDate, R.CheckOutDate, R.ReservationDate, G.Name, G.IDNo, R.Status FROM Reservation R, " + 
                            "Guest G WHERE R.GuestID LIKE G.GuestID AND R.CheckInDate LIKE '" + todaysDate + "' AND Status IN ('Created', 'Checked In', 'Check In') " + 
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
                                    "WHERE R.GuestID LIKE G.GuestID AND G.IDNo LIKE '%" + idNo + "%' " + 
                                    "AND R.CheckInDate LIKE '" + todaysDate + "' AND Status IN ('Created', 'Checked In', 'Check In') " + 
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
                                    "WHERE R.GuestID LIKE G.GuestID AND G.IDNo LIKE '%" + idNo + "%' " +
                                    "AND R.CheckInDate LIKE '" + todaysDate + "' AND Status IN ('Created', 'Checked In', 'Check In')");
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
            Label lblReservationDate = e.Item.FindControl("lblReservationDate") as Label;
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblStatusDisplay = e.Item.FindControl("lblStatusDisplay") as Label;

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);
            DateTime formatedReservationDate = Convert.ToDateTime(lblReservationDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();
            lblReservationDate.Text = formatedReservationDate.ToShortDateString();

            // Display Status
            if(lblStatus.Text == "Checked In")
            {
                lblStatusDisplay.Text = "Checked In";
                lblStatusDisplay.Style["color"] = "#00ce1b";
            }
            else if(lblStatus.Text == "Check In")
            {
                lblStatusDisplay.Text = "Check In";
                lblStatusDisplay.Style["color"] = "rgb(194, 110, 0)";
            }
            else
            {
                lblStatusDisplay.Text = "Pending";
                lblStatusDisplay.Style["color"] = "red";
            }
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
            string checkInStatus = (item.FindControl("lblStatusDisplay") as Label).Text;

            reservationID = en.encryption(reservationID);
            
            if(checkInStatus == "Checked In")
            {
                Response.Redirect("../GuestInHouse/ReservationDetails.aspx?ID=" + reservationID);
            }
            else if(checkInStatus == "Check In")
            {
                Response.Redirect("../Self-CheckIn/Staff/CheckInGuest(SelfCheckIn).aspx?ID=" + reservationID);
            }
            else
            {
                // Redirect to check in
                Response.Redirect("CheckInGuest.aspx?ID=" + reservationID);
            }
            
        }

    }


}