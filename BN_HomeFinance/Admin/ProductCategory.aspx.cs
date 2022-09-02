using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class ProductCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BN_HomeFinance.Resources.Category objCategory = new BN_HomeFinance.Resources.Category();

                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;

                    if (Request.QueryString[Helper.QueryStrings.ProductCategoryID.ToString()] != null)
                    {
                        objCategory = objCategory.ReadProductCategoryByID(Request.QueryString[Helper.QueryStrings.ProductCategoryID.ToString()]);
                        txtProductCategory.Text = objCategory.CategoryName;

                        BtnUpdate.Visible = true;
                        BtnDelete.Visible = true;
                        BtnAdd.Visible = false;

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {
                BN_HomeFinance.Resources.Category objCategory = new BN_HomeFinance.Resources.Category();

                objCategory.CategoryName = txtProductCategory.Text;
                objCategory.CreateProductCategory(objCategory);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Category Added Successfully');location.href='" + Helper.pg_ProductCategoryTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                BN_HomeFinance.Resources.Category objCategory = new BN_HomeFinance.Resources.Category();

                objCategory.DeleteProductCategoryByID(Request.QueryString[Helper.QueryStrings.ProductCategoryID.ToString()]);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Category Deleted Successfully');location.href='" + Helper.pg_ProductCategoryTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                BN_HomeFinance.Resources.Category objCategory = new BN_HomeFinance.Resources.Category();

                objCategory.CategoryID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.ProductCategoryID.ToString()]);
                objCategory.CategoryName = txtProductCategory.Text;


                objCategory.UpdateProductCategoryByID(objCategory);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product Category Updated Successfully');location.href='" + Helper.pg_ProductCategoryTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_ProductCategoryTray);
        }
    }
}