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
    public partial class EditFloor : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String floorID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private String floorNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Edit Floor";

            floorID = Request.QueryString["ID"];

            floorID = en.decryption(floorID);

            if (!IsPostBack)
            {
                // Save link for previous page
                ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
                PopupCover.Visible = false;
                setData();
            }
        }

        private void setData()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFloor = "SELECT * FROM Floor WHERE FloorID LIKE @ID";

            SqlCommand cmdGetFloor = new SqlCommand(getFloor, conn);

            cmdGetFloor.Parameters.AddWithValue("@ID", floorID);

            SqlDataReader sdr = cmdGetFloor.ExecuteReader();

            if (sdr.Read())
            {
                txtFloorName.Text = sdr.GetString(sdr.GetOrdinal("FloorName"));
                floorNumber = sdr.GetValue(2).ToString();
                setFloorNumber(floorNumber);
                ddlFloorNumber.SelectedValue = floorNumber;
                txtDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));
                String status = sdr.GetString(sdr.GetOrdinal("Status"));
                ddlStatus.SelectedValue = sdr.GetString(sdr.GetOrdinal("Status"));

                
            }

            conn.Close();
        }

        private List<int> getExistingFloorNumber()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command to get existing floor number from database
            String getFloorNumber = "SELECT FloorNumber FROM Floor";

            SqlCommand cmdGetFloorNumber = new SqlCommand(getFloorNumber, conn);

            SqlDataReader sdr = cmdGetFloorNumber.ExecuteReader();

            var floorNumber = new List<int> { };

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    floorNumber.Add(int.Parse(sdr[i].ToString()));
                }
            }

            conn.Close();

            return floorNumber;

        }

        private void setFloorNumber(String existingFloorNumber)
        {
            var floorNumber = new List<int> { };

            // Assign 20 floorNumber into list
            for (int i = 1; i <= 20; i++)
            {
                floorNumber.Add(i);
            }

            // Remove floor numbers that have added previously
            var fn = getExistingFloorNumber();

            if (fn.Count != 0)
            {
                for (int i = 0; i < fn.Count; i++)
                {
                    if(fn[i] == int.Parse(existingFloorNumber))
                    {
                        continue;
                    }

                    floorNumber.Remove(fn[i]);
                }
            }

            ddlFloorNumber.DataSource = floorNumber;
            ddlFloorNumber.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command to insert floor
            String addFloor = "UPDATE Floor SET FloorName = @FloorName, FloorNumber = @FloorNumber, Description = @Description, Status = @Status WHERE FloorID LIKE @FloorID";

            
            SqlCommand cmdAddFloor = new SqlCommand(addFloor, conn);

            cmdAddFloor.Parameters.AddWithValue("@FloorID", floorID);
            cmdAddFloor.Parameters.AddWithValue("@FloorName", txtFloorName.Text);
            cmdAddFloor.Parameters.AddWithValue("@FloorNumber", int.Parse(ddlFloorNumber.Text));
            cmdAddFloor.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmdAddFloor.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);

            int i = cmdAddFloor.ExecuteNonQuery();

            conn.Close();

            if (i > 0)
            {
                Response.Redirect("ViewFloor.aspx?ID=" + en.encryption(floorID));
            }
        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCancel.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnPopupConfirm_Click(object sender, EventArgs e)
        {

            // Reset all value
            txtFloorName.Text = "";
            ddlFloorNumber.SelectedIndex = 0;
            txtDescription.Text = "";
            ddlStatus.SelectedIndex = 0;

            PopupCancel.Visible = false;
            PopupCover.Visible = false;
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Redirect to previous page
            Response.Redirect(ViewState["PreviousPage"].ToString());
        }
    }
}