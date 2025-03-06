using RedoxMod.Architecture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.API.Eventing
{
    public interface IEventBus : IService
    {
        Task SubscribeAsync(string eventName, Action<IEvent> handler);

        Task SubscribeAsync<TEvent>(string eventName, Action<TEvent> handler) where TEvent : IEvent;

        Task UnsubscribeAsync(string eventName, Action<IEvent> handler);
        Task EmitAsync(string eventName, IEvent eventData);
    }
}
