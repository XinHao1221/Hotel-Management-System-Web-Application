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
    public partial class AddRoom : System.Web.UI.Page
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RepeaterTableUltility
        RepeaterTableUtility ultility = new RepeaterTableUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Add Room";

            if (!IsPostBack)
            {
                // Set data to drop-down list
                setDropDownListData();

                ddlFloorNumber.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));
                ddlRoomType.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));
            }

            PopupReset.Visible = false;
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        private void setDropDownListData()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            setFloorNumber();
            setRoomType();

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

            

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                ddlFloorNumber.Items.Add(new ListItem((dt.Rows[i]["FloorNumber"].ToString() + " - " + dt.Rows[i]["FloorName"].ToString()), dt.Rows[i]["FloorID"].ToString()));
            }
            //ddlFloorNumber.DataSource = dt;
            //ddlFloorNumber.DataBind();
            //ddlFloorNumber.DataTextField = "FloorNumber";
            //ddlFloorNumber.DataValueField = "FloorID";
            //ddlFloorNumber.DataBind();

            conn.Close();
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
            ddlRoomType.DataBind();
            ddlRoomType.DataTextField = "Title";
            ddlRoomType.DataValueField = "RoomTypeID";
            ddlRoomType.DataBind();
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(lblRoomNumberErrorMsg.Visible != true)
            {
                // open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                String nextRoomID = idGenerator.getNextID("RoomID", "Room", "RM");

                addRoom(nextRoomID);

                // Add Equipment 
                List<String> tableList = (List<String>)Session["FeatureList"];

                if (tableList.Any())    // Check if any equipment
                {
                    addFeature(nextRoomID);
                }

                conn.Close();

                Response.Redirect("PreviewRoom.aspx?ID=" + en.encryption(nextRoomID));
            }
            
        }

        private void addRoom(String nextRoomID)
        {
            String addRoom = "INSERT INTO Room VALUES (@RoomID, @RoomNumber, @FloorID, @RoomTypeID, @Status, @HousekeepingStatus)";

            SqlCommand cmdAddRoom = new SqlCommand(addRoom, conn);

            cmdAddRoom.Parameters.AddWithValue("@RoomID", nextRoomID);
            cmdAddRoom.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text);
            cmdAddRoom.Parameters.AddWithValue("@FloorID", ddlFloorNumber.SelectedValue);
            cmdAddRoom.Parameters.AddWithValue("@RoomTypeID", ddlRoomType.SelectedValue);
            cmdAddRoom.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmdAddRoom.Parameters.AddWithValue("@HousekeepingStatus", "Clean");

            int i = cmdAddRoom.ExecuteNonQuery();
        }

        private void addFeature(String nextRoomID)
        {
            List<String> featuretList = (List<String>)Session["FeatureList"];

            if (featuretList.Count > 0)
            {

                for (int j = 1; j <= featuretList.Count; j++)
                {
                    String nextFeatureID = idGenerator.getNextID("FeatureID", "Feature", "FT");

                    String addFeature = "INSERT INTO Feature VALUES (@ID, @Feature, @RoomID)";

                    SqlCommand cmdAddFeature = new SqlCommand(addFeature, conn);

                    cmdAddFeature.Parameters.AddWithValue("@ID", nextFeatureID);
                    cmdAddFeature.Parameters.AddWithValue("@Feature", featuretList[j - 1].ToString());
                    cmdAddFeature.Parameters.AddWithValue("@RoomID", nextRoomID);

                    int i = cmdAddFeature.ExecuteNonQuery();
                }

            }
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupBack.Visible = false;
            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }

        protected void btnPopupConfirmReset_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupReset.Visible = false;
            Response.Redirect("AddRoom.aspx");
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Room.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Check if user have enter any value
            if(txtRoomNumber.Text == "")
            {
                Response.Redirect("Room.aspx");
            }

            // If no show popup message
            PopupBack.Visible = true;
            PopupCover.Visible = true;
        }

        protected void txtRoomNumber_TextChanged(object sender, EventArgs e)
        {
            if(txtRoomNumber.Text != "")
            {
                // Check if user entered room number exist in database

                String roomNumber = txtRoomNumber.Text;

                // open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                String checkRoomNumber = "SELECT COUNT(*) " +
                                    "FROM Room " +
                                    "WHERE RoomNumber LIKE '" + roomNumber + "' AND Status IN('Active', 'Blocked')";

                SqlCommand cmdCheckRoomNumber = new SqlCommand(checkRoomNumber, conn);

                int count = (int)cmdCheckRoomNumber.ExecuteScalar();

                conn.Close();

                //If has row means room number already exists inside database
                if (count > 0)
                {
                    lblRoomNumberErrorMsg.Visible = true;   // if exists print error message
                }
                else
                {
                    lblRoomNumberErrorMsg.Visible = false;    // if no exists
                }

            }
            else
            {
                lblRoomNumberErrorMsg.Visible = false;
            }

        }

    }
}