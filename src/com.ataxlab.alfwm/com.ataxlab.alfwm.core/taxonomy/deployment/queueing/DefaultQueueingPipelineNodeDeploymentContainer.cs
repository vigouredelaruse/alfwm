﻿using com.ataxlab.alfwm.core.deployment;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
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
    public interface IDefaultQueueingPipelineNodeDeploymentContainer : IDeploymentContainer<IDefaultDeploymentNode>//  IDeploymentContainer<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance>
    {
        /// <summary>
        /// specify the mechanism whereby pipelines can send messages to each other
        /// </summary>
        DefaultQueueingChannelPipelineGateway PipelineGateway { get; set; }


        void ProvisionDeployment(IDefaultDeploymentNode deployment);

        event EventHandler<QueueingPipelineNodeContainerDeploymentSuccededEventArgs> DeploymentSucceded;

        string ToXMl();
    }

    public class QueueingPipelineNodeContainerDeploymentSuccededEventArgs : EventArgs
    {
        public QueueingPipelineNodeContainerDeploymentSuccededEventArgs()
        {

        }

        public DateTime DeploymentSucceededAt { get; set; }
    }

    /// <summary>
    /// a deployment container maps to a BPMN process
    /// and its subprocesses
    /// </summary>
    public class DefaultQueueingPipelineNodeDeploymentContainer : IDefaultQueueingPipelineNodeDeploymentContainer
    {
        public DefaultQueueingPipelineNodeDeploymentContainer()
        {
            Deployments = new ObservableCollection<IDefaultDeploymentNode>();
            ContainerId = Guid.NewGuid().ToString();
            PipelineGateway = new DefaultQueueingChannelPipelineGateway();

            // listen to traffic coming into the gateway
            
        }


        //[XmlElement]
        //public ObservableCollection<IDeploymentNode<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance>> Deployments { get; set; }

        [XmlAttribute]
        public string ContainerId { get; set; }


        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlElement]
        public DefaultQueueingChannelPipelineGateway PipelineGateway { get; set; }
        public ObservableCollection<IDefaultDeploymentNode> Deployments { get; set; }
        public string ContainerInstanceId { get; set; }

        public event EventHandler<QueueingPipelineNodeContainerDeploymentSuccededEventArgs> DeploymentSucceded;

        public string ToXMl()
        {
            return this.SerializeObject<DefaultQueueingPipelineNodeDeploymentContainer>();
        }


        public void ProvisionDeployment(IDefaultDeploymentNode deploymentNode)
        {

            try
            {
                // TODO distinguish between deployments and redeployments
                // wire gateway to deployed pipeline
                // set container id
                deploymentNode.Payload.Item1.DeploymentContext.CurrentDeploymentContainerId = this.ContainerId;

                EnsureDeploymentNodeBindings(deploymentNode);

                Deployments.Add(deploymentNode);

                OnDeploymentSuceeded();
            }
            catch (Exception e)
            {
                // TODO deployments can fail
            }
        }

        private void OnDeploymentSuceeded(QueueingPipelineNodeContainerDeploymentSuccededEventArgs eventArgs = null)
        {
            if (eventArgs == null)
            {
                eventArgs = new QueueingPipelineNodeContainerDeploymentSuccededEventArgs() { DeploymentSucceededAt = DateTime.UtcNow };
            }

            DeploymentSucceded?.Invoke(this, eventArgs);
        }

        private void EnsureDeploymentNodeBindings(IDefaultDeploymentNode deploymentNode)
        {

            // wire deployed pipeline to the container's gateway
            this.PipelineGateway.OutputPorts.Add(deploymentNode.Payload.Item1.DeployedPipeline.QueueingInputBinding);
            deploymentNode.Payload.Item1.DeployedPipeline.QueueingInputBinding.QueueHasData += DeployedPipelineInputBinding_QueueHasData;

            this.PipelineGateway.InputPorts.Add(deploymentNode.Payload.Item1.DeployedPipeline.QueueingOutputBinding);
            deploymentNode.Payload.Item1.DeployedPipeline.QueueingOutputBinding.QueueHasData += DeployedPipelineOutputBinding_QueueHasData;
        }

        /// <summary>
        /// switch data sent from pipelines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeployedPipelineOutputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            int i = 0;
        }

        /// <summary>
        /// observe data sent to pipelines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeployedPipelineInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            int i = 0;
        }

        public void ProvisionDeployment(DefaultQueueingPipelineNodeDeployment deployment)
        {
            try
            {                
                
                // TODO distinguish between deployments and redeployments
                // wire gateway to deployed pipeline
                var deploymentNode = new DefaultDeploymentNode()
                {
                    Payload = new Tuple<IDefaultQueueingPipelineNodeDeployment, IDefaultQueueingPipelineProcessInstance>(deployment, deployment.ProcessDefinitionInstance)
                };

                // TODO distinguish between deployments and redeployments
                Deployments.Add(deploymentNode);

                EnsureDeploymentNodeBindings(deploymentNode);

                OnDeploymentSuceeded();
            }
            catch (Exception e)
            {
                // TODO deployments can fail
            }
        }
    }
}
