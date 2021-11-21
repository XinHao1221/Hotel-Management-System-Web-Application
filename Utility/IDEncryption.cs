/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Web;

namespace Hotel_Management_System.Utility
{
    public class IDEncryption : IHttpModule
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

		private int key = 4;

		public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

		public String encryption(String id)
		{

			String temp = "";
			char ch;

			for (int i = id.Length - 1; i >= 0; i--)
			{

				ch = id[i];
				ch += (char)key;
				ch *= (char)key;

				temp += ch;
			}

			return temp;
		}

		public String decryption(String id)
		{

			String temp = "";
			char ch;


			//for(int i = id.length() - 1; i >= 0; i--){
			//    temp += id.charAt(i);
			//}

			for (int i = id.Length - 1; i >= 0; i--)
			{

				ch = id[i];
                ch /= (char)key;
				ch -= (char)key;

				temp += ch;
			}

			return temp;
		}


	}
}
