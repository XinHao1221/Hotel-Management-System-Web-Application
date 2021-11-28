/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

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

                pnNavHotelConfiguration.Visible = false;
                pnNavReport.Visible = false;
                PnNavHistory.Visible = false;

                lblNavFrontDesk.Style["font-weight"] = "600";

                lblNavHotelConfiguration.Style["font-weight"] = "0";
                lblNavReport.Style["font-weight"] = "0";
                lblNavHistory.Style["font-weight"] = "0";
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

                pnNavFrontDesk.Visible = false;
                pnNavReport.Visible = false;
                PnNavHistory.Visible = false;

                lblNavHotelConfiguration.Style["font-weight"] = "600";

                lblNavFrontDesk.Style["font-weight"] = "0";
                lblNavReport.Style["font-weight"] = "0";
                lblNavHistory.Style["font-weight"] = "0";
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

                pnNavFrontDesk.Visible = false;
                pnNavHotelConfiguration.Visible = false;
                PnNavHistory.Visible = false;

                lblNavReport.Style["font-weight"] = "600";

                lblNavHotelConfiguration.Style["font-weight"] = "0";
                lblNavFrontDesk.Style["font-weight"] = "0";
                lblNavHistory.Style["font-weight"] = "0";
            }
        }

        protected void LBNavHistory_Click(object sender, EventArgs e)
        {
            // Open and close nav bar option
            if (PnNavHistory.Visible == true)
            {
                PnNavHistory.Visible = false;
                lblNavHistory.Style["font-weight"] = "0";
            }
            else
            {
                PnNavHistory.Visible = true;

                pnNavFrontDesk.Visible = false;
                pnNavReport.Visible = false;
                pnNavHotelConfiguration.Visible = false;

                lblNavHistory.Style["font-weight"] = "600";

                lblNavFrontDesk.Style["font-weight"] = "0";
                lblNavReport.Style["font-weight"] = "0";
                lblNavHotelConfiguration.Style["font-weight"] = "0";
            }
        }
    }
}