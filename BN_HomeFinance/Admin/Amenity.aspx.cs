using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance;

namespace BN_HomeFinance.Admin
{
    public partial class Amenity : System.Web.UI.Page
    {
        Resources.Amenity objAmenity = new Resources.Amenity();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;

                    if (Request.QueryString[Helper.QueryStrings.AmenityID.ToString()] != null)
                    {
                        objAmenity = objAmenity.ReadAmenityByID(Request.QueryString[Helper.QueryStrings.AmenityID.ToString()]);
                        txtAmenity.Text = objAmenity.AmenityType;

                        btnUpdate.Visible = true;
                        btnDelete.Visible = true;
                        btnSubmit.Visible = false;

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objAmenity.AmenityType = txtAmenity.Text;
                objAmenity.CreateAmenity(objAmenity);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Amenity Added Successfully');location.href='"+ Helper.pg_AmenityTray + "'" , true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                objAmenity.AmenityID = Convert.ToString(Request.QueryString[Helper.QueryStrings.AmenityID.ToString()]);
                objAmenity.AmenityType = txtAmenity.Text;
                objAmenity.UpdateAmenityByID(objAmenity);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Amenity Updated Successfully');location.href='" + Helper.pg_AmenityTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_AmenityTray);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objAmenity.DeleteAmenityByID(Request.QueryString[Helper.QueryStrings.AmenityID.ToString()]);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Amenity Deleted Successfully');location.href='" + Helper.pg_AmenityTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}