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


namespace Hotel_Management_System.Hotel_Configuration_Management.PriceManager
{
    public partial class SpecialRoomPrice : System.Web.UI.Page
    {
        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RepeaterTableUltility
        RepeaterTableUtility ultility = new RepeaterTableUtility();

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
            }
        }

        private int getTotalNumberOfItem()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(DISTINCT S.Date) AS Total FROM SpecialRoomPrice S";

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

            String getName = "SELECT DISTINCT S.Date, S.Title FROM SpecialRoomPrice S  " +
                "ORDER BY S.Date DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            SqlCommand cmdInsert = new SqlCommand(getName, conn);
            cmdInsert.Parameters.AddWithValue("@offset", offset);   // Assign start index
            cmdInsert.Parameters.AddWithValue("@fetch", fetch);     // Assign no of data to be read

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdInsert);

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
            searchRoomType();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchRoomType();
        }

        private void searchRoomType()
        {
            String eventName = txtSearch.Text;
            eventName = eventName.ToUpper();

            if (eventName == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                //String searchRoomType = "SELECT * FROM RoomType WHERE UPPER(Title) LIKE '%" + roomType + "%' AND Status LIKE 'Active'";

                String searchEvent = "SELECT DISTINCT S.Date, S.Title FROM SpecialRoomPrice S  " +
                                        "WHERE UPPER(S.Title) LIKE '%" + eventName + "%'";

                SqlCommand cmdSearchEvent = new SqlCommand(searchEvent, conn);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchEvent);

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

                displayItemTotal("SELECT COUNT(DISTINCT S.Date) Total FROM SpecialRoomPrice S  " +
                                        "WHERE UPPER(S.Title) LIKE '%" + eventName + "%'");
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

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string date = (item.FindControl("Label1") as Label).Text;

            date = en.encryption(date);

            // Redirect to edit page
            Response.Redirect("EditSpecialPrice.aspx?Date=" + date);
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Assign data from RepeaterView
            DataRowView drv = e.Item.DataItem as DataRowView;
            String date = Convert.ToString(drv["Date"]);

            // Get control's reference
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            lblDate.Text = formatedDate.ToShortDateString();
        }

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get SpecialPriceID of the selected item
            String date = (item.FindControl("Label1") as Label).Text;

            date = en.encryption(date);

            // Redirect to view page
            Response.Redirect("EditSpecialPrice.aspx?Date=" + date);
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

        protected void LBDelete_Click(object sender, EventArgs e)
        {
            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get date of the selected item
            String date = (item.FindControl("Label1") as Label).Text;
            String eventName = (item.FindControl("lblTitle") as Label).Text;

            // Format data base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(date);

            // Set FacilityName into popup window
            lblPopupDeleteContent.Text = "Event Name: " + eventName + "<br />" +
                "Date: " + formatedDate.ToShortDateString() + 
                "<br /><br />";

            ViewState["Date"] = date;

            PopupCover.Visible = true;
            PopupDelete.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string date = ViewState["Date"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            string deleteEvent = "DELETE FROM SpecialRoomPrice WHERE Date LIKE @Date";

            SqlCommand cmdDeleteEvent = new SqlCommand(deleteEvent, conn);

            cmdDeleteEvent.Parameters.AddWithValue("@Date", date);

            int i = cmdDeleteEvent.ExecuteNonQuery();

            conn.Close();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;

            refreshPage();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }
    }
}