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
    public partial class AddGuest : System.Web.UI.Page
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

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Check if user have enter any value
            if (txtName.Text == "")
            {
                Response.Redirect("Guest.aspx");
            }

            // If no show popup message
            PopupBack.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            conn = new SqlConnection(strCon);
            conn.Open();

            String nextGuestID = idGenerator.getNextID("GuestID", "Guest", "G");

            // Add new guest
            addGuest(nextGuestID);

            // Add Preferences
            List<Preference> tableList = (List<Preference>)Session["PreferenceList"];

            if (tableList.Any())    // Check if any preferences
            {
                addPreferences(nextGuestID);
            }

            conn.Close();

            //Response.Redirect("PreviewGuest.aspx?ID=" + en.encryption(nextGuestID));
        }


        // Add new guest
        private void addGuest(string nextGuestID)
        {
            // Add new guest
            String addGuest = "INSERT INTO Guest VALUES (@ID, @Name, @Title, @Phone, @Email, @IDType, @IDNo, @Nationality, @DOB, @Gender)";

            SqlCommand cmdAddGuest = new SqlCommand(addGuest, conn);

            cmdAddGuest.Parameters.AddWithValue("@ID", nextGuestID);
            cmdAddGuest.Parameters.AddWithValue("@Name", txtName.Text);
            cmdAddGuest.Parameters.AddWithValue("@Title", ddlTitle.SelectedValue);
            cmdAddGuest.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cmdAddGuest.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmdAddGuest.Parameters.AddWithValue("@IDType", ddlIDType.SelectedValue);
            cmdAddGuest.Parameters.AddWithValue("@IDNo", txtIDNo.Text);
            cmdAddGuest.Parameters.AddWithValue("@Nationality", ddlNationality.SelectedValue);
            cmdAddGuest.Parameters.AddWithValue("@DOB", txtDOB.Text);
            cmdAddGuest.Parameters.AddWithValue("@Gender", ddlGender.Text);

            int i = cmdAddGuest.ExecuteNonQuery();

        }

        // Add Guest's preferences
        private void addPreferences(string nextGuestID)
        {
            List<Preference> preferenceList = (List<Preference>)Session["PreferenceList"];

            if (preferenceList.Count > 0)
            {
                Preference preference = new Preference();

                for (int j = 1; j <= preferenceList.Count; j++)
                {
                    String nextPreferenceID = idGenerator.getNextID("PreferenceID", "Preference", "P");
                    preference = preferenceList[j - 1];

                    String addPreferneces = "INSERT INTO Preference VALUES (@ID, @Preference, @Date, @GuestID)";

                    SqlCommand cmdAddPreferences = new SqlCommand(addPreferneces, conn);

                    cmdAddPreferences.Parameters.AddWithValue("@ID", nextPreferenceID);
                    cmdAddPreferences.Parameters.AddWithValue("@Preference", preference.preference);
                    cmdAddPreferences.Parameters.AddWithValue("@Date", preference.date);
                    cmdAddPreferences.Parameters.AddWithValue("@GuestID", nextGuestID);

                    int i = cmdAddPreferences.ExecuteNonQuery();
                }

            }
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupReset.Visible = true;
            PopupCover.Visible = true;
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
            Response.Redirect("AddGuest.aspx");
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Guest.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }
    }
}