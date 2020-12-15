﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class PipelineNodeQueueingPipeline3 : PipelineNodeQueueingPipelineBase3
    {
        public override QueueingPipelineProcessDefinition<PipelineToolConfiguration<IPipelineToolConfiguration>, QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>> ProcessDefinition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineInstanceId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineBinding PipelineInputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineBinding PipelineOutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public override event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public override event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public override event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public override string AddTool(IQueueingPipelineTool<QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>> node)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void StartPipeline(QueueingPipelineProcessDefinition<PipelineToolConfiguration<IPipelineToolConfiguration>, QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>> configuration)
        {
            throw new NotImplementedException();
        }

        public override void StopPipeline(string instanceId)
        {
            throw new NotImplementedException();
        }
    }

    public class PipelineNodeQueueingPipeline2 : PipelineNodeQueueingPipelineBase2
    {
        public override QueueingPipelineProcessDefinition<IPipelineToolConfiguration, QueueingConsumerChannel<IPipelineToolConfiguration>, QueueingProducerChannel<IPipelineToolConfiguration>, IPipelineToolConfiguration, IPipelineToolConfiguration> ProcessDefinition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineInstanceId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineDisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string PipelineDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineBinding PipelineInputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPipelineBinding PipelineOutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public override event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public override event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public override event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public override string AddTool(IQueueingPipelineTool<QueueingConsumerChannel<IPipelineToolConfiguration>, QueueingProducerChannel<IPipelineToolConfiguration>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>  node)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void StartPipeline(QueueingPipelineProcessDefinition<IPipelineToolConfiguration, QueueingConsumerChannel<IPipelineToolConfiguration>, QueueingProducerChannel<IPipelineToolConfiguration>, IPipelineToolConfiguration, IPipelineToolConfiguration> configuration)
        {
            throw new NotImplementedException();
        }

        public override void StopPipeline(string instanceId)
        {
            throw new NotImplementedException();
        }
    }

    public class PipelineNodeQueueingPipeline : PipelineNodeQueueingPipelineBase
    {
        public override QueueingPipelineProcessDefinition<QueueingPipelineToolConfiguration, 
                            QueueingConsumerChannel<QueueingPipelineToolConfiguration>, 
                            QueueingProducerChannel<QueueingPipelineToolConfiguration>, 
                            QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> ProcessDefinition { get; set; }
        public override string PipelineId { get; set; }
        public override string PipelineInstanceId { get; set; }
        public override string PipelineDisplayName { get; set; }
        public override string PipelineDescription { get; set; }
        public override IPipelineBinding PipelineInputBinding { get; set; }
        public override IPipelineBinding PipelineOutputBinding { get; set; }

        public override event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public override event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public override event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public override event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public override string AddTool(QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineToolConfiguration>, QueueingProducerChannel<QueueingPipelineToolConfiguration>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration>, QueueingConsumerChannel<QueueingPipelineToolConfiguration>, QueueingProducerChannel<QueueingPipelineToolConfiguration>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> node)
        {
            ProcessDefinition.PipelineToolChain.AddLast(node);
            node.PipelineTool.PipelineToolCompleted += PipelineTool_PipelineToolCompleted;
            node.PipelineTool.PipelineToolFailed += PipelineTool_PipelineToolFailed;
            node.PipelineTool.PipelineToolProgressUpdated += PipelineTool_PipelineToolProgressUpdated;
            node.PipelineTool.PipelineToolStarted += PipelineTool_PipelineToolStarted;

            
            return node.QueueingPipelineNodeId;
        }

        private void PipelineTool_PipelineToolStarted(object sender, PipelineToolStartEventArgs e)
        {
            OnPipelineStarted(sender, new PipelineStartedEventArgs() 
            {  
                SourceEvent = e,
                PipelineDisplayName = this.PipelineDisplayName

            });
        }

        private void PipelineTool_PipelineToolProgressUpdated(object sender, PipelineToolProgressUpdatedEventArgs e)
        {
            PipelineProgressUpdated?.Invoke(sender, new PipelineProgressUpdatedEventArgs());
        }

        private void PipelineTool_PipelineToolFailed(object sender, PipelineToolFailedEventArgs e)
        {
            PipelineFailed?.Invoke(sender, new PipelineFailedEventArgs());
        }

        private void PipelineTool_PipelineToolCompleted(object sender, PipelineToolCompletedEventArgs e)
        {
            PipelineCompleted?.Invoke(sender, new PipelineCompletedEventArgs());
        }

        public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            PipelineCompleted?.Invoke(sender, args);   
        }

        public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            PipelineFailed?.Invoke(sender, args);
        }

        public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            PipelineProgressUpdated?.Invoke(sender, args);
        }

        public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            PipelineStarted?.Invoke(sender, args);
        }

        public override void StartPipeline(QueueingPipelineProcessDefinition<QueueingPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineToolConfiguration>, QueueingProducerChannel<QueueingPipelineToolConfiguration>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> configuration)
        {
            this.ProcessDefinition = configuration;
        }

        public override void StopPipeline(string instanceId)
        {
            // todo do something useful here
        }
    }

    //public class PipelineNodeQueueingPipeline<TProcessDefinition,
    //                                            TPipelineTool,
    //                                            TLatchingInputBinding,
    //                                            TOutputBinding,
    //                                            TPipelineToolConfiguration,
    //                                            TInputEntity,
    //                                            TOutputEntity
    //                                            > : PipelineNodeQueueingPipelineBase
    //    where TProcessDefinition : class, IQueueingPipelineProcessDefinition<TPipelineTool, TLatchingInputBinding, TOutputBinding, TPipelineToolConfiguration, TInputEntity, TOutputEntity>, new()
    //    where TPipelineTool :  IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputEntity, TOutputEntity, TPipelineToolConfiguration >
    //    where TLatchingInputBinding : class, IQueueConsumerPipelineToolBinding<TInputEntity>,  new()
    //    where TOutputBinding : class, IQueueProducerPipelineToolBinding<TOutputEntity>, new()
    //    where TPipelineToolConfiguration : class, IPipelineToolConfiguration, new()
    //    where TInputEntity : class, new()
    //    where TOutputEntity : class, new()
    //{
    //    public override TProcessDefinition ProcessDefinition { get; set; }
    //    public override string PipelineId { get; set; }
    //    public override string PipelineInstanceId { get; set; }
    //    public override string PipelineDisplayName { get; set; }
    //    public override string PipelineDescription { get; set; }
    //    public override IPipelineBinding PipelineInputBinding { get; set; }
    //    public override IPipelineBinding PipelineOutputBinding { get; set; }

    //    public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
    //    public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
    //    public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
    //    public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

    //    public override void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void StartPipeline(TProcessDefinition configuration)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void StopPipeline(string instanceId)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
