using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Timers;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// specify a gateway that interfaces pipelinetools
    /// </summary>
    public interface IDefaultQueueingChannelPipelineToolGateway : 
        IQueueingChannelPipelineToolGateway<QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
            QueueingPipelineQueueEntity<IPipelineToolConfiguration>>
    {
        DefaultQueueingChannelPipelineToolGatewayContext GatewayContext { get; set; }
        ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> DeadLetters { get; set; }
    }

    public interface IQueueingChannelPipelineToolGateway<TInputEntity, TOutputEntity>
    {
        string Id { get; set; }
        ObservableCollection<PipelineToolQueueingConsumerChannel<TOutputEntity>> OutputPorts { get; set; }

        ObservableCollection<PipelineToolQueueingProducerChannel<TOutputEntity>> InputPorts { get; set; }

        void HandleInputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);

        void HandleOutputPortsCollectionChanged(NotifyCollectionChangedEventArgs e);
    }


    public enum DefaultQueueingChannelGatewaySwitchConfigurationFault { CurrentPipelineNotSet, InputQueueEventArrivalHandlerException }
    public class DefaultQueueingChannelGatewaySwitchConfigurationFaultArgs : EventArgs
    {
        public DefaultQueueingChannelGatewaySwitchConfigurationFaultArgs()
        {

        }

        public DefaultQueueingChannelGatewaySwitchConfigurationFault Fault { get; set; }
        public Exception FaultException { get; set; }
    }

    /// <summary>
    /// the gateway needs information about its environment
    /// </summary>
    public class DefaultQueueingChannelPipelineToolGatewayContext
    {
        public DefaultQueueingChannelPipelineToolGatewayContext()
        {

        }

        public long MessageCount { get; set; }
        public string CurrentPipelineId { get; set; }
        public int DeadLetterCount { get; internal set; }
    }

    /// <summary>
    /// application of messaging gateway pattern
    /// 
    /// applies seperation of routing and message delivery concerns
    /// </summary>
    public class DefaultQueueingChannelPipelineToolGateway : IDefaultQueueingChannelPipelineToolGateway
    {
        public DefaultQueueingChannelPipelineToolGateway()
        {

            Id = Guid.NewGuid().ToString();
            GatewayContext = new DefaultQueueingChannelPipelineToolGatewayContext();
            DeadLetters = new ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();

            OutputPorts = new ObservableCollection<PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>();
            InputPorts = new ObservableCollection<PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>>();

            OutputPorts.CollectionChanged += OutputPorts_CollectionChanged;
            InputPorts.CollectionChanged += InputPorts_CollectionChanged;

            
        }

        /// <summary>
        /// the preferred constructor
        /// </summary>
        /// <param name="ctx"></param>
        public DefaultQueueingChannelPipelineToolGateway(DefaultQueueingChannelPipelineToolGatewayContext ctx) : this()
        {
            GatewayContext = ctx;
        }

        /// <summary>
        /// upstream data has arrived
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputQueue_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            try
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
                    HandleSwitching(e);
                    int i = 0;
                }
            }
            catch(Exception ex)
            {
                this.SwitchConfigurationFaultEvent?.Invoke(this, new DefaultQueueingChannelGatewaySwitchConfigurationFaultArgs()
                {
                    Fault = DefaultQueueingChannelGatewaySwitchConfigurationFault.InputQueueEventArrivalHandlerException,
                    FaultException = ex
                });
            }
        }

        private void HandleSwitching(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            foreach (var channel in OutputPorts)
            {
                channel.InputQueue.Enqueue(e.EventPayload);
            }
        }

        private void HandleDeadLetter(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            DeadLetters.Enqueue(e.EventPayload);
        }

        private static bool IsDeadLetter(QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            return e.EventPayload.RoutingSlip == null;
        }

        private void InputPorts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleInputPortsCollectionChanged(e);
        }

        private void OutputPorts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HandleOutputPortsCollectionChanged(e);
        }

        public ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> DeadLetters { get; set; }
        public string Id { get; set; }

        public ObservableCollection<PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> OutputPorts { get; set; }

        public ObservableCollection<PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> InputPorts { get; set; }
        public DefaultQueueingChannelPipelineToolGatewayContext GatewayContext { get; set; }

        public event EventHandler<DefaultQueueingChannelGatewaySwitchConfigurationFaultArgs> SwitchConfigurationFaultEvent;

        public void HandleInputPortsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        // listen to queue arrival events
                        foreach (var item in e.NewItems)
                        {
                            ((PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>)item).QueueHasData += InputQueue_QueueHasData;
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


        // maybe listen to queue arrival events
        // but since we likely put that data there we don't really care
        // and it's downstream which is the downstream node's concern
        // we certainly don't want to dequeue anytyhing
        ///// <summary>
        ///// observe downstream queue
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ConsumerQueueingChannelGateway_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        //{

        //}

        public void HandleOutputPortsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {

                        // maybe listen to queue arrival events
                        // but since we likely put that data there we don't really care
                        // and it's downstream which is the downstream node's concern
                        // we certainly don't want to dequeue anytyhing
                        //foreach (var item in e.NewItems)
                        //{
                        //    ((PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>)item).QueueHasData += ConsumerQueueingChannelGateway_QueueHasData;
                        //}
                        break;
                    }

                case NotifyCollectionChangedAction.Remove:
                    {
                        break;
                    }

                case NotifyCollectionChangedAction.Replace:
                    {
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

    }
}
