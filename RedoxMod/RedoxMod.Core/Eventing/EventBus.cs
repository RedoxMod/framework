using RedoxMod.API.Eventing;
using RedoxMod.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.Core.Eventing
{
    [ServiceInfo("EventBus", ServiceLifetime.Singleton)]
    public class EventBus : IEventBus
    {
        private readonly Dictionary<string, List<Action<IEvent>>> _eventHandlers = new Dictionary<string, List<Action<IEvent>>>();

        public Task SubscribeAsync(string eventName, Action<IEvent> handler)
        {
            if (!this._eventHandlers.ContainsKey(eventName))
            {
                this._eventHandlers[eventName] = new List<Action<IEvent>>();
            }

            this._eventHandlers[eventName].Add(handler);

            return Task.CompletedTask;
        }

        public Task SubscribeAsync<TEvent>(string eventName, Action<TEvent> handler) where TEvent : IEvent
        {
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<Action<IEvent>>();
            }

            Action<IEvent> wrappedHandler = (e) => handler((TEvent)e);
            _eventHandlers[eventName].Add(wrappedHandler);

            return Task.CompletedTask;
        }
        public Task UnsubscribeAsync(string eventName, Action<IEvent> handler)
        {
            if (!_eventHandlers.ContainsKey(eventName)) return Task.CompletedTask; ;

            _eventHandlers[eventName].Remove(handler);

            return Task.CompletedTask;
        }

        public Task EmitAsync(string eventName, IEvent eventData)
        {
            if (!_eventHandlers.ContainsKey(eventName)) return Task.CompletedTask;

            foreach (var handler in _eventHandlers[eventName])
            {
                handler.DynamicInvoke(eventData);
            }
            return Task.CompletedTask;
        }

        public Task LoadServiceAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnloadServiceAsync()
        {
            throw new NotImplementedException();
        }
    }
}
