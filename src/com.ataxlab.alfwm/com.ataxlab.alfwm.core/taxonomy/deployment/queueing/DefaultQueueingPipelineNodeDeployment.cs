using com.ataxlab.alfwm.core.deployment.model;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using com.ataxlab.core.alfwm.utility.extension;
using System;
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
            DeploymentId = Guid.NewGuid().ToString();
            InstanceId = Guid.NewGuid().ToString();
        }

        public DefaultQueueingPipelineNodeDeployment(DefaultQueueingPipelineNodeDeploymentContext ctx) : this()
        {
            this.DeploymentContext = ctx;
        }

        [XmlAttribute]
        public DefaultQueueingPipelineNodeDeploymentContext DeploymentContext { get; set;}

        [XmlElement]
        public IDefaultQueueingPipelineProcessInstance ProcessDefinition { get; set;}

        [XmlAttribute]
        public string DeploymentId { get; set;}

        [XmlAttribute]
        public string InstanceId { get; set;}

        public string ToXml()
        {
            return this.SerializeObject<DefaultQueueingPipelineNodeDeployment>();
        }
    }


}
