using Funq.Domain;

namespace Funq.Tests;

[TestFixture]
public class ContainerFeature
{
    public interface IFoo;

    public class Foo(IBar bar) : IFoo
    {
        public IBar Bar { get; private set; } = bar;
    }

    public interface IBar;
    public class Bar : IBar;

    [Test]
    public void Registers_Types_And_Get_Instance()
    {
        var container = new Container();

        container.Register<IBar>(_ => new Bar());
        var bar = container.Resolve<IBar>();
        Assert.That(bar, Is.Not.Null);
        Assert.IsAssignableFrom<Bar>(bar);
    }

    [Test]
    public void Resolved_Gets_Dependencies_Injected()
    {
        var container = new Container();
        container.Register<IBar>(_ => new Bar());
        container.Register<IFoo>(c => new Foo(c.Resolve<IBar>()));

        var foo = container.Resolve<IFoo>() as Foo;
        Assert.That(foo, Is.Not.Null);
        Assert.That(foo.Bar, Is.Not.Null);
    }
}