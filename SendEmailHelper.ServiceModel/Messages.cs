using System;
using System.Collections.Generic;
using SendEmailHelper.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace SendEmailHelper.ServiceModel
{   
    [Route("/messages", "GET, OPTIONS")]
    [Route("/messages/applications/{ApplicationId}")]
    public class Messages : IReturn<MessagesResponse>
    {
        public int ApplicationId { get; set; }
    }

    [Route("/messages/{Id}")]
    public class GetMessage : IReturn<Message>
    {
        public int Id { get; set; }
    }

    [Route("/messages", "POST")]
    public class CreateMessage : IMessage, IReturn<MessageResponse>
    {
        public int ApplicationId { get; set; }
        public IEnumerable<string> To { get; set; }
        public string From { get; set; }
        public string Sender { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public IEnumerable<string> ReplyTo { get; set; }
        public string Subject { get; set; }
        //public string Headers { get; set; }
        public string Body { get; set; }
        public Connection Connection { get; set; }
        public Credential Credential { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
    }

    [Route("/messages", "PUT")]
    public class UpdateMessage : IMessage, IReturn<MessageResponse>
    {
        public int Id { get; set; }
        public bool ResendEmail { get; set; }
        public int ApplicationId { get; set; }
        public IEnumerable<string> To { get; set; }
        public string From { get; set; }
        public string Sender { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public IEnumerable<string> ReplyTo { get; set; }
        public string Subject { get; set; }
        //public string Headers { get; set; }
        public string Body { get; set; }
        public Connection Connection { get; set; }
        public Credential Credential { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
    }

    [Route("/messages", "PATCH")]
    public class PatchMessage : IReturn<MessageResponse>
    {
        // Patch currently supports on resending e-mails
        public int Id { get; set; }
        public bool ResendEmail { get; set; }
    }

    public class MessageResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }

    public class MessagesResponse
    {
        public IEnumerable<Message> Messages { get; set; }
    }
}