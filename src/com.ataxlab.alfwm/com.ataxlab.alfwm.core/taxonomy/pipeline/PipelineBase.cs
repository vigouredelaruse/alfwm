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
        public abstract StartResult StartPipeline<StartResult, StartConfiguration>(StartConfiguration configuration)
            where StartResult : class
            where StartConfiguration : class;
        public abstract StopResult StopPipeline<StopResult>(string instanceId) where StopResult : class;

        bool IPipeline.AddTool<TPipelineTool, TConfiguration>(TPipelineTool tool, TConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// this is hopefully exactly the minimally viable composition
    /// of a Pipeline
    /// </summary>
    /// <typeparam name="TProcessDefinition"></typeparam>
    public abstract class PipelineBase<TProcessDefinition> : IPipeline<TProcessDefinition>
        where TProcessDefinition : class, new()
    {
        public abstract TProcessDefinition PipelineTools { get; set; }
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
        public abstract StartResult StartPipeline<StartResult, StartConfiguration>(StartConfiguration configuration)
            where StartResult : class
            where StartConfiguration : class;
        public abstract StartResult StartPipeline<StartResult>(TProcessDefinition configuration) where StartResult : class;
        public abstract StopResult StopPipeline<StopResult>(string instanceId) where StopResult : class;

        public abstract bool AddTool<TPipelineTool, TConfiguration>(TPipelineTool tool, TConfiguration configuration)
            where TPipelineTool : class, IPipelineTool<TConfiguration>, new()
            where TConfiguration : class, new();

    }
}
