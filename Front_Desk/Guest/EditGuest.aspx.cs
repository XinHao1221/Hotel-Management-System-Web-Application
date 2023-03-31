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

namespace Hotel_Management_System.Front_Desk.Guest
{
    public partial class EditGuest : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String guestID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private object cmdAddGuest;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Edit Guest";

            guestID = Request.QueryString["ID"];

            guestID = en.decryption(guestID);

            //guestID = "G10000001";

            if (!IsPostBack)
            {
                // Save link for previous page
                ViewState["PreviousPage"] = Request.UrlReferrer.ToString();

                //PopupCover.Visible = false;
                setData();
            }

            
        }

        private void setData()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get guest
            String getGuest = "SELECT * FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuest = new SqlCommand(getGuest, conn);

            cmdGetGuest.Parameters.AddWithValue("@ID", guestID);

            SqlDataReader sdr = cmdGetGuest.ExecuteReader();

            if (sdr.Read())
            {
                ddlTitle.SelectedValue = sdr.GetString(sdr.GetOrdinal("Title"));

                txtName.Text = sdr.GetString(sdr.GetOrdinal("Name"));

                ddlGender.SelectedValue = sdr.GetString(sdr.GetOrdinal("Gender"));

                ddlIDType.SelectedValue = sdr.GetString(sdr.GetOrdinal("IDType"));

                txtIDNo.Text = sdr.GetString(sdr.GetOrdinal("IDNo"));

                ddlNationality.SelectedValue = sdr.GetString(sdr.GetOrdinal("Nationality"));

                if(sdr.GetString(sdr.GetOrdinal("DOB")) != "")
                {
                    txtDOB.Text = sdr.GetString(sdr.GetOrdinal("DOB"));
                    getAge(sdr.GetString(sdr.GetOrdinal("DOB")));
                }
                else
                {
                    txtDOB.Text = "";
                }
                

                txtPhone.Text = sdr.GetString(sdr.GetOrdinal("Phone"));

                txtEmail.Text = sdr.GetString(sdr.GetOrdinal("Email"));
            }

            conn.Close();

        }

        protected void txtDOB_TextChanged(object sender, EventArgs e)
        {
            // Get Age of guest
            DateTime dob = Convert.ToDateTime(txtDOB.Text);
            DateTime currentDate = DateTime.Now;

            // Get total day
            double totalDay = Convert.ToDouble((currentDate - dob).TotalDays);

            // Convert total day in data of birth
            Decimal age = Math.Ceiling(Convert.ToDecimal(totalDay / 365));

            lblAge.Text = age.ToString();

        }

        private void getAge(string dateOfBirth)
        {
            // Get Age of guest
            DateTime dob = Convert.ToDateTime(dateOfBirth);
            DateTime currentDate = DateTime.Now;

            // Get total day
            double totalDay = Convert.ToDouble((currentDate - dob).TotalDays);

            // Convert total day in data of birth
            Decimal age = Math.Ceiling(Convert.ToDecimal(totalDay / 365));

            lblAge.Text = age.ToString();
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

            // Update guest
            String updateGuest = "UPDATE Guest " +
                "SET Name = @Name, Title = @Title, Phone = @Phone, Email = @Email, IDType = @IDType, IDNo = @IDNo, Nationality = @Nationality, " +
                "DOB = @DOB, Gender = @Gender WHERE GuestID LIKE @ID";

            SqlCommand cmdUpdateGuest = new SqlCommand(updateGuest, conn);

            cmdUpdateGuest.Parameters.AddWithValue("@ID", guestID);
            cmdUpdateGuest.Parameters.AddWithValue("@Name", txtName.Text);
            cmdUpdateGuest.Parameters.AddWithValue("@Title", ddlTitle.SelectedValue);
            cmdUpdateGuest.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cmdUpdateGuest.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmdUpdateGuest.Parameters.AddWithValue("@IDType", ddlIDType.SelectedValue);
            cmdUpdateGuest.Parameters.AddWithValue("@IDNo", txtIDNo.Text);

            cmdUpdateGuest.Parameters.AddWithValue("@Nationality", ddlNationality.SelectedValue);
            if (txtDOB.Text != "")
            {
                cmdUpdateGuest.Parameters.AddWithValue("@DOB", txtDOB.Text);
            }
            else
            {
                cmdUpdateGuest.Parameters.AddWithValue("@DOB", "");
            }

            cmdUpdateGuest.Parameters.AddWithValue("@Gender", ddlGender.Text);

            int i = cmdUpdateGuest.ExecuteNonQuery();

            conn.Close();

            // Redirect to View Guest Page
            Response.Redirect("ViewGuest.aspx?ID=" + en.encryption(guestID));
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true; 
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }

        protected void btnPopupConfirmReset_Click(object sender, EventArgs e)
        {
            // Reset all text field
            ddlTitle.SelectedIndex = 0;
            txtName.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlIDType.SelectedIndex = 0;
            txtIDNo.Text = "";
            ddlNationality.SelectedIndex = 0;
            txtDOB.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            lblAge.Text = "";


            PopupCover.Visible = false;
            PopupReset.Visible = false;
        }


    }
}