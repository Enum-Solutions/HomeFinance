using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class Tenure : System.Web.UI.Page
    {
        BN_HomeFinance.Resources.Tenure objTenure = new BN_HomeFinance.Resources.Tenure();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;

                    if (Request.QueryString[Helper.QueryStrings.TenureID.ToString()] != null)
                    {
                        objTenure = objTenure.ReadTenureByID(Request.QueryString[Helper.QueryStrings.TenureID.ToString()]);
                        txtTenure.Text = objTenure.tenure;

                        BtnUpdate.Visible = true;
                        BtnDelete.Visible = true;
                        BtnAdd.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                objTenure.tenure = txtTenure.Text;
                objTenure.CreateTenure(objTenure);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tenure Item Added Successfully');location.href='" + Helper.pg_TenureTray + "'", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objTenure.DeleteTenureByID(Request.QueryString[Helper.QueryStrings.TenureID.ToString()]);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tenure Item Deleted Successfully');location.href='" + Helper.pg_TenureTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                objTenure.ID = Request.QueryString[Helper.QueryStrings.TenureID.ToString()];
                objTenure.tenure = txtTenure.Text;

                objTenure.UpdateTenureByID(objTenure);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tenure Item Updated Successfully');location.href='" + Helper.pg_TenureTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_TenureTray);
            }
            catch (Exception ex)
            {

            }
        }
    }
}