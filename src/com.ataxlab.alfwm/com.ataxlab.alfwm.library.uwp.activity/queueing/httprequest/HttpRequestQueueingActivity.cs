using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    public class HttpRequestQueueingActivity2 : IQueueingPipelineTool<QueueingConsumerChannel<PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>>,
                                                                      QueueingProducerChannel<PipelineToolConfiguration<HttpRequestQueueingActivityResult>>,
                                                                                              PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>,
                                                                                              PipelineToolConfiguration<HttpRequestQueueingActivityResult>,
                                                                                              PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>>
    {
        public QueueingConsumerChannel<PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>> InputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public QueueingProducerChannel<PipelineToolConfiguration<HttpRequestQueueingActivityResult>> OutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPipelineToolConfiguration<PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>> PipelineToolConfiguration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PipelineToolInstanceId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PipelineToolId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PipelineToolDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PipelineToolDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPipelineToolStatus PipelineToolStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPipelineToolContext PipelineToolContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPipelineToolBinding PipelineToolOutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Func<PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>, PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>> QueueHasAvailableDataEvent;
        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new()
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnQueueHasData(object sender, PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration> availableData)
        {
            throw new NotImplementedException();
        }

        public void StartPipelineTool(PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration> configuration, Action<PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>> callback)
        {
            throw new NotImplementedException();
        }

        public StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }
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

            this.InputBinding = new core.taxonomy.binding.QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>();
            this.OutputBinding = new core.taxonomy.binding.QueueingProducerChannel<List<Tuple<string, string>>>();
            
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
                this.OutputBinding.OutputQueue.Enqueue(outMsg);
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



        public override void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Payload = configuration };
        }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            // TODO something useful here
            return default(StopResult);
                
        }
    }
}
