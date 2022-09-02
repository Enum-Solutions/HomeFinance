using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class CreateStory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString[Helper.QueryStrings.StoryID.ToString()] != null)
                    {
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;

                        BNPost Story = new BNPost(Helper.Module.Stories);

                        Story.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.StoryID.ToString()]);

                        Story = Story.GetPost(Story);

                        txtHeader.Text = Story.Header;
                        txtDescription.Value = Story.Description;
                        ddlStatus.SelectedValue = Story.Status;
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

                    BNPost Story = new BNPost(Helper.Module.Stories);

                    Story.Header = txtHeader.Text;
                    Story.Description = txtDescription.Value;
                    Story.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                    Story.Status = ddlStatus.SelectedValue;

                    Story.Image.ImageData = null;

                    Story.InsertPost(Story);

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                        && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                        && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                        Response.Redirect("/Admin/" + Helper.pg_AllStories);
                    else
                        Response.Redirect(Helper.pg_StoriesTray);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session[Helper.Sessions.User.ToString()] != null)
            {
                BNPost Story = new BNPost(Helper.Module.Stories);

                Story.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.StoryID.ToString()]);

                if (Story.DeletePost(Story))
                {
                    Story.Header = txtHeader.Text;
                    Story.Description = txtDescription.Value;
                    Story.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                    Story.Status = ddlStatus.SelectedValue;

                    Story.Image.ImageData = null;

                    Story.InsertPost(Story);
                }
                if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                    Response.Redirect("/Admin/" + Helper.pg_AllStories);
                else
                    Response.Redirect(Helper.pg_StoriesTray);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BNPost Story = new BNPost(Helper.Module.Stories);

                Story.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.StoryID.ToString()]);

                if (Story.DeletePost(Story))
                {
                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                        Response.Redirect("/Admin/" + Helper.pg_AllStories);
                    else
                        Response.Redirect(Helper.pg_StoriesTray);
                }

                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This Story cant be deleted.')");
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == "Admin")
                Response.Redirect("/Admin/" + Helper.pg_AllStories);
            else
                Response.Redirect(Helper.pg_StoriesTray);
        }
    }
}