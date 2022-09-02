using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance;

namespace BN_HomeFinance.Admin
{
    public partial class InterestRate : System.Web.UI.Page
    {
        Resources.InterestRate objIR = new Resources.InterestRate();

        Resources.InterestRateCollection objIRCollection = new Resources.InterestRateCollection();

        int TenureCheck;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ddlCategory.DataSource = Helper.GetData(Resources.PropertyTypeConstants.proc_GetPropertyType, null);
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Choose Category", "0"));

                    DataSet dt = Helper.GetData(Resources.TenureConstants.Proc_ReadAllTenure, null);

                    ddlTenureFrom.DataSource = dt;
                    ddlTenureFrom.DataBind();
                    ddlTenureFrom.Items.Insert(0, new ListItem("Choose Tenure From", "0"));

                    ddlTenureTo.DataSource = dt;
                    ddlTenureTo.DataBind();
                    ddlTenureTo.Items.Insert(0, new ListItem("Choose Tenure To", "0"));

                    if (Request.QueryString[Helper.QueryStrings.InterestRateID.ToString()] != null)
                    {
                        objIR = objIR.GetInterestRateByID(Request.QueryString[Helper.QueryStrings.InterestRateID.ToString()].ToString());

                        txtIR.Text = objIR.Interest;

                        ddlCategory.SelectedValue = objIR.Category.PropertyTypeID.ToString();

                        ddlTenureFrom.SelectedValue = objIR.TenureFrom;

                        ddlTenureTo.SelectedValue = objIR.TenureTo;

                        BtnUpdate.Visible = true;
                        BtnDelete.Visible = true;
                        BtnAdd.Visible = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                objIRCollection = objIRCollection.GetFromToValuesForInterestRate();

            }
        }

        protected void AddInterestRate_Click(object sender, EventArgs e)
        {
            try
            {
                int From, To;
                lblError.Visible = false;
                objIR.TenureFrom = ddlTenureFrom.SelectedValue;
                objIR.TenureTo = ddlTenureTo.SelectedValue;
                From = Convert.ToInt32(objIR.TenureFrom);
                To = Convert.ToInt32(objIR.TenureTo);
                objIR.Tenure = Convert.ToInt32(objIR.TenureTo) - Convert.ToInt32(objIR.TenureFrom);
                TenureCheck = Convert.ToInt32(objIR.TenureTo) - Convert.ToInt32(objIR.TenureFrom);
                objIR.Interest = txtIR.Text;
                objIR.Category.PropertyTypeID = Convert.ToInt32(ddlCategory.SelectedValue);

                if (To < From)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerter", "alert('Value of \"Tenure To\" cannot be less than the value of \"Tenure From\" ')");
                }
                else
                {
                    if (objIRCollection.Count.Equals(0))
                    {
                        objIR.CreateInterestRate(objIR);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", "alert('Successfully added')");
                        Response.Redirect(Helper.pg_InterestTray);
                    }
                    else
                    {
                        string error = "";

                        for (int i = 0; i < objIRCollection.Count; i++)
                        {
                            if (Enumerable.Range(Convert.ToInt32(objIRCollection.interestRate[i].TenureFrom), Convert.ToInt32(objIRCollection.interestRate[i].TenureTo)).Contains(From)
                                && objIRCollection.interestRate[i].Category.PropertyTypeID.ToString().ToLower() == ddlCategory.SelectedValue.ToString().ToLower())
                            {
                                error = "Tenure From: " + Convert.ToInt32(objIR.TenureFrom) + " has already been assigned an Interest Rate";
                                break;
                            }
                            else if (Enumerable.Range(Convert.ToInt32(objIRCollection.interestRate[i].TenureFrom), Convert.ToInt32(objIRCollection.interestRate[i].TenureTo)).Contains(To)
                                && objIRCollection.interestRate[i].Category.PropertyTypeID.ToString().ToLower() == ddlCategory.SelectedValue.ToString().ToLower())
                            {
                                error = "Tenure To: " + Convert.ToInt32(objIR.TenureTo) + " has already been assigned an Interest Rate";
                                break;
                            }
                            else if (!Enumerable.Range(Convert.ToInt32(objIRCollection.interestRate[i].TenureFrom), Convert.ToInt32(objIRCollection.interestRate[i].TenureTo)).Contains(From) && !Enumerable.Range(Convert.ToInt32(objIRCollection.interestRate[i].TenureFrom), Convert.ToInt32(objIRCollection.interestRate[i].TenureTo)).Contains(To))
                            {
                                objIR.CreateInterestRate(objIR);
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profit Configuration Added Successfully');location.href='" + Helper.pg_InterestTray + "'", true);
                            }
                        }

                        if (error != "")
                        {
                            lblError.Visible = true;
                            lblError.Text = error;
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = error;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        protected void DeleteInterestRate_Click(object sender, EventArgs e)
        {
            try
            {
                objIR.DeleteInterestRateByID(Request.QueryString[Helper.QueryStrings.InterestRateID.ToString()]);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profit Configuration Deleted Successfully');location.href='" + Helper.pg_InterestTray + "'", true);
            }
            catch
            {
                throw;
            }
        }

        protected void UpdateInterestRate_Click(object sender, EventArgs e)
        {
            try
            {
                objIR.ID = Request.QueryString[Helper.QueryStrings.InterestRateID.ToString()];
                objIR.TenureFrom = ddlTenureFrom.SelectedValue;
                objIR.TenureTo = ddlTenureTo.SelectedValue;
                objIR.Tenure = Convert.ToInt32(objIR.TenureTo) - Convert.ToInt32(objIR.TenureFrom);
                TenureCheck = Convert.ToInt32(objIR.TenureTo) - Convert.ToInt32(objIR.TenureFrom);
                objIR.Interest = txtIR.Text;
                objIR.Category.PropertyTypeID = Convert.ToInt32(ddlCategory.SelectedIndex);

                if (Convert.ToInt32(objIR.TenureTo) < Convert.ToInt32(objIR.TenureFrom))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tenure To cannot be less than Tenure From')");
                }
                else
                {
                    objIR.UpdateInterestRateByID(objIR);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Profit Configuration Updated Successfully');location.href='" + Helper.pg_InterestTray + "'", true);
                }
            }
            catch
            {
                throw;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_InterestTray);
        }

    }
}