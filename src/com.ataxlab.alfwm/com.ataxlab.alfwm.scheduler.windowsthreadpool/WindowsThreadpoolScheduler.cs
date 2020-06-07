using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool
{
    public partial class WindowsThreadpoolScheduler : SchedulerBase
    {
        public WindowsThreadpoolScheduler() : base()
        { }


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

        public async override Task<IPipelineToolStatus> StartActivity<TActivity, IPipelineToolStatus, TConfiguration>(TConfiguration activityConfiguration)
        {
            TActivity activity = new TActivity();
            activity.Configuration = activityConfiguration;
            activity.InstanceId = Guid.NewGuid().ToString();
            return  await activity.Start<IPipelineToolStatus, TConfiguration>(activityConfiguration);
        }

        public override Task<TPipelineConfiguration> StartPipeline<TPipelineConfiguration>(TPipelineConfiguration pipelineConfiguration)
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
