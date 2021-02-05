using com.ataxlab.alfwm.core.deployment.model;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using com.ataxlab.core.alfwm.utility.extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.deployment.queueing
{
    public class DefaultQueueingPipelineNodeDeploymentContext
    {

        public DefaultQueueingPipelineNodeDeploymentContext()
        {
            DeploymentTime = DateTime.UtcNow;
        }

        [XmlAttribute]
        public string CurrentDeploymentContainerId { get; set; }

        [XmlAttribute]

        public DateTime DeploymentTime { get; set; }

    }

    public interface IDefaultQueueingPipelineNodeDeployment : IDeployment<IDefaultQueueingPipelineProcessInstance>
    {

        DefaultQueueingPipelineNodeDeploymentContext DeploymentContext { get; set; }
        DefaultPipelineNodeQueueingPipeline DeployedPipeline { get; set; }
         
        DefaultQueueingPipelineProcessDefinitionEntity DeployedProcessDefinition { get; set; }

        void DeployProcessDefinition(DefaultQueueingPipelineProcessDefinitionEntity processDefinition);

        String ToXml();

    }

    /// <summary>
    /// encapsulates a process definition 
    /// </summary>
    public class DefaultQueueingPipelineNodeDeployment : IDefaultQueueingPipelineNodeDeployment
    {
        public DefaultQueueingPipelineNodeDeployment()
        {
            DeploymentContext = new DefaultQueueingPipelineNodeDeploymentContext();
            DeployedPipeline = new DefaultPipelineNodeQueueingPipeline();
            DeployedPipeline.PipelineCompleted += DeployedPipeline_PipelineCompleted;
            DeployedPipeline.PipelineProgressUpdated += DeployedPipeline_PipelineProgressUpdated;
            // listen to output from the deployed pipeline
            DeployedPipeline.QueueingOutputBinding.QueueHasData += QueueingOutputBinding_QueueHasData;
            DeploymentId = Guid.NewGuid().ToString();
            InstanceId = Guid.NewGuid().ToString();
            DeployedPipelineCompletionEvents = new ConcurrentDictionary<string, PipelineCompletedEventArgs>();
            DeployedPipelineQueueOutput = new ConcurrentDictionary<string, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>();
        }

        
        private void QueueingOutputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            DeployedPipelineQueueOutput.TryAdd(e.EventPayload.Id, e);
        }

        private void DeployedPipeline_PipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs e)
        {
            var payload = e.ToolProgressUpdatedEvent.InstanceId;

        }

        private void DeployedPipeline_PipelineCompleted(object sender, PipelineCompletedEventArgs e)
        {
            
            var payload = e.Payload;
            if (e.Payload != null)
            {
                DeployedPipelineCompletionEvents.TryAdd(payload.InstanceId, e);
            }
        }

        public DefaultQueueingPipelineNodeDeployment(DefaultQueueingPipelineNodeDeploymentContext ctx) : this()
        {
            this.DeploymentContext = ctx;
        }


        [XmlElement]
        public DefaultPipelineNodeQueueingPipeline DeployedPipeline { get; set; }


        [XmlAttribute]
        public DefaultQueueingPipelineNodeDeploymentContext DeploymentContext { get; set;}

        [XmlElement]
        public IDefaultQueueingPipelineProcessInstance ProcessDefinitionInstance { get; set;}

        [XmlAttribute]
        public string DeploymentId { get; set;}

        [XmlAttribute]
        public string InstanceId { get; set;}

        [XmlElement]
        public ConcurrentDictionary<string, PipelineCompletedEventArgs> DeployedPipelineCompletionEvents { get; set; }
        public ConcurrentDictionary<string, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> DeployedPipelineQueueOutput { get; set; }
        [XmlElement]
        public DefaultQueueingPipelineProcessDefinitionEntity DeployedProcessDefinition { get; set;}

        /// <summary>
        /// primary api for deploying process definitions to pipelines
        /// </summary>
        /// <param name="processDefinition"></param>
        public void DeployProcessDefinition(DefaultQueueingPipelineProcessDefinitionEntity processDefinition)
        {
            // TODO - here we must apply auditing to events coming from the pipeline
            // deploy the process definition to the pipeline
            this.DeployedPipeline?.Deploy(processDefinition);
            
            // cache the process definition
            this.DeployedProcessDefinition = processDefinition;
            if(this.DeployedProcessDefinition.Id == null || this.DeployedProcessDefinition.Id == string.Empty)
            {
                this.DeployedProcessDefinition.Id = Guid.NewGuid().ToString();
            }
            // surface the materialized process definition
            this.ProcessDefinitionInstance = new DefaultQueueingPipelineProcessInstance()
            {
                 Id = this.DeployedPipeline.ProcessDefinition.Id,
                 QueueingPipelineNodes = this.DeployedPipeline.ProcessDefinition.QueueingPipelineNodes
            };
        }

        public string ToXml()
        {
            return this.SerializeObject<DefaultQueueingPipelineNodeDeployment>();
        }
    }


}
