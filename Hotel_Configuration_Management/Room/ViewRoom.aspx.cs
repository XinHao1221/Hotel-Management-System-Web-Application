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
    public partial class ViewRoom : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String roomID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Room Details";

            //roomID = "RM10000002";

            roomID = en.decryption(Request.QueryString["ID"]);

            setText();
            setFeature();
        }

        private void setText()
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
                lblRoomNumber.Text = sdr.GetString(sdr.GetOrdinal("RoomNumber"));

                floorID = sdr.GetString(sdr.GetOrdinal("FloorID"));
                roomTypeID = sdr.GetString(sdr.GetOrdinal("RoomTypeID"));

                lblStatus.Text = sdr.GetString(sdr.GetOrdinal("Status"));

                // Format color for status
                if (lblStatus.Text == "Active")
                {
                    lblStatus.Style["color"] = "#00ce1b";
                }
                else
                {
                    lblStatus.Style["color"] = "red";
                }
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
                lblFloorNumber.Text = sdr.GetValue(sdr.GetOrdinal("FloorNumber")).ToString() + " - " + sdr.GetString(sdr.GetOrdinal("FloorName"));
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
                lblRoomType.Text = sdr.GetString(sdr.GetOrdinal("Title"));
            }

            conn.Close();
        }

        private void setFeature()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFeature = "SELECT * FROM Feature WHERE RoomID LIKE @ID";

            SqlCommand cmdGetFeature = new SqlCommand(getFeature, conn);

            cmdGetFeature.Parameters.AddWithValue("@ID", roomID);

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetFeature);


            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            // Bind data into repeater to display
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            if (dt.Rows.Count <= 0)
            {
                lblNoItemFound.Visible = true;
            }

            conn.Close();
        }

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // Get current RoomID
            roomID = en.encryption(roomID);

            // Redirect to edit page
            Response.Redirect("EditRoom.aspx?ID=" + roomID);
        }
    }
}