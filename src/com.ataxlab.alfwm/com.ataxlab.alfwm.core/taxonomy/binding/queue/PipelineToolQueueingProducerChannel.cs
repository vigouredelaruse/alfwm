using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{
    public class PipelineToolQueueingProducerChannel<TQueueEntity> : IQueueProducerPipelineToolBinding<TQueueEntity>
              // where TQueueEntity : class,  new()
    {

        private double DefaultPollingInterval = 50;
        public bool IsQueuePollingEnabled { get; set; }
        private int SyncPoint = 0;

        public virtual ConcurrentQueue<TQueueEntity> OutputQueue {get; set;}
        public System.Timers.Timer ProducerPollingTimer { get; private set; }
        public string PipelineToolBindingDisplayName {get; set;}
        public string PipelineToolBindingKey {get; set;}
        public PipelineVariableDictionary PipelineToolBindingValue {get; set;}

        public event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        public PipelineToolQueueingProducerChannel()
        {
            OutputQueue = new ConcurrentQueue<TQueueEntity>();
            ProducerPollingTimer = new System.Timers.Timer(DefaultPollingInterval);
            ProducerPollingTimer.Elapsed += ProducerPollingTimer_Elapsed;

            IsQueuePollingEnabled = true;
        }

        public void Dispose()
        {
            this.ProducerPollingTimer.Stop();
            this.ProducerPollingTimer.Dispose();
        }

        private void ProducerPollingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // pause the timer
            int sync = Interlocked.CompareExchange(ref this.SyncPoint, 1, 0);
            if (sync == 0)
            {
                // pause the timer
                this.ProducerPollingTimer.Enabled = false;
                this.IsQueuePollingEnabled = false;

                // manage the queue / dequeue / notifylisteners operation
                HandleTimerElapsedNotOverlapping();

                // reset the sync point
                SyncPoint = 0;

                // renable the timer
                this.ProducerPollingTimer.Enabled = true;
                this.IsQueuePollingEnabled = true;
            }
            else
            {
                // here because we have discarded events
                // for our current implementation we 
                // will discard these timer events
                // as there is low risk of data loss
            }
        }

        /// <summary>
        /// TODO - consider adding this to an interface
        /// this gives an opportunity to signal 
        /// when the activity's producer timer fires
        /// </summary>
        public virtual void HandleTimerElapsedNotOverlapping()
        {
            if(OutputQueue.Count > 0)
            {

            }
        }

        /// <summary>
        /// this is awkward for a producer queue - an impedence mismatch
        /// the issue is sending the listenre a copy of the quque data
        /// means dequeueing it
        /// 
        /// instead we probably want the Queue Consumer's logic to handle this
        /// entirely
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="availableData"></param>
        public virtual void OnQueueHasData(DateTime timestamp, TQueueEntity availableData)
        {
            QueueDataAvailableEventArgs<TQueueEntity> eventArgs = new QueueDataAvailableEventArgs<TQueueEntity>(availableData);
            eventArgs.TimeStamp = timestamp;

            // race condition mitigation
            EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> listeners = this.QueueHasData;

            // notify the listeners
            if (listeners != null)
            {
                listeners(this, eventArgs);
            }
        }
    }
}
