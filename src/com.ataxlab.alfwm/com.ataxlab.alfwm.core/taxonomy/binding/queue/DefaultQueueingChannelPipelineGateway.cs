using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// specify a gateway that interfaces pipelines
    /// </summary>
    public interface  IDefaultQueueingChannelPipelineGateway : 
        IDefaultQueueingChannelPipelineGateway<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
                                                QueueingPipelineQueueEntity<IPipelineToolConfiguration>>
    {
        DefaultQueueingChannelPipelineGatewayContext GatewayContext { get; set; }
    }

    /// <summary>
    /// the gateway needs information about its environment
    /// </summary>
    public class DefaultQueueingChannelPipelineGatewayContext
    {
        public DefaultQueueingChannelPipelineGatewayContext()
        {

        }

        public long MessageCount { get; set; }
        public ObservableCollection<string> SeenPipelineIds { get; set; }
    }
    public interface IDefaultQueueingChannelPipelineGateway<TInputEntity, TOutputEntity>
    {
        string Id { get; set; }
        ObservableCollection<PipelineQueueingConsumerChannel<TOutputEntity>> OutputPorts { get; set; }

        ObservableCollection<PipelineQueueingProducerChannel<TOutputEntity>> InputPorts { get; set; }

        void HandleInputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);

        void HandleOutputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);
    }
}
