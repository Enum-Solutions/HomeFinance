using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Admin
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString[Helper.QueryStrings.MessageID.ToString()] != null)
                    {
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;

                        BNPost post = new BNPost(Helper.Module.Messages);

                        post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.MessageID.ToString()]);

                        post = post.GetPost(post);

                        txtHeader.Text = post.Header;
                        txtDescription.Value = HttpUtility.HtmlDecode(post.Description);
                        ddlStatus.SelectedValue = post.Status;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    BNPost post = new BNPost(Helper.Module.Messages);

                    post.Header = txtHeader.Text;
                    post.Description = txtDescription.Value;
                    post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                    post.Status = ddlStatus.SelectedValue;

                    post.Image.ImageData = null;

                    post.InsertPost(post);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Message Added Successfully');location.href='" + Helper.pg_MessageTray + "'", true);
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
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    BNPost post = new BNPost(Helper.Module.Messages);

                    post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.MessageID.ToString()]);

                    if (post.DeletePost(post))
                    {
                        post.Header = txtHeader.Text;
                        post.Description = HttpUtility.HtmlEncode(txtDescription.Value);
                        post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                        post.Status = ddlStatus.SelectedValue;

                        post.Image.ImageData = null;

                        post.InsertPost(post);
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Message Updated Successfully');location.href='" + Helper.pg_MessageTray + "'", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_MessageTray);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.Messages);

                post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.MessageID.ToString()]);

                if (post.DeletePost(post))
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Message Deleted Successfully');location.href='" + Helper.pg_MessageTray + "'", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This Message can't be deleted.')");
            }
            catch (Exception ex)
            {

            }
        }
    }
}