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

namespace Hotel_Management_System.Front_Desk.CheckOut
{
    public partial class EquipmentCheckList : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setReservedRoomToRepeater();

                Session["MissingEquipments"] = new List<MissingEquipment>();
            }

            // Navigate to previous page
            // If back button clicked
            this.formBtnBack.OnClientClick = "javascript:window.history.go(-1);return false;";
        }

        private void setReservedRoomToRepeater()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            List<ReservationRoom> reservedRooms = reservationDetails.reservedRoom;

            RepeaterReservedRoom.DataSource = reservedRooms;
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
                    // Find control from RepeaterReservedRoom
                    Label lblRoomTypeID = item.FindControl("lblRoomTypeID") as Label;
                    Label lblRoomType = item.FindControl("lblRoomType") as Label;
                    Label lblSelectedRoomID = item.FindControl("lblSelectedRoomID") as Label;
                    Label lblSelectedRoomNo = item.FindControl("lblSelectedRoomNo") as Label;

                    Label lblEquipmentList = (Label)FindControlRecursive(item, "lblEquipmentList");
                    Label lblEquipmentID = (Label)FindControlRecursive(item, "lblEquipmentID");
                    CheckBox cbEquipmentCheckList = (CheckBox)FindControlRecursive(item, "cbEquipmentCheckList");

                    if(cbEquipmentCheckList.Checked == false)
                    {
                        MissingEquipment me = new MissingEquipment(lblEquipmentID.Text, lblEquipmentList.Text,
                            lblRoomTypeID.Text, lblRoomType.Text, lblSelectedRoomID.Text, lblSelectedRoomNo.Text);

                        missingEquipments.Add(me);
                    }
                }
            }


            Response.Redirect("CheckOutConfirmation.aspx");
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

    }
}