using System;
using SendEmailHelper.ServiceModel;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

namespace SendEmailHelper.ConsoleClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            string input;
            int choice;
            object response;
            var client = new JsonServiceClient("http://localhost:59200/");

            Console.WriteLine("Options:");
            Console.WriteLine("1. /message GET");
            Console.WriteLine("2. /message POST");
            Console.WriteLine("3. /messages GET");
            Console.WriteLine("q. Quit");
            do
            {
                Console.Write("Please make a choice: ");
                input = Console.ReadLine();

                if (input != null && input.Equals("q"))
                {
                    return;
                }

                if (!int.TryParse(input, out choice))
                {
                    Console.Write("Invalid choice. Please make a choice: ");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        response = client.Get(new GetMessage {Id = 1});
                        response.PrintDump();
                        break;
                    case 2:
                        var request = new CreateMessage
                        {
                            ApplicationId = 1,
                            Bcc = new[] {"test@test.com"},
                            Body = "This is a test email.",
                            Cc = new[] {"test2@test2.com"},
                            Connection = new Connection
                            {
                                EnableSsl = false,
                                Host = "localhost",
                                Port = 25
                            },
                            Credential = new Credential(),
                            From = "test3@test3.com",
                            ReplyTo = new[] {"test3@test3.com"},
                            Sender = "test3@test3.com",
                            Subject = "Test Message",
                            To = new[] {"test4@test4.com"}
                        };
                        response = client.Post(request);
                        response.PrintDump();
                        break;
                    case 3:
                        response = client.Get(new Messages {ApplicationId = 1});
                        response.PrintDump();
                        break;
                }
            } while (input != null);
        }
    }
}
