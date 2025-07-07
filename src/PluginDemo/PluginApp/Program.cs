using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PluginDemo;

var pluginsDirectory = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
        throw new InvalidOperationException("Cannot get the main application directory path"),
    "Plugins");

#pragma warning disable CA1303 // Do not pass literals as localized parameters
Console.WriteLine("Microsoft.Extensions.DependencyInjection version:");
#pragma warning restore CA1303 // Do not pass literals as localized parameters

Console.WriteLine($"App: {typeof(ServiceCollection).Assembly.GetName().Version}");

string[] names = ["PluginFoo", "PluginBar"];

var plugins = names.Select(n =>
    new PluginSandbox(Path.Combine(pluginsDirectory, n)).LoadAssemblyByName(n))
    .SelectMany(a => a.GetTypes())
    .Where(t => t.IsAssignableTo(typeof(IPlugin)))
    .Select(t => Activator.CreateInstance(t))
    .Cast<IPlugin>();

foreach (var p in plugins)
{
    Console.WriteLine($"{p.Name}: {p.DependencyInjectionVersion}");
}
