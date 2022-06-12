using System;
using RedoxMod.Architecture;
using RedoxMod.Tests.Architecture.Contracts;

namespace RedoxMod.Tests.Architecture.Concretes
{
    [ServiceInfo("", ServiceLifetime.Singleton)]
    public class Logger : ILogger
    {
        public Logger(string _)
        {
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}