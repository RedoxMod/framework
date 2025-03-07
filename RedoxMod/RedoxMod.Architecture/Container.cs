using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using RedoxMod.Architecture.Exceptions;

namespace RedoxMod.Architecture
{
    /// <summary>
    /// The Container is responsible for handling the dependency injection of the RedoxMod framework.
    /// </summary>
    public class Container : IContainer
    {
        private readonly List<ServiceBinding> _bindings = new List<ServiceBinding>();

        /// <summary>
        /// Register a binding into the container.
        /// </summary>
        /// <typeparam name="TService">The Service Contract.</typeparam>
        /// <typeparam name="TConcrete">The Concrete Service.</typeparam>
        /// <exception cref="InvalidServiceTypeException">Throws when the service is not a abstract class nor interface</exception>
        /// <exception cref="ServiceBindingException">Throws when the service contract is already bound.</exception>
        public void Bind<TService, TConcrete>()
            where TService : IService
            where TConcrete : class, TService
        {
            Type serviceType = typeof(TService);
            Type concreteType = typeof(TConcrete);

            if (!serviceType.IsAbstract || !serviceType.IsInterface)
                throw new InvalidServiceTypeException($"Type {serviceType.Name} must be either abstract or an interface!");

            if (this.Bound<TService>())
                throw new ServiceBindingException($"Service Type {serviceType.Name} is already bound");

            this._bindings.Add(new ServiceBinding(serviceType, concreteType));
        }

        /// <summary>
        /// Checks if the service is already bound.
        /// </summary>
        /// <typeparam name="TService">The Service Contract.</typeparam>
        /// <param name="service">The service type (Optional)</param>
        /// <returns>If the service is bound or not.</returns>
        public bool Bound<TService>(Type? service = null)
             where TService : IService
        {
            Type serviceType = service ?? typeof(TService);
            return this._bindings.Any(b => b.ServiceType == serviceType);
        }

        /// <summary>
        /// Register a instance into the container.
        /// </summary>
        /// <param name="instance">The service instance.</param>
        /// <typeparam name="TService">The Service Contract</typeparam>
        /// <returns>The instance of the service.</returns>
        /// <exception cref="InvalidServiceTypeException">Throws when the service is not a abstract class nor interface</exception>
        public TService Instance<TService>([DisallowNull] TService instance)
             where TService : IService
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));

            Type serviceType = typeof(TService);

            if (!serviceType.IsAbstract || !serviceType.IsInterface)
                throw new InvalidServiceTypeException($"Type {serviceType.Name} must be either abstract or an interface!");

            if (this.Bound<TService>())
            {
                instance = (TService)this._bindings.Single(b => b.ServiceType == serviceType).Instance!;
                return instance;
            }

            this._bindings.Add(new ServiceBinding(serviceType, instance!.GetType(), instance));
            return instance;
        }

        public TService Resolve<TService>()
             where TService : IService
        {
            Type serviceType = typeof(TService);
            return (TService)this.Resolve(serviceType);
        }

        /// <summary>
        /// Resolves a binding with service type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        /// <exception cref="ServiceBindingException"></exception>
        public IService Resolve(Type serviceType)
        {
            if (!this.Bound<IService>(serviceType))
                throw new ServiceBindingException($"Service Type {serviceType.Name} is not bound");

            ServiceBinding binding = this._bindings.Single(b => b.ServiceType == serviceType);

            IService concreteInstance = this.ResolveBinding(binding);
            return concreteInstance;
        }

        private IService ResolveBinding(ServiceBinding binding)
        {
            if (binding.Lifetime is ServiceLifetime.Transient)
            {
                this.InstantiateBinding(binding);
                return binding.Instance!;
            }

            if (binding.Instance != null)
                return binding.Instance;

            this.InstantiateBinding(binding);
            return binding.Instance!;
        }

        private void InstantiateBinding(ServiceBinding binding)
        {
            IService instance = binding.ConcreteType.CreateInstanceWithDependencies(this);
            binding.Instance = instance;

            instance.LoadServiceAsync();
        }
    }
}