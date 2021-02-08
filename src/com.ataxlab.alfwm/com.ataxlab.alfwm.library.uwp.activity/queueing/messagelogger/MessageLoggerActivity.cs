using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.messagelogger
{
    /// <summary>
    /// a message tap pass-through activity that
    /// maintains a log of all the messages
    /// it sees
    /// 
    /// all its inputs appear on all its outputs
    /// and the messages are maintained in a public collection
    /// </summary>
    public class MessageLoggerActivity : DefaultQueueingPipelineTool
    {
        public MessageLoggerActivity() :base()
        {
            PipelineToolDisplayName = "Message Logger Activity";
            this.PipelineToolDescription = "accepts all kinds of messages and reflects them all, logging them";
            PipelineToolInstanceId = Guid.NewGuid().ToString();
            this.PipelineToolVariables = new ObservableCollection<IPipelineVariable>();

            SeenMessages = new ObservableCollection<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            WorkQueueProcessTimer = new System.Timers.Timer();
            WorkQueueProcessTimer.AutoReset = false;
            WorkQueueProcessTimer.Interval = 50;
            WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
            WorkQueueProcessTimer.Enabled = true;

            this.WorkItemCache = new ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>(); // new ConcurrentQueue<QueueingPipelineQueueEntityHttpRequestQueueingActivityConfiguration>>();
                                                                                                                               // enable the queue
            this.QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData;

            this.QueueingInputBinding.IsQueuePollingEnabled = true;
        }

        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {

            // log the seen message
            SeenMessages.Add(e.EventPayload);
            // update the egressing message vital stats
            e.EventPayload.TimeStamp = DateTime.UtcNow;
            e.EventPayload.RoutingSlip = new QueueingPipelineQueueEntityRoutingSlip() { IsIgnoreRoutingSlipSteps = true };
            e.EventPayload.CurrentPipelineId = this.CurrentPipelineId;

            foreach (var binding in this.QueueingOutputBindingCollection)
            {
                binding.InputQueue.Enqueue(e.EventPayload);
            }

            var egressEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(e.EventPayload);
            egressEntity.RoutingSlip = new QueueingPipelineQueueEntityRoutingSlip() { IsIgnoreRoutingSlipSteps = true };
            egressEntity.CurrentPipelineId = this.CurrentPipelineId;
            this.QueueingOutputBinding.OutputQueue.Enqueue(egressEntity);
        }

        private void WorkQueueProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            WorkQueueProcessTimer.Enabled = true;
        }

        public ObservableCollection<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> SeenMessages { get; set; }

        public System.Timers.Timer WorkQueueProcessTimer { get; private set; }
        public ConcurrentQueue<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> WorkItemCache { get; private set; }
    }
}
