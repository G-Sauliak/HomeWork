using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using HomeWork.Repositories;
using HomeWork.Services;
using HomeWork.ViewModelBulder;
using HomeWork.Models;

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
            container.RegisterType<IViewFactory, ViewModelFactory>();
            container.RegisterType<IViewBuilder<int, EditViewModel>, EditViewBuilder>();
            container.RegisterType<IViewBuilder<int,int,int,IndexViewModel>, IndexViewBuild>();
            

            return container;
        }
    }
}