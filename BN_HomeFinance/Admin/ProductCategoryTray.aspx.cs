using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class ProductCategoryTray : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gv_Categories.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_ProductCategory + "?" + Helper.QueryStrings.ProductCategoryID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewProductCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_ProductCategory);
        }
    }
}