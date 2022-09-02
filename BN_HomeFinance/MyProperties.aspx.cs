using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance
{
    public partial class MyProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        BNUser usr = (BNUser)Session[Helper.Sessions.User.ToString()];

                        Properties prp = new Properties();

                        gv_Properties.DataSource = prp.GetMyPropertiesDatatable(usr.UserID);
                        gv_Properties.DataBind();
                        gv_Properties.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            _link = "<a href=\"../" + Helper.pg_CreateProperty + "?" + Helper.QueryStrings.PropertyID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnProperty_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_CreateProperty);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_Properties_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string value = e.Row.Cells[2].Text;

                try
                {
                    e.Row.Cells[2].Text = "OMR " + Convert.ToDouble(value).ToString("N2");
                }
                catch
                {
                    e.Row.Cells[2].Text = value;
                }
            }
        }
    }
}