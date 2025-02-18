using Funq.Domain;

namespace Funq.Tests;

[TestFixture]
public class ServiceKeyFeature
{
    [Test]
    public void Keys_Equal_By_ServiceType_And_FactoryType()
    {
        // Arrange
        var key1 = new ServiceKey(typeof(IFormatProvider), typeof(IFormatProvider));
        var key2 = new ServiceKey(typeof(IFormatProvider), typeof(IFormatProvider));

        // Act && Assert
        Assert.That(key1, Is.EqualTo(key2));
        Assert.That(key1.GetHashCode(), Is.EqualTo(key2.GetHashCode()));

    }

    [Test]
    public void Keys_Not_Equal_If_Different_ServiceType ()
    {
        // Arrange
        var key1 = new ServiceKey(typeof(object), typeof(IFormatProvider));
        var key2 = new ServiceKey(typeof(IFormatProvider), typeof(IFormatProvider));

        // Act && Assert
        Assert.That(key1, Is.Not.EqualTo(key2));
        Assert.That(key1.GetHashCode(), Is.Not.EqualTo(key2.GetHashCode()));
    }

    [Test]
    public void Keys_Not_Equal_If_Different_FactoryType()
    {
        // Arrange
        var key1 = new ServiceKey(typeof(IFormatProvider), typeof(object));
        var key2 = new ServiceKey(typeof(IFormatProvider), typeof(IFormatProvider));

        // Act && Assert
        Assert.That(key1, Is.Not.EqualTo(key2));
        Assert.That(key1.GetHashCode(), Is.Not.EqualTo(key2.GetHashCode()));
    }
}