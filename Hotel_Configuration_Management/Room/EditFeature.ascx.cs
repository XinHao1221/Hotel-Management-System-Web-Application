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

namespace Hotel_Management_System.Hotel_Configuration_Management.Room
{
    public partial class EditFeature : System.Web.UI.UserControl
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String roomID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        protected void Page_Load(object sender, EventArgs e)
        {
            roomID = Request.QueryString["ID"];
            roomID = en.decryption(roomID);

            //roomID = "RM10000002";

            if (!IsPostBack)
            {
                setFeature();
            }
        }

        private void setFeature()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFeature = "SELECT * FROM Feature WHERE RoomID LIKE @ID";

            SqlCommand cmdGetFeature = new SqlCommand(getFeature, conn);

            cmdGetFeature.Parameters.AddWithValue("@ID", roomID);

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetFeature);


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
            else
            {
                lblNoItemFound.Visible = false;
            }

            conn.Close();
        }

        protected void IBDeleteFeature_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get feature name for the selected item
            String featureID = (item.FindControl("lblFeatureID") as Label).Text;
            String feature = (item.FindControl("lblFeature") as Label).Text;

            // Set Feature ID to ViewState
            ViewState["FeatureID"] = featureID;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Feature: " + feature + "<br /><br />";

            PopupCover.Visible = true;
            PopupDelete.Visible = true;

        }

        protected void btnDeleteFeature_Click(object sender, EventArgs e)
        {
            // Delete selected feature
            String featureID = ViewState["FeatureID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String deleteFeature = "DELETE FROM Feature WHERE FeatureID LIKE @ID";

            SqlCommand cmdDeleteFeature= new SqlCommand(deleteFeature, conn);

            cmdDeleteFeature.Parameters.AddWithValue("@ID", featureID);

            int i = cmdDeleteFeature.ExecuteNonQuery();

            conn.Close();

            // Close popup message
            PopupCover.Visible = false;
            PopupDelete.Visible = false;

            // Refresh feature list
            setFeature();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnSaveFeature_Click(object sender, EventArgs e)
        {
            // Generate next feature's id
            String nextFeatureID = idGenerator.getNextID("FeatureID", "Feature", "FT");

            conn = new SqlConnection(strCon);
            conn.Open();

            String addFeature = "INSERT INTO Feature VALUES (@ID, @Feature, @RoomID)";

            SqlCommand cmdAddFeature = new SqlCommand(addFeature, conn);

            cmdAddFeature.Parameters.AddWithValue("@ID", nextFeatureID);
            cmdAddFeature.Parameters.AddWithValue("@Feature", txtFeature.Text);
            cmdAddFeature.Parameters.AddWithValue("@RoomID", roomID);

            int i = cmdAddFeature.ExecuteNonQuery();

            conn.Close();

            txtFeature.Text = null;

            // Refresh feature's list
            setFeature();


        }
    }
}