using RedoxMod.API.Configuration;

namespace RedoxMod.API.Plugins
{
    public interface IPluginContext : IConfigurable      
    {
        string FileName { get; }
        string WorkingDirectory { get; }
    }
}                  