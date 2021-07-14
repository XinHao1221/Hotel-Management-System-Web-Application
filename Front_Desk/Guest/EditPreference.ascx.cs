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
    public partial class EditPreference : System.Web.UI.UserControl
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String guestID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        protected void Page_Load(object sender, EventArgs e)
        {
            guestID = "G10000001";

            if (!IsPostBack)
            {
                setPreference();
            }
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

        protected void IBDeletePreference_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get preference for the selected item
            String preferenceID = (item.FindControl("lblPreferenceID") as Label).Text;
            String preference = (item.FindControl("lblPreference") as Label).Text;

            // Set Preference ID to ViewState
            ViewState["PreferenceID"] = preferenceID;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Preference: " + preference + "<br /><br />";

            PopupCover.Visible = true;
            PopupDelete.Visible = true;

        }

        protected void btnDeletePreference_Click(object sender, EventArgs e)
        {
            // Delete selected feature
            String preferenceID = ViewState["PreferenceID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String deletePreference = "DELETE FROM Preference WHERE PreferenceID LIKE @ID";

            SqlCommand cmdDeletePreference = new SqlCommand(deletePreference, conn);

            cmdDeletePreference.Parameters.AddWithValue("@ID", preferenceID);

            int i = cmdDeletePreference.ExecuteNonQuery();

            conn.Close();

            // Close popup message
            PopupCover.Visible = false;
            PopupDelete.Visible = false;

            // Refresh preference list
            setPreference();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnSavePreference_Click(object sender, EventArgs e)
        {
            // Generate next preference id
            String nextPreferenceID = idGenerator.getNextID("PreferenceID", "Preference", "P");

            // Get Current Date
            DateTime dateTimeNow = DateTime.Now;

            conn = new SqlConnection(strCon);
            conn.Open();

            String addPreferneces = "INSERT INTO Preference VALUES (@ID, @Preference, @Date, @GuestID)";

            SqlCommand cmdAddPreferences = new SqlCommand(addPreferneces, conn);

            cmdAddPreferences.Parameters.AddWithValue("@ID", nextPreferenceID);
            cmdAddPreferences.Parameters.AddWithValue("@Preference", txtPreference.Text);
            cmdAddPreferences.Parameters.AddWithValue("@Date", dateTimeNow.ToShortDateString());
            cmdAddPreferences.Parameters.AddWithValue("@GuestID", guestID);

            int i = cmdAddPreferences.ExecuteNonQuery();

            conn.Close();

            txtPreference.Text = null;

            // Refresh feature's list
            setPreference();
        }
    }
}