using com.ataxlab.alfwm.core.taxonomy.binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.taxonomy.pipeline
{
    /// <summary>
    /// convenience implementation of IPipeline 
    /// so implementors don't have to begin from the bare
    /// interface specification
    /// </summary>
    [Obsolete]
    public abstract class PipelineBase : IPipeline
    {
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

        public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
        public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
        public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
        public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
        public abstract void StartPipeline<StartConfiguration>(StartConfiguration configuration)
            where StartConfiguration : class;
        public abstract void StopPipeline(string instanceId);

    }

    /// <summary>
    /// this is hopefully exactly the minimally viable composition
    /// of a Pipeline
    /// </summary>
    /// <typeparam name="TProcessDefinition"></typeparam>
    public abstract class PipelineBase<TProcessDefinition> : IPipeline<TProcessDefinition>
        
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

        public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
        public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
        public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
        public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);
        public abstract void StartPipeline<StartConfiguration>(StartConfiguration configuration)
            where StartConfiguration : class;
        public abstract void StartPipeline(TProcessDefinition configuration);
        public abstract void StopPipeline(string instanceId);


    }
}
