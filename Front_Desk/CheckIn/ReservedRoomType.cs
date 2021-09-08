using System;
using System.Collections.Generic;
using System.Web;

namespace Hotel_Management_System.Front_Desk.CheckIn
{
    public class ReservedRoomType : IHttpModule
    {
        private AvailableRoom ar;

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
        public string date { get; set; }

        public List<AvailableRoom> availableRooms { get; set; }
        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public ReservedRoomType()
        {

        }

        public ReservedRoomType(string roomTypeID, string date, List<AvailableRoom> availableRooms)
        {
            this.roomTypeID = roomTypeID;
            this.date = date;
            this.availableRooms = availableRooms;
        }

        public ReservedRoomType(string roomTypeID, string date, AvailableRoom ar)
        {
            this.roomTypeID = roomTypeID;
            this.date = date;
            this.ar = ar;
        }
    }
}
