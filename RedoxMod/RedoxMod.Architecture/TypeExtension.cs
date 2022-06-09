using System;
using System.Reflection;

namespace RedoxMod.Architecture
{
    public static class TypeExtension
    {
        public static object CreateInstanceWithDependencies(this Type type, Container container)
        {
            ConstructorInfo constructor = type.GetConstructors(BindingFlags.Public)[0];
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