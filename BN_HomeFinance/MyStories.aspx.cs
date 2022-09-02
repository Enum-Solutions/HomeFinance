using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class MyStories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        BNPost Story = new BNPost(Helper.Module.Stories);

                        gv_Stories.DataSource = Story.GetPostsByUserIDDatatable(Story.Module, ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID);
                        gv_Stories.DataBind();
                        gv_Stories.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
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
            _link = "<a href=\"" + Helper.pg_CreateStories + "?" + Helper.QueryStrings.StoryID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewStory_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_CreateStories);
        }
    }
}