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

namespace Hotel_Management_System.Utility
{
    public class IDGenerator : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public String getNextID(String tableID, String tableName, String prefix)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            String getLastID = "SELECT TOP 1 " + tableID + " FROM " + tableName + " ORDER BY " + tableID + " DESC";

            SqlCommand cmdGetLastID = new SqlCommand(getLastID, conn);
            
            String id = (String)cmdGetLastID.ExecuteScalar();

            conn.Close();

            return generateNextID(id, prefix);

        }

        private String generateNextID(String lastID, string prefix)
        {

            if(lastID == null)  // Execute when there is no item inside the database
            {
                return prefix + "10000001";
            }
            else 
            {   // Get next ID

                int i = prefix.Length;

                String extractedID = (String)lastID.Substring(prefix.Length, (lastID.Length - prefix.Length));

                int intID = int.Parse(extractedID);

                intID += 1;

                return prefix + intID.ToString();
            }

        }

        public void Init(HttpApplication context)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
