using System;
using System.Reflection;

namespace RedoxMod.Architecture
{
    /// <summary>
    /// Represents a bound service.
    /// </summary>
    public sealed class ServiceBinding
    {
        /// <summary>
        /// Gets the Service Type. (Interface or Abstract Class)
        /// </summary>
        public Type ServiceType { get; private set; }
        
        /// <summary>
        /// Gets the Concrete (Implementation) type of the service.
        /// </summary>
        public Type ConcreteType { get; internal set; }
        
        /// <summary>
        /// Gets the alias of this binding.
        /// <para>A service alias can be used as an alternative to resolve services from the container.</para>
        /// </summary>
        public string Alias { get; }
        
        /// <summary>
        /// Gets the life time of this service.
        /// </summary>
        public ServiceLifetime Lifetime { get; }
        
        /// <summary>
        /// Gets the instance of the Service Implementation.
        /// </summary>
        public IService? Instance { get; internal set; }
        
        /// <summary>
        /// Gets if the service has been resolved or not.
        /// </summary>
        public bool Resolved { get; internal set; }
        
        public ServiceBinding(Type serviceType, Type concreteType, IService? instance = null)
        {
            ServiceType = serviceType;
            ConcreteType = concreteType;
            Instance = instance;

            ServiceInfoAttribute serviceInfo = concreteType.GetCustomAttribute<ServiceInfoAttribute>();
            this.Alias = serviceInfo?.Alias ?? string.Empty;
            this.Lifetime = serviceInfo?.ServiceLifetime ?? ServiceLifetime.Singleton;
        }
    }
}