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

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = "RS10000001";

            if (!IsPostBack)
            {
                ViewState["SurveyResponse"] = new SurveyResponse(reservationID);
            }
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {

        }

        private void getSurveyResponse()
        {
            // Get reference of SurveyResponse object
            SurveyResponse surveyResponse = (SurveyResponse)ViewState["SurveyResponse"];

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
            SurveyResponse surveyResponse = (SurveyResponse)ViewState["SurveyResponse"];

            lblGuestName.Text = surveyResponse.guestName;
            lblIDNo.Text = surveyResponse.idNo;
            lblCheckIn.Text = surveyResponse.checkInDate;
            lblCheckOut.Text = surveyResponse.checkOutDate;
        }

    }
}