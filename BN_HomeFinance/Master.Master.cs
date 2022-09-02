using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;

namespace BN_HomeFinance
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    lnkLogin.Visible = false;
                    lnkLogout.Visible = true;

                    BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                    if (user.UserType.UserType == User_Constants.UserType.Admin.ToString())
                    {
                        linkHome.NavigateUrl = "/Admin/" + Helper.pg_Dashboard;
                    }
                    else
                    {
                        linkHome.NavigateUrl = "../" + Helper.pg_Index;
                    }

                    if (user.UserType.UserType == User_Constants.UserType.Vendor.ToString() || user.UserType.UserType == User_Constants.UserType.Builder.ToString())
                        linkProfile.Visible = true;
                    else
                        linkProfile.Visible = false;
                }
                else
                {
                    linkHome.NavigateUrl = "../" + Helper.pg_Index;
                }


                #region Modal

                BNPost post = new BNPost(Helper.Module.Popup);

                BNPostCollection collection = new BNPostCollection();

                collection = post.GetAllPostsWithImages(post);

                for (int i = 0; i < collection.Count; i++)
                {
                    if (Request.Url.ToString().ToLower().Contains(collection.Posts[i].Description.ToLower()))
                    {
                        if (collection.Posts[i].Image.ImageData != null)
                        {
                            HasModal.Value = "1";

                            imgPopup.ImageUrl = collection.Posts[i].Image.ImageURL;
                        }

                        break;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
    }
}