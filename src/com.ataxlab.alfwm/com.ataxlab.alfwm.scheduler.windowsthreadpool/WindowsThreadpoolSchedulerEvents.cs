using com.ataxlab.alfwm.core.scheduler;
using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.scheduler.windowsthreadpool
{
    public partial class WindowsThreadpoolSchedulerEvents : IScheduler
    {
        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineToolStartEventArgs> WorkflowStarted;
        public event EventHandler<PipelineToolCompletedEventArgs> WorkflowCompleted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> WorkflowProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> WorkflowFailed;
        public event EventHandler<PipelineToolStartEventArgs> ActivityStarted;
        public event EventHandler<PipelineToolCompletedEventArgs> ActivityCompleted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> ActivityProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> ActivityFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public void OnActivityCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            EventHandler<PipelineToolCompletedEventArgs> handler = ActivityCompleted;
            if (handler != null)
            {
                handler(sender, args);
            };
        }

        public void OnActivityFailed(object sender, PipelineToolFailedEventArgs args)
        {
            EventHandler<PipelineToolFailedEventArgs> handler = ActivityFailed;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public void OnActivityProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            EventHandler<PipelineToolProgressUpdatedEventArgs> handler = ActivityProgressUpdated;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public void OnActivityStarted(object sender, PipelineToolStartEventArgs args)
        {
            EventHandler<PipelineToolStartEventArgs> handler = ActivityStarted;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        public void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnWorkflowCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnWorkflowFailed(object sender, PipelineToolFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnWorkflowProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnWorkflowStarted(object sender, PipelineToolStartEventArgs args)
        {
            throw new NotImplementedException();
        }

        public Task<T> SchedulePipeline<T>(T pipeline) where T : IPipelineConfiguration
        {
            throw new NotImplementedException();
        }

        public Task<TStatus> ShutdownActivity<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }

        public Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId) where TStatus : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }

        public Task<TStatus> StartActivity<TActivity, TStatus, TConfiguration>(TConfiguration activityConfiguration)
            where TActivity : IPipelineTool, new()
            where TStatus : IPipelineToolStatus, new()
            where TConfiguration : IPipelineToolConfiguration, new()
        {
            throw new NotImplementedException();
        }

        public Task<TPipelineConfiguration> StartPipeline<TPipelineConfiguration>(TPipelineConfiguration pipelineConfiguration) where TPipelineConfiguration : IPipelineConfiguration, new()
        {
            throw new NotImplementedException();
        }

        public Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration)
            where TWorkflow : IPipelineTool, new()
            where TStatus : IPipelineToolStatus, new()
            where TConfiguration : IPipelineToolConfiguration, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> StopPipeline<T>(string instanceId) where T : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }
}
