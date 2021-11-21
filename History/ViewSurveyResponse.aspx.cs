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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.History
{
    public partial class ViewSurveyResponse : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        private String reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = en.decryption(Request.QueryString["ID"]);

            // Page TItle
            Page.Title = "Survey Response";

            if (!IsPostBack)
            {
                Session["SurveyResponse"] = new SurveyResponse(reservationID);

                setStayDetails();

                getSurveyResponse();

                setResponseStatus();

                displaySurveyResponse();
            }
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewTransactionHistory.aspx?ID=" + en.encryption(reservationID));
        }

        private void getSurveyResponse()
        {
            // Get reference of SurveyResponse object
            SurveyResponse surveyResponse = (SurveyResponse)Session["SurveyResponse"];

            List<SurveyAnswer> surveyAnswers = new List<SurveyAnswer>();

            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getSurveyResponse = "SELECT QuestionID, Answer FROM Survey S, SurveyAnswer SA WHERE S.ReservationID LIKE @ID AND " +
                                        "S.SurveyID LIKE SA.SurveyID";

            SqlCommand cmdGetSurveyResponse = new SqlCommand(getSurveyResponse, conn);

            cmdGetSurveyResponse.Parameters.AddWithValue("@ID", reservationID);

            SqlDataReader sdr = cmdGetSurveyResponse.ExecuteReader();

            while (sdr.Read())
            {
                surveyAnswers.Add(new SurveyAnswer(sdr["QuestionID"].ToString(), int.Parse(sdr["Answer"].ToString())));
            }

            conn.Close();

            surveyResponse.surveyAnswers = surveyAnswers;
        }

        private void setStayDetails()
        {
            // Get reference of SurveyResponse object
            SurveyResponse surveyResponse = (SurveyResponse)Session["SurveyResponse"];

            lblGuestName.Text = surveyResponse.guestName;
            lblIDNo.Text = surveyResponse.idNo;

            // Format date base on date format on user's computer
            DateTime formatedCheckInDate = Convert.ToDateTime(surveyResponse.checkInDate);
            DateTime formatedCheckOutDate = Convert.ToDateTime(surveyResponse.checkOutDate);

            lblCheckIn.Text = formatedCheckInDate.ToShortDateString();
            lblCheckOut.Text = formatedCheckOutDate.ToShortDateString();

            int durationOfStay = reservationUtility.getdurationOfStay(lblCheckIn.Text, lblCheckOut.Text);

            lblDurationOfStay.Text = durationOfStay.ToString() + " Night";

        }

        private void displaySurveyResponse()
        {
            // Get reference of SurveyResponse object
            SurveyResponse surveyResponse = (SurveyResponse)Session["SurveyResponse"];

            List<SurveyAnswer> surveyAnswers = surveyResponse.surveyAnswers;

            RepeaterSurveyResponse.DataSource = surveyAnswers;
            RepeaterSurveyResponse.DataBind();
        }

        protected void RepeaterSurveyResponse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblAnswer = e.Item.FindControl("lblAnswer") as Label;
            RadioButtonList rblSurveyAnswer = e.Item.FindControl("rblSurveyAnswer") as RadioButtonList;

            rblSurveyAnswer.SelectedValue = lblAnswer.Text;
        }

        private void setResponseStatus()
        {
            // Get reference of SurveyResponse object
            SurveyResponse surveyResponse = (SurveyResponse)Session["SurveyResponse"];

            List<SurveyAnswer> surveyAnswers = surveyResponse.surveyAnswers;

            int totalScore = 0;

            // Total up the score of survey response
            for(int i = 0; i < surveyAnswers.Count; i++)
            {
                totalScore += surveyAnswers[i].answer;
            }

            if((totalScore / surveyAnswers.Count) < 3)
            {
                lblStatus.Text = "Bad";
                lblStatus.Style["color"] = "red";
                lblStatus.Style["font-weight"] = "bold";
            }
            else if((totalScore / surveyAnswers.Count) == 3)
            {
                lblStatus.Text = "Average";
                lblStatus.Style["color"] = "rgb(194, 110, 0)";
                lblStatus.Style["font-weight"] = "bold";
            }
            else
            {
                lblStatus.Text = "Good";
                lblStatus.Style["color"] = "rgb(0, 206, 27)";
                lblStatus.Style["font-weight"] = "bold";
            }
        }
    }
}