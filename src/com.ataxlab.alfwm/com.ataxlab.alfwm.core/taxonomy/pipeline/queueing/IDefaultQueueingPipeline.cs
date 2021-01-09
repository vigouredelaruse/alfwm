using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline.queueing
{
    /// <summary>
    /// furnish a queueing specialization of 
    /// the pipeline interface
    /// </summary>
    public interface IDefaultQueueingPipeline : IPipeline<IDefaultQueueingPipelineProcessDefinition>
    {
        QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingInputBinding { get; set; }
        IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingOutputBinding { get; set; }

        bool AddAfterPipelineNode(int pipelineNodeIndex, QueueingPipelineToolNode newNode);
        bool AddFirstPipelineNode(QueueingPipelineToolNode newNode);
        bool AddLastPipelineNode(QueueingPipelineToolNode newNode);
        void Deploy(DefaultQueueingPipelineProcessDefiniionEntity processDefinition);
        void EnsurePipelineIngressEgressBindings();
        void EnsurePipelineToolListeners(QueueingPipelineToolNode newNode);
    }

    public interface IQueueingPipeline<TProcessDefinition, TPipelineNode> : IPipeline<TProcessDefinition>
    {


        string AddTool(TPipelineNode node);
    }
}
