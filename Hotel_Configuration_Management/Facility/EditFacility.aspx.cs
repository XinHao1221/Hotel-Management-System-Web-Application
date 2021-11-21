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

namespace Hotel_Management_System.Hotel_Configuration_Management.Facility
{
    public partial class EditFacility : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String facilityID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Edit Facility";

            facilityID = Request.QueryString["ID"];

            facilityID = en.decryption(facilityID);

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

            String getFacility = "SELECT * FROM Facility WHERE FacilityID LIKE @ID";

            SqlCommand cmdGetFacility = new SqlCommand(getFacility, conn);

            cmdGetFacility.Parameters.AddWithValue("@ID", facilityID);

            SqlDataReader sdr = cmdGetFacility.ExecuteReader();

            if (sdr.Read())
            {
                txtFacilityName.Text = sdr.GetString(sdr.GetOrdinal("FacilityName"));
                txtDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));

                // Get quantity
                txtQty.Text = sdr.GetValue(4).ToString();

                // Get Price
                txtPrice.Text = sdr.GetValue(sdr.GetOrdinal("Price")).ToString();

                ddlPriceType.SelectedValue = sdr.GetString(sdr.GetOrdinal("PriceType"));

                ddlStatus.SelectedValue = sdr.GetString(sdr.GetOrdinal("Status"));
            }

            conn.Close();
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

            // SQL command to update facility
            String updateFacility = "UPDATE Facility SET FacilityName = @FacilityName, Description = @Description, Status = @Status, " +
                "Quantity = @Quantity, Price = @Price, PriceType = @PriceType WHERE FacilityID LIKE @FacilityID";

            SqlCommand cmdUpdateFacility = new SqlCommand(updateFacility, conn);

            cmdUpdateFacility.Parameters.AddWithValue("@FacilityID", facilityID);
            cmdUpdateFacility.Parameters.AddWithValue("@FacilityName", txtFacilityName.Text);
            cmdUpdateFacility.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdUpdateFacility.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            if (txtQty.Text == "")
            {
                cmdUpdateFacility.Parameters.AddWithValue("@Quantity", 0);
            }
            else
            {
                cmdUpdateFacility.Parameters.AddWithValue("@Quantity", int.Parse(txtQty.Text));
            }

            cmdUpdateFacility.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
            cmdUpdateFacility.Parameters.AddWithValue("@PriceType", ddlPriceType.SelectedValue);

            int i = cmdUpdateFacility.ExecuteNonQuery();

            conn.Close();

            if (i > 0)
            {
                Response.Redirect("ViewFacility.aspx?ID=" + en.encryption(facilityID));
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnPopupConfirm_Click(object sender, EventArgs e)
        {
            // Reset all value
            txtFacilityName.Text = "";
            txtDescription.Text = "";
            txtQty.Text = "";
            ddlPriceType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtPrice.Text = "";

            PopupCancel.Visible = false;
            PopupCover.Visible = false;

        }
    }
}