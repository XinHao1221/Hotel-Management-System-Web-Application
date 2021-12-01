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
using System.Web.UI.DataVisualization.Charting;

namespace Hotel_Management_System.History
{
    public partial class ViewSurveyStatistics : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // **** Control access
            if (Session["UserRole"].ToString() != "Manager")
            {
                Response.Redirect("../Error/PermissionError.aspx");
            }

            // Set page title
            Page.Title = "Survey Response";

            setItemToRepeaterSurvey();
        }

        protected void LBBack_Click(object sender, EventArgs e)
        {
            // Navigate back
            Response.Redirect("History.aspx");
        }

        private void setItemToRepeaterSurvey()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // Retrieve a list of survey question
            string getSurveyQuestionID = "SELECT DISTINCT QuestionID FROM SurveyAnswer";

            SqlCommand cmdGetSurveyQuestionID = new SqlCommand(getSurveyQuestionID, conn);

            SqlDataReader sdr = cmdGetSurveyQuestionID.ExecuteReader();

            // Set data into repeater
            if (sdr.HasRows)
            {
                RepeaterSurveyResponse.DataSource = sdr;
                RepeaterSurveyResponse.DataBind();
            }

            conn.Close();
        }

        protected void RepeaterSurveyResponse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Get control's refernce
            Label lblQuestion = e.Item.FindControl("lblQuestion") as Label;
            Label lblQuestionID = e.Item.FindControl("lblQuestionID") as Label;
            Label lblTotalResponses = e.Item.FindControl("lblTotalResponses") as Label;
            Chart ChartSurveyQuestion = e.Item.FindControl("ChartSurveyQuestion") as Chart;

            // Display survey question
            lblQuestion.Text = getSurveyQuestion(lblQuestionID.Text);

            // Set data to histogram
            displayChartData(ChartSurveyQuestion, lblQuestionID.Text);

            // Get survey response total
            lblTotalResponses.Text = getTotalResponse(lblQuestionID.Text);
        }

        private string getTotalResponse(string questionID)
        {
            // Open Connection
            conn = new SqlConnection(strCon);
            conn.Open();

            string getTotalResponse = "SELECT COUNT(*) FROM SurveyAnswer WHERE QuestionID LIKE @QuestionID";

            SqlCommand cmdGetTotalResponse = new SqlCommand(getTotalResponse, conn);

            cmdGetTotalResponse.Parameters.AddWithValue("@QuestionID", questionID);

            int total = 0;

            try
            {
                total = (int)cmdGetTotalResponse.ExecuteScalar();
            }
            catch
            {

            }

            conn.Close();

            return total.ToString();
        }

        private string getSurveyQuestion(string questionID)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get survey question
            string getSurveyQuestion = "SELECT Question FROM SurveyQuestion WHERE QuestionID LIKE @QuestionID";

            SqlCommand cmdGetSurveyQuestion = new SqlCommand(getSurveyQuestion, conn);

            cmdGetSurveyQuestion.Parameters.AddWithValue("@QuestionID", questionID);

            string question = (string)cmdGetSurveyQuestion.ExecuteScalar();

            conn.Close();

            return question;
        }

        private void displayChartData(Chart ChartSurveyQuestion, string questionID)
        {
            // Initialize variable for x and y axis
            List<int> x = new List<int>();
            List<int> y = new List<int>();

            // Set data to be displayed
            for(int i = 1; i <= 5; i++)
            {
                x.Add(i);

                // get total response for a specific score
                y.Add(getTotalSelected(questionID, i));
            }

            // Set the data to be displayed on the histogram
            ChartSurveyQuestion.Series[0].Points.DataBindXY(x, y);

            ChartSurveyQuestion.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;

            ChartSurveyQuestion.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ChartSurveyQuestion.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            ChartSurveyQuestion.Legends[0].Enabled = false;

            // Set chart's tooltip
            foreach (Series s in ChartSurveyQuestion.Series)
            {
                s.Label = "#VALY   (#PERCENT)";
                s["PieLabelStyle"] = "Outside";
                s.ToolTip = "Room Type:";
            }
        }

        private int getTotalSelected(string questionID, int answer)
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // Get total user that voted a specific score
            string getTotalVoted = "SELECT COUNT(*) FROM SurveyAnswer WHERE QuestionID LIKE @QuestionID AND Answer = @Answer";

            SqlCommand cmdGetTotalVoted = new SqlCommand(getTotalVoted, conn);

            cmdGetTotalVoted.Parameters.AddWithValue("@QuestionID", questionID);
            cmdGetTotalVoted.Parameters.AddWithValue("@Answer", answer);

            int total = 0;

            try
            {
                total = (int)cmdGetTotalVoted.ExecuteScalar();
            }
            catch
            {

            }

            conn.Close();

            return total;
        }
    }
}