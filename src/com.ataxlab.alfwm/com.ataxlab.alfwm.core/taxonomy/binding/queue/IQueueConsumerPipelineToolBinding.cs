using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// support a push notification Pipeline Tool binding 
    /// that is a Concurrent Queue of 
    /// Tuples of a type you specify
    /// 
    /// the consumer may want to periodically
    /// dequeue and optionally pass-through
    /// messages that appear on the InputQueue
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public interface IQueueConsumerPipelineToolBinding<TQueueEntity> : IPipelineToolBinding, IDisposable
        where TQueueEntity : class
    {
        ConcurrentQueue<TQueueEntity> InputQueue { get; set; }
         
        /// <summary>
        /// period after which the timer elapsed event fires
        /// </summary>
        int PollingintervalMilliseconds { get; set; }

        /// <summary>
        /// set false to pause the timer
        /// during processing for example
        /// </summary>
        bool IsAutoResetPollingTimer { get; set; }

        /// <summary>
        /// control the polling timer state
        /// nothing precludes use of this binding
        /// without the use of the timer
        /// </summary>
        bool IsQueuePollingEnabled { get; set; }

        /// <summary>
        /// timer that determines polling interval of the queue
        ///
        /// presumably you configure the timer cancels firing during processing of a tuple
        /// </summary>
        System.Timers.Timer ConsumerPollingTimer { get; set; }

        /// <summary>
        /// called when the queue poller determines (via Peek not DeQueue)
        /// that the queue has new data - consumer has a chance to determine
        /// whether it can consume the data or not
        /// 
        /// prior to firing QueueHasData event to subscribers
        /// </summary>
        /// <param name="timestamp"></param>
        void OnQueueHasData(DateTime timestamp, TQueueEntity availableData);

        /// <summary>
        /// clients of the binding subscribe to this event to be notified
        /// when the queue poller has dequeued new data
        /// </summary>
        event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;
    }
}
