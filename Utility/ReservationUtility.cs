using System;
using System.Web;

namespace Hotel_Management_System.Utility
{
    public class ReservationUtility : IHttpModule
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

        private int year;
        private int month;
        private int day;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public int getdurationOfStay(String checkIn, String checkOut)
        {

            DateTime checkInDate = Convert.ToDateTime(checkIn);
            DateTime checkOutDate = Convert.ToDateTime(checkOut);

            return Convert.ToInt32((checkOutDate - checkInDate).TotalDays);
        }

        public String[] getReservationDate(String checkIn, String checkOut)
        {

            int durationOfStay = getdurationOfStay(checkIn, checkOut);
            String[] reservationDate = new string[durationOfStay];

            DateTime checkInDate = Convert.ToDateTime(checkIn);
            DateTime checkOutDate = Convert.ToDateTime(checkOut);

            for (int i = 0; i < durationOfStay; i++)
            {
                DateTime temp = checkInDate.AddDays(i);
                reservationDate[i] = temp.ToString();
            }

            return reservationDate;

        }

        public String[] getReservationDate(String checkIn, int durationOfStay)
        {

            String[] reservationDate = new string[durationOfStay];

            DateTime checkInDate = Convert.ToDateTime(checkIn);

            for (int i = 0; i < durationOfStay; i++)
            {
                DateTime temp = checkInDate.AddDays(i);
                reservationDate[i] = formatDate(temp.ToString());
            }

            return reservationDate;

        }

        public String formatDate(int year, int month, int day)
        {

            String date = year + "-" + ((month < 10 ? "0" : "") + month) + "-" + ((day < 10 ? "0" : "") + day);

            return date;

        }

        public String formatDate(String date)
        {

            int[] dateComponent = new int[3];   // day month year
            String temp = "";

            int j = 0;

            for (int i = 0; i < 3; i++)
            {
                while ((date[j] != '/') && (date[j] != ' '))
                {
                    temp += date[j];
                    j++;
                }
                dateComponent[i] = int.Parse(temp);
                j++;
                temp = "";
            }

            /* Notes: 
              dateComponent[0] = day
              dateComponent[1] = month
              dateComponent[2] = year
             */

            String formatedDate = dateComponent[2] + "-" + ((dateComponent[1] < 10 ? "0" : "") + dateComponent[1]) + "-" + ((dateComponent[0] < 10 ? "0" : "") + dateComponent[0]);

            return formatedDate;

        }

        public String getNextDate(String currentDate)
        {
            DateTime nextDate = Convert.ToDateTime(currentDate);

            return formatDate(nextDate.AddDays(1).ToString());
        }

        public String getNextDate(String currentDate, int duration)
        {
            DateTime nextDate = Convert.ToDateTime(currentDate);

            return formatDate(nextDate.AddDays(duration).ToString());
        }
    }
}
