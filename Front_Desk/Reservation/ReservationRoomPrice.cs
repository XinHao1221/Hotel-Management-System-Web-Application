/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

// Use to hold the room price within the selected check-in and check-out date for repeater purpose

using System;
using System.Web;

namespace Hotel_Management_System.Front_Desk.Reservation
{
    public class ReservationRoomPrice : IHttpModule
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

        public string roomTypeID { get; set; }
        public string roomType { get; set; }
        public string date { get; set; }
        public double roomPrice { get; set; }

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public ReservationRoomPrice(string roomTypeID, string roomType, string date, double roomPrice)
        {
            this.roomTypeID = roomTypeID;
            this.roomType = roomType;
            this.date = date;
            this.roomPrice = roomPrice;
        }


    }
}
