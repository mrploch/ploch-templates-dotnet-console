using Microsoft.Extensions.DependencyInjection;
using Ploch.TemplateConsole.UI.ConsoleApp.Commands;
using Ploch.TemplateConsole.UI.ConsoleApp.Infrastructure;
using Spectre.Console.Cli;

namespace Ploch.TemplateConsole.UI.ConsoleApp;

/// <summary>
///     Composes the command-line application: its services and its commands.
/// </summary>
/// <remarks>
///     Program.cs supplies the real console; tests supply a <c>TestConsole</c>, so every command can be exercised
///     end to end without touching the terminal.
/// </remarks>
internal static class ConsoleApplication
{
    /// <summary>
    ///     Creates the command application.
    /// </summary>
    /// <param name="services">
    ///     The service collection, which must already contain the <see cref="Spectre.Console.IAnsiConsole" /> to write to.
    /// </param>
    /// <returns>The configured <see cref="CommandApp" />.</returns>
    public static CommandApp Create(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IGreeter, Greeter>();

        var app = new CommandApp(new TypeRegistrar(services));
        app.Configure(config =>
        {
            config.SetApplicationName("template-console");
            config.AddCommand<GreetCommand>("greet")
                  .WithDescription("Greets someone by name.")
                  .WithExample("greet", "Ada")
                  .WithExample("greet", "Ada", "--shout");
        });

        return app;
    }
}
