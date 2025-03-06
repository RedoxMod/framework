using System;
using System.Threading.Tasks;
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

        public Task LoadServiceAsync()
        {
            return Task.CompletedTask;
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public Task UnloadServiceAsync()
        {

            return Task.CompletedTask;
        }
    }
}