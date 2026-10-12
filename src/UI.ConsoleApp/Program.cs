using Microsoft.Extensions.DependencyInjection;
using Ploch.TemplateConsole.UI.ConsoleApp;
using Spectre.Console;

var services = new ServiceCollection().AddSingleton(AnsiConsole.Console);

return await ConsoleApplication.Create(services).RunAsync(args);
