using System.Threading.Tasks;

public interface IViewBuilder<TInput, TInput1, TInput2, TView>
{
    Task<TView> Build(TInput indexPage, TInput1 counter, TInput2 maxShowUser);
}

public interface IViewBuilder<TInput, TView>
{
    Task<TView> Build(TInput id);
}