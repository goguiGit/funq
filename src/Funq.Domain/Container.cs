
namespace Funq.Domain;

public class Container
{
    private readonly Dictionary<ServiceKey, object> _factories = new();

    public void Register<TService>(Func<Container, TService> factory)
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TService>));
        _factories.Add(key, factory);
    }

    public void Register<TService, TArg>(Func<Container, TArg, TService> factory)
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TArg, TService>));
        _factories.Add(key, factory);
    }

    public void Register<TService, TArg, TArg2>(Func<Container, TArg, TArg2, TService> factory)
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TArg, TArg2, TService>));
        _factories.Add(key, factory);
    }

    public TService Resolve<TService>()
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TService>));
        var tService = _factories[key] as Func<Container, TService>;
        return tService!.Invoke(this);
    }

    public TService Resolve<TService, TArg>(TArg arg)
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TArg, TService>));
        var tService = _factories[key] as Func<Container, TArg, TService>;
        return tService!.Invoke(this, arg);
    }

    public TService Resolve<TService, TArg, TArg2>(TArg arg, TArg2 arg2)
    {
        var key = new ServiceKey(typeof(TService), typeof(Func<Container, TArg, TArg2, TService>));
        var tService = _factories[key] as Func<Container, TArg, TArg2, TService>;
        return tService!.Invoke(this, arg, arg2);
    }
}