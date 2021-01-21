using com.ataxlab.alfwm.core.deployment.model;
using com.ataxlab.alfwm.core.taxonomy.deployment.queueing;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.deployment
{
    public interface IDeploymentNode<TDeployment, TProcessInstance>
    {
        Tuple<TDeployment, TProcessInstance> Value { get; set; }

    }

    public interface IDefaultDeploymentNode : IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance>
    {

    }

    public class DefaultDeploymentNode : IDefaultDeploymentNode
    {
        public Tuple<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance> Value { get; set; }
    }

    public class DefaultDeploymentNode<TDeployment, TProcessInstance> : IDeploymentNode<TDeployment, TProcessInstance>
    {

        public DefaultDeploymentNode()
        {

        }

        public Tuple<TDeployment, TProcessInstance> Value {get; set;}
    }

    /// <summary>
    /// abstration at lowest level useful for building
    /// 'Managers' - like WorkflowManager or Process Engine
    /// 
    /// when there is a need for a component that contains
    /// and potentially participates in scheduling multiple workflow processes
    /// </summary>
    /// 
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
    /// <typeparam name="TProcessInstance"></typeparam>
    /// <typeparam name="TDeploymentStatus"></typeparam>
    public interface IDeploymentContainer<TDeployment, TProcessInstance>
        : IDeploymentContainer 
        //where TProcessDefinition : class
        //where TDeployment : class

    {
        ObservableCollection<IDeploymentNode<TDeployment, TProcessInstance>> Deployments { get; set; }
    }

    public interface IDeploymentContainer<TDeploymentContainerNode> : IDeploymentContainer
    {
        ObservableCollection<TDeploymentContainerNode> Deployments { get; set; }
    }

}
