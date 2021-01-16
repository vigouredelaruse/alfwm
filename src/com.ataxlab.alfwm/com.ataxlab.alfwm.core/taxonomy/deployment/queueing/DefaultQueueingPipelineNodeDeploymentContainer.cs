﻿using com.ataxlab.alfwm.core.deployment;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.core.alfwm.utility.extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.deployment.queueing
{
    /// <summary>
    /// specifies the interface for a container of 1 or more Process definition deployments
    /// </summary>
    public interface IDefaultQueueingPipelineNodeDeploymentContainer : IDeploymentContainer<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition>
    {
        /// <summary>
        /// specify the mechanism whereby pipelines can send messages to each other
        /// </summary>
        DefaultQueueingChannelPipelineGateway PipelineGateway { get; set; }

        void ProvisionDeployment(IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition> deployment);

        string ToXMl();
    }

    /// <summary>
    /// a deployment container maps to a BPMN process
    /// and its subprocesses
    /// </summary>
	public class DefaultQueueingPipelineNodeDeploymentContainer : IDefaultQueueingPipelineNodeDeploymentContainer
    {
        public DefaultQueueingPipelineNodeDeploymentContainer()
        {
            Deployments = new ObservableCollection<IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition>>();
            ContainerId = Guid.NewGuid().ToString();
            PipelineGateway = new DefaultQueueingChannelPipelineGateway();
        }


        [XmlElement]
        public ObservableCollection<IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition>> Deployments { get; set; }

        [XmlAttribute]
        public string ContainerId { get; set; }


        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlElement]
        public DefaultQueueingChannelPipelineGateway PipelineGateway { get; set; }

        public void ProvisionDeployment(IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition> deployment)
        {
            throw new NotImplementedException();
        }

        public string ToXMl()
        {
            return this.SerializeObject<DefaultQueueingPipelineNodeDeploymentContainer>();
        }
    }
}