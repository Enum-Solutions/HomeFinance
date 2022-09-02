using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class PropertyType : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    PropertyTypes objPropertyType = new PropertyTypes();

                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                    if (Request.QueryString[Helper.QueryStrings.PropertyTypeID.ToString()] != null)
                    {
                        objPropertyType = objPropertyType.ReadPropertyTypeByID(Request.QueryString[Helper.QueryStrings.PropertyTypeID.ToString()]);
                        txtPropertyType.Text = objPropertyType.PropertyType;

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
                PropertyTypes objPropertyType = new PropertyTypes();

                objPropertyType.PropertyType = txtPropertyType.Text;
                objPropertyType.CreatePropertyType(objPropertyType);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Type Added Successfully');location.href='" + Helper.pg_PropertyTypeTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyTypes objPropertyType = new PropertyTypes();

                objPropertyType.DeletePropertyTypeByID(Request.QueryString[Helper.QueryStrings.PropertyTypeID.ToString()]);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Type Deleted Successfully');location.href='" + Helper.pg_PropertyTypeTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyTypes objPropertyType = new PropertyTypes();

                objPropertyType.PropertyTypeID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.PropertyTypeID.ToString()]);
                objPropertyType.PropertyType = txtPropertyType.Text;


                objPropertyType.UpdatePropertyTypeByID(objPropertyType);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Property Type Updated Successfully');location.href='" + Helper.pg_PropertyTypeTray + "'", true);
            }
            catch (Exception ex)
            {

            }

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_PropertyTypeTray);
        }
    }
}