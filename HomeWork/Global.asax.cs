
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using HomeWork.Common;

namespace HomeWork
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.Initialise();
        //    Database.SetInitializer<UserContext>(new DropCreateDatabaseIfModelChanges<UserContext>());
        }
    }
}
