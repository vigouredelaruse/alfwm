using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    public class QueueingPipeline : IQueueingPipeline
    {
        public IQueueingPipelineProcessDefinition ProcessDefinition { get; set; }
        public string PipelineId { get; set; }
        public string PipelineInstanceId { get; set; }
        public string PipelineDisplayName { get; set; }
        public string PipelineDescription { get; set; }
        public IPipelineBinding PipelineInputBinding { get; set; }
        public IPipelineBinding PipelineOutputBinding { get; set; }

        public event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

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

        public void StartPipeline(IQueueingPipelineProcessDefinition configuration)
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
            where TLatchingInputBinding : class, new()
            where TOutputBinding : class, new()
            where TInputQueueENtity : class, new()
            where TOutputQueueEntity : class, new()
            where TConfiguration : class, new()
        {
            var newNode = new QueueingPipelineNode<TPipelineToolNode, TLatchingInputBinding, TOutputBinding, TConfiguration, TInputQueueENtity, TOutputQueueEntity>() { PipelineTool = node };

            return true;
        }
    }
}
