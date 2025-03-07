using RedoxMod.API;
using RedoxMod.API.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.Core.Plugins
{
    public class PluginServiceProvider : ServiceProvider
    {
        public PluginServiceProvider(IRedoxApplication app) : base(app){}

        public override void Register()
        {
            this.App.Container.Bind<IPluginLoader, PluginLoader>();
        }
    }
}
