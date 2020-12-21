using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class QueueingPipelineNode : IQueueingPipelineNode
    {

        public QueueingPipelineNode()
        {
            
        }
        public string QueueingPipelineNodeId { get; set; }
        public QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> PipelineTool { get; set; }
        public IDefaultQueueingPipelineTool PipelineToolEx { get; set; }

        public IQueueingPipelineTool QueueingPipelineTool { get; set; }
    }

    public class QueueingPipelineNode<TPipelineTool> : IQueueingPipelineNode<TPipelineTool>
    //where TPipelineTool : 
    //                        IQueueingPipelineTool<
    //                            QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
    //                            QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
    //                            IPipelineToolConfiguration,
    //                            IPipelineToolConfiguration,
    //                            IPipelineToolConfiguration>
    {
        public QueueingPipelineNode()
        {

        }

        public TPipelineTool PipelineTool { get; set; }
        public string QueueingPipelineNodeId { get; set; }
        public IDefaultQueueingPipelineTool PipelineToolEx { get; set; }
        QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> IQueueingPipelineNode.PipelineTool { get; set; }
    }

    public class QueueingPipelineNode2<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity> :
      IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
    //where TPipelineTool : class, new()
    //where TPipelineToolConfiguration : class, new()
    // where TInputEntity : class, new()
    // where TOutputEntity : class, new()
    {
        public TPipelineTool PipelineTool { get; set; }
        public IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }
        public IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
        public string QueueingPipelineNodeId { get; set; }
        public IDefaultQueueingPipelineTool PipelineToolEx { get; set; }
        QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> IQueueingPipelineNode.PipelineTool { get; set; }
    }

    public class QueueingPipelineNode<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity> :
        IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
    //where TPipelineTool :  class, new()
    //where TPipelineToolConfiguration :  class, new()
    //where TLatchingInputBinding : class, new()
    //where TOutputBinding : class, new()
    //where TOutputEntity : class, new()
    //where TInputEntity : class, new()
    {
        public TPipelineTool PipelineTool { get; set; }
        public IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }
        public IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
        public string QueueingPipelineNodeId { get; set; }
        public IDefaultQueueingPipelineTool PipelineToolEx { get; set; }
        QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> IQueueingPipelineNode.PipelineTool { get; set; }
    }
}
