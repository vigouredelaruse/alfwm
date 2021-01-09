using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingChannel
{
    [TestClass]
    public class QueueingChannelSmokeTest
    {
        /// <summary>
        /// queue channel event listener will add dequeued entities to this queue
        /// the contents of this must match the items posted to the queue
        /// </summary>
        private ConcurrentQueue<TaskItemPipelineVariable> ReceiverQueue { get; set; }
        
        [TestInitialize]
        public void Setup()
        {
            ReceiverQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
        }

        [TestMethod]
        public void TestCanEnqueueAndNotifyListeners()
        {

            double pollingInterval = 50;
            int itemsToSend = 10;
            List<TaskItemPipelineVariable> sentItems = new List<TaskItemPipelineVariable>();

            PipelineToolQueueingConsumerChannel<TaskItemPipelineVariable> channel = new PipelineToolQueueingConsumerChannel<TaskItemPipelineVariable>(pollingInterval);
            channel.QueueHasData += Channel_QueueHasData;

            // enable the channel timer
            channel.IsQueuePollingEnabled = true;
            
            // iterate over varing versions of the payload and queue message
            for(int i = 0; i < itemsToSend; i++)
            {
                TaskItemPipelineVariable queueEntity = GetNewQueueEntity(i);

                // call the method under test
                channel.InputQueue.Enqueue(queueEntity);

                // cache the sent item for post-receive validation
                sentItems.Add(queueEntity);


            }

            // pause the test
            Thread.Sleep(15000);

            var receivedCount = ReceiverQueue.Count;
            
            // validate the number of items received = number sent
            Assert.IsTrue(receivedCount == itemsToSend, "test failed, did not receive the number of messages sent");

            // validate the items in the queue match the items in the cache of sent items
            for(int i = 0; i < itemsToSend; i++)
            {
                var cacheditem = sentItems[i];

                var receivedCachedItem = ReceiverQueue.Contains(cacheditem);

                Assert.IsTrue(receivedCachedItem == true, "test failed, could not find cached item in received items");
            }
        }

        private TaskItemPipelineVariable GetNewQueueEntity(int i)
        {
            // initialize some test data
            TaskItem payload = new TaskItem()
            {
                EndTime = DateTime.UtcNow.AddDays(1),
                StartTimme = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                TaskName = "Task Name" + i,
                TaskSummary = "Task Summary. This is the task summary " + i
            };

            TaskItemPipelineVariable queueEntity = new TaskItemPipelineVariable(payload);
            queueEntity.ID = Guid.NewGuid().ToString();
            queueEntity.TimeStamp = DateTime.UtcNow;
            return queueEntity;
        }

        /// <summary>
        /// this is the raison d'etre for clients of the channel
        /// this method is registered as a delegate to be called
        /// when the queue has items
        /// 
        /// unless this method can complete its work within the channel's polling timer interval
        /// the payload should probably be cached
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_QueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<TaskItemPipelineVariable> e)
        {
            // update the receiver queue
            // as a hedge that the receive rate
            // exceedes processing time
            ReceiverQueue.Enqueue(e.EventPayload);
        }
    }
}
