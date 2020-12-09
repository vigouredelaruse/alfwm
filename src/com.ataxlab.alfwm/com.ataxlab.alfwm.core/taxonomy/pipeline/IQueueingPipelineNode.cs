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


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    public interface IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration> : IQueueingPipelineNode
        where TPipelineTool : class, IPipelineTool<TPipelineToolConfiguration>, new()
        where TPipelineToolConfiguration : class, new()
    {
        TPipelineTool PipelineTool { get; set; }

    }
}
