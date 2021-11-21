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
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management_System.Dashboard
{
    public partial class RoomAvailability : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Room Availability";

            if (!IsPostBack)
            {
                setItemToRepeaterRoomAvailability();
                setRoomType();
                ddlRoomType.Items.Insert(0, new ListItem("All", ""));
            }
            
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
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

        private void setItemToRepeaterRoomAvailability()
        {
            List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

            List<RoomOccupancy> roomOccupancies = new List<RoomOccupancy>();

            for(int i = 0; i < roomTypes.Count; i++)
            {
                List<RoomOccupancy> ra = roomTypes[i].roomOccupancies;

                for(int j = 0; j < ra.Count; j++)
                {
                    if(ra[j].status != "Blocked")
                    {
                        roomOccupancies.Add(new RoomOccupancy(ra[j].roomID, ra[j].available));
                    }
                }
            }

            if(roomOccupancies.Count > 0)
            {
                RepeaterRoomAvailability.DataSource = roomOccupancies;
                RepeaterRoomAvailability.DataBind();

                lblNoItemFound.Visible = false;
            }
            else
            {
                ddlRoomType.Enabled = false;
                lblNoItemFound.Enabled = true;
            }
            
        }

        protected void RepeaterRoomAvailability_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblRoomID = e.Item.FindControl("lblRoomID") as Label;
            Label lblRoomType = e.Item.FindControl("lblRoomType") as Label;
            Label lblHousekeepingStatus = e.Item.FindControl("lblHousekeepingStatus") as Label;
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblAvailability = e.Item.FindControl("lblAvailability") as Label;

            formatHousekeepingStatusColor(lblHousekeepingStatus);

            setRoomType(lblRoomID.Text, lblRoomType);

            setRoomStatus(lblStatus, lblAvailability);

        }

        private void formatHousekeepingStatusColor(Label lblHousekeepingStatus)
        {
            if(lblHousekeepingStatus.Text == "Clean")
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

        private void setRoomStatus(Label lblStatus, Label lblAvailability)
        {
            if(lblAvailability.Text == "True")
            {
                lblStatus.Text = "Available";
                lblStatus.Style["color"] = "rgb(0, 206, 27)";
            }
            else
            {
                lblStatus.Text = "Unavailable";
                lblStatus.Style["color"] = "red";
            }
        }

        protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomTypeID = ddlRoomType.SelectedValue;

            List<RoomOccupancy> roomOccupancies = new List<RoomOccupancy>();

            if (roomTypeID != "")
            {
                List<RoomType> roomTypes = (List<RoomType>)Session["RoomType"];

                for (int i = 0; i < roomTypes.Count; i++)
                {
                    if (roomTypes[i].roomTypeID == roomTypeID)
                    {
                        if(roomTypes[i].roomOccupancies.Count > 0)
                        {
                            List<RoomOccupancy> ra = roomTypes[i].roomOccupancies;

                            for (int j = 0; j < ra.Count; j++)
                            {
                                if (ra[j].status != "Blocked")
                                {
                                    roomOccupancies.Add(new RoomOccupancy(ra[j].roomID, ra[j].available));
                                }
                            }

                            RepeaterRoomAvailability.DataSource = roomOccupancies;
                            RepeaterRoomAvailability.DataBind();

                            lblNoItemFound.Visible = false;
                        }
                        else
                        {
                            RepeaterRoomAvailability.DataSource = roomTypes[i].roomOccupancies;
                            RepeaterRoomAvailability.DataBind();

                            lblNoItemFound.Visible = true;
                        }
                        
                    }
                }
            }
            else
            {
                setItemToRepeaterRoomAvailability();
            }
            
        }
    }
}