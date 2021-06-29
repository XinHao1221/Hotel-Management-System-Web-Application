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

namespace Hotel_Management_System.Hotel_Configuration_Management.Facility
{
    public partial class ViewFacility : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String facilityID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            facilityID = Request.QueryString["ID"];

            facilityID = en.decryption(facilityID);

            setText();
        }

        private void setText()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFacility = "SELECT * FROM Facility WHERE FacilityID LIKE @ID";

            SqlCommand cmdGetFacility = new SqlCommand(getFacility, conn);

            cmdGetFacility.Parameters.AddWithValue("@ID", facilityID);

            SqlDataReader sdr = cmdGetFacility.ExecuteReader();

            if (sdr.Read())
            {
                lblFacilityName.Text = sdr.GetString(sdr.GetOrdinal("FacilityName"));
                lblDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));

                lblStatus.Text = sdr.GetString(sdr.GetOrdinal("Status"));

                if (lblStatus.Text == "Active")
                {
                    lblStatus.Style["color"] = "#00ce1b";
                }
                else
                {
                    lblStatus.Style["color"] = "red";
                }

                // Get quantity
                lblQty.Text = sdr.GetValue(4).ToString();

                // Get Price
                lblPrice.Text = sdr.GetValue(sdr.GetOrdinal("Price")).ToString();

                lblPriceType.Text = sdr.GetString(sdr.GetOrdinal("PriceType"));

                
            }

            conn.Close();
        }

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // Get current FacilityID
            facilityID = en.encryption(facilityID);

            // Redirect to edit page
            Response.Redirect("EditFacility.aspx?ID=" + facilityID);
        }
    }
}