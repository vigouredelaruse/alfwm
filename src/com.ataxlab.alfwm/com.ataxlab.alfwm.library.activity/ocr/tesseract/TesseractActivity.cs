﻿using com.ataxlab.alfwm.core.taxonomy;
using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.pipeline;
using com.ataxlab.alfwm.library.activity.ocr.tesseract.model;
using System;
using System.Collections.Generic;
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

        public TesseractActivity()
        {
            // get the path to the tesseract trained files
            // TesseractLanguateFilesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"tessdata");

            // initialize the input channel
            this.InputBinding = new QueueingChannel<TesseractPipelineVariable>();
            this.QueueingOutputBindingCollection = new List<QueueingChannel<TesseractPipelineVariable>>();

            // register a listener to the input queue
            InputBinding.QueueHasData += OnInputBindingQueueHasData;


        }

        /// <summary>
        /// handle the tesseract job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputBindingQueueHasData(object sender, core.taxonomy.binding.queue.QueueDataAvailableEventArgs<TesseractPipelineVariable> e)
        {

            try
            {

                byte[] sample = e.EventPayload.Payload as byte[];

                using (var engine = new TesseractEngine(TesseractLanguateFilesPath, "eng", EngineMode.Default))
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
            catch(Exception ex)
            {

            }
        }

        public QueueingChannel<TesseractPipelineVariable> InputBinding { get; set ; }
        public List<QueueingChannel<TesseractPipelineVariable>> QueueingOutputBindingCollection { get; set; }
        public string InstanceId { get; set; }
        public IPipelineToolStatus Status { get; set; }
        public IPipelineToolContext Context { get; set; }
        public IPipelineToolConfiguration Configuration { get; set; }
        public IPipelineToolBinding OutputBinding { get; set; }

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
