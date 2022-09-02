using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        Properties prp = new Properties();

                        gv_Properties.DataSource = prp.GetAllPropertiesDatatable();
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
            _link = "<a href=\"../" + Helper.pg_CreateProperty + "?" + Helper.QueryStrings.PropertyID.ToString() + "=" + _ID + "&" + Helper.QueryStrings.IsAdmin.ToString() + "=1\">" + txt + "</a>";
            return _link;
        }

        protected void btnNewProperty_Click(object sender, EventArgs e)
        {
            Response.Redirect("../" + Helper.pg_CreateProperty + "?" + Helper.QueryStrings.IsAdmin.ToString() + "=1");
        }

        protected void gv_Properties_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string value = e.Row.Cells[1].Text;

                try
                {
                    e.Row.Cells[1].Text = "OMR " + Convert.ToDouble(value).ToString("N2");
                }
                catch
                {
                    e.Row.Cells[1].Text = value;
                }
            }
        }
    }
}