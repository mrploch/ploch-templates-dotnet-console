using Ploch.Common.ArgumentChecking;

namespace Ploch.TemplateConsole.UI.ConsoleApp;

/// <inheritdoc />
internal sealed class Greeter : IGreeter
{
    /// <inheritdoc />
    public string Greet(string name, bool shout)
    {
        // Ploch.Common's argument guards: throws ArgumentException for a null, empty or whitespace name.
        var trimmed = name.NotNullOrEmpty().Trim();
        if (trimmed.Length == 0)
        {
            throw new ArgumentException("The name must contain a non-whitespace character.", nameof(name));
        }

        var greeting = $"Hello, {trimmed}!";

        return shout ? greeting.ToUpperInvariant() : greeting;
    }
}
