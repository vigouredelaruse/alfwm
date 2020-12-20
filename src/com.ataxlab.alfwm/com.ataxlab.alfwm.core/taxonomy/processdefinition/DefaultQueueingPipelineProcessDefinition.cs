using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{

    public class QueueingPipelineProcessDefinitionEx<TPipelineTool, TPipelineToolConfiguration, TLatchingInputBinding, TLatchingOutputBinding, TInputEntity, TOutputEntity>
    : IQueueingPipelineProcessDefinition<TPipelineTool,
                                                    TLatchingInputBinding, TLatchingOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity>
    //where TPipelineToolConfiguration : class, new()
    //where TPipelineToolConfiguration : class, IPipelineToolConfiguration, new()
    // where TInputEntity : class, IPipelineToolConfiguration, new()
    // where TOutputEntity : class, IPipelineToolConfiguration, new()
    // where TPipelineTool : class,  new()
    // where TLatchingInputBinding : class, new() // class, IQueueConsumerPipelineToolBinding<QueueingPipelineQueueEntity<TInputEntity>>, new()
     // where TLatchingOutputBinding : class, new() // class, IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<TOutputEntity>>, new()
    {
        public string Id {get; set; }
        public LinkedList<TPipelineTool> PipelineTools {get; set; }
        public LinkedList<IQueueingPipelineNode<TPipelineTool, TPipelineToolConfiguration, TInputEntity, TOutputEntity>> PipelineToolChain {get; set; }

        public LinkedList<QueueingPipelineNode> QueueingPipelineNodes { get; set; }

        public string AddAfter(string nodeId)
        {
            throw new NotImplementedException();
        }

        public string AddFirstNode(TPipelineTool node)
        {
            int i = 0;
            return "";
        }

        public string AddLastNode(TPipelineTool node)
        {
            throw new NotImplementedException();
        }

        public bool Bind(string node1Id, string node2Id)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// a dictionary of specialized IPipelineTools
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public class DefaultQueueingPipelineProcessDefinition : IDefaultQueueingPipelineProcessDefinition

    {

        public DefaultQueueingPipelineProcessDefinition()
        {
            PipelineToolChain = new ConcurrentDictionary<string, IQueueingPipelineNode>();
            QueueingPipelineNodes = new LinkedList<IQueueingPipelineNode>();
            PipelineTools = new LinkedList<QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration>>();
        }
        public string Id { get; set; }
        public ConcurrentDictionary<string, IQueueingPipelineNode> PipelineToolChain { get; set; }
        public LinkedList<IQueueingPipelineNode> QueueingPipelineNodes { get; set; }
        public LinkedList<QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration>> PipelineTools { get; set; }

        public string AddTool(QueueingPipelineToolBase<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, QueueingPipelineQueueEntity<IPipelineToolConfiguration>, IPipelineToolConfiguration> node)
        {
            return string.Empty;
        }

        public bool Bind(string node1Id, string node2Id)
        {
            return false;
        }
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
    public class QueueingPipelineProcessDefinition<TPipelineNode>
        : IQueueingPipelineProcessDefinition<TPipelineNode>
        where TPipelineNode : QueueingPipelineNode<
                            IQueueingPipelineTool<
                                                    QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                                                    QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                                                    IPipelineToolConfiguration,
                                                    IPipelineToolConfiguration,
                                                    IPipelineToolConfiguration
                                                  >
                                        >
    {
        public QueueingPipelineProcessDefinition()
        {
            this.PipelineTools = new LinkedList<TPipelineNode>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set;}
        public LinkedList<TPipelineNode> PipelineTools { get; set;}

        public bool Bind(string node1Id, string node2Id)
        {
            return false;
        }
    }
}
