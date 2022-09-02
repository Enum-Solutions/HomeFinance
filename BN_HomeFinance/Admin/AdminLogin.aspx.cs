using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BNUser user = new BNUser();
                UserLogs userLogs = new UserLogs();

                if (user.ValidateCredentials(txtLoginName.Text, txtPassword.Text))
                {
                    user = user.GetUser(txtLoginName.Text);

                    Session[Helper.Sessions.User.ToString()] = user;
                    userLogs.UserName = user.LoginName;
                    if (Session[Helper.Sessions.Source.ToString()] != null)
                    {
                        Response.Redirect((string)Session[Helper.Sessions.Source.ToString()]);
                    }
                    else
                    {
                        userLogs.UpdateUserLogs(userLogs);
                        Response.Redirect(Helper.pg_Dashboard);
                    }
                        
                }
                else
                {
                    divError.Visible = true;
                    lblError.Text = "User Name or Password is Incorrect";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_ForgotPassword);
        }

        protected void lnkSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Signup);
        }

    }
}