using RedoxMod.API.Configuration;
using RedoxMod.Architecture;
using Semver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedoxMod.API
{
    public interface IRedoxApplication : IService, IConfigurable
    {
        /// <summary>
        /// Gets the version of the application.
        /// </summary>
        SemVersion Version { get; }
        
        /// <summary>
        /// Gets the dependency container of this application.
        /// </summary>
        IContainer Container { get;}

        ServiceProvider[] Providers { get;}
        
        /// <summary>
        /// Gets the custom path of the RedoxMod installation.
        /// </summary>
        string BasePath { get; }
        
        /// <summary>
        /// Gets the path to the plugins directory.
        /// </summary>
        string PluginsPath { get; }
        
        /// <summary>
        /// Gets the path to the language directory.
        /// </summary>
        string LangPath { get; }
        
        /// <summary>
        /// Gets the path to the configurations directory.
        /// </summary>
        string ConfigurationsPath { get; }
        
        /// <summary>
        /// Gets the path to the directory where the RedoxMod assemblies are located.
        /// </summary>
        string RootPath { get; }
        

        /// <summary>
        /// Initializes the application.
        /// </summary>
        Task InitializeAsync();

        void RegisterServiceProvider<TServiceProvider>() where TServiceProvider : ServiceProvider;
    }
}