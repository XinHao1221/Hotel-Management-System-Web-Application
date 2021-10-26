using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class SelfCheckIn_Error_ : System.Web.UI.Page
    {
        string checkInDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Page TItle
            Page.Title = "Error";

            // Display check in date 
            checkInDate = Request.QueryString["Date"];

            lblCheckInDate.Text = checkInDate;
        }
    }
}