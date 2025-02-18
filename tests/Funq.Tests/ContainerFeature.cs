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

    public class Bar : IBar
    {
        public Bar() { }

        public Bar(string arg1)
        {
            Arg1 = arg1;
        }

        public Bar(string arg1, bool arg2) : this(arg1) 
        {
            Arg2 = arg2;
        }

        public string Arg1 { get; set; }
        public bool Arg2 { get; set; }
    }

    [Test]
    public void Registers_Types_And_Get_Instance()
    {
        // Arrange
        var container = new Container();

        container.Register<IBar>(_ => new Bar());

        // Act
        var bar = container.Resolve<IBar>();

        // Assert
        Assert.That(bar, Is.Not.Null);
        Assert.IsAssignableFrom<Bar>(bar);
    }

    [Test]
    public void Resolved_Gets_Dependencies_Injected()
    {
        // Arrange
        var container = new Container();
        container.Register<IBar>(_ => new Bar());
        container.Register<IFoo>(c => new Foo(c.Resolve<IBar>()));

        // Act
        var foo = container.Resolve<IFoo>() as Foo;
        
        // Assert
        Assert.That(foo, Is.Not.Null);
        Assert.That(foo.Bar, Is.Not.Null);
    }

    [Test]
    public void Constructor_Argument_Passed_On_Resolve()
    {
        // Arrange
        const string expectedArg1 = "arg1";
        var container = new Container();
        
        container.Register<IBar, string>((_, arg) => new Bar(arg));

        // Act
        var bar = container.Resolve<IBar, string>(expectedArg1) as Bar;

        // Assert
        Assert.That(bar, Is.Not.Null);
        Assert.That(bar.Arg1, Is.EqualTo(expectedArg1));
    }

    [Test]
    public void Registers_Multiple_Constructor_Overloads()
    {
        // Arrange
        const string expectedArg1 = "arg1";
        var container = new Container();

        container.Register<IBar, string>((_, arg) => new Bar(arg));
        container.Register<IBar>(_ => new Bar());

        // Act
        var bar = container.Resolve<IBar, string>(expectedArg1) as Bar;
        var bar2 = container.Resolve<IBar>() as Bar;

        // Assert
        Assert.That(bar, Is.Not.Null);
        Assert.That(bar.Arg1, Is.EqualTo(expectedArg1));

        Assert.That(bar2, Is.Not.Null);
    }

    [Test]
    public void Constructors_Argument_Passed_On_Resolve()
    {
        // Arrange
        const string expectedArg1 = "arg1";
        var container = new Container();

        container.Register<IBar, string, bool>((_, arg, boolValue) => new Bar(arg, boolValue));

        // Act
        var bar = container.Resolve<IBar, string, bool>(expectedArg1, true) as Bar;

        // Assert
        Assert.That(bar, Is.Not.Null);
        Assert.That(bar.Arg1, Is.EqualTo(expectedArg1));
        Assert.That(bar.Arg2, Is.True);
    }
}