using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendEmailHelper.Web.Util
{
    public class EmailClientResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception SmtpException { get; set; }
    }
}