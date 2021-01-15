using com.ataxlab.alfwm.core.deployment;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }

    /// <summary>
    /// a deployment container maps to a BPMN process
    /// and its subprocesses
    /// </summary>
    public class DefaultQueueingPipelineNodeDeploymentContainer : IDefaultQueueingPipelineNodeDeploymentContainer
    {
        public ObservableCollection<IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessDefinition>> Deployments { get; set; }
        public string ContainerId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DefaultQueueingChannelPipelineGateway PipelineGateway {get; set; }
    }
}
