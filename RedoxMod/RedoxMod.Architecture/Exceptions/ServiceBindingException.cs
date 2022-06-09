using System;

namespace RedoxMod.Architecture.Exceptions
{
    public class ServiceBindingException : Exception
    {
        public ServiceBindingException() {}
        public ServiceBindingException(string message) : base(message) {}
    }
}