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
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class QueueingPipelineTool<TQueueEntity> : IQueueingPipelineTool<QueueingChannel<TQueueEntity>, QueueingChannel<TQueueEntity>, TQueueEntity>
    where TQueueEntity : class, new()
    {
        public QueueingPipelineTool()
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

        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;
        public event Func<TQueueEntity, TQueueEntity> QueueHasAvailableDataEvent;

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

        public StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class
        {
            throw new NotImplementedException();
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
        public void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
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
        public void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class
        {
            ThreadPool.QueueUserWorkItem((ccc) =>
            {
                callback(configuration);
            });
        }
    }
}
