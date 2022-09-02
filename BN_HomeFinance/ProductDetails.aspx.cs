using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        public string html = "", html2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString[Helper.QueryStrings.ProductID.ToString()] != null)
                    {
                        Product product = new Product();

                        product.ProductID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.ProductID.ToString()]);

                        product = product.GetProduct(product.ProductID.ToString());

                        if (product != null)
                        {
                            lblProductName.Text = product.ProductName;
                            lblPrice.Text = string.Format("{0:N}", product.Price.ToString());
                            lblCategory.Text = product.Category.CategoryName;
                            //lblIsAvailable.Text = product.IsAvailable;
                            //lblAvailableQuantity.Text = product.Quantity.ToString();
                            //lblDeliveryPossible.Text = product.IsDeliveryPossible;
                            //lblDeliveryDays.Text = product.DeliveryTime;
                            //lblDeliveryCharges.Text = string.Format("{0:N}", product.DeliveryCharges);
                            lblDescription.Text = HttpUtility.HtmlDecode(product.Description);

                            product.CreatedBy = product.CreatedBy.GetUserWithUserID(product.CreatedBy.UserID);
                            lblName.Text = product.CreatedBy.FullName;
                            lblAddress.Text = product.CreatedBy.Address + ", " + product.CreatedBy.City + ", " + product.CreatedBy.Country + ".";
                            lblPhone.Text = product.CreatedBy.Contact;

                            if (product.Images.Count >= 0)
                            {
                                for (int i = 0; i < product.Images.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        html += @"<li class='active' data-bs-target='#product_details_slider'  aria-current='true' data-bs-slide-to='" + i + @"' 
                                                  aria-label='Slide " + (i + 1) + "' style='background-image: url(" + product.Images.BNImages[i].ImageURL + @");'></li>";

                                        html2 += @"<div class='carousel-item active'><a class='gallery_img' href='TempStyleJS/img/product-img/product-9.jpg'>
                                                   <img class='d-block w-100' src='" + product.Images.BNImages[i].ImageURL + @"' alt='First slide'></a></div>";
                                    }
                                    else
                                    {
                                        html += @"<li data-bs-target='#product_details_slider' data-bs-slide-to='" + i + @"' 
                                                  aria-label='Slide " + (i + 1) + "' style='background-image: url(" + product.Images.BNImages[i].ImageURL + @");'></li>";

                                        html2 += @"<div class='carousel-item'><a class='gallery_img' href='TempStyleJS/img/product-img/product-9.jpg'>
                                                   <img class='d-block w-100' src='" + product.Images.BNImages[i].ImageURL + @"' alt='First slide'></a></div>";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
                return;
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

        }
    }
}