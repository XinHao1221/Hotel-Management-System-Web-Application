﻿/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
#pragma warning disable CS0105 // The using directive for 'System.Data.SqlClient' appeared previously in this namespace
using System.Data.SqlClient;
#pragma warning restore CS0105 // The using directive for 'System.Data.SqlClient' appeared previously in this namespace

namespace Hotel_Management_System.Front_Desk.CheckOut
{
    public class MissingEquipment : IHttpModule
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
        public string equipmentID { get; set; }

        public string title { get; set; }

        public double fineCharges { get; set; }

        public string roomTypeID { get; set; }

        public string roomType { get; set; }

        public string roomID { get; set; }

        public string roomNO { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public MissingEquipment()
        {

        }

        public MissingEquipment(string equipmentID, string title, string roomTypeID, string roomType, string roomID, string roomNo)
        {
            this.equipmentID = equipmentID;
            this.title = title;
            this.fineCharges = getFineChargesFromDatabase(this.equipmentID);
            this.roomTypeID = roomTypeID;
            this.roomType = roomType;
            this.roomID = roomID;
            this.roomNO = roomNo;
        }

        public MissingEquipment(string equipmentID, string title, double fineCharges)
        {
            this.equipmentID = equipmentID;
            this.title = title;
            this.fineCharges = fineCharges;
        }

        private double getFineChargesFromDatabase(string equipmentID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getFineCharges = "SELECT FineCharges FROM Equipment WHERE EquipmentID LIKE @ID";

            SqlCommand cmdGetFineCharges = new SqlCommand(getFineCharges, conn);

            cmdGetFineCharges.Parameters.AddWithValue("@ID", equipmentID);

            double fineCharges = Convert.ToDouble(cmdGetFineCharges.ExecuteScalar());

            conn.Close();

            return fineCharges;
        }
    }
}
