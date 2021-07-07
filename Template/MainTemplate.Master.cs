using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Template
{
    public partial class MainTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LBNavFrontDesk_Click(object sender, EventArgs e)
        {
            // Open and close nav bar option
            if (pnNavFrontDesk.Visible == true)
            {
                pnNavFrontDesk.Visible = false;
                lblNavFrontDesk.Style["font-weight"] = "0";
            }
            else
            {
                pnNavFrontDesk.Visible = true;
                lblNavFrontDesk.Style["font-weight"] = "600";
            }
        }

        protected void LBNavHotelConfiguration_Click(object sender, EventArgs e)
        {
            // Open and close nav bar option
            if (pnNavHotelConfiguration.Visible == true)
            {
                pnNavHotelConfiguration.Visible = false;
                lblNavHotelConfiguration.Style["font-weight"] = "0";
            }
            else
            {
                pnNavHotelConfiguration.Visible = true;
                lblNavHotelConfiguration.Style["font-weight"] = "600";
            }
        }

        protected void LBNavReport_Click(object sender, EventArgs e)
        {
            // Open and close nav bar option
            if (pnNavReport.Visible == true)
            {
                pnNavReport.Visible = false;
                lblNavReport.Style["font-weight"] = "0";
            }
            else
            {
                pnNavReport.Visible = true;
                lblNavReport.Style["font-weight"] = "600";
            }
        }
    }
}