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
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Hotel_Configuration_Management.Room_Type
{
    public partial class AddRoomType : System.Web.UI.Page
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of IDEncrptions class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Add Room Type";

            PopupReset.Visible = false;
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String nextRoomTypeID = idGenerator.getNextID("RoomTypeID", "RoomType", "RT");

            // Add New Type
            addRoomType(nextRoomTypeID);

            // Add Room Price
            addStandardRoomPrice(nextRoomTypeID);


            // Add Equipment 
            List<Equipment> tableList = (List<Equipment>)Session["EquipmentList"];

            if (tableList.Any())    // Check if any equipment
            {
                addEquipment(nextRoomTypeID);
            }

            conn.Close();

            Response.Redirect("PreviewRoomType.aspx?ID=" + en.encryption(nextRoomTypeID));
        }

        private void addRoomType(String nextRoomTypeID)
        {
            Boolean extraBed = cbExtraBed.Checked;

            // SQL command to get existing floor number from database
            String addRoomType = "INSERT INTO RoomType VALUES (@RoomTypeID, @Title, @ShortCode, @Description, @BaseOccupancy, @HigherOccupancy, @ExtraBed, @ExtraBedPrice, @Status)";

            SqlCommand cmdAddRoomType = new SqlCommand(addRoomType, conn);

            cmdAddRoomType.Parameters.AddWithValue("@RoomTypeID", nextRoomTypeID);
            cmdAddRoomType.Parameters.AddWithValue("@Title", txtTittle.Text);
            cmdAddRoomType.Parameters.AddWithValue("@ShortCode", txtShortCode.Text);
            cmdAddRoomType.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdAddRoomType.Parameters.AddWithValue("@BaseOccupancy", int.Parse(txtBaseOccupancy.Text));
            cmdAddRoomType.Parameters.AddWithValue("@HigherOccupancy", int.Parse(txtHigherOccupancy.Text));
            cmdAddRoomType.Parameters.AddWithValue("@ExtraBed", extraBed.ToString());
            cmdAddRoomType.Parameters.AddWithValue("@Status", "Active");

            if (extraBed == true)
            {
                cmdAddRoomType.Parameters.AddWithValue("@ExtraBedPrice", Convert.ToDecimal(txtExtraBedPrice.Text));
            }
            else
            {
                cmdAddRoomType.Parameters.AddWithValue("@ExtraBedPrice", Convert.ToDecimal("0.00"));
            }

            int i = cmdAddRoomType.ExecuteNonQuery();
        }

        private void addStandardRoomPrice(String nextRoomTypeID)
        {
            String nextStandardPriceID = idGenerator.getNextID("StandardPriceID", "StandardRoomPrice", "ST");

            String addStandardPrice = "INSERT INTO StandardRoomPrice VALUES (@StandardPriceID, @MondayPrice, @TuesdayPrice, @WednesdayPrice, " +
                "@ThursdayPrice, @Fridayprice, @SaturdayPrice, @SundayPrice, @RoomTypeID)";

            SqlCommand cmdAddStandardPrice = new SqlCommand(addStandardPrice, conn);

            cmdAddStandardPrice.Parameters.AddWithValue("@StandardPriceID", nextStandardPriceID);
            cmdAddStandardPrice.Parameters.AddWithValue("@MondayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@TuesdayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@WednesdayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@ThursdayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@Fridayprice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@SaturdayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@SundayPrice", Convert.ToDecimal(txtPrice.Text));
            cmdAddStandardPrice.Parameters.AddWithValue("@RoomTypeID", nextRoomTypeID);

            int i = cmdAddStandardPrice.ExecuteNonQuery();
        }

        private void addEquipment(String nextRoomTypeID)
        {
            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];

            if (equipmentList.Count > 0)
            {
                Equipment equipment = new Equipment();

                for (int j = 1; j <= equipmentList.Count; j++)
                {
                    String nextEquipmentID = idGenerator.getNextID("EquipmentID", "Equipment", "E");
                    equipment = equipmentList[j - 1];

                    String addEquipment = "INSERT INTO Equipment VALUES (@ID, @EquipmentName, @FineCharges, @RoomTypeID)";

                    SqlCommand cmdAddEquipment = new SqlCommand(addEquipment, conn);

                    cmdAddEquipment.Parameters.AddWithValue("@ID", nextEquipmentID);
                    cmdAddEquipment.Parameters.AddWithValue("@EquipmentName", equipment.equipmentName);
                    cmdAddEquipment.Parameters.AddWithValue("@FineCharges", equipment.fineCharges);
                    cmdAddEquipment.Parameters.AddWithValue("@RoomTypeID", nextRoomTypeID);

                    int i = cmdAddEquipment.ExecuteNonQuery();
                }

            }
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
        }

        protected void cbExtraBed_CheckedChanged(object sender, EventArgs e)
        {
            if(cbExtraBed.Checked == true)
            {
                RFVExtraBedPrice.Enabled = true;
                pnExtraBedPrice.Visible = true;
            }
            else
            {
                RFVExtraBedPrice.Enabled = false;
                pnExtraBedPrice.Visible = false;

            }
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
            Response.Redirect("AddRoomType.aspx");
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoomType.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Check if user have enter any value
            if (txtTittle.Text == "")
            {
                Response.Redirect("RoomType.aspx");
            }

            // If no show popup message
            PopupBack.Visible = true;
            PopupCover.Visible = true;
        }
    }
}