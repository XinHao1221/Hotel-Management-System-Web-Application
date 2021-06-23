using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Management_System.Hotel_Configuration_Management.Room_Type
{
    public partial class AddRoomType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void formBtnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void cbExtraBed_CheckedChanged(object sender, EventArgs e)
        {
            if(cbExtraBed.Checked == true)
            {
                RFVExtraBedPrice.Enabled = true;
                pnExtraBedPrice.Visible = true;
            }
            else
            {
                RFVExtraBedPrice.Enabled = false;
                pnExtraBedPrice.Visible = false;

            }
        }
    }
}