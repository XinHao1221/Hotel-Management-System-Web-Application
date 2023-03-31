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

namespace Hotel_Management_System.Reporting.Reservation_Report
{
    public partial class ReservationReport : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Used for date
        DateUtility dateUtility = new DateUtility();

        private List<ReservedRoomType> reservedRoomTypes = new List<ReservedRoomType>();

        private List<ReservedFacility> reservedFacilities = new List<ReservedFacility>();
        protected void Page_Load(object sender, EventArgs e)
        {
            // **** Control access
            if (Session["UserRole"].ToString() != "Manager")
            {
                Response.Redirect("../../Error/PermissionError.aspx");
            }

            // Page TItle
            Page.Title = "Reservation Report";

            getRoomType();
            getFacility();

            if (!IsPostBack)
            {
                setDDLYear();
            }

            PNReservationReportDetails.Visible = false;

            lblNoFacilityFound.Visible = false;
            lblNoRoomTypeFound.Visible = false;
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

            // Set the possible year of report into drop down list
            if (currentYear >= year)
            {
                for (int i = year; i <= currentYear; i++)
                {

                    ddlYear.Items.Insert((i - year + 1), new ListItem(i.ToString(), i.ToString()));
                }
            }

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            // If there is any date has been selected
            if(txtDate.Text != "")
            {
                PNReservationReportDetails.Visible = true;

                getReservedRoomTypeQuantity(txtDate.Text);
                // Sort list from small to large
                sortRoomType();
                displayChartRoomType();

                // Display reserved room type details
                RepeaterRentedRoomType.DataSource = reservedRoomTypes;
                RepeaterRentedRoomType.DataBind();

                // Display facility renting details with Histogram
                getReservedFacilityQty(txtDate.Text);
                sortFacility();
                displayChartFacility();

                // Display reserved facility details in the table
                RepeaterFacility.DataSource = reservedFacilities;
                RepeaterFacility.DataBind();

                // Check if there is no any record to display
                checkIfEmpty();
            }
            else
            {
                PNReservationReportDetails.Visible = false;
            }
            

        }

