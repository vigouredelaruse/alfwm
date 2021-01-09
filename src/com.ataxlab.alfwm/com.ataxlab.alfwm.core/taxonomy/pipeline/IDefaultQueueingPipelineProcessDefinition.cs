﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public interface IDefaultQueueingPipelineProcessDefinition
    {
        string Id { get; set; }

        LinkedList<QueueingPipelineToolNode> QueueingPipelineNodes { get; set; }


    }

    //public interface IQueueingPipelineProcessDefinition<TProcessDefinition>
    //    // where TProcessDefinition : class, new()
    //{ 
    //    string Id { get; set; }

    //    TProcessDefinition PipelineToolChain { get; set; }
    //}

    public interface IQueueingPipelineProcessDefinition<TPipelineNode> where TPipelineNode :
                    QueueingPipelineNode<
                            IQueueingPipelineTool<
                                                    PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                                                    PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                                                    IPipelineToolConfiguration,
                                                    IPipelineToolConfiguration,
                                                    IPipelineToolConfiguration
                                                  >
                                        >

    {
        string Id { get; set; }

        LinkedList<TPipelineNode> PipelineTools { get; set; }


        bool Bind(string node1Id, string node2Id);

    }

    public interface IQueueingPipelineProcessDefinition<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
    // where TPipelineTool : IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>
    // where TPipelineToolConfiguration : IPipelineToolConfiguration // class, IPipelineToolConfiguration, new()
    // where TInputEntity : class, IPipelineToolConfiguration, new()
    // where TOutputEntity : class, IPipelineToolConfiguration, new()
        // where TPipelineTool : class, new()
    // where TPipelineTool : class, IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>, new()
     // where TLatchingInputBinding : class, new() // class, IQueueConsumerPipelineToolBinding<QueueingPipelineQueueEntity<TInputEntity>>, new()
      // where TOutputBinding : class, new() // class, IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<TOutputEntity>>, new()
    { 
        string Id { get; set; }

        LinkedList<TPipelineTool> PipelineTools { get; set; }

        [Obsolete]
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
