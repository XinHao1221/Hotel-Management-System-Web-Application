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

namespace Hotel_Management_System.Dashboard
{
    public partial class OccupiedRoom : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Occupied Room";

            setItemToRepeaterOccupiedRoom();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        private void setItemToRepeaterOccupiedRoom()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            List<RoomOccupancy> roomOccupancies = new List<RoomOccupancy>();

            for (int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> ra = roomTypes[i].roomOccupancies;

                for (int j = 0; j < ra.Count; j++)
                {
                    if (ra[j].status != "Blocked" && ra[j].available == false)
                    {
                        roomOccupancies.Add(new RoomOccupancy(ra[j].roomID, ra[j].available, ra[j].overtime));
                    }
                }
            }

            if (roomOccupancies.Count > 0)
            {
                RepeaterOccupiedRoom.DataSource = roomOccupancies;
                RepeaterOccupiedRoom.DataBind();

                lblNoItemFound.Visible = false;
            }
            else
            {
                RepeaterOccupiedRoom.DataSource = null;
                RepeaterOccupiedRoom.DataBind();

                lblNoItemFound.Enabled = true;
            }

        }

        protected void RepeaterOccupiedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblRoomID = e.Item.FindControl("lblRoomID") as Label;
            Label lblRoomType = e.Item.FindControl("lblRoomType") as Label;
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblOvertimeStatus = e.Item.FindControl("lblOvertimeStatus") as Label;
            Label lblGuestName = e.Item.FindControl("lblGuestName") as Label;

            setRoomType(lblRoomID.Text, lblRoomType);

            setRoomStatus(lblStatus, lblOvertimeStatus, lblRoomID.Text);

            setGuestName(lblRoomID.Text, lblGuestName);
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

        private void setRoomStatus(Label lblStatus, Label lblOvertimeStatus, string roomID)
        {
            if (lblOvertimeStatus.Text == "True")
            {
                lblStatus.Text = "Overtime";
                lblStatus.Style["color"] = "red";
            }
            else
            {
                // Check if the room is checkout today
                lblStatus.Text = checkOutTodays(roomID);
                lblStatus.Style["color"] = "#00ce1b";

            }
        }

        private string checkOutTodays(string roomID)
        {
            // Get previous date 
            DateTime dateNow = DateTime.Now;
            string yesterdayDate = dateNow.AddDays(-1).ToShortDateString();

            conn = new SqlConnection(strCon);
            conn.Open();

            string checkIfCheckOutTodays = "SELECT COUNT(*) FROM ReservationRoom RR, Reservation R " + 
                                            "WHERE RR.RoomID LIKE @RoomID AND " +
                                            "R.CheckOutDate LIKE @CheckOutDate AND " +
                                            "R.Status IN('Checked In', 'Check In') AND " + 
                                            "R.ReservationID LIKE RR.ReservationID AND " + 
                                            "RR.Date LIKE @ReservedDate";

            SqlCommand cmdCheckIfCheckOutTodays = new SqlCommand(checkIfCheckOutTodays, conn);

            cmdCheckIfCheckOutTodays.Parameters.AddWithValue("@RoomID", roomID);
            cmdCheckIfCheckOutTodays.Parameters.AddWithValue("@CheckOutDate", reservationUtility.formatDate(dateNow.ToShortDateString()));
            cmdCheckIfCheckOutTodays.Parameters.AddWithValue("@ReservedDate", reservationUtility.formatDate(yesterdayDate));

            int count = (int)cmdCheckIfCheckOutTodays.ExecuteScalar();

            conn.Close();

            if(count > 0)
            {
                return "Check Out Today";
            }
            else
            {
                return "";
            }
            
        }

        private void setGuestName(string roomID, Label lblGuestName)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getGuestName = "SELECT Top 1 G.Name " +
                                "FROM Reservation R, ReservationRoom RR, Guest G " +
                                "WHERE R.Status IN('Checked In', 'Check In') AND RR.RoomID LIKE @RoomID " +
                                "AND R.ReservationID LIKE RR.ReservationID " +
                                "AND R.GuestID LIKE G.GuestID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@RoomID", roomID);

            try
            {
                lblGuestName.Text = (string)cmdGetGuestName.ExecuteScalar();

            }
            catch
            {
                lblGuestName.Text = "";
            }

            conn.Close();
        }
    }
}