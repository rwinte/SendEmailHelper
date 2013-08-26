using System.Collections.Generic;
using SendEmailHelper.ServiceModel;
using SendEmailHelper.Web.Data;
using ServiceStack.ServiceInterface;

namespace SendEmailHelper.Web.Services
{
    public class ApplicationsService : Service
    {
        public List<Application> Any(Application request)
        {
            var dataStore = new DataStore();
            return dataStore.GetAllApplications();
        } 
    }
}