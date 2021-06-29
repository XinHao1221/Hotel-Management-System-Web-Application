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

namespace Hotel_Management_System.Hotel_Configuration_Management.PriceManager
{
    public partial class EditRegularPrice : System.Web.UI.Page
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

        private String roomTypeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setRoomType();
                ddlRoomType.Items.Insert(0, new ListItem("-- Please Select --", "-- Please Select --"));

                roomTypeID = Request.QueryString["ID"];

                if (roomTypeID != null)
                {
                    // Check if the request is navigate from PreviewRoomType.aspx

                    roomTypeID = en.decryption(Request.QueryString["ID"]);

                    string roomType = getRoomType();

                    if (roomType != "")
                    {
                        ddlRoomType.Items.FindByText(roomType).Selected = true;
                        getStandardRoomPrice();
                    }
                }
                    



            }

            PopupCover.Visible = false;
            PopupSaved.Visible = false;
        }

        private string getRoomType()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomType = "SELECT Title FROM RoomType WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetRoomType = new SqlCommand(getRoomType, conn);

            cmdGetRoomType.Parameters.AddWithValue("@ID", roomTypeID);

            string roomType = (string)cmdGetRoomType.ExecuteScalar();

            conn.Close();

            return roomType;
        }

        private void setRoomType()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

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

            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String roomTypeID = ddlRoomType.SelectedValue;

            String updateStandardRoomPrice = "UPDATE StandardRoomPrice SET MondayPrice = @MonPrice, TuesdayPrice = @TuePrice," +  
                "WednesdayPrice = @WedPrice, ThursdayPrice = @ThuPrice, FridayPrice = @FriPrice, SaturdayPrice = @SatPrice, SundayPrice = @SunPrice " + 
                "WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdUpdateStandardRoomPrice = new SqlCommand(updateStandardRoomPrice, conn);

            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@MonPrice", Convert.ToDecimal(txtMonPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@TuePrice", Convert.ToDecimal(txtTuePrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@WedPrice", Convert.ToDecimal(txtWedPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@ThuPrice", Convert.ToDecimal(txtThuPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@FriPrice", Convert.ToDecimal(txtFriPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@SatPrice", Convert.ToDecimal(txtSatPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@SunPrice", Convert.ToDecimal(txtSunPrice.Text));
            cmdUpdateStandardRoomPrice.Parameters.AddWithValue("@ID", roomTypeID);

            int i = cmdUpdateStandardRoomPrice.ExecuteNonQuery();

            conn.Close();
            

            // Show success message by using popup
            lblPopupSavedContent.Text = "Price updated!" + "<br />";

            PopupCover.Visible = true;
            PopupSaved.Visible = true;


        }

        protected void ddlRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStandardRoomPrice();
        }

        private void getStandardRoomPrice()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String roomTypeID = ddlRoomType.SelectedValue;

            String getStandardRoomPrice = "SELECT * FROM StandardRoomPrice WHERE RoomTypeID LIKE @ID";

            SqlCommand cmdGetStandardRoomPrice = new SqlCommand(getStandardRoomPrice, conn);

            cmdGetStandardRoomPrice.Parameters.AddWithValue("@ID", roomTypeID);

            SqlDataReader sdr = cmdGetStandardRoomPrice.ExecuteReader();

            if (sdr.Read())
            {
                txtMonPrice.Text = Math.Round(decimal.Parse(sdr["MondayPrice"].ToString()), 2).ToString();
                txtTuePrice.Text = Math.Round(decimal.Parse(sdr["TuesdayPrice"].ToString()), 2).ToString();
                txtWedPrice.Text = Math.Round(decimal.Parse(sdr["WednesdayPrice"].ToString()), 2).ToString();
                txtThuPrice.Text = Math.Round(decimal.Parse(sdr["ThursdayPrice"].ToString()), 2).ToString();
                txtFriPrice.Text = Math.Round(decimal.Parse(sdr["FridayPrice"].ToString()), 2).ToString();
                txtSatPrice.Text = Math.Round(decimal.Parse(sdr["SaturdayPrice"].ToString()), 2).ToString();
                txtSunPrice.Text = Math.Round(decimal.Parse(sdr["SundayPrice"].ToString()), 2).ToString();
            }

            conn.Close();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRegularPrice.aspx");
        }
    }
}