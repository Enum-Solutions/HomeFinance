using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Controls
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState[Helper.ViewStates.Attempts.ToString()] = 0;
            else
                return;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BNUser user = new BNUser();
                UserLogs userLogs = new UserLogs();

                if (txtLoginName.Text.ToLower() != "admin")
                {
                    user = user.GetUserWithUserLogin(txtLoginName.Text);

                    if (user.HasUser)
                    {
                        if (user.Status != User_Constants.UserStatus.Locked.ToString())
                        {
                            if (user.ValidateCredentials(txtLoginName.Text, txtPassword.Text))
                            {
                                user = user.GetUser(txtLoginName.Text);

                                Session[Helper.Sessions.User.ToString()] = user;
                                userLogs.UserName = user.LoginName;
                                if (Session[Helper.Sessions.Source.ToString()] != null)
                                {
                                    string URI = (string)Session[Helper.Sessions.Source.ToString()].ToString();
                                    Session[Helper.Sessions.Source.ToString()] = null;
                                    Response.Redirect(URI);
                                }
                                else
                                {
                                    if (user.UserType.UserType == User_Constants.UserType.Builder.ToString() || user.UserType.UserType == User_Constants.UserType.Vendor.ToString())
                                    {
                                        userLogs.UpdateUserLogs(userLogs);
                                        Response.Redirect(Helper.pg_UserProfile);
                                    } 
                                    else
                                        Response.Redirect(Helper.pg_Index);
                                }
                            }
                            else
                            {
                                if (ViewState[Helper.ViewStates.Attempts.ToString()] != null)
                                {
                                    int attempts = (int)ViewState[Helper.ViewStates.Attempts.ToString()];

                                    if (attempts < 3)
                                    {
                                        ViewState[Helper.ViewStates.Attempts.ToString()] = (attempts + 1);
                                        divError.Visible = true;
                                        lblError.Text = "User Name or Password is Incorrect";
                                    }
                                    else
                                    {
                                        if (user.UpdateUserStatus(txtLoginName.Text, User_Constants.UserStatus.Locked.ToString()))
                                        {
                                            divError.Visible = true;
                                            lblError.Text = "User Locked. Please Contact System Administrator to Unlock the account.";
                                        }
                                        else
                                        {
                                            divError.Visible = true;
                                            lblError.Text = "Cannot Attempt to login to the server. Please try again later.";
                                        }
                                    }
                                }
                                else
                                {
                                    divError.Visible = true;
                                    lblError.Text = "Cannot Attempt to login to the server. Please try again later.";
                                }
                            }
                        }
                        else
                        {
                            divError.Visible = true;
                            lblError.Text = "User Locked. Please Contact System Administrator to Unlock the account.";
                        }
                    }
                    else
                    {
                        divError.Visible = true;
                        lblError.Text = "User name does not exists.";
                    }
                }
                else
                {
                    divError.Visible = true;
                    lblError.Text = "User does not exists";
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

        protected void btnGoogle_Click(object sender, EventArgs e)
        {
            string clientid = ConfigurationManager.AppSettings[Helper.Keys.ClientID.ToString()].ToString();
            //your client secret  
            string clientsecret = ConfigurationManager.AppSettings[Helper.Keys.ClientSecret.ToString()].ToString();
            //your redirection url  
            string redirection_url = ConfigurationManager.AppSettings[Helper.Keys.SiteURL.ToString()].ToString();

            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=" + clientid + "";

            Response.Redirect(url);
        }
    }
}