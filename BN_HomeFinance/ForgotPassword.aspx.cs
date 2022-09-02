using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Controls
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[Helper.QueryStrings.Key.ToString()] != null && Request.QueryString[Helper.QueryStrings.Email.ToString()] != null)
                {
                    BNUser user = new BNUser();

                    if (user.ValidateKey(Request.QueryString[Helper.QueryStrings.Email.ToString()].ToString(),
                        Request.QueryString[Helper.QueryStrings.Key.ToString()].ToString()))
                    {
                        user = user.GetUserWithEmail(Request.QueryString[Helper.QueryStrings.Email.ToString()].ToString());

                        lblLoginLabel.Visible = true;
                        lblLoginName.Visible = true;
                        lblLoginName.Text = user.LoginName;

                        lblPassword.Visible = true;
                        txtPassword.Visible = true;

                        lblConfirmPassword.Visible = true;
                        txtConfirmPassword.Visible = true;

                        lblMandatoryPassword.Visible = true;
                        lblMandatoryConfirmPassword.Visible = true;

                        lblEmailAddres.Visible = false;
                        txtEmail.Visible = false;

                        btnProceed.Visible = false;

                        btnReset.Visible = true;

                        header.InnerText = "Reset Password";
                    }
                }
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
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_UpdatePassword, User_Constants.Par_Email, Request.QueryString[Helper.QueryStrings.Email.ToString()].ToString(), User_Constants.Par_Password, Helper.EncryptDecrypt(Helper.Encryption.Encrypt.ToString(), txtPassword.Text));
                    Helper.ExecuteSPScalarWithParameters(User_Constants.Proc_DeleteKey, User_Constants.Par_Email, Request.QueryString[Helper.QueryStrings.Email.ToString()].ToString());

                    Response.Redirect(Helper.pg_Login);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                BNUser user = new BNUser();

                if (user.ResetPasswordApply(txtEmail.Text))
                {
                    btnProceed.Visible = false;
                    txtEmail.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert(Email Address Does not Exists.)");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}