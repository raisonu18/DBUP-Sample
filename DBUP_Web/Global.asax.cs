using DbUp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DBUP_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AddDbMigrations();
        }
        private void AddDbMigrations()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestEntities"].ConnectionString;
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .Build();

            var result = upgrader.PerformUpgrade();
            if (result.Error != null)
            {
                throw new MigrationException(result.Error, connectionString);
            }
        }
        public class MigrationException : Exception
        {
            private readonly Exception _exception;
            private readonly string _connectionString;
            private readonly string message;

            public MigrationException(Exception exception, string connectionString) : base("", exception.InnerException)
            {
                _exception = exception;
                _connectionString = connectionString;
                message = exception.Message;
                if (exception.Data.Contains("Error occurred in script: "))
                {
                    message = message + " (" + exception.Data["Error occurred in script: "] + ")";
                }

                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    message = message + " in " + connectionString;
                }
            }

            public override string Message
            {
                get { return message; }
            }

            public override IDictionary Data
            {
                get { return _exception.Data; }
            }

            public override string Source
            {
                get
                {
                    return _exception.Source;
                }
                set { _exception.Source = value; }
            }

            public override string StackTrace
            {
                get { return _exception.StackTrace; }
            }

            public override Exception GetBaseException()
            {
                return _exception.GetBaseException();
            }
        }
    }
}
