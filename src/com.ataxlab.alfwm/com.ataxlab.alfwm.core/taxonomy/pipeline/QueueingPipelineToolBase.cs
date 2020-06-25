using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public abstract class QueueingPipelineToolBase<TQueueEntity> : IQueueingPipelineTool<QueueingChannel<TQueueEntity>, QueueingChannel<TQueueEntity>, TQueueEntity>
        where TQueueEntity : class, new()
    {
        public abstract QueueingChannel<TQueueEntity> InputBinding { get; set; }
        public abstract List<QueueingChannel<TQueueEntity>> QueueingOutputBindingCollection { get; set; }
        public abstract string InstanceId { get; set; }
        public abstract IPipelineToolStatus Status { get; set; }
        public abstract IPipelineToolContext Context { get; set; }
        public abstract IPipelineToolConfiguration Configuration { get; set; }
        public abstract IPipelineToolBinding OutputBinding { get; set; }

        public abstract event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;
        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class;
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void OnQueueHasData(object sender, TQueueEntity availableData);
        public abstract void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new();
        public abstract void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class;
        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
