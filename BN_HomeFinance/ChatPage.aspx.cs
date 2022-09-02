using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BN_HomeFinance.Resources;
using System.Web.Services;

namespace BN_HomeFinance
{
    public partial class ChatPage : System.Web.UI.Page
    {
        public string html = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session[Helper.Sessions.User.ToString()] != null)
                {
                    HDCurrentUser.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).UserID;
                    HDFullName.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).FullName;
                    HDUrl.Value = ((BNUser)Session[Helper.Sessions.User.ToString()]).Picture.ImageURL;

                    if (Request.QueryString[Helper.QueryStrings.ChatID.ToString()] != null)
                    {
                        HDChatID.Value = Request.QueryString[Helper.QueryStrings.ChatID.ToString()].ToString();

                        Chat chat = new Chat();

                        chat = chat.GetChat(Request.QueryString[Helper.QueryStrings.ChatID.ToString()].ToString());

                        html += @"<div class='row mt-1 chat-container'><div class='col-12 col-sm-12 col-md-12 col-lg-12 message-container'>
                                            <div class='chat-message mt-2 w-100'><div class='row'><div class='col-2 col-sm-2 col-md-2 col-lg-2 img-fluid'>
                                            <img src='" + chat.CreatedBy.Picture.ImageURL + @"'></div><div class='col-6 col-sm-6 col-md-7 col-lg-7'><div class='row'>
                                            <div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6 class='c-w'>" + chat.CreatedBy.FullName + @"</h6>
                                            <p class='c-w'>" + chat.Thread + @"</p></div><div class='col-12 col-sm-12 col-md-12 col-lg-12'></div>
                                            </div></div><div class='col-2 col-sm-2 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-gray br' value='Reply' onclick='ViewSubReply(this)'></div>
                                            <div class='col-2 col-sm-2 col-md-1 col-lg-1 comment-icon-container'>
                                            <i class='far fa-comments c-w'></i></div><div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'>
                                            <div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'><input type='text' class='form-control' placeholder='Enter Reply...' /></div>
                                            <div class='col-3 col-sm-3 col-md-2 col-lg-2'><input type='button' class='btn bg-gray br c-b btn-submit f-r' value='Submit' onclick='SubmitReply(this);'>
                                            </div></div></div></div>";

                        if (chat.Messages.Count > 0)
                        {
                            for (int count = 0; count < chat.Messages.IDs.Count; count++)
                            {
                                html += @"<div class='reply'><div class='row reply-box'><div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'>
                                              <img src='" + chat.Messages.CreatedBy[count].Picture.ImageURL + @"'></div><div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'>
                                              <div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'><h6>" + chat.Messages.CreatedBy[count].FullName + @"</h6>
                                              <p>" + chat.Messages.Messsages[count] + @"</p>
                                              </div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><input type='button' class='btn bg-purple 
                                              br c-w' value='Reply' onclick='ViewSubReply(this)'></div><div class='col-12 col-sm-12 col-md-12 col-lg-12 text-reply-fluid'>
                                              <div class='row'><div class='col-9 col-sm-9 col-md-10 col-lg-10'><input type='text' class='form-control' placeholder='Enter Reply...' />
                                              </div><div class='col-3 col-sm-3 col-md-2 col-lg-2'><div class='d-none m-id'><input type='text' value='" + chat.Messages.IDs[count].ToString() + @"'></div>
                                              <input type='button' class='btn bg-purple br c-w btn-submit' value='Submit' onclick='SubmitSubReply(this);'>
                                              </div></div></div>
                                              <!-- Sub reply to be added here -->";

                                if (chat.Messages.Replies[count].Count > 0)
                                {
                                    for (int iter = 0; iter < chat.Messages.Replies[count].IDs.Count; iter++)
                                    {
                                        html += @"<div class=' col-12 col-sm-12 col-md-12 col-lg-12 sub-reply'><div class='row bg-gray c-b sub-reply-box'>
                                              <div class='col-3 col-sm-3 col-md-2 col-lg-2 img-fluid-reply'><img src='" + chat.Messages.Replies[count].CreatedBy[iter].Picture.ImageURL + @"'></div>
                                              <div class='col-6 col-sm-6 col-md-8 col-lg-8'><div class='row'><div class='col-12 col-sm-12 col-md-12 col-lg-12 chat-text'>
                                              <h6>" + chat.Messages.Replies[count].CreatedBy[iter].FullName + @"</h6><p>" + chat.Messages.Replies[count].Replies[iter] + @"</p>
                                              </div></div></div><div class='col-3 col-sm-3 col-md-2 col-lg-2 btn-reply-fluid'><i class='fas fa-reply c-p .fs-7'></i></div></div></div>";
                                    }
                                }

                                html += @"<!-- Sub reply to be added here -->
                                        </div></div>";
                            }
                        }

                        html += @"<!-- Reply Added Here -->
                            </div></div></div>";
                    }
                }
                else
                {
                    if (Request.QueryString[Helper.QueryStrings.ChatID.ToString()] != null)
                    {
                        Session[Helper.Sessions.Source.ToString()] = Helper.pg_ChatPage + "?" + Helper.QueryStrings.ChatID.ToString() + "=" + Request.QueryString[Helper.QueryStrings.ChatID.ToString()].ToString();
                        Response.Redirect(Helper.pg_Login);
                    }
                }
            }
            else
            {
                return;
            }
        }

        [WebMethod]
        public static bool SubmitReply(string MessageID, string Reply, string UserID)
        {
            try
            {
                ChatMessgeReplies reply = new ChatMessgeReplies();

                reply.IDs.Add(Convert.ToInt32(MessageID));
                reply.Replies.Add(Reply);
                BNUser user = new BNUser();
                user.UserID = UserID;
                reply.CreatedBy.Add(user);
                reply.Count = 1;

                reply.InsertReply(reply);
            }
            catch
            {
                throw;
            }

            return true;
        }

        [WebMethod]
        public static string SubmitMessage(string ChatID, string Message, string UserID)
        {
            string id = "";
            try
            {
                ChatMessages message = new ChatMessages();

                message.IDs.Add(Convert.ToInt32(ChatID));
                message.Messsages.Add(Message);
                BNUser user = new BNUser();
                user.UserID = UserID;
                message.CreatedBy.Add(user);
                message.Count = 1;

                id = message.InsertMessage(message);
            }
            catch
            {
                throw;
            }

            return id;
        }
    }
}