using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class MyProducts : System.Web.UI.Page
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

                        Product prp = new Product();

                        gv_Products.DataSource = prp.GetMyProductsDatatable(usr.UserID);
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
            _link = "<a href=\"../" + Helper.pg_CreateProduct + "?" + Helper.QueryStrings.ProductID.ToString() + "=" + _ID + "\">" + txt + "</a>";
            return _link;
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Helper.pg_CreateProduct);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_Products_RowDataBound(object sender, GridViewRowEventArgs e)
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