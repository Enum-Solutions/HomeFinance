using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AmenityTray : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gv_Amenities.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch
            {

            }
        }
        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_Amenity + "?" + Helper.QueryStrings.AmenityID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewAmenity_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Amenity);
        }
    }
}