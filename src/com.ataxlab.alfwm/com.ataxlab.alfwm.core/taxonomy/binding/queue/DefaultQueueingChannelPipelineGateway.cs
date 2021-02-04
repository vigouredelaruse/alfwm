using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{

    /// <summary>
    /// provides a ay to connect the gateway to other gateways via gateway hubs
    /// </summary>
    public interface IDefaultQueueingPipelineGatewayUplink<TEntity>
    {
        PipelineQueueingProducerChannel<TEntity> OutputPort { get; set; }

        PipelineQueueingConsumerChannel<TEntity> InputPort { get; set; }

    }

    public class DefaultQueueingPipelineGatewayUplink : IDefaultQueueingPipelineGatewayUplink<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>
    {
        public DefaultQueueingPipelineGatewayUplink()
        {
            OutputPort = new PipelineQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            InputPort = new PipelineQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
        }

        public PipelineQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> OutputPort { get; set; }
        public PipelineQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> InputPort { get; set; }
     }

    /// <summary>
    /// specify a gateway that interfaces pipelines
    /// </summary>
    public interface  IDefaultQueueingChannelPipelineGateway : 
        IDefaultQueueingChannelPipelineGateway<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
                                                QueueingPipelineQueueEntity<IPipelineToolConfiguration>>
    {
        DefaultQueueingChannelPipelineGatewayContext GatewayContext { get; set; }
        ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> DeadLetters { get; }
        ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> PipelineEgressPort { get; }
        DefaultQueueingPipelineGatewayUplink FullDuplexUplinkChannel { get; set; }
    }

    public interface IDefaultQueueingChannelPipelineGateway<TInputEntity, TOutputEntity>
    {
        string Id { get; set; }
        ObservableCollection<PipelineToolQueueingConsumerChannel<TOutputEntity>> OutputPorts { get; set; }

        ObservableCollection<PipelineToolQueueingProducerChannel<TOutputEntity>> InputPorts { get; set; }

        void HandleInputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);

        void HandleOutputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);
    }

    /// <summary>
    /// the gateway needs information about its environment
    /// </summary>
    public class DefaultQueueingChannelPipelineGatewayContext
    {
        public DefaultQueueingChannelPipelineGatewayContext()
        {
            SeenPipelineIds = new ObservableCollection<string>();
        }
        public long DeadLetterCount { get; set; }
        public long MessageCount { get; set; }
        public ObservableCollection<string> SeenPipelineIds { get; set; }
        public string GatewayId { get;  set; }
    }

    public class DefaultQueueingChannelPipelineGateway : IDefaultQueueingChannelPipelineGateway
    {
        public DefaultQueueingChannelPipelineGateway()
        {
            Id = Guid.NewGuid().ToString();           
            DeadLetters = new ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            PipelineEgressPort = new ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            FullDuplexUplinkChannel = new DefaultQueueingPipelineGatewayUplink();
            GatewayContext = new DefaultQueueingChannelPipelineGatewayContext() { GatewayId = this.Id };

            OutputPorts = new ObservableCollection<PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>();
            InputPorts = new ObservableCollection<PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>();

            OutputPorts.CollectionChanged += OutputPorts_CollectionChanged;
            InputPorts.CollectionChanged += InputPorts_CollectionChanged;
        }

        public DefaultQueueingChannelPipelineGateway(DefaultQueueingChannelPipelineGatewayContext ctx) :this()
        {
            this.GatewayContext = ctx;
        }

        private void InputPorts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleInputPortsCollectionChanged(e);
        }

        private void OutputPorts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleOutputPortsCollectionChanged(e);
        }

        [XmlElement]
        public DefaultQueueingChannelPipelineGatewayContext GatewayContext {get; set; }
        public ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> DeadLetters { get; private set; }

        [XmlAttribute]
        public string Id {get; set; }
        public ObservableCollection<PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> OutputPorts {get; set; }
        public ObservableCollection<PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> InputPorts {get; set; }

        public ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> PipelineEgressPort { get; set; }
        public DefaultQueueingPipelineGatewayUplink FullDuplexUplinkChannel { get; set; }

        public void HandleInputPortsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        // listen to queue arrival events
                        foreach (var item in e.NewItems)
                        {
                            try
                            {
                                ((PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>)item).QueueHasData += InputQueue_QueueHasData;
                            }
                            catch(Exception ex)
                            {
                                int i = 0;
                            }
                        }

                        break;
                    }

                case NotifyCollectionChangedAction.Remove:
                    {
                        break;
                    }

                case NotifyCollectionChangedAction.Replace:
                    {

                        // unlisten to queue arrival events
                        foreach (var item in e.OldItems)
                        {
                            // todo remove listeners
                        }

                        // listen to queue arrival events
                        foreach (var item in e.NewItems)
                        {
                            ((PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>)item).QueueHasData += InputQueue_QueueHasData;
                        }

                        break;
                    }

                case NotifyCollectionChangedAction.Reset:
                    {
                        break;
                    }

                case NotifyCollectionChangedAction.Move:
                    {
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void InputQueue_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            // the gateway treats a null routingslip 
            // as a deliver to nobody scenario, ergo deadletter
            if (IsDeadLetter(e))
            {
                // dequeue the message
                var firingChannel = this.InputPorts.Where(w => w.Id.Equals(e.SourceChannelId)).FirstOrDefault();
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> msg;
                firingChannel?.OutputQueue.TryDequeue(out msg);
                GatewayContext.DeadLetterCount++;
                HandleDeadLetter(e);
            }
            else
            {
                // dequeue the message
                var firingChannel = this.InputPorts.Where(w => w.Id.Equals(e.SourceChannelId)).FirstOrDefault();
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> msg;
                firingChannel?.OutputQueue.TryDequeue(out msg);
                GatewayContext.MessageCount++;

                if (e.EventPayload.RoutingSlip.IsIgnoreRoutingSlipSteps)
                {
                    // here we want to filter messages that have 
                    // originated from deployed pipelines and 
                    // are the outputs of pipelines
                    // for disposition at the client's liesure
                    // and do not need to be switched
                    var currentOutputMessagesWithThisId = PipelineEgressPort.Where(w => w.Id != e.EventPayload.Id).ToList();
                    
                    if(currentOutputMessagesWithThisId.Count == 0)
                    {
                        PipelineEgressPort.Enqueue(e.EventPayload); 
                    }
                }
                else
                {
                    HandleSwitching(e);
                }
            }
        }

        /// <summary>
        /// todo 
        /// route discovery is a hard problem and is deferred to switch logic
        /// for switching between pipelinegateways
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void HandleSwitching(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            var destinationComponentId = e.EventPayload.RoutingSlip.RoutingSteps
                            .Where(w => w.DestinationPipeline.Item1 == routing.QueueingPipelineRoutingSlipDestination.Pipeline)
                            .Select(s => s.DestinationPipeline.Item2)
                            .FirstOrDefault();

            // note this is currently only switching for one destination
            var destinationChannel = OutputPorts.Where(w => w.HostComponentId.Equals(destinationComponentId)).FirstOrDefault();

            if(destinationChannel != null)
            {
                destinationChannel.InputQueue.Enqueue(e.EventPayload);
            }
            else
            {
                foreach (var channel in OutputPorts)
                {
                    channel.InputQueue.Enqueue(e.EventPayload);
                }
            }

        }

        private void HandleDeadLetter(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            DeadLetters.Enqueue(e.EventPayload);
        }

        public void HandleOutputPortsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // currently we don't really care about this as it's a downstream node concern
            // if these queues are hydrated
        }

        private bool IsDeadLetter(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            bool ret = false;
            //return e.EventPayload.RoutingSlip == null || e.EventPayload.RoutingSlip.IsIgnoreRoutingSlipSteps == true;

            if(e.EventPayload.RoutingSlip == null)
            {
                ret = true;
                return ret;
            }

            if(e.EventPayload.RoutingSlip.IsIgnoreRoutingSlipSteps == false)
            {
                ret = false;
                return ret;
            }

            if (e.EventPayload.RoutingSlip.IsIgnoreRoutingSlipSteps == true)
            {
                ret = false;
                return ret;
            }

            return ret;
        }

    }
}
