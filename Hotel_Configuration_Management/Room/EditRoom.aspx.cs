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

namespace Hotel_Management_System.Hotel_Configuration_Management.Room
{
    public partial class EditRoom : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String roomID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //roomID = Request.QueryString["ID"];
            //roomID = en.decryption(roomID);

            roomID = "RM10000001";

            if (!IsPostBack)
            {
                // Save link for previous page
                //ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
                //PopupCover.Visible = false;

                // Set data to drop-down list
                setDropDownListData();

                //ddlFloorNumber.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));
                //ddlRoomType.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));

                // Set stored data into text field
                setData();
            }
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

        private void setData()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoom = "SELECT * FROM Room WHERE RoomID LIKE @ID";

            SqlCommand cmdGetRoom = new SqlCommand(getRoom, conn);

            cmdGetRoom.Parameters.AddWithValue("@ID", roomID);

            SqlDataReader sdr = cmdGetRoom.ExecuteReader();

            String floorID = null, roomTypeID = null;

            if (sdr.Read())
            {
                txtRoomNumber.Text = sdr.GetString(sdr.GetOrdinal("RoomNumber"));

                floorID = sdr.GetString(sdr.GetOrdinal("FloorID"));
                roomTypeID = sdr.GetString(sdr.GetOrdinal("RoomTypeID"));

                ddlStatus.SelectedValue = sdr.GetString(sdr.GetOrdinal("Status"));

            }

            conn.Close();

            // Set data into Label
            if (floorID != null && roomTypeID != null)
            {
                setFloorNumber(floorID);
                setRoomType(roomTypeID);
            }

        }

        private void setFloorNumber(String floorID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFloor = "SELECT * FROM Floor WHERE FloorID LIKE @ID";

            SqlCommand cmdGetFloor = new SqlCommand(getFloor, conn);

            cmdGetFloor.Parameters.AddWithValue("@ID", floorID);

            SqlDataReader sdr = cmdGetFloor.ExecuteReader();

            if (sdr.Read())
            {
                ddlFloorNumber.SelectedValue = sdr.GetString(sdr.GetOrdinal("FloorID"));
            }

            conn.Close();
        }

        private void setRoomType(String roomTypeID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomType = "SELECT * FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            SqlDataReader sdr = cmdGetRoomType.ExecuteReader();

            if (sdr.Read())
            {
                ddlRoomType.SelectedValue = sdr.GetString(sdr.GetOrdinal("RoomTypeID"));
            }

            conn.Close();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Redirect to previous page
            Response.Redirect(ViewState["PreviousPage"].ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL Command to update existing room's details

            String updateRoom = "UPDATE Room SET RoomNumber = @RoomNumber, FloorID = @FloorID, RoomTypeID = @RoomTypeID, Status = @Status " +
                "WHERE RoomID LIKE @RoomID";

            SqlCommand cmdUpdateRoom = new SqlCommand(updateRoom, conn);

            cmdUpdateRoom.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text);
            cmdUpdateRoom.Parameters.AddWithValue("@FloorID", ddlFloorNumber.SelectedValue);
            cmdUpdateRoom.Parameters.AddWithValue("@RoomTypeID", ddlRoomType.SelectedValue);
            cmdUpdateRoom.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmdUpdateRoom.Parameters.AddWithValue("@RoomID", roomID);

            int i = cmdUpdateRoom.ExecuteNonQuery();

            //Response.Redirect("ViewRoom.aspx?ID=" + en.encryption(roomID));
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }

        protected void btnPopupConfirmReset_Click(object sender, EventArgs e)
        {
            // Reset all text field
            txtRoomNumber.Text = "";
            ddlFloorNumber.SelectedIndex = 0;
            ddlRoomType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            // *** Delete all equipment
            //deleteAllEquipment();
            //EC1.setEquipment();

            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }
    }
}