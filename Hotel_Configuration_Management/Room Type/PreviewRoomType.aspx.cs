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
    public partial class PreviewRoomType : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String roomTypeID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            roomTypeID = "RT10000002";

            setText();
        }

        private void setText()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomType = "SELECT * FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            SqlDataReader sdr = cmdGetRoomType.ExecuteReader();

            if (sdr.Read())
            {
                lblTitle.Text = sdr.GetString(sdr.GetOrdinal("Title"));
                lblShortCode.Text = sdr.GetString(sdr.GetOrdinal("ShortCode"));
                lblDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));
                lblBaseOccupancy.Text = sdr.GetValue(sdr.GetOrdinal("BaseOccupancy")).ToString();
                lblHigherOccupancy.Text = sdr.GetValue(sdr.GetOrdinal("HigherOccupancy")).ToString();
                
                // Set status to checkbox
                String extraBed = sdr.GetString(sdr.GetOrdinal("ExtraBed"));
                if (extraBed == "True")
                {
                    IChecked.Visible = true;
                    lblExtraBedPrice.Text = sdr.GetValue(sdr.GetOrdinal("ExtraBedPrice")).ToString();
                    pnExtraBedPrice.Visible = true;
                }
                else
                {
                    cbExtraBed.Visible = true;
                }
            }
        }
    }
}