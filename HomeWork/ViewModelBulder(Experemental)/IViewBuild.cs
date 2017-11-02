using System.Threading.Tasks;

public interface IViewBuilder<TInput, TView>
{
    Task<TView> Build(TInput id);
}