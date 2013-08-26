using SendEmailHelper.ServiceModel;
using SendEmailHelper.Web.Data;
using ServiceStack.ServiceInterface;

namespace SendEmailHelper.Web.Services
{
    public class MessagesService : Service
    {
        public MessagesResponse Get(Messages request)
        {
            var dataStore = new DataStore();
            var list = dataStore.GetMessagesByApplicationId(request.ApplicationId);

            return new MessagesResponse
            {
                Messages = list
            };
        }
    }
}