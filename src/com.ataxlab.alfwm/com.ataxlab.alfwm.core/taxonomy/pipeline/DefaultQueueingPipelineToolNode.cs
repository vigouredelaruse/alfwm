using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline.queueing
{


    public class DefaultQueueingPipelineToolNode : IDefaultQueueingPipelineNode
    {


        public DefaultQueueingPipelineToolNode()
        {
            QueueingPipelineNodeId = Guid.NewGuid().ToString();
        }

        [XmlAttribute]
        public string QueueingPipelineNodeId { get; set; }

        [XmlElement]
        public IDefaultQueueingPipelineTool QueueingPipelineTool { get; set; }

        [XmlAttribute]
        public DefaultQueueingPipelineNodeTypeEnum QueueingPipelineNodeType { get; set; }

        [XmlElement]
        public IDefaultQueueingPipeline QueueingPipeline { get; set; }


        [XmlElement]
        public IDefaultQueueingChannelPipelineToolGateway QueueingPipelineToolGateway { get; set; }


        [XmlElement]
        public IDefaultQueueingChannelPipelineGateway QueueingPipelineGateway { get; set; }
    }

    public class QueueingPipelineNode<TPipelineTool> : IQueueingPipelineNode<TPipelineTool>
    //where TPipelineTool : 
    //                        IQueueingPipelineTool<
    //                            QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
    //                            QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
    //                            IPipelineToolConfiguration,
    //                            IPipelineToolConfiguration,
    //                            IPipelineToolConfiguration>
    {
        public QueueingPipelineNode()
        {

        }

        public TPipelineTool PipelineTool {get; set; }
        public string QueueingPipelineNodeId {get; set; }
        public DefaultQueueingPipelineNodeTypeEnum QueueingPipelineNodeType {get; set; }
        public IDefaultQueueingPipelineTool QueueingPipelineTool {get; set; }
        public IDefaultQueueingPipeline QueueingPipeline {get; set; }
        public IDefaultQueueingChannelPipelineToolGateway QueueingPipelineToolGateway {get; set; }
        public IDefaultQueueingChannelPipelineGateway QueueingPipelineGateway {get; set; }
    }


}
