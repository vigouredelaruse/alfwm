using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{

    public abstract class QueueingPipelineToolBase<TInputQueueEntity, TOutputQueueEntity, TQueueConfiguration>
        : IQueueingPipelineTool<QueueingChannel<TInputQueueEntity>, QueueingChannel<TOutputQueueEntity>, TInputQueueEntity, TOutputQueueEntity, TQueueConfiguration>
        where TInputQueueEntity : class, new()
        where TOutputQueueEntity : class, new()
        where TQueueConfiguration : class, new()
    {
        public virtual QueueingChannel<TInputQueueEntity> InputBinding {get; set;}
        public virtual QueueingChannel<TOutputQueueEntity> OutputBinding {get; set;}
        public virtual string PipelineToolInstanceId {get; set;}
        public ObservableCollection<IPipelineVariable> PipelineToolVariables {get; set;}
        public virtual string PipelineToolId {get; set;}
        public virtual string PipelineToolDisplayName {get; set;}
        public virtual string PipelineToolDescription {get; set;}
        public virtual IPipelineToolStatus PipelineToolStatus {get; set;}
        public virtual IPipelineToolContext PipelineToolContext {get; set;}
        public virtual IPipelineToolConfiguration<TQueueConfiguration> PipelineToolConfiguration {get; set;}
        public virtual IPipelineToolBinding PipelineToolOutputBinding {get; set;}

        public virtual event  Func<TInputQueueEntity, TInputQueueEntity> QueueHasAvailableDataEvent;
        public virtual event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public virtual event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public virtual event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public virtual event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public virtual void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(this, new PipelineToolFailedEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { }
            });
        }

        public virtual void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, new PipelineToolProgressUpdatedEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { },
                OutputVariables = this.PipelineToolVariables
            });
        }

        public virtual void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {

            PipelineToolStarted?.Invoke(this, new PipelineToolStartEventArgs()
            {
                Status = { },
                InstanceId = this.PipelineToolInstanceId
            }) ;
        }

        public abstract void OnQueueHasData(object sender, TInputQueueEntity availableData);

        public abstract void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
            where StartConfiguration : class, IPipelineToolConfiguration, new();
        public abstract void StartPipelineTool(TQueueConfiguration configuration, Action<TQueueConfiguration> callback);

        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }


    public abstract class QueueingPipelineToolBase<TQueueEntity, TConfiguration> : IQueueingPipelineTool<QueueingChannel<TQueueEntity>, QueueingChannel<TQueueEntity>, TQueueEntity, TConfiguration>
        where TConfiguration : class, new()
        where TQueueEntity : class, new()
    {
        public abstract QueueingChannel<TQueueEntity> InputBinding { get; set; }
        public abstract List<QueueingChannel<TQueueEntity>> QueueingOutputBindingCollection { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public ObservableCollection<IPipelineVariable> PipelineToolVariables {get; set;}

        public abstract event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;
        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void OnQueueHasData(object sender, TQueueEntity availableData);
        public abstract void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
            where StartConfiguration : class, IPipelineToolConfiguration, new();
        public abstract void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback);
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
