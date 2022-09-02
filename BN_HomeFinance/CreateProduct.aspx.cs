using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class CreateProduct : System.Web.UI.Page
    {
        public string HTMLImages = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                        ddlCategory.DataSource = Helper.ExecuteSPWithNoParameters(Category.proc_GetCategory);
                        ddlCategory.DataTextField = Category.col_CategoryName;
                        ddlCategory.DataValueField = Category.col_CategoryID;
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, new ListItem("Select Category", "-1"));

                        ddlUser.DataSource = Helper.GetData(User_Constants.Proc_GetActiveVendorBuilders, new SqlParameter(User_Constants.Par_UserType, User_Constants.UserType.Vendor.ToString()));
                        ddlUser.DataBind();
                        ddlUser.Items.Insert(0, new ListItem("Choose Vendor ", "0"));

                        if (Request.QueryString[Helper.QueryStrings.ProductID.ToString()] != null)
                        {
                            header.InnerText = "Edit Product";

                            Product product = new Product();

                            product.ProductID = Convert.ToInt16(Request.QueryString[Helper.QueryStrings.ProductID.ToString()]);

                            product = product.GetProduct(product.ProductID.ToString());

                            txtProductName.Text = product.ProductName;
                            ddlCategory.SelectedValue = product.Category.CategoryID.ToString();
                            txtQuantity.Text = product.Quantity.ToString();
                            txtPrice.Text = product.Price.ToString();
                            txtDescription.InnerText = HttpUtility.HtmlDecode(product.Description);
                            ddlDelivery.SelectedValue = product.IsDeliveryPossible;
                            ddlUser.SelectedValue = product.CreatedBy.UserID;

                            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                               && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                               && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                            {
                                divUser.Visible = true;
                            }

                            if (product.IsDeliveryPossible == "Yes")
                            {
                                txtDeliveryTime.Text = product.DeliveryTime;
                                txtDeliveryCharges.Text = product.DeliveryCharges;
                            }
                            else
                            {
                                txtDeliveryTime.Enabled = false;
                                txtDeliveryCharges.Enabled = false;
                            }

                            ddlIsAvailable.SelectedValue = product.IsAvailable;

                            if (product.Images.Count > 0)
                            {
                                for (int i = 0; i < product.Images.Count; i++)
                                    HTMLImages += @"<div class='col-4 col-sm-3 col-md-3 col-lg-2 mt-2 t-c'><img class='prop-img w-100' src= '" + product.Images.BNImages[i].ImageURL + "'></div>";

                                ViewState[Helper.ViewStates.Image.ToString()] = product.Images;
                            }

                            btnSubmit.Visible = false;

                            btnUpdate.Visible = true;
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                               && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                               && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString().ToString())
                            {
                                divUser.Visible = true;
                            }
                            else
                            {
                                ddlUser.SelectedValue = user.UserID;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
                return;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                product.ProductName = txtProductName.Text;
                product.Category.CategoryID = Convert.ToInt16(ddlCategory.SelectedValue);
                product.Category.CategoryName = ddlCategory.SelectedValue;

                if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                {
                    product.CreatedBy.UserID = ddlUser.SelectedValue;
                }
                else
                {
                    product.CreatedBy.UserID = ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID;
                }

                product.Quantity = 0;
                product.Price = Convert.ToDouble(txtPrice.Text);
                product.CreatedAt = DateTime.Now;
                product.Description = HttpUtility.HtmlEncode(txtDescription.InnerText);
                product.IsDeliveryPossible = "";
                product.DeliveryTime = "";
                product.DeliveryCharges = "";
                product.IsAvailable = "Active";

                if (FUImages.HasFiles)
                {
                    for (int i = 0; i < FUImages.PostedFiles.Count; i++)
                    {
                        BNImage img = new BNImage();

                        using (Stream str = FUImages.PostedFiles[i].InputStream)
                        {
                            using (BinaryReader rdr = new BinaryReader(str))
                            {
                                img.ImageData = rdr.ReadBytes((Int32)str.Length);
                                img.ImageName = FUImages.PostedFiles[i].FileName;
                            }
                        }

                        product.Images.BNImages.Add(img);
                        product.Images.Count++;
                    }
                }

                product.InsertProduct(product);

                if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Added Successfully');location.href='/Admin/" + Helper.pg_AllProducts + "'", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Added Successfully');location.href='" + Helper.pg_MyProducts + "'", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                if (product.DeleteProduct(Request.QueryString[Helper.QueryStrings.ProductID.ToString()].ToString()))
                {
                    product.ProductName = txtProductName.Text;
                    product.Category.CategoryID = Convert.ToInt16(ddlCategory.SelectedValue);
                    product.Category.CategoryName = ddlCategory.SelectedValue;

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                    {
                        product.CreatedBy.UserID = ddlUser.SelectedValue;
                    }
                    else
                    {
                        product.CreatedBy.UserID = ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID;
                    }

                    product.Quantity = 0;
                    product.Price = Convert.ToDouble(txtPrice.Text);
                    product.CreatedAt = DateTime.Now;
                    product.Description = HttpUtility.HtmlEncode(txtDescription.InnerText);
                    product.IsDeliveryPossible = "";
                    product.DeliveryTime = "";
                    product.DeliveryCharges = "";
                    product.IsAvailable = "Active";

                    if (FUImages.HasFiles)
                    {
                        for (int i = 0; i < FUImages.PostedFiles.Count; i++)
                        {
                            BNImage img = new BNImage();

                            using (Stream str = FUImages.PostedFiles[i].InputStream)
                            {
                                using (BinaryReader rdr = new BinaryReader(str))
                                {
                                    img.ImageData = rdr.ReadBytes((Int32)str.Length);
                                    img.ImageName = FUImages.PostedFiles[i].FileName;
                                }
                            }

                            product.Images.BNImages.Add(img);
                            product.Images.Count++;
                        }
                    }
                    else
                    {
                        if (ViewState[Helper.ViewStates.Image.ToString()] != null)
                        {
                            product.Images = (BNImageCollection)ViewState[Helper.ViewStates.Image.ToString()];
                        }
                    }

                    product.InsertProduct(product);

                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Updated Successfully');location.href='/Admin/" + Helper.pg_AllProducts + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Updated Successfully');location.href='" + Helper.pg_MyProducts + "'", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                Response.Redirect("/Admin/" + Helper.pg_AllProducts);
            else
                Response.Redirect(Helper.pg_MyProducts);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();

                if (product.DeleteProduct(Request.QueryString[Helper.QueryStrings.ProductID.ToString()]))
                {
                    if (Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] != null
                           && Request.QueryString[Helper.QueryStrings.IsAdmin.ToString()] == "1"
                           && Session[Helper.Sessions.User.ToString()] != null && ((BNUser)Session[Helper.Sessions.User.ToString()]).UserType.UserType == User_Constants.UserType.Admin.ToString())
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Deleted Successfully');location.href='/Admin/" + Helper.pg_AllProducts + "'", true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Deleted Successfully');location.href='" + Helper.pg_MyProducts + "'", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static bool UploadProductImages(string binary)
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