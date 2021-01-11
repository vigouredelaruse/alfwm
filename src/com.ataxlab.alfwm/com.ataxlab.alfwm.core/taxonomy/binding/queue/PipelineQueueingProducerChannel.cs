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
        private int default_polling_interval = 50;
        public String Id { get; set; }
        private int SyncPoint = 0;
        public PipelineQueueingProducerChannel()
        {
            Id = Guid.NewGuid().ToString();
            OutputQueue = new ConcurrentQueue<TQueueEntity>();
            ProducerPollingTimer = new System.Timers.Timer(default_polling_interval);
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
                this.IsQueuePollingEnabled = true;
            }
        }

        private void HandleTimerElapsedNotOverlapping()
        {
            // examine the queue
            TQueueEntity newEntity = default(TQueueEntity);
            OutputQueue.TryPeek(out newEntity);

            if (newEntity != null)
            {

                // create the notification event and notify listeners
                // note this algorithm produces a firehose
                // listeners probably want to build their own private 
                // queue of work items
                this.OnQueueHasData(DateTime.UtcNow, newEntity);

                TQueueEntity dequeuedEntity;
                // remove the item at the top of the queue
                OutputQueue.TryDequeue(out dequeuedEntity);
            }
        }

        public ConcurrentQueue<TQueueEntity> OutputQueue { get; set;}
        public Timer ProducerPollingTimer { get; private set; }
        public string PipelineBindingDisplayName { get; set;}
        public string PipelineBindingKey { get; set;}
        public PipelineVariableDictionary PipelineBindingValue { get; set;}
        public double DefaultPollingInterval { get; set; }

        bool _isQueuePollingEnabled = false;
        public bool IsQueuePollingEnabled 
        { 
            get
            {
                return this.ProducerPollingTimer.Enabled;
            }
            set

            {
                _isQueuePollingEnabled = value;
                this.ProducerPollingTimer.Enabled = _isQueuePollingEnabled;
            }
        }

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
