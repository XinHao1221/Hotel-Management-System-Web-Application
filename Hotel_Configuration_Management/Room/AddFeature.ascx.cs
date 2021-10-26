using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Hotel_Configuration_Management.Room
{
    public partial class AddFeature : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["FeatureList"] = new List<String>();

                PopupCover.Visible = false;
                PopupDelete.Visible = false;
            }

            checkIsEmpty();
        }

        private void checkIsEmpty()
        {
            List<String> featureList = (List<String>)Session["FeatureList"];

            if (featureList.Count == 0)
            {
                lblNoItemFound.Visible = true;
            }
            else
            {
                lblNoItemFound.Visible = false;
            }
        }

        protected void btnSaveFeature_Click(object sender, EventArgs e)
        {
            List<String> featureList = (List<String>)Session["FeatureList"];

            // Add to feature list
            featureList.Add(txtFeature.Text);

            Repeater1.DataSource = featureList;
            Repeater1.DataBind();

            lblNoItemFound.Visible = false;

            txtFeature.Text = null;
        }


        protected void IBDeleteFeature_Click(object sender, ImageClickEventArgs e)
        {
            // When user click on delete icon
            RepeaterItem item = (sender as ImageButton).NamingContainer as RepeaterItem;

            // Get item no for the selected item
            String itemIndex = (item.FindControl("lblNumber") as Label).Text;
            String feature = (item.FindControl("lblFeature") as Label).Text;

            lblPopupDeleteContent.Text = "Feature: " + feature + "<br /><br />";

            ViewState["ItemIndex"] = itemIndex;

            PopupDelete.Visible = true;
            PopupCover.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PopupDelete.Visible = false;
            PopupCover.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int itemIndex = int.Parse(ViewState["ItemIndex"].ToString());

            List<String> featuretList = (List<String>)Session["FeatureList"];

            featuretList.RemoveAt(itemIndex - 1);

            Repeater1.DataSource = featuretList;
            Repeater1.DataBind();

            checkIsEmpty();

            PopupCover.Visible = false;
            PopupDelete.Visible = false;
        }

        protected void btnDeleteFeature_Click(object sender, EventArgs e)
        {

        }
    }
}