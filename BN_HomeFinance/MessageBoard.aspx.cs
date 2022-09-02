using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class MessageBoard : System.Web.UI.Page
    {
        public string HTML = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BNPost post = new BNPost(Helper.Module.Messages);

                    BNPostCollection collection = new BNPostCollection();

                    collection = collection.GetAllActivePosts(post);

                    if (collection != null && collection.Count > 0)
                    {
                        for (int count = 0; count < collection.Count; count++)
                        {
                            HTML += @"<div class='accordion-item shadow'><h2 class='accordion-header' id='flush-heading" + count + @"'>
                                    <button class='accordion-button collapsed' type='button' data-bs-toggle='collapse' data-bs-target='#flush-collapse" + count + @"' 
                                    aria-expanded='false' aria-controls='flush-collapse" + count + @"'>" + collection.Posts[count].Header + @"</button></h2>
                                    <div id='flush-collapse" + count + @"' class='accordion-collapse collapse' aria-labelledby='flush-heading" + count + @"' data-bs-parent='#accordionFlushExample'>
                                    <div class='accordion-body'><span style='float:right;font-weight: bold;'>| " + collection.Posts[count].DateCreated.ToString("MMMM, dd yyyy") + @"</span>" + HttpUtility.HtmlDecode(collection.Posts[count].Description) + @"</div></div></div>";
                        }
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {

            }
        }
    }
}