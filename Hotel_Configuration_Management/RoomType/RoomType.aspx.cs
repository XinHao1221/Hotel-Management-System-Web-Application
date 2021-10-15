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



namespace Hotel_Management_System.Hotel_Configuration_Management.Room_Type
{
    public partial class RoomType : System.Web.UI.Page
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

                PopupCover.Visible = false;
                PopupDelete.Visible = false;
                PopupBoxDelete.Visible = false;

            }
        }

        private int getTotalNumberOfItem()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(*) FROM RoomType WHERE Status LIKE 'Active'";

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

            //String getRoomType = "SELECT * FROM RoomType WHERE Status LIKE 'Active' " +
            //    "ORDER BY RoomTypeID OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            String getRoomType = "SELECT (RT.RoomTypeID) AS RoomTypeID, (RT.Title) AS Title, (RT.ShortCode) AS ShortCode, (RT.BaseOccupancy) AS BaseOccupancy, (RT.HigherOccupancy) AS HigherOccupancy, " +
                "coalesce(R.Count, 0) AS NumberOfRoom " +
                "from RoomType RT " +
                "left join (" +
                "select RoomTypeID, count(*) as Count " +
                "from Room " +
                "Where Status IN ('Active', 'Blocked') " +
                "group by RoomTypeID" +
                ") R on (RT.RoomTypeID = R.RoomTypeID) " +
                "WHERE RT.Status LIKE 'Active' " +
                "ORDER BY RT.RoomTypeID DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);
            cmdGetRoomType.Parameters.AddWithValue("@offset", offset);   // Assign start index
            cmdGetRoomType.Parameters.AddWithValue("@fetch", fetch);     // Assign no of data to be read

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoomType);


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
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchRoomType();
        }

        protected void LBMenuSearchBar_Click(object sender, EventArgs e)
        {
            searchRoomType();
        }

        private void searchRoomType()
        {
            String roomType = txtSearch.Text;
            roomType = roomType.ToUpper();

            if (roomType == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                //String searchRoomType = "SELECT * FROM RoomType WHERE UPPER(Title) LIKE '%" + roomType + "%' AND Status LIKE 'Active'";

                String searchRoomType = "SELECT (RT.RoomTypeID) AS RoomTypeID, (RT.Title) AS Title, (RT.ShortCode) AS ShortCode, (RT.BaseOccupancy) AS BaseOccupancy, (RT.HigherOccupancy) AS HigherOccupancy, " +
                        "coalesce(R.Count, 0) AS NumberOfRoom " +
                        "from RoomType RT " +
                        "left join (" +
                        "select RoomTypeID, count(*) as Count " +
                        "from Room " +
                        "Where Status IN ('Active', 'Blocked') " +
                        "group by RoomTypeID" +
                        ") R on (RT.RoomTypeID = R.RoomTypeID) " +
                        "WHERE RT.Status LIKE 'Active' AND UPPER(RT.Title) LIKE '%" + roomType + "%' " +
                        "ORDER BY RT.RoomTypeID DESC";

                SqlCommand cmdSearchRoomType = new SqlCommand(searchRoomType, conn);

                cmdSearchRoomType.Parameters.AddWithValue("@FacilityName", roomType);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchRoomType);

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
                        "from RoomType RT " +
                        "left join (" +
                        "select RoomTypeID, count(*) as Count " +
                        "from Room " +
                        "Where Status IN ('Active', 'Blocked') " +
                        "group by RoomTypeID" +
                        ") R on (RT.RoomTypeID = R.RoomTypeID) " +
                        "WHERE RT.Status LIKE 'Active' AND UPPER(RT.Title) LIKE '%" + roomType + "%'");
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

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get RoomTypeID of the selected item
            String roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;

            roomTypeID = en.encryption(roomTypeID);

            // Redirect to view page
            Response.Redirect("ViewRoomType.aspx?ID=" + roomTypeID);
        }

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;

            roomTypeID = en.encryption(roomTypeID);

            // Redirect to edit page
            Response.Redirect("EditRoomType.aspx?ID=" + roomTypeID);
        }

        protected void LBDelete_Click(object sender, EventArgs e)
        {
            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get RoomTypeID of the selected item
            String roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;
            String title = (item.FindControl("lblTitle") as Label).Text;

            // Set RoomType Title into popup window
            lblPopupDeleteContent.Text = "Room Type: " + title + "<br /><br />";

            ViewState["RoomTypeID"] = roomTypeID;
            ViewState["Title"] = title;

            // check if any rooms have references to the selected room type
            if (hasRoom() > 0)
            {
                PopupBoxDelete.Visible = true;

                // Display the room attached to the selected room type
                deleteConfirmationMessage();
            }
            else
            {
                PopupDelete.Visible = true;
            }

            PopupCover.Visible = true;
        }

        private int hasRoom()
        {
            // check if any room have references to the selected room type
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomCount = "SELECT Count(RoomNumber) FROM Room WHERE RoomTypeID LIKE @ID AND Status IN ('Active', 'Blocked')";

            SqlCommand cmdGetRoomCount = new SqlCommand(getRoomCount, conn);

            cmdGetRoomCount.Parameters.AddWithValue("@ID", roomTypeID);

            int noOfItem = (int)cmdGetRoomCount.ExecuteScalar();

            conn.Close();

            return noOfItem;
        }

        private void deleteConfirmationMessage()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            PopupCover.Visible = true;
            PopupBoxDelete.Visible = true;

            String getRoom = "SELECT RoomNumber FROM Room WHERE RoomTypeID LIKE @ID AND Status IN ('Active', 'Blocked')";

            SqlCommand cmdGetRoom = new SqlCommand(getRoom, conn);

            cmdGetRoom.Parameters.AddWithValue("@ID", roomTypeID);

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoom);

            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            lblTotalRoom.Text = (dt.Rows.Count).ToString();

            lblFloorName.Text = ViewState["Title"].ToString();
            lblFloorName.Style["font-weight"] = "600";

            // Bind data into repeater to display
            Repeater2.DataSource = dt;
            Repeater2.DataBind();

            conn.Close();
        }

        // Activate Delete button for popup box
        protected void cbDeleteAnywhere_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDeleteAnywhere.Checked)
            {
                btnPopupBoxDelete.Visible = true;
            }
            else
            {
                btnPopupBoxDelete.Visible = false;
            }
        }

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            PopupBoxDelete.Visible = false;
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnPopupBoxDelete_Click(object sender, EventArgs e)
        {
            // Change Status to "Deleted"
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateRoomTypeStatus = "UPDATE RoomType SET Status = @Status WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdUpdateRoomTypeStatus = new SqlCommand(updateRoomTypeStatus, conn);

            cmdUpdateRoomTypeStatus.Parameters.AddWithValue("@Status", "Deleted");
            cmdUpdateRoomTypeStatus.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdUpdateRoomTypeStatus.ExecuteNonQuery();

            // change status all room that attached to the deleted room type to deleted.
            changeRoomStatus();

            conn.Close();

            PopupCover.Visible = false;
            PopupBoxDelete.Visible = false;

            refreshPage();
        }

        private void changeRoomStatus()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            String updateRoomTypeStatus = "UPDATE Room SET Status = @Status WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdUpdateRoomTypeStatus = new SqlCommand(updateRoomTypeStatus, conn);

            cmdUpdateRoomTypeStatus.Parameters.AddWithValue("@Status", "Deleted");
            cmdUpdateRoomTypeStatus.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdUpdateRoomTypeStatus.ExecuteNonQuery();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            

            conn = new SqlConnection(strCon);
            conn.Open();

            deleteStandardRoomPrice();
            deleteSpecialRoomPrice();
            deleteEquipment();
            deleteRoomType();

            conn.Close();

            PopupDelete.Visible = false;
            PopupCover.Visible = false;

            refreshPage();
        }

        private void deleteStandardRoomPrice()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            String deleteStandardRoomPrice = "DELETE FROM StandardRoomPrice WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdDeleteStandardRoomPrice = new SqlCommand(deleteStandardRoomPrice, conn);

            cmdDeleteStandardRoomPrice.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdDeleteStandardRoomPrice.ExecuteNonQuery();
        }

        private void deleteSpecialRoomPrice()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            String deleteSpecialRoomPrice = "DELETE FROM SpecialRoomPrice WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdDeleteSpecialRoomPrice = new SqlCommand(deleteSpecialRoomPrice, conn);

            cmdDeleteSpecialRoomPrice.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdDeleteSpecialRoomPrice.ExecuteNonQuery();

        }

        private void deleteRoomType()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            String deleteRoomType = "DELETE FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdDeleteRoomType = new SqlCommand(deleteRoomType, conn);

            cmdDeleteRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdDeleteRoomType.ExecuteNonQuery();

        }

        private void deleteEquipment()
        {
            String roomTypeID = ViewState["RoomTypeID"].ToString();

            String deleteEquipment = "DELETE FROM Equipment WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdDeleteEquipment = new SqlCommand(deleteEquipment, conn);

            cmdDeleteEquipment.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdDeleteEquipment.ExecuteNonQuery();
        }



    }
}