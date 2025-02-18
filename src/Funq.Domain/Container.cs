
namespace Funq.Domain;

public class Container
{
    private readonly Dictionary<Type, object> _factories = new();

    public void Register<TService>(Func<Container, TService> factory)
    {
        _factories.Add(typeof(TService), factory);
    }

    public TService Resolve<TService>()
    {
        var tService = _factories[typeof(TService)] as Func<Container, TService>;
        return tService!.Invoke(this);
    }
}