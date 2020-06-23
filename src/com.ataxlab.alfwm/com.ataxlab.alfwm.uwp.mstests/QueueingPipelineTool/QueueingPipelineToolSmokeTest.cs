using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipelineTool
{
    [TestClass]
    public class QueueingPipelineToolSmokeTest
    {
        private ConcurrentQueue<TaskItemPipelineVariable> ToolAWorkItemQueue { get; set; }
        private ConcurrentQueue<TaskItemPipelineVariable> ToolBWorkItemQueue { get; set; }

        [TestInitialize]
        public void Setup()
        {
            ToolAWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
            ToolBWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
        }

        /// <summary>
        /// initialize a queueing pipeline tool
        /// post some messages to its input
        /// receive some processed messages on its output
        /// </summary>
        [TestMethod]
        public void CanPassThroughToOutputChannels()
        {

            var itemsToInsert = 10;

            // initialize a tool
            QueueingPipelineTool<TaskItemPipelineVariable> toolA = new QueueingPipelineTool<TaskItemPipelineVariable>();
            // wire a listener to the tool's queue arrival event
            toolA.InputBinding.QueueHasData += ToolA_InputBinding_QueueHasData;
            
            QueueingPipelineTool<TaskItemPipelineVariable> toolB = new QueueingPipelineTool<TaskItemPipelineVariable>();
            // wire a listener to the tool's queue arrival event
            toolB.InputBinding.QueueHasData += ToolB_InputBinding_QueueHasData1;

            // wire Tool B downstream from Tool A
            toolA.QueueingOutputBindingCollection.Add(toolB.InputBinding);

            for(int itemNo = 0; itemNo < itemsToInsert; itemNo++)
            {
                // initialize a new pipeline tuple
                var newItem = this.GetNewQueueEntity(itemNo);

                // post new item to Tool A input Q
                toolA.InputBinding.InputQueue.Enqueue(newItem);
            }

            // validate the received work item queues have the right counts
            Assert.IsTrue(ToolAWorkItemQueue.Count == itemsToInsert, "test failed, incorrect number of items on receive queue");

            int i = 0;
        }

        private void ToolB_InputBinding_QueueHasData1(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<TaskItemPipelineVariable> e)
        {
            // new dta on Tool A queue - build a private work item queue
            ToolBWorkItemQueue.Enqueue(e.EventPayload);
        }

        private void ToolA_InputBinding_QueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<TaskItemPipelineVariable> e)
        {
            ToolAWorkItemQueue.Enqueue(e.EventPayload);
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
    }
}
