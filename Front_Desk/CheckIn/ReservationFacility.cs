/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public class ReservationFacility : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public string reservationFacilityID { get; set; }
        public string facilityID { get; set; }
        public string facilityName { get; set; }
        public int quantity { get; set; }
        public string priceType { get; set; }
        public double price { get; set; }
        public string rentDate { get; set; }
        public string returnDate { get; set; }
        public double subTotal { get; set; }
        public int group { get; set; }

        // True if the facility is rented via self-check in process
        public Boolean newlyAdded { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public ReservationFacility()
        {

        }

        public ReservationFacility(string reservationFacilityID, string facilityID, double price, string date, int group)
        {
            this.reservationFacilityID = reservationFacilityID;
            this.facilityID = facilityID;
            this.price = price;
            this.rentDate = date;
            this.group = group;
        }

        public ReservationFacility(string reservationFacilityID, string facilityID, double price, string date, int group, Boolean newlyAdded)
        {
            this.reservationFacilityID = reservationFacilityID;
            this.facilityID = facilityID;
            this.price = price;
            this.rentDate = date;
            this.group = group;
            this.newlyAdded = newlyAdded;
        }

        public ReservationFacility(string reservationFacilityID, string facilityID, int quantity, double price, string rentedDate, string returnDate)
        {
            this.reservationFacilityID = reservationFacilityID;
            this.facilityID = facilityID;
            this.facilityName = getFacilityName(facilityID);
            this.quantity = quantity;
            this.priceType = getPriceType(facilityID);
            this.price = price;
            this.rentDate = rentedDate;
            this.returnDate = returnDate;
            this.subTotal = calculateSubTotal(rentedDate, returnDate, quantity, price, priceType);
        }

        public ReservationFacility(string reservationFacilityID, string facilityID, int quantity, double price, string rentedDate, string returnDate, int group, Boolean newlyAdded)
        {
            this.reservationFacilityID = reservationFacilityID;
            this.facilityID = facilityID;
            this.facilityName = getFacilityName(facilityID);
            this.quantity = quantity;
            this.priceType = getPriceType(facilityID);
            this.price = price;
            this.rentDate = rentedDate;
            this.returnDate = returnDate;
            this.subTotal = calculateSubTotal(rentedDate, returnDate, quantity, price, priceType);
            this.group = group;
            this.newlyAdded = newlyAdded;
        }

        private string getFacilityName(String facilityID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getFacilityName = "SELECT FacilityName FROM Facility WHERE FacilityID LIKE @ID";

            SqlCommand cmdFacilityName = new SqlCommand(getFacilityName, conn);

            cmdFacilityName.Parameters.AddWithValue("@ID", facilityID);

            String facilityName = (String)cmdFacilityName.ExecuteScalar();

            conn.Close();

            return facilityName;

        }

        private string getPriceType(string facilityID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getPriceType = "SELECT PriceType FROM Facility WHERE FacilityID LIKE @ID";

            SqlCommand cmdGetPriceType = new SqlCommand(getPriceType, conn);

            cmdGetPriceType.Parameters.AddWithValue("@ID", facilityID);

            String priceType = (String)cmdGetPriceType.ExecuteScalar();

            conn.Close();

            return priceType;
        }

        private double calculateSubTotal(string rentedDate, string returnDate, int qty, double price, string priceType)
        {
            double subTotal;

            if (priceType == "Per Reservation")
            {
                subTotal = price * qty;
            }
            else
            {
                int durationOfStay = reservationUtility.getdurationOfStay(rentedDate, returnDate);

                subTotal = price * (qty * durationOfStay);
            }

            return subTotal;
        }

    }
}
