using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeWork.ViewModelBulder
{
    public class ViewModelFactory : IViewFactory
    {
        public async Task<TView> CreateView<TInput, TView>(TInput input)
        {
            var builder = DependencyResolver.Current.GetService<IViewBuilder<TInput, TView>>();

            if (builder != null)
            {
                return await builder.Build(input);
            }

            return (TView)Activator.CreateInstance(typeof(TView), input);
        }
    }
}