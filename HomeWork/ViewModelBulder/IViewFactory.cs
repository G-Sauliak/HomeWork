using System.Threading.Tasks;

public interface IViewFactory
{
    Task<TView> CreateView<TInput, TView>(TInput id);
    Task<TView> CreateView<TInput, TInput1, TInput2, TView>(TInput indexPage, TInput1 counter, TInput2 countShowUser);
}