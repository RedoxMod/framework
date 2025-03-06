using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.Core.Eventing
{
    public abstract class CancelableEvent : BaseEvent
    {
        /// <summary>
        /// Gets or sets a value indicating whether this event should be canceled.
        /// </summary>
        public bool Cancel { get; set; } = false;
    }
}
