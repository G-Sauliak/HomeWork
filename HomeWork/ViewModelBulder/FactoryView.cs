using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeWork.ViewModelBulder
{
    public class ViewModelFactory : IViewFactory
    {
        public async Task<TView> CreateView<TInput, TView>(TInput id)
        {
            var builder = DependencyResolver.Current.GetService<IViewBuilder<TInput, TView>>();

            if (builder != null)
            {
                return await builder.Build(id);
            }

            return (TView)Activator.CreateInstance(typeof(TView), id);
        }

        public async Task<TView> CreateView<TInput, TInput1, TInput2, TView>(TInput indexPage, TInput1 counter, TInput2 countShowUser)
        {
            var builder = DependencyResolver.Current.GetService<IViewBuilder<TInput, TInput1, TInput2, TView>>();

            if (builder != null)
            {
                return await builder.Build(indexPage, counter, countShowUser);
            }
            
            return (TView)Activator.CreateInstance(typeof(TView), indexPage, counter, countShowUser);
        }
    }
}