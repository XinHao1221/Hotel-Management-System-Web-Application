using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public class RentedFacility : IHttpModule
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

        public string facilityID { get; set; }
        public string facilityName { get; set; }
        public int quantity { get; set; }
        public string priceType { get; set; }
        public double price { get; set; }
        public string rentDate { get; set; }
        public string returnDate { get; set; }
        public double subTotal { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public RentedFacility()
        {

        }

        public RentedFacility(string facilityID, string facilityName, int quantity, string priceType, double price, string rentDate, string returnDate, double subTotal)
        {
            this.facilityID = facilityID;
            this.facilityName = facilityName;
            this.quantity = quantity;
            this.priceType = priceType;
            this.price = price;
            this.rentDate = rentDate;
            this.returnDate = returnDate;
            this.subTotal = subTotal;
        }

    }
}
