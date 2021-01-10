using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline.queueing
{
    /// <summary>
    /// enforce a process definition schema
    /// that at a minimum can only be of 1 kind at a time
    /// </summary>
    public enum DefaultQueueingPipelineNodeTypeEnum
    {
        IsPipelineNode,
        IsPipelineNodeGatewayNode,
        IsPipelineToolNode,
        IsPipelineToolGatewayNode
    }

    /// <summary>
    /// abstract and normalize the payload of the 
    /// queueing pipeline collection
    /// </summary>
    public interface IDefaultQueueingPipelineNode
    {
        string QueueingPipelineNodeId { get; set; }

        DefaultQueueingPipelineNodeTypeEnum QueueingPipelineNodeType { get; set; }

        IDefaultQueueingPipelineTool QueueingPipelineTool { get; set; }

        IDefaultQueueingPipeline QueueingPipeline { get; set; }

        IDefaultQueueingChannelPipelineToolGateway QueueingPipelineToolGateway { get; set; }

        IDefaultQueueingChannelPipelineGateway QueueingPipelineGateway { get; set; }
    }

    public interface IQueueingPipelineNode<TPipelineTool> : IDefaultQueueingPipelineNode
    {
        new TPipelineTool PipelineTool { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration> : IDefaultQueueingPipelineNode
         where TPipelineToolConfiguration : class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

    }

    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity> : IDefaultQueueingPipelineNode

    //where TPipelineToolConfiguration : class, new()
    //where TOutputEntity : class, new()
    //where TInputEntity : class, new()
    {
        new TPipelineTool PipelineTool { get; set; }

        IQueueConsumerPipelineToolBinding<TInputEntity> PipelineToolInputBinding { get; set; }

        IQueueProducerPipelineToolBinding<TOutputEntity> PipelineToolOutputBinding { get; set; }
    }
}
