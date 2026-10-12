using AutoFixture.Xunit3;

namespace Ploch.TemplateConsole.UI.ConsoleApp.Tests;

public class GreeterTests
{
    [Theory]
    [AutoData]
    public void Greet_should_greet_the_person_by_name(string name)
    {
        new Greeter().Greet(name, shout: false).Should().Be($"Hello, {name}!");
    }

    [Fact]
    public void Greet_should_trim_the_name()
    {
        new Greeter().Greet("  Ada  ", shout: false).Should().Be("Hello, Ada!");
    }

    [Fact]
    public void Greet_should_return_upper_case_when_shouting()
    {
        new Greeter().Greet("Ada", shout: true).Should().Be("HELLO, ADA!");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Greet_should_throw_when_the_name_is_missing(string? missingName)
    {
        var act = () => new Greeter().Greet(missingName!, shout: false);

        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }
}
