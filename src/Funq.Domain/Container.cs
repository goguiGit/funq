
namespace Funq.Domain;

public class Container
{
    private readonly Dictionary<ServiceKey, object> _factories = new();

    public void Register<TService>(Func<Container, TService> factory) 
        => RegisterImpl<TService, Func<Container, TService>>(factory);

    public void Register<TService, TArg>(Func<Container, TArg, TService> factory) 
        => RegisterImpl<TService, Func<Container, TArg, TService>>(factory);

    public void Register<TService, TArg, TArg2>(Func<Container, TArg, TArg2, TService> factory) 
        => RegisterImpl<TService, Func<Container, TArg, TArg2, TService>>(factory);

    public TService Resolve<TService>() 
        => ResolveImpl<TService, Func<Container, TService>>(f => f(this));

    public TService Resolve<TService, TArg>(TArg arg) 
        => ResolveImpl<TService, Func<Container, TArg, TService>>(f => f(this, arg));

    public TService Resolve<TService, TArg, TArg2>(TArg arg, TArg2 arg2) 
        => ResolveImpl<TService, Func<Container, TArg, TArg2, TService>>(f => f(this, arg, arg2));

    private void RegisterImpl<TService, TFunc>(TFunc factory)
    {
        var key = new ServiceKey(typeof(TService), typeof(TFunc));
        _factories.Add(key, factory!);
    }

    private TService ResolveImpl<TService, TFunc>(Func<TFunc, TService> invoker)
    {
        var key = new ServiceKey(typeof(TService), typeof(TFunc));
        var tFunc = (TFunc)_factories[key];
        var instance = invoker(tFunc);
        return instance;
    }
}