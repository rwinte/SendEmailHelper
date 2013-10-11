using System;
using System.Collections.Generic;
using SendEmailHelper.ServiceModel;
using SendEmailHelper.ServiceModel.Types;
using SendEmailHelper.Web.Data;
using ServiceStack.ServiceInterface;

namespace SendEmailHelper.Web.Services
{
    public class MessagesService : Service
    {
        public List<Message> Get(Messages request)
        {
            var dataStore = new DataStore();
            var list = dataStore.GetMessagesByApplicationId(request.ApplicationId);
            return list;
        }
    }
}