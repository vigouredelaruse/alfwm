﻿using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    /// <summary>
    /// a dictionary of specialized IPipelineTools
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public class QueueingPipelineProccessDefinition : IQueueingPipelineProcessDefinition
        
    {
        public QueueingPipelineProccessDefinition()
        {
            this.PipelineToolChain = new ConcurrentDictionary<string, IQueueingPipelineNode>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public ConcurrentDictionary<string, IQueueingPipelineNode> PipelineToolChain { get ; set ; }
 
    }

    /// <summary>
    /// Queue Process Definition implememted as a linked list
    /// of IQueueingPipelineNodes, where each node has an IPipelineTool
    /// and an Input binding and an Output binding
    /// </summary>
    /// <typeparam name="TPipelineTool"></typeparam>
    /// <typeparam name="TLatchingInputBinding"></typeparam>
    /// <typeparam name="TOutputBinding"></typeparam>
    /// <typeparam name="TPipelineToolConfiguration"></typeparam>
    /// <typeparam name="TInputEntity"></typeparam>
    /// <typeparam name="TOutputEntity"></typeparam>
    public class QueueingPipelineProcessDefinition<TPipelineToolConfiguration, TLatchingInputBinding, TLatchingOutputBinding, TInputEntity, TOutputEntity>
        : IQueueingPipelineProcessDefinition<LinkedList<IQueueingPipelineNode<IQueueingPipelineTool<TLatchingInputBinding, TLatchingOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>,
            TPipelineToolConfiguration, TInputEntity, TOutputEntity>>>
        where TPipelineToolConfiguration : class, new()
        where TInputEntity : class, new()
        where TOutputEntity : class, new()
        where TLatchingOutputBinding : class, new()
        where TLatchingInputBinding : class, new()
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LinkedList<IQueueingPipelineNode<IQueueingPipelineTool<TLatchingInputBinding, TLatchingOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration>, TPipelineToolConfiguration, TInputEntity, TOutputEntity>> PipelineToolChain { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
