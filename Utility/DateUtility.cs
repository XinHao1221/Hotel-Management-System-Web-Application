using System;
using System.Web;

namespace Hotel_Management_System.Utility
{
    public class DateUtility : IHttpModule
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

        private DateTime date = DateTime.Now.Date;
        private DayOfWeek day = DateTime.Now.DayOfWeek;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public String getDate()
        {
            return date.ToShortDateString();
        }

        public String getDay()
        {
            return date.Day.ToString();
        }

        public String getMonth()
        {

            String month = date.Month.ToString();

            switch (month)
            {
                case "1": return "January";
                case "2": return "February";
                case "3": return "March";
                case "4": return "April";
                case "5": return "May";
                case "6": return "June";
                case "7": return "July";
                case "8": return "August";
                case "9": return "September";
                case "10": return "October";
                case "11": return "November";
                default: return "December";
            }
        }

        public String getYear()
        {
            return date.Year.ToString();
        }

        public String getDayOfWeek()
        {
            return day.ToString();
        }
    }
}
