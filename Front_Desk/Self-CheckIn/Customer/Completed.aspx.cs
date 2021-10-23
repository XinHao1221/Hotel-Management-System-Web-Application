using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Front_Desk.Self_CheckIn.Customer
{
    public partial class Completed : System.Web.UI.Page
    {
        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            int temp = RandomNumber(100000, 999999);
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(100000, 999999);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RentFacility.aspx");
        }
    }
}