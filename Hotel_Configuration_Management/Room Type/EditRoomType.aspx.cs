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
    public partial class EditRoomType : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String roomTypeID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            roomTypeID = Request.QueryString["ID"];
            roomTypeID = en.decryption(roomTypeID);

            //roomTypeID = "RT10000001";

            if (!IsPostBack)
            {
                // Save link for previous page
                ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
                PopupCover.Visible = false;
                setData();
            }
        }

        private void setData()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomType = "SELECT * FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            SqlDataReader sdr = cmdGetRoomType.ExecuteReader();

            if (sdr.Read())
            {
                txtTittle.Text = sdr.GetString(sdr.GetOrdinal("Title"));
                txtShortCode.Text = sdr.GetString(sdr.GetOrdinal("ShortCode"));
                txtDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));
                txtBaseOccupancy.Text = sdr.GetValue(sdr.GetOrdinal("BaseOccupancy")).ToString();
                txtHigherOccupancy.Text = sdr.GetValue(sdr.GetOrdinal("HigherOccupancy")).ToString();

                // Set status to checkbox
                String extraBed = sdr.GetString(sdr.GetOrdinal("ExtraBed"));
                if (extraBed == "True")
                {
                    cbExtraBed.Checked = true;
                    txtExtraBedPrice.Text = sdr.GetValue(sdr.GetOrdinal("ExtraBedPrice")).ToString();
                    pnExtraBedPrice.Visible = true;
                }
                else
                {
                    cbExtraBed.Checked = false;
                }

            }

            conn.Close();
        }

        protected void cbExtraBed_CheckedChanged(object sender, EventArgs e)
        {
            if (cbExtraBed.Checked == true)
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

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Redirect to previous page
            Response.Redirect(ViewState["PreviousPage"].ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            Boolean extraBed = cbExtraBed.Checked;

            // SQL Command to update existing room type's details
            String updateRoomType = "UPDATE RoomType SET Title = @Title, ShortCode = @ShortCode, Description = @Description, " +
                "BaseOccupancy = @BaseOccupancy, HigherOccupancy = @HigherOccupancy, ExtraBed = @ExtraBed, ExtraBedPrice = @ExtraBedPrice WHERE RoomTypeID LIKE @RoomTypeID";

            SqlCommand cmdUpdateRoomType = new SqlCommand(updateRoomType, conn);

            cmdUpdateRoomType.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
            cmdUpdateRoomType.Parameters.AddWithValue("@Title", txtTittle.Text);
            cmdUpdateRoomType.Parameters.AddWithValue("@ShortCode", txtShortCode.Text);
            cmdUpdateRoomType.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdUpdateRoomType.Parameters.AddWithValue("@BaseOccupancy", int.Parse(txtBaseOccupancy.Text));
            cmdUpdateRoomType.Parameters.AddWithValue("@HigherOccupancy", int.Parse(txtHigherOccupancy.Text));
            cmdUpdateRoomType.Parameters.AddWithValue("@ExtraBed", extraBed.ToString());

            if (extraBed == true)
            {
                cmdUpdateRoomType.Parameters.AddWithValue("@ExtraBedPrice", Convert.ToDecimal(txtExtraBedPrice.Text));
            }
            else
            {
                cmdUpdateRoomType.Parameters.AddWithValue("@ExtraBedPrice", Convert.ToDecimal("0.00"));
            }

            int i = cmdUpdateRoomType.ExecuteNonQuery();

            conn.Close();

            Response.Redirect("ViewRoomType.aspx?ID=" + en.encryption(roomTypeID));
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
        }

        private void deleteAllEquipment()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String deleteAllEquipment = "DELETE FROM Equipment WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdDeleteAllEquipment = new SqlCommand(deleteAllEquipment, conn);

            cmdDeleteAllEquipment.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdDeleteAllEquipment.ExecuteNonQuery();

            conn.Close();
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }

        protected void btnPopupConfirmReset_Click(object sender, EventArgs e)
        {
            // Reset all text field
            txtTittle.Text = "";
            txtShortCode.Text = "";
            txtHigherOccupancy.Text = "";
            txtBaseOccupancy.Text = "";
            txtDescription.Text = "";
            txtExtraBedPrice.Text = "";
            cbExtraBed.Checked = false;
            pnExtraBedPrice.Visible = false;
            
            // *** Delete all equipment
            //deleteAllEquipment();
            //EC1.setEquipment();

            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }

        protected void LBPriceManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PriceManager/RegularRoomPrice.aspx");
        }
    }
}