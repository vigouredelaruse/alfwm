using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using Newtonsoft.Json;
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
        public string PipelineToolDescription { get; set; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public IPipelineToolConfiguration<ThreadPoolActivityConfiguration> PipelineToolConfiguration { get; set; }

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


        public virtual StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            return new StopResult();
        }


        public void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
            where StartConfiguration : class, IPipelineToolConfiguration, new()
        {

            this.PipelineToolConfiguration = new PipelineToolConfiguration<ThreadPoolActivityConfiguration>() { Configuration = configuration as ThreadPoolActivityConfiguration};
            this.OnPipelineToolStarted(this, new PipelineToolStartEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { }
            });


            try
            {
                // queue the callback method and cache the result
                ThreadPool.QueueUserWorkItem( (config) =>
                {

                    callback(config as StartConfiguration);
                    //result = res as ThreadPoolActivityStartResult;
                }, configuration);


                ThreadPoolActivityStartResult result = new ThreadPoolActivityStartResult();
                // signal the completed event
                PipelineToolCompletedEventArgs<ThreadPoolActivityStartResult> jobResult = new PipelineToolCompletedEventArgs<ThreadPoolActivityStartResult>(result);
                this.OnPipelineToolCompleted<ThreadPoolActivityStartResult>(this, jobResult);
            }
            catch(Exception ex)
            {
                this.OnPipelineToolFailed(this, new PipelineToolFailedEventArgs()
                {
                    InstanceId = this.PipelineToolInstanceId,
                    Status = { StatusJson = JsonConvert.SerializeObject(ex) } 
                });
            }


        }

        public void StartPipelineTool(ThreadPoolActivityConfiguration configuration, Action<ThreadPoolActivityConfiguration> callback)
        {
            throw new NotImplementedException();
        }
    }

    public class ThreadPoolActivityScaffold
    { 
        public void test()
        {
            var v = new ThreadPoolActivity();
            var config = new PipelineToolConfiguration<ThreadPoolActivityConfiguration>();
            v.StartPipelineTool<PipelineToolConfiguration<ThreadPoolActivityConfiguration>>(config, (co) => 
            {
                v.PipelineToolConfiguration = config;

                // return config;

            });

        }
    }

 
}
