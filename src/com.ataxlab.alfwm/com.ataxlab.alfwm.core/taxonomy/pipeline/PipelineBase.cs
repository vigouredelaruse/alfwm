﻿using com.ataxlab.alfwm.core.taxonomy.binding;
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
        public abstract string InstanceId { get; set; }
        public abstract IPipelineBinding InputBinding { get; set; }
        public abstract IPipelineBinding OutputBinding { get; set; }

        public abstract event EventHandler<PipelineStartedEventArgs> PipelineStarted;
        public abstract event EventHandler<PipelineProgressUpdatedEventArgs> PipelineProgressUpdated;
        public abstract event EventHandler<PipelineFailedEventArgs> PipelineFailed;
        public abstract event EventHandler<PipelineCompletedEventArgs> PipelineCompleted;

        public abstract void OnPipelineCompleted(object sender, PipelineCompletedEventArgs args);
        public abstract void OnPipelineFailed(object sender, PipelineFailedEventArgs args);
        public abstract void OnPipelineProgressUpdated(object sender, PipelineProgressUpdatedEventArgs args);
        public abstract void OnPipelineStarted(object sender, PipelineStartedEventArgs args);




        public abstract StartResult Start<StartResult, StartConfiguration>(StartConfiguration configuration)
            where StartResult : class
            where StartConfiguration : class;
        public abstract StopResult Stop<StopResult>(string instanceId) where StopResult : class;
    }
}
