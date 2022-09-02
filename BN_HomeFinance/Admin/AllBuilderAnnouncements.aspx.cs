using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllBuilderAnnouncements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.BuilderAnnouncements);

                gv_Announcements.DataSource = post.GetAllPostsDatatable(post);
                gv_Announcements.DataBind();
                gv_Announcements.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_BuilderAnnouncement + "?" + Helper.QueryStrings.AnnouncementID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewAnnouncement_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_BuilderAnnouncement);
        }
    }
}