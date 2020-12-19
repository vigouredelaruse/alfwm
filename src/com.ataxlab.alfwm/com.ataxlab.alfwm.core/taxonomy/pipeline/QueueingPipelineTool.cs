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

    /// <summary>
    /// furnish the most generic specification of a Queueing Pipeline tool
    /// it requires input and output queues
    /// with no restriction on the queue data
    /// </summary>
    public class QueueingPipelineTool : IQueueingPipelineTool, IPipelineTool<IPipelineToolConfiguration>
    {
        public IQueueConsumerPipelineToolBinding<object> QueueingInputBinding { get; set; }
        public IQueueProducerPipelineToolBinding<object> QueueingOutputBinding { get; set; }
        public IPipelineToolConfiguration<IPipelineToolConfiguration> PipelineToolConfiguration { get; set;}
        public string PipelineToolInstanceId { get; set;}
        public ObservableCollection<IPipelineVariable> PipelineToolVariables { get; set;}
        public string PipelineToolId { get; set;}
        public string PipelineToolDisplayName { get; set;}
        public string PipelineToolDescription { get; set;}
        public IPipelineToolStatus PipelineToolStatus { get; set;}
        public IPipelineToolContext PipelineToolContext { get; set;}
        public IPipelineToolBinding PipelineToolOutputBinding { get; set;}

        public event Func<object, object> QueueHasAvailableDataEvent;
        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class, new()
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

        public void OnQueueHasData(object sender, object availableData)
        {
            throw new NotImplementedException();
        }

        public void StartPipelineTool(IPipelineToolConfiguration configuration, Action<IPipelineToolConfiguration> callback)
        {
            throw new NotImplementedException();
        }

        public StopResult StopPipelineTool<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }

    public class QueueingPipelineTool<TInputQueueEntity, TOutputQueueEntity, TConfiguration> :
        QueueingPipelineToolBase<TInputQueueEntity, TOutputQueueEntity, TConfiguration>
            where TInputQueueEntity : class, IPipelineToolConfiguration, new()
            where TOutputQueueEntity : class, IPipelineToolConfiguration, new()
        // where TConfiguration : IPipelineToolConfiguration //class, new()
    {
        public override event Func<TInputQueueEntity, TInputQueueEntity> QueueHasAvailableDataEvent;
        public override event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public override event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public override event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public override event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public QueueingPipelineTool()
        {
            this.PipelineToolInstanceId = Guid.NewGuid().ToString();

            QueueingInputBinding = new QueueingConsumerChannel<QueueingPipelineQueueEntity<TInputQueueEntity>>();
            QueueingOutputBinding = new QueueingProducerChannel<QueueingPipelineQueueEntity<TOutputQueueEntity>>();
            PipelineToolVariables = new ObservableCollection<IPipelineVariable>();

            QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData; //  InputBinding_QueueHasData;
        }

        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TInputQueueEntity>> e)
        {
            // delegate the logic of the queue event handler 
            // this.OnQueueHasData(sender, e.EventPayload);
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

        [Obsolete]
        private void InputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<TInputQueueEntity> e)
        {
            // delegate the logic of the queue event handler 
            this.OnQueueHasData(sender, e.EventPayload);
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
        where TQueueEntity : class, IPipelineToolConfiguration, new()
        // where TConfiguration :  IPipelineToolConfiguration // class, new()
    {
        public QueueingPipelineTool()
        {
            this.PipelineToolInstanceId = Guid.NewGuid().ToString();

            QueueingInputBinding = new QueueingConsumerChannel<QueueingPipelineQueueEntity<TQueueEntity>>();
            QueueingOutputBinding = new QueueingProducerChannel<QueueingPipelineQueueEntity<TQueueEntity>>();

            QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData; // InputBinding_QueueHasData;
        }

        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TQueueEntity>> e)
        {
            // delegate the logic of the queue event handler 
            // this.OnQueueHasData(sender, e.EventPayload.Payload);
        }

        private void InputBinding_QueueHasData(object sender, binding.queue.QueueDataAvailableEventArgs<TQueueEntity> e)
        {

        }


        public override string PipelineToolInstanceId { get; set; }
        public override IPipelineToolStatus PipelineToolStatus { get; set; }
        public override IPipelineToolContext PipelineToolContext { get; set; }
        public override IPipelineToolConfiguration<TConfiguration> PipelineToolConfiguration { get; set; }
        public override IPipelineToolBinding PipelineToolOutputBinding { get; set; }
        public override string PipelineToolId { get ; set; }
        public override string PipelineToolDisplayName { get; set; }
        public override string PipelineToolDescription { get; set; }
        public override QueueingConsumerChannel<QueueingPipelineQueueEntity<TQueueEntity>> QueueingInputBinding {get; set; }
        public override QueueingProducerChannel<QueueingPipelineQueueEntity<TQueueEntity>> QueueingOutputBinding {get; set; }
        public override ObservableCollection<IPipelineVariable> PipelineToolVariables {get; set; }

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
                        // this.QueueingOutputBinding.OutputQueue.Enqueue(new QueueingPipelineQueueEntity<TQueueEntity>() {  Payload = result });
                        
                    });

                }
                catch (Exception e)
                {
                    // job failed, signal listeners
                }
            }

        }


        public override StopResult StopPipelineTool<StopResult>(string instanceId)
        {
            return default(StopResult);
        }

        public override void StartPipelineTool(TConfiguration configuration, Action<TConfiguration> callback)
        {
            this.PipelineToolConfiguration = new PipelineToolConfiguration<TConfiguration>() { Payload = configuration };
        }
    }
}
