using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotel_Management_System.Utility;

namespace Hotel_Management_System.Hotel_Configuration_Management.Floor
{

    public partial class ViewFloor : System.Web.UI.Page
    {
        IDEncryption en = new IDEncryption();

        protected void Page_Load(object sender, EventArgs e)
        {
            String id = en.decryption(Request.QueryString["ID"]);

            Label1.Text = id;
        }
    }
}