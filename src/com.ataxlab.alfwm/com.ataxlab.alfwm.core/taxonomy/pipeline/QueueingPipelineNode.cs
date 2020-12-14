using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class QueueingPipelineNode<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity> : 
        IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
         where TPipelineTool :  IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>
         where TPipelineToolConfiguration : IPipelineToolConfiguration // class, new()
         //where TLatchingInputBinding : class, new()
         //where TOutputBinding : class, new()
         //where TOutputEntity : class, new()
         //where TInputEntity : class, new()
    {
        public TPipelineTool PipelineTool { get; set;}
        public IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set;}
        public IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set;}
        public string QueueingPipelineNodeId { get; set;}
    }
}
