using System;
using System.Net;
using System.Net.Mail;
using SendEmailHelper.ServiceModel;

namespace SendEmailHelper.Web.Util
{
    public class EmailClient
    {
        public EmailClientResult Send(IMessage request)
        {
            try
            {
                int port = request.Connection.Port;
                var client = new SmtpClient(request.Connection.Host, port > 0 ? port : 25);
                var message = new MailMessage
                {
                    Body = request.Body,
                    From = new MailAddress(request.From),
                    Sender = new MailAddress(request.Sender),
                    Subject = request.Subject
                };

                // If credential username is provided, assume credentials are desired
                if (request.Credential != null && !string.IsNullOrWhiteSpace(request.Credential.Username))
                {
                    var credential = new NetworkCredential(request.Credential.Username, request.Credential.Password,
                        request.Credential.Domain);
                    client.Credentials = credential;
                }

                foreach (var to in request.To)
                {
                    message.To.Add(to);
                }

                foreach (var cc in request.Cc)
                {
                    message.CC.Add(cc);
                }

                foreach (var bcc in request.Bcc)
                {
                    message.Bcc.Add(bcc);
                }

                foreach (var replyto in request.ReplyTo)
                {
                    message.ReplyToList.Add(replyto);
                }

                client.Send(message);
            }
            catch (Exception ex)
            {
                return new EmailClientResult
                {
                    Message = ex.Message,
                    SmtpException = ex,
                    Success = false
                };
            }

            return new EmailClientResult
            {
                Message = "Success",
                Success = true
            };
        }
    }
}