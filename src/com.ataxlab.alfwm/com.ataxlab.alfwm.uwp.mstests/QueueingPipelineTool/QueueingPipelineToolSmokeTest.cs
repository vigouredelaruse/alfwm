using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using com.ataxlab.alfwm.library.uwp.activity.queueing.htmlparser;
using com.ataxlab.alfwm.library.uwp.activity.queueing.httprequest;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using com.ataxlab.core.alfwm.utility.extension;
using com.ataxlab.alfwm.core.taxonomy.pipeline.queueing;
using com.ataxlab.alfwm.core.taxonomy.binding.queue.routing;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipelineTool
{
    public class TestDTO
    {
        public TestDTO()
        {
            id = "teststring";
            timestamp = "newtime";
        }

        public string id { get; set; }

        public string timestamp { get; set; }
    }

    [TestClass]
    public class QueueingPipelineToolSmokeTest
    {

        private ConcurrentQueue<PipelineVariable> TesseractToolWorkItemQueue { get; set; }
        private ConcurrentQueue<TaskItemPipelineVariable> ToolAWorkItemQueue { get; set; }
        private ConcurrentQueue<TaskItemPipelineVariable> ToolBWorkItemQueue { get; set; }

        [TestInitialize]
        public void Setup()
        {
            ToolAWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
            ToolBWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
            TesseractWorkQueue = new ConcurrentQueue<TaskItemPipelineVariable>();

            TesseractToolWorkItemQueue = new ConcurrentQueue<PipelineVariable>();
        }

        #region queueing pipeline tests

        /// <summary>
        /// exercises pipeline deployment operations
        /// 
        /// manually manipulates processdefinition collection
        /// deploying tools
        /// serializes it to xml
        /// then clears process definition
        /// and redeploys the serialized process definition
        /// and sends the pipeline a test message
        /// and validates the activities function as expected
        /// consuming messages, producimg results and emitting events
        /// surfaced by the pipeline
        /// </summary>
        [TestMethod]
        public void CanAddAndBindPipelineToolsToPipeline()
        {

            var testPipeline = new DefaultPipelineNodeQueueingPipeline();


            var testDTO = new TestDTO();
            
            var testPipelineVariable = new PipelineVariable(new TestDTO())
            {
                CreateDate = DateTime.UtcNow,
                Description = "variable description",
                DisplayName = "display name",
                Key = "test1",
                TimeStamp = DateTime.UtcNow,
                ID = Guid.NewGuid().ToString()
            };

            var testPipelineVariable2 = new PipelineVariable(new TestDTO())
            {
                CreateDate = DateTime.UtcNow,
                Description = "variable description",
                DisplayName = "display name",
                Key = "test1",
                TimeStamp = DateTime.UtcNow,
                ID = Guid.NewGuid().ToString()
            };

            
 

            testPipeline.PipelineCompleted += TestPipeline_PipelineCompleted;
            testPipeline.PipelineStarted += TestPipeline_PipelineStarted;
            testPipeline.PipelineProgressUpdated += TestPipeline_PipelineProgressUpdated;

            var httpActivity = new HttpRequestQueueingActivity();
            // exercise pipeline variables
            httpActivity.PipelineToolVariables.Add(testPipelineVariable);
            httpActivity.PipelineToolVariables.Add(testPipelineVariable);

            var htmlParserActivity = new HtmlParserQueueingActivity();
            // exercise pipeline variables
            htmlParserActivity.PipelineToolVariables.Add(testPipelineVariable);

            // httpActivity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent1;
            httpActivity.QueueingInputBinding.IsQueuePollingEnabled = true;

            var httpActivityConfig = new HttpRequestQueueingActivityConfiguration();
            httpActivityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
            httpActivity.PipelineToolCompleted += httpActivity_PipelineToolCompleted;

            var inputBinding = new PipelineToolQueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>();
            var outputBinding = new PipelineToolQueueingProducerChannel<HttpRequestQueueingActivityResult>();



            DefaultQueueingPipelineToolNode httpActivityNode = new DefaultQueueingPipelineToolNode()
            {
                QueueingPipelineTool = httpActivity
            };
            

            DefaultQueueingPipelineToolNode htmlParserNode = new DefaultQueueingPipelineToolNode() 
            {
                QueueingPipelineTool = htmlParserActivity
            };

            Exception bindEx = null;
            bool isMustResetBuilder = true;

            var builder = new DefaultQueueingPipelineProcessDefinitionBuilder();
            var testEntity = builder
                    .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolClassName(httpActivityNode.GetType().AssemblyQualifiedName)
                    .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolDisplayName(httpActivityNode.QueueingPipelineTool.PipelineToolDisplayName)
                    .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolId(httpActivityNode.QueueingPipelineTool.PipelineToolId)
                    //.buildPipelineTool.withPipelineToolInstanceId(httpActivityNode.QueueingPipelineTool.PipelineToolInstanceId)
                    .UsePipelineNodeBuilder.ToBuildPipelineTool.withPipelineToolPipelineVariable(testPipelineVariable)
                    .UsePipelineNodeBuilder.withToolChainSlotNumber(0)
                    .NextPipelineToolNode()
                    .BuildProcessDefinitionEntitiy(isMustResetBuilder);

            var testXml = testEntity.ToXml();

            var processDefinition = new DefaultQueueingPipelineProcessDefinitionEntity();

            try
            {
                testPipeline.AddFirstPipelineNode(httpActivityNode);

                processDefinition.QueueingPipelineNodes.Add(
                    new QueueingPipelineNodeEntity()
                        {
                            ClassName = httpActivityNode.GetType().AssemblyQualifiedName,
                            InstanceId = httpActivityNode.QueueingPipelineTool.PipelineToolInstanceId,
                            QueueingPipelineTool = new QueueingPipelineToolEntity()
                            {
                                 QueueingPipelineToolClassName = httpActivityNode.QueueingPipelineTool.GetType().AssemblyQualifiedName,
                                 PipelineToolDisplayName = httpActivityNode.QueueingPipelineTool.PipelineToolDisplayName,
                                 PipelineToolId = httpActivityNode.QueueingPipelineTool.PipelineToolId,
                                 PipelineToolDescription = httpActivityNode.QueueingPipelineTool.PipelineToolDescription,
                                PipelineToolVariables = new System.Collections.ObjectModel.ObservableCollection<PipelineVariable> { testPipelineVariable }

                            },
                            ToolChainSlotNumber = 0
                         
                        }
                    ); 

                testPipeline.AddAfterPipelineNode(0, htmlParserNode);

                processDefinition.QueueingPipelineNodes.Add(
                    new QueueingPipelineNodeEntity()
                    {
                        ClassName = htmlParserNode.GetType().AssemblyQualifiedName,
                        InstanceId = htmlParserNode.QueueingPipelineTool.PipelineToolInstanceId,
                        QueueingPipelineTool = new QueueingPipelineToolEntity()
                        {

                            QueueingPipelineToolClassName = htmlParserNode.QueueingPipelineTool.GetType().AssemblyQualifiedName,
                            PipelineToolDisplayName = htmlParserNode.QueueingPipelineTool.PipelineToolDisplayName,
                            PipelineToolId = htmlParserNode.QueueingPipelineTool.PipelineToolId,
                            PipelineToolDescription = htmlParserNode.QueueingPipelineTool.PipelineToolDescription,
                            PipelineToolVariables = new System.Collections.ObjectModel.ObservableCollection<PipelineVariable> { testPipelineVariable2 }
                        },
                        ToolChainSlotNumber = 1

                    }
                    );

                // TODO test AddLast

                var activityConfig = new HttpRequestQueueingActivityConfiguration();
                activityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
                
                // attach a pipelinevariable to the trigger message sent to the pipeline's q
                activityConfig.PipelineVariables.Add(testPipelineVariable);
                
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> entity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                {
                    Payload = activityConfig
                };

                // serialize the process definition
                var processDefinitionXML = processDefinition.ToXml(); //.SerializeObject<DefaultQueueingPipelineProcessDefiniionEntity>();

                // deserialize the process definition
                var incarnateProcessDefinition = processDefinitionXML.DeSerializeObject<DefaultQueueingPipelineProcessDefinitionEntity>();

                // validate we materialized the pipelinevariables

                var sourceVariable = htmlParserActivity.PipelineToolVariables[0].JsonValue.Trim();
                var materializedVariable = incarnateProcessDefinition.QueueingPipelineNodes[1].QueueingPipelineTool.PipelineToolVariables[0].JsonValue.Trim();

                // note this is an inaccurate test
                // TODO activate the objects and compare those
                // var variablesMatch = sourceVariable.Equals(materializedVariable);

                // validate that we persisted the pipeline tool's pipeline variables
                // Assert.IsTrue(variablesMatch, "failed to serialize pipelinetool's pipeline variables");

                // test deploying a processdefinition
                // testPipeline.Deploy(processDefinition);
                testPipeline.Deploy(incarnateProcessDefinition);

                // post the test message
                testPipeline.QueueingInputBinding.InputQueue.Enqueue(entity);
                Thread.Sleep(30000);

                var pipelineEgressMsgs = testPipeline.QueueingOutputBinding.OutputQueue.Count;
                Assert.IsTrue(pipelineEgressMsgs > 0, "pipeline did not egress messages");

                Assert.IsTrue(testPipelineDidFirePipelineProgressUpdated == true, "pipeline progress events not firing properly");
            }
            catch (Exception bEx)
            {
                bindEx = bEx;
            }

            Assert.IsNull(bindEx, "failed to add pipeline tools. exception " + bindEx?.Message);

        }

        private void TestPipeline_PipelineProgressUpdated(object sender, core.taxonomy.PipelineProgressUpdatedEventArgs e)
        {
            testPipelineDidFirePipelineProgressUpdated = true;
        }

        private void TestPipeline_PipelineStarted(object sender, core.taxonomy.PipelineStartedEventArgs e)
        {
            int i = 0;
        }

        private void TestPipeline_PipelineCompleted(object sender, core.taxonomy.PipelineCompletedEventArgs e)
        {
            int i = 0;
        }

        #endregion queueing pipeline tests
        #region default queueing tool tests
        /// <summary>
        /// test pipeline tool gateway
        /// </summary>
        [TestMethod]
        public void PipelineGatewaySmokeTest()
        {
            Exception e = null;
            string testPipelineId = Guid.NewGuid().ToString();
            string destinationPipelineId = Guid.NewGuid().ToString();
            int destinationSlot = 0;

            DefaultQueueingChannelPipelineGatewayContext testCtx = new DefaultQueueingChannelPipelineGatewayContext();
            testCtx.SeenPipelineIds.Add(testPipelineId);
            try
            {
                // gateway under test egresses entities from pipeline tools
                DefaultQueueingChannelPipelineGateway testGateway =
                    new DefaultQueueingChannelPipelineGateway(testCtx);

                // create producer and consumer channels and wire them to the gateway
                var producerChannel_PipelineGatewaySmokeTest = new PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
                var consumerChannel_PipelineGatewaySmokeTest = new PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
                testGateway.InputPorts.Add(producerChannel_PipelineGatewaySmokeTest);

                testGateway.OutputPorts.Add(consumerChannel_PipelineGatewaySmokeTest);

                consumerChannel_PipelineGatewaySmokeTest.QueueHasData += ConsumerChannel_PipelineGatewaySmokeTest_QueueHasData;

                var newItem = this.GetNewQueueEntity(1);
                // enqueue the item without a routing slip
                // we expect this entity to appear on the dead letter queue
                var newEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(newItem);
                producerChannel_PipelineGatewaySmokeTest.OutputQueue.Enqueue(newEntity);

                Thread.Sleep(30000);
                Assert.IsTrue(testGateway.DeadLetters.Count > 0, "gateway did not manage dead letter - entity has missing routing slip");

                QueueingPipelineQueueEntityRoutingSlipStep routingSlipStep = GetRoutingSlipStep(testPipelineId, destinationSlot);

                var node = new LinkedListNode<QueueingPipelineQueueEntityRoutingSlipStep>(routingSlipStep);

                QueueingPipelineQueueEntityRoutingSlip routingSlip = new QueueingPipelineQueueEntityRoutingSlip();
                routingSlip.RoutingSteps.AddFirst(
                       node
                    );

                // add the routingslip to the entity and enqueue it again
                newEntity.RoutingSlip = routingSlip;
                producerChannel_PipelineGatewaySmokeTest.OutputQueue.Enqueue(newEntity);

                Thread.Sleep(5000);

                // expect the gateway state is 1 dead letter
                Assert.IsTrue(PipelineGatewaySmokeTestDidFireQueueHasData == true, "gateway propagated enqueued entity");
                Assert.IsTrue(testGateway.DeadLetters.Count == 1, "gateway did not manage dead letter - entity has required routing slip");


                Assert.IsTrue(consumerChannel_PipelineGatewaySmokeTest.InputQueue.Count == 0, "invalid gateway state - consumer channel queue should be dehydrated by event notification on a consumer channel");

                Assert.IsTrue(testGateway.GatewayContext.MessageCount == 1, "invalid gateway state - gateway context has incorrect switched message count");
                Assert.IsTrue(testGateway.GatewayContext.DeadLetterCount == 1, "invalid gateway state - gateway context has incorrect deadletter count");


            }
            catch (Exception ex)
            {
                e = ex;
            }


            Assert.IsTrue(PipelineGatewaySmokeTestDidFireQueueHasData == true, "did not egress input entity");
            Assert.IsTrue(e == null, "test threw exception " + e?.Message);
        }

        private static QueueingPipelineQueueEntityRoutingSlipStep GetRoutingSlipStep(string destinationPipelineId, int destinationSlot)
        {
            return new QueueingPipelineQueueEntityRoutingSlipStep()
            {
                DestinationPipeline =
                                new Tuple<QueueingPipelineRoutingSlipDestination, string>(QueueingPipelineRoutingSlipDestination.Pipeline, destinationPipelineId),
                DestinationSlot = new Tuple<QueueingPipelineRoutingSlipDestination, int>(QueueingPipelineRoutingSlipDestination.PipelineSlot, destinationSlot)
            };
        }

        private void ConsumerChannel_PipelineGatewaySmokeTest_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            PipelineGatewaySmokeTestDidFireQueueHasData = true;
            PipelineGatewaySmokeTestDidFireQueueHasData_DequeuedMessages++;
        }

        /// <summary>
        /// test pipeline tool gateway
        /// </summary>
        [TestMethod]
        public void PipelineToolGatewaySmokeTest()
        {
            string testPipelineId = Guid.NewGuid().ToString();
            string destinationPipelineId = Guid.NewGuid().ToString();
            int destinationSlot = 0;

            Exception e = null;
            try
            {
                // gateway under test egresses entities from pipeline tools
                DefaultQueueingChannelPipelineToolGateway testGateway =
                    new DefaultQueueingChannelPipelineToolGateway(new DefaultQueueingChannelPipelineToolGatewayContext()
                    { CurrentPipelineId = testPipelineId });

                // create producer and consumer channels and wire them to the gateway
                var producerChannel = new PipelineToolQueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
                var consumerChannel = new PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
                testGateway.InputPorts.Add(producerChannel);
                testGateway.OutputPorts.Add(consumerChannel);
                testGateway.EgressQueueHasData += TestGateway_EgressQueueHasData;
                consumerChannel.QueueHasData += ConsumerChannel_QueueHasData;

                var newItem = this.GetNewQueueEntity(1);
                // enqueue the item without a routing slip
                // we expect this entity to appear on the dead letter queue
                var newEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(newItem);
                producerChannel.OutputQueue.Enqueue(newEntity);

                Thread.Sleep(5000);
                Assert.IsTrue(testGateway.DeadLetters.Count > 0, "gateway did not manage dead letter - entity has missing routing slip");

                string externalDestinationPipelineId = Guid.NewGuid().ToString();
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> testEntity = GetItemWithRoutingSlip(testPipelineId, destinationSlot);
                producerChannel.OutputQueue.Enqueue(testEntity);

                Thread.Sleep(5000);
                // expect the gateway state is 1 dead letter
                Assert.IsTrue(PipelineToolGatewaySmokeTestDidFireQueueHasData == true, "gateway did not propagate enqueued entity");
                Assert.IsTrue(testGateway.DeadLetters.Count == 1, "gateway did not manage dead letter - entity has required routing slip");


                Assert.IsTrue(consumerChannel.InputQueue.Count == 0, "invalid gateway state - consumer channel queue should be dehydrated by event notification on a consumer channel");

                Assert.IsTrue(testGateway.GatewayContext.MessageCount == 1, "invalid gateway state - gateway context has incorrect switched message count");
                Assert.IsTrue(testGateway.GatewayContext.DeadLetterCount == 1, "invalid gateway state - gateway context has incorrect deadletter count");

                // expect the gateway to have 1 egress message - the destination pipeline is not the current pipeline
                // listen for gateway pipeline egress message events
                //testGateway.EgressQueueHasData += TestPipelineToolGateway_EgressQueueHasData1;
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> newerEntity = GetItemWithRoutingSlip(destinationPipelineId, destinationSlot);
                producerChannel.OutputQueue.Enqueue(newerEntity);

                Thread.Sleep(5000);
                var egressPortEntityCount = testGateway.PipelineEgressPort.Count;

                // did the egress event fire on the gateway
                Assert.IsTrue(TestPipelineToolGatewayDidEgressExternalPipelineData == true,"pipeline tool gateway did not fire egress event for external pipeline destination");
                Assert.IsTrue(egressPortEntityCount == 1, "pipeline tool gateway did not fire egress event for external pipeline destination");
                int i = 0;
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.IsTrue(e == null, "test threw exception " + e?.Message);
            Assert.IsTrue(PipelineToolGatewaySmokeTestDidFireQueueHasData == true, "did not egress input entity");

         }


        private QueueingPipelineQueueEntity<IPipelineToolConfiguration> GetItemWithRoutingSlip(string destinationPipelineId, int destinationSlot)
        {
            // test delivery of entities to external pipelines
            // expect the test gateway to deliver such nodes to 
            // the egress queue and fire the relevant event
            // test entity delivery to external pipelines
            var newerItem = this.GetNewQueueEntity(1);
            var newerEntity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>(newerItem);
            QueueingPipelineQueueEntityRoutingSlipStep newerroutingSlipStep = GetRoutingSlipStep(destinationPipelineId, destinationSlot);
            var newerNode = new LinkedListNode<QueueingPipelineQueueEntityRoutingSlipStep>(newerroutingSlipStep);
            QueueingPipelineQueueEntityRoutingSlip newerroutingSlip = new QueueingPipelineQueueEntityRoutingSlip();
            newerroutingSlip.RoutingSteps.AddFirst(
                   newerNode
                );

            newerEntity.RoutingSlip = newerroutingSlip;
            return newerEntity;
        }

        private void TestGateway_EgressQueueHasData(object sender, EgressQueueHasDataEventArgs e)
        {
            TestPipelineToolGatewayDidEgressExternalPipelineData = true;
        }

        bool PipelineToolGatewaySmokeTestDidFireQueueHasData = false;
        int PipelineToolGatewaySmokeTestDidFireQueueHasData_DequeuedMessages = 0;

        private void ConsumerChannel_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            PipelineToolGatewaySmokeTestDidFireQueueHasData = true;
            PipelineToolGatewaySmokeTestDidFireQueueHasData_DequeuedMessages++;
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
            var toolA = new QueueingPipelineTool<TaskItemPipelineVariable, QueueingPipelineToolConfiguration>();
            // wire a listener to the tool's queue arrival event
            toolA.QueueingInputBinding.QueueHasData += ToolAQueueingInputBinding_QueueHasData; // += ToolA_InputBinding_QueueHasData;
            toolA.QueueingInputBinding.IsQueuePollingEnabled = true;

            var toolB = new QueueingPipelineTool<TaskItemPipelineVariable, QueueingPipelineToolConfiguration>();
            // wire a listener to the tool's queue arrival event
            toolB.QueueingInputBinding.QueueHasData += ToolBQueueingInputBinding_QueueHasData1; //  += ToolB_InputBinding_QueueHasData1;

            // wire Tool B downstream from Tool A
            toolA.QueueingOutputBindingCollection.Add(toolB.QueueingInputBinding);

            for (int itemNo = 0; itemNo < itemsToInsert; itemNo++)
            {
                // initialize a new pipeline tuple
                var newItem = this.GetNewQueueEntity(itemNo);

                // post new item to Tool A input Q
                toolA.QueueingInputBinding.InputQueue.Enqueue(new QueueingPipelineQueueEntity<TaskItemPipelineVariable>(newItem));
            }

            // pause the test while the queue processes the inputs
            Thread.Sleep(30000);

            // validate the received work item queues have the right counts
            Assert.IsTrue(ToolAWorkItemQueue.Count == itemsToInsert, "test failed, reported " + ToolAWorkItemQueue.Count + " queued items, an incorrect number of items on receive queue");

            int i = 0;
        }

        private void ToolBQueueingInputBinding_QueueHasData1(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TaskItemPipelineVariable>> e)
        {
            // new dta on Tool A queue - build a private work item queue
            ToolBWorkItemQueue.Enqueue(e.EventPayload.Payload);
        }

        private void ToolAQueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TaskItemPipelineVariable>> e)
        {
            ToolAWorkItemQueue.Enqueue(e.EventPayload.Payload);
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

        public  TaskItemPipelineVariable GetNewQueueEntity(int i)
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

        #endregion default queueing tool tests

        #region http tool tests

        [TestMethod]
        public async Task HttpToolSmokeTest()
        {
            // get an image as a byte array
            byte[] imageAsBytes;
            imageAsBytes = await GetSampleImageAsBytes();


            int i = imageAsBytes.Length;

            // initialize the tesseract tool
            PipelineVariable testTuple = new PipelineVariable();
            testTuple.Payload = imageAsBytes;

            var activity = new HttpRequestQueueingActivity();
            activity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent3; //  Activity_QueueHasAvailableDataEvent2;  //  += Activity_QueueHasAvailableDataEvent1;
            activity.QueueingInputBinding.IsQueuePollingEnabled = true;

            var activityConfig = new HttpRequestQueueingActivityConfiguration();
            activityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };

             QueueingPipelineQueueEntity<IPipelineToolConfiguration> entity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
            {
                Payload = activityConfig
            };

            activity.PipelineToolCompleted += httpActivity_PipelineToolCompleted;

            // add an output channel
            var outputChannel = new PipelineToolQueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
            activity.QueueingOutputBindingCollection.Add(outputChannel);
            var outputQueue = outputChannel; // activity.QueueingOutputBindingCollection.First();

            outputQueue.QueueHasData += OutputQueue_QueueHasData1; // += httpActivityTestOutputQueue_QueueHasData1; //  += OutputQueue_QueueHasData;
            activity.QueueingInputBinding.InputQueue.Enqueue(entity);

            // remove this when not debugging
            Thread.Sleep(30000);
            var loopMax = 60;
            var loopCounter = 0;
            var outQMsg = new List<Tuple<String, String>>();

            while (loopCounter++ < loopMax && didSignalDownstreamActivity == false)
            {
                Thread.Sleep(1000);
            }

            

            Assert.IsTrue(didSignalDownstreamActivity, "Failed to dequeue output q message");

            Assert.IsTrue(didfirePipelineToolCompleted, "test failed, did not fire activity completed event");
        }

        private void OutputQueue_QueueHasData1(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> e)
        {
            didSignalDownstreamActivity = true;

       
        }

        private IQueueingPipelineQueueEntity<IPipelineToolConfiguration> Activity_QueueHasAvailableDataEvent3(IQueueingPipelineQueueEntity<IPipelineToolConfiguration> arg)
        {
            return arg;
        }



        private object Activity_QueueHasAvailableDataEvent2(object arg)
        {
            PipelineVariable v = new PipelineVariable();
            return v;
        }

        bool didSignalDownstreamActivity = false;


        private void httpActivity_PipelineToolCompleted(object sender, core.taxonomy.PipelineToolCompletedEventArgs e)
        {
            didfirePipelineToolCompleted = true;
        }

        bool didFireQueueHasAvailableDataEvent = false;
        bool didfirePipelineToolCompleted = false;
        private bool testPipelineDidFirePipelineProgressUpdated;
        private bool PipelineGatewaySmokeTestDidFireQueueHasData;
        private int PipelineGatewaySmokeTestDidFireQueueHasData_DequeuedMessages;
        private bool TestGateway_EgressQueueHasData_DidFire;
        private bool TestPipelineToolGatewayDidEgressExternalPipelineData;

        private HttpRequestQueueingActivityConfiguration Activity_QueueHasAvailableDataEvent1(HttpRequestQueueingActivityConfiguration arg)
        {
            Assert.IsNotNull(arg, "test failed: queue arrival data");
            this.didFireQueueHasAvailableDataEvent = true;
            return arg;
        }

        ConcurrentQueue<TaskItemPipelineVariable> TesseractWorkQueue { get; set; }


        private static async Task<byte[]> GetSampleImageAsBytes()
        {
            SoftwareBitmap softwareBitmap;
            byte[] imageAsBytes;

            Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;

            StorageFile sampleFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///sample.png"));

            //Windows.Storage.StorageFile sampleFile =
            //    await storageFolder.GetFileAsync("sample.png");

            using (IRandomAccessStream stream = await sampleFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                var re = softwareBitmap.BitmapPixelFormat;
                uint size = (uint)softwareBitmap.PixelHeight * (uint)softwareBitmap.PixelWidth;
                Windows.Storage.Streams.Buffer buffer = new Windows.Storage.Streams.Buffer(size * 32);
                softwareBitmap.CopyToBuffer(buffer);

                imageAsBytes = buffer.ToArray(0, (int)buffer.Length);

            }

            return imageAsBytes;
        }

        #endregion http tool tests
    }
}
