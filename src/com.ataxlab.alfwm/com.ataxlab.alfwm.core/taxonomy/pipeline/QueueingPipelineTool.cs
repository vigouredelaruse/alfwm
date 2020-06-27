using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// canonical implementation of a Queueing Pipeline Tool 
    /// - supply your own queue entity
    /// - supply your own queue processing event handler 
    /// - expect automatic notification of queue data arrival
    /// or
    /// - supply your own derived class overriding the virtuals as you see fit
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class QueueingPipelineToolObsolete<TQueueEntity> : IQueueingPipelineTool<QueueingChannel<TQueueEntity>, QueueingChannel<TQueueEntity>, TQueueEntity>
    where TQueueEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public QueueingPipelineToolObsolete()
        {

            this.InstanceId = Guid.NewGuid().ToString();

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
        public string PipelineToolId { get; set; }
        public string DisplayName { get; set ; }
        public string Description { get ; set ; }

        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;
        public event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;

        public virtual void OnPipelineToolCompleted(object sender, PipelineToolCompletedEventArgs args)
        {
            PipelineToolCompleted?.Invoke(this, args);
        }

        public virtual void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(this, args);
        }

        public virtual void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, args);
        }

        public virtual void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(this, args);
        }

        /// <summary>
        /// dispatch the event processing logic delegates on the threadpool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="availableData"></param>
        public virtual void OnQueueHasData(object sender, TQueueEntity availableData)
        {
            // signal listeners to the event
            Func<TQueueEntity, TQueueEntity> handler = this.QueueHasAvailableDataEvent;
            if(handler != null)
            {
                try
                {
                    var threadStarted = ThreadPool.QueueUserWorkItem((arg) =>
                    {
                        // invoke the client's delegate logic
                        var result = handler(availableData);

                        // reflect the result on the output binding
                        foreach (var channel in this.QueueingOutputBindingCollection)
                        {
                            channel.InputQueue.Enqueue(result);
                        }
                    });

                }
                catch(Exception e)
                {
                    // job failed, signal listeners
                }
            }

            // 
        }

        public virtual StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }

        public virtual void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class
        {
            PipelineToolCompletedEventArgs completedArgs = new PipelineToolCompletedEventArgs();
            completedArgs.Payload = args.Payload;
            PipelineToolCompleted?.Invoke(this, completedArgs);
        }

        /// <summary>
        /// dispatch a work item on the threadpool
        /// note this is independent of the queue processing
        /// 
        /// clients of the queue may for instance, dispatch work items on the threadpool here
        /// </summary>
        /// <typeparam name="StartResult"></typeparam>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        public virtual void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new()
        {
            ThreadPool.QueueUserWorkItem((ccc) =>
            {
                var result = callback(configuration);
            });
        }

        /// <summary>
        /// dispatch a workitem on the threadpool
        /// note this is independent of queue processing
        /// </summary>
        /// <typeparam name="StartConfiguration"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="callback"></param>
        public virtual void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class
        {
            ThreadPool.QueueUserWorkItem((ccc) =>
            {
                callback(configuration);
            });
        }
    }

    public class QueueingPipelineTool<TQueueEntity> : QueueingPipelineToolBase<TQueueEntity>
        where TQueueEntity : class, new()
    {
        public QueueingPipelineTool()
        {
            this.InstanceId = Guid.NewGuid().ToString();

            InputBinding = new QueueingChannel<TQueueEntity>();
            QueueingOutputBindingCollection = new List<QueueingChannel<TQueueEntity>>();

            InputBinding.QueueHasData += InputBinding_QueueHasData;
        }

        private void InputBinding_QueueHasData(object sender, binding.queue.QueueDataAvailableEventArgs<TQueueEntity> e)
        {
            // delegate the logic of the queue event handler 
            this.OnQueueHasData(sender, e.EventPayload);
        }

        public override QueueingChannel<TQueueEntity> InputBinding { get; set; }
        public override List<QueueingChannel<TQueueEntity>> QueueingOutputBindingCollection { get; set; }
        public override string InstanceId { get; set; }
        public override IPipelineToolStatus Status { get; set; }
        public override IPipelineToolContext Context { get; set; }
        public override IPipelineToolConfiguration Configuration { get; set; }
        public override IPipelineToolBinding OutputBinding { get; set; }
        public override string PipelineToolId { get ; set; }
        public override string DisplayName { get; set; }
        public override string Description { get; set; }

        public override event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;
        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {
            PipelineToolCompletedEventArgs evt = new PipelineToolCompletedEventArgs();
            evt.Payload = args.Payload;

            PipelineToolCompleted?.Invoke(this, evt);
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        { 

            PipelineToolFailed?.Invoke(this, args);
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, args);
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(this, args);
        }

        public override void OnQueueHasData(object sender, TQueueEntity availableData)
        {
            Func<TQueueEntity, TQueueEntity> handler = this.QueueHasAvailableDataEvent;
            if (handler != null)
            {
                try
                {
                    var threadStarted = ThreadPool.QueueUserWorkItem((arg) =>
                    {
                        // invoke the client's delegate logic
                        var result = handler(availableData);

                        // reflect the result on the output binding
                        foreach (var channel in this.QueueingOutputBindingCollection)
                        {
                            channel.InputQueue.Enqueue(result);
                        }
                    });

                }
                catch (Exception e)
                {
                    // job failed, signal listeners
                }
            }

        }

        public override void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            throw new NotImplementedException();
        }

        public override StopResult Stop<StopResult>(string instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
