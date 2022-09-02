using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;
using System.Data;

namespace BN_HomeFinance.Admin
{
    public partial class Announcements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FUAnnouncementImage.Attributes["onchange"] = "UploadFile(this)";

                    if (Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()] != null)
                    {
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        imgAttachment.Visible = true;
                        btnDelete.Visible = true;

                        divAttachment.Style.Add("display", "none");

                        BNPost post = new BNPost(Helper.Module.Announcements);

                        post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                        PostColors colors = new PostColors();

                        colors.PostID = post.ID;
                        colors.Module = Helper.Module.Announcements.ToString();

                        colors = colors.GetPostColors(colors);

                        post = post.GetPost(post);

                        txtHeader.Text = post.Header;
                        txtDescription.Value = post.Description;
                        ddlStatus.SelectedValue = post.Status;
                        imgAttachment.ImageUrl = post.Image.ImageURL;

                        txtHeaderColor.Text = colors.HeaderColor;
                        txtDescriptionColor.Text = colors.DescriptionColor;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = post.Image.ImageName;
                        dr[BNImage_Constants.Col_ImageData] = post.Image.ImageData;

                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.Image.ToString()] = dt;

                        Session[Helper.Sessions.PreviewImageHome.ToString()] = post.Image.ImageURL;

                        txtHasImage.Text = "Yes";
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
                    if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                    {
                        DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            BNPost post = new BNPost(Helper.Module.Announcements);

                            PostColors colors = new PostColors();

                            post.Header = txtHeader.Text;
                            post.Description = txtDescription.Value;
                            post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                            post.Status = ddlStatus.SelectedValue;

                            post.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                            post.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                            colors.PostID = (post.InsertPost(post)).ID;

                            colors.HeaderColor = txtHeaderColor.Text;
                            colors.DescriptionColor = txtDescriptionColor.Text;
                            colors.Module = Helper.Module.Announcements.ToString();

                            colors.InsertPostColor(colors);

                            Session[Helper.Sessions.PreviewImageHome.ToString()] = null;
                        }
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Announcement Added Successfully');location.href='" + Helper.pg_AnnouncementTray + "'", true);
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
                if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                {
                    BNPost post = new BNPost(Helper.Module.Announcements);

                    PostColors colors = new PostColors();

                    post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                    colors.PostID = post.ID;
                    colors.Module = Helper.Module.Announcements.ToString();

                    if (post.DeletePost(post))
                    {
                        if (colors.DeletePostColor(colors))
                        {
                            DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                post.Header = txtHeader.Text;
                                post.Description = txtDescription.Value;
                                post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                                post.Status = ddlStatus.SelectedValue;

                                post.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                                post.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                                colors.PostID = (post.InsertPost(post)).ID;

                                colors.HeaderColor = txtHeaderColor.Text;
                                colors.DescriptionColor = txtDescriptionColor.Text;

                                colors.InsertPostColor(colors);

                                Session[Helper.Sessions.PreviewImageHome.ToString()] = null;
                            }
                        }
                    }
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Announcement Updated Successfully');location.href='" + Helper.pg_AnnouncementTray + "'", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.Announcements);

                PostColors colors = new PostColors();

                post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                colors.PostID = post.ID;
                colors.Module = Helper.Module.Announcements.ToString();

                Session[Helper.Sessions.PreviewImageHome.ToString()] = null;

                if (post.DeletePost(post))
                {
                    if (colors.DeletePostColor(colors))
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Announcement Deleted Successfully');location.href='" + Helper.pg_AnnouncementTray + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This announcement cant be deleted.')");
                }
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This announcement cant be deleted.')");
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session[Helper.Sessions.PreviewImageHome.ToString()] = null;
            Response.Redirect(Helper.pg_AnnouncementTray);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FUAnnouncementImage.HasFile)
                {
                    divAttachment.Style.Add("display", "none");
                    imgAttachment.Visible = true;

                    using (Stream stream = FUAnnouncementImage.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(stream))
                        {
                            byte[] ImageData = br.ReadBytes((Int32)stream.Length);
                            string rarBase64Data = Convert.ToBase64String(ImageData);
                            string ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);

                            Session[Helper.Sessions.PreviewImageHome.ToString()] = ImageURL;

                            imgAttachment.ImageUrl = ImageURL;

                            DataTable dt = new DataTable();

                            dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                            dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                            DataRow dr = dt.NewRow();

                            dr[BNImage_Constants.Col_ImageName] = FUAnnouncementImage.PostedFile.FileName;
                            dr[BNImage_Constants.Col_ImageData] = ImageData;
                            dt.Rows.Add(dr);

                            ViewState[Helper.ViewStates.Image.ToString()] = dt;

                            txtHasImage.Text = "Yes";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}