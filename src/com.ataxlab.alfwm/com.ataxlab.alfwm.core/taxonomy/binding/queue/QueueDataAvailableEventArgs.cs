using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// event args for event raised when data is available on the consumer pipeline tool binding
    /// </summary>
    /// <typeparam name="TAvailableQueuePayload"></typeparam>
    public class QueueDataAvailableEventArgs<TAvailableQueuePayload> : EventArgs
    {
        public QueueDataAvailableEventArgs(TAvailableQueuePayload payload)
        {
            EventPayload = payload;
        }

        public TAvailableQueuePayload EventPayload { get; set; }
        public DateTime TimeStamp { get; internal set; }
        public string SourceChannelId { get; internal set; }
    }
}
