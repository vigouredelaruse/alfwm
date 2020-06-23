using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// canonical implementation of a Queueing Pipeline Tool 
    /// - supply your own queue entity
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class QueueingPipelineTool<TQueueEntity> : IQueueingPipelineTool<QueueingChannel<TQueueEntity>, QueueingChannel<TQueueEntity>, TQueueEntity>
    where TQueueEntity : class
    {
        public QueueingPipelineTool()
        {
            InputBinding = new QueueingChannel<TQueueEntity>();
            QueueingOutputBindingCollection = new List<QueueingChannel<TQueueEntity>>();

            InputBinding.QueueHasData += InputBinding_QueueHasData;
        }

        /// <summary>
        /// event listener delegate for input channel
        /// new arrivals on the queue are pushed here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void InputBinding_QueueHasData(object sender, binding.queue.QueueDataAvailableEventArgs<TQueueEntity> e)
        {
            // delegate the logic of the queue event handler 
            this.OnQueueHasData(sender, e.EventPayload);
        }

        public QueueingChannel<TQueueEntity> InputBinding { get; set; }
        public List<QueueingChannel<TQueueEntity>> QueueingOutputBindingCollection { get; set; }
        public string InstanceId { get; set; }
        public IPipelineToolStatus Status { get; set; }
        public IPipelineToolContext Context { get; set; }
        public IPipelineToolConfiguration Configuration { get; set; }
        public IPipelineToolBinding OutputBinding { get; set; }

        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// dispatch the event processing logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="availableData"></param>
        public virtual void OnQueueHasData(object sender, TQueueEntity availableData)
        {
            
        }

        public void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Action<StartResult> callback)
            where StartResult : IPipelineToolStatus, new()
            where StartConfiguration : IPipelineToolConfiguration, new()
        {
            throw new NotImplementedException();
        }

        public void Start<StartResult>(Action<StartResult> callback) where StartResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }

        public StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }
}
