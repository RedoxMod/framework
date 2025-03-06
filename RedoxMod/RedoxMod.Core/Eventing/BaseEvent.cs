using RedoxMod.API.Eventing;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.Core.Eventing
{
    public abstract class BaseEvent: IEvent
    {
        /// <summary>
        /// Gets the timestamp of when this event was created.
        /// </summary>
        public DateTime Timestamp { get; } = DateTime.UtcNow;
    }
}
