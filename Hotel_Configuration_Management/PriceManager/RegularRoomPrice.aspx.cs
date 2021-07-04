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

namespace Hotel_Management_System.Hotel_Configuration_Management.PriceManager
{
    public partial class RegularRoomPrice : System.Web.UI.Page
    {
        // Create instance of Encryption class
        IDEncryption en = new IDEncryption();

        // Create connection to database
        SqlConnection conn;
        String strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Create instance of RepeaterTableUltility
        RepeaterTableUtility ultility = new RepeaterTableUtility();

        int page;
        int offset = 0; // start index of data
        int fetch;      // Number of data to display per page

        protected void Page_Load(object sender, EventArgs e)
        {
            fetch = int.Parse(ddlItemPerPage.SelectedValue.ToString());
            page = getTotalNumberOfPage();


            // Execute when page is first loaded
            if (!IsPostBack)
            {
                if (page != 0)
                {
                    txtPage.Text = "1";

                    setItemToRepeater1();
                }
                else
                {   // Execute when no data is found
                    lblNoItemFound.Visible = true;
                    txtPage.Enabled = false;
                    ddlItemPerPage.AutoPostBack = false;
                }
            }
        }

        private int getTotalNumberOfItem()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(SP.StandardPriceID) " +
                                    "FROM RoomType RT, StandardRoomPrice SP " +
                                    "WHERE RT.Status LIKE 'Active' AND RT.RoomTypeID LIKE SP.RoomTypeID";

            

            SqlCommand cmdGetItemCount = new SqlCommand(getTotalNumebrOfItem, conn);

            int noOfItem = (int)cmdGetItemCount.ExecuteScalar();

            conn.Close();

            lblTotalNoOfItem.Text = noOfItem.ToString();


            return noOfItem;
        }

        private int getTotalNumberOfPage()
        {
            // Calculate total page of data

            double temp = (((double)getTotalNumberOfItem() / (double)fetch));

            double page = Math.Ceiling(temp);

            lblPage.Text = page.ToString();

            return (int)page;
        }

        private void setNoOfDisplayedItem()
        {
            // Display data range that currently displayed

            // Eg. 1 - 10
            if (!((offset + fetch) > (int)getTotalNumberOfItem()))
            {
                lblItemDisplayed.Text = (offset + 1).ToString() + " - " + (offset + fetch).ToString();
            }
            else
            {
                lblItemDisplayed.Text = (offset + 1).ToString() + " - " + getTotalNumberOfItem().ToString();
            }

        }

        private void setItemToRepeater1()
        {
            setNoOfDisplayedItem();

            conn = new SqlConnection(strCon);
            conn.Open();

            //String getName = "SELECT R.RoomID AS RoomID, R.RoomNumber AS RoomNumber, CONVERT(NVARCHAR(10), F.FloorNumber) + '  -  ' + F.FloorName AS FloorNo, RT.Title AS RoomType, " +
            //    "R.Status AS Status " +
            //        "FROM Room R, Floor F, RoomType RT " +
            //        "WHERE R.FloorID LIKE F.FloorID AND R.RoomTypeID LIKE RT.RoomTypeID AND R.Status IN('Active', 'Blocked') " +
            //        "ORDER BY R.RoomID DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            String getName = "SELECT (RT.RoomTypeID) AS RoomTypeID, (RT.Title) AS RoomType, (SP.MondayPrice) AS MonPrice, (SP.TuesdayPrice) AS TuePrice, " +
                "(SP.WednesdayPrice) AS WedPrice, (SP.ThursdayPrice) ThuPrice, " +
                "(SP.FridayPrice)AS FriPrice, (SP.SaturdayPrice)AS SatPrice, (SP.SundayPrice)AS SunPrice " +
                "FROM RoomType RT, StandardRoomPrice SP " +
                "WHERE RT.Status LIKE 'Active' AND RT.RoomTypeID LIKE SP.RoomTypeID " +
                "ORDER BY RT.RoomTypeID DESC OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

            SqlCommand cmdInsert = new SqlCommand(getName, conn);
            cmdInsert.Parameters.AddWithValue("@offset", offset);   // Assign start index
            cmdInsert.Parameters.AddWithValue("@fetch", fetch);     // Assign no of data to be read

            // Hold the data read from database
            SqlDataAdapter sda = new SqlDataAdapter(cmdInsert);

            DataTable dt = new DataTable();

            // Assign the data from database into dataTable
            sda.Fill(dt);

            // Bind data into repeater to display
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            // Display message it no item was found
            if (dt.Rows.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }

            conn.Close();
        }

        protected void IBArrowLeft_Click(object sender, EventArgs e)
        {
            // Execute when user request for previous page

            int nextOffset = int.Parse(txtPage.Text) - 1;

            // when current page is first page
            if (nextOffset == 0)
            {
                nextOffset = page;

                offset = (nextOffset * fetch) - fetch;

                txtPage.Text = nextOffset.ToString();
            }
            else
            {
                offset = (nextOffset * fetch) - fetch;
                txtPage.Text = nextOffset.ToString();
            }

            setItemToRepeater1();

        }

        protected void IBArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            // Execute when user request for next page

            int nextOffset = int.Parse(txtPage.Text) + 1;

            // When next page is last page
            if (nextOffset > page)
            {
                offset = 0;
                txtPage.Text = "1";
            }
            else
            {
                offset = (nextOffset * fetch) - fetch;
                txtPage.Text = nextOffset.ToString();
            }

