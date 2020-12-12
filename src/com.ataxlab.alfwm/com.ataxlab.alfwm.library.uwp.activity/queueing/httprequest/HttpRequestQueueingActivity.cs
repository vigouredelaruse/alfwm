using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    /// <summary>
    /// canonical implementation of a Queueing Pipeline Tool
    /// that performs an HTTP Request
    /// it accepts the PipelineTool configuration as an Input Queue
    /// message and outputs a List of Tuple<string,string> 
    /// on its output queue binding
    /// </summary>
    public class HttpRequestQueueingActivity : QueueingPipelineToolBase<HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>, HttpRequestQueueingActivityConfiguration>
    {
        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        private HttpClient httpClient = new HttpClient();

        ConcurrentQueue<HttpRequestQueueingActivityConfiguration> WorkItemCache { get; set; }
        public Timer WorkQueueProcessTimer { get; private set; }

        public HttpRequestQueueingActivity() : base()
        {
            WorkItemCache = new ConcurrentQueue<HttpRequestQueueingActivityConfiguration>();

            WorkQueueProcessTimer = new System.Timers.Timer();
            WorkQueueProcessTimer.AutoReset = false;
            WorkQueueProcessTimer.Interval = 50;
            WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
            WorkQueueProcessTimer.Enabled = true;

            this.InputBinding = new core.taxonomy.binding.QueueingChannel<HttpRequestQueueingActivityConfiguration>();
            this.OutputBinding = new core.taxonomy.binding.QueueingChannel<List<Tuple<string, string>>>();
            
            // enable the queue
            this.InputBinding.IsQueuePollingEnabled = true;
            this.InputBinding.QueueHasData += InputBinding_QueueHasData;
        }

        private void InputBinding_QueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<HttpRequestQueueingActivityConfiguration> e)
        {
            // cache the data 
            WorkItemCache.Enqueue(e.EventPayload);
        }

        private IAsyncAction HttpRequestAsyncAction;

        private async void WorkQueueProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(WorkItemCache.Count > 0)
            {
                HttpRequestQueueingActivityConfiguration config;
                var workItem = WorkItemCache.TryDequeue(out config);
                HttpRequestAsyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                        async (state) =>
                        {
                            OnPipelineToolStarted(this, new PipelineToolStartEventArgs());
                            var success = await EnsureHttpRequest(config);
                        }
                    );

                await HttpRequestAsyncAction;
            }
        }

        private async Task<bool> EnsureHttpRequest(HttpRequestQueueingActivityConfiguration config)
        {
            try
            {
                var request = config.RequestMessage;
                
                
                HttpResponseMessage response = await httpClient.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();
                var outMsg = new List<Tuple<String, String>>();
                var evtMsg = new List<String>();
                evtMsg.Add(content);
                var outMsgPayload = new Tuple<String, String>("content", content);
                
                outMsg.Add(outMsgPayload);

                var outTuple = new PipelineToolCompletedEventArgs<List<String>>() { Payload = evtMsg };

                OnPipelineToolCompleted<List<String>>(this,outTuple);
                this.OutputBinding.InputQueue.Enqueue(outMsg);
                this.OutputBinding.OnQueueHasData(DateTime.UtcNow, outMsg);
                return true;
            }
            catch(Exception ex)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs() { Status = { StatusJson = JsonConvert.SerializeObject(ex)} });
            }

            return false;        
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {

            this.PipelineToolStarted?.Invoke(sender, args);
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            this.PipelineToolProgressUpdated?.Invoke(sender, args);
        }
        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            this.PipelineToolFailed?.Invoke(sender, args);
        }
        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            this.PipelineToolCompleted?.Invoke(sender, new PipelineToolCompletedEventArgs() { Payload = args });
        }

        public override void OnQueueHasData(object sender, HttpRequestQueueingActivityConfiguration availableData)
        {
            // cache the data 
            WorkItemCache.Enqueue(availableData);
        }

        public override void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Configuration = configuration as HttpRequestQueueingActivityConfiguration };
        }

        public override void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Configuration = configuration };
        }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            // TODO something useful here
            return default(StopResult);
                
        }
    }
}
