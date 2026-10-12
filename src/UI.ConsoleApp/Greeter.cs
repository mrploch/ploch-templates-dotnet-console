using Ploch.Common.ArgumentChecking;

namespace Ploch.TemplateConsole.UI.ConsoleApp;

/// <summary>
///     Builds greetings. A placeholder for the application's real services; replace it.
/// </summary>
internal interface IGreeter
{
    /// <summary>
    ///     Creates a greeting for <paramref name="name" />.
    /// </summary>
    /// <param name="name">The name of the person to greet.</param>
    /// <param name="shout">Whether to return the greeting in upper case.</param>
    /// <returns>The greeting.</returns>
    string Greet(string name, bool shout);
}

/// <inheritdoc />
internal sealed class Greeter : IGreeter
{
    /// <inheritdoc />
    public string Greet(string name, bool shout)
    {
        // Ploch.Common's argument guards: throws ArgumentException for a null, empty or whitespace name.
        var trimmed = name.NotNullOrEmpty(nameof(name)).Trim();
        if (trimmed.Length == 0)
        {
            throw new ArgumentException("The name must contain a non-whitespace character.", nameof(name));
        }

        var greeting = $"Hello, {trimmed}!";

        return shout ? greeting.ToUpperInvariant() : greeting;
    }
}
