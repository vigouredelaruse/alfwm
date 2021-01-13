using com.ataxlab.alfwm.core.deployment.model;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.deployment.queueing
{
    public class DefaultQueueingPipelineNodeDeploymentContext
    {
        public DefaultQueueingPipelineNodeDeploymentContext()
        {

        }

        public string CurrentDeploymentContainerId { get; set; }

        public DateTime DeploymentTime { get; set; }

    }

    public interface IDefaultQueueingPipelineNodeDeployment : IDeployment<IDefaultQueueingPipelineProcessDefinition>
    {

        DefaultQueueingPipelineNodeDeploymentContext DeploymentContext { get; set; }

    }

    /// <summary>
    /// encapsulates a process definition 
    /// </summary>
    public class DefaultQueueingPipelineNodeDeployment : IDefaultQueueingPipelineNodeDeployment
    {
        public DefaultQueueingPipelineNodeDeployment()
        {
            DeploymentContext = new DefaultQueueingPipelineNodeDeploymentContext();
            DeploymentId = Guid.NewGuid().ToString();
            InstanceId = Guid.NewGuid().ToString();
        }

        public DefaultQueueingPipelineNodeDeploymentContext DeploymentContext { get; set;}
        public IDefaultQueueingPipelineProcessDefinition ProcessDefinition { get; set;}
        public string DeploymentId { get; set;}
        public string InstanceId { get; set;}
    }


}
