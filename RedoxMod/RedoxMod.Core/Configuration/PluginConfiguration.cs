using RedoxMod.API.Configuration;
using RedoxMod.API.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.Core.Configuration
{
    public sealed class PluginConfiguration : Configuration
    {
        public PluginConfiguration(IRedoxPlugin plugin, string fileName, string workingDirectory) : base(fileName, workingDirectory)
        {
            this.Plugin = plugin;
        }

        public IRedoxPlugin Plugin { get; }
    }
}
