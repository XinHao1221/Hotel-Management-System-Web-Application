using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Hotel_Configuration_Management.Room
{
    public partial class AddRoom : System.Web.UI.Page
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RepeaterTableUltility
        RepeaterTableUtility ultility = new RepeaterTableUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set data to drop-down list
                setDropDownListData();

                ddlFloorNumber.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));
                ddlRoomType.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));
            }
            
        }

        private void setDropDownListData()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            setFloorNumber();
            setRoomType();

            conn.Close();
        }

        private void setFloorNumber()
        {

            // SQL command 
            String getFloorNumber = "SELECT * FROM Floor WHERE Status LIKE 'Active' ORDER BY FloorNumber";

            SqlCommand cmdGetFloorNumber = new SqlCommand(getFloorNumber, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetFloorNumber);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                ddlFloorNumber.Items.Add(new ListItem((dt.Rows[i]["FloorNumber"].ToString() + " - " + dt.Rows[i]["FloorName"].ToString()), dt.Rows[i]["FloorID"].ToString()));
            }
            //ddlFloorNumber.DataSource = dt;
            //ddlFloorNumber.DataBind();
            //ddlFloorNumber.DataTextField = "FloorNumber";
            //ddlFloorNumber.DataValueField = "FloorID";
            //ddlFloorNumber.DataBind();

        }

        private void setRoomType()
        {

            String getRoomType = "SELECT * FROM RoomType WHERE Status LIKE 'Active'";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoomType);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            ddlRoomType.DataSource = dt;
            ddlRoomType.DataBind();
            ddlRoomType.DataTextField = "Title";
            ddlRoomType.DataValueField = "RoomTypeID";
            ddlRoomType.DataBind();
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String nextRoomID = idGenerator.getNextID("RoomID", "Room", "RM");

            addRoom(nextRoomID);

            // Add Equipment 
            List<String> tableList = (List<String>)Session["FeatureList"];

            if (tableList.Any())    // Check if any equipment
            {
                addFeature(nextRoomID);
            }

            conn.Close();
        }

        private void addRoom(String nextRoomID)
        {
            String addRoom = "INSERT INTO Room VALUES (@RoomID, @RoomNumber, @FloorID, @RoomTypeID, @Status)";

            SqlCommand cmdAddRoom = new SqlCommand(addRoom, conn);

            cmdAddRoom.Parameters.AddWithValue("@RoomID", nextRoomID);
            cmdAddRoom.Parameters.AddWithValue("@RoomNumber", txtRoomNumber.Text);
            cmdAddRoom.Parameters.AddWithValue("@FloorID", ddlFloorNumber.SelectedValue);
            cmdAddRoom.Parameters.AddWithValue("@RoomTypeID", ddlRoomType.SelectedValue);
            cmdAddRoom.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            int i = cmdAddRoom.ExecuteNonQuery();
        }

        private void addFeature(String nextRoomID)
        {
            List<String> featuretList = (List<String>)Session["FeatureList"];

            if (featuretList.Count > 0)
            {

                for (int j = 1; j <= featuretList.Count; j++)
                {
                    String nextEquipmentID = idGenerator.getNextID("FeatureID", "Feature", "FT");

                    String addFeature = "INSERT INTO Feature VALUES (@ID, @Feature, @RoomID)";

                    SqlCommand cmdAddEquipment = new SqlCommand(addFeature, conn);

                    cmdAddEquipment.Parameters.AddWithValue("@ID", nextEquipmentID);
                    cmdAddEquipment.Parameters.AddWithValue("@Feature", featuretList[j - 1].ToString());
                    cmdAddEquipment.Parameters.AddWithValue("@RoomID", nextRoomID);

                    int i = cmdAddEquipment.ExecuteNonQuery();
                }

            }
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}