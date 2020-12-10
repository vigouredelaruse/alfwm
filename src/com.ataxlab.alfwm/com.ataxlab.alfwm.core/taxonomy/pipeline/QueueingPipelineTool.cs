using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{

    public class QueueingPipelineTool<TInputQueueEntity, TOutputQueueEntity, TConfiguration> :
        QueueingPipelineToolBase<TInputQueueEntity, TOutputQueueEntity, TConfiguration>
        where TInputQueueEntity : class, new()
        where TOutputQueueEntity : class, new()
        where TConfiguration : class, new()
    {
        public override event Func<TInputQueueEntity, TInputQueueEntity> QueueHasAvailableDataEvent;
        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public QueueingPipelineTool()
        {
            this.PipelineToolInstanceId = Guid.NewGuid().ToString();

            InputBinding = new QueueingChannel<TInputQueueEntity>();
            OutputBinding = new QueueingChannel<TOutputQueueEntity>();
            PipelineToolVariables = new ObservableCollection<IPipelineVariable>();

            InputBinding.QueueHasData += InputBinding_QueueHasData;
        }

        public override void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args)
        {

            PipelineToolStarted?.Invoke(this, new PipelineToolStartEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { }
            });
        }

        public override void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            PipelineToolFailed?.Invoke(this, new PipelineToolFailedEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { }
            });
        }

        public override void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            PipelineToolProgressUpdated?.Invoke(this, new PipelineToolProgressUpdatedEventArgs()
            {
                InstanceId = this.PipelineToolInstanceId,
                Status = { },
                OutputVariables = this.PipelineToolVariables
            });
        }

        public override void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            PipelineToolStarted?.Invoke(this, new PipelineToolStartEventArgs()
            {
                Status = { },
                InstanceId = this.PipelineToolInstanceId
            });
        }

        public override void OnQueueHasData(object sender, TInputQueueEntity availableData)
        {
            Func<TInputQueueEntity, TInputQueueEntity> handler = this.QueueHasAvailableDataEvent;
            if (handler != null)
            {
                try
                {
                    var threadStarted = ThreadPool.QueueUserWorkItem((arg) =>
                    {
                        // invoke the client's delegate logic
                        var result = handler(availableData);

                        // perhaps include auditing strategy uwing the result here

                    });

                }
                catch (Exception e)
                {
                    // job client delegate failed, perhaps signal listeners
                    // perhaps audit
                }
            }

        }

        private void InputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<TInputQueueEntity> e)
        {
            // delegate the logic of the queue event handler 
            this.OnQueueHasData(sender, e.EventPayload);
        }

        public override void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            /// TODO something useful here
        }


        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            // TODO something useful here
            return default(StopResult);
        }

        public override void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback)
        {
           // TODO something useful here
        }
    }

    /// <summary>
    /// minimal implementation of a queueing pipeline tool
    /// supply your own Queue Entity and Queue event arrival handler logic
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class QueueingPipelineTool<TQueueEntity, TConfiguration> : QueueingPipelineToolBase<TQueueEntity, TConfiguration>
        where TQueueEntity : class, new()
        where TConfiguration : class, new()
    {
        public QueueingPipelineTool()
        {
            this.PipelineToolInstanceId = Guid.NewGuid().ToString();

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
        public override string PipelineToolInstanceId { get; set; }
        public override IPipelineToolStatus PipelineToolStatus { get; set; }
        public override IPipelineToolContext PipelineToolContext { get; set; }
        public override IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        public override IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public override string PipelineToolId { get ; set; }
        public override string PipelineToolDisplayName { get; set; }
        public override string PipelineToolDescription { get; set; }

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

        public override void StartPipelineTool<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<TConfiguration>() { Configuration = configuration as TConfiguration};
            callback(configuration);
        }

        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            return default(StopResult);
        }

        public override void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<TConfiguration>() { Configuration = configuration };
        }
    }
}
