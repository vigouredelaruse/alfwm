using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace com.ataxlab.alfwm.core.taxonomy.binding
{

    /// <summary>
    /// specify a polled queue with event driven 
    /// notification of new items available on the queue
    /// </summary>
    /// <typeparam name="TQueueEntity"></typeparam>
    /// <typeparam name="TPollingTimer"></typeparam>
    public class QueueingConsumerChannel<TQueueEntity> : IQueueConsumerPipelineToolBinding<TQueueEntity>
        where TQueueEntity : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private double DefaultPollingInterval = 50;

        /// <summary>
        /// synchronization entity for mitigation
        /// against overlapping timer elapsed event processing 
        /// as per https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer.stop?view=netcore-3.1
        /// </summary>
        private int SyncPoint = 0;
       
        public QueueingConsumerChannel() 
        {
            InputQueue = new ConcurrentQueue<TQueueEntity>();
            ConsumerPollingTimer = new System.Timers.Timer(DefaultPollingInterval);
            ConsumerPollingTimer.Elapsed += ConsumerPollingTimer_Elapsed;

            IsQueuePollingEnabled = true;

        }


        public QueueingConsumerChannel(double pollingInterval)
        {

            InputQueue = new ConcurrentQueue<TQueueEntity>();

            ConsumerPollingTimer = new System.Timers.Timer(pollingInterval);
            this.PollingintervalMilliseconds = pollingInterval;
            ConsumerPollingTimer.Elapsed += ConsumerPollingTimer_Elapsed;

            IsQueuePollingEnabled = true;
        }

        public virtual ConcurrentQueue<TQueueEntity> InputQueue { get; set; }
        
        public virtual double PollingintervalMilliseconds 
        { 
            get
            {
                return ConsumerPollingTimer.Interval;
            }

            set
            {
                ConsumerPollingTimer.Interval = value;
            }
        }
 
        public virtual bool IsAutoResetPollingTimer 
        { 
            get
            {
                return ConsumerPollingTimer.AutoReset;
            }
            set
            {
                ConsumerPollingTimer.AutoReset = value;
            }
        }
        
        public virtual bool IsQueuePollingEnabled 
        { 
            get
            {
                return ConsumerPollingTimer.Enabled;
            }
            set
            {
                ConsumerPollingTimer.Enabled = value;
            }
        }
        public System.Timers.Timer ConsumerPollingTimer { get; set; }
        public string PipelineToolBindingDisplayName { get; set; }
        public string PipelineToolBindingKey { get; set; }
        public PipelineVariableDictionary PipelineToolBindingValue { get; set; }

        public event EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> QueueHasData;

        public void Dispose()
        {
            this.ConsumerPollingTimer.Stop();
            this.ConsumerPollingTimer.Dispose();
        }

        /// <summary>
        /// signal the listeners
        /// as per https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="availableData"></param>
        public virtual void OnQueueHasData(DateTime timestamp, TQueueEntity availableData)
        {
            // prepare the eventArgs
            QueueDataAvailableEventArgs<TQueueEntity> eventArgs = new QueueDataAvailableEventArgs<TQueueEntity>(availableData);
            eventArgs.TimeStamp = timestamp;

            // race condition mitigation
            EventHandler<QueueDataAvailableEventArgs<TQueueEntity>> listeners = this.QueueHasData;
            
            // notify the listeners
            if(listeners != null)
            {
                listeners(this, eventArgs);
            }
        }

        /// <summary>
        /// timer elapsed - examine the queue
        /// probably want to configure the timer to be oneshotmode
        /// to prevent re-entrancy issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ConsumerPollingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // pause the timer
            int sync = Interlocked.CompareExchange(ref this.SyncPoint, 1, 0);
            if(sync == 0)
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
            TQueueEntity newEntity = null;
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
    }
}
