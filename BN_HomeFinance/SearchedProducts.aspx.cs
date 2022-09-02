using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class SearchedProducts : System.Web.UI.Page
    {
        public string Products = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Helper.Sessions.Products.ToString()] != null)
                {
                    ProductCollection product = (ProductCollection)Session[Helper.Sessions.Products.ToString()];

                    if (product != null && product.Count > 0)
                    {
                        for (int i = 0; i < product.Count; i++)
                        {
                            Products += @"<div class='col-md-4'><div class='card-box-a card-shadow'><div class='img-box-a'><img src='" + product.Products[i].Image.ImageURL + @"' alt='' class='img-a img-fluid'></div>
                                      <div class='card-overlay'><div class='card-overlay-a-content'><div class='card-header-a'><h2 class='card-title-a'>
                                      <a href='" + Helper.pg_ProductDetails + @"?" + Helper.QueryStrings.ProductID.ToString() + @"=" + product.Products[i].ProductID + @"'>" + product.Products[i].ProductName + @"</a></h2></div>
                                      <div class='card-body-a'><div class='price-box d-flex'><span class='price-a'>" + product.Products[i].Category.CategoryName + @" | OMR " + product.Products[i].Price + @"</span></div>
                                      <a href='" + Helper.pg_ProductDetails + @"?" + Helper.QueryStrings.ProductID.ToString() + @"=" + product.Products[i].ProductID + @"' class='link-a'>Click here to view<span class='bi bi-chevron-right'></span></a></div>
                                      <div class='card-footer-a'></div></div></div></div></div>";
                        }
                    }
                }
            }
            else
                return;
        }
    }
}