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
    public partial class EditSpecialPrice : System.Web.UI.Page
    {
        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RoomTpye
        RoomType roomType = new RoomType();

        private string date;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                date = Request.QueryString["Date"];

                if (date != null)
                {
                    // Check if the request is navigate from PreviewRoomType.aspx

                    date = en.decryption(Request.QueryString["Date"]);

                    //string roomType = getRoomType();

                    //if (roomType != "")
                    //{
                    //    ddlRoomType.Items.FindByText(roomType).Selected = true;
                    //    getStandardRoomPrice();
                    //}
                    txtDate.Text = date;
                    getSpecialRoomPrice();
                }

            }

            PopupCover.Visible = false;
            PopupSaved.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ViewState["ExistingSpecialPrice"].ToString() == "false")
            {
                savePrice(Repeater1);
            }
            else
            {
                updatePrice();

                // Check if any new room type that doesn't have special price
                if (ViewState["Repeater2"].ToString() == "true")
                {
                    savePrice(Repeater2);
                }
            }

            showPopup();
        }

        private void updatePrice()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // *** Get price for each room type from repeater ***
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    String updatePrice = "UPDATE SpecialRoomPrice SET Title = @Title, Price = @Price WHERE RoomTypeID LIKE @RoomTypeID AND Date LIKE @Date";

                    SqlCommand cmdUpdatePrice = new SqlCommand(updatePrice, conn);

                    // Get references of the textbox and label
                    var lblRoonType = item.FindControl("lblRoomType") as Label;
                    var lblRoonTypeID = item.FindControl("lblRoonTypeID") as Label;
                    var txtPrice = item.FindControl("txtPrice") as TextBox;

                    String roomTypeID = lblRoonTypeID.Text;
                    String roomType = lblRoonType.Text;
                    String price = txtPrice.Text;

                    cmdUpdatePrice.Parameters.AddWithValue("@Title", txtEventName.Text);
                    cmdUpdatePrice.Parameters.AddWithValue("@Price", Convert.ToDouble(price));
                    cmdUpdatePrice.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
                    cmdUpdatePrice.Parameters.AddWithValue("@Date", txtDate.Text);

                    int i = cmdUpdatePrice.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        private void savePrice(Repeater repeater)
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // *** Get price for each room type from repeater ***
            foreach (RepeaterItem item in repeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    String nextSpecialPriceID = idGenerator.getNextID("SpecialPriceID", "SpecialRoomPrice", "SP");

                    String addSpecialPrice = "INSERT INTO SpecialRoomPrice VALUES (@ID, @Title, @Price, @Date, @RoomTypeID)";

                    SqlCommand cmdAddSpecialRoomPrice = new SqlCommand(addSpecialPrice, conn);

                    // Get references of the textbox and label
                    var lblRoonType = item.FindControl("lblRoomType") as Label;
                    var lblRoonTypeID = item.FindControl("lblRoonTypeID") as Label;
                    var txtPrice = item.FindControl("txtPrice") as TextBox;

                    String roomTypeID = lblRoonTypeID.Text;
                    String roomType = lblRoonType.Text;
                    String price = txtPrice.Text;

                    cmdAddSpecialRoomPrice.Parameters.AddWithValue("@ID", nextSpecialPriceID);
                    cmdAddSpecialRoomPrice.Parameters.AddWithValue("@Title", txtEventName.Text);
                    cmdAddSpecialRoomPrice.Parameters.AddWithValue("@Price", Convert.ToDouble(price));
                    cmdAddSpecialRoomPrice.Parameters.AddWithValue("@Date", txtDate.Text);
                    cmdAddSpecialRoomPrice.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

                    int i = cmdAddSpecialRoomPrice.ExecuteNonQuery();

                }
            }

            conn.Close();
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            getSpecialRoomPrice();
        }

        private void getSpecialRoomPrice()
        {
            // SELECT R.Title, S.MondayPrice FROM RoomType R, StandardRoomPrice S WHERE Status IN ('Active') AND R.RoomTypeID LIKE S.RoomTypeID

            // Get day from specific date
            DateTime dateTime = Convert.ToDateTime(txtDate.Text);
            String selectedDay = dateTime.ToString("dddd");

            if (specialPriceExist())
            {
                ViewState["ExistingSpecialPrice"] = "true";
                ViewState["Repeater2"] = "false";

                getExistingSpecialPrice();
                var roomTypeID = new List<string> { };
                roomTypeID = getOtherRoomType();

                // If having any room that doesn't have special room price
                if (roomTypeID.Count > 0)
                {
                    ViewState["Repeater2"] = "true";
                    setRepeater2(roomTypeID, selectedDay);
                }

            }
            else
            {
                ViewState["ExistingSpecialPrice"] = "false";
                ViewState["Repeater2"] = "false";

                getStandardRoomPrice(selectedDay);
                txtEventName.Text = "";
            }
        }

        private void setRepeater2(List<string> roomTypeID, string selectedDay)
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            DataTable dtMerged = new DataTable();

            for (int i = 0; i < roomTypeID.Count; i++)
            {
                string getRoomPrice = "SELECT (R.RoomTypeID) AS RoomTypeID, Title, (" + selectedDay + "Price) AS Price FROM RoomType R, StandardRoomPrice S WHERE Status IN ('Active') AND R.RoomTypeID LIKE @ID AND R.RoomTypeID LIKE S.RoomTypeID";

                SqlCommand cmdGetRoomPrice = new SqlCommand(getRoomPrice, conn);

                cmdGetRoomPrice.Parameters.AddWithValue("@ID", roomTypeID[i]);

                SqlDataAdapter sda = new SqlDataAdapter(cmdGetRoomPrice);

                DataTable dt = new DataTable();

                sda.Fill(dt);

                // Merge data into one table
                dtMerged.Merge(dt);

            }

            // Set the merged data table into repeater2
            Repeater2.DataSource = dtMerged;
            Repeater2.DataBind();

            conn.Close();
        }



        private void getExistingSpecialPrice()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getExistingSpecialPrice = "SELECT (R.RoomTypeID) AS RoomTypeID, (R.Title) AS Title, (S.Title) AS EventName, (S.Price) AS Price, (S.Title) AS EventName FROM RoomType R, SpecialRoomPrice S WHERE R.Status IN ('Active') AND R.RoomTypeID LIKE S.RoomTypeID AND S.Date LIKE @Date";

            SqlCommand cmdGetExistingSpeicalPrice = new SqlCommand(getExistingSpecialPrice, conn);

            cmdGetExistingSpeicalPrice.Parameters.AddWithValue("Date", txtDate.Text);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetExistingSpeicalPrice);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            // Set data to repeater
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            // Set data to txtEvetName
            DataRow row = dt.Rows[0];
            txtEventName.Text = row["EventName"].ToString();

            conn.Close();

        }

        // To get room type which doesn't have any 
        private List<string> getOtherRoomType()
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getRoomTypeID = "SELECT (R.RoomTypeID) AS RoomTypeID FROM RoomType R WHERE NOT EXISTS (SELECT RoomTypeID FROM SpecialRoomPrice S WHERE R.RoomTypeID LIKE S.RoomTypeID AND S.Date LIKE @Date) AND R.Status LIKE ('Active');";

            SqlCommand cmdGetRoomTypeID = new SqlCommand(getRoomTypeID, conn);

            cmdGetRoomTypeID.Parameters.AddWithValue("@Date", txtDate.Text);

            SqlDataReader sdr = cmdGetRoomTypeID.ExecuteReader();

            var roomTypeID = new List<string> { };

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    roomTypeID.Add(sdr[i].ToString());
                }
            }

            conn.Close();
            return roomTypeID;
        }

        private void getStandardRoomPrice(String selectedDay)
        {
            // open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getStandardPrice = "SELECT (R.RoomTypeID) AS RoomTypeID, Title, (" + selectedDay + "Price)" + " AS Price FROM RoomType R, StandardRoomPrice S WHERE Status IN ('Active') AND R.RoomTypeID LIKE S.RoomTypeID";

            SqlCommand cmdGetStandardPrice = new SqlCommand(getStandardPrice, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmdGetStandardPrice);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            // Clear data source for repeater 2
            DataTable dtEmpty = new DataTable();

            Repeater2.DataSource = dtEmpty;
            Repeater2.DataBind();

            conn.Close();
        }

        private bool specialPriceExist()
        {
            // Check if the special room price for the selected data already exist 
            conn = new SqlConnection(strCon);
            conn.Open();

            String checkSpecialPriceExist = "SELECT * FROM SpecialRoomPrice WHERE Date LIKE @Date";

            SqlCommand cmdCheckSpecialRoomPriceExist = new SqlCommand(checkSpecialPriceExist, conn);

            cmdCheckSpecialRoomPriceExist.Parameters.AddWithValue("@Date", txtDate.Text);

            SqlDataAdapter sda = new SqlDataAdapter(cmdCheckSpecialRoomPriceExist);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        private void showPopup()
        {
            // Show success message by using popup
            lblPopupSavedContent.Text = "Price saved!" + "<br />";

            PopupCover.Visible = true;
            PopupSaved.Visible = true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditSpecialPrice.aspx");
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Check if user have enter any value
            if (txtEventName.Text == "")
            {
                Response.Redirect("SpecialRoomPrice.aspx");
            }

            // If no show popup message
            PopupBack.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnConfirmBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpecialRoomPrice.aspx");
            PopupCover.Visible = false;
            PopupBack.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupBack.Visible = false;
            PopupCover.Visible = false;
        }
    }
}