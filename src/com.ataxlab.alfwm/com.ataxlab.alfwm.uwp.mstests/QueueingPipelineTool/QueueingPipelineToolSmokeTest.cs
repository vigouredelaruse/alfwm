using com.ataxlab.alfwm.core.collections;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace com.ataxlab.alfwm.uwp.mstests.QueueingPipelineTool
{
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
        [TestMethod]
        public void CanAddAndBindPipelineToolsToPipeline()
        {
            //var testPipeline = new QueueingPipeline<QueueingPipelineProcessDefinition<HttpRequestQueueingActivityConfiguration, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingProducerChannel<List<Tuple<String, String>>>, HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>>
            //  , QueueingPipelineNode<HttpRequestQueueingActivity, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingProducerChannel<List<Tuple<String, String>>>, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>>>();

            var testPipeline = new DefaultPipelineNodeQueueingPipeline();
            
            testPipeline.PipelineCompleted += TestPipeline_PipelineCompleted;
            testPipeline.PipelineStarted += TestPipeline_PipelineStarted;
            testPipeline.PipelineProgressUpdated += TestPipeline_PipelineProgressUpdated;
            //IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, 
            //                        QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>> httpActivity = new HttpRequestQueueingActivity2();

            var httpActivity = new HttpRequestQueueingActivity();
            var htmlParserActivity = new HtmlParserQueueingActivity();

            // httpActivity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent1;
            httpActivity.QueueingInputBinding.IsQueuePollingEnabled = true;

            var httpActivityConfig = new HttpRequestQueueingActivityConfiguration();
            httpActivityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
            httpActivity.PipelineToolCompleted += httpActivity_PipelineToolCompleted;

            var inputBinding = new QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>();
            var outputBinding = new QueueingProducerChannel<HttpRequestQueueingActivityResult>();



            QueueingPipelineNode httpActivityNode = new QueueingPipelineNode()
            {
                QueueingPipelineTool = httpActivity
            };
            

            QueueingPipelineNode htmlParserNode = new QueueingPipelineNode() 
            {
                QueueingPipelineTool = htmlParserActivity
            };
            // testPipeline.ProcessDefinition = processDefinition;
            // testPipeline.ProcessDefinition.QueueingPipelineNodes.AddLast(new LinkedListNode<IQueueingPipelineNode>(httpActivityNode));
            // testPipeline.ProcessDefinition.QueueingPipelineNodes.AddLast(new LinkedListNode<IQueueingPipelineNode>(htmlParserNode));

            Exception bindEx = null;
            try
            {
                testPipeline.AddFirstPipelineNode(httpActivityNode);
                testPipeline.AddAfterPipelineNode(0, htmlParserNode);

                // TODO test AddLast

                var activityConfig = new HttpRequestQueueingActivityConfiguration();
                activityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
                QueueingPipelineQueueEntity<IPipelineToolConfiguration> entity = new QueueingPipelineQueueEntity<IPipelineToolConfiguration>()
                {
                    Payload = activityConfig
                };

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
            var outputChannel = new QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>();
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
