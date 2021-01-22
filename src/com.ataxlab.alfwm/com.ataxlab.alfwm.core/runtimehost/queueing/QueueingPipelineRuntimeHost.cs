using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.deployment.queueing;
using com.ataxlab.core.alfwm.utility.extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.runtimehost.queueing
{

    public class QueueingPipelineRuntimeHost : IDefaultQueueingPipelineRuntimeHost
    {
        public QueueingPipelineRuntimeHost()
        {
            // TODO introduce the MAC address into the context
            Context = new DefaultQueueingPipelineRuntimeHostContext()
            {
                HostStartedAt = DateTime.UtcNow
                
                
            };
            RuntimeHostId = Guid.NewGuid().ToString();
            RuntimeHostInstanceId = Guid.NewGuid().ToString();
            DeployedContainers = new ConcurrentDictionary<string, IDefaultQueueingPipelineNodeDeploymentContainer>();
            GatewayHub = new DefaultQueueingChannelPipelineGateway();

        }

        public QueueingPipelineRuntimeHost(DefaultQueueingPipelineRuntimeHostContext ctx) : this()
        {
            this.Context = ctx;
        }

        [XmlAttribute]
        public string RuntimeHostId { get; set; }
        public string RuntimeHostInstanceId { get; private set; }
        [XmlAttribute]
        public string RuntimeHostDisplayName { get; set; }

        [XmlElement]
        public IRuntimeHostContext Context { get; set; }

        [XmlElement]
        public ConcurrentDictionary<string, IDefaultQueueingPipelineNodeDeploymentContainer> DeployedContainers { get; set; }

        public event EventHandler<RuntimeHostDeploymentFailedEventArgs> DeploymentFailed;
        public event EventHandler<RuntimeHostDeploymentSuceededEventArgs> DeploymenSuceeded;

        /// <summary>
        /// expose the gateways deployed in the runtime host
        /// </summary>
        [XmlIgnore]
        public List<IDefaultQueueingChannelPipelineGateway> DeployedGateways
        {
            get
            {
                return DeployedContainers.Select(w => w.Value.PipelineGateway).ToList<IDefaultQueueingChannelPipelineGateway>();
            }
        }

        [XmlElement]
        /// <summary>
        /// provides messaging interconnect between deployed pipelines
        /// </summary>
        public IDefaultQueueingChannelPipelineGateway GatewayHub { get; set; }

        public void Deploy(IDefaultQueueingPipelineNodeDeploymentContainer container)
        {
            try
            {
                if (ValidateDeployment(container))
                {
                    // add the container ot our collection of deployed containers
                    this.DeployedContainers.TryAdd(container?.ContainerId, container);
                    EnsureContainerFulDuplexWiring(container);

                    this.DeploymenSuceeded?.Invoke(container?.ContainerId, new RuntimeHostDeploymentSuceededEventArgs() { DeployedContainerId = container?.ContainerId });
                }
                else
                {

                    DeploymentFailed?.Invoke(container?.ContainerId, new RuntimeHostDeploymentFailedEventArgs() { FailingContainerId = container?.ContainerId });
                }
            }
            catch (Exception e)
            {
                DeploymentFailed?.Invoke(container?.ContainerId, new RuntimeHostDeploymentFailedEventArgs() { FailingContainerId = container?.ContainerId });
            }
        }

        private void EnsureContainerFulDuplexWiring(IDefaultQueueingPipelineNodeDeploymentContainer container)
        {

            // wire the deployed container's gateway input port to the output ports collection of the hub gateway
            // and vice versa
            //this.GatewayHub.OutputPorts.Add(container.PipelineGateway.FullDuplexUplinkChannel.InputPort);

            //this.GatewayHub.InputPorts.Add(container.PipelineGateway.FullDuplexUplinkChannel.OutputPort);
        }

        /// <summary>
        /// todo - implement more robust validation
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool ValidateDeployment(IDefaultQueueingPipelineNodeDeploymentContainer container)
        {
            return container == null;
        }

        public string ToXml()
        {
            return this.SerializeObject<QueueingPipelineRuntimeHost>();
        }
    }


    public interface IDefaultQueueingPipelineRuntimeHostContext : IRuntimeHostContext
    {

    }

    public class DefaultQueueingPipelineRuntimeHostContext : IDefaultQueueingPipelineRuntimeHostContext
    {
        public DefaultQueueingPipelineRuntimeHostContext()
        {

        }

        public DateTime HostStartedAt { get; set; }
        public string RuntimeHostAddress { get; set; }
    }


    public interface IDefaultQueueingPipelineRuntimeHost : IRuntimeHost
    {
        IDefaultQueueingChannelPipelineGateway GatewayHub { get; set; }

        ConcurrentDictionary<string, IDefaultQueueingPipelineNodeDeploymentContainer> DeployedContainers { get; set; }
        List<IDefaultQueueingChannelPipelineGateway> DeployedGateways { get; }

        void Deploy(IDefaultQueueingPipelineNodeDeploymentContainer container);
        bool ValidateDeployment(IDefaultQueueingPipelineNodeDeploymentContainer container);

        event EventHandler<RuntimeHostDeploymentFailedEventArgs> DeploymentFailed;
        event EventHandler<RuntimeHostDeploymentSuceededEventArgs> DeploymenSuceeded;

        string ToXml();

    }



    public class RuntimeHostDeploymentSuceededEventArgs : EventArgs
    {
        public RuntimeHostDeploymentSuceededEventArgs()
        {

        }

        public string DeployedContainerId { get; set; }
    }

    /// <summary>
    /// todo - hydrate this class with UI friendly deployment failure properties
    /// </summary>

    public class RuntimeHostDeploymentFailedEventArgs : EventArgs
    {
        public RuntimeHostDeploymentFailedEventArgs()
        {

        }

        public string FailingContainerId { get; set; }
    }
}