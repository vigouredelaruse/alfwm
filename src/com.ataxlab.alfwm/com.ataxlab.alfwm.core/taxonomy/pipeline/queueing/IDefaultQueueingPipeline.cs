using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline.queueing
{
    /// <summary>
    /// furnish a queueing specialization of 
    /// the pipeline interface
    /// </summary>
    public interface IDefaultQueueingPipeline : IPipeline<IDefaultQueueingPipelineProcessDefinition>
    {
        #region pipeline toolchain input
        PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingInputBinding { get; set; }
        IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingOutputBinding { get; set; }
        #endregion pipeline toolchain input

        #region pipeline interconnects
        ObservableCollection<PipelineQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> QueueingPipelineInputs { get; set; }
        ObservableCollection<PipelineQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> QueueingPipelineOutputs { get; set; }
        #endregion pipeline interconnects

        bool AddAfterPipelineNode(int pipelineNodeIndex, DefaultQueueingPipelineToolNode newNode);
        bool AddFirstPipelineNode(DefaultQueueingPipelineToolNode newNode);
        bool AddLastPipelineNode(DefaultQueueingPipelineToolNode newNode);
        void Deploy(DefaultQueueingPipelineProcessDefinitionEntity processDefinition);
        void EnsurePipelineIngressEgressBindings();
        void EnsurePipelineToolListeners(DefaultQueueingPipelineToolNode newNode);
    }

    public interface IQueueingPipeline<TProcessDefinition, TPipelineNode> : IPipeline<TProcessDefinition>
    {


        string AddTool(TPipelineNode node);
    }
}
