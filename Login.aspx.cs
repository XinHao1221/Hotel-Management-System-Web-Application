using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace myFYP
{
    public partial class Login : System.Web.UI.Page
    {
        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page Title
            Page.Title = "Login";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            string sqlquery = "Select [UserID] from [User] WHERE [UserID]=@UserID AND [UserPassword]=@Password";

            // Get User ID
            SqlCommand sqlcomm = new SqlCommand(sqlquery, conn);

            sqlcomm.Parameters.AddWithValue("@UserID", txtUserID.Text);
            sqlcomm.Parameters.AddWithValue("@Password", txtPass.Text);
            String userID = (string)sqlcomm.ExecuteScalar();

            sqlquery = "SELECT [UserRole] FROM [User] WHERE [UserID]=@UserID AND [UserPassword]=@Password";

            // Get Role
            sqlcomm = new SqlCommand(sqlquery, conn);

            sqlcomm.Parameters.AddWithValue("@UserID", txtUserID.Text);
            sqlcomm.Parameters.AddWithValue("@Password", txtPass.Text);
            String userRole = (string)sqlcomm.ExecuteScalar();

            if (userID != null && userRole != null)
            {
                Session["UserID"] = userID;
                Session["UserRole"] = userRole;

                Response.Redirect("Dashboard/Dashboard.aspx");
            }
            else
            {
                string msg = "Invalid username or password! Please try again";
                Response.Write("<script>alert('" + msg + "')</script>");
            }

            conn.Close();
        }
    }
}