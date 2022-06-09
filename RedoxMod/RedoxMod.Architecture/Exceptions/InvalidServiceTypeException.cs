using System;

namespace RedoxMod.Architecture.Exceptions
{
    public class InvalidServiceTypeException : Exception
    {
        public InvalidServiceTypeException() { }
        public InvalidServiceTypeException(string message) : base(message) { }
    }
}