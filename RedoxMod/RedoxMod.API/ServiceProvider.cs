using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RedoxMod.API
{
    public abstract class ServiceProvider
    {
        protected readonly IApplication App;


        /// <summary>
        /// Gets the service bindings of this provider.
        /// </summary>
        public virtual IDictionary<Type, Type> Bindings { get; } = new Dictionary<Type, Type>();

        /// <summary>
        /// Gets if the provider is deferrable or not.
        /// </summary>
        public virtual bool IsDeferrable { get; } = false;
        
        public ServiceProvider(IApplication app)
        {
            this.App = app;
        }

        /// <summary>
        /// Register all the provider services.
        /// </summary>
        public virtual void Register() {}
    }
}