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
            // Page TItle
            Page.Title = "Revenue Report";

            if (!IsPostBack)
            {
                setDDLYear();

            }

            PNReportDetails.Visible = false;
        }

        private void setDDLYear()
        {
            // Get the oldest payment record from the database
            // To know the oldest year for the reservation report

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
            // If there is any data being selected
            if(txtDate.Text != "")
            {
                // Display total profit for each category
                totalReservationProfit = getReservationProfit(txtDate.Text);
                totalFaclityProfit = getFacilityProfit(txtDate.Text);
                totalFineCharges = getTotalFineCharges(txtDate.Text);
                totalServicesCharges = getTotalServicesCharges(txtDate.Text);

                displayRevenueDetails();

                displayChartProfit();

                // Display revenue details for each category
                displayRoomProfitDetails(txtDate.Text);
                displayFacilityProfitDetails(txtDate.Text);
                displayFineChargesDetails(txtDate.Text);
                displayServicesDetails(txtDate.Text);
            }
            else
            {
                PNReportDetails.Visible = false;

                // Reset all price to zero
                lblRoomTotal.Text = "0.00";
                lblFacilityTotal.Text = "0.00";
                lblFineChargesTotal.Text = "0.00";
                lblServicesTotal.Text = "0.00";
                lblProfit.Text = "0.00";
            }
            

        }

        protected void txtYearMonth_TextChanged(object sender, EventArgs e)
        {
            if(txtYearMonth.Text != "")
            {
                // Display total profit for each category
                totalReservationProfit = getReservationProfit(txtYearMonth.Text);
                totalFaclityProfit = getFacilityProfit(txtYearMonth.Text);
                totalFineCharges = getTotalFineCharges(txtYearMonth.Text);
                totalServicesCharges = getTotalServicesCharges(txtYearMonth.Text);

                displayRevenueDetails();

                displayChartProfit();

                // Display revenue details for each category
                displayRoomProfitDetails(txtYearMonth.Text);
                displayFacilityProfitDetails(txtYearMonth.Text);
                displayFineChargesDetails(txtYearMonth.Text);
                displayServicesDetails(txtYearMonth.Text);
            }
            else
            {
                PNReportDetails.Visible = false;

                // Reset all price to zero
                lblRoomTotal.Text = "0.00";
                lblFacilityTotal.Text = "0.00";
                lblFineChargesTotal.Text = "0.00";
                lblServicesTotal.Text = "0.00";
                lblProfit.Text = "0.00";
            }
            
        }

        protected void ddlYear_TextChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex != 0)
            {
                // Display total profit for each category
                totalReservationProfit = getReservationProfit(ddlYear.SelectedValue);
                totalFaclityProfit = getFacilityProfit(ddlYear.SelectedValue);
                totalFineCharges = getTotalFineCharges(ddlYear.SelectedValue);
                totalServicesCharges = getTotalServicesCharges(ddlYear.SelectedValue);

                displayRevenueDetails();

                displayChartProfit();

                // Display revenue details for each category
                displayRoomProfitDetails(ddlYear.SelectedValue);
                displayFacilityProfitDetails(ddlYear.SelectedValue);
                displayFineChargesDetails(ddlYear.SelectedValue);
                displayServicesDetails(ddlYear.SelectedValue);
            }
            else
            {
                PNReportDetails.Visible = false;

                lblRoomTotal.Text = "0.00";
                lblFacilityTotal.Text = "0.00";
                lblFineChargesTotal.Text = "0.00";
                lblServicesTotal.Text = "0.00";
                lblProfit.Text = "0.00";
            }
                
        }

        protected void ddlReportType_TextChanged(object sender, EventArgs e)
        {
            PNReportDetails.Visible = false;
            lblNoDetailsFound.Visible = false;

            // Reset all value to zero
            lblRoomTotal.Text = "0.00";
            lblFacilityTotal.Text = "0.00";
            lblFineChargesTotal.Text = "0.00";
            lblServicesTotal.Text = "0.00";
            lblProfit.Text = "0.00";

            // Check which report type is being selected
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

            // Calculate total profit for the room renting
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
                                        "WHERE DateRented LIKE '" + date + "%'";

            SqlCommand cmdGetFacilityProfit = new SqlCommand(getFacilityProfit, conn);

            SqlDataReader sdr = cmdGetFacilityProfit.ExecuteReader();

            // Calculate total profit for the facility renting
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

            // Calculate total profit for the fine charges
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

            // Calculate total profit for the service charges
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
            PNReportDetails.Visible = false;
            lblNoDetailsFound.Visible = false;

            if (totalReservationProfit > 0)
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
                PNReportDetails.Visible = true;
            }
            else
            {
                lblProfit.Text = "0.00";
                lblNoDetailsFound.Visible = true;
            }
           
        }

        private void displayChartProfit()
        {

            String[] x = { "Room", "Facility", "Fine", "Services" };
            double[] y = { totalReservationProfit, totalFaclityProfit, totalFineCharges, totalServicesCharges };

            // Set data into pie chart
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

        private void displayRoomProfitDetails(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomProfitDetails = "SELECT Date, RoomTypeID, RoomPrice, ExtraBedCharges " +
                                            "FROM ReservationRoom " +
                                            "WHERE Date LIKE '" + date + "%'";

            SqlCommand cmdGetRoomProfitDetails = new SqlCommand(getRoomProfitDetails, conn);

            SqlDataReader sdr = cmdGetRoomProfitDetails.ExecuteReader();

            // Set date to repeater
            if (sdr.HasRows)
            {
                // Set date to repeater
                RepeaterRoom.DataSource = sdr;
                RepeaterRoom.DataBind();

                lblNoRoomFound.Visible = false;
            }
            else
            {
                // Set repeater as null
                RepeaterRoom.DataSource = null;
                RepeaterRoom.DataBind();

                lblNoRoomFound.Visible = true;
            }

            conn.Close();
        }

        protected void RepeaterRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblRoomTypeID = e.Item.FindControl("lblRoomTypeID") as Label;
            Label lblRoomTypeName = e.Item.FindControl("lblRoomTypeName") as Label;
            Label lblRoomPrice = e.Item.FindControl("lblRoomPrice") as Label;
            Label lblExtraBedPrice = e.Item.FindControl("lblExtraBedPrice") as Label;
            Label lblSubTotal = e.Item.FindControl("lblSubTotal") as Label;
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Get Room Type Name
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomTypeName = "SELECT Title FROM RoomType WHERE RoomTypeID LIKE @RoomTypeID";

            SqlCommand cmdGetRoomTypeName = new SqlCommand(getRoomTypeName, conn);

            cmdGetRoomTypeName.Parameters.AddWithValue("@RoomTypeID", lblRoomTypeID.Text);

            lblRoomTypeName.Text = (string)cmdGetRoomTypeName.ExecuteScalar();

            conn.Close();


            // Calculate subtotal
            double subTotal = Convert.ToDouble(lblRoomPrice.Text) + Convert.ToDouble(lblExtraBedPrice.Text);

            // Display subtotal
            lblSubTotal.Text = string.Format("{0:0.00}", subTotal);


            // Format date
            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();

        }

        private void displayFacilityProfitDetails(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFacilityProfitDetails = "SELECT FacilityID, DateRented, Price FROM ReservationFacility " +
                                                "WHERE DateRented LIKE '" + date + "%'";

            SqlCommand cmdGetFacilityProfitDetails = new SqlCommand(getFacilityProfitDetails, conn);

            SqlDataReader sdr = cmdGetFacilityProfitDetails.ExecuteReader();

            // Check if any record being retrieved
            if (sdr.HasRows)
            {
                RepeaterFacility.DataSource = sdr;
                RepeaterFacility.DataBind();

                lblNoFacilityFound.Visible = false;
            }
            else
            {
                RepeaterFacility.DataSource = null;
                RepeaterFacility.DataBind();

                lblNoFacilityFound.Visible = true;
            }
            

            conn.Close();
        }

        protected void RepeaterFacility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblFacilityID = e.Item.FindControl("lblFacilityID") as Label;
            Label lblDateRented = e.Item.FindControl("lblDateRented") as Label;
            Label lblFacilityName = e.Item.FindControl("lblFacilityName") as Label;

            // Get Facility Name
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFacilityName = "SELECT FacilityName FROM Facility WHERE FacilityID LIKE @FacilityID";

            SqlCommand cmdGetFacilityName = new SqlCommand(getFacilityName, conn);

            cmdGetFacilityName.Parameters.AddWithValue("@FacilityID", lblFacilityID.Text);

            lblFacilityName.Text = (string)cmdGetFacilityName.ExecuteScalar();

            conn.Close();

            // Format date
            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDateRented.Text);

            // Display the formated date
            lblDateRented.Text = formatedDate.ToShortDateString();
        }

        private void displayFineChargesDetails(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFineCharges = "SELECT Title, DateCreated, Amount FROM OtherCharges " +
                                    "WHERE Category LIKE 'Fine' AND DateCreated LIKE '" + date + "%'";

            SqlCommand cmdGetFineCharges = new SqlCommand(getFineCharges, conn);

            SqlDataReader sdr = cmdGetFineCharges.ExecuteReader();

            if (sdr.HasRows)
            {
                RepeaterFine.DataSource = sdr;
                RepeaterFine.DataBind();

                lblNoFineCharges.Visible = false;
            }
            else
            {
                RepeaterFine.DataSource = null;
                RepeaterFine.DataBind();

                lblNoFineCharges.Visible = true;
            }

            conn.Close();
        }

        protected void RepeaterFine_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format date
            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();
        }

        private void displayServicesDetails(string date)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getServicesDetails = "SELECT Title, Amount, DateCreated FROM OtherCharges " +
                                        "WHERE Category LIKE 'Service' AND DateCreated LIKE '" + date + "%'";

            SqlCommand cmdGetServicesDetails = new SqlCommand(getServicesDetails, conn);

            SqlDataReader sdr = cmdGetServicesDetails.ExecuteReader();

            if (sdr.HasRows)
            {
                RepeaterServices.DataSource = sdr;
                RepeaterServices.DataBind();

                lblNoServicesCharges.Visible = false;
            }
            else
            {
                RepeaterServices.DataSource = null;
                RepeaterServices.DataBind();

                lblNoServicesCharges.Visible = true;
            }

        }

        protected void RepeaterServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblDate = e.Item.FindControl("lblDate") as Label;

            // Format date
            // Format date base on date format on user's computer
            DateTime formatedDate = Convert.ToDateTime(lblDate.Text);

            // Display the formated date
            lblDate.Text = formatedDate.ToShortDateString();
        }


    }
}