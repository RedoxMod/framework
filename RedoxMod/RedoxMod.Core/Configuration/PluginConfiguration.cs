using RedoxMod.API.Configuration;
using RedoxMod.API.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.Core.Configuration
{
    public sealed class PluginConfiguration : Configuration
    {
        public IRedoxPlugin Plugin { get; }

        public PluginConfiguration(IRedoxPlugin plugin, string fileName, string workingDirectory) : base(fileName, workingDirectory)
        {
            this.Plugin = plugin;
        }

        public PluginConfiguration(IRedoxPlugin plugin, string fileName)
            : base(fileName, plugin.ConfigPath)
        {
            
            Plugin = plugin;
        }
    }
}
