using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BN_HomeFinance.Resources;
using System.IO;

namespace BN_HomeFinance.Controls
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString[Helper.QueryStrings.EditMode.ToString()] != null &&
                    Request.QueryString[Helper.QueryStrings.EditMode.ToString()].ToString() == "1")
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        divAbout.Visible = true;
                        divAddress.Visible = true;
                        divContact.Visible = true;
                        divCountry.Visible = true;
                        divWilayat.Visible = true;

                        Heading.InnerText = "Edit Profile";

                        BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                        txtFullName.Text = user.FullName;
                        txtLoginName.Text = user.LoginName;
                        txtEmail.Text = user.Email;
                        txtCity.Text = user.City;
                        ddlCountry.SelectedValue = user.Country;
                        ViewState[Helper.ViewStates.ConsumerID.ToString()] = user.UserType.UserTypeID;
                        txtAddress.Text = user.Address;
                        txtContact.Text = user.Contact;
                        txtAbout.Value = user.About = HttpUtility.HtmlDecode(user.About);

                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;

                        lblPassword.Visible = false;
                        lblPasswordMandatory.Visible = false;
                        txtPassword.Visible = false;
                        lblConfirmPassword.Visible = false;
                        lblConfirmPasswordMandatory.Visible = false;
                        txtConfirmPassword.Visible = false;

                        txtLoginName.Enabled = false;

                        hplResetPassword.Visible = true;
                        hplResetPassword.NavigateUrl = Helper.pg_ResetPassword;

                        if (user.Picture.ImageData != null)
                        {
                            imgAttachment.ImageUrl = user.Picture.ImageURL;
                            divAttachment.Style.Add("display", "none");

                            DataTable dt = new DataTable();

                            dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                            dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                            DataRow dr = dt.NewRow();

                            dr[BNImage_Constants.Col_ImageName] = user.Picture.ImageName;
                            dr[BNImage_Constants.Col_ImageData] = user.Picture.ImageData;

                            dt.Rows.Add(dr);

                            ViewState[Helper.ViewStates.Image.ToString()] = dt;
                        }

                        if (user.BGPicture.ImageData != null)
                        {
                            imgAttachmentCover.ImageUrl = user.BGPicture.ImageURL;
                            divAttachmentCover.Style.Add("display", "none");

                            DataTable dt = new DataTable();

                            dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                            dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                            DataRow dr = dt.NewRow();

                            dr[BNImage_Constants.Col_ImageName] = user.Picture.ImageName;
                            dr[BNImage_Constants.Col_ImageData] = user.Picture.ImageData;

                            dt.Rows.Add(dr);

                            ViewState[Helper.ViewStates.CoverImage.ToString()] = dt;
                        }
                    }
                    else
                    {
                        Response.Redirect(Helper.pg_Login);
                    }
                }
                else
                {
                    ViewState[Helper.ViewStates.ConsumerID.ToString()] = (Helper.ExecuteSPWithNoParameters(User_Constants.Proc_GetConsumerTypeID)).Rows[0][User_Constants.Col_UserTypeID].ToString();
                }
            }
            else
            {
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BNUser user = new BNUser();

                if (!user.CheckLoginExists(txtLoginName.Text))
                {
                    if (!user.CheckEmailExists(txtEmail.Text))
                    {
                        lblError.Visible = false;

                        Guid guid = Guid.NewGuid();

                        user.UserID = guid.ToString();
                        user.FullName = txtFullName.Text;
                        user.LoginName = txtLoginName.Text;
                        user.Password = txtPassword.Text;
                        user.Email = txtEmail.Text;
                        user.City = txtCity.Text;
                        user.Country = ddlCountry.SelectedValue;
                        user.Pin = "";
                        user.UserType.UserTypeID = (string)ViewState[Helper.ViewStates.ConsumerID.ToString()];
                        user.Address = txtAddress.Text;
                        user.Contact = txtContact.Text;
                        user.Password = txtPassword.Text;
                        user.Status = User_Constants.UserStatus.Active.ToString();
                        user.About = HttpUtility.HtmlEncode(txtAbout.Value);

                        if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                        {
                            DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                                user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                            }
                        }

                        if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                        {
                            DataTable dt = (DataTable)ViewState[Helper.ViewStates.CoverImage.ToString()];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                                user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                            }
                        }

                        if (user.CreateUser(user))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Signup Successfull');location.href='" + Helper.pg_Login + "'", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('User Could not be created.')");
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Email Address Already Exists. Please try another Email Address";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Login Name Already Exists. Please try another Login Name";
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
                    BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                    if (user.DeleteUser(user.UserID))
                    {
                        if (!user.CheckLoginExists(txtLoginName.Text))
                        {
                            lblError.Visible = false;
                            user.FullName = txtFullName.Text;
                            user.LoginName = txtLoginName.Text;
                            user.Email = txtEmail.Text;
                            user.City = txtCity.Text;
                            if (ddlCountry.SelectedValue != "-1")
                                user.Country = ddlCountry.SelectedItem.Text;
                            else
                                user.Country = "";
                            user.UserType.UserTypeID = (string)ViewState[Helper.ViewStates.ConsumerID.ToString()];
                            user.Address = txtAddress.Text;
                            user.Contact = txtContact.Text;
                            user.Status = User_Constants.UserStatus.Active.ToString();
                            user.About = HttpUtility.HtmlEncode(txtAbout.Value);

                            if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                            {
                                DataTable dt = (DataTable)ViewState[Helper.ViewStates.Image.ToString()];

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    user.Picture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                                    user.Picture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                                }
                            }

                            if (ViewState[Helper.ViewStates.CoverImage.ToString()] != null)
                            {
                                DataTable dt = (DataTable)ViewState[Helper.ViewStates.CoverImage.ToString()];

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    user.BGPicture.ImageData = (byte[])dt.Rows[0][BNImage_Constants.Col_ImageData];
                                    user.BGPicture.ImageName = dt.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                                }
                            }

                            if (user.CreateUser(user))
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profile Updated Successfully');location.href='" + Helper.pg_UserProfile + "'", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('User Could not be updated.')");
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Login Name Already Exists. Please try another Login Name";
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('User Could not be updated.')");
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Helper.QueryStrings.EditMode.ToString()] != null &&
                    Request.QueryString[Helper.QueryStrings.EditMode.ToString()].ToString() == "1")
            {
                Response.Redirect(Helper.pg_Index);
            }
            else
            {
                Response.Redirect(Helper.pg_Login);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                divAttachment.Style.Add("display", "none");
                imgAttachment.Visible = true;

                using (Stream stream = fuImage.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        byte[] ImageData = br.ReadBytes((Int32)stream.Length);
                        string rarBase64Data = Convert.ToBase64String(ImageData);
                        string ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);

                        imgAttachment.ImageUrl = ImageURL;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = fuImage.PostedFile.FileName;
                        dr[BNImage_Constants.Col_ImageData] = ImageData;
                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.Image.ToString()] = dt;
                    }
                }

                txtPassword.Text = txtPassword.Text;
                txtConfirmPassword.Text = txtConfirmPassword.Text;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUploadCover_Click(object sender, EventArgs e)
        {
            try
            {
                divAttachmentCover.Style.Add("display", "none");

                using (Stream stream = fuImageCover.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        byte[] ImageData = br.ReadBytes((Int32)stream.Length);
                        string rarBase64Data = Convert.ToBase64String(ImageData);
                        string ImageURL = string.Format("data:image/png;base64,{0}", rarBase64Data);

                        imgAttachmentCover.ImageUrl = ImageURL;

                        DataTable dt = new DataTable();

                        dt.Columns.Add(BNImage_Constants.Col_ImageName, typeof(string));
                        dt.Columns.Add(BNImage_Constants.Col_ImageData, typeof(byte[]));

                        DataRow dr = dt.NewRow();

                        dr[BNImage_Constants.Col_ImageName] = fuImageCover.PostedFile.FileName;
                        dr[BNImage_Constants.Col_ImageData] = ImageData;
                        dt.Rows.Add(dr);

                        ViewState[Helper.ViewStates.CoverImage.ToString()] = dt;
                    }
                }

                txtPassword.Text = txtPassword.Text;
                txtConfirmPassword.Text = txtConfirmPassword.Text;
            }
            catch (Exception ex)
            {

            }
        }
    }
}