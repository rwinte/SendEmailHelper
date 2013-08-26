using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using SendEmailHelper.ServiceModel;
using ServiceStack.ServiceClient.Web;
using Xunit;

namespace SendEmailHelper.IntegrationTests
{
    public class MesageServiceTests
    {
        [Fact]
        public void Test_PostAndGetMessage()
        {
            // Create message
            var postRequest = new CreateMessage
            {
                ApplicationId = 1,
                Bcc = new[] { "test@test.com" },
                Body = "This is a test email.",
                Cc = new[] { "test2@test2.com" },
                Connection = new Connection
                {
                    EnableSsl = false,
                    Host = "localhost",
                    Port = 25
                },
                Credential = new Credential(),
                From = "test3@test3.com",
                ReplyTo = new[] { "test3@test3.com" },
                Sender = "test3@test3.com",
                Subject = "Test Message",
                To = new[] { "test4@test4.com" }
            };
            var client = new JsonServiceClient("http://localhost:59200/");
            var postResponse = client.Post(postRequest);

            // Get message
            var getRequest = new GetMessage
            {
                Id = postResponse.Id
            };
            var getResponse = client.Get(getRequest);

            Assert.Equal(2, getResponse.Status.TypeMessageStatusId);
            Assert.Equal(postResponse.Id, getResponse.Id);
        }

        [Fact]
        public void Test_PostWithBadHostnameAndGetMessage_StatusIsFailed()
        {
            // Create message
            var postRequest = new CreateMessage
            {
                ApplicationId = 1,
                Bcc = new[] { "test@test.com" },
                Body = "This is a test email.",
                Cc = new[] { "test2@test2.com" },
                Connection = new Connection
                {
                    EnableSsl = false,
                    Host = "nonexistant",
                    Port = 25
                },
                Credential = new Credential(),
                From = "test3@test3.com",
                ReplyTo = new[] { "test3@test3.com" },
                Sender = "test3@test3.com",
                Subject = "Test Message",
                To = new[] { "test4@test4.com" }
            };
            var client = new JsonServiceClient("http://localhost:59200/");
            var postResponse = client.Post(postRequest);

            // Get message
            var getRequest = new GetMessage
            {
                Id = postResponse.Id
            };
            var getResponse = client.Get(getRequest);

            Assert.Equal(3, getResponse.Status.TypeMessageStatusId);
            Assert.Equal(postResponse.Id, getResponse.Id);
        }
    }
}
