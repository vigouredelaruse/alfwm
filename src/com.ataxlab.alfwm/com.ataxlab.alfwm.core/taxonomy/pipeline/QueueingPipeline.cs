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

        public bool Bind<TInputQEntity, TOutputQEntity>(string SourceInstanceId, string DestinationInstanceId)
        {
            throw new NotImplementedException();
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
            // QueueingPipelineStartResult result = new QueueingPipelineStartResult();

            // return result;
        }

        public void StartPipeline<StartConfiguration>(StartConfiguration configuration)
            where StartConfiguration : class
        {
            throw new NotImplementedException();
        }

        public void StopPipeline(string instanceId)
        {
            throw new NotImplementedException();
        }

        bool IPipeline.AddTool<TPipelineTool, TConfiguration>(TPipelineTool tool, TConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
