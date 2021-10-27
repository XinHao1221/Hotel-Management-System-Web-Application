﻿using System;
using System.Web;

namespace Hotel_Management_System.Dashboard
{
    public class GuestInHouse : IHttpModule
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

        public string reservationID { get; set; }

        public string roomID { get; set; }

        public int adults { get; set; }

        public int kids { get; set; }

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public GuestInHouse()
        {

        }

        public GuestInHouse(string reservationID, string roomID, int adults, int kids)
        {
            this.reservationID = reservationID;
            this.roomID = roomID;
            this.kids = kids;
            this.adults = adults;
        }
    }
}
