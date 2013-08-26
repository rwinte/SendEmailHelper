using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

namespace SendEmailHelper.ServiceModel
{
    [Route("/applications")]
    public class Application
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
    }
}
