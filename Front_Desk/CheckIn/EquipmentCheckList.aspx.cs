﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public partial class EquipmentCheckList : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            setReservedRoomToRepeater();
        }

        private void setReservedRoomToRepeater()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservedRoom> reservedRooms = reservationDetails.reservedRoom;

            RepeaterReservedRoom.DataSource = reservedRooms;
            RepeaterReservedRoom.DataBind();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {

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

        }

        protected void CVEquipmentCheckList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CheckBox cbEquipmentCheckList = (CheckBox)((RepeaterItem)((Control)source).Parent).FindControl("cbEquipmentCheckList");
            Label lblEquipmentList = (Label)((RepeaterItem)((Control)source).Parent).FindControl("lblEquipmentList");
            
            // Check if the checkList is checked
            if(cbEquipmentCheckList.Checked == false)
            {
                lblEquipmentList.Style["color"] = "red";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }


        }
    }
}