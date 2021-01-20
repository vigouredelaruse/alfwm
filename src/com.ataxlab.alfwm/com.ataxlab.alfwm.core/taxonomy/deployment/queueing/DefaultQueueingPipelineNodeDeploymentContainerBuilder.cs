using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.deployment.queueing
{
    public class DefaultQueueingPipelineNodeDeploymentContainerBuilder
    {
        private DefaultQueueingPipelineNodeDeploymentContainer container;
        public DefaultQueueingPipelineNodeDeploymentBuilder UseDeploymentBuilder;

        public DefaultQueueingPipelineNodeDeploymentContainerBuilder()
        {
            container = new DefaultQueueingPipelineNodeDeploymentContainer();

            UseDeploymentBuilder = new DefaultQueueingPipelineNodeDeploymentBuilder(this);
        }

    }
}
