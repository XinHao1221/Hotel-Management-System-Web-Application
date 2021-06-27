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


namespace Hotel_Management_System.Hotel_Configuration_Management.Floor
{
    public partial class AddFloor : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                setFloorNumber();
            }

            PopupCancel.Visible = false;
            PopupCover.Visible = false;
            
        }

        private void setFloorNumber()
        {
            var floorNumber = new List<int> { };

            // Assign 20 floorNumber into list
            for(int i = 1; i <= 20; i++)
            {
                floorNumber.Add(i);
            }

            // Remove floor numbers that have added previously
            var fn = getExistingFloorNumber();

            if(fn.Count != 0)
            {
                for(int i = 0; i < fn.Count; i++)
                {
                    floorNumber.Remove(fn[i]);
                }
            }

            ddlFloorNumber.DataSource = floorNumber;
            ddlFloorNumber.DataBind();
        }

        private List<int> getExistingFloorNumber()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command to get existing floor number from database
            String getFloorNumber = "SELECT FloorNumber FROM Floor WHERE Status IN ('Active', 'Suspend')";

            SqlCommand cmdGetFloorNumber = new SqlCommand(getFloorNumber, conn);

            SqlDataReader sdr = cmdGetFloorNumber.ExecuteReader();

            var floorNumber = new List<int> { };

            while (sdr.Read())
            { 
                for(int i = 0; i < sdr.FieldCount; i++)
                {
                    floorNumber.Add(int.Parse(sdr[i].ToString()));
                }
            }

            conn.Close();

            return floorNumber;

        }

        // Execute when user press save button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String nextFloorID = idGenerator.getNextID("FloorID", "Floor", "F");

            // SQL command to insert floor
            String addFloor = "INSERT INTO FLOOR VALUES (@FloorID, @FloorName, @FloorNumber, @Description, @status)";

            SqlCommand cmdAddFloor = new SqlCommand(addFloor, conn);

            cmdAddFloor.Parameters.AddWithValue("@FloorID", nextFloorID);
            cmdAddFloor.Parameters.AddWithValue("@FloorName", txtFloorName.Text);
            cmdAddFloor.Parameters.AddWithValue("@FloorNumber", int.Parse(ddlFloorNumber.Text)) ;
            cmdAddFloor.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdAddFloor.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);

            int i = cmdAddFloor.ExecuteNonQuery();

            conn.Close();

            if(i > 0)
            {
                Response.Redirect("PreviewFloor.aspx?ID=" + en.encryption(nextFloorID));
            }

            //Response.Redirect("~/Hotel_Configuration_Management/AddFloor.aspx");

        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnPopupConfirm_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = false;
            PopupCover.Visible = false;

            resetAll();
        }

        private void resetAll()
        {
            Response.Redirect("AddFloor.aspx");
        }

        protected void btnResetForm_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = true;
            PopupCover.Visible = true;
            
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {

            // Check if user have enter any value
            if(txtFloorName.Text == "")
            {
                Response.Redirect("Floor.aspx");
            }

            // If no show popup message
            PopupCover.Visible = true;
            PopupBack.Visible = true;
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Floor.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnCancelBack_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }
    }
}