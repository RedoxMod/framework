using RedoxMod.Architecture;
using System.Threading.Tasks;

namespace RedoxMod.API.Plugins
{
    public interface IPluginLoader : IService
    {
        IRedoxPlugin[] Plugins { get; }

        Task LoadPluginsAsync();

        Task LoadPluginAsync(string name);

        Task ReloadPluginAsync(string name);

        Task ReloadPluginsAsync();

        Task UnloadPluginAsync(string name);

        Task UnloadPluginsAsync();
    }
}