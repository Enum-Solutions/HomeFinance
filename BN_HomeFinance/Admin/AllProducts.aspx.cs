using BN_HomeFinance.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BN_HomeFinance.Admin
{
    public partial class AllProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        Product prp = new Product();

                        gv_Products.DataSource = prp.GetAllProductsDatatable();
                        gv_Products.DataBind();
                        gv_Products.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            _link = "<a href=\"../" + Helper.pg_CreateProduct + "?" + Helper.QueryStrings.ProductID.ToString() + "=" + _ID + "&" + Helper.QueryStrings.IsAdmin.ToString() + "=1\">" + txt + "</a>";
            return _link;
        }
        protected void btnNewProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("../" +Helper.pg_CreateProduct + "?" + Helper.QueryStrings.IsAdmin.ToString() + "=1");
        }

        protected void gv_Products_RowDataBound(object sender, GridViewRowEventArgs e)
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