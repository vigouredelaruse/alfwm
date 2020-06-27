using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.activity.ocr.tesseract.model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Tesseract;

namespace com.ataxlab.alfwm.library.activity.ocr.tesseract
{
    /// <summary>
    /// pipeline tool that performs ocr using tesseract
    /// </summary>
    /// <typeparam name="TInputBinding"></typeparam>
    /// <typeparam name="TOutputBinding"></typeparam>
    /// <typeparam name="TQueueEntity"></typeparam>
    public class TesseractActivity:
        IQueueingPipelineTool<QueueingChannel<TesseractPipelineVariable>, QueueingChannel<TesseractPipelineVariable>, TesseractPipelineVariable>
    {

        /// <summary>
        /// path to tesseract language data files
        /// </summary>
        private string TesseractLanguateFilesPath { get; set; }

        private System.Timers.Timer WorkQueueProcessTimer { get; set; }

        private ConcurrentQueue<TesseractPipelineVariable> WorkQueue = new ConcurrentQueue<TesseractPipelineVariable>();

        public TesseractActivity()
        {
            // get the path to the tesseract trained files
            TesseractLanguateFilesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"tessdata");

            // initialize the input channel
            this.InputBinding = new QueueingChannel<TesseractPipelineVariable>();
            this.QueueingOutputBindingCollection = new List<QueueingChannel<TesseractPipelineVariable>>();

            // register a listener to the input queue
            InputBinding.QueueHasData += OnInputBindingQueueHasData;

            // this timer event handler pulls items from the 
            // work item queue
            // it is autoreset = false to prevent 
            // overlapping event requests
            WorkQueueProcessTimer = new System.Timers.Timer();
            WorkQueueProcessTimer.AutoReset = false;
            WorkQueueProcessTimer.Interval = 50;
            WorkQueueProcessTimer.Elapsed += WorkQueueProcessTimer_Elapsed;
            WorkQueueProcessTimer.Enabled = true;

        }

        private void WorkQueueProcessTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (WorkQueue.Count > 0)
            {
                try
                {
                    TesseractPipelineVariable workitem;
                    WorkQueue.TryDequeue(out workitem);

                    byte[] sample = workitem.Payload as byte[];
                    
                    using (var engine = new TesseractEngine(TesseractLanguateFilesPath, "eng", EngineMode.LstmOnly))
                    {
                        using (var img = Pix.LoadFromMemory(sample))
                        {
                            using (var page = engine.Process(img))
                            {
                                var text = page.GetText();
                                Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                                Console.WriteLine("Text (GetText): \r\n{0}", text);
                                Console.WriteLine("Text (iterator):");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    int i = 0;
                }
            }
            else
            {
                // nothing to do queue is empty
            }

            // restart the timer
            WorkQueueProcessTimer.Enabled = true;

            Debug.WriteLine("work queue timer elapsed");
        }

        
        /// <summary>
        /// handle the tesseract job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputBindingQueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<TesseractPipelineVariable> e)
        {
            // cache arrivals on the queue for processing 
            // in the event processing timer elapsed handler
            WorkQueue.Enqueue(e.EventPayload);
        }

        public QueueingChannel<TesseractPipelineVariable> InputBinding { get; set ; }
        public List<QueueingChannel<TesseractPipelineVariable>> QueueingOutputBindingCollection { get; set; }
        public string InstanceId { get; set; }
        public IPipelineToolStatus Status { get; set; }
        public IPipelineToolContext Context { get; set; }
        public IPipelineToolConfiguration Configuration { get; set; }
        public IPipelineToolBinding OutputBinding { get; set; }
        public string PipelineToolId { get; set; }
        public string DisplayName { get; set ; }
        public string Description { get; set ; }

        public event Func<TesseractPipelineVariable, TesseractPipelineVariable> QueueHasAvailableDataEvent;
        public event EventHandler<PipelineToolStartEventArgs> PipelineToolStarted;
        public event EventHandler<PipelineToolProgressUpdatedEventArgs> PipelineToolProgressUpdated;
        public event EventHandler<PipelineToolFailedEventArgs> PipelineToolFailed;
        public event EventHandler<PipelineToolCompletedEventArgs> PipelineToolCompleted;

        public void OnPipelineToolCompleted<TPayload>(object sender, PipelineToolCompletedEventArgs<TPayload> args) where TPayload : class
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolFailed(object sender, PipelineToolFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineToolStarted(object sender, PipelineToolStartEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnQueueHasData(object sender, TesseractPipelineVariable availableData)
        {
            int i = 0;
            i = 1;
        }

        public void Start<StartResult, StartConfiguration>(StartConfiguration configuration, Func<StartConfiguration, StartResult> callback)
            where StartResult : class, new()
            where StartConfiguration : class, new()
        {
            throw new NotImplementedException();
        }

        public void Start<StartConfiguration>(StartConfiguration configuration, Action<StartConfiguration> callback) where StartConfiguration : class
        {
            throw new NotImplementedException();
        }

        public StopResult Stop<StopResult>(string instanceId) where StopResult : IPipelineToolStatus, new()
        {
            throw new NotImplementedException();
        }
    }
}
