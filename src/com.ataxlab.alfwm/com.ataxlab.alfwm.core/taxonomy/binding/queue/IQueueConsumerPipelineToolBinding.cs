using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// support a Pipeline Tool binding 
    /// that is a Concurrent Queue of 
    /// Tuples of a type you specify
    /// 
    /// the consumer may want to periodically
    /// dequeue and optionally pass-through
    /// messages that appear on the InputQueue
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public interface IQueueConsumerPipelineToolBinding<TQueueEntity, TPollingTimer> : IPipelineToolBinding
        where TQueueEntity : class
        where TPollingTimer : class
    {
        ConcurrentQueue<TQueueEntity> InputQueue { get; set; }
         
        /// <summary>
        /// presumably your timer cancels firing during processing of a tuple
        /// </summary>
        TPollingTimer ConsumerPollingTimer { get; set; }
    }
}
