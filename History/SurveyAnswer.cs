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

namespace Hotel_Management_System.History
{
    public class SurveyAnswer : IHttpModule
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

        public string questionID { get; set; }

        public string question { get; set; }

        public int answer { get; set; }

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public SurveyAnswer()
        {

        }

        public SurveyAnswer(string questionID, int answer)
        {
            this.questionID = questionID;
            this.answer = answer;
            getsurveyQuestion();
        }

        private void getsurveyQuestion()
        {
            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getSurveyQuestion = "SELECT Question FROM SurveyQuestion WHERE QuestionID LIKE @QuestionID";

            SqlCommand cmdGetSurveyQuestion = new SqlCommand(getSurveyQuestion, conn);

            cmdGetSurveyQuestion.Parameters.AddWithValue("@QuestionID", questionID);

            question = (string)cmdGetSurveyQuestion.ExecuteScalar();

            conn.Close();
        }

    }
}
