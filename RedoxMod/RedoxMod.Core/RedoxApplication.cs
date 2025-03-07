using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RedoxMod.API;
using RedoxMod.API.Configuration;
using RedoxMod.Architecture;
using Semver;

namespace RedoxMod.Core
{
    [ServiceInfo("Application", ServiceLifetime.Singleton)]
    public sealed class RedoxApplication : IRedoxApplication
    {
        private readonly IList<ServiceProvider> _serviceProviders = new List<ServiceProvider>();

        public ServiceProvider[] Providers
        {
            get
            {
                return this._serviceProviders.ToArray();
            }
        }

        public IConfiguration Configuration => throw new NotImplementedException();


        /// <inheritdoc />
        public SemVersion Version
        {
            get
            {
                Version version = this.GetType().Assembly.GetName().Version;
                return SemVersion.FromVersion(version);
            }
        }

        /// <inheritdoc />
        public IContainer Container { get; private set; }

        /// <inheritdoc />
        public string BasePath { get; }

        /// <inheritdoc />
        public string PluginsPath { get; private set; }

        /// <inheritdoc />
        public string LangPath { get; private set; }

        /// <inheritdoc />
        public string ConfigurationsPath { get; private set; }

        /// <inheritdoc />
        public string RootPath { get; private set;}   
        public RedoxApplication(string basePath = "")
        {
            BasePath = string.IsNullOrEmpty(basePath) ? 
                    Path.Combine(Directory.GetCurrentDirectory(), "redox") 
                    : basePath;
            
        }

        /// <inheritdoc />
        public Task InitializeAsync()
        {
            this.CheckDirectories();
            this.RegisterBindings();

            return Task.CompletedTask;
        }

        private void RegisterBindings()
        {
            this.Container = new Container();
            
            this.Container.Instance<IRedoxApplication>(this);

        }

        private void CheckDirectories()
        {
            this.PluginsPath = Path.Combine(this.BasePath, "Plugins");
            this.LangPath = Path.Combine(this.BasePath, "Lang");
            this.ConfigurationsPath = Path.Combine(this.BasePath, "Configs");

            if (!Directory.Exists(this.BasePath))
                Directory.CreateDirectory(this.BasePath);
            
            if(!Directory.Exists(this.PluginsPath))
                Directory.CreateDirectory(this.PluginsPath);
            
            if(!Directory.Exists(this.LangPath))
                Directory.CreateDirectory(this.LangPath);
            
            if(!Directory.Exists(this.ConfigurationsPath))
                Directory.CreateDirectory(this.ConfigurationsPath);
        }

        public Task LoadServiceAsync()
        {
            return Task.CompletedTask;
        }

        public Task UnloadServiceAsync()
        {
            return Task.CompletedTask;
        }

        public void RegisterServiceProvider<TServiceProvider>() where TServiceProvider : ServiceProvider
        {
            try
            {
                ServiceProvider provider = this.Providers.FirstOrDefault(p => p.GetType() == typeof(TServiceProvider));

                if (provider != null)
                {
                    return;
                }

                provider = (ServiceProvider)Activator.CreateInstance(typeof(TServiceProvider), this);
                this._serviceProviders.Add(provider);

                provider.Register();
            }
            catch(Exception ex)
            {

            }
        }
    }
}