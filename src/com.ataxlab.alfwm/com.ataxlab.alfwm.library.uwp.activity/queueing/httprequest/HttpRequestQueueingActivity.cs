﻿using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.activity.httpactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;

namespace com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest
{
    /// <summary>
    /// accepts a HttpRequeustQueueingActivityConfiguration
    ///  that configures a HttpRequest
    /// emits a HttpRequeustQueueingActivityResult
    ///   with a payload of List<Tuple<String,String>>
    ///   
    /// dispatches the workitem on a threadpool thread
    /// </summary>
    public class HttpRequestQueueingActivity : DefaultQueueingPipelineTool
    {


        private HttpClient httpClient;

        public HttpRequestQueueingActivity() : base()
        {

            PipelineToolDisplayName = "Http Request Activity";
            this.PipelineToolDescription = "Accepts an entity with a payload of HttpMessage, sends the request and produces an entity with a payload of List<tuple<string,string>>";
            PipelineToolInstanceId = Guid.NewGuid().ToString();
            this.PipelineToolVariables = new ObservableCollection<IPipelineVariable>();

            WorkQueueProcessTimer = new System.Timers.Timer();
            WorkQueueProcessTimer.AutoReset = false;
            WorkQueueProcessTimer.Interval = 50;
            WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
            WorkQueueProcessTimer.Enabled = true;

            this.WorkItemCache = new ConcurrentQueue<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>(); // new ConcurrentQueue<QueueingPipelineQueueEntityHttpRequestQueueingActivityConfiguration>>();
                                                                                                                               // enable the queue
            this.QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData;

            this.QueueingInputBinding.IsQueuePollingEnabled = true;
        }

        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            this.OnQueueHasData(sender, e.EventPayload);
        }

        private IAsyncAction HttpRequestAsyncAction;


