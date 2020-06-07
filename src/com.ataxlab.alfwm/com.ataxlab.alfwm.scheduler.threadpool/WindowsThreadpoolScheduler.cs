using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool
{
    public partial class WindowsThreadpoolScheduler : SchedulerBase
    {
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

        public override async Task<IPipelineToolStatus> StartActivity<IActivity, IPipelineToolStatus, IPipelineToolConfiguration>(IActivity activity, IPipelineToolConfiguration activityConfiguration)
        {
            activity.Configuration = activityConfiguration;
            activity.InstanceId = Guid.NewGuid().ToString();
                     return await activity.Start<IPipelineToolStatus, IPipelineToolConfiguration>(activityConfiguration);
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
