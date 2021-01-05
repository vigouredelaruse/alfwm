using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser
{
    public class HtmlParserQueueingActivity : DefaultQueueingPipelineTool
    {
        public System.Timers.Timer WorkQueueProcessTimer { get; private set; }
        ConcurrentQueue<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>> WorkItemCache { get; set; }

        public HtmlParserQueueingActivity() : base()
        {

            WorkQueueProcessTimer = new System.Timers.Timer();
            WorkQueueProcessTimer.AutoReset = false;
            WorkQueueProcessTimer.Interval = 50;
            WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
            WorkQueueProcessTimer.Enabled = true;

            this.WorkItemCache = new ConcurrentQueue<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>>();

            this.QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData;
            this.QueueingInputBinding.IsQueuePollingEnabled = true;
        }

        #region base class overrides

        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            PipelineToolCompleted?.Invoke(sender, new PipelineToolCompletedEventArgs() { Payload = args.Payload });
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(sender, new PipelineToolFailedEventArgs() { InstanceId = this.PipelineToolInstanceId, Status = { StatusJson = JsonConvert.SerializeObject(args) } });
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(sender, new PipelineToolProgressUpdatedEventArgs() { InstanceId = this.PipelineToolInstanceId });
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(sender, new PipelineToolStartEventArgs() { InstanceId = this.PipelineToolInstanceId });
        }
        /// <summary>
        /// handle the data that arrived on the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="availableData"></param>
        public override void OnQueueHasData(object sender, QueueingPipelineQueueEntity<IPipelineToolConfiguration> availableData)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(availableData.Payload);
                HttpRequestQueueingActivityResult typedData = JsonConvert.DeserializeObject<HttpRequestQueueingActivityResult>(jsonData);
                WorkItemCache.Enqueue(new QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>()
                {
                    Payload = typedData
                });
            }
            catch(Exception e)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                { Status = { StatusJson = JsonConvert.SerializeObject(e) } });

            }
        }

        #endregion base class overrides

        #region private methods
        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            this.OnQueueHasData(sender, e.EventPayload);
        }

        /// <summary>
        /// checks for presence of data on input queue
        /// and fires appropriate data arrival events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkQueueProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
           if(WorkItemCache.Count > 0)
            {
                try
                {
                    // we expect these messages on the work item queue
                    QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult> config;
                    var workItem = WorkItemCache.TryDequeue(out config);

                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion private methods
    }
}
