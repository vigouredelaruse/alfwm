using com.ataxlab.alfwm.core.deployment.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.deployment
{
    /// <summary>
    /// abstration at lowest level useful for building
    /// 'Managers' - like WorkflowManager or Process Engine
    /// 
    /// when there is a need for a component that contains
    /// and potentially participates in scheduling multiple workflow processes
    /// </summary>
    public interface IDeploymentContainer
    {
        string ContainerId { get; set; }

        string DisplayName { get; set; }

        string Description { get; set; }
    }

    /// <summary>
    /// specify a mechanism for a runtimer container for deployments
    /// </summary>
    /// <typeparam name="TDeployment"></typeparam>
    /// <typeparam name="TProcessDefinition"></typeparam>
    /// <typeparam name="TDeploymentStatus"></typeparam>
    public interface IDeploymentContainer<TDeployment, TProcessDefinition>
        : IDeploymentContainer 
        //where TProcessDefinition : class
        //where TDeployment : class

    {
    }
}
