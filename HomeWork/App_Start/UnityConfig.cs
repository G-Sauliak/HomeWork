using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using HomeWork.Repositories;
using HomeWork.Services;

namespace HomeWork
{
    public static class UnityConfig
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
  
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();


            return container;
        }
    }
}