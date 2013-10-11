using System.Net;
using SendEmailHelper.ServiceModel;
using SendEmailHelper.Web.Data;
using SendEmailHelper.Web.Util;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace SendEmailHelper.Web.Services
{
    public class MessageService : Service
    {
        public HttpResult Get(GetMessage request)
        {
            var ds = new DataStore();
            var message = ds.GetMessage(request.Id);
            return new HttpResult(message);
        }

        public HttpResult Post(CreateMessage request)
        {
            // POST does not support update
            //if (request.Id > 0)
            //    return new HttpResult(HttpStatusCode.NotFound, "POST does not support updating idividual messages.");

            //var id = request.Id;
            var ds = new DataStore();
            var id = ds.InsertMessage(request);

            var client = new EmailClient();
            var emailResult = client.Send(request);

            var response = new MessageResponse
            {
                Id = id,
                Success = true
            };

            if (emailResult.Success)
            {
                ds.InsertMessageStatus(id, 2);
            }
            else
            {
                ds.InsertMessageStatus(id, 3, emailResult.Message, emailResult.SmtpException);
                response.Result = emailResult.Message;
                response.Success = false;
            }

            var result = new HttpResult(response)
            {
                StatusCode = HttpStatusCode.Created,
                StatusDescription = RequestContext.AbsoluteUri.CombineWith(id)
            };

            return result;
        }

        public HttpResult Put(UpdateMessage request)
        {
            // PUT does not support create
            if (request.Id <= 0) return new HttpResult(HttpStatusCode.NotFound, "Message was not found.");

            var ds = new DataStore();
            ds.UpdateMessage(request);

            // Resend e-mail if requested
            if (request.ResendEmail)
            {
                var savedMesssage = ds.GetMessage(request.Id);
                var client = new EmailClient();
                var emailResult = client.Send(savedMesssage);

                if (emailResult.Success)
                {
                    ds.InsertMessageStatus(request.Id, 2);
                }
                else
                {
                    ds.InsertMessageStatus(request.Id, 3, emailResult.Message, emailResult.SmtpException);
                    return new HttpResult(HttpStatusCode.OK, "Message was updated, but e-mail failed to send.");
                }
            }

            return new HttpResult(HttpStatusCode.OK, "Message was updated.");
        }

        public HttpResult Patch(PatchMessage request)
        {
            if (request.Id <= 0) return new HttpResult(HttpStatusCode.NotFound, "Message was not found.");

            // Only resend e-mail supported by patch at the moment
            if (!request.ResendEmail) return new HttpResult(HttpStatusCode.NotImplemented, "Message PATCH only supports resending e-mail.");

            var ds = new DataStore();
            var message = ds.GetMessage(request.Id);

            var client = new EmailClient();
            var emailResult = client.Send(message);

            if (emailResult.Success)
            {
                ds.InsertMessageStatus(request.Id, 2);
            }
            else
            {
                ds.InsertMessageStatus(request.Id, 3, emailResult.Message, emailResult.SmtpException);
                return new HttpResult(HttpStatusCode.OK, "Message failed to send.");
            }

            return new HttpResult(HttpStatusCode.OK, "Message was updated.");
        }
    }
}