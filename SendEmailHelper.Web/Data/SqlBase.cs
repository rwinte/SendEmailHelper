using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SendEmailHelper.Web.Data
{
    public abstract class SqlBase
    {
        protected SqlConnection Connection;
        protected SqlCommand Command;

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SendEmailHelper"].ConnectionString;
            }
        }
    }
}