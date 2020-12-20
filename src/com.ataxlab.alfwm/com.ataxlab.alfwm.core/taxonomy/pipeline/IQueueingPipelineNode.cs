using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// abstract and normalize the payload of the 
    /// queueing pipeline collection
    /// </summary>
    public interface IQueueingPipelineNode
    {
        string QueueingPipelineNodeId { get; set; }

        IDefaultQueueingPipelineTool PipelineToolEx { get; set; }

       

        QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> PipelineTool { get; set; }
    }

    public interface IQueueingPipelineNode<TPipelineTool> : IQueueingPipelineNode
    {
        new TPipelineTool PipelineTool { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration>  : IQueueingPipelineNode
         where TPipelineToolConfiguration :  class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

    }

    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity> : IQueueingPipelineNode

       //where TPipelineToolConfiguration : class, new()
       //where TOutputEntity : class, new()
       //where TInputEntity : class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

        IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }

        IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
    }
}
