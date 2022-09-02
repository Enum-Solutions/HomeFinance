using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class MyPromotions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BNPost Promotion = new BNPost(Helper.Module.Promotions);

                    gv_Promotions.DataSource = Promotion.GetPostsByUserIDDatatable(Promotion.Module, ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID);
                    gv_Promotions.DataBind();
                    gv_Promotions.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            _link = "<a href=\"" + Helper.pg_CreatePromotion + "?" + Helper.QueryStrings.PromotionID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewPromotion_Click(object sender, EventArgs e)
        {
            Response.Redirect(Helper.pg_CreatePromotion);
        }
    }
}