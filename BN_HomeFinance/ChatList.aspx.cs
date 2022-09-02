using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;
using Newtonsoft.Json;

namespace BN_HomeFinance
{
    public partial class ChatList : System.Web.UI.Page
    {
        public string html = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session[Helper.Sessions.User.ToString()] != null)
                    {
                        BNUser user = (BNUser)Session[Helper.Sessions.User.ToString()];

                        hdUserName.Value = user.UserID;
                        hdFullName.Value = user.FullName;
                        hdUrl.Value = user.Picture.ImageURL;
                    }
                    else
                    {
                        //ChatList.Visible = false;
                    }

                    ChatCollection collection = new ChatCollection();

                    collection = collection.GetAllChats();

                    if (collection != null && collection.Count > 0)
                    {
                        for (int i = 0; i < collection.Count; i++)
                        {
                            string uri = "\"/" + Helper.pg_ChatPage + "?" + Helper.QueryStrings.ChatID.ToString() + "=" + collection.Chats[i].ID + "\"";

                            html += @"<div class='row mt-1 chat-container'><div class='col-12 col-sm-12 col-md-12 col-lg-12 message-container'>
                                    <div class='chat-message shadow mt-2 w-100'><div class='row'><div class='col-2 col-sm-2 col-md-2 col-lg-2 img-fluid'>
                                    <img src='" + collection.Chats[i].CreatedBy.Picture.ImageURL + @"'></div><div class='col-6 col-sm-6 col-md-7 col-lg-7'><div class='row'>
                                    <div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6 class='c-w'>" + collection.Chats[i].CreatedBy.FullName + @"</h6>
                                    <p class='c-w'>" + collection.Chats[i].Thread + @"</p></div><div class='col-12 col-sm-12 col-md-12 col-lg-12'></div>
                                    </div></div><div class='col-2 col-sm-2 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-gray br' value='View'
                                    onclick='route(" + uri + @")'></div><div class='col-2 col-sm-2 col-md-1 col-lg-1 comment-icon-container'>
                                    <i class='far fa-comments c-w'></i></div></div></div></div></div>";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
                return;
        }

        [WebMethod]
        public static string SubmitChat(string thread, string userID)
        {
            string ChatID = "0";

            try
            {
                Chat chat = new Chat();

                chat.Thread = thread;

                BNUser user = new BNUser();

                user.UserID = userID;

                chat.CreatedBy = user;

                chat.ID = chat.PostMessage(chat);

                if (chat.ID != 0)
                    ChatID = chat.ID.ToString();
            }
            catch
            {
                throw;
            }

            return ChatID;
        }
    }
}