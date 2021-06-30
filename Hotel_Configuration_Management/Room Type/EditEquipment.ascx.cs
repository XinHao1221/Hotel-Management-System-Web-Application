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
    public partial class EditEquipment : System.Web.UI.UserControl
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of IDEncrptions class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private String roomTypeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //roomTypeID = ViewState["RoomTypeID"].ToString();

            //roomTypeID = "RT10000003";

            roomTypeID = Request.QueryString["ID"];
            roomTypeID = en.decryption(roomTypeID);

            if (!IsPostBack)
            {
                setEquipment();
            }
            
        }

        protected void btnSaveEquipment_Click(object sender, EventArgs e)
        {
            String nextEquipmentID = idGenerator.getNextID("EquipmentID", "Equipment", "E");

            conn = new SqlConnection(strCon);
            conn.Open();

            String addEquipment = "INSERT INTO Equipment VALUES (@EquipmentID, @Title, @FineCharges, @RoomTypeID)";

            SqlCommand cmdAddEquipment = new SqlCommand(addEquipment, conn);

            cmdAddEquipment.Parameters.AddWithValue("@EquipmentID", nextEquipmentID);
            cmdAddEquipment.Parameters.AddWithValue("@Title", txtEquipment.Text);
            cmdAddEquipment.Parameters.AddWithValue("@FineCharges", Convert.ToDecimal(txtEquipmentPrice.Text));
            cmdAddEquipment.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            int i = cmdAddEquipment.ExecuteNonQuery();

            conn.Close();

            // Refresh Equipment List
            setEquipment();

        }

        public void setEquipment()
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

            if (dt.Rows.Count <= 0)
            {
                lblNoItemFound.Visible = true;
            }

            conn.Close();
        }

        protected void IBDeleteEquipment_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get floorName for the selected item
            String equipmentID = (item.FindControl("lblEquipmentID") as Label).Text;
            String equipmentName = (item.FindControl("lblEquipmentName") as Label).Text;
            String fineCharges = (item.FindControl("lblFineCharges") as Label).Text;

            // Set EquipmentID to ViewState
            ViewState["EquipmentID"] = equipmentID;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Equipment: " + equipmentName + "<br />" +
                "Fine Charges: " + fineCharges + "<br /><br />";

            PopupCover.Visible = true;
            PopupDelete.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete selected equipment
            String equipmentID = ViewState["EquipmentID"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String deleteEquipment = "DELETE FROM Equipment WHERE EquipmentID LIKE @ID";

            SqlCommand cmdDeleteEquipment = new SqlCommand(deleteEquipment, conn);

            cmdDeleteEquipment.Parameters.AddWithValue("@ID", equipmentID);

            int i = cmdDeleteEquipment.ExecuteNonQuery();

            conn.Close();

            // Close popup message
            PopupCover.Visible = false;
            PopupDelete.Visible = false;
            
            // Refresh equipment list
            setEquipment();

        }
    }
}