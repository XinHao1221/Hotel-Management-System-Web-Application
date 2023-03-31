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
            // Page TItle
            Page.Title = "Room Type Details";

            //roomTypeID = "RT10000001";
            roomTypeID = en.decryption(Request.QueryString["ID"]);

            setText();
            setEquipment();
        }

        private void setText()
        {
            // Open database connection
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

            conn.Close();
        }

        private void setEquipment()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getEquipment = "SELECT * FROM Equipment WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetEquipment = new SqlCommand(getEquipment, conn);

            cmdGetEquipment.Parameters.AddWithValue("@ID", roomTypeID);

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetEquipment);


            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            // Bind data into repeater to display
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            if(dt.Rows.Count <= 0)
            {
                lblNoItemFound.Visible = true;
            }

            conn.Close();
        }

        protected void LBPriceManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("../PriceManager/EditRegularPrice.aspx?ID=" + en.encryption(roomTypeID));
        }
    }
}