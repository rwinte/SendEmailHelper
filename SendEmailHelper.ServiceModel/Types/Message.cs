using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailHelper.ServiceModel.Types
{
    public class Message : IMessage
    {
        public int Id { get; set; }
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
}
