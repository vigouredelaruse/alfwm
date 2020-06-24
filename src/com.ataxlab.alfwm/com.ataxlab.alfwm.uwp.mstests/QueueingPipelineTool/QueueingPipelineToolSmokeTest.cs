using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.activity.ocr.tesseract;
using com.ataxlab.alfwm.library.activity.ocr.tesseract.model;
using com.ataxlab.alfwm.uwp.mstests.datasetprovider.litedb.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        private ConcurrentQueue<TesseractPipelineVariable> TesseractToolWorkItemQueue { get; set; }
        private ConcurrentQueue<TaskItemPipelineVariable> ToolAWorkItemQueue { get; set; }
        private ConcurrentQueue<TaskItemPipelineVariable> ToolBWorkItemQueue { get; set; }

        [TestInitialize]
        public void Setup()
        {
            ToolAWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
            ToolBWorkItemQueue = new ConcurrentQueue<TaskItemPipelineVariable>();
            TesseractWorkQueue = new ConcurrentQueue<TesseractPipelineVariable>();

            TesseractToolWorkItemQueue = new ConcurrentQueue<TesseractPipelineVariable>();
        }

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
            QueueingPipelineTool<TaskItemPipelineVariable> toolA = new QueueingPipelineTool<TaskItemPipelineVariable>();
            // wire a listener to the tool's queue arrival event
            toolA.InputBinding.QueueHasData += ToolA_InputBinding_QueueHasData;
            toolA.InputBinding.IsQueuePollingEnabled = true;

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

            // pause the test while the queue processes the inputs
            Thread.Sleep(15000);

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

        #endregion default queueing tool tests

        #region tesseract tool tests

        [TestMethod]
        public async Task TesseractToolSmokeTest()
        {
            // get an image as a byte array
            byte[] imageAsBytes;
            imageAsBytes = await GetSampleImageAsBytes();

            int i = imageAsBytes.Length;

            // initialize the tesseract tool
            TesseractPipelineVariable testTuple = new TesseractPipelineVariable();
            testTuple.Payload = imageAsBytes;

            TesseractActivity activity = new TesseractActivity();
            activity.QueueHasAvailableDataEvent += Activity_QueueHasAvailableDataEvent;
            activity.InputBinding.IsQueuePollingEnabled = true;

            activity.InputBinding.InputQueue.Enqueue(testTuple);

            Thread.Sleep(10000);

            Assert.IsTrue(activity.InputBinding.InputQueue.Count > 0, "test failed, input queue count incorrect");
        }

        ConcurrentQueue<TesseractPipelineVariable> TesseractWorkQueue { get; set; }

        private TesseractPipelineVariable Activity_QueueHasAvailableDataEvent(TesseractPipelineVariable arg)
        {
            TesseractPipelineVariable v = new TesseractPipelineVariable();
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

        #endregion tesseract tool tests
    }
}
