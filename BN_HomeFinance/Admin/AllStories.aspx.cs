using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance.Admin
{
    public partial class AllStories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BNPost Story = new BNPost(Helper.Module.Stories);

                    gv_Attachments.DataSource = Story.GetAllPostsDatatable(Story);
                    gv_Attachments.DataBind();
                    gv_Attachments.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception ex)
                {

                }
            }
            else
                return;
        }

        protected string EditLink(string _ID, string txt)
        {
            string _link = "";
            _link = "<a href=\"../" + Helper.pg_CreateStories + "?" + Helper.QueryStrings.StoryID.ToString() + "=" + _ID + "&" + Helper.QueryStrings.IsAdmin.ToString() + "=1\">" + txt + "</a>";
            return _link;
        }
    }
}