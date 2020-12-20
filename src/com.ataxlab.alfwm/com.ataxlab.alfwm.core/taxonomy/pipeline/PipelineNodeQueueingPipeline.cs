using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{

    public class PipelineNodeQueueingPipeline2 : IQueueingPipeline
    {
        public PipelineNodeQueueingPipeline2()
        {
            this.ProcessDefinition = new DefaultQueueingPipelineProcessDefinition();
        }

        public string PipelineId { get; set; }
        public string PipelineInstanceId { get; set; }
        public string PipelineDisplayName { get; set; }
        public string PipelineDescription { get; set; }
        public IPipelineBinding PipelineInputBinding { get; set; }
        public IPipelineBinding PipelineOutputBinding { get; set; }
        public IDefaultQueueingPipelineProcessDefinition ProcessDefinition { get; set; }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public bool Bind(string SourceInstanceId, string DestinationInstanceId)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void StartPipeline(IDefaultQueueingPipelineProcessDefinition configuration)
        {
            throw new NotImplementedException();
        }

        public void StopPipeline(string instanceId)
        {
            throw new NotImplementedException();
        }
    }


    public class PipelineNodeQueueingPipelineEx : PipelineNodeQueueingPipelineBaseEx
        <
                    IQueueingPipelineTool<
                QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration>,
            IQueueingPipelineTool<
                QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration,
                IPipelineToolConfiguration>,
            QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
            QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>,
            IPipelineToolConfiguration,
            IPipelineToolConfiguration,
            IPipelineToolConfiguration
        >



    {

        public PipelineNodeQueueingPipelineEx()
        {
            this.ProcessDefinition = new QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration>();

        }

        public override QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration> ProcessDefinition { get; set; }
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

        public override string AddTool(IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration> node)
        {
            int i = 0;
            return "";
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

        public override void StartPipeline(QueueingPipelineProcessDefinitionEx<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>, IPipelineToolConfiguration, QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration> configuration)
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

        public override string AddTool(QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>> node)
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

        public override void StartPipeline(QueueingPipelineProcessDefinition<QueueingPipelineNode<IQueueingPipelineTool<QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, QueueingProducerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>>, IPipelineToolConfiguration, IPipelineToolConfiguration, IPipelineToolConfiguration>>> configuration)
        {
            throw new NotImplementedException();
        }

        public override void StopPipeline(string instanceId)
        {
            throw new NotImplementedException();
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
