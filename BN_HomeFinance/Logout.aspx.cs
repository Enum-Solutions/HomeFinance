using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((BNUser)Session[Helper.Sessions.User.ToString()] != null)
            {
                BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                Session[Helper.Sessions.User.ToString()] = null;

                if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                    Response.Redirect("/Admin/" + Helper.pg_AdminLogin);
                else
                    Response.Redirect(Helper.pg_Login);
            }
        }
    }
}