using System;

namespace RedoxMod.Architecture
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ServiceInfoAttribute : Attribute
    {
        public string Alias { get;}
        public ServiceLifetime ServiceLifetime { get; }
        
        public ServiceInfoAttribute(string alias, ServiceLifetime serviceLifetime)
        {
            Alias = alias;
            ServiceLifetime = serviceLifetime;
        }
    }
}