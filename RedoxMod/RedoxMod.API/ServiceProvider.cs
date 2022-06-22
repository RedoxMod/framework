using System;

namespace RedoxMod.API
{
    public abstract class ServiceProvider
    {
        private readonly IApplication _app;

        /// <summary>
        /// Gets the service contracts of this provider.
        /// </summary>
        public virtual Type[] Contracts { get; }

        /// <summary>
        /// Gets if the provider is deferrable or not.
        /// </summary>
        public virtual bool IsDeferrable { get; } = false;
        
        protected ServiceProvider(IApplication app)
        {
            _app = app;
        }

        /// <summary>
        /// Register all the provider services.
        /// </summary>
        public virtual void Register()
        {
        }
    }
}