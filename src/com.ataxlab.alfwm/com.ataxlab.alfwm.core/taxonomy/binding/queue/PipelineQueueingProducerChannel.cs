using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    /// <summary>
    /// implements output queue for pipelines
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class PipelineQueueingProducerChannel<TQueueEntity> : IQueueProducerPipelineBinding<TQueueEntity>
    {
        public String Id { get; set; }
        private int SyncPoint = 0;
        public PipelineQueueingProducerChannel()
        {
            Id = Guid.NewGuid().ToString();
            OutputQueue = new ConcurrentQueue<TQueueEntity>();
            ProducerPollingTimer = new System.Timers.Timer(DefaultPollingInterval);
            ProducerPollingTimer.Elapsed += ProducerPollingTimer_Elapsed;

            IsQueuePollingEnabled = true;
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
                // HandleTimerElapsedNotOverlapping();

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

        public ConcurrentQueue<TQueueEntity> OutputQueue { get; set;}
        public Timer ProducerPollingTimer { get; private set; }
        public string PipelineBindingDisplayName { get; set;}
        public string PipelineBindingKey { get; set;}
        public PipelineVariableDictionary PipelineBindingValue { get; set;}
        public double DefaultPollingInterval { get; private set; }
        public bool IsQueuePollingEnabled { get; private set; }

        public event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        public void OnQueueHasData(DateTime timestamp, TQueueEntity availableData)
        {
            QueueDataAvailableEventArgs<TQueueEntity> eventArgs = new QueueDataAvailableEventArgs<TQueueEntity>(availableData);
            eventArgs.TimeStamp = timestamp;
            eventArgs.SourceChannelId = this.Id;

            // race condition mitigation
            EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> listeners = this.QueueHasData;

            // notify the listeners
            if (listeners != null)
            {
                listeners(this, eventArgs);
            }
        }

        public void Dispose()
        {
            this.ProducerPollingTimer.Stop();
            this.ProducerPollingTimer.Dispose();
        }
    }
}
