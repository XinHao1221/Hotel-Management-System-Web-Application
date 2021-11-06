using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management_System.Dashboard
{
    public partial class BlockedRoom : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Blocked Room";

            setItemToRepeaterBlockedRoom();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        private void setItemToRepeaterBlockedRoom()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            List<RoomOccupancy> roomOccupancies = new List<RoomOccupancy>();

            for (int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> ra = roomTypes[i].roomOccupancies;

                for (int j = 0; j < ra.Count; j++)
                {
                    if (ra[j].status == "Blocked")
                    {
                        roomOccupancies.Add(new RoomOccupancy(ra[j].roomID, ra[j].available));
                    }
                }
            }

            if (roomOccupancies.Count > 0)
            {
                RepeaterBlockedRoom.DataSource = roomOccupancies;
                RepeaterBlockedRoom.DataBind();

                lblNoItemFound.Visible = false;
            }
            else
            {
                lblNoItemFound.Enabled = true;
            }

        }

        protected void RepeaterBlockedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblRoomID = e.Item.FindControl("lblRoomID") as Label;
            Label lblRoomType = e.Item.FindControl("lblRoomType") as Label;
            Label lblHousekeepingStatus = e.Item.FindControl("lblHousekeepingStatus") as Label;

            formatHousekeepingStatusColor(lblHousekeepingStatus);

            setRoomType(lblRoomID.Text, lblRoomType);
        }

        private void formatHousekeepingStatusColor(Label lblHousekeepingStatus)
        {
            if (lblHousekeepingStatus.Text == "Clean")
            {
                lblHousekeepingStatus.Style["color"] = "rgb(0, 206, 27)";
            }
            else
            {
                lblHousekeepingStatus.Style["color"] = "red";
            }
        }

        private void setRoomType(string roomID, Label lblRoomType)
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomType = "SELECT RT.Title FROM Room R, RoomType RT WHERE R.RoomID LIKE @RoomID AND RT.RoomTypeID LIKE R.RoomTypeID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@RoomID", roomID);

            lblRoomType.Text = (string)cmdGetRoomType.ExecuteScalar();

            conn.Close();
        }
    }
}