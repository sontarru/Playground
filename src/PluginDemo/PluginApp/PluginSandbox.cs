using System.Reflection;
using System.Runtime.Loader;

namespace PluginDemo;

internal sealed class PluginSandbox
{
    private readonly string _rootPath;
    private readonly AssemblyLoadContext _assemblyLoadContext;

    public PluginSandbox(string rootPath)
    {
        ArgumentNullException.ThrowIfNull(rootPath);
        ThrowIfNotFullyQualifiedPath(rootPath, nameof(rootPath));
        ThrowIfNotExistingDirectory(rootPath, nameof(rootPath));

        _rootPath = rootPath;
        _assemblyLoadContext = new(default);

        _assemblyLoadContext.Resolving += (_, asm) =>
            asm.Name != null ? LoadAssemblyByName(asm.Name) : null;
    }

    private static void ThrowIfNotFullyQualifiedPath(string path, string argName)
    {
        if (!Path.IsPathFullyQualified(path))
        {
            throw new ArgumentException("The path is not fully qualifyed.", argName);
        }
    }

    private static void ThrowIfNotExistingDirectory(string path, string argName)
    {
        if (!Directory.Exists(path))
        {
            throw new ArgumentException(
                "The path does not exists or is not a directory path.",
                argName);
        }
    }

    public Assembly LoadAssemblyByName(string name) =>
        _assemblyLoadContext.LoadFromAssemblyPath(
            Path.Combine(_rootPath, $"{name}.dll"));
}
