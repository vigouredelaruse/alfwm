using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    public abstract class Activity<TConfiguration> : IPipelineTool<TConfiguration>
        where TConfiguration : class, new()
    {
        public abstract IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }

        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void StartPipelineTool<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
             where StartResult : IStartResult
             where StartConfiguration : IPipelineToolConfiguration;
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }

    [Obsolete]
    public abstract class Activity : IPipelineTool<ActivityConfiguration>
    {
        public abstract IPipelineToolConfiguration<ActivityConfiguration> PipelineToolConfiguration { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void StartPipelineTool<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
             where StartResult : IStartResult
             where StartConfiguration : IPipelineToolConfiguration;
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
