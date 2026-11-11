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
