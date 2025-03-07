using RedoxMod.API.Plugins;
using RedoxMod.Architecture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.Core.Plugins
{
    [ServiceInfo("PluginLoader", ServiceLifetime.Singleton)]
    public class PluginLoader : IPluginLoader
    {

        private readonly IList<IRedoxPlugin> _plugins = new List<IRedoxPlugin>();
        public IRedoxPlugin[] Plugins => throw new NotImplementedException();

        public Task LoadPluginAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task LoadPluginsAsync()
        {
            throw new NotImplementedException();
        }

        public Task LoadServiceAsync()
        {
            return Task.CompletedTask;
        }

        public Task ReloadPluginAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task ReloadPluginsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnloadPluginAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UnloadPluginsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnloadServiceAsync()
        {
            return Task.CompletedTask;
        }
    }
}
