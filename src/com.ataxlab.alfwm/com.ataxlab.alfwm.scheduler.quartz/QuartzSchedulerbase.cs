using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.quartz
{
    public abstract class QuartzSchedulerbase : IScheduler
    {
        public abstract event EventHandler PipelineStarted;
        public abstract event EventHandler PipelineProgressUpdated;
        public abstract event EventHandler PipelineFailed;
        public abstract event EventHandler WorkflowStarted;
        public abstract event EventHandler WorkflowProgressUpdated;
        public abstract event EventHandler WorkflowFailed;
        public abstract event EventHandler ActivityStarted;
        public abstract event EventHandler ActivityProgressUpdated;
        public abstract event EventHandler ActivityFailed;

        public abstract Task<T> SchedulePipeline<T>(T pipeline) where T : IPipelineConfiguration;
        public abstract Task<TStatus> ShutdownActivity<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new();
        public abstract Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new();
        public abstract Task<TStatus> StartActivity<TActivity, TStatus, TConfiguration>(TConfiguration activityConfiguration)
            where TActivity : IPipelineTool, new()
            where TStatus : IPipelineToolStatus, new()
            where TConfiguration : IPipelineToolConfiguration, new();
        public abstract Task<TPipelineConfiguration> StartPipeline<TPipelineConfiguration>(TPipelineConfiguration pipelineConfiguration) where TPipelineConfiguration : IPipelineConfiguration, new();
        public abstract Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration)
            where TWorkflow : IPipelineTool, new()
            where TStatus : IPipelineToolStatus, new()
            where TConfiguration : IPipelineToolConfiguration, new();
        public abstract Task<T> StopPipeline<T>(string instanceId) where T : IPipelineToolStatus, new();
    }
}
