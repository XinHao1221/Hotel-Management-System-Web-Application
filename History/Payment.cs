using System;
using System.Web;

namespace Hotel_Management_System.History
{
    public class Payment : IHttpModule
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

        public string paymentID { get; set; }

        public string paymentMethod { get; set; }

        public string referenceNo { get; set; }

        public double amount { get; set; }

        public string date { get; set; }

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public Payment()
        {

        }

        public Payment(string paymentID, string paymentMethod, string referenceNo, double amount, string date)
        {
            this.paymentID = paymentID;
            this.paymentMethod = paymentMethod;
            this.referenceNo = referenceNo;
            this.amount = amount;
            this.date = date;
        }
    }
}
