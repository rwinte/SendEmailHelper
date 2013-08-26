using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailHelper.ServiceModel
{
    public interface IMessage
    {
        int ApplicationId { get; set; }
        IEnumerable<string> To { get; set; }
        string From { get; set; }
        string Sender { get; set; }
        IEnumerable<string> Cc { get; set; }
        IEnumerable<string> Bcc { get; set; }
        IEnumerable<string> ReplyTo { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        Connection Connection { get; set; }
        Credential Credential { get; set; }
        MessageStatus Status { get; set; }
        DateTime CreateDate { get; set; }
    }
}
