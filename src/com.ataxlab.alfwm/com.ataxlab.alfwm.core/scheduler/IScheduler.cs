using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.activity;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.scheduler
{
    public interface IScheduler
    {

        event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        event EventHandler<PipelineToolStartEventArgs> WorkflowStarted;
        event EventHandler<PipelineToolCompletedEventArgs> WorkflowCompleted;
        event EventHandler<PipelineToolProgressUpdatedEventArgs> WorkflowProgressUpdated;
        event EventHandler<PipelineToolFailedEventArgs> WorkflowFailed;
        event EventHandler<PipelineToolStartEventArgs> ActivityStarted;
        event EventHandler<PipelineToolCompletedEventArgs> ActivityCompleted;
        event EventHandler<PipelineToolProgressUpdatedEventArgs> ActivityProgressUpdated;
        event EventHandler<PipelineToolFailedEventArgs> ActivityFailed;

        void OnWorkflowStarted(object sender, PipelineToolStartEventArgs args);
        void OnActivityStarted(object sender, PipelineToolStartEventArgs args);

        void OnWorkflowProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        void OnActivityProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);

        void OnWorkflowFailed(object sender, PipelineToolFailedEventArgs args);
        void OnActivityFailed(object sender, PipelineToolFailedEventArgs args);

        void OnWorkflowCompleted(object sender, PipelineToolCompletedEventArgs args);
        void OnActivityCompleted(object sender, PipelineToolCompletedEventArgs args);

        void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
        void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
        void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
        void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);


        //        Task<TPipelineConfiguration> StartPipeline<TPipelineConfiguration>(TPipelineConfiguration pipelineConfiguration)
        //                where TPipelineConfiguration : IPipelineConfiguration, new();
        //    Task<TPipeline> StartPipeline<TPipeline>(TPipeline pipeline)
        //where TPipeline : IPipeline, new();

        Task<TStatus> StartPipeline<TStatus, TPipeline>(TPipeline pipeline) 
            where TPipeline : class, IPipeline, new() 
            where TStatus : class,  IPipelineStatus, new();

        void StartActivity<TActivity, TStatus, TConfiguration>(TActivity activity, TConfiguration activityConfiguration, Action<TStatus> callback)
            where TActivity : class, IPipelineTool<TConfiguration>,  new() 
            where TConfiguration : class, new()
            where TStatus : ActivityStatus, new();

        Task<T> StopPipeline<T>(string instanceId) 
            where T : IPipelineToolStatus, new();

        Task<T> SchedulePipeline<T>(T pipeline) 
            where T : IPipelineConfiguration;

        Task<TStatus> StartWorkflow<TWorkflow, TStatus, TConfiguration>(TConfiguration workflowConfiguration) 
            where TWorkflow : class, IPipelineTool<TConfiguration>, new()
            where TConfiguration : class, new() 
            where TStatus : class,  IPipelineToolStatus, new();

        Task<TStatus> ShutdownWorkflow<TStatus>(string instanceId) 
            where TStatus : IPipelineToolStatus, new();



        Task<TStatus> ShutdownActivity<TStatus>(string instanceId)
            where TStatus : IPipelineToolStatus, new();


    }
}
