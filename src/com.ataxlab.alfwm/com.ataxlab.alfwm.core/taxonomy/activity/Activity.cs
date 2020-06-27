using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.core.taxonomy.activity
{
    [Obsolete]
    public abstract class Activity : IPipelineTool
    {
        public abstract string InstanceId { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string DisplayName { get; set; }
        public abstract string Description { get; set; }
        public abstract IPipelineToolStatus Status { get; set; }
        public abstract IPipelineToolContext Context { get; set; }
        public abstract IPipelineToolConfiguration Configuration { get; set; }
        public abstract IPipelineToolBinding OutputBinding { get; set; }

        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class;
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new();
        public abstract void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class;
        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
