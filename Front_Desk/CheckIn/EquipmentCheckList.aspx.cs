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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public partial class EquipmentCheckList : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private String reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Equipment Checklist";

            if (!IsPostBack)
            {
                setReservedRoomToRepeater();
            }

            // Navigate to previous page
            // If back button clicked
            this.formBtnBack.OnClientClick = "javascript:window.history.go(-1);return false;";

        }

        private List<RoomEquipment> getRoomNoRented()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = reservationDetails.reservedRoom;

            List<RoomEquipment> roomEquipment = new List<RoomEquipment>();

            // Set the first item into RoomEquipment
            roomEquipment.Add(new RoomEquipment(reservedRooms[0].reservationRoomID, reservedRooms[0].roomTypeID, 
                reservedRooms[0].roomTypeName, reservedRooms[0].roomNo, reservedRooms[0].roomID));

            if(reservedRooms.Count > 1)
            {
                for (int i = 0; i < reservedRooms.Count; i++)
                {
                    // check if same, 1 = same, 0 = not same
                    int condition = 0;

                    for (int j = 0; j < roomEquipment.Count; j++)
                    {
                        
                        if (reservedRooms[i].roomID == roomEquipment[j].roomID)
                        {
                            condition = 1;
                        }
                    }

                    if (condition != 1)
                    {
                        roomEquipment.Add(new RoomEquipment(reservedRooms[i].reservationRoomID, reservedRooms[i].roomTypeID,
                        reservedRooms[i].roomTypeName, reservedRooms[i].roomNo, reservedRooms[i].roomID));
                    }

                }
            }

            return roomEquipment;
            
        }

        private void setReservedRoomToRepeater()
        {
            RepeaterReservedRoom.DataSource = getRoomNoRented();
            RepeaterReservedRoom.DataBind();
        }

        protected void RepeaterReservedRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get the reference of RoomTypeID and Repeater
            Label lblRoomTypeID = e.Item.FindControl("lblRoomTypeID") as Label;
            Repeater RepeaterEquipmentCheckList = e.Item.FindControl("RepeaterEquipmentCheckList") as Repeater;

            conn = new SqlConnection(strCon);
            conn.Open();

            String getEquipments = "SELECT Title FROM Equipment WHERE RoomTypeID LIKE @RoomTypeID";

            SqlCommand cmdGetEquipments = new SqlCommand(getEquipments, conn);

            cmdGetEquipments.Parameters.AddWithValue("@RoomTypeID", lblRoomTypeID.Text);

            SqlDataReader sdr = cmdGetEquipments.ExecuteReader();

            // Bind the data to RepeaterEquipmentCheckList
            RepeaterEquipmentCheckList.DataSource = sdr;
            RepeaterEquipmentCheckList.DataBind();

            conn.Close();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("CheckInConfirmation.aspx?ID=" + en.encryption(reservationID));
            }
        }

        protected void CVEquipmentCheckList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CheckBox cbEquipmentCheckList = (CheckBox)((RepeaterItem)((Control)source).Parent).FindControl("cbEquipmentCheckList");

            Label lblEquipmentList = (Label)((RepeaterItem)((Control)source).Parent).FindControl("lblEquipmentList");


            if (cbEquipmentCheckList.Checked == false)
            {
                lblEquipmentList.Style["color"] = "red";
                args.IsValid = false;
            }
            else
            {
                lblEquipmentList.Style["color"] = "black";
                args.IsValid = true;
            }


        }
    }
}