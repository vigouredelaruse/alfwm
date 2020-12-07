using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    /// <summary>
    /// .net 2.0 standard compatible threadpool scheduling activity
    /// you supply a delegate that is called by this activity on the threadpool - act accordingly
    /// 
    /// </summary>
    public class ThreadPoolActivity : IPipelineTool<ThreadPoolActivityConfiguration>
    {
        public ThreadPoolActivity()
        {
            this.PipelineToolInstanceId = Guid.NewGuid().ToString();
            this.PipelineToolVariables = new ObservableCollection<IPipelineVariable>();
        }

        public string PipelineToolInstanceId { get; set; }
        public IPipelineToolStatus PipelineToolStatus { get; set; }
        public IPipelineToolContext PipelineToolContext { get; set; }
  
        public IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public string PipelineToolId { get; set; }
        public string PipelineToolDisplayName { get; set; }
        public string PipelineToolDescription { get; set ; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public ThreadPoolActivityConfiguration PipelineToolConfiguration { get; set; }

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

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new()
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
            PipelineToolFailed?.Invoke(this, new PipelineToolFailedEventArgs()
            {
                 InstanceId = this.PipelineToolInstanceId,
                 Status = args.Status
            });
        }

        public void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, new PipelineToolProgressUpdatedEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = args.Status,
                OutputVariables = args.OutputVariables
 
            });
        }

        public void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(this, new PipelineToolStartEventArgs()
            {
                 InstanceId = this.PipelineToolInstanceId, 
                 Status = args.Status
            });
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
        public virtual void StartPipelineTool<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
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

        public virtual void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class, new()
        {
            this.OnPipelineToolStarted(this, new PipelineToolStartEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { }
            });
        }

        public virtual StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            return new StopResult();   
        }
    }
}
