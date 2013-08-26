using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailHelper.ServiceModel
{
    public class MessageStatus
    {
        public int MessageId { get; set; }
        public int TypeMessageStatusId { get; set; }
        public string Description { get; set; }
        public string AdditionalMessage { get; set; }
        public Exception Exception { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
