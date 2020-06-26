using com.ataxlab.alfwm.deployment.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.deployment
{
    public interface IDeploymentContainer
    {
        string ContainerId { get; set; }

        string DisplayName { get; set; }

        string Description { get; set; }
    }

    /// <summary>
    /// specify a mechanism for a runtimer container for deployments
    /// </summary>
    /// <typeparam name="TProcessDefinitionItems"></typeparam>
    /// <typeparam name="TProcessDefinition"></typeparam>
    /// <typeparam name="TDeploymentStatus"></typeparam>
    public interface IDeploymentContainer<TProcessDefinitionItems, TProcessDefinition>
        : IDeploymentContainer 
        where TProcessDefinition : class
        where TProcessDefinitionItems : class

    {
        TProcessDefinitionItems Deployments { get; set; }

    }
}
