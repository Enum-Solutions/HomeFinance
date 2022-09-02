using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Helper.Sessions.User.ToString()] == null)
                    Response.Redirect(Helper.pg_Login);
                else
                    HDLogin.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).LoginName;
            }
            else
            {
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    BNUser user = new BNUser();

                    user = (BNUser)Session[Helper.Sessions.User.ToString()];

                    if (txtOldPassword.Text == user.Password)
                    {
                        if (txtPassword.Text == txtConfirmPassword.Text)
                        {
                            if (user.Email != "")
                            {
                                Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_UpdatePassword, User_Constants.Par_Email, user.Email, User_Constants.Par_Password, Helper.EncryptDecrypt(Helper.Encryption.Encrypt.ToString(), txtPassword.Text));

                                Session[Helper.Sessions.User.ToString()] = null;
                                Response.Redirect(Helper.pg_Login);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password cannot be updated as the email address could not be found for the user.')");
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password cannot be updated as Password and Confirm Password is not same.')");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incorrect Old Password.Please type correct password to reset your password.')");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Index);
        }
    }
}