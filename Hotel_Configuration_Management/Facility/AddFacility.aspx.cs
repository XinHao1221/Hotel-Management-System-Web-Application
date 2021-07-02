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
    public partial class AddFacility : System.Web.UI.Page
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
            PopupReset.Visible = false;
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String nextFacilityID = idGenerator.getNextID("FacilityID", "Facility", "FC");

            // SQL command to insert facility
            String addFacility = "INSERT INTO Facility VALUES (@FacilityID, @FacilityName, @Description, @Status, @Quantity, @Price, @PriceType)";

            SqlCommand cmdAddFacility = new SqlCommand(addFacility, conn);

            cmdAddFacility.Parameters.AddWithValue("@FacilityID", nextFacilityID);
            cmdAddFacility.Parameters.AddWithValue("@FacilityName", txtFacilityName.Text);
            cmdAddFacility.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdAddFacility.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            if (txtQty.Text == "")
            {
                cmdAddFacility.Parameters.AddWithValue("@Quantity", 0);
            }
            else
            {
                cmdAddFacility.Parameters.AddWithValue("@Quantity", int.Parse(txtQty.Text));
            }
            
            cmdAddFacility.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
            cmdAddFacility.Parameters.AddWithValue("@PriceType", ddlPriceType.SelectedValue);

            int i = cmdAddFacility.ExecuteNonQuery();

            conn.Close();

            if (i > 0)
            {
                Response.Redirect("PreviewFacility.aspx?ID=" + en.encryption(nextFacilityID));
            }
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = false;
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnPopupConfirm_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = false;
            PopupCover.Visible = false;

            Response.Redirect("AddFacility.aspx");
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Facility.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Check if user have enter any value
            if (txtFacilityName.Text == "")
            {
                Response.Redirect("Facility.aspx");
            }

            // If no show popup message
            PopupCover.Visible = true;
            PopupBack.Visible = true;
        }
    }
}