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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Hotel_Management_System.Reporting.Revenue_Report
{
    public partial class RevenueReport : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Used for date
        DateUtility dateUtility = new DateUtility();

        private double totalReservationProfit = 0;
        private double totalFaclityProfit = 0;
        private double totalFineCharges = 0;
        private double totalServicesCharges = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setDDLYear();

            }
        }

        private void setDDLYear()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFirstPaymentDate = "SELECT TOP 1 (Date) FROM Payment";

            SqlCommand cmdGetFirstPaymentDate = new SqlCommand(getFirstPaymentDate, conn);

            string lastPaymentDate = "";

            try
            {
                lastPaymentDate = (string)cmdGetFirstPaymentDate.ExecuteScalar();
            }
            catch
            {

            }

            conn.Close();

            // Set item to dropdownlist
            int year = int.Parse(lastPaymentDate.Substring(0, 4));
            int currentYear = int.Parse(dateUtility.getYear());

            // Set first item
            ddlYear.Items.Insert(0, new ListItem("--- Please Select ---", ""));

            if (currentYear >= year)
            {
                for(int i = year; i <= currentYear; i++)
                {
                    
                    ddlYear.Items.Insert((i - year + 1), new ListItem(i.ToString(), i.ToString()));
                }
            }
            
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            totalReservationProfit = getReservationProfit(txtDate.Text);
            totalFaclityProfit = getFacilityProfit(txtDate.Text);
            totalFineCharges = getTotalFineCharges(txtDate.Text);
            totalServicesCharges = getTotalServicesCharges(txtDate.Text);

            displayRevenueDetails();

            displayChartProfit();
        }

        protected void txtYearMonth_TextChanged(object sender, EventArgs e)
        {
            totalReservationProfit = getReservationProfit(txtYearMonth.Text);
            totalFaclityProfit = getFacilityProfit(txtYearMonth.Text);
            totalFineCharges = getTotalFineCharges(txtYearMonth.Text);
            totalServicesCharges = getTotalServicesCharges(txtYearMonth.Text);

            displayRevenueDetails();

            displayChartProfit();
        }

        protected void ddlYear_TextChanged(object sender, EventArgs e)
        {
            totalReservationProfit = getReservationProfit(ddlYear.SelectedValue);
            totalFaclityProfit = getFacilityProfit(ddlYear.SelectedValue);
            totalFineCharges = getTotalFineCharges(ddlYear.SelectedValue);
            totalServicesCharges = getTotalServicesCharges(ddlYear.SelectedValue);

            displayRevenueDetails();

            displayChartProfit();
        }

        protected void ddlReportType_TextChanged(object sender, EventArgs e)
        {
            lblRoomTotal.Text = "0.00";
            lblFacilityTotal.Text = "0.00";
            lblFineChargesTotal.Text = "0.00";
            lblServicesTotal.Text = "0.00";
            lblProfit.Text = "0.00";

            if(ddlReportType.SelectedValue == "Daily")
            {
                PNDateFilter.Visible = true;
                PNMonthFilter.Visible = false;
                PNYearFilter.Visible = false;

                // Reset filter
                txtDate.Text = "";
                txtYearMonth.Text = "";
                ddlYear.SelectedIndex = 0;

            }
            else if(ddlReportType.SelectedValue == "Monthly")
            {
                PNDateFilter.Visible = false;
                PNMonthFilter.Visible = true;
                PNYearFilter.Visible = false;

                // Reset filter
                txtDate.Text = "";
                txtYearMonth.Text = "";
                ddlYear.SelectedIndex = 0;
            }
            else
            {
                PNDateFilter.Visible = false;
                PNMonthFilter.Visible = false;
                PNYearFilter.Visible = true;

                // Reset filter
                txtDate.Text = "";
                txtYearMonth.Text = "";
                ddlYear.SelectedIndex = 0;
            }
        }

        private double getReservationProfit(string date)
        {
            double roomPrice = 0;
            double extraBedCharges = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getReservationProfit = "SELECT SUM(RoomPrice) AS RoomPrice, SUM(ExtraBedCharges) AS ExtraBedCharges " +
                                            "FROM ReservationRoom " +
                                            "WHERE Date LIKE '" + date + "%'";

            SqlCommand cmdGetReservationProfit = new SqlCommand(getReservationProfit, conn);

            SqlDataReader sdr = cmdGetReservationProfit.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    roomPrice = Convert.ToDouble(sdr["RoomPrice"]);
                    extraBedCharges = Convert.ToDouble(sdr["ExtraBedCharges"]);
                }
                catch
                {

                }
                
            }

            conn.Close();

            return roomPrice + extraBedCharges;
        }

        private double getFacilityProfit(string date)
        {
            double total = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getFacilityProfit = "SELECT SUM(Price) AS Price FROM ReservationFacility " +
                                        "WHERE DateCreated LIKE '" + date + "%'";

            SqlCommand cmdGetFacilityProfit = new SqlCommand(getFacilityProfit, conn);

            SqlDataReader sdr = cmdGetFacilityProfit.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    total = Convert.ToDouble(sdr["Price"]);
                }
                catch
                {

                }
                
            }

            conn.Close();

            return total;
        }

        private double getTotalFineCharges(string date)
        {
            double total = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalFineCharges = "SELECT SUM(Amount) AS Total FROM OtherCharges " +
                                        "WHERE Category LIKE 'Fine' AND DateCreated LIKE '" + date + "%'";

            SqlCommand cmdGetTotalFineCharges = new SqlCommand(getTotalFineCharges, conn);

            SqlDataReader sdr = cmdGetTotalFineCharges.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    total = Convert.ToDouble(sdr["Total"]);
                }
                catch
                {

                }
            }

            conn.Close();

            return total;
        }

        private double getTotalServicesCharges(string date)
        {
            double total = 0;

            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalServiceCharges = "SELECT SUM(Amount) AS Total FROM OtherCharges " +
                                            "WHERE Category LIKE 'Service' AND DateCreated LIKE '" + date + "%'";

            SqlCommand cmdGetTotalServiceCharges = new SqlCommand(getTotalServiceCharges, conn);

            SqlDataReader sdr = cmdGetTotalServiceCharges.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    total = Convert.ToDouble(sdr["Total"]);
                }
                catch
                {

                }
                
            }
            
            conn.Close();

            return total;
        }

        private void displayRevenueDetails()
        {
            if(totalReservationProfit > 0)
            {
                lblRoomTotal.Text = string.Format("{0:0.00}", totalReservationProfit);
            }
            else
            {
                lblRoomTotal.Text = "0.00";
            }
            
            if(totalFaclityProfit > 0)
            {
                lblFacilityTotal.Text = string.Format("{0:0.00}", totalFaclityProfit);
            }
            else
            {
                lblFacilityTotal.Text = "0.00";
            }
            
            if(totalFineCharges > 0)
            {
                lblFineChargesTotal.Text = string.Format("{0:0.00}", totalFineCharges);
            }
            else
            {
                lblFineChargesTotal.Text = "0.00";
            }

            if(totalServicesCharges > 0)
            {
                lblServicesTotal.Text = string.Format("{0:0.00}", totalServicesCharges);
            }
            else
            {
                lblServicesTotal.Text = "0.00";
            }

            double profit = totalReservationProfit + totalFaclityProfit + totalFineCharges + totalServicesCharges;

            if(profit > 0)
            {
                lblProfit.Text = string.Format("{0:0.00}", profit);
            }
            else
            {
                lblProfit.Text = "0.00";
            }
            

            
        }

        private void displayChartProfit()
        {

            String[] x = { "Room", "Facility", "Fine", "Services" };
            double[] y = { totalReservationProfit, totalFaclityProfit, totalFineCharges, totalServicesCharges };

            ChartProfit.Series[0].Points.DataBindXY(x, y);

            ChartProfit.Series[0].BorderWidth = 5;
            ChartProfit.Series[0].ChartType = SeriesChartType.Pie;

            ChartProfit.Legends[0].Enabled = true;

            // Set chart's tooltip
            foreach (Series s in ChartProfit.Series)
            {
                s.Label = "#VALX   #PERCENT";
                s["PieLabelStyle"] = "Outside";
                s.ToolTip = "Category: #VALX\n\nPercentage: #PERCENT\n\nTotal: RM #VALY.00";
            }
        }

        protected void ChartGuestInHouse_Customize(object sender, EventArgs e)
        {
            //hide label value if zero
            foreach (System.Web.UI.DataVisualization.Charting.Series series in ChartProfit.Series)
            {
                foreach (System.Web.UI.DataVisualization.Charting.DataPoint point in series.Points)
                {
                    if (point.YValues.Length > 0 && (double)point.YValues.GetValue(0) == 0)
                    {
                        point.LegendText = point.AxisLabel;//In case you have legend
                        point.AxisLabel = string.Empty;
                        point.Label = string.Empty;
                    }
                }
            }
        }
    }
}