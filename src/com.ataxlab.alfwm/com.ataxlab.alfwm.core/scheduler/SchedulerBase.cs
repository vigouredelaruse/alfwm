using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.scheduler
{
    /// <summary>
    /// designed with the need to surface CancellationTOken capability
    /// and flexibility of pipeline specification
    /// </summary>
    public abstract partial class SchedulerBase : IScheduler
    {
        public abstract Task<T> SchedulePipeline<T>(T pipeline) where T : IPipelineConfiguration;
        public abstract Task<TStatus> ShutdownActivity<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new();
        public abstract Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new();
        public abstract  void StartActivity<TActivity, TStatus, TConfiguration>(TActivity activity, TConfiguration activityConfiguration, Action<TStatus> callback)
            where TActivity : class, IPipelineTool, new()
            where TStatus : ActivityStatus, new()
            where TConfiguration : class, IPipelineToolConfiguration, new();
        public abstract Task<TStatus> StartPipeline<TStatus, TPipeline>(TPipeline pipeline)
            where TStatus : class, IPipelineStatus, new()
            where TPipeline : class, IPipeline, new();
        public abstract Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration)
            where TWorkflow : class, IPipelineTool, new()
            where TStatus : class, IPipelineToolStatus, new()
            where TConfiguration : class, IPipelineToolConfiguration, new();
        public abstract Task<T> StopPipeline<T>(string instanceId) where T : IPipelineToolStatus, new();
    }
}
