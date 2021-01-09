using com.ataxlab.alfwm.core.taxonomy.binding;
using com.ataxlab.alfwm.core.taxonomy.binding.queue;
using com.ataxlab.alfwm.core.taxonomy.processdefinition;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    [Obsolete]
    public class QueueingPipeline : IDefaultQueueingPipeline
    {
        public QueueingPipeline()
        {
            ProcessDefinition = new DefaultQueueingPipelineProcessDefinition();
        }

        public IDefaultQueueingPipelineProcessDefinition ProcessDefinition { get; set; }
        public string PipelineId { get; set; }
        public string PipelineInstanceId { get; set; }
        public string PipelineDisplayName { get; set; }
        public string PipelineDescription { get; set; }
        public IPipelineBinding PipelineInputBinding { get; set; }
        public IPipelineBinding PipelineOutputBinding { get; set; }
        public QueueingConsumerChannel<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingInputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<IPipelineToolConfiguration>> QueueingOutputBinding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        public event EventHandler<PipelineDeploymentFailedEventArgs> PipelineDeploymentFailed;

        public bool Bind(string SourceInstanceId, string DestinationInstanceId)
        {
            return false;
        }

        public void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args)
        {
            PipelineCompleted?.Invoke(this, args);
        }

        public void OnPipelineFailed(object sender, PipelineFailedEventArgs args)
        {
            PipelineFailed?.Invoke(this, args);
        }

        public void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args)
        {
            PipelineProgressUpdated?.Invoke(this, args);
        }

        public void OnPipelineStarted(object sender, PipelineStartedEventArgs args)
        {
            PipelineStarted?.Invoke(this, args);
        }

        public void StartPipeline(IDefaultQueueingPipelineProcessDefinition configuration)
        {
            this.ProcessDefinition = configuration;
        }



        public void StopPipeline(string instanceId)
        {
            // throw new NotImplementedException();
        }

        public bool AddTool<TPipelineTool, TConfiguration>(TPipelineTool tool, TConfiguration configuration)
            where TPipelineTool : class, IPipelineTool<TConfiguration>, new()
            where TConfiguration : class, new()
        {

            // create the pipeline tool node
            // var node =  QueueingPipelineNode<TPipelineTool, TConfiguration>
            return true;
        }

        public bool AddQueueingPipelineNode<TPipelineToolNode, TLatchingInputBinding, TOutputBinding, TInputQueueENtity, TOutputQueueEntity, TConfiguration>(TPipelineToolNode node)
            where TPipelineToolNode : class, IQueueingPipelineTool<TLatchingInputBinding, TOutputBinding, TInputQueueENtity, TOutputQueueEntity, TConfiguration>, new()
             where TLatchingInputBinding : class, IQueueConsumerPipelineToolBinding<QueueingPipelineQueueEntity<TInputQueueENtity>>, new()
              where TOutputBinding : class, IQueueProducerPipelineToolBinding<QueueingPipelineQueueEntity<TOutputQueueEntity>>, new()
            where TInputQueueENtity : class, IPipelineToolConfiguration, new()
            where TOutputQueueEntity : class, IPipelineToolConfiguration, new()
            where TConfiguration : class, IPipelineToolConfiguration, new() // class, new()
        {
            var newNode = new QueueingPipelineNode<TPipelineToolNode, TLatchingInputBinding, TOutputBinding, TConfiguration, TInputQueueENtity, TOutputQueueEntity>() { PipelineTool = node };

            // this.ProcessDefinition.PipelineToolChain
            return true;
        }

        public bool AddAfterPipelineNode(int pipelineNodeIndex, QueueingPipelineToolNode newNode)
        {
            throw new NotImplementedException();
        }

        public bool AddFirstPipelineNode(QueueingPipelineToolNode newNode)
        {
            throw new NotImplementedException();
        }

        public bool AddLastPipelineNode(QueueingPipelineToolNode newNode)
        {
            throw new NotImplementedException();
        }

        public void Deploy(DefaultQueueingPipelineProcessDefiniionEntity processDefinition)
        {
            throw new NotImplementedException();
        }

        public void EnsurePipelineIngressEgressBindings()
        {
            throw new NotImplementedException();
        }

        public void EnsurePipelineToolListeners(QueueingPipelineToolNode newNode)
        {
            throw new NotImplementedException();
        }

        public void OnPipelineDeploymentFailed(object sender, PipelineDeploymentFailedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class QueueingPipeline<TProcessDefinition, TPipelineNode> : IQueueingPipeline<TProcessDefinition, TPipelineNode>
    {
        public abstract TProcessDefinition ProcessDefinition { get; set; }
        public abstract string PipelineId { get; set; }
        public abstract string PipelineInstanceId { get; set; }
        public abstract string PipelineDisplayName { get; set; }
        public abstract string PipelineDescription { get; set; }
        public abstract IPipelineBinding PipelineInputBinding { get; set; }
        public abstract IPipelineBinding PipelineOutputBinding { get; set; }

        public abstract event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public abstract event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public abstract event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public abstract event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;
        public abstract event EventHandler<PipelineDeploymentFailedEventArgs> PipelineDeploymentFailed;

        public abstract string AddTool(TPipelineNode node);
        public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
        public abstract void OnPipelineDeploymentFailed(object sender, PipelineDeploymentFailedEventArgs args);
        public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
        public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
        public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
        public abstract void StartPipeline(TProcessDefinition configuration);
        public abstract void StopPipeline(string instanceId);
    }
}