        protected void txtYearMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtYearMonth.Text != "")
            {
                PNReservationReportDetails.Visible = true;

                getReservedRoomTypeQuantity(txtYearMonth.Text);

                // Sort list from small to large
                sortRoomType();
                displayChartRoomType();

                // Display reserved room type details
                RepeaterRentedRoomType.DataSource = reservedRoomTypes;
                RepeaterRentedRoomType.DataBind();

                // Display facility renting details with Histogram
                getReservedFacilityQty(txtYearMonth.Text);
                sortFacility();
                displayChartFacility();

                // Display reserved facility details in the table
                RepeaterFacility.DataSource = reservedFacilities;
                RepeaterFacility.DataBind();

                // Check if there is no any record to display
                checkIfEmpty();
            }
            else
            {
                PNReservationReportDetails.Visible = false;
            }
            
        }

        protected void ddlYear_TextChanged(object sender, EventArgs e)
        {
            if(ddlYear.SelectedIndex != 0)
            {
                PNReservationReportDetails.Visible = true;

                // Sort list from small to large
                getReservedRoomTypeQuantity(ddlYear.SelectedValue);
                sortRoomType();
                displayChartRoomType();

                // Display reserved room type details
                RepeaterRentedRoomType.DataSource = reservedRoomTypes;
                RepeaterRentedRoomType.DataBind();

                // Display facility renting details with Histogram
                getReservedFacilityQty(ddlYear.SelectedValue);
                sortFacility();
                displayChartFacility();

                // Display reserved facility details in the table
                RepeaterFacility.DataSource = reservedFacilities;
                RepeaterFacility.DataBind();

                checkIfEmpty();
            }

            

        }

        protected void ddlReportType_TextChanged(object sender, EventArgs e)
        {
            PNReservationReportDetails.Visible = false;

            if (ddlReportType.SelectedValue == "Daily")
            {
                PNDateFilter.Visible = true;
                PNMonthFilter.Visible = false;
                PNYearFilter.Visible = false;

                // Reset filter
                txtDate.Text = "";
                txtYearMonth.Text = "";
                ddlYear.SelectedIndex = 0;

            }
            else if (ddlReportType.SelectedValue == "Monthly")
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

        private void getReservedRoomTypeQuantity(string date)
        {
            // Get the total quality rented for all room type
            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                string getReservedQty = "SELECT COUNT(*) FROM ReservationRoom WHERE RoomTypeID LIKE @RoomTypeID AND Date LIKE '" + date + "%'";

                SqlCommand cmdGetReservedQty = new SqlCommand(getReservedQty, conn);

                cmdGetReservedQty.Parameters.AddWithValue("@RoomTypeID", reservedRoomTypes[i].roomTypeID);

                reservedRoomTypes[i].quantity = (int)cmdGetReservedQty.ExecuteScalar();

                conn.Close();
            }
        }

        private void displayChartRoomType()
        {
            List<String> x = new List<string>();
            List<int> y = new List<int>();

            // Set data for x & y axis
            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if(reservedRoomTypes[i].quantity > 0)
                {
                    x.Add(reservedRoomTypes[i].roomTypeName);
                    y.Add(reservedRoomTypes[i].quantity);
                }
            }

            //String[] x = { "Room", "Facility", "Fine", "Services" };
            //double[] y = { 1, 2, 3, 4 };

            // Set data in to histogram
            ChartRoomType.Series[0].Points.DataBindXY(x, y);

            ChartRoomType.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;

            ChartRoomType.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ChartRoomType.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            ChartRoomType.ChartAreas["ChartArea1"].AxisX.Title = "Room Types";
            ChartRoomType.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";

            ChartRoomType.Legends[0].Enabled = false;

            // Set chart's tooltip
            foreach (Series s in ChartRoomType.Series)
            {
                s.Label = "#VALX   #PERCENT";
                s["PieLabelStyle"] = "Outside";
                s.ToolTip = "Room Type: #VALX\n\nPercentage: #PERCENT\n\nQuantity: #VALY";
            }
        }

        private void getRoomType()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getRoomTypes = "SELECT DISTINCT(RoomTypeID), Title FROM RoomType";

            SqlCommand cmdGetRoomTypes = new SqlCommand(getRoomTypes, conn);

            SqlDataReader sdr = cmdGetRoomTypes.ExecuteReader();

            while (sdr.Read())
            {
                reservedRoomTypes.Add(new ReservedRoomType(sdr["RoomTypeID"].ToString(), sdr["Title"].ToString()));
            }

            conn.Close();
        }

        private void sortRoomType()
        {
            reservedRoomTypes.Sort((x, y) => y.quantity.CompareTo(x.quantity));
        }

        private void sortFacility()
        {
            reservedFacilities.Sort((x, y) => y.quantity.CompareTo(x.quantity));
        }

        private void getFacility()
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFacility = "SELECT FacilityID, FacilityName FROM Facility";

            SqlCommand cmdGetFacility = new SqlCommand(getFacility, conn);

            SqlDataReader sdr = cmdGetFacility.ExecuteReader();

            while (sdr.Read())
            {
                reservedFacilities.Add(new ReservedFacility(sdr["FacilityID"].ToString(), sdr["FacilityName"].ToString()));
            }

            conn.Close();
        }

        private void getReservedFacilityQty(string date)
        {
            // Get total rented quantity for all facility
            for(int i = 0; i < reservedFacilities.Count; i++)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                string getReservedFacilityQty = "SELECT COUNT(*) FROM ReservationFacility " +
                                                "WHERE FacilityID LIKE @FacilityID AND DateRented LIKE '" + date + "%'";

                SqlCommand cmdGetReservedFacility = new SqlCommand(getReservedFacilityQty, conn);

                cmdGetReservedFacility.Parameters.AddWithValue("@FacilityID", reservedFacilities[i].facilityID);

                reservedFacilities[i].quantity = (int)cmdGetReservedFacility.ExecuteScalar();

                conn.Close();
            }
        }

        private void displayChartFacility()
        {
            List<String> x = new List<string>();
            List<int> y = new List<int>();

            // Set data for x & y axis
            for (int i = 0; i < reservedFacilities.Count; i++)
            {
                if (reservedFacilities[i].quantity > 0)
                {
                    x.Add(reservedFacilities[i].facilityName);
                    y.Add(reservedFacilities[i].quantity);
                }
            }

            // Set the data to be displayed on the histogram
            ChartFacility.Series[0].Points.DataBindXY(x, y);

            ChartFacility.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;

            ChartFacility.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ChartFacility.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            ChartFacility.ChartAreas["ChartArea1"].AxisX.Title = "Facility";
            ChartFacility.ChartAreas["ChartArea1"].AxisY.Title = "Quantity";

            ChartFacility.Legends[0].Enabled = false;

            // Set chart's tooltip
            foreach (Series s in ChartFacility.Series)
            {
                s.Label = "#VALX   #PERCENT";
                s["PieLabelStyle"] = "Outside";
                s.ToolTip = "Facility: #VALX\n\nPercentage: #PERCENT\n\nQuantity: #VALY";
            }
        }

        private void checkIfEmpty()
        {
            lblNoRoomTypeFound.Visible = true;

            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                if (reservedRoomTypes[i].quantity > 0){

                    lblNoRoomTypeFound.Visible = false;

                }
            }

            lblNoFacilityFound.Visible = true;

            for (int i = 0; i < reservedFacilities.Count; i++)
            {
                if (reservedFacilities[i].quantity > 0)
                {

                    lblNoFacilityFound.Visible = false;

                }
            }
        }
    }
}