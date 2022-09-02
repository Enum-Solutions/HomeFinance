using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class Master_Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Helper.Sessions.User.ToString()] != null)
            {
                BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];
                
                if (!user.IsAdmin)
                {
                    Response.Redirect(Helper.pg_NoPermissions);
                }
            }
            else
            {
                Session[Helper.Sessions.Source.ToString()] = Request.Url.AbsoluteUri;
                Response.Redirect("/Admin/" + Helper.pg_AdminLogin);
            }
        }
    }
}