using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.activity.httpactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{

    public class HttpRequestQueueingActivity : DefaultQueueingPipelineTool
    {
        public HttpRequestQueueingActivity()
        {
            this.QueueingInputBinding = new QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>();
            this.QueueingOutputBinding = new QueueingConsumerChannel<HttpRequestQueueingActivityResult>();

        }

        public new QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration> QueueingInputBinding { get; set; }
        public new QueueingConsumerChannel<HttpRequestQueueingActivityResult> QueueingOutputBinding { get; set; }
        public IPipelineToolConfiguration<IPipelineToolConfiguration> PipelineToolConfiguration { get; set; }
        public string PipelineToolInstanceId { get; set; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public string PipelineToolId { get; set; }
        public string PipelineToolDisplayName { get; set; }
        public string PipelineToolDescription { get; set; }
        public IPipelineToolStatus PipelineToolStatus { get; set; }
        public IPipelineToolContext PipelineToolContext { get; set; }
        public IPipelineToolBinding PipelineToolOutputBinding { get; set; }

        public event Func<object, object> QueueHasAvailableDataEvent;
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

        public void OnQueueHasData(object sender, object availableData)
        {
            throw new NotImplementedException();
        }

        public void StartPipelineTool(IPipelineToolConfiguration configuration, Action<IPipelineToolConfiguration> callback)
        {
            throw new NotImplementedException();
        }

        public StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }


    public class HttpRequestQueueingActivityEx : QueueingPipelineToolBase<
                                                                            QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>,
                                                                            QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>,
                                                                            HttpRequestQueueingActivityConfiguration>
    {

  
        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            throw new NotImplementedException();
        }

        public override void OnQueueHasData(object sender, QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration> availableData)
        {
            throw new NotImplementedException();
        }

        public override void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
        {
            throw new NotImplementedException();
        }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            throw new NotImplementedException();
        }
    }

    public class HttpRequestQueueingActivity2 : IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>,
                                                                      QueueingProducerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>>,
                                                                                              HttpRequestQueueingActivityConfiguration,
                                                                                              HttpRequestQueueingActivityResult,
                                                                                             HttpRequestQueueingActivityConfiguration>
    {
        public HttpRequestQueueingActivity2()
        {
            this.PipelineToolOutputBinding = new QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>();
            this.QueueingInputBinding = new QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>();
            this.PipelineToolVariables = new ObservableCollection<IPipelineVariable>();
        }

        public QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>> QueueingInputBinding { get; set; }
        public QueueingProducerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>> QueueingOutputBinding { get; set; }
        public IPipelineToolConfiguration<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>> PipelineToolConfiguration { get; set; }
        public string PipelineToolInstanceId { get; set; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public string PipelineToolId { get; set; }
        public string PipelineToolDisplayName { get; set; }
        public string PipelineToolDescription { get; set; }
        public IPipelineToolStatus PipelineToolStatus { get; set; }
        public IPipelineToolContext PipelineToolContext { get; set; }
        public IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        IPipelineToolConfiguration<HttpRequestQueueingActivityConfiguration> IPipelineTool<HttpRequestQueueingActivityConfiguration>.PipelineToolConfiguration { get; set; }

        public event Func<HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration> QueueHasAvailableDataEvent;
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

        public void OnQueueHasData(object sender, HttpRequestQueueingActivityConfiguration availableData)
        {
            throw new NotImplementedException();
        }

        public void StartPipelineTool(QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration> configuration, Action<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>> callback)
        {
            throw new NotImplementedException();
        }

        public void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
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
    //public class HttpRequestQueueingActivity : QueueingPipelineToolBase<HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>, HttpRequestQueueingActivityConfiguration>
    //{
    //    public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
    //    public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
    //    public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
    //    public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

    //    private HttpClient httpClient = new HttpClient();

    //    ConcurrentQueue<HttpRequestQueueingActivityConfiguration> WorkItemCache { get; set; }
    //    public Timer WorkQueueProcessTimer { get; private set; }

    //    public HttpRequestQueueingActivity() : base()
    //    {
    //        WorkItemCache = new ConcurrentQueue<HttpRequestQueueingActivityConfiguration>();

    //        WorkQueueProcessTimer = new System.Timers.Timer();
    //        WorkQueueProcessTimer.AutoReset = false;
    //        WorkQueueProcessTimer.Interval = 50;
    //        WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
    //        WorkQueueProcessTimer.Enabled = true;

    //        this.QueueingInputBinding = new QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>();
    //        this.QueueingOutputBinding = new QueueingProducerChannel<QueueingPipelineQueueEntity<List<Tuple<string, string>>>>();

    //        // enable the queue
    //        this.QueueingInputBinding.IsQueuePollingEnabled = true;
    //        this.QueueingOutputBinding.QueueHasData += QueueingOutputBinding_QueueHasData; // InputBinding_QueueHasData;
    //    }

    //    private void QueueingOutputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<List<Tuple<string, string>>>> e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private void InputBinding_QueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<HttpRequestQueueingActivityConfiguration> e)
    //    {
    //        // cache the data 
    //        WorkItemCache.Enqueue(e.EventPayload);
    //    }

    //    private IAsyncAction HttpRequestAsyncAction;

    //    private async void WorkQueueProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
    //    {
    //        if(WorkItemCache.Count > 0)
    //        {
    //            HttpRequestQueueingActivityConfiguration config;
    //            var workItem = WorkItemCache.TryDequeue(out config);
    //            HttpRequestAsyncAction = Windows.System.Threading.ThreadPool.RunAsync(
    //                    async (state) =>
    //                    {
    //                        OnPipelineToolStarted(this, new PipelineToolStartEventArgs());
    //                        var success = await EnsureHttpRequest(config);
    //                    }
    //                );

    //            await HttpRequestAsyncAction;
    //        }
    //    }

    //    private async Task<bool> EnsureHttpRequest(HttpRequestQueueingActivityConfiguration config)
    //    {
    //        try
    //        {
    //            var request = config.RequestMessage;


    //            HttpResponseMessage response = await httpClient.SendAsync(request);

    //            var content = await response.Content.ReadAsStringAsync();
    //            var outMsg = new List<Tuple<String, String>>();
    //            var evtMsg = new List<String>();
    //            evtMsg.Add(content);
    //            var outMsgPayload = new Tuple<String, String>("content", content);

    //            outMsg.Add(outMsgPayload);

    //            var outTuple = new PipelineToolCompletedEventArgs<List<String>>() { Payload = evtMsg };

    //            OnPipelineToolCompleted<List<String>>(this,outTuple);
    //            //this.QueueingOutputBinding.OutputQueue.Enqueue(outMsg);
    //            //this.QueueingOutputBinding.OnQueueHasData(DateTime.UtcNow, outMsg);
    //            return true;
    //        }
    //        catch(Exception ex)
    //        {
    //            OnPipelineToolFailed(this, new PipelineToolFailedEventArgs() { Status = { StatusJson = JsonConvert.SerializeObject(ex)} });
    //        }

    //        return false;        
    //    }

    //    public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
    //    {

    //        this.PipelineToolStarted?.Invoke(sender, args);
    //    }

    //    public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
    //    {
    //        this.PipelineToolProgressUpdated?.Invoke(sender, args);
    //    }
    //    public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
    //    {
    //        this.PipelineToolFailed?.Invoke(sender, args);
    //    }
    //    public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
    //    {
    //        this.PipelineToolCompleted?.Invoke(sender, new PipelineToolCompletedEventArgs() { Payload = args });
    //    }

    //    public override void OnQueueHasData(object sender, HttpRequestQueueingActivityConfiguration availableData)
    //    {
    //        // cache the data 
    //        WorkItemCache.Enqueue(availableData);
    //    }



    //    public override void StartPipelineTool(HttpRequestQueueingActivityConfiguration configuration, Action<HttpRequestQueueingActivityConfiguration> callback)
    //    {
    //        this.PipelineToolConfiguration = new PipelineToolConfiguration<HttpRequestQueueingActivityConfiguration>() { Payload = configuration };
    //    }

    //    public override StopResult StopPipelineTool<StopResult>(string instanceId)
    //    {
    //        // TODO something useful here
    //        return default(StopResult);

    //    }

    //}
}
