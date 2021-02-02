using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser
{
    /// <summary>
    /// accepts a HttpRequeustQueueingActivityResult
    /// emits a HtmlParserQueueigActivityResult
    ///   with a payload of HtmlDocument
    ///   depeds on https://www.nuget.org/packages/HtmlAgilityPack/
    /// </summary>
    public class HtmlParserQueueingActivity : DefaultQueueingPipelineTool
    {
        public System.Timers.Timer WorkQueueProcessTimer { get; private set; }
        ConcurrentQueue<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>> WorkItemCache { get; set; }

        public HtmlParserQueueingActivity() : base()
        {

            PipelineToolDisplayName = "Html Parser";
            this.PipelineToolDescription = "Accepts Q Messages with a payload of List<tuple<string,string>> containing <html/> , produces HTML Agility Pack HtmlDocument";
            PipelineToolInstanceId = Guid.NewGuid().ToString();
            this.PipelineToolVariables = new ObservableCollection<core.taxonomy.binding.IPipelineVariable>();

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
            PipelineToolCompleted?.Invoke(sender, new PipelineToolCompletedEventArgs() { InstanceId = this.PipelineToolInstanceId, Payload = args.Payload });
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(sender,args);
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(sender,args);
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(sender, args);
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
            catch (Exception e)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                {
                    Status = new HtmlParserQueueingActivityStatus()
                    {
                        StatusJson = JsonConvert.SerializeObject(e)
                    }
                }); 

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

                    OnPipelineToolStarted(this, new PipelineToolStartEventArgs()
                    {
                        InstanceId = this.PipelineToolInstanceId
                    });

                    // we expect these messages on the work item queue
                    QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult> workitem;
                    var dQResult = WorkItemCache.TryDequeue(out workitem);
                    
                    // expect a payload of tuple<string,string>
                    if(dQResult)
                    {
                        var content = workitem.Payload.Payload[0];
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(content.Item2);

                        PipelineToolCompleted?.Invoke(this, new PipelineToolCompletedEventArgs()
                        {
                            InstanceId = this.PipelineToolInstanceId,

                        });

                        EnsureEgressMessage(doc);

                        //var xpath = "//text()"; // "//text()";
                        //var textNodes = doc.DocumentNode.SelectNodes(xpath);

                        //var text = textNodes
                        //            .Where(w => w.InnerText.Trim().Length > 2)                                  
                        //            .Select(w => w.InnerText)  
                        //            .ToList();


                    }
                }
                catch (Exception ex)
                {

                }
            }

            WorkQueueProcessTimer.Enabled = true;
        }

        private void EnsureEgressMessage(HtmlDocument doc)
        {
            var egressMsg = new HtmlParserQueueingActivityResult()
            {
                Payload = doc,
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Html Parser Activity Result",
                TimeStamp = DateTime.UtcNow

            };

            var egressEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(egressMsg);

            foreach (var binding in this.QueueingOutputBindingCollection)
            {
                binding.InputQueue.Enqueue(new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                {
                    Payload = egressMsg,
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = "Html Parser Activity Result", 
                    TimeStamp = DateTime.UtcNow,
                    RoutingSlip = new core.taxonomy.binding.queue.routing.QueueingPipelineQueueEntityRoutingSlip()
                   
                }) ;
            }

            this.QueueingOutputBinding.OutputQueue.Enqueue(egressEntity);
        }

        #endregion private methods
    }
}