        private async Task<HttpRequestQueueingActivityResult> EnsureHttpRequest(HttpRequestQueueingActivityConfiguration config)
        {


            if(httpClient == null)
            {
                httpClient = new HttpClient();
            }

            HttpRequestQueueingActivityResult activityResult = new HttpRequestQueueingActivityResult();
            
            try
            {
                httpClient = new HttpClient();
                var request = config.RequestMessage;
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                var _cancelTokenSource = new CancellationTokenSource();
                var _cancelToken = _cancelTokenSource.Token;

                var response =  await httpClient.SendAsync(request, _cancelToken).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync();

                var outMsg = new List<Tuple<String, String>>();
                outMsg.Add(new Tuple<string, string>("content", content));

                activityResult.HttpResponseHeaderCollection = response.Headers.ToList < KeyValuePair<String, IEnumerable<String>>>();
               
                // activityResult.HttpResponseHeaders = response.Headers
                activityResult.ResponseStatusCode = response.StatusCode;
                activityResult.ReasonPhrase = response.ReasonPhrase;
                activityResult.Payload.Add(new Tuple<string, string>("content", content));
                activityResult.SourceUrl = request.RequestUri.ToString();
                activityResult.HttpMethod = request.Method.ToString();
                activityResult.CommandMessage = config; // encapsulate the original message

                var evtMsg = new List<String>();
                evtMsg.Add(content);
                
                // TODO fix this oddity
                var completionArgs = new PipelineToolCompletedEventArgs<HttpRequestQueueingActivityResult>(activityResult);
           
                OnPipelineToolCompleted<HttpRequestQueueingActivityResult>(this, new PipelineToolCompletedEventArgs<HttpRequestQueueingActivityResult>(activityResult));

                return activityResult;
            }
            catch (Exception ex)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs() { Status = new HttpRequestQueueingActivityStatus { StatusJson = JsonConvert.SerializeObject(ex.Message) } });
            }

            return activityResult;
        }


        private async void WorkQueueProcessTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // the result message of this pipeline tool
            HttpRequestQueueingActivityResult activityResult = new HttpRequestQueueingActivityResult();

            try 
            {
                if (WorkItemCache.Count > 0)
                {
                    // we expect these messages on the work item queue
                    QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration> config;
                    var workItem = WorkItemCache.TryDequeue(out config);

                    // operate on the queue data via the threadpool
                    HttpRequestAsyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                            async (state) =>
                            {
                                try
                                {
                                    OnPipelineToolStarted(this, new PipelineToolStartEventArgs()
                                    {
                                        InstanceId = this.PipelineToolInstanceId
                                    });

                                    // perform the business logic using 
                                    // dequeued configuration 
                                    var result = await EnsureHttpRequest(config.Payload);
                                    
                                    activityResult = EnsureDecoratedEgressMessage(config, result);

                                    var egressEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                                    {
                                        Payload = activityResult,
                                        RoutingSlip = new QueueingPipelineQueueEntityRoutingSlip()
                                        {
                                            IsIgnoreRoutingSlipSteps = true
                                        },
                                        CurrentPipelineId = this.CurrentPipelineId
                                    };

                                    // copy the pipeline variables
                                    // from the ingress entity 
                                    // to the egress entity
                                    // supporting pipeline variable scope
                                    // egressEntity.PopulatePipelineVariables(config);
                                    this.EnsureMessageEgressed(config, egressEntity);

                                    // EnsureMessageEgressed(activityResult);

                                    PipelineToolCompleted?.Invoke(this, new PipelineToolCompletedEventArgs()
                                    {
                                      InstanceId = this.PipelineToolInstanceId,
                                      
                                    });
                                }
                                catch (Exception ex)
                                {
                                    OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                                    {
                                        InstanceId = this.PipelineToolInstanceId,
                                        Status = new HttpRequestQueueingActivityStatus { StatusJson = JsonConvert.SerializeObject(ex) }
                                    });
                                }

                            }
                        );

                    // execute the threadpool quque operation
                    await HttpRequestAsyncAction;

                }

            }
            catch(Exception eex)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                {
                    InstanceId = this.PipelineToolInstanceId,
                    Status = new HttpRequestQueueingActivityStatus { StatusJson = JsonConvert.SerializeObject(eex) }
                    
                });
            }

            WorkQueueProcessTimer.Enabled = true;
        }

        /// <summary>
        /// this code moved to QueueingPipelineTool, base class of this
        /// </summary>
        /// <param name="config"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private HttpRequestQueueingActivityResult EnsureDecoratedEgressMessage(QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration> config, HttpRequestQueueingActivityResult result)
        {
            HttpRequestQueueingActivityResult activityResult = result;
           
            // attach the message that generated the result to the result
            // of the activity
            activityResult.ConfigurationJson = JsonConvert.SerializeObject(config);

            // nullcheck defence
            if (config?.Payload?.RequestHeaders != null)
            {
                foreach (var item in config?.Payload?.RequestHeaders)
                {
                    var key = item.Key;
                    var value = item.Value.ToList<string>();

                    activityResult.RequestHeaders.Add(
                        Tuple.Create<string, List<string>>(key, value));

                }
            }


            foreach (var item in result?.HttpResponseHeaderCollection)
            {
                var key = item.Key;
                var value = item.Value.ToList<string>();

                activityResult.ResponseHeaders.Add(

                    Tuple.Create<string, List<string>>(key, value)
                    );
            }

            activityResult.HttpResponseHeaders = result.HttpResponseHeaders;
            
            //activityResult.SourceUrl = result.SourceUrl.ToString();
            //activityResult.HttpMethod = result.HttpMethod;
            //activityResult.TimeStamp = DateTime.UtcNow;
            return activityResult;
        }

        /// <summary>
        /// reflect the operation result of this tool
        /// on its output binding, for egress by downstream nodes
        /// in the pipeline
        /// </summary>
        /// <param name="activityResult"></param>
        [Obsolete]
        private void EnsureMessageEgressed(HttpRequestQueueingActivityResult activityResult)
        {
            // signal downstream
            foreach (var binding in this.QueueingOutputBindingCollection)
            {
                binding.InputQueue.Enqueue(new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                {
                    Payload = activityResult,
                    RoutingSlip = new QueueingPipelineQueueEntityRoutingSlip()
                    {
                        IsIgnoreRoutingSlipSteps = true
                    },
                    CurrentPipelineId = this.CurrentPipelineId
                });
            }

            this.QueueingOutputBinding.OutputQueue
                .Enqueue(new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                {
                    Payload = activityResult,
                    RoutingSlip = new QueueingPipelineQueueEntityRoutingSlip()
                    {
                        IsIgnoreRoutingSlipSteps = true
                    },
                    CurrentPipelineId = this.CurrentPipelineId
                });
        }

        ConcurrentQueue<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>> WorkItemCache { get; set; }
        public System.Timers.Timer WorkQueueProcessTimer { get; private set; }

        // public new QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>> QueueingInputBinding { get; set; }

        public override IPipelineToolConfiguration<IPipelineToolConfiguration> PipelineToolConfiguration { get; set; }
        public override string PipelineToolInstanceId { get; set; }
        public override ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public override string PipelineToolId { get; set; }
        public override string PipelineToolDisplayName { get; set; }
        public override string PipelineToolDescription { get; set; }
        public override IPipelineToolStatus PipelineToolStatus { get; set; }
        public override IPipelineToolContext PipelineToolContext { get; set; }
        public override IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            PipelineToolCompleted?.Invoke(sender, new PipelineToolCompletedEventArgs() { Payload = args.Payload } );
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(sender, args);
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(sender, args);
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(sender, args);
        }

        public override void OnQueueHasData(object sender, QueueingPipelineQueueEntity<IPipelineToolConfiguration> availableData)
        {
            try
            {
                //var jsonData = JsonConvert.SerializeObject(availableData.Payload);
                //HttpRequestQueueingActivityConfiguration typedData = JsonConvert.DeserializeObject<HttpRequestQueueingActivityConfiguration>(jsonData);
                //WorkItemCache.Enqueue(new QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>()
                //{
                //    Payload = typedData
                //});

                var jsondata = JsonConvert.SerializeObject(availableData);
                QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration> typedData =
                    JsonConvert.DeserializeObject<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>(jsondata);

                WorkItemCache.Enqueue(typedData);

                OnPipelineToolProgressUpdated(this, new PipelineToolProgressUpdatedEventArgs()
                {
                    InstanceId = Guid.NewGuid().ToString(),
                    TimeStamp = DateTime.UtcNow
                }); 

            }
            catch(Exception e)
            {
                OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                { Status = new HttpRequestQueueingActivityStatus { StatusJson = JsonConvert.SerializeObject(e) } });
            }
        }

        //public override void OnQueueHasData(object sender, object availableData)
        //{
        //    // guard against data arrival we don't handle

        //    if (availableData is HttpRequestQueueingActivityConfiguration || availableData is QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>)
        //    {
        //        // cache the data 
        //        WorkItemCache.Enqueue(availableData as HttpRequestQueueingActivityConfiguration);
        //    }
        //}

        public override void StartPipelineTool(IPipelineToolConfiguration configuration, Action<IPipelineToolConfiguration> callback)
        {
            // TODO - enqueue the supplied data
        }


    }


}
