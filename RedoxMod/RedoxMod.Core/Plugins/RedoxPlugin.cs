using Acornima.Ast;
using Jint;
using Jint.Native.Function;
using RedoxMod.API.Eventing;
using RedoxMod.API.Plugins;
using RedoxMod.Architecture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedoxMod.Core.Plugins
{
    public class RedoxPlugin : IRedoxPlugin
    {

        private Engine _engine;
        private Prepared<Script> _script;
        private ClassDeclaration _mainPluginClass;

        private readonly IEventBus _eventBus;

        private readonly IList<RegisteredHook> _hooks = new List<RegisteredHook>();

        public RedoxPlugin(IContainer container)
        {
            this.Container = container;

            this._eventBus = container.Resolve<IEventBus>();
        }

        public IContainer Container { get; }

        public IPluginContext Context => throw new NotImplementedException();

        public IPluginLoader Loader => throw new NotImplementedException();

        public PluginStatus Status { get; private set; }

        public Task<bool> DeInitializeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InitializeAsync()
        {
            string fullPluginPath = this.GetAbsolutePath();

            if(!File.Exists(fullPluginPath))
            {
                // TODO: Add error log that the path doesn't exists.
                return false;
            }

            this._engine = new Engine(cfg => cfg.AllowClr())
                .SetValue("redoxPlugin", this);

            string code = await File.ReadAllTextAsync(fullPluginPath);

            this._script = Engine.PrepareScript(code);

            this._mainPluginClass = this._script.Program.Body
                .FirstOrDefault(x => x.Type == NodeType.ClassDeclaration) as ClassDeclaration;

            if(this._mainPluginClass == null)
            {
                //TODO: Add A error log that the plugin couldn't load.
                return false;
            }

            this._engine.Execute(this._script);

            this.LoadMethods();
            await this.RegisterEventHandlersAsync();

            // var pluginInstance = this._engine.Evaluate($"const plugin = new {mainPluginClass}()");

            return true;
        }

        private async Task RegisterEventHandlersAsync()
        {
            foreach(RegisteredHook hook in this._hooks)
            {
                if (hook.Name.StartsWith("on"))  // e.g., "onPlayerJoin"
                {
                    await this._eventBus.SubscribeAsync(hook.Name, (eventData) =>
                    {
                        this._engine.Invoke($"plugin.{hook.Name}", eventData);
                    });
                }
            }
           
        }
        private void LoadMethods()
        {

            IEnumerable<MethodDefinition> methods = this._mainPluginClass.Body.ChildNodes
                  .Where(x => x.Type == NodeType.MethodDefinition)
                  .Cast<MethodDefinition>();

            string[] methodNames = methods.SelectMany(method => method.ChildNodes)
                .OfType<Identifier>()
                .Select(identifier => identifier.Name)
                .ToArray();

            Dictionary<string, Function> functionDictionary = methodNames
                .Select(name => new { Name = name, Function = this._engine.Evaluate($"plugin.{name}") as Function })
                .Where(x => x.Function != null)
                .ToDictionary(x => x.Name, x => x.Function);

            foreach (KeyValuePair<string, Function> item in functionDictionary)
            {
                this._hooks.Add(new RegisteredHook(item.Key, item.Value));
            }
        }

        public object Invoke(string func, params object[] args)
        {
            throw new NotImplementedException();
        }

        private string GetAbsolutePath()
        {
            return Path.Combine(this.Context.WorkingDirectory, this.Context.FileName);
        }
    }
}
