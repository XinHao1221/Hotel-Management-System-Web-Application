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
using Hotel_Management_System.Front_Desk.CheckIn;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.CheckOut
{
    public partial class EquipmentCheckList : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private string reservationID;

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

                Session["MissingEquipments"] = new List<MissingEquipment>();
            }
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

            if (reservedRooms.Count > 1)
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

            String getEquipments = "SELECT EquipmentID, Title FROM Equipment WHERE RoomTypeID LIKE @RoomTypeID";

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
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            // *** Get Missing Equipment From Equipment CheckList from repeater ***
            foreach (RepeaterItem item in RepeaterReservedRoom.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater RepeaterEquipmentCheckList = item.FindControl("RepeaterEquipmentCheckList") as Repeater;

                    foreach(RepeaterItem childItem in RepeaterEquipmentCheckList.Items)
                    {
                        // Find control from RepeaterReservedRoom
                        Label lblRoomTypeID = item.FindControl("lblRoomTypeID") as Label;
                        Label lblRoomType = item.FindControl("lblRoomType") as Label;
                        Label lblSelectedRoomID = item.FindControl("lblSelectedRoomID") as Label;
                        Label lblSelectedRoomNo = item.FindControl("lblSelectedRoomNo") as Label;

                        Label lblEquipmentList = (Label)FindControlRecursive(childItem, "lblEquipmentList");
                        Label lblEquipmentID = (Label)FindControlRecursive(childItem, "lblEquipmentID");
                        CheckBox cbEquipmentCheckList = (CheckBox)FindControlRecursive(childItem, "cbEquipmentCheckList");

                        try
                        {
                            if (cbEquipmentCheckList.Checked == false)
                            {
                                MissingEquipment me = new MissingEquipment(lblEquipmentID.Text, lblEquipmentList.Text,
                                    lblRoomTypeID.Text, lblRoomType.Text, lblSelectedRoomID.Text, lblSelectedRoomNo.Text);

                                missingEquipments.Add(me);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    
                }
            }


            Response.Redirect("CheckOutConfirmation.aspx?ID=" + en.encryption(reservationID));
        }

        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            return root.Controls.Cast<Control>()
               .Select(c => FindControlRecursive(c, id))
               .FirstOrDefault(c => c != null);
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

        protected void formBtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckOutGuest.aspx?ID=" + en.encryption(reservationID));
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckOutGuest.aspx?ID=" + en.encryption(reservationID));
        }
    }
}