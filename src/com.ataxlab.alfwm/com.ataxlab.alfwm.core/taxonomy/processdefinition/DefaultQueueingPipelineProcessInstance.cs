﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using com.ataxlab.core.alfwm.utility.extension;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.processdefinition
{
    public enum QueueingPipelineNodeType { PipelineTool, PipelineToolGateway}

    [XmlType("DefaultQueueingChannelPipelineToolGatewayContext")]
    public class DefaultQueueingChannelPipelineToolGatewayContextEntity
    {
        public DefaultQueueingChannelPipelineToolGatewayContextEntity()
        {

        }

        [XmlAttribute]
        public long MessageCount { get; set; }
        [XmlAttribute]
        public string CurrentPipelineId { get; set; }
        [XmlAttribute]
        public int DeadLetterCount { get; set; }
    }

    [XmlType("DefaultQueueingChannelPipelineToolGateway")]
    [XmlInclude(typeof(DefaultQueueingChannelPipelineToolGatewayContextEntity))]
    public class DefaultQueueingChannelPipelineToolGatewayEntity
    {
        public DefaultQueueingChannelPipelineToolGatewayEntity()
        {
            GatewayContext = new DefaultQueueingChannelPipelineToolGatewayContextEntity();
        }

        [XmlElement]
        public DefaultQueueingChannelPipelineToolGatewayContextEntity GatewayContext { get; set; }
    }

    [XmlType("QueueingPipelineTool")]
    [XmlInclude(typeof(PipelineVariable))]
    [XmlInclude(typeof(List<PipelineVariable>))]

    public class QueueingPipelineToolEntity
    {
        public QueueingPipelineToolEntity()
        {
            PipelineToolVariables = new ObservableCollection<PipelineVariable>();
            PipelineToolId = Guid.NewGuid().ToString();
        }


        [XmlArray("PipelineVariables")]

        [XmlArrayItem("PipelineVariable", typeof(PipelineVariable))]
        public ObservableCollection<PipelineVariable> PipelineToolVariables { get; set; }

        [XmlAttribute]
        public string QueueingPipelineToolClassName { get; set; }


        [XmlAttribute]
        public string PipelineToolDisplayName { get; set; }


        [XmlAttribute]
        public string PipelineToolId { get; set; }

        [XmlAttribute]
        public string PipelineToolDescription { get; set; }
    }

    [XmlType("QueueingPipelineNode")]
    public class QueueingPipelineNodeEntity
    {
        public QueueingPipelineNodeEntity()
        {
            InstanceId = Guid.NewGuid().ToString();
            QueueingPipelineToolGateway = new DefaultQueueingChannelPipelineToolGatewayEntity();
        }

        [XmlAttribute]
        public QueueingPipelineNodeType NodeType { get; set; }

        [XmlAttribute]
        public string ClassName { get; set; }

        [XmlAttribute]

        public string InstanceId { get; set; }

        [XmlAttribute]

        public int ToolChainSlotNumber { get; set; }

        [XmlElement]
        public QueueingPipelineToolEntity QueueingPipelineTool { get; set; }

        [XmlElement]
        public DefaultQueueingChannelPipelineToolGatewayEntity QueueingPipelineToolGateway { get; set; }

    }

    [XmlType("QueueingPipelineProcessDefinition")]
    [XmlInclude(typeof(QueueingPipelineNodeEntity))]
    public class DefaultQueueingPipelineProcessDefinitionEntity
    {
        public DefaultQueueingPipelineProcessDefinitionEntity()
        {
            QueueingPipelineNodes = new List<QueueingPipelineNodeEntity>();
            Id = Guid.NewGuid().ToString();
        }

        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// enforce the idea that the enum defines the applicable payload properties
        /// </summary>
        [XmlIgnore]
        public LinkedList<Tuple<DefaultQueueingPipelineNodeTypeEnum, DefaultQueueingPipelineToolNode>> QueueingPipelineTuples { get; set; }


        [XmlArray("QueueingPipelineNodes")]

        [XmlArrayItem("QueueingPipelineNode", typeof(QueueingPipelineNodeEntity))]
        public List<QueueingPipelineNodeEntity> QueueingPipelineNodes { get; set; }

        public string ToXml()
        {
            return this.SerializeObject<DefaultQueueingPipelineProcessDefinitionEntity>();
        }
    }

    /// <summary>
    /// a dictionary of specialized IPipelineTools
    /// </summary>
    /// <typeparam name="TConfiguration"></typeparam>
    public class DefaultQueueingPipelineProcessInstance : IDefaultQueueingPipelineProcessInstance

    {

        public DefaultQueueingPipelineProcessInstance()
        {

            QueueingPipelineNodes = new LinkedList<DefaultQueueingPipelineToolNode>();
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public LinkedList<DefaultQueueingPipelineToolNode> QueueingPipelineNodes { get; set; }

        /// <summary>
        /// enforce the idea that the enum defines the applicable payload properties
        /// </summary>
        [Obsolete]
        public LinkedList<Tuple<DefaultQueueingPipelineNodeTypeEnum, DefaultQueueingPipelineToolNode>> QueueingPipelineTuples { get; set; }

    }

}
