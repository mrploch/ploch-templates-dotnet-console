using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Ploch.TemplateConsole.UI.ConsoleApp.Commands;

/// <summary>
///     The <c>greet</c> command: prints a greeting for the given name.
/// </summary>
internal sealed class GreetCommand(IAnsiConsole console, IGreeter greeter) : Command<GreetCommand.Settings>
{
    /// <inheritdoc />
    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        console.MarkupLineInterpolated($"[green]{greeter.Greet(settings.Name, settings.Shout)}[/]");

        return 0;
    }

    /// <summary>
    ///     The arguments and options of the <c>greet</c> command.
    /// </summary>
    internal sealed class Settings : CommandSettings
    {
        /// <summary>
        ///     Gets the name of the person to greet.
        /// </summary>
        [CommandArgument(0, "<name>")]
        [Description("The name of the person to greet.")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        ///     Gets a value indicating whether the greeting is printed in upper case.
        /// </summary>
        [CommandOption("-s|--shout")]
        [Description("Print the greeting in upper case.")]
        public bool Shout { get; init; }

        /// <inheritdoc />
        public override ValidationResult Validate() =>
            string.IsNullOrWhiteSpace(Name) ? ValidationResult.Error("The name must not be empty.") : ValidationResult.Success();
    }
}
