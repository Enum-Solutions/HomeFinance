using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Admin
{
    public partial class AnnouncementsTray : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BNPost post = new BNPost(Helper.Module.Announcements);

                gv_Attachments.DataSource = post.GetAllPostsDatatable(post);
                gv_Attachments.DataBind();
                gv_Attachments.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"" + Helper.pg_Announcements + "?" + Helper.QueryStrings.AnnouncementID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewAnnouncement_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_Announcements);
        }
    }
}