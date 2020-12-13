using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IQueueingPipelineProcessDefinition
    {
        string Id { get; set; }

        /// <summary>
        /// the Pipeline's toolchain
        /// indexed by InstanceId
        /// 
        /// the payload is expected (by the Bind mechanism)
        /// to expose a ConcurrentQueue<Entity>         /// </summary>
        ConcurrentDictionary<string, IQueueingPipelineNode> PipelineToolChain { get; set; }
    }

    public interface IQueueingPipelineProcessDefinition<TProcessDefinition>
        where TProcessDefinition : class, new()
    { 
        string Id { get; set; }

        TProcessDefinition PipelineToolChain { get; set; }
    }

    public interface IQueueingPipelineProcessDefinition<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
         where TPipelineTool : class, IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>, new()
         where TPipelineToolConfiguration : class, new()
         where TLatchingInputBinding : class, new()
         where TOutputBinding : class, new()
         where TOutputEntity : class, new()
         where TInputEntity : class, new()
    { 
        string Id { get; set; }

        LinkedList<IQueueingPipelineNode<TPipelineTool,TPipelineToolConfiguration, TInputEntity, TOutputEntity>> PipelineToolChain { get; set; }

        /// <summary>
        /// mirror linkedlist api
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        String AddFirstNode(TPipelineTool node);

        /// <summary>
        /// mirror linked list api
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        String AddLastNode(TPipelineTool node);


        String AddAfter(String nodeId);

        bool Bind(String node1Id, String node2Id);
    }

}
