using System;
using System.Linq;
using System.Reflection;

namespace RedoxMod.Architecture
{
    public static class TypeExtension
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;
        
        /// <summary>
        /// Instantiate this type using the dependency container.
        /// </summary>
        /// <param name="type">The extension type.</param>
        /// <param name="container">The dependency container.</param>
        /// <returns>The created instance.</returns>
        public static object CreateInstanceWithDependencies(this Type type, Container container)
        {
            ConstructorInfo constructor = type.GetConstructors(Flags).FirstOrDefault() 
                                          ?? throw new InvalidOperationException($"Type {type.Name} doesn't have a public constructor!");
            
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] @params = new object[parameters.Length];
            
            for (var i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                Type parameterType = parameter.ParameterType;

                if (!container.Bound<object>(parameterType))
                {
                    @params[i] = null!;
                    continue;
                }

                @params[i] = container.Resolve<object>(parameterType);
            }
            
            object instance = Activator.CreateInstance(type, @params);
            return instance;
        }
    }
}