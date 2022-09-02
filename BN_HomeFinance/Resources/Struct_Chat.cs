using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BN_HomeFinance.Resources
{
    public class Chat : ChatFactory
    {
        public int ID { get; set; }

        private BNUser createdBy = new BNUser();
        public BNUser CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public string Thread { get; set; }

        public DateTime CreatedAt { get; set; }

        private ChatMessages messages = new ChatMessages();
        public ChatMessages Messages
        {
            get { return messages; }
            set { messages = value; }
        }
    }

    public class ChatCollection : ChatFactory
    {

        private List<Chat> chats = new List<Chat>();
        public List<Chat> Chats
        {
            get { return chats; }
            set { chats = value; }
        }

        public int Count { get; set; }
    }

    public class ChatMessages : ChatFactory
    {
        private List<int> iD = new List<int>();
        public List<int> IDs
        {
            get { return iD; }
            set { iD = value; }
        }

        private List<string> messages = new List<string>();
        public List<string> Messsages
        {
            get { return messages; }
            set { messages = value; }
        }

        private List<DateTime> createdAt = new List<DateTime>();
        public List<DateTime> CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        private List<BNUser> createdBy = new List<BNUser>();
        public List<BNUser> CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        private List<ChatMessgeReplies> replies = new List<ChatMessgeReplies>();
        public List<ChatMessgeReplies> Replies
        {
            get { return replies; }
            set { replies = value; }
        }

        public int Count { get; set; }
    }

    public class ChatMessgeReplies : ChatFactory
    {
        private List<int> iD = new List<int>();
        public List<int> IDs
        {
            get { return iD; }
            set { iD = value; }
        }

        private List<string> replies = new List<string>();
        public List<string> Replies
        {
            get { return replies; }
            set { replies = value; }
        }

        private List<DateTime> createdAt = new List<DateTime>();
        public List<DateTime> CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        private List<BNUser> createdBy = new List<BNUser>();
        public List<BNUser> CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public int Count { get; set; }
    }

    public class ChatConstants
    {
        #region Columns

        public const string col_ChatID = "ChatID";
        public const string col_Thread = "Thread";
        public const string col_MessageID = "MessageID";
        public const string col_Message = "Message";
        public const string col_ReplyID = "ReplyID";
        public const string col_Reply = "Reply";
        public const string col_CreatedAt = "CreatedAt";
        public const string col_CreatedBy = "CreatedBy";

        #endregion

        #region Procedures

        public const string proc_GetChats = "GetChats";
        public const string proc_GetChat = "GetChat";
        public const string proc_GetChatMessages = "GetChatMessages";
        public const string proc_GetChatMessageReplies = "GetChatMessageReplies";
        public const string proc_GetCompleteChat = "GetCompleteChat";
        public const string proc_SubmitReply = "SubmitReply";
        public const string proc_SubmitMessage = "SubmitMessage";
        public const string proc_SubmitPost = "SubmitPost";
        public const string proc_GetRecentChats = "GetRecentChats";
        public const string proc_GetAllChats = "GetAllChats";

        #endregion

        #region Parameters

        public const string par_ChatID = "@ChatID";
        public const string par_Thread = "@Thread";
        public const string par_MessageID = "@MessageID";
        public const string par_Message = "@Message";
        public const string par_ReplyID = "@ReplyID";
        public const string par_Reply = "@Reply";
        public const string par_CreatedAt = "@CreatedAt";
        public const string par_CreatedBy = "@CreatedBy";

        #endregion
    }

    public class ChatFactory
    {
        public Chat GetChat(string ChatID)
        {
            Chat ch = new Chat();

            try
            {
                DataSet ds = Helper.ExecuteSPWithParametersWithDataSet(ChatConstants.proc_GetCompleteChat, ChatConstants.par_ChatID, ChatID);

                DataTable dtChats = ds.Tables[0];
                DataTable dtChatsMessages = ds.Tables[1];
                DataTable dtChatReplies = ds.Tables[2];

                if (dtChats != null && dtChats.Rows.Count > 0)
                {
                    ch.ID = Convert.ToInt32(dtChats.Rows[0][ChatConstants.col_ChatID]);
                    ch.Thread = dtChats.Rows[0][ChatConstants.col_Thread].ToString();
                    ch.CreatedAt = Convert.ToDateTime(dtChats.Rows[0][ChatConstants.col_CreatedAt]);
                    ch.CreatedBy.UserID = dtChats.Rows[0][User_Constants.Col_UserID].ToString();
                    ch.CreatedBy.FullName = dtChats.Rows[0][User_Constants.Col_FullName].ToString();
                    ch.CreatedBy.UserType.UserTypeID = dtChats.Rows[0][User_Constants.Col_UserTypeID].ToString();
                    ch.CreatedBy.UserType.UserType = dtChats.Rows[0][User_Constants.Col_UserType].ToString();

                    if (dtChats.Rows[0][BNImage_Constants.Col_ImageID].ToString() != "")
                    {
                        ch.CreatedBy.Picture.ImageID = dtChats.Rows[0][BNImage_Constants.Col_ImageID].ToString();
                        ch.CreatedBy.Picture.ImageName = dtChats.Rows[0][BNImage_Constants.Col_ImageName].ToString();
                        ch.CreatedBy.Picture.ImageData = (byte[])dtChats.Rows[0][BNImage_Constants.Col_ImageData];
                        ch.CreatedBy.Picture.ImageURL = Helper.ConvertUrlFromByteArray(ch.CreatedBy.Picture.ImageData);
                    }
                    else
                    {
                        ch.CreatedBy.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                    }

                    if (dtChatsMessages != null && dtChatsMessages.Rows.Count > 0)
                    {
                        ch.Messages.Count = 0;

                        for (int count = 0; count < dtChatsMessages.Rows.Count; count++)
                        {
                            ch.Messages.IDs.Add(Convert.ToInt32(dtChatsMessages.Rows[count][ChatConstants.col_MessageID]));
                            ch.Messages.Messsages.Add(dtChatsMessages.Rows[count][ChatConstants.col_Message].ToString());
                            ch.Messages.CreatedAt.Add((Convert.ToDateTime(dtChatsMessages.Rows[count][ChatConstants.col_CreatedAt])));

                            BNUser user = new BNUser();
                            user.UserID = dtChatsMessages.Rows[count][User_Constants.Col_UserID].ToString();
                            user.FullName = dtChatsMessages.Rows[count][User_Constants.Col_FullName].ToString();
                            user.UserType.UserTypeID = dtChatsMessages.Rows[count][User_Constants.Col_UserTypeID].ToString();
                            user.UserType.UserType = dtChatsMessages.Rows[count][User_Constants.Col_UserType].ToString();

                            if (dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageID].ToString() != "")
                            {
                                user.Picture.ImageID = dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageID].ToString();
                                user.Picture.ImageName = dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageName].ToString();
                                user.Picture.ImageData = (byte[])dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageData];
                                user.Picture.ImageURL = Helper.ConvertUrlFromByteArray(user.Picture.ImageData);
                            }
                            else
                            {
                                user.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                            }

                            ch.Messages.CreatedBy.Add(user);
                            ch.Messages.Count++;

                            ChatMessgeReplies re = new ChatMessgeReplies();
                            re.Count = 0;

                            if (dtChatReplies != null && dtChatReplies.Rows.Count > 0)
                            {
                                for (int iter = 0; iter < dtChatReplies.Rows.Count; iter++)
                                {
                                    if (dtChatsMessages.Rows[count][ChatConstants.col_MessageID].ToString() ==
                                        dtChatReplies.Rows[iter][ChatConstants.col_MessageID].ToString())
                                    {
                                        re.IDs.Add(Convert.ToInt32(dtChatReplies.Rows[iter][ChatConstants.col_ReplyID]));
                                        re.Replies.Add(dtChatReplies.Rows[iter][ChatConstants.col_Reply].ToString());
                                        re.CreatedAt.Add(Convert.ToDateTime(dtChatReplies.Rows[iter][ChatConstants.col_CreatedAt]));

                                        BNUser user2 = new BNUser();

                                        user2.UserID = dtChatReplies.Rows[iter][User_Constants.Col_UserID].ToString();
                                        user2.FullName = dtChatReplies.Rows[iter][User_Constants.Col_FullName].ToString();
                                        user2.UserType.UserTypeID = dtChatReplies.Rows[iter][User_Constants.Col_UserTypeID].ToString();
                                        user2.UserType.UserType = dtChatReplies.Rows[iter][User_Constants.Col_UserType].ToString();

                                        if (dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageID].ToString() != "")
                                        {
                                            user2.Picture.ImageID = dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageID].ToString();
                                            user2.Picture.ImageName = dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageName].ToString();
                                            user2.Picture.ImageData = (byte[])dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageData];
                                            user2.Picture.ImageURL = Helper.ConvertUrlFromByteArray(user2.Picture.ImageData);
                                        }
                                        else
                                        {
                                            user2.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                                        }

                                        re.CreatedBy.Add(user2);
                                        re.Count = iter + 1;
                                    }
                                }
                            }

                            ch.Messages.Replies.Add(re);
                        }
                    }
                    else
                    {
                        ch.Messages.Count = 0;
                    }
                }
            }
            catch
            {
                throw;
            }

            return ch;
        }

        public bool InsertReply(ChatMessgeReplies reply)
        {
            bool b = false;

            try
            {
                if (reply != null && reply.Count > 0)
                {
                    if (Helper.ExecuteSPScalarWithParameters(ChatConstants.proc_SubmitReply, ChatConstants.par_MessageID, reply.IDs[0].ToString(),
                         ChatConstants.par_Reply, reply.Replies[0], ChatConstants.par_CreatedBy, new Guid(reply.CreatedBy[0].UserID)) != "")
                        b = true;
                }
            }
            catch
            {
                throw;
            }
            return b;
        }

        public string InsertMessage(ChatMessages message)
        {
            string b = "0";

            try
            {
                if (message != null && message.Count > 0)
                {
                    b = Helper.ExecuteSPScalarWithParameters(ChatConstants.proc_SubmitMessage, ChatConstants.par_ChatID, message.IDs[0].ToString(),
                         ChatConstants.par_Message, message.Messsages[0], ChatConstants.par_CreatedBy, new Guid(message.CreatedBy[0].UserID));
                }
            }
            catch
            {
                throw;
            }
            return b;
        }

        public int PostMessage(Chat chat)
        {
            int ID = 0;

            try
            {
                if (chat != null)
                {
                    ID = Helper.ExecuteSPScalarWithParametersIntChat(ChatConstants.proc_SubmitPost,
                         ChatConstants.par_Thread, chat.Thread, ChatConstants.par_CreatedBy, new Guid(chat.CreatedBy.UserID));
                }
            }
            catch
            {
                throw;
            }
            return ID;
        }

        public ChatCollection GetRecentChats()
        {
            ChatCollection collection = new ChatCollection();

            try
            {
                DataSet ds = Helper.ExecuteSPWithNoParametersDataSet(ChatConstants.proc_GetRecentChats);

                DataTable dtChats = ds.Tables[0];
                DataTable dtChatsMessages = ds.Tables[1];
                DataTable dtChatReplies = ds.Tables[2];

                if (dtChats != null && dtChats.Rows.Count > 0)
                {
                    collection.Count = 0;

                    for (int i = 0; i < dtChats.Rows.Count; i++)
                    {
                        Chat ch = new Chat();

                        ch.ID = Convert.ToInt32(dtChats.Rows[i][ChatConstants.col_ChatID]);
                        ch.Thread = dtChats.Rows[i][ChatConstants.col_Thread].ToString();
                        ch.CreatedAt = Convert.ToDateTime(dtChats.Rows[i][ChatConstants.col_CreatedAt]);
                        ch.CreatedBy.UserID = dtChats.Rows[i][User_Constants.Col_UserID].ToString();
                        ch.CreatedBy.FullName = dtChats.Rows[i][User_Constants.Col_FullName].ToString();
                        ch.CreatedBy.UserType.UserTypeID = dtChats.Rows[i][User_Constants.Col_UserTypeID].ToString();
                        ch.CreatedBy.UserType.UserType = dtChats.Rows[i][User_Constants.Col_UserType].ToString();

                        if (dtChats.Rows[i][BNImage_Constants.Col_ImageID].ToString() != "")
                        {
                            ch.CreatedBy.Picture.ImageID = dtChats.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                            ch.CreatedBy.Picture.ImageName = dtChats.Rows[i][BNImage_Constants.Col_ImageName].ToString();
                            ch.CreatedBy.Picture.ImageData = (byte[])dtChats.Rows[i][BNImage_Constants.Col_ImageData];
                            ch.CreatedBy.Picture.ImageURL = Helper.ConvertUrlFromByteArray(ch.CreatedBy.Picture.ImageData);
                        }
                        else
                        {
                            ch.CreatedBy.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                        }

                        if (dtChatsMessages != null && dtChatsMessages.Rows.Count > 0)
                        {
                            ch.Messages.Count = 0;

                            for (int count = 0; count < dtChatsMessages.Rows.Count; count++)
                            {
                                if (ch.ID == Convert.ToInt16(dtChatsMessages.Rows[count][ChatConstants.col_ChatID]))
                                {
                                    ch.Messages.IDs.Add(Convert.ToInt32(dtChatsMessages.Rows[count][ChatConstants.col_MessageID]));
                                    ch.Messages.Messsages.Add(dtChatsMessages.Rows[count][ChatConstants.col_Message].ToString());
                                    ch.Messages.CreatedAt.Add((Convert.ToDateTime(dtChatsMessages.Rows[count][ChatConstants.col_CreatedAt])));

                                    BNUser user = new BNUser();
                                    user.UserID = dtChatsMessages.Rows[count][User_Constants.Col_UserID].ToString();
                                    user.FullName = dtChatsMessages.Rows[count][User_Constants.Col_FullName].ToString();
                                    user.UserType.UserTypeID = dtChatsMessages.Rows[count][User_Constants.Col_UserTypeID].ToString();
                                    user.UserType.UserType = dtChatsMessages.Rows[count][User_Constants.Col_UserType].ToString();

                                    if (dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageID].ToString() != "")
                                    {
                                        user.Picture.ImageID = dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageID].ToString();
                                        user.Picture.ImageName = dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageName].ToString();
                                        user.Picture.ImageData = (byte[])dtChatsMessages.Rows[count][BNImage_Constants.Col_ImageData];
                                        user.Picture.ImageURL = Helper.ConvertUrlFromByteArray(user.Picture.ImageData);
                                    }
                                    else
                                    {
                                        user.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                                    }

                                    ch.Messages.CreatedBy.Add(user);
                                    ch.Messages.Count++;

                                    if (dtChatReplies != null && dtChatReplies.Rows.Count > 0)
                                    {
                                        ChatMessgeReplies re = new ChatMessgeReplies();

                                        for (int iter = 0; iter < dtChatReplies.Rows.Count; iter++)
                                        {
                                            if (dtChatsMessages.Rows[count][ChatConstants.col_MessageID].ToString() ==
                                                dtChatReplies.Rows[iter][ChatConstants.col_MessageID].ToString())
                                            {
                                                re.IDs.Add(Convert.ToInt32(dtChatReplies.Rows[iter][ChatConstants.col_ReplyID]));
                                                re.Replies.Add(dtChatReplies.Rows[iter][ChatConstants.col_Reply].ToString());
                                                re.CreatedAt.Add(Convert.ToDateTime(dtChatReplies.Rows[iter][ChatConstants.col_CreatedAt]));

                                                BNUser user2 = new BNUser();

                                                user2.UserID = dtChatReplies.Rows[iter][User_Constants.Col_UserID].ToString();
                                                user2.FullName = dtChatReplies.Rows[iter][User_Constants.Col_FullName].ToString();
                                                user2.UserType.UserTypeID = dtChatReplies.Rows[iter][User_Constants.Col_UserTypeID].ToString();
                                                user2.UserType.UserType = dtChatReplies.Rows[iter][User_Constants.Col_UserType].ToString();

                                                if (dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageID].ToString() != "")
                                                {
                                                    user2.Picture.ImageID = dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageID].ToString();
                                                    user2.Picture.ImageName = dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageName].ToString();
                                                    user2.Picture.ImageData = (byte[])dtChatReplies.Rows[iter][BNImage_Constants.Col_ImageData];
                                                    user2.Picture.ImageURL = Helper.ConvertUrlFromByteArray(user2.Picture.ImageData);
                                                }
                                                else
                                                {
                                                    user2.Picture.ImageURL = "/Resources/Images/Unknown.jpg";
                                                }

                                                re.CreatedBy.Add(user2);
                                                re.Count = iter + 1;
                                            }
                                        }

                                        ch.Messages.Replies.Add(re);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ch.Messages.Count = 0;
                        }

                        collection.Count++;
                        collection.Chats.Add(ch);
                    }
                }
            }
            catch
            {
                throw;
            }

            return collection;
        }

        public ChatCollection GetAllChats()
        {
            ChatCollection chats = new ChatCollection();
            chats.Count = 0;

            try
            {
                DataTable dt = Helper.ExecuteSPWithNoParameters(ChatConstants.proc_GetAllChats);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Chat chat = new Chat();

                        chat.ID = Convert.ToInt32(dt.Rows[i][ChatConstants.col_ChatID]);
                        chat.Thread = dt.Rows[i][ChatConstants.col_Thread].ToString();
                        chat.CreatedAt = Convert.ToDateTime(dt.Rows[i][ChatConstants.col_CreatedAt]);
                        chat.CreatedBy.FullName = dt.Rows[i][User_Constants.Col_FullName].ToString();
                        chat.CreatedBy.Picture.ImageID = dt.Rows[i][BNImage_Constants.Col_ImageID].ToString();
                        chat.CreatedBy.Picture.ImageData = (byte[])dt.Rows[i][BNImage_Constants.Col_ImageData];

                        if (chat.CreatedBy.Picture.ImageData != null)
                            chat.CreatedBy.Picture.ImageURL = Helper.ConvertUrlFromByteArray(chat.CreatedBy.Picture.ImageData);
                        else
                            chat.CreatedBy.Picture.ImageURL = "~/Resources/Images/Unknown.jpg";

                        chats.Chats.Add(chat);
                        chats.Count++;
                    }
                }
            }
            catch
            {

            }

            return chats;
        }
    }
}