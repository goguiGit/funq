namespace Funq.Domain;

public class ServiceKey
{
    public ServiceKey(Type serviceType, Type factoryType)
    {
        ServiceType = serviceType;
        FactoryType = factoryType;
    }

    public Type ServiceType { get; set; }

    public Type FactoryType { get; set; }

    protected bool Equals(ServiceKey other)
    {
        return ServiceType == other.ServiceType && FactoryType == other.FactoryType;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ServiceKey)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ServiceType, FactoryType);
    }

}