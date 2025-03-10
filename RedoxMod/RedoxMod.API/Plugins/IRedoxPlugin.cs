using RedoxMod.Architecture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.API.Plugins
{
    public interface IRedoxPlugin
    {
        string ConfigPath { get; }

        IRedoxApplication Application { get; }

        IContainer Container { get; }

        IPluginContext Context { get; }

        IPluginLoader Loader { get; }

        PluginStatus Status { get; }

        Task<bool> InitializeAsync();

        Task<bool> DeInitializeAsync();

        object Invoke(string func, params object[] args);
    }
}
