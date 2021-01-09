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
    public interface IQueueingPipelineToolNode
    {
        string QueueingPipelineNodeId { get; set; }

        IDefaultQueueingPipelineTool QueueingPipelineTool { get; set; }

     }

    public interface IQueueingPipelineNode<TPipelineTool> : IQueueingPipelineToolNode
    {
        new TPipelineTool PipelineTool { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration>  : IQueueingPipelineToolNode
         where TPipelineToolConfiguration :  class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

    }

    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity> : IQueueingPipelineToolNode

       //where TPipelineToolConfiguration : class, new()
       //where TOutputEntity : class, new()
       //where TInputEntity : class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

        IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }

        IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
    }
}
