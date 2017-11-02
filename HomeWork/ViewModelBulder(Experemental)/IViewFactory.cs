using System.Threading.Tasks;

public interface IViewFactory
{
    Task<TView> CreateView<TInput, TView>(TInput input);
}