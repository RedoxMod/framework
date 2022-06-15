using System;
using System.IO;
using System.Reflection;
using RedoxMod.API;
using RedoxMod.Architecture;
using Semver;

namespace RedoxMod.Core
{
    public sealed class Application : IApplication
    {
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
        public Container Container { get; }

        /// <inheritdoc />
        public string BasePath { get; }

        /// <inheritdoc />
        public string PluginsPath { get; private set; }

        /// <inheritdoc />
        public string LangPath { get; private set; }

        /// <inheritdoc />
        public string RootPath { get; private set;}
        
        public Application(string basePath = "")
        {
            BasePath = string.IsNullOrEmpty(basePath) ? 
                    Path.Combine(Directory.GetCurrentDirectory(), "redox") 
                    : basePath;
            
        }

        /// <inheritdoc />
        public void Initialize()
        {
            this.CheckDirectories();
            this.RegisterBindings();
        }

        private void RegisterBindings()
        {
           // this.Container.Instance<IApplication>(this);
        }

        private void CheckDirectories()
        {
            this.PluginsPath = Path.Combine(this.BasePath, "Plugins");
            this.LangPath = Path.Combine(this.BasePath, "Lang");
        }
    }
}