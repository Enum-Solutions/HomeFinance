using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BN_HomeFinance.Resources;
using System.Web.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace BN_HomeFinance
{
    public partial class CreateProperty : System.Web.UI.Page
    {
        DataSet ds = new DataSet();

        public string HTMLImages = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    Properties objProperty = new Properties();

                    BNUser user = ((BNUser)Session[Helper.Sessions.User.ToString()]);

                    ddlUser.DataSource = Helper.GetData(User_Constants.Proc_GetActiveVendorBuilders, new SqlParameter(User_Constants.Par_UserType, User_Constants.UserType.Builder.ToString()));
                    ddlUser.DataBind();
                    ddlUser.Items.Insert(0, new ListItem("Choose Builder ", "0"));

                    ddlGovernorate.DataSource = Helper.GetData(GovernorateConstants.Proc_GetGovernorate, null);
                    ddlGovernorate.DataBind();
                    ddlGovernorate.Items.Insert(0, new ListItem(" Choose Governorate ", "0"));

                    ddlWilayat.DataSource = Helper.GetData(WilayatConstants.proc_GetAllWilayats, null);
                    ddlWilayat.DataBind();
                    ddlWilayat.Items.Insert(0, new ListItem(" Choose Wilayat ", "0"));

                    ddlPropertyType.DataSource = Helper.GetData(PropertyTypeConstants.proc_GetPropertyType, null);
                    ddlPropertyType.DataBind();
                    ddlPropertyType.Items.Insert(0, new ListItem(" Choose Property Type", "0"));
                    ddlPropertyType.Items.RemoveAt(ddlPropertyType.Items.Count - 1);

                    chkboxAmenities.DataSource = Helper.GetData(AmenityConstants.proc_GetAmenities, null);
                    chkboxAmenities.DataBind();

                    ddlWilayat.Enabled = false;

                    if (Request.QueryString[Helper.QueryStrings.PropertyID.ToString()] == null)
                    {
                        btnDeleteProperty.Visible = false;
                        btnUpdateProperty.Visible = false;

                        if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        {
                            divUsers.Visible = true;
                        }
                        else
                        {
                            ddlUser.SelectedValue = user.UserID;
                        }
                    }
                    else
                    {
                        objProperty = objProperty.GetPropertyByPropertyID(Request.QueryString[Helper.QueryStrings.PropertyID.ToString()].ToString());

                        txtPropertyName.Text = objProperty.PropertyName;
                        ddlPropertyType.SelectedIndex = objProperty.PropertyType.PropertyTypeID;
                        txtPropertyAddress.Text = objProperty.PropertyAddress;
                        txtPropertyDesc.InnerText = HttpUtility.HtmlDecode(objProperty.PropertyDescription);
                        txtArea.Text = objProperty.PropertyAreaInMsq;
                        txtGeoLocation.Text = objProperty.PropertyGeoLocation;
                        txtBedroom.Text = Convert.ToString(objProperty.PropertyBedrooms);
                        txtBathroom.Text = Convert.ToString(objProperty.PropertyBathrooms);
                        txtGarage.Text = Convert.ToString(objProperty.PropertyGarage);
                        txtPropertyValue.Text = Convert.ToString(objProperty.PropertyValue);
                        ddlGovernorate.SelectedValue = objProperty.PropertyGovernorate.GovernorateID.ToString();
                        ddlUser.SelectedValue = objProperty.Creator.UserID;

                        if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        {
                            divUsers.Visible = true;
                        }

                        //PopulateWilayat();

                        ddlWilayat.SelectedValue = objProperty.PropertyWilayat.WilayatID.ToString();

                        if (objProperty.Amenity.AmenityID != "")
                            objProperty.Amenities = objProperty.Amenities.GetAmenities(objProperty.Amenity.AmenityID);

                        if (objProperty.Amenities != null && objProperty.Amenities.Count > 0)
                        {
                            for (int i = 0; i < objProperty.Amenities.Count; i++)
                            {
                                for (int k = 0; k < chkboxAmenities.Items.Count; k++)
                                {
                                    if (objProperty.Amenities.Amenity[i].AmenityID == chkboxAmenities.Items[k].Value)
                                    {
                                        chkboxAmenities.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (objProperty.Images.Count > 0)
                        {
                            for (int i = 0; i < objProperty.Images.Count; i++)
                                HTMLImages += @"<div class='col-4 col-sm-3 col-md-3 col-lg-2 mt-2 t-c'><img class='prop-img w-100' src= '" + objProperty.Images.BNImages[i].ImageURL + "'></div>";

                            ViewState[Helper.ViewStates.Image.ToString()] = objProperty.Images;
                        }

                        AddPropertyBtn.Visible = false;

                        if (user.UserID == objProperty.Creator.UserID)
                        {
                            btnDeleteProperty.Visible = true;
                            btnUpdateProperty.Visible = true;
                        }
                    }
                }
                else
                {
                    Session[Helper.Sessions.Source.ToString()] = Helper.pg_CreateProperty;
                    Response.Redirect(Helper.pg_Login);
                }
            }
            else
                return;
        }

        protected void AddPropertyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    BNUser user = ((BNUser)Session[Helper.Sessions.User.ToString()]);

                    Properties objProperty = new Properties();

                    if (fileUploadPropertyImages.HasFiles)
                    {
                        for (int i = 0; i < fileUploadPropertyImages.PostedFiles.Count; i++)
                        {
                            BNImage img = new BNImage();

                            using (Stream str = fileUploadPropertyImages.PostedFiles[i].InputStream)
                            {
                                using (BinaryReader rdr = new BinaryReader(str))
                                {
                                    img.ImageData = rdr.ReadBytes((Int32)str.Length);
                                    img.ImageName = fileUploadPropertyImages.PostedFiles[i].FileName;
                                }
                            }

                            objProperty.Images.BNImages.Add(img);
                            objProperty.Images.Count++;
                        }

                        objProperty.Images = objProperty.Images.SaveImages(objProperty.Images);
                    }

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                    {
                        objProperty.Creator.UserID = ddlUser.SelectedValue;
                    }
                    else
                    {
                        objProperty.Creator.UserID = user.UserID;
                    }

                    objProperty.PropertyName = txtPropertyName.Text;
                    objProperty.PropertyType.PropertyTypeID = Convert.ToInt16(ddlPropertyType.SelectedValue);
                    objProperty.PropertyAddress = txtPropertyAddress.Text;
                    objProperty.PropertyDescription = HttpUtility.HtmlEncode(txtPropertyDesc.InnerText);
                    objProperty.PropertyAreaInMsq = txtArea.Text;
                    objProperty.PropertyGovernorate.GovernorateID = ddlGovernorate.SelectedIndex;
                    objProperty.ConcatinatedIDs = BNImage_Factory.ConcatenateIDS(objProperty.Images);
                    objProperty.PropertyGeoLocation = Convert.ToString(txtGeoLocation.Text);
                    if (txtBedroom.Text != "")
                        objProperty.PropertyBedrooms = int.Parse(txtBedroom.Text);
                    else
                        objProperty.PropertyBedrooms = 0;
                    if (txtBathroom.Text != "")
                        objProperty.PropertyBathrooms = int.Parse(txtBathroom.Text);
                    else
                        objProperty.PropertyBathrooms = 0;
                    if (txtGarage.Text != "")
                        objProperty.PropertyGarage = Convert.ToInt32(txtGarage.Text);
                    else
                        objProperty.PropertyGarage = 0;
                    if (txtPropertyValue.Text != "")
                        objProperty.PropertyValue = Convert.ToInt32(txtPropertyValue.Text);
                    else
                        objProperty.PropertyValue = 0;
                    objProperty.PropertyWilayat.WilayatID = Convert.ToInt32(ddlWilayat.SelectedValue);

                    foreach (ListItem ListItem in chkboxAmenities.Items)
                    {
                        if (ListItem.Selected)
                        {
                            if (chkboxAmenities.Items.Count == 0)
                                objProperty.Amenity.AmenityID += ListItem.Value;
                            else
                                objProperty.Amenity.AmenityID += ListItem.Value + ",";
                        }
                    }

                    if (objProperty.Amenity.AmenityID != null && objProperty.Amenity.AmenityID.Contains(","))
                        objProperty.Amenity.AmenityID = objProperty.Amenity.AmenityID.TrimEnd(',');
                    else
                        objProperty.Amenity.AmenityID = "";

                    objProperty.CreateProperty(objProperty);

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Added Successfully');location.href='/Admin/" + Helper.pg_AllPropertiesAdmin + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Added Successfully');location.href='" + Helper.pg_MyProperties + "'", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdateProperty_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[Helper.QueryStrings.PropertyID.ToString()] != null)
                {
                    Properties objProperty = new Properties();

                    if (objProperty.DeletePropertyByPropertyID(Request.QueryString[Helper.QueryStrings.PropertyID.ToString()].ToString()))
                    {
                        if (Session[Helper.Sessions.User.ToString()] != null)
                        {
                            BNUser user = ((BNUser)Session[Helper.Sessions.User.ToString()]);

                            if (fileUploadPropertyImages.HasFiles)
                            {
                                for (int i = 0; i < fileUploadPropertyImages.PostedFiles.Count; i++)
                                {
                                    BNImage img = new BNImage();

                                    using (Stream str = fileUploadPropertyImages.PostedFiles[i].InputStream)
                                    {
                                        using (BinaryReader rdr = new BinaryReader(str))
                                        {
                                            img.ImageData = rdr.ReadBytes((Int32)str.Length);
                                            img.ImageName = fileUploadPropertyImages.PostedFiles[i].FileName;
                                        }
                                    }

                                    objProperty.Images.BNImages.Add(img);
                                    objProperty.Images.Count++;
                                }

                                objProperty.Images = objProperty.Images.SaveImages(objProperty.Images);
                            }
                            else
                            {
                                if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                                {
                                    objProperty.Images = (BNImageCollection)ViewState[Helper.ViewStates.Image.ToString()];
                                    objProperty.Images = objProperty.Images.SaveImages(objProperty.Images);
                                }
                            }

                            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                            {
                                objProperty.Creator.UserID = ddlUser.SelectedValue;
                            }
                            else
                            {
                                objProperty.Creator.UserID = user.UserID;
                            }

                            objProperty.PropertyName = txtPropertyName.Text;
                            objProperty.PropertyType.PropertyTypeID = ddlPropertyType.SelectedIndex;
                            objProperty.PropertyAddress = txtPropertyAddress.Text;
                            objProperty.PropertyDescription = HttpUtility.HtmlEncode(txtPropertyDesc.InnerText);
                            objProperty.PropertyAreaInMsq = txtArea.Text;
                            objProperty.PropertyGovernorate.GovernorateID = ddlGovernorate.SelectedIndex;
                            objProperty.ConcatinatedIDs = BNImage_Factory.ConcatenateIDS(objProperty.Images);
                            objProperty.PropertyGeoLocation = Convert.ToString(txtGeoLocation.Text);
                            objProperty.PropertyBedrooms = int.Parse(txtBedroom.Text);
                            objProperty.PropertyBathrooms = int.Parse(txtBathroom.Text);
                            objProperty.PropertyGarage = Convert.ToInt32(txtGarage.Text);
                            objProperty.PropertyValue = Convert.ToInt32(txtPropertyValue.Text);
                            objProperty.PropertyWilayat.WilayatID = Convert.ToInt32(ddlWilayat.SelectedValue);

                            foreach (ListItem ListItem in chkboxAmenities.Items)
                            {
                                if (ListItem.Selected)
                                {
                                    if (chkboxAmenities.Items.Count == 0)
                                        objProperty.Amenity.AmenityID += ListItem.Value;
                                    else
                                        objProperty.Amenity.AmenityID += ListItem.Value + ",";
                                }
                            }
                            if (objProperty.Amenity.AmenityID != null && objProperty.Amenity.AmenityID.Contains(","))
                                objProperty.Amenity.AmenityID = objProperty.Amenity.AmenityID.TrimEnd(',');
                            else
                                objProperty.Amenity.AmenityID = "";

                            objProperty.CreateProperty(objProperty);

                            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                                   && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                                   && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Updated Successfully');location.href='/Admin/" + Helper.pg_AllPropertiesAdmin + "'", true);
                            else
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Updated Successfully');location.href='" + Helper.pg_MyProperties + "'", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //protected void ddlGovernorate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlWilayat.Items.Insert(0, new ListItem(" Choose Wilayat ", "0"));
        //    PopulateWilayat();
        //}

        protected void btnViewProperty_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllProperties.aspx");
        }

        protected void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            try
            {
                Properties objProperty = new Properties();

                if (objProperty.DeletePropertyByPropertyID(Request.QueryString[Helper.QueryStrings.PropertyID.ToString()].ToString()))
                {
                    Response.Write("<script>alert('Property deleted successfully')</script>");

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Deleted Successfully');location.href='/Admin/" + Helper.pg_AllPropertiesAdmin + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Deleted Successfully');location.href='" + Helper.pg_MyProperties + "'", true);
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Property could not be deleted')</script>");
            }

        }

        //protected void PopulateWilayat()
        //{
        //    ds.Clear();
        //    if (ddlGovernorate.SelectedIndex == 0)
        //    {
        //        ddlWilayat.Enabled = false;
        //    }
        //    else
        //    {
        //        ddlWilayat.Enabled = true;
        //        SqlParameter governorateIDParamForWilayat = new SqlParameter("@governorateID", ddlGovernorate.SelectedValue);

        //        DataSet DS = Helper.GetData(WilayatConstants.proc_GetWilayatByGovernorate, governorateIDParamForWilayat);
        //        //ddlWilayat.DataTextField = ds.Tables[0].Columns["Wilayat"].ToString(); // text field name of table dispalyed in dropdown       
        //        //ddlWilayat.DataValueField = ds.Tables[0].Columns["WilayatID"].ToString();
        //        ddlWilayat.DataSource = DS;
        //        ddlWilayat.DataBind();

        //        ddlWilayat.Items.Insert(0, new ListItem(" Choose Wilayat ", "0"));
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                       && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                       && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                Response.Redirect("/Admin/" + Helper.pg_AllPropertiesAdmin);
            else
                Response.Redirect(Helper.pg_MyProperties);
        }

        [WebMethod]
        public static string GetWilayats(int govtID)
        {
            WilayatCollection objWilayats = new WilayatCollection();
            
            objWilayats = objWilayats.GetWilayatByGovernorateID(govtID);

            return JsonConvert.SerializeObject(objWilayats).ToString();
        }
        
        [WebMethod]
        public static bool UploadPropertyImages(string binary , string ID)
        {
            if (binary != "")
            {
                if (binary.Contains("data:"))
                {
                    if (binary.Contains(","))
                    {
                        binary = binary.Split(',')[1];
                    }
                }
                byte[] arr = Convert.FromBase64String(binary);

                BNImage image = new BNImage();
                image.ImageData = arr;

                image.SaveImage(image);
            }
            return true;
        }
    }
}