using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BN_HomeFinance.Admin
{
    public partial class CreateStakeHolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserTypes ut = new UserTypes();

                DataTable dtDDL = ut.GetAllUserTypesDataTable();

                if (dtDDL != null && dtDDL.Rows.Count > 0)
                {
                    ddlUserType.DataSource = dtDDL;
                    ddlUserType.DataTextField = User_Constants.Col_UserType;
                    ddlUserType.DataValueField = User_Constants.Col_UserTypeID;
                    ddlUserType.DataBind();
                    ddlUserType.Items.RemoveAt(ddlUserType.Items.Count - 1);

                    if (Request.QueryString[Helper.QueryStrings.UserID.ToString()] != null)
                    {
                        BNUser user = new BNUser();

                        user = user.GetUserWithUserID(Request.QueryString[Helper.QueryStrings.UserID.ToString()].ToString());

                        txtFullName.Text = user.FullName;
                        txtFullLoginName.Text = user.LoginName;
                        txtEmail.Text = user.Email;
                        txtCity.Text = user.City;
                        ddlCountry.SelectedValue = user.Country;
                        ddlUserType.SelectedValue = user.UserType.UserTypeID;
                        txtPin.Text = user.Pin;
                        txtAddress.Text = user.Address;
                        txtContact.Text = user.Contact;
                        ddlStatus.SelectedValue = user.Status;
                        txtAbout.Value = user.About;

                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;

                        lblPassword.Visible = false;
                        lblPasswordMandatory.Visible = false;
                        lblConfirmPassword.Visible = false;
                        lblConfirmPasswordMandatory.Visible = false;
                        txtFullPassword.Visible = false;
                        txtConfirmPassword.Visible = false;

                        txtFullLoginName.Enabled = false;

                        hplResetPassword.Visible = true;
                        hplResetPassword.NavigateUrl = Helper.pg_AdminResetPassword + "?" + Helper.QueryStrings.UserID.ToString() + "=" + user.UserID;

                        imgAttachment.Visible = true;

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

                        ViewState[Helper.ViewStates.User.ToString()] = user;
                    }
                    else
                    {
                        txtFullLoginName.Text = "";
                        txtFullPassword.Text = "";
                    }
                }
                else
                {

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
                UserLogs userLogs = new UserLogs();
                if (!user.CheckLoginExists(txtFullLoginName.Text))
                {
                    if (!user.CheckEmailExists(txtEmail.Text))
                    {
                        user.UserID = Guid.NewGuid().ToString();
                        user.FullName = txtFullName.Text;
                        user.LoginName = txtFullLoginName.Text;
                        user.Password = txtFullPassword.Text;
                        user.Email = txtEmail.Text;
                        user.City = txtCity.Text;
                        user.Country = ddlCountry.SelectedValue;
                        user.Pin = txtPin.Text;
                        user.UserType.UserTypeID = ddlUserType.SelectedValue;
                        user.Address = txtAddress.Text;
                        user.Contact = txtContact.Text;
                        user.Password = txtFullPassword.Text;
                        user.UserType.UserTypeID = ddlUserType.SelectedValue;
                        user.UserType.UserType = ddlUserType.SelectedItem.Text;
                        user.Status = ddlStatus.SelectedValue;
                        user.About = txtAbout.Value;

                        userLogs.UserID = user.UserID;
                        userLogs.UserName = user.LoginName;

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
                            userLogs.CreateUserLogs(userLogs);
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User Created Successfully');location.href='" + Helper.pg_AllUsers + "'", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('User Could not be Created.')");
                        }
                    }
                    else
                    {
                        if (txtEmail.Text != null)
                        {
                            lblError.Visible = true;
                            lblError.Text = "Email Address Already Exists. Please try another Email Address";
                        }

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
                if (ViewState[Helper.ViewStates.User.ToString()] != null)
                {
                    BNUser user = (BNUser)ViewState[Helper.ViewStates.User.ToString()];

                    if (user.DeleteUser(user.UserID))
                    {
                        if (!user.CheckLoginExists(txtFullLoginName.Text))
                        {
                            user.FullName = txtFullName.Text;
                            user.LoginName = txtFullLoginName.Text;
                            user.Email = txtEmail.Text;
                            user.City = txtCity.Text;
                            user.Country = ddlCountry.SelectedValue;
                            user.Pin = txtPin.Text;
                            user.UserType.UserTypeID = ddlUserType.SelectedValue;
                            user.Address = txtAddress.Text;
                            user.Contact = txtContact.Text;
                            user.UserType.UserTypeID = ddlUserType.SelectedValue;
                            user.UserType.UserType = ddlUserType.SelectedItem.Text;
                            user.Status = ddlStatus.SelectedValue;
                            user.About = txtAbout.Value;

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
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User Updated Successfully');location.href='" + Helper.pg_AllUsers + "'", true);
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
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState[Helper.ViewStates.User.ToString()] != null)
                {
                    BNUser user = (BNUser)ViewState[Helper.ViewStates.User.ToString()];

                    if (user.DeleteUser(user.UserID))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User Deleted Successfully');location.href='" + Helper.pg_AllUsers + "'", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('User Could not be deleted.')");
                }
            }
            catch (Exception ex)
            {

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

                txtFullPassword.Text = txtFullPassword.Text;
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

                txtFullPassword.Text = txtFullPassword.Text;
                txtConfirmPassword.Text = txtConfirmPassword.Text;
            }
            catch (Exception ex)
            {

            }
        }
    }
}