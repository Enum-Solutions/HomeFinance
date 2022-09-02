using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Admin
{
    public partial class AdminResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString[Helper.QueryStrings.UserID.ToString()] != null)
                    {
                        BNUser user = new BNUser();

                        user = user.GetUserWithUserID(Request.QueryString[Helper.QueryStrings.UserID.ToString()].ToString());

                        if (user != null)
                        {
                            dvError.Visible = false;

                            header.InnerText = "Reset Password for " + user.FullName;
                            HDLogin.Value = user.LoginName;

                            ViewState[Helper.ViewStates.User.ToString()] = user;
                        }
                        else
                        {
                            dvError.Visible = true;
                            txtPassword.Visible = false;
                            txtConfirmPassword.Visible = false;
                            btnUpdate.Visible = false;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[Helper.ViewStates.User.ToString()] != null)
                {
                    BNUser user = (BNUser)ViewState[Helper.ViewStates.User.ToString()];

                    dvError.Visible = false;

                    if (txtPassword.Text != "" && txtConfirmPassword.Text != "")
                    {
                        if (txtPassword.Text == txtConfirmPassword.Text)
                        {
                            if (user.Email != "")
                            {
                                Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_UpdatePassword, User_Constants.Par_Email, user.Email, User_Constants.Par_Password, Helper.EncryptDecrypt(Helper.Encryption.Encrypt.ToString(), txtPassword.Text));

                                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Password has been resetted successfully.')");

                                Response.Redirect(Helper.pg_CreateStakeHolder + "?" + Helper.QueryStrings.UserID.ToString() + "=" + user.UserID);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password cannot be updated as the email address could not be found for the user.')");
                            }
                        }
                    }
                    else
                        dvError.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/" + Helper.pg_AllUsers);
        }
    }
}