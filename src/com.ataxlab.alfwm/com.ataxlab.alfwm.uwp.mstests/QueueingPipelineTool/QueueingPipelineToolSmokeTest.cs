using com.ataxlab.alfwm.core.collections;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
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

            var testPipeline = new PipelineNodeQueueingPipelineEx();

            //IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, 
            //                        QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>, 
            //                        QueueingPipelineQueueEntity<IPipelineToolConfiguration>> httpActivity = new HttpRequestQueueingActivity2();

            var httpActivity = new HttpRequestQueueingActivity2();
  

            // httpActivity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent1;
            httpActivity.QueueingInputBinding.IsQueuePollingEnabled = true;

            var httpActivityConfig = new HttpRequestQueueingActivityConfiguration();
            httpActivityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
            httpActivity.PipelineToolCompleted += Activity_PipelineToolCompleted;

            var inputBinding = new QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>();
            var outputBinding = new QueueingProducerChannel<HttpRequestQueueingActivityResult>();

            // var newNode = new QueueingPipelineNode<HttpRequestQueueingActivity2, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingProducerChannel<HttpRequestQueueingActivityResult>, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityResult>();

            var newerNode = new QueueingPipelineNode2<HttpRequestQueueingActivity2,
                                                    QueueingConsumerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityConfiguration>>,
                                                    QueueingProducerChannel<QueueingPipelineQueueEntity<HttpRequestQueueingActivityResult>>,
                                                    HttpRequestQueueingActivityConfiguration,
                                                    HttpRequestQueueingActivityResult,
                                                    HttpRequestQueueingActivityConfiguration
                                                    >();

            var newNode = new QueueingPipelineNode<HttpRequestQueueingActivity2, 
                            QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, 
                            QueueingProducerChannel<HttpRequestQueueingActivityConfiguration>, 
                            HttpRequestQueueingActivityConfiguration,
                            HttpRequestQueueingActivityConfiguration, 
                            HttpRequestQueueingActivityConfiguration>();
            //newNode.PipelineTool = httpActivity;
            //newNode.PipelineToolInputBinding = httpActivity.InputBinding;
            //newNode.PipelineToolOutputBinding = httpActivity.OutputBinding;


    //        var processDefinition2 = new QueueingPipelineProcessDefinition<HttpRequestQueueingActivityConfiguration, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingProducerChannel<HttpRequestQueueingActivityConfiguration>, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration>
    //();
            // testPipeline.ProcessDefinition.PipelineTools.AddLast(newerNode);
            // testPipeline.ProcessDefinition.AddFirstNode(httpActivity);
            // testPipeline.ProcessDefinition.;
            int iii = 0;

            var tHttpActivity = httpActivity  as IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>; // = httpActivity;
            // testPipeline.ProcessDefinition = processDefinition;
            // testPipeline.ProcessDefinition.AddFirstNode(httpActivity);

            //var id = testPipeline.AddQueueingPipelineNode < QueueingPipelineNode<HttpRequestQueueingActivity, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingProducerChannel<List<Tuple<String, String>>>, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>>,

            //var id = testPipeline.AddQueueingPipelineNode<IQueueingPipelineNode<QueueingPipelineToolBase<HttpRequestQueueingActivityConfiguration, List<Tuple<String, String>>, HttpRequestQueueingActivityConfiguration>>,
            //     QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>,
            //    QueueingProducerChannel<List<Tuple<String, String>>>,
            //    HttpRequestQueueingActivityConfiguration,
            //    List<Tuple<String, String>>,
            //    HttpRequestQueueingActivityConfiguration>(newNode);

                 //var processDefinition = new QueueingPipelineProcessDefinition<(); // TODO  make a TConfiguration that uses PipelineToolVariables
                 // otherwise he linked list of the process definition becomes hardcoded to 1 configuration
                 // and the linked list must specify arbitrary pipelinetool<tconfiguration> nodes


            //var result = testPipeline.AddTool < QueueingPipelineNode<HttpRequestQueueingActivity, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration, HttpRequestQueueingActivityConfiguration>,
            //    QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>,
            //    QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>,
            //    QueueingConsumerChannel<HttpRequestQueueingActivityConfiguration>,
            //    HttpRequestQueueingActivityConfiguration,
            //    HttpRequestQueueingActivityConfiguration,
            //     HttpRequestQueueingActivityConfiguration
            //    > (httpActivity, httpActivityConfig);



            // testPipeline.AddTool<HttpRequestQueueingActivity, HttpRequestQueueingActivityConfiguration>(httpActivity, httpActivityConfig);
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

            //var itemsToInsert = 10;

            //// initialize a tool
            //var toolA = new QueueingPipelineTool<TaskItemPipelineVariable, QueueingPipelineToolConfiguration>();
            //// wire a listener to the tool's queue arrival event
            //toolA.QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData; // += ToolA_InputBinding_QueueHasData;
            //toolA.QueueingInputBinding.IsQueuePollingEnabled = true;

            //var toolB = new QueueingPipelineTool<TaskItemPipelineVariable, QueueingPipelineToolConfiguration>();
            //// wire a listener to the tool's queue arrival event
            //toolB.QueueingInputBinding.QueueHasData += QueueingInputBinding_QueueHasData1; //  += ToolB_InputBinding_QueueHasData1;

            //// wire Tool B downstream from Tool A
            //toolA.QueueingOutputBinding.Add(toolB.QueueingInputBinding);

            //for (int itemNo = 0; itemNo < itemsToInsert; itemNo++)
            //{
            //    // initialize a new pipeline tuple
            //    var newItem = this.GetNewQueueEntity(itemNo);

            //    // post new item to Tool A input Q
            //    toolA.InputBinding.InputQueue.Enqueue(newItem);
            //}

            //// pause the test while the queue processes the inputs
            //Thread.Sleep(15000);

            //// validate the received work item queues have the right counts
            //Assert.IsTrue(ToolAWorkItemQueue.Count == itemsToInsert, "test failed, incorrect number of items on receive queue");

            //int i = 0;
        }

        private void QueueingInputBinding_QueueHasData1(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TaskItemPipelineVariable>> e)
        {
            throw new NotImplementedException();
        }

        private void QueueingInputBinding_QueueHasData(object sender, QueueDataAvailableEventArgs<QueueingPipelineQueueEntity<TaskItemPipelineVariable>> e)
        {
            throw new NotImplementedException();
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

        //[TestMethod]
        //public async Task HttpToolSmokeTest()
        //{
        //    // get an image as a byte array
        //    byte[] imageAsBytes;
        //    imageAsBytes = await GetSampleImageAsBytes();


        //    int i = imageAsBytes.Length;

        //    // initialize the tesseract tool
        //    PipelineVariable testTuple = new PipelineVariable();
        //    testTuple.Payload = imageAsBytes;

        //    var activity = new HttpRequestQueueingActivity();
        //    activity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent1;
        //    activity.InputBinding.IsQueuePollingEnabled = true;

        //    var activityConfig = new HttpRequestQueueingActivityConfiguration();
        //    activityConfig.RequestMessage = new System.Net.Http.HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri("https://www.cnn.com") };
        //    activity.PipelineToolCompleted += Activity_PipelineToolCompleted;

        //    var outputQueue = activity.OutputBinding;
        //    outputQueue.QueueHasData += OutputQueue_QueueHasData;
        //    activity.InputBinding.InputQueue.Enqueue(activityConfig);

        //    Thread.Sleep(30000);
        //    var loopMax = 60;
        //    var loopCounter = 0;
        //    var outQMsg = new List<Tuple<String, String>>();

        //    while(loopCounter++ < loopMax && didSignalDownstreamActivity == false)
        //    {
        //         Thread.Sleep(1000);
        //    }

        //    Assert.IsTrue(didSignalDownstreamActivity, "Failed to dequeue output q message");

        //    Assert.IsTrue(didfirePipelineToolCompleted, "test failed, did not fire activity completed event");
        //}

        bool didSignalDownstreamActivity = false;
        /// <summary>
        /// signal from outputut channel that outputut queue has data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputQueue_QueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<List<Tuple<string, string>>> e)
        {
            int i = 0;
            didSignalDownstreamActivity = true;
        }

        private void Activity_PipelineToolCompleted(object sender, core.taxonomy.PipelineToolCompletedEventArgs e)
        {
            didfirePipelineToolCompleted = true;
        }

        bool didFireQueueHasAvailableDataEvent = false;
        bool didfirePipelineToolCompleted = false;
        private HttpRequestQueueingActivityConfiguration Activity_QueueHasAvailableDataEvent1(HttpRequestQueueingActivityConfiguration arg)
        {
            Assert.IsNotNull(arg, "test failed: queue arrival data");
            this.didFireQueueHasAvailableDataEvent = true;
            return arg;
        }

        ConcurrentQueue<TaskItemPipelineVariable> TesseractWorkQueue { get; set; }

        private PipelineVariable Activity_QueueHasAvailableDataEvent(PipelineVariable arg)
        {
            PipelineVariable v = new PipelineVariable();
            return v;
        }

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
