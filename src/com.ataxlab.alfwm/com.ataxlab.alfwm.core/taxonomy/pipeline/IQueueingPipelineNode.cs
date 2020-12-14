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

       
    }

    public interface IQueueingPipelineNode<TPipelineTool> : IQueueingPipelineNode
    {
        TPipelineTool PipelineTool { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration> : IQueueingPipelineNode
        where TPipelineTool : IPipelineTool<TPipelineToolConfiguration>
        where TPipelineToolConfiguration : IPipelineConfiguration // class, new()
    {
        TPipelineTool PipelineTool { get; set; }

    }

    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity> : IQueueingPipelineNode
        where TPipelineToolConfiguration : IPipelineToolConfiguration
       // where TPipelineTool : class, IPipelineTool<TPipelineToolConfiguration>, new()
       // where TPipelineToolConfiguration : class, new()
       //where TOutputEntity : class, new()
       // where TInputEntity : class, new()
    {
        TPipelineTool PipelineTool { get; set; }

        IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }

        IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
    }
}
