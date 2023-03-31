/*
 * Author: Koh Xin Hao
 * Student ID: 20WMR09471
 * Programme: RSF3G4
 * Year: 2021
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;
using Hotel_Management_System.Front_Desk.CheckIn;
using System.Configuration;
using System.Net.Mail;
using System.IO;

namespace Hotel_Management_System.Front_Desk.CheckOut
{
    public partial class CheckOutConfirmation : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        string reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Save Check Out";

            if (!IsPostBack)
            {
                Session["ServiceCharges"] = new List<ServiceCharges>();

                setReservationDetails();

                setMissingEquipmentToRepeater();

                checkIfNoMissingEquipment();

                calculateTotalCharges();

                PopupCover.Visible = false;
                PopupDelete.Visible = false;

                enableOrDisablePaymentDetails();
            }

            checkServiceChargesIsEmpty();
        }

        private void enableOrDisablePaymentDetails()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            // If there is no any charges
            if(serviceCharges.Count == 0 && missingEquipments.Count == 0)
            {
                txtReferenceNo.Enabled = false;
                ddlPaymentMethod.Enabled = false;
            }
            else
            {
                txtReferenceNo.Enabled = true;
                ddlPaymentMethod.Enabled = true;
            }
        }

        private void setReservationDetails()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Set the reservation details into label
            lblGuestName.Text = getGuestName(reservationDetails.guestID);
            lblCheckInDate.Text = reservationDetails.checkInDate;
            lblCheckOutDate.Text = reservationDetails.checkOutDate;
            lblDurationOfStay.Text = reservationUtility.getdurationOfStay(reservationDetails.checkInDate, reservationDetails.checkOutDate).ToString();

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(lblCheckInDate.Text);
            DateTime formatedCheckOutDate = Convert.ToDateTime(lblCheckOutDate.Text);

            // Display the formated date
            lblCheckInDate.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOutDate.Text = formatedCheckOutDate.ToShortDateString();
        }

        private string getGuestName(string guestID)
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String getGuestName = "SELECT Name FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetGuestName = new SqlCommand(getGuestName, conn);

            cmdGetGuestName.Parameters.AddWithValue("@ID", guestID);

            string guestName = (string)cmdGetGuestName.ExecuteScalar();

            conn.Close();

            return guestName;
        }

        private void checkIfNoMissingEquipment()
        {
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            if(missingEquipments.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        private void setMissingEquipmentToRepeater()
        {

            // Set the missing equipment to repeater
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];


            RepeaterMissingEquipment.DataSource = missingEquipments;
            RepeaterMissingEquipment.DataBind();
        }

        public void calculateTotalCharges()
        {
            // Get reference of MissingEquipment
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            // Get reference of ServiceCharges
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            double totalPayment = 0;

            for(int i = 0; i < missingEquipments.Count; i++)
            {
                totalPayment += missingEquipments[i].fineCharges;
            }

            for(int i = 0; i < serviceCharges.Count; i++)
            {
                totalPayment += serviceCharges[i].charges;
            }

            if (totalPayment != 0.00)
            {
                // Display total
                lblTotal.Text = string.Format("{0:0.00}", totalPayment);

                // Calculate grand total
                totalPayment += calcTaxCharges(totalPayment);

                // Display grand total
                lblGrandTotal.Text = string.Format("{0:0.00}", totalPayment);
            }
            else
            {   // If no facility add on

                // Disable the text box
                txtReferenceNo.Enabled = false;
                ddlPaymentMethod.Enabled = false;

                // Set the total to 0
                lblTotal.Text = "0.00";
                lblTax.Text = "0.00";
                lblGrandTotal.Text = "0.00";
            }

        }
        private double calcTaxCharges(double totalPayment)
        {
            // Calculate tax

            double tax = 0;

            // Get the tax rate from application variable
            tax = totalPayment * (double)Application["TaxRate"];

            // Display total tax charges
            lblTax.Text = string.Format("{0:0.00}", tax);

            return tax;
        }

        private void checkServiceChargesIsEmpty()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            if (serviceCharges.Count == 0)
            {
                lblNoServiceCharges.Visible = true;
            }
            else
            {
                lblNoServiceCharges.Visible = false;
            }
        }

        protected void btnSaveServiceCharges_Click(object sender, EventArgs e)
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            String charges = txtCharges.Text;

            // If user doesn't enter equipment price
            if (charges == "")
            {
                charges = "0";  // Set it to zero
            }

            // Add to Equipment class
            serviceCharges.Add(new ServiceCharges() { service = txtService.Text, charges = Convert.ToDouble(charges) });

            RepeaterServiceCharges.DataSource = serviceCharges;
            RepeaterServiceCharges.DataBind();

            txtService.Text = null;
            txtCharges.Text = null;

            lblNoServiceCharges.Visible = false;

            // Calculate total 
            calculateTotalCharges();
            
            enableOrDisablePaymentDetails();
        }

        protected void IBDeleteServiceCharges_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get service details for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String service = (item.FindControl("lblService") as Label).Text;
            String charges = (item.FindControl("lblCharges") as Label).Text;

            // Set index of current selected item to ViewState
            ViewState["ItemIndex"] = itemIndex;

            // Set delete message into popup
            lblPopupDeleteContent.Text = "Service: " + service + "<br />" +
                "Charges: " + charges + "<br /><br />";

            PopupCover.Visible = true;
            PopupDelete.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int itemIndex = int.Parse(ViewState["ItemIndex"].ToString());

            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            serviceCharges.RemoveAt(itemIndex - 1);

            RepeaterServiceCharges.DataSource = serviceCharges;
            RepeaterServiceCharges.DataBind();

            checkServiceChargesIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;

            // Calculate total 
            calculateTotalCharges();

            enableOrDisablePaymentDetails();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentCheckList.aspx?ID=" + en.encryption(reservationID));
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            // If there is any services charges added
            if (serviceCharges.Count > 0)
            {
                saveOtherCharges();
            }

            // If there is any missing equipment 
            if (missingEquipments.Count > 0)
            {
                saveFineCharges();
            }

            // Save Payment Details, if there is any amount due
            if (serviceCharges.Count > 0 || missingEquipments.Count > 0)
            {
                savePayment();
            }

            // Update reservation status to "Checked Out"
            updateReservationStatus();

            //***************************** Update housekeeping status to dirty*******************************************
            //updateHousekeepingStatusToDirty();

            // Send survey form to guest
            sendSurveyForm();

            // Show popup Success Message
            lblPopupCheckedOut.Text = "Check Out Success.";
            PopupCheckOut.Visible = true;
            PopupCover.Visible = true;
        }

        private void saveOtherCharges()
        {
            List<ServiceCharges> serviceCharges = (List<ServiceCharges>)Session["ServiceCharges"];

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Save a list of service charges
            for (int i = 0; i < serviceCharges.Count; i++)
            {
                // get next ChargesID
                String nextChargesID = idGenerator.getNextID("ChargesID", "OtherCharges", "OC");

                // Open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                string saveServiceCharges = "INSERT INTO OtherCharges (ChargesID, Title, Category, Amount, ReservationID, DateCreated) " +
                                            "VALUES (@ChargesID, @Title, @Category, @Amount, @ReservationID, @Date)";

                SqlCommand cmdSaveFineCharges = new SqlCommand(saveServiceCharges, conn);

                cmdSaveFineCharges.Parameters.AddWithValue("@ChargesID", nextChargesID);
                cmdSaveFineCharges.Parameters.AddWithValue("@Title", serviceCharges[i].service);
                cmdSaveFineCharges.Parameters.AddWithValue("@Category", "Service");
                cmdSaveFineCharges.Parameters.AddWithValue("@Amount", Convert.ToDecimal(serviceCharges[i].charges));
                cmdSaveFineCharges.Parameters.AddWithValue("@ReservationID", reservationID);
                cmdSaveFineCharges.Parameters.AddWithValue("@Date", todaysDate);

                int success = cmdSaveFineCharges.ExecuteNonQuery();

                conn.Close();
            }
            
        }

        private void saveFineCharges()
        {
            List<MissingEquipment> missingEquipments = (List<MissingEquipment>)Session["MissingEquipments"];

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Save a list of service charges
            for (int i = 0; i < missingEquipments.Count; i++)
            {
                // get next ChargesID
                String nextChargesID = idGenerator.getNextID("ChargesID", "OtherCharges", "OC");

                // Open connection
                conn = new SqlConnection(strCon);
                conn.Open();

                string saveFineCharges = "INSERT INTO OtherCharges VALUES (@ChargesID, @Title, @Category, @EquipmentID, @Amount, @ReservationID, @Date)";

                SqlCommand cmdSaveFineCharges = new SqlCommand(saveFineCharges, conn);

                cmdSaveFineCharges.Parameters.AddWithValue("@ChargesID", nextChargesID);
                cmdSaveFineCharges.Parameters.AddWithValue("@Title", missingEquipments[i].title);
                cmdSaveFineCharges.Parameters.AddWithValue("@Category", "Fine");
                cmdSaveFineCharges.Parameters.AddWithValue("@EquipmentID", missingEquipments[i].equipmentID);
                cmdSaveFineCharges.Parameters.AddWithValue("@Amount", Convert.ToDecimal(missingEquipments[i].fineCharges));
                cmdSaveFineCharges.Parameters.AddWithValue("@ReservationID", reservationID);
                cmdSaveFineCharges.Parameters.AddWithValue("@Date", todaysDate);

                int success = cmdSaveFineCharges.ExecuteNonQuery();

                conn.Close();
            }
        }


        private void savePayment()
        {
            // get next PaymentID
            String nextPaymentID = idGenerator.getNextID("PaymentID", "Payment", "P");

            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            String addPayment = "INSERT INTO Payment VALUES (@PaymentID, @PaymentMethod, @ReferenceNo, @Amount, @ReservationID, @Date)";

            SqlCommand cmdAddPayment = new SqlCommand(addPayment, conn);

            cmdAddPayment.Parameters.AddWithValue("@PaymentID", nextPaymentID);
            cmdAddPayment.Parameters.AddWithValue("@PaymentMethod", ddlPaymentMethod.SelectedValue);
            cmdAddPayment.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text);
            cmdAddPayment.Parameters.AddWithValue("@Amount", Convert.ToDecimal(lblGrandTotal.Text));
            cmdAddPayment.Parameters.AddWithValue("@ReservationID", reservationID);
            cmdAddPayment.Parameters.AddWithValue("@Date", todaysDate);

            int i = cmdAddPayment.ExecuteNonQuery();

            conn.Close();

        }

        private void updateReservationStatus()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string updateReservationStatus = "UPDATE Reservation SET Status = 'Checked Out', Feedback = @Feedback " +
                                                "WHERE ReservationID LIKE @ID";

            SqlCommand cmdUpdateReservationStatus = new SqlCommand(updateReservationStatus, conn);

            cmdUpdateReservationStatus.Parameters.AddWithValue("@ID", reservationID);
            cmdUpdateReservationStatus.Parameters.AddWithValue("@Feedback", txtFeedback.Text);

            int i = cmdUpdateReservationStatus.ExecuteNonQuery();

            conn.Close();

        }

        private void updateHousekeepingStatusToDirty()
        {
            // Use to update the last staying room as dirty after the guest check out from the hotel
            List<String> roomID = getLastStayingRoom();

            for(int i = 0; i < roomID.Count; i++)
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                string updateHousekeepingStatus = "UPDATE Room SET HousekeepingStatus = 'Dirty' WHERE RoomID LIKE @RoomID";

                SqlCommand cmdUpdateHouseKeepingStatus = new SqlCommand(updateHousekeepingStatus, conn);

                cmdUpdateHouseKeepingStatus.Parameters.AddWithValue("@RoomID", roomID[i]);

                int success = cmdUpdateHouseKeepingStatus.ExecuteNonQuery();

                conn.Close();
            }
           

        }

        private List<String> getLastStayingRoom()
        {
            List<String> roomID = new List<string>();

            // Get last reservation date
            string lastReservationDate = getLastReservationDate(reservationID);

            conn = new SqlConnection(strCon);
            conn.Open();

            string getLastStayingRoom = "SELECT RoomID FROM ReservationRoom WHERE ReservationID LIKE @ID AND Date LIKE @Date";

            SqlCommand cmdGetLastStayingRoom = new SqlCommand(getLastStayingRoom, conn);

            cmdGetLastStayingRoom.Parameters.AddWithValue("@ID", reservationID);
            cmdGetLastStayingRoom.Parameters.AddWithValue("@Date", lastReservationDate);

            SqlDataReader sdr = cmdGetLastStayingRoom.ExecuteReader();

            while (sdr.Read())
            {
                roomID.Add(sdr["RoomID"].ToString());
            }

            conn.Close();

            return roomID;

        }

        private string getLastReservationDate(string reservationID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string getCheckOutDate = "SELECT CheckOutDate FROM Reservation WHERE ReservationID LIKE @ID";

            SqlCommand cmdGetCheckOutDate = new SqlCommand(getCheckOutDate, conn);

            cmdGetCheckOutDate.Parameters.AddWithValue("@ID", reservationID);

            string checkOutDate = (string)cmdGetCheckOutDate.ExecuteScalar();

            conn.Close();

            // Get last reservation date
            DateTime previousDate = Convert.ToDateTime(checkOutDate);
            previousDate = previousDate.AddDays(-1);

            // Return last reservation date
            return reservationUtility.formatDate(previousDate.ToShortDateString());
        }

        private void sendSurveyForm()
        {
            if (surveyFormEnabled())
            {
                // **** Important Note *****
                // Please set up this three variable b4 sending email
                // Set receiver email
                string emailTo = getGuestEmailAddress();


                // Check if guest have any emailAddress
                if (emailTo.Length > 0)
                {
                    // Set sender and receiver email
                    string emailFrom = "hmsagent1221@gmail.com";
                    string password = "Asdfg12345@";
                    try
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(emailTo);
                            mail.Subject = "Guest Satisfactory Survey";
                            mail.Body = CreateEmailBody();
                            mail.IsBodyHtml = true;

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                lblEmailStatus.Text = "Survey Form Sent.";

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblEmailStatus.Text = ex.Message;
                    }
                }

            }
        }

        private string CreateEmailBody()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            IDEncryption en = new IDEncryption();

            string encryptedReservationID = en.encryption(reservationID);

            string emailBody = String.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("Survey (Email).html")))
            {
                emailBody = reader.ReadToEnd();
            }

            // Replace the text in Template.html
            emailBody = emailBody.Replace("{fname}", getGuestName(reservationDetails.guestID));
            emailBody = emailBody.Replace("{link}", "https://localhost:" + Application["LocalHostID"].ToString() + "/Front_Desk/Survey/SurveyForm.aspx?ID=" + encryptedReservationID);

            return emailBody;
        }

        private Boolean surveyFormEnabled()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalEnabledQuestion = "SELECT COUNT(*) FROM SurveyQuestion WHERE Status LIKE 'Active'";

            SqlCommand cmdGetTotalEnabledQuestion = new SqlCommand(getTotalEnabledQuestion, conn);

            int count = (int)cmdGetTotalEnabledQuestion.ExecuteScalar();

            conn.Close();

            if(count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string getGuestEmailAddress()
        {
            // Get refernce of ReservationDetail
            ReservationDetail reservationDetails = (ReservationDetail)Session["ReservationDetails"];

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getEmailAddress = "SELECT Email FROM Guest WHERE GuestID LIKE @ID";

            SqlCommand cmdGetEmailAddress = new SqlCommand(getEmailAddress, conn);

            cmdGetEmailAddress.Parameters.AddWithValue("@ID", reservationDetails.guestID);

            string emailAddress = "";
            try
            {
               emailAddress  = (string)cmdGetEmailAddress.ExecuteScalar();
            }
            catch(Exception ex)
            {

            }

            conn.Close();

            return emailAddress;
        }


        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckOut.aspx");
        }
    }
}