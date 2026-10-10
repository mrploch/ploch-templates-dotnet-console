using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Testing;

namespace Ploch.TemplateConsole.UI.ConsoleApp.Tests;

public class GreetCommandTests
{
    [Fact]
    public async Task Greet_should_print_the_greeting_and_succeed()
    {
        var (exitCode, output) = await RunAsync("greet", "Ada");

        exitCode.Should().Be(0);
        output.Should().Contain("Hello, Ada!");
    }

    [Fact]
    public async Task Greet_with_shout_should_print_the_greeting_in_upper_case()
    {
        var (exitCode, output) = await RunAsync("greet", "Ada", "--shout");

        exitCode.Should().Be(0);
        output.Should().Contain("HELLO, ADA!");
    }

    [Fact]
    public async Task Greet_should_fail_when_the_name_is_blank()
    {
        var (exitCode, output) = await RunAsync("greet", " ");

        exitCode.Should().NotBe(0);
        output.Should().Contain("The name must not be empty.");
    }

    [Fact]
    public async Task Greet_should_fail_when_the_name_is_missing()
    {
        var (exitCode, _) = await RunAsync("greet");

        exitCode.Should().NotBe(0);
    }

    private static async Task<(int ExitCode, string Output)> RunAsync(params string[] args)
    {
        using var console = new TestConsole();
        var services = new ServiceCollection().AddSingleton<IAnsiConsole>(console);
        var app = ConsoleApplication.Create(services);
        app.Configure(config => config.Settings.Console = console);

        var exitCode = await app.RunAsync(args, TestContext.Current.CancellationToken);

        return (exitCode, console.Output);
    }
}
