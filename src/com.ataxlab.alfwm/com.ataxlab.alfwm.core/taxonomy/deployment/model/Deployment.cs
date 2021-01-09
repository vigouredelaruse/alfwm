using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.deployment.model
{
    /// <summary>
    /// defines a mechanism for persisting process definition configurations
    /// 
    /// this model nudges you to think that
    /// a unit of deployment is a pipeline
    /// which contains a collection of sechedulable pipeline tools
    /// specified by TProcessDefinition
    /// monitored via TProcessDefinitionStatus
    /// 
    /// but if your spec disagrees, you have the flexibility
    /// of not having this forced down your throat
    /// with highly specific generic constraints
    /// </summary>
    /// <typeparam name="TProcessDefinition"></typeparam>
    /// <typeparam name="TDeploymentStatus"></typeparam>
    public class Deployment<TProcessDefinition, TDeploymentStatus> : IDeployment<TProcessDefinition, TDeploymentStatus>
        where TProcessDefinition : class
        where TDeploymentStatus : class

    {
        public string InstanceId { get; set; }

        /// <summary>
        /// a suitable collection of process definition elements
        /// probably a tree or even a linked list
        /// </summary>
        public TProcessDefinition ProcessDefinition { get; set; }

        public TDeploymentStatus DeploymentStatus { get; set; }
        public string DeploymentId { get; set; }
    }
}
