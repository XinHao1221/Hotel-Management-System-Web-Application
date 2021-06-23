using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Hotel_Management_System.Hotel_Configuration_Management.Room_Type
{
    public partial class AddEquipment : System.Web.UI.UserControl
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    Session["EquipmentList"] = new List<Equipment>();
                }
            }

            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];

            if (equipmentList.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void btnSaveEquipment_Click(object sender, EventArgs e)
        {
            List<Equipment> equipmentList = (List<Equipment>)Session["EquipmentList"];
            equipmentList.Add(new Equipment() { equipmentName = txtEquipment.Text, fineCharges = txtEquipmentPrice.Text });

            Repeater1.DataSource = equipmentList;
            Repeater1.DataBind();

            lblNoItemFound.Visible = false;
        }
    }
}