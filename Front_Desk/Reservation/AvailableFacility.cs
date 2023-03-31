/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Web;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public class AvailableFacility : IHttpModule
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
        public int availableQty { set; get; }
        public double price { set; get; }
        public string priceType { set; get; }
        public string status { set; get; }
        public string selected { set; get; }

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public AvailableFacility()
        {

        }

        public AvailableFacility(string facilityID, string facilityName, int availableQty, double price, string priceType, string status, string selected)
        {
            this.facilityID = facilityID;
            this.facilityName = facilityName;
            this.availableQty = availableQty;
            this.price = price;
            this.priceType = priceType;
            this.status = status;
            this.selected = selected;
        }
    }
}
