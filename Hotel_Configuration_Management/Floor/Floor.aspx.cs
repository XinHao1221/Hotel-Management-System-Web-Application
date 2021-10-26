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

namespace Hotel_Management_System.Hotel_Configuration_Management.Floor
{
    public partial class Floor : System.Web.UI.Page
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
            // Page TItle
            Page.Title = "Floor";

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

            }
        }

        private int getTotalNumberOfItem()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(*) FROM Floor WHERE Status IN ('Active', 'Suspend')";

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

            String getName = "SELECT * FROM Floor WHERE Status IN ('Active', 'Suspend') " +
                "ORDER BY FloorNumber OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

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

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            refreshPage();
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

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string floorID = (item.FindControl("lblFloorID") as Label).Text;

            floorID = en.encryption(floorID);

            // Redirect to edit page
            Response.Redirect("EditFloor.aspx?ID=" + floorID);
        }

        protected void LBDelete_Click(object sender, EventArgs e)
        {
            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get FloorID of the selected item
            String floorID = (item.FindControl("lblFloorID") as Label).Text;
            String floorName = (item.FindControl("lblFloorName") as Label).Text;

            // Set FloorName into popup window
            lblPopupDeleteContent.Text = "Floor Name: " + floorName + "<br /><br />";

            ViewState["FloorID"] = floorID;
            ViewState["FloorName"] = floorName;

            // check if any rooms have references to the selected floor
            if (hasRoom() > 0)
            {
                PopupBoxDelete.Visible = true;

                // Display the room attached to the selected floor
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
            // check if any room have references to the selected floor
            String floorID = ViewState["FloorID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomCount = "SELECT Count(RoomNumber) FROM Room WHERE FloorID LIKE @ID AND Status IN ('Active', 'Blocked')";

            SqlCommand cmdGetRoomCount = new SqlCommand(getRoomCount, conn);

            cmdGetRoomCount.Parameters.AddWithValue("@ID", floorID);

            int noOfItem = (int)cmdGetRoomCount.ExecuteScalar();

            conn.Close();

            return noOfItem;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Assign data from RepeaterView
            DataRowView drv = e.Item.DataItem as DataRowView;
            String status = Convert.ToString(drv["Status"]);

            // Get control's reference
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblChangeStatus = e.Item.FindControl("lblChangeStatus") as Label;
            Image IMChangeStatus = e.Item.FindControl("IMChangeStatus") as Image;
            
            if (status == "Active")
            {
                lblStatus.Style["color"] = "#00ce1b";  // Assign green color

                // Assign new options button to suspend
                lblChangeStatus.Text = "Suspend";
                lblChangeStatus.Style["color"] = "red";
                IMChangeStatus.ImageUrl = "~/Image/suspend_icon.png";

            }
            else
            {
                
                lblStatus.Style["color"] = "red";  // Assign red color
            }

        }

        protected void LBChangeStatus_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get floorName for the selected item
            string floorName = (item.FindControl("lblFloorName") as Label).Text;
            String status = (item.FindControl("lblStatus") as Label).Text;
            String floorID = (item.FindControl("lblFloorID") as Label).Text;

            // Format popup window's content
            formatPopupWindow(floorName, status);
            PopupCover.Visible = true;
            PopupStatus.Visible = true;

            // Hold value for next postback
            ViewState["FloorID"] = floorID;
            ViewState["Status"] = status;
        }

        // To change the content of popup window dynamically
        private void formatPopupWindow(String floorName, String status)
        {
            lblPopupContent.Text = "<br />Floor Name: " + floorName + "<br /><br />";

            if(status == "Active")
            {
                lblPopupTitle.Text = "<br/>Suspend";
                lblPopupTitle.Style["color"] = "red";
                btnPopupActivate.Style["background-color"] = "red";
                btnPopupActivate.Text = "Suspend";
            }
            else
            {
                lblPopupTitle.Text = "<br/>Activate";
                lblPopupTitle.Style["color"] = "#00ce1b";
                btnPopupActivate.Style["background-color"] = "#00ce1b";
                btnPopupActivate.Text = "Activate";
            }

        }

        protected void btnPopupActivate_Click(object sender, EventArgs e)
        {
            // Update floor's status
            changeStatus();

            // Close popup window
            PopupStatus.Visible = false;
            PopupCover.Visible = false;

            // Refresh the page
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

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupStatus.Visible = false;
            PopupDelete.Visible = false;

        }

        private void changeStatus()
        {
            // Get FloorID and status stored inside ViewState
            String floorID = ViewState["FloorID"].ToString();
            String status = ViewState["Status"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateStatus = "UPDATE Floor SET Status = @Status WHERE FloorID LIKE @ID";

            SqlCommand cmdUpdateStatus = new SqlCommand(updateStatus, conn);

            cmdUpdateStatus.Parameters.AddWithValue("@ID", floorID);

            if (status == "Active")
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Suspend");
            }
            else
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Active");
            }

            int i = cmdUpdateStatus.ExecuteNonQuery();

            conn.Close();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            String floorID = ViewState["FloorID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String deleteFloor = "DELETE FROM Floor WHERE FloorID LIKE @ID";

            SqlCommand cmdDeleteFloor = new SqlCommand(deleteFloor, conn);

            cmdDeleteFloor.Parameters.AddWithValue("@ID", floorID);


            int i = cmdDeleteFloor.ExecuteNonQuery();

            conn.Close();

            PopupDelete.Visible = false;
            PopupCover.Visible = false;

            refreshPage();
        }

        private void deleteConfirmationMessage()
        {
            String floorID = ViewState["FloorID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            PopupCover.Visible = true;
            PopupBoxDelete.Visible = true;

            String getRoom = "SELECT RoomNumber FROM Room WHERE FloorID LIKE @ID AND Status IN ('Active', 'Blocked')";

            SqlCommand cmdGetRoom = new SqlCommand(getRoom, conn);

            cmdGetRoom.Parameters.AddWithValue("@ID", floorID);

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoom);

            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            lblTotalRoom.Text = (dt.Rows.Count).ToString();

            lblFloorName.Text = ViewState["FloorName"].ToString();
            lblFloorName.Style["font-weight"] = "600";

            // Bind data into repeater to display
            Repeater2.DataSource = dt;
            Repeater2.DataBind();

            conn.Close();
        }

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get FloorID of the selected item
            String floorID = (item.FindControl("lblFloorID") as Label).Text;

            floorID = en.encryption(floorID);

            // Redirect to view page
            Response.Redirect("ViewFloor.aspx?ID=" + floorID);
        }

        protected void LBMenuSearchBar_Click(object sender, EventArgs e)
        {
            searchFloor();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchFloor();
        }

        private void searchFloor()
        {
            String floorName = txtSearch.Text;
            floorName = floorName.ToUpper(); 

            if (floorName == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                String searchFloor = "SELECT * FROM Floor WHERE UPPER(FloorName) LIKE '%" + floorName + "%' AND Status IN ('Active', 'Suspend')";

                SqlCommand cmdSearchFloor = new SqlCommand(searchFloor, conn);

                cmdSearchFloor.Parameters.AddWithValue("@FloorName", floorName);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchFloor);

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

                displayItemTotal("SELECT COUNT(*) FROM Floor WHERE UPPER(FloorName) LIKE '%" + floorName + "%' AND Status IN ('Active', 'Suspend')");
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

        protected void IBClosePopUpBox_Click(object sender, ImageClickEventArgs e)
        {
            PopupBoxDelete.Visible = false;
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnPopupBoxDelete_Click(object sender, EventArgs e)
        {
            // Change Status to "Deleted"
            String floorID = ViewState["FloorID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateFloorStatus = "UPDATE Floor SET Status = @Status WHERE FloorID LIKE @ID";

            SqlCommand cmdUpdateFloorStatus = new SqlCommand(updateFloorStatus, conn);

            cmdUpdateFloorStatus.Parameters.AddWithValue("@Status", "Deleted");
            cmdUpdateFloorStatus.Parameters.AddWithValue("@ID", floorID);

            int i = cmdUpdateFloorStatus.ExecuteNonQuery();

            // Remove all room that attached to the deleted floor.
            removeRoom();

            conn.Close();

            PopupCover.Visible = false;
            PopupBoxDelete.Visible = false;

            refreshPage();
        }

        private void removeRoom()
        {
            String floorID = ViewState["FloorID"].ToString();

            String updateFloorStatus = "UPDATE Room SET Status = @Status WHERE FloorID LIKE @ID";

            SqlCommand cmdUpdateFloorStatus = new SqlCommand(updateFloorStatus, conn);

            cmdUpdateFloorStatus.Parameters.AddWithValue("@Status", "Deleted");
            cmdUpdateFloorStatus.Parameters.AddWithValue("@ID", floorID);

            int i = cmdUpdateFloorStatus.ExecuteNonQuery();

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
    }
}