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

namespace Hotel_Management_System.Front_Desk
{
    public partial class SurveyForm : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        string reservationID;

        // Create instance of IDEncryption class
        IDEncryption en = new IDEncryption();

        // Create instance of IDGerator class
        IDGenerator idGenerator = new IDGenerator();

        // Create instance of ReservationUltility class
        ReservationUtility reservationUtility = new ReservationUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            reservationID = Request.QueryString["ID"];

            // Page TItle
            Page.Title = "Survey Form";

            reservationID = en.decryption(reservationID);

            if (!IsPostBack)
            {
                checkIfSurveyResponded();

                setSurveyQuestionToRepeater();
            }
            
        }

        private void checkIfSurveyResponded()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string checkIfResponded = "SELECT COUNT(*) FROM Survey WHERE ReservationID LIKE @ID";

            SqlCommand cmdCheckIfResponded = new SqlCommand(checkIfResponded, conn);

            cmdCheckIfResponded.Parameters.AddWithValue("@ID", reservationID);

            int count = (int)cmdCheckIfResponded.ExecuteScalar();

            conn.Close();

            if(count > 0)
            {
                Response.Redirect("SurveySuccess.aspx");
            }
            
        }

        private void setSurveyQuestionToRepeater()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getQuestion = "SELECT * FROM SurveyQuestion WHERE Status LIKE 'Active'";

            SqlCommand cmdGetQuestion = new SqlCommand(getQuestion, conn);

            SqlDataReader sdr = cmdGetQuestion.ExecuteReader();

            RepeaterQuestion.DataSource = sdr;
            RepeaterQuestion.DataBind();

            conn.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String nextSurveyID = idGenerator.getNextID("SurveyID", "Survey", "S");

            saveSurveyID(nextSurveyID);

            foreach (RepeaterItem item in RepeaterQuestion.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    //var rblSurveyAnswer = item.FindControl("rblSurveyAnswer") as RadioButtonList;
                    Label lblQuestionID = item.FindControl("lblQuestionID") as Label;
                    RadioButtonList rblSurveyAnswer = item.FindControl("rblSurveyAnswer") as RadioButtonList;
                    String temp = rblSurveyAnswer.SelectedValue;

                    // Open connection
                    conn = new SqlConnection(strCon);
                    conn.Open();

                    string saveResponse = "INSERT INTO SurveyAnswer Values (@SurveyID, @QuestionID, @Answer)";

                    SqlCommand cmdSaveResponse = new SqlCommand(saveResponse, conn);

                    cmdSaveResponse.Parameters.AddWithValue("@SurveyID", nextSurveyID);
                    cmdSaveResponse.Parameters.AddWithValue("@QuestionID", lblQuestionID.Text);
                    cmdSaveResponse.Parameters.AddWithValue("@Answer", int.Parse(rblSurveyAnswer.SelectedValue));

                    int i = cmdSaveResponse.ExecuteNonQuery();

                    conn.Close();
                }
            }

            // Redirect to success page
            Response.Redirect("SurveySuccess.aspx");
        }

        private void saveSurveyID(string nextSurveyID)
        {
            // Get current date
            DateTime dateNow = DateTime.Now;
            string todaysDate = reservationUtility.formatDate(dateNow.ToString());

            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string addSurveyID = "INSERT INTO Survey VALUES (@SurveyID, @ReservationID, @Date)";

            SqlCommand cmdAddSurveyID = new SqlCommand(addSurveyID, conn);

            cmdAddSurveyID.Parameters.AddWithValue("@SurveyID", nextSurveyID);
            cmdAddSurveyID.Parameters.AddWithValue("@ReservationID", reservationID);
            cmdAddSurveyID.Parameters.AddWithValue("@Date", todaysDate);

            int i = cmdAddSurveyID.ExecuteNonQuery();

            conn.Close();

        }
    }
}