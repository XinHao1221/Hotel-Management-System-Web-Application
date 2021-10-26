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
    public partial class ViewGuest : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String guestID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            guestID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Guest Details";

            //guestID = "G10000001";

            setText();

            setPreference();
        }

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // Get current guestID
            guestID = en.encryption(guestID);

            // Redirect to edit page
            Response.Redirect("EditGuest.aspx?ID=" + guestID);
        }

        private void setText()
        {
            // Open database connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // Guest specific guest's details
            String getGuest = "SELECT * FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuest = new SqlCommand(getGuest, conn);

            cmdGetGuest.Parameters.AddWithValue("@ID", guestID);

            SqlDataReader sdr = cmdGetGuest.ExecuteReader();

            if (sdr.Read())
            {
                lblName.Text = sdr.GetString(sdr.GetOrdinal("Title")) + " " +
                        sdr.GetString(sdr.GetOrdinal("Name"));

                lblGender.Text = sdr.GetString(sdr.GetOrdinal("Gender"));

                lblIDType.Text = sdr.GetString(sdr.GetOrdinal("IDType"));

                lblIDNo.Text = sdr.GetString(sdr.GetOrdinal("IDNo"));

                lblNationality.Text = sdr.GetString(sdr.GetOrdinal("Nationality"));

                lblDOB.Text = sdr.GetString(sdr.GetOrdinal("DOB"));

                getAge(sdr.GetString(sdr.GetOrdinal("DOB")));

                lblPhoneNo.Text = sdr.GetString(sdr.GetOrdinal("Phone"));

                lblEmail.Text = sdr.GetString(sdr.GetOrdinal("Email"));
            }

            conn.Close();
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

        private void setPreference()
        {
            // Open database connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get preference under selected guest
            String getPreference = "SELECT * FROM Preference WHERE GuestID LIKE @ID";

            SqlCommand cmdGetPreference = new SqlCommand(getPreference, conn);

            cmdGetPreference.Parameters.AddWithValue("@ID", guestID);


            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetPreference);


            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            // Bind data into repeater to display
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            if (dt.Rows.Count <= 0)
            {
                lblNoItemFound.Visible = true;
            }

            conn.Close();
        }
    }
}