using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BNUser user = new BNUser();

                    gv_Users.DataSource = user.GetAllUsersDatatable();
                    gv_Users.DataBind();
                    gv_Users.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return;
            }
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_CreateStakeHolder + "?" + Helper.QueryStrings.UserID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_CreateStakeHolder);
        }

        protected void btnNewUser_Click1(object sender, EventArgs e)
        {

        }
    }
}