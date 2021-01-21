using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.deployment.queueing
{
    /// <summary>
    /// convenience class for building queueing pipeline node  deployments 
    /// </summary>
    public class DefaultQueueingPipelineNodeDeploymentBuilder
    {
        private DefaultQueueingPipelineNodeDeployment deployment;
        private DefaultQueueingPipelineNodeDeploymentContext deploymentContext;

        private DefaultQueueingPipelineNodeDeploymentContainerBuilder _parentBuilder;

        public DefaultQueueingPipelineNodeDeploymentBuilder()
        {
            deployment = new DefaultQueueingPipelineNodeDeployment();
            deploymentContext = new DefaultQueueingPipelineNodeDeploymentContext();
        }

        public DefaultQueueingPipelineNodeDeploymentBuilder(DefaultQueueingPipelineNodeDeploymentContainerBuilder parent) : this()
        {
            _parentBuilder = parent;
        }

        public DefaultQueueingPipelineNodeDeployment Build(bool isMustResetBuilder)
        {
            return deployment;
        }

        public DefaultQueueingPipelineNodeDeploymentContainerBuilder WithDeploymentContext(DefaultQueueingPipelineNodeDeploymentContext ctx)
        {
            deployment.DeploymentContext = ctx;
            return _parentBuilder;

        }

        public DefaultQueueingPipelineNodeDeploymentContainerBuilder WithDeploymentId(string id)
        {
            deployment.DeploymentId = id;
            return _parentBuilder;
        }

        public DefaultQueueingPipelineNodeDeploymentContainerBuilder WithInstanceId(string id)
        {
            deployment.InstanceId = id;
            return _parentBuilder;
        }

        public DefaultQueueingPipelineNodeDeploymentContainerBuilder WithProcessDefinition(IDefaultQueueingPipelineProcessInstance processDefinition)
        {
            deployment.ProcessDefinitionInstance = processDefinition;
            return _parentBuilder;
        }
    }
}
