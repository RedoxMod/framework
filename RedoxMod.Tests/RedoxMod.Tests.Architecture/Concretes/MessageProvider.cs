﻿using RedoxMod.Architecture;
using RedoxMod.Tests.Architecture.Contracts;
using System.Threading.Tasks;

namespace RedoxMod.Tests.Architecture.Concretes
{
    [ServiceInfo("", ServiceLifetime.Singleton)]
    public class MessageProvider : IMessageProvider
    {
        private readonly ILogger _logger;

        public MessageProvider(ILogger logger)
        {
            _logger = logger;
        }

        public void Greet()
        {
            this._logger.Log("Hello there fellow humans :)");
        }

        public void Goodbye()
        {
            this._logger.Log("Goodbye everyone, hope to see you later :)");
        }

        public Task LoadServiceAsync()
        {
            return Task.CompletedTask;
        }

        public Task UnloadServiceAsync()
        {
            return Task.CompletedTask;
        }
    }
}