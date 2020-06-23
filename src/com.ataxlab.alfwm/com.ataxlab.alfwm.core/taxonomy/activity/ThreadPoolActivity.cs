using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    /// <summary>
    /// .net 2.0 standard compatible threadpool scheduling activity
    /// you supply a delegate that is called by this activity on the threadpool - act accordingly
    /// 
    /// </summary>
    public class ThreadPoolActivity : IPipelineTool
    {
        public ThreadPoolActivity()
        {
            this.InstanceId = Guid.NewGuid().ToString();
        }

        public string InstanceId { get; set; }
        public IPipelineToolStatus Status { get; set; }
        public IPipelineToolContext Context { get; set; }
        public IPipelineToolConfiguration Configuration { get; set; }
        public IPipelineToolBinding OutputBinding { get; set; }

        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = PipelineToolCompleted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = PipelineToolCompleted;
            PipelineToolCompletedEventArgs resultArgs = new PipelineToolCompletedEventArgs();
            resultArgs.Payload = args.Payload;

            if (handler != null)
            {
                handler(sender, resultArgs);
            }
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

        /// <summary>
        /// schedule the callback on the threadpool
        /// and signal the PipelineToolCompleted listeners
        /// with the payload
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        public void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new()
        {
            StartResult result = new StartResult();

            // queue the callback method and cache the result
            ThreadPool.QueueUserWorkItem((cofig) =>
            {
               
                result = callback(configuration);
            });

            // signal the completed event
            PipelineToolCompletedEventArgs<StartResult> jobResult = new PipelineToolCompletedEventArgs<StartResult>(result);
            this.OnPipelineToolCompleted<StartResult>(this, jobResult);

        }

        public void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class
        {
            throw new NotImplementedException();
        }

        public StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }
}
