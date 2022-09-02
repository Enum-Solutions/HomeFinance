using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Admin
{
    public partial class InterestRateTray : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (Helper.GetData(InterestRateConstants.Proc_GetAllInterestRates, null)).Tables[0];

                gv_InterestRate.DataSource = dt;
                gv_InterestRate.DataBind();
                gv_InterestRate.HeaderRow.TableSection = TableRowSection.TableHeader;

                gv_Tenure.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch
            {

            }
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_InterestRate + "?" + Helper.QueryStrings.InterestRateID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewInterest_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_InterestRate);
        }

        protected void btnNewTenure_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Tenure);
        }

        protected string EditLinkTenure(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_Tenure + "?" + Helper.QueryStrings.TenureID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }
    }
}