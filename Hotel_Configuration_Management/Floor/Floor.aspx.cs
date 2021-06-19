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

namespace Hotel_Management_System.Hotel_Configuration_Management.Floor
{
    public partial class Floor : System.Web.UI.Page
    {

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

                PopupCover.Visible = false;

            }
        }

        private int getTotalNumberOfItem()
        {
            // Open connection
            conn = new SqlConnection(strCon);
            conn.Open();

            // SQL command 
            String getTotalNumebrOfItem = "SELECT COUNT(*) FROM Floor";

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

            String getName = "SELECT * FROM Floor ORDER BY FloorID OFFSET @offset ROWS FETCH NEXT @fetch ROWS ONLY";

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

            conn.Close();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Execute when user type in page number to display

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

        protected void LBEdit_Click(object sender, EventArgs e)
        {
            // When user click on edit button in more option panel

            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string PopupOpacity = (item.FindControl("Label3") as Label).Text;
        }

        protected void LBDelete_Click(object sender, EventArgs e)
        {
            // When user click on delete button in more option panel

            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            string name = (item.FindControl("Label3") as Label).Text;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Assign data from RepeaterView
            DataRowView drv = e.Item.DataItem as DataRowView;
            String comment = Convert.ToString(drv["Status"]);

            // Get control's reference
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblChangeStatus = e.Item.FindControl("lblChangeStatus") as Label;
            Image IMChangeStatus = e.Item.FindControl("IMChangeStatus") as Image;
            
            if (comment == "Active")
            {
                lblStatus.Style["color"] = "#00ce1b";  // Assign green color

                // Assign new options button to suspend
                lblChangeStatus.Text = "Suspend";
                lblChangeStatus.Style["color"] = "red";
                IMChangeStatus.ImageUrl = "~/Image/suspend_icon.png";

            }
            else
            {
                
                lblStatus.Style["color"] = "red";  // Assign red color
            }

        }

        protected void LBChangeStatus_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            // Get floorName for the selected item
            string floorName = (item.FindControl("lblFloorName") as Label).Text;
            String status = (item.FindControl("lblStatus") as Label).Text;
            String floorID = (item.FindControl("lblFloorID") as Label).Text;

            // Format popup window's content
            formatPopupWindow(floorName, status);
            PopupCover.Visible = true;
            popup.Visible = true;

            // Hold value for next postback
            ViewState["FloorID"] = floorID;
            ViewState["Status"] = status;
        }

        // To change the content of popup window dynamically
        private void formatPopupWindow(String floorName, String status)
        {
            lblPopupContent.Text = "<br />Floor Name: " + floorName + "<br /><br />";

            if(status == "Active")
            {
                lblPopupTitle.Text = "<br/>Suspend";
                lblPopupTitle.Style["color"] = "red";
                btnPopupActivate.Style["background-color"] = "red";
            }
            else
            {
                lblPopupTitle.Text = "<br/>Activate";
                lblPopupTitle.Style["color"] = "#00ce1b";
                btnPopupActivate.Style["background-color"] = "#00ce1b";
            }

            

        }

        protected void btnPopupActivate_Click(object sender, EventArgs e)
        {
            changeStatus();
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            PopupCover.Visible = false;
            popup.Visible = false;
        }

        private void changeStatus()
        {
            // Get FloorID and status stored inside ViewState
            String floorID = ViewState["FloorID"].ToString();
            String status = ViewState["Status"].ToString();

            conn = new SqlConnection(strCon);
            conn.Open();

            String updateStatus = "UPDATE Floor SET Status = @Status WHERE FloorID LIKE @ID";

            SqlCommand cmdUpdateStatus = new SqlCommand(updateStatus, conn);

            cmdUpdateStatus.Parameters.AddWithValue("@ID", floorID);

            if (status == "Active")
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Suspend");
            }
            else
            {
                cmdUpdateStatus.Parameters.AddWithValue("@Status", "Active");
            }

            int i = cmdUpdateStatus.ExecuteNonQuery();

            conn.Close();

        }
    }
}