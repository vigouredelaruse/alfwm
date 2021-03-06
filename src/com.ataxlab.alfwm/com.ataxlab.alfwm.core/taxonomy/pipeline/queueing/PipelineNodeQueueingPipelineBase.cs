﻿using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using System;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{

 
    //public abstract class PipelineNodeQueueingPipelineBase3 : IQueueingPipeline<
    //                                                                                QueueingPipelineProcessDefinition<PipelineToolConfiguration<IPipelineToolConfiguration>,
    //                                                                                                                  QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>,
    //                                                                                                                  QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>,
    //                                                                                                                  PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>>,
    //                                                                                IQueueingPipelineNode>
    //{
    //    public abstract QueueingPipelineProcessDefinition<PipelineToolConfiguration<IPipelineToolConfiguration>, QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>> ProcessDefinition { get; set; }
    //    public abstract string PipelineId { get; set; }
    //    public abstract string PipelineInstanceId { get; set; }
    //    public abstract string PipelineDisplayName { get; set; }
    //    public abstract string PipelineDescription { get; set; }
    //    public abstract IPipelineBinding PipelineInputBinding { get; set; }
    //    public abstract IPipelineBinding PipelineOutputBinding { get; set; }

    //    public abstract event EventHandler<PipelineStartedEventArgs> PipelineStarted;
    //    public abstract event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
    //    public abstract event EventHandler<PipelineFailedEventArgs> PipelineFailed;
    //    public abstract event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

    //    public abstract string AddTool(IQueueingPipelineNode node);
    //    public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
    //    public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
    //    public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
    //    public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
    //    public abstract void StartPipeline(QueueingPipelineProcessDefinition<PipelineToolConfiguration<IPipelineToolConfiguration>, QueueingConsumerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, QueueingProducerChannel<PipelineToolConfiguration<IPipelineToolConfiguration>>, PipelineToolConfiguration<IPipelineToolConfiguration>, PipelineToolConfiguration<IPipelineToolConfiguration>> configuration);
    //    public abstract void StopPipeline(string instanceId);
    //}




    //public abstract class PipelineNodeQueueingPipelineBase : IQueueingPipeline<QueueingPipelineProcessDefinition<QueueingPipelineToolConfiguration,
    //                                                        QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>,
    //                                                        QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>,
    //                                                        QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration>
    //    , QueueingPipelineNode<IQueueingPipelineTool<
    //                                                    QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>,
    //        QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, 
    //        QueueingPipelineToolConfiguration>, 
    //        QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>,
    //        QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration>

    //    >
    //{
    //    public abstract string PipelineId { get; set; }
    //    public abstract string PipelineInstanceId { get; set; }
    //    public abstract string PipelineDisplayName { get; set; }
    //    public abstract string PipelineDescription { get; set; }
    //    public abstract IPipelineBinding PipelineInputBinding { get; set; }
    //    public abstract IPipelineBinding PipelineOutputBinding { get; set; }
    //    public abstract QueueingPipelineProcessDefinition<QueueingPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> ProcessDefinition { get; set; }

    //    public abstract event EventHandler<PipelineStartedEventArgs> PipelineStarted;
    //    public abstract event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
    //    public abstract event EventHandler<PipelineFailedEventArgs> PipelineFailed;
    //    public abstract event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;


    //      public abstract string AddTool(QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration>, QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> node);
    //    public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
    //    public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
    //    public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
    //    public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
    //    public abstract void StartPipeline(QueueingPipelineProcessDefinition<QueueingPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<QueueingPipelineToolConfiguration>>, QueueingPipelineToolConfiguration, QueueingPipelineToolConfiguration> configuration);
    //    public abstract void StopPipeline(string instanceId);
    //}
}