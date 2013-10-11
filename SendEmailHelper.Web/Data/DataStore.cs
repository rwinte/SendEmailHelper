using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SendEmailHelper.ServiceModel;
using SendEmailHelper.ServiceModel.Types;
using ServiceStack.Text;

namespace SendEmailHelper.Web.Data
{
    // TODO: Maybe change this to use DataMapper pattern or some other pattern?
    // TODO: Extract an interface from this
    // TODO: Maybe split this up into separate interfaces/classes per DTO?
    public class DataStore
    {
        public List<Application> GetAllApplications()
        {
            var list = new List<Application>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[SelectAllApplications]", CommandType.StoredProcedure))
            {
                while (proxy.Read())
                {
                    var app = new Application
                    {
                        Id = (int)proxy["Id"],
                        ApplicationName = proxy["ApplicationName"] as string,
                    };

                    list.Add(app);
                }
            }

            return list;
        }

        public Message GetMessage(int id)
        {
            Message msg = null;

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageSelectById]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@Id", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    msg = new Message
                    {
                        Id = (int) proxy["Id"],
                        From = proxy["From"] as string,
                        Sender = proxy["Sender"] as string,
                        Subject = proxy["Subject"] as string,
                        Body = proxy["Body"] as string,
                        To = GetMessageToByMessageId(id),
                        ReplyTo = GetMessageReplyToByMessageId(id),
                        Cc = GetMessageCcByMessageId(id),
                        Bcc = GetMessageBccByMessageId(id),
                        CreateDate = proxy.GetDateTime(proxy.GetOrdinal("CreateDate")),
                        Status = GetCurrentMessageStatusByMessageId(id),
                        Connection = new Connection
                        {
                            EnableSsl = proxy.GetBoolean(proxy.GetOrdinal("EnableSsl")),
                            Host = proxy["Host"] as string,
                            Port = proxy.GetInt32(proxy.GetOrdinal("Port"))
                        },
                        Credential = GetCredential(id),
                    };
                }
            }

            return msg;
        }

        public List<Message> GetMessagesByApplicationId(int id)
        {
            var list = new List<Message>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageSelectByApplication]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@ApplicationId", id) {DbType = DbType.Int32});
                while (proxy.Read())
                {
                    var msg = new Message
                    {
                        Id = (int)proxy["Id"],
                        From = proxy["From"] as string,
                        Sender = proxy["Sender"] as string,
                        Subject = proxy["Subject"] as string,
                        Body = proxy["Body"] as string,
                    };

                    list.Add(msg);
                }
            }

            // Load children
            foreach (var msg in list)
            {
                msg.To = GetMessageToByMessageId(msg.Id);
                msg.ReplyTo = GetMessageReplyToByMessageId(msg.Id);
                msg.Cc = GetMessageCcByMessageId(msg.Id);
                msg.Bcc = GetMessageBccByMessageId(msg.Id);
                msg.Status = GetCurrentMessageStatusByMessageId(msg.Id);
                msg.Credential = GetCredential(msg.Id);
            }

            return list;
        }

        private IEnumerable<string> GetMessageToByMessageId(int id)
        {
            var list = new List<string>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageToSelectByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) {DbType = DbType.Int32});
                while (proxy.Read())
                {
                    list.Add(proxy["To"] as string);
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageToByMessageId(int id, SqlConnection conn, SqlTransaction trans)
        {
            var list = new List<string>();

            using (var cmd = new SqlCommand("[dbo].[MessageToSelectByMessageId]", conn, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["To"] as string);
                    }
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageCcByMessageId(int id)
        {
            var list = new List<string>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageCcSelectByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    list.Add(proxy["Cc"] as string);
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageCcByMessageId(int id, SqlConnection conn, SqlTransaction trans)
        {
            var list = new List<string>();

            using (var cmd = new SqlCommand("[dbo].[MessageCcSelectByMessageId]", conn, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["Cc"] as string);
                    }
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageBccByMessageId(int id)
        {
            var list = new List<string>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageBccSelectByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    list.Add(proxy["Bcc"] as string);
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageBccByMessageId(int id, SqlConnection conn, SqlTransaction trans)
        {
            var list = new List<string>();

            using (var cmd = new SqlCommand("[dbo].[MessageBccSelectByMessageId]", conn, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["Bcc"] as string);
                    }
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageReplyToByMessageId(int id)
        {
            var list = new List<string>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageReplyToSelectByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    list.Add(proxy["ReplyTo"] as string);
                }
            }

            return list;
        }

        private IEnumerable<string> GetMessageReplyToByMessageId(int id, SqlConnection conn, SqlTransaction trans)
        {
            var list = new List<string>();

            using (var cmd = new SqlCommand("[dbo].[MessageReplyToSelectByMessageId]", conn, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["ReplyTo"] as string);
                    }
                }
            }

            return list;
        }

        public MessageStatus GetCurrentMessageStatusByMessageId(int id)
        {
            MessageStatus status = null;

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageStatusGetMostRecentByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    status = new MessageStatus
                    {
                        MessageId = (int) proxy["MessageId"],
                        TypeMessageStatusId = (int) proxy["TypeMessageStatusId"],
                        Description = proxy["MessageStatusText"] as string,
                        AdditionalMessage = proxy["AdditionalMessage"] as string,
                        Exception = JsonSerializer.DeserializeFromString<Exception>(proxy["Exception"] as string),
                        CreateDate = (DateTime) proxy["CreateDate"]
                    };
                }
            }

            return status;
        }

        public Credential GetCredential(int id)
        {
            Credential credential = null;

            using (var proxy = new SqlDataReaderProxy("[dbo].[CredentialSelectByMessageId]", CommandType.StoredProcedure))
            {
                proxy.Parameters.Add(new SqlParameter("@MessageId", id) { DbType = DbType.Int32 });
                while (proxy.Read())
                {
                    credential = new Credential
                    {
                        Username = proxy["Username"] as string,
                        Password = proxy["Password"] as string,
                        Domain = proxy["Domain"] as string
                    };
                }
            }

            return credential;
        }

        private IEnumerable<TypeMessageStatus> GetAllTypeMessageStatus()
        {
            var list = new List<TypeMessageStatus>();

            using (var proxy = new SqlDataReaderProxy("[dbo].[MessageReplyToSelectByMessageId]", CommandType.StoredProcedure))
            {
                while (proxy.Read())
                {
                    list.Add(new TypeMessageStatus
                    {
                        Id = (int)proxy["Id"],
                        MessageStatusText = proxy["MessageStatusText"] as string
                    });
                }
            }

            return list;
        }

        public int InsertMessage(CreateMessage message)
        {
            int id = -1;

            // TODO: refactor this out of here?
            using (var conn = new SqlConnection(SqlBase.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("MessageInsert", conn, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", id).DbType = DbType.Int32;
                            cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@ApplicationId", message.ApplicationId).DbType = DbType.Int32;
                            cmd.Parameters.AddWithValue("@From", message.From).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Sender", message.Sender).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Subject", message.Subject).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Body", message.Body).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Host", message.Connection.Host).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Port", message.Connection.Port).DbType = DbType.Int32;
                            cmd.Parameters.AddWithValue("@EnableSsl", message.Connection.EnableSsl).DbType =
                                DbType.Boolean;
                            cmd.Parameters.AddWithValue("@CreateDate", DateTime.UtcNow).DbType = DbType.DateTime;
                            cmd.ExecuteNonQuery();
                            id = (int) cmd.Parameters["@Id"].Value;
                        }

                        if (message.Credential != null && !string.IsNullOrWhiteSpace(message.Credential.Username))
                        {
                            using (var cmd = new SqlCommand("CredentialInsert", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MessageId", id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@Username", message.Credential.Username).DbType =
                                    DbType.String;
                                cmd.Parameters.AddWithValue("@Password", message.Credential.Password).DbType =
                                    DbType.String;
                                cmd.Parameters.AddWithValue("@Domain", message.Credential.Domain).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (var to in message.To)
                        {
                            using (var cmd = new SqlCommand("MessageToInsert", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MessageId", id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@To", to).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (var cc in message.Cc)
                        {
                            using (var cmd = new SqlCommand("MessageCcInsert", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MessageId", id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@Cc", cc).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (var bcc in message.Bcc)
                        {
                            using (var cmd = new SqlCommand("MessageBccInsert", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MessageId", id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@Bcc", bcc).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (var replyTo in message.ReplyTo)
                        {
                            using (var cmd = new SqlCommand("MessageReplyToInsert", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MessageId", id).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@ReplyTo", replyTo).DbType = DbType.String;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                    trans.Commit();
                }
            }

            return id;
        }

        public void InsertMessageStatus(int messageId, int typeMessageStatusId, string additionalMessage = "",
            Exception exception = null)
        {
            using (var conn = new SqlConnection(SqlBase.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("MessageStatusInsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", messageId).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@TypeMessageStatusId", typeMessageStatusId).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@AdditionalMessage", additionalMessage).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Exception", JsonSerializer.SerializeToString(exception)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.UtcNow).DbType = DbType.DateTime;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMessage(UpdateMessage msg)
        {
            using (var conn = new SqlConnection(SqlBase.ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("MessageUpdate", conn, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", msg.Id).DbType = DbType.Int32;
                            cmd.Parameters.AddWithValue("@ApplicationId", NullIfZero(msg.ApplicationId)).DbType =
                                DbType.Int32;
                            cmd.Parameters.AddWithValue("@From", msg.From).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Sender", msg.Sender).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Subject", msg.Subject).DbType = DbType.String;
                            cmd.Parameters.AddWithValue("@Body", msg.Body).DbType = DbType.String;
                            if (msg.Connection != null)
                            {
                                cmd.Parameters.AddWithValue("@Host", msg.Connection.Host).DbType = DbType.String;
                                cmd.Parameters.AddWithValue("@Port", msg.Connection.Port).DbType = DbType.Int32;
                                cmd.Parameters.AddWithValue("@EnableSsl", msg.Connection.EnableSsl).DbType =
                                    DbType.Boolean;
                            }
                            cmd.ExecuteNonQuery();
                        }

                        UpdateMessageToChild(msg, conn, trans);
                        UpdateMessageCcChild(msg, conn, trans);
                        UpdateMessageBccChild(msg, conn, trans);
                        UpdateMessageReplyToChild(msg, conn, trans);

                        // TODO: update other children

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateMessageToChild(UpdateMessage msg, SqlConnection conn, SqlTransaction trans)
        {
            // Get list of messages first to determine what items need inserting or deleting
            var messageToList = GetMessageToByMessageId(msg.Id, conn, trans).ToList();

            // Delete To's
            foreach (var currentTo in messageToList.Where(currentTo => !msg.To.Any(t => t.Equals(currentTo))))
            {
                using (var cmd = new SqlCommand("MessageToDelete", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@To", currentTo).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }

            // Insert new To's
            foreach (var to in msg.To.Where(to => !messageToList.Any(t => t.Equals(to))))
            {
                using (var cmd = new SqlCommand("MessageToInsert", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@To", to).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateMessageCcChild(UpdateMessage msg, SqlConnection conn, SqlTransaction trans)
        {
            // Get list of messages first to determine what items need inserting or deleting
            var messageCcList = GetMessageCcByMessageId(msg.Id, conn, trans).ToList();

            // Delete To's
            foreach (var currentCc in messageCcList.Where(currentCc => !msg.Cc.Any(t => t.Equals(currentCc))))
            {
                using (var cmd = new SqlCommand("MessageCcDelete", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Cc", currentCc).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }

            // Insert new To's
            foreach (var cc in msg.Cc.Where(cc => !messageCcList.Any(t => t.Equals(cc))))
            {
                using (var cmd = new SqlCommand("MessageCcInsert", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Cc", cc).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateMessageBccChild(UpdateMessage msg, SqlConnection conn, SqlTransaction trans)
        {
            // Get list of messages first to determine what items need inserting or deleting
            var messageBccList = GetMessageBccByMessageId(msg.Id, conn, trans).ToList();

            // Delete To's
            foreach (var currentBcc in messageBccList.Where(currentBcc => !msg.Bcc.Any(t => t.Equals(currentBcc))))
            {
                using (var cmd = new SqlCommand("MessageBccDelete", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Bcc", currentBcc).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }

            // Insert new To's
            foreach (var bcc in msg.Bcc.Where(bcc => !messageBccList.Any(t => t.Equals(bcc))))
            {
                using (var cmd = new SqlCommand("MessageBccInsert", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Bcc", bcc).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateMessageReplyToChild(UpdateMessage msg, SqlConnection conn, SqlTransaction trans)
        {
            // Get list of messages first to determine what items need inserting or deleting
            var messageReplyToList = GetMessageReplyToByMessageId(msg.Id, conn, trans).ToList();

            // Delete To's
            foreach (var currentReplyTo in messageReplyToList.Where(currentReplyTo => !msg.ReplyTo.Any(t => t.Equals(currentReplyTo))))
            {
                using (var cmd = new SqlCommand("MessageReplyToDelete", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@ReplyTo", currentReplyTo).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }

            // Insert new To's
            foreach (var replyTo in msg.ReplyTo.Where(replyTo => !messageReplyToList.Any(t => t.Equals(replyTo))))
            {
                using (var cmd = new SqlCommand("MessageReplyToInsert", conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", msg.Id).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@ReplyTo", replyTo).DbType = DbType.String;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private object NullIfZero(int value)
        {
            if (value != 0)
                return value;
            else
                return DBNull.Value;
        }
    }
}