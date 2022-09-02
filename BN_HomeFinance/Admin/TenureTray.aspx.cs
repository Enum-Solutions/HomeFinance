using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class TenureTray : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gv_Tenure.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnNewTenure_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Tenure);
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_Tenure + "?" + Helper.QueryStrings.TenureID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }
    }
}