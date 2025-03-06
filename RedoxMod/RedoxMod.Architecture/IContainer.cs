using System;
using System.Diagnostics.CodeAnalysis;

namespace RedoxMod.Architecture
{
    public interface IContainer
    {

        void Bind<TService, TConcrete>()
            where TService : IService
            where TConcrete : class, TService;

        bool Bound<TService>(Type? service = null) where TService : IService;

        TService Instance<TService>([DisallowNull] TService instance) where TService : IService;

        IService Resolve(Type serviceType);

        TService Resolve<TService>() where TService : IService;
    }
}
