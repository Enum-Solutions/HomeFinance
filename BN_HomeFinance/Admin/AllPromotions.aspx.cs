using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllPromotions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BNPost Promotion = new BNPost(Helper.Module.Promotions);

                    gv_Attachments.DataSource = Promotion.GetAllPostsDatatable(Promotion);
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
            _link = "<a href=\"../" + Helper.pg_CreatePromotion + "?" + Helper.QueryStrings.PromotionID.ToString() + "=" + _ID + "&" + Helper.QueryStrings.IsAdmin.ToString() + "=1\">" + txt + "</a>";
            return _link;
        }
    }
}