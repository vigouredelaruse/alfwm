﻿using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.scheduler.uwp;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Foundation;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool
{
    public partial class WindowsThreadpoolScheduler : WindowsThreadpoolSchedulerBase
    {
        /// <summary>
        /// TODO - the scheduler is currently entirely stateless
        /// * this assumes the implementer will save state
        /// * a sample implementation is provided in the 
        /// * WorkflowManager ALFM object
        /// </summary>
        public WindowsThreadpoolScheduler()
        {

        }

        private IAsyncAction asyncAction;

        public override Task<T> SchedulePipeline<T>(T pipeline)
        {
            throw new NotImplementedException();
        }

        public override Task<TStatus> ShutdownActivity<TStatus>(string instanceId)
        {
            throw new NotImplementedException();
        }

        public override Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId)
        {
            throw new NotImplementedException();
        }


        public override void StartActivity<TActivity, TConfiguration>(TActivity activity, TConfiguration activityConfiguration, Action<TConfiguration> callback)
        {
            try
            {
                var startEventArgs = new PipelineToolStartEventArgs();
                

                activity.PipelineToolConfiguration = new PipelineToolConfiguration<TConfiguration>() { Payload = activityConfiguration };
                activity.PipelineToolInstanceId = Guid.NewGuid().ToString();
                activity.PipelineToolCompleted += this.OnActivityCompleted;
                activity.PipelineToolStarted += this.OnActivityStarted;

                
                startEventArgs.InstanceId = activity.PipelineToolInstanceId;

                OnActivityStarted(this, startEventArgs);

                asyncAction = Windows.System.Threading.ThreadPool.RunAsync(

                       (workItem) =>
                       {
                       try
                       {
                           activity.StartPipelineTool(activityConfiguration, (config) => 
                               {
                                   callback(config);
                                   //return new ThreadPoolActivityStartResult();

                                   
                                   var result = new  WindowsThreadPoolSchedulerStartResult();
                                   // return result;
                               });
                               //activity.Start<TConfiguration>(activityConfiguration, c =>
                               //{

                               //    callback(s);
                               //});


                           }
                           catch (Exception ex)
                           {
                               throw new WindowsThreadpoolSchedulerException(ex.Message);
                           }

                       });
            }
            catch(Exception ex)
            {
                throw new WindowsThreadpoolSchedulerException(ex.Message);
            }
        }

        public override Task<TStatus> StartPipeline<TStatus, TPipeline>(TPipeline pipeline)
        {
            throw new NotImplementedException();
        }

        public override Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration)
        {
            throw new NotImplementedException();
        }

        public override Task<T> StopPipeline<T>(string instanceId)
        {
            throw new NotImplementedException();
        }

    
    }
}
