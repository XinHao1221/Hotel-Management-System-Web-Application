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

        protected void Page_Load(object sender, EventArgs e)
        {
            
            getRoomType();

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
                for (int i = year; i <= currentYear; i++)
                {

                    ddlYear.Items.Insert((i - year + 1), new ListItem(i.ToString(), i.ToString()));
                }
            }

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            getReservedRoomTypeQuantity(txtDate.Text);
            // Sort list from small to large
            sorting();
            displayChartProfit();

            RepeaterRentedRoomType.DataSource = reservedRoomTypes;
            RepeaterRentedRoomType.DataBind();

        }

        protected void txtYearMonth_TextChanged(object sender, EventArgs e)
        {
            getReservedRoomTypeQuantity(txtDate.Text);
            sorting();
            displayChartProfit();

        }

        protected void ddlYear_TextChanged(object sender, EventArgs e)
        {
            getReservedRoomTypeQuantity(txtDate.Text);
            sorting();
            displayChartProfit();
        }

        protected void ddlReportType_TextChanged(object sender, EventArgs e)
        {
            

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

        private void displayChartProfit()
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

            string getRoomTypes = "SELECT DISTINCT(RoomTypeID), Title FROM RoomType ORDER BY RoomTypeID DESC";

            SqlCommand cmdGetRoomTypes = new SqlCommand(getRoomTypes, conn);

            SqlDataReader sdr = cmdGetRoomTypes.ExecuteReader();

            while (sdr.Read())
            {
                reservedRoomTypes.Add(new ReservedRoomType(sdr["RoomTypeID"].ToString(), sdr["Title"].ToString()));
            }

            conn.Close();
        }

        private void sorting()
        {
            // Before sorted
            for(int i = 0; i < reservedRoomTypes.Count; i++)
            {
                string temp = reservedRoomTypes[i].roomTypeName;
                int qty = reservedRoomTypes[i].quantity;
            }

            reservedRoomTypes.Sort((x, y) => y.quantity.CompareTo(x.quantity));

            for (int i = 0; i < reservedRoomTypes.Count; i++)
            {
                string temp = reservedRoomTypes[i].roomTypeName;
                int qty = reservedRoomTypes[i].quantity;
            }

        }
    }
}