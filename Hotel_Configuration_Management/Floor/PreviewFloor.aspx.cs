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

    public partial class ViewFloor : System.Web.UI.Page
    {
        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();
        private String floorID;

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            floorID = en.decryption(Request.QueryString["ID"]);

            setText();
        }

        private void setText()
        {

            conn = new SqlConnection(strCon);
            conn.Open();

            String getFloor = "SELECT * FROM Floor WHERE FloorID LIKE @ID";

            SqlCommand cmdGetFloor = new SqlCommand(getFloor, conn);

            cmdGetFloor.Parameters.AddWithValue("@ID", floorID);

            SqlDataReader sdr = cmdGetFloor.ExecuteReader();

            if (sdr.Read())
            {
               lblFloorName.Text = sdr.GetString(sdr.GetOrdinal("FloorName"));
               lblFloorNumber.Text = sdr.GetValue(2).ToString();
               lblDescription.Text = sdr.GetString(sdr.GetOrdinal("Description"));
               lblStatus.Text = sdr.GetString(sdr.GetOrdinal("Status"));

               if(lblStatus.Text == "Active")
               {
                    lblStatus.Style["color"] = "#00ce1b";
               }
               else
               { 
                   lblStatus.Style["color"] = "red";
               }
            }

            conn.Close();
        }
    }
}