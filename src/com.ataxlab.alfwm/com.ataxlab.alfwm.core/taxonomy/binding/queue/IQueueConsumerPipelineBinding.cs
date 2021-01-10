using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// specify the channel that interconnects pipelines, 
    /// isomorphic with their analog that connects pipeline tool nodes
    /// that are specified by a process definition
    /// 
    /// as per Messaging Gateway Enterprise Integration Pattern
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public interface IQueueConsumerPipelineBinding<TQueueEntity> : IPipelineBinding, IDisposable
    {
        ConcurrentQueue<TQueueEntity> InputQueue { get; set; }

        /// <summary>
        /// period after which the timer elapsed event fires
        /// </summary>
        double PollingintervalMilliseconds { get; set; }

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
        Timer ConsumerPollingTimer { get; set; }

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
