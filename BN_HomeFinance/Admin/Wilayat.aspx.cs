using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class Wilayat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlGovernorate.DataSource = Helper.GetData(GovernorateConstants.Proc_GetGovernorate, null);
                ddlGovernorate.DataBind();
                ddlGovernorate.Items.Insert(0, new ListItem(" Choose Governorate ", "0"));

                if (Request.QueryString[Helper.QueryStrings.WilayatID.ToString()] == null)
                {
                    BtnUpdate.Visible = false;
                    BtnDelete.Visible = false;
                }
                else
                {
                    DataSet ds = new DataSet();
                    Wilayats objWilayat = new Wilayats();

                    objWilayat = objWilayat.ReadWilayatByID(Request.QueryString[Helper.QueryStrings.WilayatID.ToString()]);
                    txtWilayat.Text = objWilayat.Wilayat;
                    ddlGovernorate.DataSource = Helper.GetData(GovernorateConstants.Proc_GetGovernorate, null);
                    ddlGovernorate.DataBind();
                    ddlGovernorate.Items.Insert(0, new ListItem(" Choose Governorate ", "0"));
                    ddlGovernorate.SelectedValue = objWilayat.Governorate.GovernorateID.ToString();
                    BtnUpdate.Visible = true;
                    BtnDelete.Visible = true;
                    BtnAdd.Visible = false;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_WilayatTray);
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Wilayats objWilayat = new Wilayats();

                objWilayat.DeleteWilayatByID(Request.QueryString[Helper.QueryStrings.WilayatID.ToString()]);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Wilayat Deleted Successfully');location.href='" + Helper.pg_WilayatTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Wilayats objWilayat = new Wilayats();

                objWilayat.WilayatID = Convert.ToInt32(Request.QueryString[Helper.QueryStrings.WilayatID.ToString()]);
                objWilayat.Wilayat = txtWilayat.Text;
                objWilayat.Governorate.GovernorateID = Convert.ToInt16(ddlGovernorate.SelectedValue);

                objWilayat.UpdateWilayatByID(objWilayat);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Wilayat Updated Successfully');location.href='" + Helper.pg_WilayatTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Wilayats objWilayat = new Wilayats();

                objWilayat.Wilayat = txtWilayat.Text;
                objWilayat.Governorate.GovernorateID = Convert.ToInt16(ddlGovernorate.SelectedValue);
                objWilayat.CreateWilayat(objWilayat);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Wilayat Added Successfully');location.href='" + Helper.pg_WilayatTray + "'", true);
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}