using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class VendorAnnouncement : System.Web.UI.Page
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

                        BNPost post = new BNPost(Helper.Module.VendorAnnouncements);
                        post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                        post = post.GetPost(post);

                        txtHeader.Text = post.Header;
                        ddlStatus.SelectedValue = post.Status;
                        imgAttachment.ImageUrl = post.Image.ImageURL;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = post.Image.ImageName;
                        dr[BNImage_Constants.Col_ImageData] = post.Image.ImageData;

                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.Image.ToString()] = dt;

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
                            BNPost post = new BNPost(Helper.Module.VendorAnnouncements);

                            post.Header = txtHeader.Text;
                            post.Description = "";
                            post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                            post.Status = ddlStatus.SelectedValue;

                            post.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                            post.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                            post.InsertPost(post);

                            Session[Helper.Sessions.PreviewImageVendor.ToString()] = null;
                        }
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vendor Announcement Added Successfully');location.href='" + Helper.pg_AllVendorAnnouncements + "'", true);
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
                    BNPost post = new BNPost(Helper.Module.VendorAnnouncements);

                    post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                    if (post.DeletePost(post))
                    {
                        DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            post.Header = txtHeader.Text;
                            post.Creator = (BNUser)Session[Helper.Sessions.User.ToString()];
                            post.Description = "";
                            post.Status = ddlStatus.SelectedValue;

                            post.Image.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                            post.Image.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();

                            post.InsertPost(post);

                            Session[Helper.Sessions.PreviewImageVendor.ToString()] = null;
                        }
                    }
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vendor Announcement Updated Successfully');location.href='" + Helper.pg_AllVendorAnnouncements + "'", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.VendorAnnouncements);

                post.ID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.AnnouncementID.ToString()]);

                Session[Helper.Sessions.PreviewImageVendor.ToString()] = null;

                if (post.DeletePost(post))
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vendor Announcement Deleted Successfully');location.href='" + Helper.pg_AllVendorAnnouncements + "'", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('This announcement cant be deleted.')");
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session[Helper.Sessions.PreviewImageVendor.ToString()] = null;

            Response.Redirect(Helper.pg_AllVendorAnnouncements);
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

                            Session[Helper.Sessions.PreviewImageVendor.ToString()] = ImageURL;

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