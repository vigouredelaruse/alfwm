using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.uwp
{
    public abstract class WindowsThreadpoolSchedulerBase : SchedulerBase
    {
        public abstract override Task<T> SchedulePipeline<T>(T pipeline);

        public abstract override Task<TStatus> ShutdownActivity<TStatus>(string instanceId);

        public abstract override Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId);

        public abstract override  void StartActivity<TActivity, TConfiguration>(TActivity activity, TConfiguration activityConfiguration, Action<TConfiguration> callback);
        
        
        public abstract override Task<TStatus> StartPipeline<TStatus, TPipeline>(TPipeline pipeline);

        public abstract override Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration);

        public abstract override Task<T> StopPipeline<T>(string instanceId);

    }
}
