using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class Governorate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Governorates objGovernorate = new Governorates();

                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                    if (Request.QueryString[Helper.QueryStrings.GovernorateID.ToString()] != null)
                    {
                        objGovernorate = objGovernorate.ReadGovernorateByID(Request.QueryString[Helper.QueryStrings.GovernorateID.ToString()]);
                        txtGovernorate.Text = objGovernorate.Governorate;

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
                Governorates objGovernorate = new Governorates();

                objGovernorate.Governorate = txtGovernorate.Text;
                objGovernorate.CreateGovernorate(objGovernorate);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Governorate Added Successfully');location.href='" + Helper.pg_GovernorateTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Governorates objGovernorate = new Governorates();

                objGovernorate.DeleteGovernorateByID(Request.QueryString[Helper.QueryStrings.GovernorateID.ToString()]);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Governorate Deleted Successfully');location.href='" + Helper.pg_GovernorateTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                Governorates objGovernorate = new Governorates();

                objGovernorate.GovernorateID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.GovernorateID.ToString()]);
                objGovernorate.Governorate = txtGovernorate.Text;

                if (true)
                {
                    objGovernorate.UpdateGovernorateByID(objGovernorate);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Governorate Updated Successfully');location.href='" + Helper.pg_GovernorateTray + "'", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_GovernorateTray);
        }
    }
}