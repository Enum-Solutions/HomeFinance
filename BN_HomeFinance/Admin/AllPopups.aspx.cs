using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllPopups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.Popup);

                gv_Popups.DataSource = post.GetAllPostsDatatable(post);
                gv_Popups.DataBind();
                gv_Popups.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_Popup + "?" + Helper.QueryStrings.PopupID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;

        }
        protected void btnNewPopup_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Popup);
        }
    }
}