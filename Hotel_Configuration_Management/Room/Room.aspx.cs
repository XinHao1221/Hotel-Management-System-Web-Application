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

namespace Hotel_Management_System.Hotel_Configuration_Management.Room
{
    public partial class Room : System.Web.UI.Page
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

                // Set drop-down list data
                setFloorNumber();
                setRoomType();
                ddlFloorNumber.Items.Insert(0, new ListItem("All", ""));
                ddlRoomType.Items.Insert(0, new ListItem("All", ""));

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
            String getTotalNumebrOfItem = "SELECT COUNT(*) FROM Room WHERE Status IN ('Active', 'Blocked')";

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

            String getRoom = "SELECT R.RoomID AS RoomID, R.RoomNumber AS RoomNumber, CONVERT(NVARCHAR(10), F.FloorNumber) + '  -  ' + F.FloorName AS FloorNo, RT.Title AS RoomType, " +
                "R.Status AS Status " +
                    "FROM Room R, Floor F, RoomType RT " +
                    "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') " +
                    "ORDER BY R.RoomID DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            SqlCommand cmdGetRoom = new SqlCommand(getRoom, conn);
            cmdGetRoom.Parameters.AddWithValue("@offset", offset);   // Assign start index
            cmdGetRoom.Parameters.AddWithValue("@fetch", fetch);     // Assign no of data to be read

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoom);


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
            catch (Exception i)
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
            searchRoom();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchRoom();
        }

        private void searchRoom()
        {
            String roomNo = txtSearch.Text;
            roomNo = roomNo.ToUpper();

            if (roomNo == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                String searchRoom = "SELECT R.RoomID AS RoomID, R.RoomNumber AS RoomNumber, CONVERT(NVARCHAR(10), F.FloorNumber) + '  -  ' + F.FloorName AS FloorNo, RT.Title AS RoomType, " +
                "R.Status AS Status " +
                    "FROM Room R, Floor F, RoomType RT " +
                    "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') AND UPPER(R.RoomNumber) LIKE '%" + roomNo + "%' " +
                    "ORDER BY R.RoomID DESC";

                SqlCommand cmdSearchRoom = new SqlCommand(searchRoom, conn);

                cmdSearchRoom.Parameters.AddWithValue("@FacilityName", roomNo);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchRoom);

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
                    "FROM Room R, Floor F, RoomType RT " +
                    "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') AND UPPER(R.RoomNumber) LIKE '%" + roomNo + "%'");
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
            // Assign data from RepeaterView
            DataRowView drv = e.Item.DataItem as DataRowView;
            String comment = Convert.ToString(drv["Status"]);

            // Get control's reference
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblChangeStatus = e.Item.FindControl("lblChangeStatus") as Label;
            Image IMChangeStatus = e.Item.FindControl("IMChangeStatus") as Image;

            if (comment == "Active")
            {
                lblStatus.Style["color"] = "#00ce1b";  // Assign green color

                // Assign new options button to suspend
                lblChangeStatus.Text = " Blocked";
                lblChangeStatus.Style["color"] = "red";
                IMChangeStatus.ImageUrl = "~/Image/suspend_icon.png";

            }
            else
            {

                lblStatus.Style["color"] = "red";  // Assign red color
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
                catch (Exception ex)
                {

                }

            }
        }

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get RoomID of the selected item
            String roomID = (item.FindControl("lblRoomID") as Label).Text;

            roomID = en.encryption(roomID);

            // Redirect to view page
            Response.Redirect("ViewRoom.aspx?ID=" + roomID);
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
        // ******************
        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string roomID = (item.FindControl("lblRoomID") as Label).Text;

            roomID = en.encryption(roomID);

            // Redirect to edit page
            Response.Redirect("EditRoom.aspx?ID=" + roomID);
        }

        protected void LBChangeStatus_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get room number for the selected item
            string roomNo = (item.FindControl("lblRoomNumber") as Label).Text;
            String status = (item.FindControl("lblStatus") as Label).Text;
            String roomID = (item.FindControl("lblRoomID") as Label).Text;

            // Format popup window's content
            formatPopupWindow(roomNo, status);
            PopupCover.Visible = true;
            PopupStatus.Visible = true;

            // Hold value for next postback
            ViewState["RoomID"] = roomID;
            ViewState["Status"] = status;
        }

        // To change the content of popup window dynamically
        private void formatPopupWindow(String roomNo, String status)
        {
            lblPopupContent.Text = "<br />Room Number: " + roomNo + "<br /><br />";

            if (status == "Active")
            {
                lblPopupTitle.Text = "<br/>Block Room";
                lblPopupTitle.Style["color"] = "red";
                btnPopupActivate.Style["background-color"] = "red";
                btnPopupActivate.Text = "Block";
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

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupStatus.Visible = false;
            PopupDelete.Visible = false;

        }

        private void changeStatus()
        {
            // Get RoomID and status stored inside ViewState
            String roomID = ViewState["RoomID"].ToString();
            String status = ViewState["Status"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateStatus = "UPDATE Room SET Status = @Status WHERE RoomID LIKE @ID";

            SqlCommand cmdUpdateStatus = new SqlCommand(updateStatus, conn);

            cmdUpdateStatus.Parameters.AddWithValue("@ID", roomID);

            if (status == "Active")
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Blocked");
            }
            else
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Active");
            }

            int i = cmdUpdateStatus.ExecuteNonQuery();

            conn.Close();

        }


        protected void LBDelete_Click(object sender, EventArgs e)
        {
            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get FacilityID of the selected item
            String roomID = (item.FindControl("lblRoomID") as Label).Text;
            String roomNo = (item.FindControl("lblRoomnumber") as Label).Text;

            // Set FacilityName into popup window
            lblPopupDeleteContent.Text = "Room Number: " + roomNo + "<br /><br />";

            ViewState["RoomID"] = roomID;
            ViewState["RoomNo"] = roomNo;

            PopupCover.Visible = true;
            PopupDelete.Visible = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
           
            // Check if the roomID exists in ReservationRoom table
            if (checkReservationRoom() > 0)
            {
                changeStatusToDelete();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                deleteFeature();
                deleteRoom();

                conn.Close();
            }

            PopupDelete.Visible = false;
            PopupCover.Visible = false;

            refreshPage();
        }

        private int checkReservationRoom()
        {
            // Get RoomID and status stored inside ViewState
            String roomID = ViewState["RoomID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String countReservationRoom = "SELECT COUNT(*) FROM ReservationRoom WHERE RoomID LIKE @ID";

            SqlCommand cmdCountReservationRoom = new SqlCommand(countReservationRoom, conn);

            cmdCountReservationRoom.Parameters.AddWithValue("@ID", roomID);

            int count = (int)cmdCountReservationRoom.ExecuteScalar();

            conn.Close();

            return count;
        }

        private void deleteRoom()
        {
            // Get RoomID and status stored inside ViewState
            String roomID = ViewState["RoomID"].ToString();

            String deleteRoom = "DELETE FROM Room WHERE RoomID LIKE @ID";

            SqlCommand cmdDeleteRoom = new SqlCommand(deleteRoom, conn);

            cmdDeleteRoom.Parameters.AddWithValue("@ID", roomID);

            int i = cmdDeleteRoom.ExecuteNonQuery();
        }

        // Delete all feature for the selected room
        private void deleteFeature()
        {
            // Get RoomID and status stored inside ViewState
            String roomID = ViewState["RoomID"].ToString();

            String deleteFeature = "DELETE FROM Feature WHERE RoomID LIKE @ID";

            SqlCommand cmdDeleteFeature = new SqlCommand(deleteFeature, conn);

            cmdDeleteFeature.Parameters.AddWithValue("@ID", roomID);

            int i = cmdDeleteFeature.ExecuteNonQuery();

        }

        private void changeStatusToDelete()
        {
            // Get RoomID and status stored inside ViewState
            String roomID = ViewState["RoomID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateRoomStatus = "UPDATE Room SET Status = @Status WHERE RoomID LIKE @ID";

            SqlCommand cmdUpdateStatus = new SqlCommand(updateRoomStatus, conn);

            cmdUpdateStatus.Parameters.AddWithValue("@Status", "Deleted");
            cmdUpdateStatus.Parameters.AddWithValue("@ID", roomID);

            int i = cmdUpdateStatus.ExecuteNonQuery();

            conn.Close();
        }
        private void setFloorNumber()
        {

            // SQL command 
            String getFloorNumber = "SELECT * FROM Floor WHERE Status IN ('Active', 'Suspend') ORDER BY FloorNumber";

            SqlCommand cmdGetFloorNumber = new SqlCommand(getFloorNumber, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetFloorNumber);

            DataTable dt = new DataTable();

            sda.Fill(dt);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlFloorNumber.Items.Add(new ListItem((dt.Rows[i]["FloorNumber"].ToString() + " - " + dt.Rows[i]["FloorName"].ToString()), dt.Rows[i]["FloorID"].ToString()));
            }
            //ddlFloorNumber.DataSource = dt;
            //ddlFloorNumber.DataBind();
            //ddlFloorNumber.DataTextField = "FloorNumber";
            //ddlFloorNumber.DataValueField = "FloorID";
            //ddlFloorNumber.DataBind();

        }

        private void setRoomType()
        {

            String getRoomType = "SELECT * FROM RoomType WHERE Status LIKE 'Active'";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoomType);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            ddlRoomType.DataSource = dt;
            ddlRoomType.DataBind();
            ddlRoomType.DataTextField = "Title";
            ddlRoomType.DataValueField = "RoomTypeID";
            ddlRoomType.DataBind();

        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string floorID = "";
            string roomTypeID = "";

            if(ddlFloorNumber.SelectedValue == "" && ddlRoomType.SelectedValue == "")
            {
                setItemToRepeater1();
            }
            else
            {
                string temp = ddlFloorNumber.SelectedValue;
                // prepare query based on selectedItem
                if (ddlFloorNumber.SelectedValue != "")
                {
                    floorID = "AND F.FloorID LIKE '" + ddlFloorNumber.SelectedValue + "' ";
                }
                if(ddlRoomType.SelectedValue != "")
                {
                    roomTypeID = "AND R.RoomTypeID LIKE '" + ddlRoomType.SelectedValue + "' ";
                }

                // open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                String getRoom = "SELECT R.RoomID AS RoomID, R.RoomNumber AS RoomNumber, CONVERT(NVARCHAR(10), F.FloorNumber) + '  -  ' + F.FloorName AS FloorNo, RT.Title AS RoomType, " +
                "R.Status AS Status " +
                    "FROM Room R, Floor F, RoomType RT " +
                    "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') " + floorID + roomTypeID + 
                    "ORDER BY R.RoomID DESC";

                SqlCommand cmdGetRoom = new SqlCommand(getRoom, conn);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoom);


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

                displayItemTotal("SELECT COUNT(*) " + 
                    "FROM Room R, Floor F, RoomType RT " +
                    "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') " + floorID + roomTypeID);

            }

           

        }

        
    }
}