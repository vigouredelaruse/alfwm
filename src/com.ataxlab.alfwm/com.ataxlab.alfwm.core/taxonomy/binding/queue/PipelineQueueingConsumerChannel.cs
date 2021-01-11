using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;


namespace com.ataxlab.alfwm.core.taxonomy.binding.queue
{
    public class PipelineQueueingConsumerChannel<TQueueEntity> : IQueueConsumerPipelineBinding<TQueueEntity>
    {
        private int SyncPoint;
        double defaultPollingInterval = 50;

        public PipelineQueueingConsumerChannel()
        {
            DefaultPollingInterval = defaultPollingInterval;

            InputQueue = new ConcurrentQueue<TQueueEntity>();
            ConsumerPollingTimer = new System.Timers.Timer(defaultPollingInterval);
            ConsumerPollingTimer.Elapsed += ConsumerPollingTimer_Elapsed;

            IsQueuePollingEnabled = true;

        }

        private void ConsumerPollingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // pause the timer
            int sync = Interlocked.CompareExchange(ref this.SyncPoint, 1, 0);
            if (sync == 0)
            {
                // pause the timer
                this.ConsumerPollingTimer.Enabled = false;
                this.IsQueuePollingEnabled = false;

                // manage the queue / dequeue / notifylisteners operation
                HandleTimerElapsedNotOverlapping();

                // reset the sync point
                SyncPoint = 0;

                // renable the timer
                this.ConsumerPollingTimer.Enabled = true;
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
        /// delegates logic for polling timer elapsed event handler
        /// </summary>
        public virtual void HandleTimerElapsedNotOverlapping()
        {

            // examine the queue
            TQueueEntity newEntity = default(TQueueEntity);
            InputQueue.TryPeek(out newEntity);

            if (newEntity != null)
            {

                // create the notification event and notify listeners
                // note this algorithm produces a firehose
                // listeners probably want to build their own private 
                // queue of work items
                this.OnQueueHasData(DateTime.UtcNow, newEntity);

                TQueueEntity dequeuedEntity;
                // remove the item at the top of the queue
                InputQueue.TryDequeue(out dequeuedEntity);

            }

        }
        public ConcurrentQueue<TQueueEntity> InputQueue { get; set;}
        public double PollingintervalMilliseconds 
        {
            get
            {
                return this.ConsumerPollingTimer.Interval;
            }
            set
            {
                this.ConsumerPollingTimer.Interval = value;
            }
        }
        public bool IsAutoResetPollingTimer 
        {
            get
            {
                return this.ConsumerPollingTimer.AutoReset;
            }
            set
            {
                this.ConsumerPollingTimer.AutoReset = value;
            }
        }
        public bool IsQueuePollingEnabled { 
            get
            {
                return this.ConsumerPollingTimer.Enabled;
            }
            set
            {
                this.ConsumerPollingTimer.Enabled = value;
            }
        }
        public System.Timers.Timer ConsumerPollingTimer { get; set;}
        public string PipelineBindingDisplayName { get; set;}
        public string PipelineBindingKey { get; set;}
        public PipelineVariableDictionary PipelineBindingValue { get; set;}
        public double DefaultPollingInterval { get; private set; }

        public event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        public void Dispose()
        {
            this.ConsumerPollingTimer.Stop();
            this.ConsumerPollingTimer.Dispose();
        }

        public void OnQueueHasData(DateTime timestamp, TQueueEntity availableData)
        {
            // prepare the eventArgs
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
