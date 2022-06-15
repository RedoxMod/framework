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