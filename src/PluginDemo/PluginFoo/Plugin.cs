using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace PluginDemo;

/// <summary>
/// The plugin.
/// </summary>
public sealed class Plugin : IPlugin
{
    /// <inheritdoc/>
    public string Name => "Foo";

    /// <inheritdoc/>
    public Version? NewtonsoftVersion => typeof(JsonConverter).Assembly.GetName().Version;

    /// <inheritdoc/>
    public Version? DependencyInjectionVersion => typeof(ServiceCollection).Assembly.GetName().Version;
}