            setItemToRepeater1();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When user select no of item per page. The displayed data will start from index 1
            txtPage.Text = "1";
            setItemToRepeater1();
        }

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            refreshPage();
        }

        private void refreshPage()
        {
            page = getTotalNumberOfPage();

            try
            {   // Update offset and fetch according to page number entered
                int number = int.Parse(txtPage.Text);

                if (number <= page && number > 0)
                {
                    // If user enter valid page number
                    offset = (number * fetch) - fetch;
                }
                else
                {
                    // If user enter page number which isn't within range
                    number = 1;

                    txtPage.Text = number.ToString();
                }


            }
            catch (Exception i)
            {
                // If user input is not an integer
                int number = 1;

                txtPage.Text = number.ToString();
            }

            // Set item to repeater
            setItemToRepeater1();
        }

        protected void LBMenuSearchBar_Click(object sender, EventArgs e)
        {
            searchRoomType();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchRoomType();
        }

        private void searchRoomType()
        {
            String roomType = txtSearch.Text;
            roomType = roomType.ToUpper();

            if (roomType == "")
            {
                lblNoItemFound.Visible = false;
                refreshPage();
            }
            else
            {
                conn = new SqlConnection(strCon);
                conn.Open();

                //String searchRoomType = "SELECT * FROM RoomType WHERE UPPER(Title) LIKE '%" + roomType + "%' AND Status LIKE 'Active'";

                String searchRoomType = "SELECT (RT.RoomTypeID) AS RoomTypeID, (RT.Title) AS RoomType, (SP.MondayPrice) AS MonPrice, (SP.TuesdayPrice) AS TuePrice, " +
                                    "(SP.WednesdayPrice) AS WedPrice, (SP.ThursdayPrice) ThuPrice, " +
                                    "(SP.FridayPrice)AS FriPrice, (SP.SaturdayPrice)AS SatPrice, (SP.SundayPrice)AS SunPrice " +
                                    "FROM RoomType RT, StandardRoomPrice SP " +
                                    "WHERE RT.Status LIKE 'Active' AND RT.RoomTypeID LIKE SP.RoomTypeID AND UPPER(RT.Title) LIKE '%" + roomType + "%' " +
                                    "ORDER BY RT.RoomTypeID DESC";

                SqlCommand cmdSearchRoomType = new SqlCommand(searchRoomType, conn);

                // Hold the data read from database
                SqlDataAdapter sda = new SqlDataAdapter(cmdSearchRoomType);

                DataTable dt = new DataTable();

                // Assign the data from database into dataTable
                sda.Fill(dt);

                // Bind data into repeater to display
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblNoItemFound.Visible = true;
                }
                else
                {
                    lblNoItemFound.Visible = false;
                }

                conn.Close();

                displayItemTotal("SELECT COUNT(*) " +
                        "FROM RoomType RT, StandardRoomPrice SP " +
                        "WHERE RT.Status LIKE 'Active' AND RT.RoomTypeID LIKE SP.RoomTypeID AND UPPER(RT.Title) LIKE '%" + roomType + "%' ");
            }
        }

        private void displayItemTotal(String query)
        {
            conn = new SqlConnection(strCon);
            conn.Open();

            SqlCommand cmdGetTotalItem = new SqlCommand(query, conn);

            int noOfItem = (int)cmdGetTotalItem.ExecuteScalar();

            conn.Close();

            lblItemDisplayed.Text = "1 - " + noOfItem.ToString();
            lblTotalNoOfItem.Text = noOfItem.ToString();
        }

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;

            roomTypeID = en.encryption(roomTypeID);

            // Redirect to edit page
            Response.Redirect("EditRegularPrice.aspx?ID=" + roomTypeID);
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // DIslay more option
            Panel panel = (Panel)e.Item.FindControl("TableOptionMenu");

            if (panel.Visible == false)
            {
                resetPanelVisibility();
                panel.Visible = true;

                var item = e.Item;
            }
            else
            {
                panel.Visible = false;
            }
        }

        private void resetPanelVisibility()
        {
            // Ensure only one panel(More option) is displayed
            for (int i = 0; i < 20; i++)
            {

                try
                {
                    Panel panel = Repeater1.Items[i].FindControl("TableOptionMenu") as Panel;

                    panel.Visible = false;


                }
                catch (Exception ex)
                {

                }

            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void LBRepeater_Click(object sender, EventArgs e)
        {
            IDEncryption en = new IDEncryption();

            // When user click on delete button in more option panel
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get RoomTypeID of the selected item
            String roomTypeID = (item.FindControl("lblRoomTypeID") as Label).Text;

            roomTypeID = en.encryption(roomTypeID);

            // Redirect to view page
            Response.Redirect("EditRegularPrice.aspx?ID=" + roomTypeID);
        }

        protected void IBMoreOption_Click(object sender, ImageClickEventArgs e)
        {
            // Execute when user click on "More Option" button
            ImageButton btn = (ImageButton)sender;

            RepeaterItem repeater = (RepeaterItem)btn.NamingContainer;


            Panel panel = (Panel)repeater.FindControl("TableOptionMenu");

            // Set margin-top for first item on repeater to 40px
            if (repeater.ItemIndex == 0)
            {
                panel.Style["margin-top"] = "90px";
            }
        }
    }
}