using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public abstract class QueueingPipelineToolbase<TInputQueueEntity, TOutputQueueEntity, TPipelineToolConfiguration> : IQueueingPipelineTool<TInputQueueEntity, TOutputQueueEntity>, IPipelineTool<TPipelineToolConfiguration>
        where TInputQueueEntity : class, IQueueingPipelineQueueEntity<IPipelineToolConfiguration>, new()
        where TOutputQueueEntity : class, IQueueingPipelineQueueEntity<IPipelineToolConfiguration>, new()
        where TPipelineToolConfiguration : IPipelineToolConfiguration
    {
        public abstract QueueingConsumerChannel<TInputQueueEntity> QueueingInputBinding { get; set; }
        public abstract QueueingProducerChannel<TOutputQueueEntity> QueueingOutputBinding { get; set; }
        public abstract List<QueueingConsumerChannel<TOutputQueueEntity>> QueueingOutputBindingCollection { get; set; }
        public abstract IPipelineToolConfiguration<TPipelineToolConfiguration> PipelineToolConfiguration { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }

        public abstract event Func<TInputQueueEntity, TInputQueueEntity> QueueHasAvailableDataEvent;
        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void OnQueueHasData(object sender, TInputQueueEntity availableData);
        public abstract void StartPipelineTool(TPipelineToolConfiguration configuration, Action<TPipelineToolConfiguration> callback);
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }

    public abstract class QueueingPipelineToolBase : IQueueingPipelineTool, IPipelineTool<IPipelineToolConfiguration>
    {
        public abstract QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingInputBinding { get; set; }
        public abstract QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingOutputBinding { get; set; }
        //public abstract List<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> QueueingOutputBindingCollection { get; set; }
        public abstract IPipelineToolConfiguration<IPipelineToolConfiguration> PipelineToolConfiguration { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public abstract List<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>> QueueingOutputBindingCollection { get; set; }

        public abstract event Func<IQueueingPipelineQueueEntity<IPipelineToolConfiguration>, IQueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueHasAvailableDataEvent;
        public abstract event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public abstract event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public abstract event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public abstract event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public abstract void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new();
        public abstract void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args);
        public abstract void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args);
        public abstract void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args);
        public abstract void OnQueueHasData(object sender, QueueingPipelineQueueEntity<IPipelineToolConfiguration> availableData);
        public abstract void StartPipelineTool(IPipelineToolConfiguration configuration, Action<IPipelineToolConfiguration> callback);
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }

    public abstract class QueueingPipelineToolBase<TInputQueueEntity, TOutputQueueEntity, TQueueConfiguration>
        : IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<TInputQueueEntity>>, 
            QueueingProducerChannel<QueueingPipelineQueueEntity<TOutputQueueEntity>>, TInputQueueEntity, TOutputQueueEntity, TQueueConfiguration>
            where TInputQueueEntity : class, IPipelineToolConfiguration, new()
            where TOutputQueueEntity : class, IPipelineToolConfiguration, new()
        //where TQueueConfiguration : class, new()
    {
        public virtual string PipelineToolInstanceId {get; set;}
        public virtual ObservableCollection<IPipelineVariable> PipelineToolVariables {get; set;}
        public virtual string PipelineToolId {get; set;}
        public virtual string PipelineToolDisplayName {get; set;}
        public virtual string PipelineToolDescription {get; set;}
        public virtual IPipelineToolStatus PipelineToolStatus {get; set;}
        public virtual IPipelineToolContext PipelineToolContext {get; set;}
        public virtual IPipelineToolConfiguration<TQueueConfiguration> PipelineToolConfiguration {get; set;}
        public virtual IPipelineToolBinding PipelineToolOutputBinding {get; set;}
        public virtual QueueingConsumerChannel<QueueingPipelineQueueEntity<TInputQueueEntity>> QueueingInputBinding { get; set; }
        public virtual QueueingProducerChannel<QueueingPipelineQueueEntity<TOutputQueueEntity>> QueueingOutputBinding { get; set; }
        public abstract List<QueueingConsumerChannel<QueueingPipelineQueueEntity<TInputQueueEntity>>> QueueingOutputBindingPorts { get; set; }
        public abstract List<QueueingConsumerChannel<QueueingPipelineQueueEntity<TInputQueueEntity>>> QueueingOutputBindingCollection { get; set; }

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

        public abstract void StartPipelineTool(TQueueConfiguration configuration, Action<TQueueConfiguration> callback);

        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();


    }


    public abstract class QueueingPipelineToolBase<TQueueEntity, TConfiguration> :
        IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<TQueueEntity>>, QueueingProducerChannel<QueueingPipelineQueueEntity<TQueueEntity>>, TQueueEntity, TConfiguration>
          //where TConfiguration : class, new()
          where TQueueEntity : class, IPipelineToolConfiguration, new()
    {
        public abstract QueueingConsumerChannel<QueueingPipelineQueueEntity<TQueueEntity>> QueueingInputBinding { get; set; }
        public abstract List<QueueingConsumerChannel<QueueingPipelineQueueEntity<TQueueEntity>>> QueueingOutputBindingCollection { get; set; }
        public abstract QueueingProducerChannel<QueueingPipelineQueueEntity<TQueueEntity>> QueueingOutputBinding { get; set; }
        public abstract IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        public abstract string PipelineToolInstanceId { get; set; }
        public abstract ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set; }
        public abstract string PipelineToolId { get; set; }
        public abstract string PipelineToolDisplayName { get; set; }
        public abstract string PipelineToolDescription { get; set; }
        public abstract IPipelineToolStatus PipelineToolStatus { get; set; }
        public abstract IPipelineToolContext PipelineToolContext { get; set; }
        public abstract IPipelineToolBinding PipelineToolOutputBinding { get; set; }

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
        public abstract void OnQueueHasData(object sender, object availableData);
        public abstract void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback);
        public abstract StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new();
    }
}
