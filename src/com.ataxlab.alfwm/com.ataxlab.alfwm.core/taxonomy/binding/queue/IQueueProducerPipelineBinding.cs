using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    public interface IQueueProducerPipelineBinding<TQueueEntity> : IPipelineBinding
    {
        ConcurrentQueue<TQueueEntity> OutputQueue { get; set; }

        event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        void OnQueueHasData(DateTime timestamp, TQueueEntity availableData);
    }
}
