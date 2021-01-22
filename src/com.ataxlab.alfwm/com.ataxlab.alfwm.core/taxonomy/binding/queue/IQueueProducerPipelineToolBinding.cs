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
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public interface IQueueProducerPipelineToolBinding<TQueueEntity> : IPipelineToolBinding
              // where TQueueEntity : class,  new()
    {
        String HostComponentId { get; set; }
        ConcurrentQueue<TQueueEntity> OutputQueue { get; set; }

        event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        void OnQueueHasData(DateTime timestamp, TQueueEntity availableData);
    }
}
