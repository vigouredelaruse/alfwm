using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.deployment
{
    /// <summary>
    /// defines a runtime container for process definitions
    /// somewhat implies the need for a container of containers
    /// </summary>
    /// <typeparam name="TProcessDefinitionItems"></typeparam>
    /// <typeparam name="TProcessDefinition"></typeparam>
    public class DeploymentContainer<TProcessDefinitionItems, TProcessDefinition> : IDeploymentContainer<TProcessDefinitionItems, TProcessDefinition>
                where TProcessDefinition : class
                where TProcessDefinitionItems : class
    {
        public string ContainerId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public TProcessDefinitionItems ProcessArtifact { get; set; }
    }
}
